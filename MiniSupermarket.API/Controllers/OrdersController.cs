/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: Controller Orders - Thanh toán hóa đơn POS, trừ kho và tích/đổi điểm thưởng (Tiêu chí 42, 47)
 *        + Quản lý đơn hàng: xem danh sách, chi tiết, cập nhật trạng thái, hủy đơn
 */
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Dtos;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private const decimal AmountPerPoint  = 10_000m;  // 10.000đ = 1 điểm tích lũy
        private const decimal DiscountPerPoint = 1_000m;  // 1 điểm  = 1.000đ trừ vào hóa đơn

        private readonly SupermarketDbContext _db;

        public OrdersController(SupermarketDbContext db)
        {
            _db = db;
        }

        // =====================================================================
        // DTOs nội bộ cho Checkout
        // =====================================================================

        public class CheckoutItem
        {
            public int     ProductId  { get; set; }
            public int     Quantity   { get; set; }
            public decimal UnitPrice  { get; set; }  // chỉ để tham khảo, server dùng giá trong DB
        }

        public class CheckoutRequest
        {
            public string? CashierUsername  { get; set; }
            public string? CustomerPhone    { get; set; }

            // Tiêu chí 47: Điểm thưởng muốn dùng để giảm tiền hóa đơn
            public int PointsToRedeem { get; set; } = 0;

            [Required, MinLength(1)]
            public List<CheckoutItem> Items { get; set; } = new();
        }

        // =====================================================================
        // POST api/orders/checkout
        // Tiêu chí 47: Cập nhật đồng thời trừ điểm trong Customers
        //              và trừ tiền thanh toán FinalAmount trong cùng một Transaction
        // =====================================================================
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng đang trống, không thể thanh toán." });

            if (req.Items.Any(i => i.Quantity <= 0))
                return BadRequest(new { message = "Số lượng mỗi sản phẩm phải lớn hơn 0." });

            // Gộp các dòng trùng sản phẩm
            var lines = req.Items
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToList();

            var ids      = lines.Select(l => l.ProductId).ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToListAsync();

            if (products.Count != ids.Count)
                return BadRequest(new { message = "Có sản phẩm trong giỏ không còn tồn tại trên hệ thống." });

            // Kiểm tra tồn kho trước khi thanh toán
            foreach (var l in lines)
            {
                var p = products.First(x => x.ProductId == l.ProductId);
                if (p.StockQuantity < l.Quantity)
                {
                    return BadRequest(new
                    {
                        message = $"Sản phẩm \"{p.ProductName}\" chỉ còn {p.StockQuantity} trong kho (yêu cầu {l.Quantity})."
                    });
                }
            }

            // Khách hàng thân thiết (nếu có số điện thoại)
            Customer? customer = null;
            string phone = (req.CustomerPhone ?? string.Empty).Trim();
            if (phone.Length > 0)
            {
                customer = await _db.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phone);
                if (customer == null)
                    return BadRequest(new { message = "Không tìm thấy khách hàng với số điện thoại này." });
            }

            // Bắt đầu Transaction bảo vệ ACID
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    OrderDate        = DateTime.Now,
                    CashierUsername  = User.Identity?.Name ?? req.CashierUsername ?? "unknown",
                    CustomerId       = customer?.CustomerId,
                    Status           = OrderStatus.Completed
                };

                // Trừ tồn kho và lưu chi tiết hóa đơn
                foreach (var l in lines)
                {
                    var p = products.First(x => x.ProductId == l.ProductId);
                    p.StockQuantity -= l.Quantity;

                    order.Details.Add(new OrderDetail
                    {
                        ProductId = p.ProductId,
                        Quantity  = l.Quantity,
                        UnitPrice = p.Price
                    });
                }

                decimal grossTotal     = order.Details.Sum(d => d.UnitPrice * d.Quantity);
                decimal discountAmount = 0m;
                int     pointsRedeemed = 0;

                // =========================================================================
                // TIÊU CHÍ 47: ĐỔI ĐIỂM THƯỞNG TRỪ TIỀN HÓA ĐƠN
                // Nghiệp vụ : Dùng 50 điểm trừ 50.000 đ vào hóa đơn (1 điểm = 1.000 VNĐ)
                // Kỹ thuật  : Cập nhật đồng thời trừ điểm trong Customers và trừ TotalAmount
                // =========================================================================
                if (req.PointsToRedeem > 0)
                {
                    if (customer == null)
                        return BadRequest(new { message = "Không thể áp dụng đổi điểm cho khách vãng lai." });

                    if (customer.RewardPoints < req.PointsToRedeem)
                        return BadRequest(new
                        {
                            message = $"Khách hàng chỉ có {customer.RewardPoints} điểm, không đủ để đổi {req.PointsToRedeem} điểm."
                        });

                    decimal requestedDiscount = req.PointsToRedeem * DiscountPerPoint;
                    if (requestedDiscount > grossTotal)
                    {
                        requestedDiscount = grossTotal;
                        pointsRedeemed    = (int)(requestedDiscount / DiscountPerPoint);
                    }
                    else
                    {
                        pointsRedeemed = req.PointsToRedeem;
                    }

                    discountAmount = pointsRedeemed * DiscountPerPoint;
                    customer.RewardPoints -= pointsRedeemed;
                }

                // Tiền thanh toán cuối cùng sau khi trừ điểm thưởng
                order.TotalAmount = Math.Max(0, grossTotal - discountAmount);

                // Tích lũy điểm mới (10.000đ = 1 điểm)
                int pointsEarned = 0;
                if (customer != null)
                {
                    pointsEarned = (int)(order.TotalAmount / AmountPerPoint);
                    customer.RewardPoints  += pointsEarned;
                    customer.MembershipRank = CustomersController.RankByPoints(customer.RewardPoints);
                }

                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    success        = true,
                    message        = "Thanh toán hóa đơn POS thành công!",
                    orderId        = order.OrderId,
                    grossTotal,
                    discountAmount,
                    pointsRedeemed,
                    totalAmount    = order.TotalAmount,
                    pointsEarned,
                    customerName   = customer?.CustomerName,
                    rewardPoints   = customer?.RewardPoints,
                    membershipRank = customer?.MembershipRank
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var inner = ex.InnerException != null ? $" ({ex.InnerException.Message})" : string.Empty;
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi trong quá trình thanh toán, giao dịch đã được hoàn tác an toàn: " + ex.Message + inner
                });
            }
        }

        // =====================================================================
        // GET api/orders  - Lấy danh sách tất cả đơn hàng kèm chi tiết
        // =====================================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _db.Orders
                .Include(o => o.Details)
                .Select(o => new OrderDto
                {
                    OrderId         = o.OrderId,
                    OrderDate       = o.OrderDate,
                    CashierUsername = o.CashierUsername,
                    CustomerId      = o.CustomerId,
                    TotalAmount     = o.TotalAmount,
                    Status          = (int)o.Status,
                    Details         = o.Details.Select(d => new OrderDetailDto
                    {
                        OrderDetailId = d.OrderDetailId,
                        ProductId     = d.ProductId,
                        Quantity      = d.Quantity,
                        UnitPrice     = d.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }

        // =====================================================================
        // GET api/orders/{id}  - Lấy chi tiết một đơn hàng
        // =====================================================================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound(new { message = "Không tìm thấy đơn hàng." });

            var dto = new OrderDto
            {
                OrderId         = order.OrderId,
                OrderDate       = order.OrderDate,
                CashierUsername = order.CashierUsername,
                CustomerId      = order.CustomerId,
                TotalAmount     = order.TotalAmount,
                Status          = (int)order.Status,
                Details         = order.Details.Select(d => new OrderDetailDto
                {
                    OrderDetailId = d.OrderDetailId,
                    ProductId     = d.ProductId,
                    Quantity      = d.Quantity,
                    UnitPrice     = d.UnitPrice
                }).ToList()
            };

            return Ok(dto);
        }

        // =====================================================================
        // PUT api/orders/{id}  - Cập nhật trạng thái đơn hàng
        // =====================================================================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderRequest req)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
                return NotFound(new { message = "Không tìm thấy đơn hàng." });

            order.Status = (OrderStatus)req.Status;
            await _db.SaveChangesAsync();

            return Ok(new { success = true, message = "Cập nhật trạng thái thành công.", orderId = id, newStatus = order.Status });
        }

        // =====================================================================
        // DELETE api/orders/{id}  - Hủy đơn, khôi phục kho và điểm thưởng
        // =====================================================================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound(new { message = "Không tìm thấy đơn hàng." });

            if (order.Status == OrderStatus.Cancelled)
                return BadRequest(new { message = "Đơn hàng này đã được hủy trước đó." });

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // Khôi phục tồn kho
                foreach (var detail in order.Details)
                {
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == detail.ProductId);
                    if (product != null)
                        product.StockQuantity += detail.Quantity;
                }

                // Khôi phục điểm thưởng (trừ đi điểm đã tích từ đơn hàng này)
                if (order.CustomerId.HasValue)
                {
                    var customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == order.CustomerId);
                    if (customer != null)
                    {
                        int pointsEarned = (int)(order.TotalAmount / AmountPerPoint);
                        customer.RewardPoints   = Math.Max(0, customer.RewardPoints - pointsEarned);
                        customer.MembershipRank = CustomersController.RankByPoints(customer.RewardPoints);
                    }
                }

                order.Status = OrderStatus.Cancelled;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { success = true, message = $"Đã hủy đơn hàng #{id} và khôi phục tồn kho.", orderId = id });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var inner = ex.InnerException != null ? $" ({ex.InnerException.Message})" : string.Empty;
                return StatusCode(500, new { success = false, message = "Hủy đơn hàng thất bại: " + ex.Message + inner });
            }
        }
    }
}
