using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Cashier")]   // Chỉ Thu ngân và Admin được bán hàng
    public class OrdersController : ControllerBase
    {
        private const decimal AmountPerPoint = 10_000m;   // 10.000đ = 1 điểm

        private readonly SupermarketDbContext _db;

        public OrdersController(SupermarketDbContext db)
        {
            _db = db;
        }

        public class CheckoutItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }   // chỉ để tham khảo, server dùng giá trong DB
        }

        public class CheckoutRequest
        {
            public string? CashierUsername { get; set; }
            public string? CustomerPhone { get; set; }

            [Required, MinLength(1)]
            public List<CheckoutItem> Items { get; set; } = new();
        }

        // POST api/orders/checkout
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            if (req.Items == null || req.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng đang trống." });

            if (req.Items.Any(i => i.Quantity <= 0))
                return BadRequest(new { message = "Số lượng sản phẩm phải lớn hơn 0." });

            // Gộp các dòng trùng sản phẩm
            var lines = req.Items
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToList();

            var ids = lines.Select(l => l.ProductId).ToList();
            var products = await _db.Products.Where(p => ids.Contains(p.ProductId)).ToListAsync();

            if (products.Count != ids.Count)
                return BadRequest(new { message = "Có sản phẩm trong giỏ không còn tồn tại." });

            // Kiểm tra tồn kho
            foreach (var l in lines)
            {
                var p = products.First(x => x.ProductId == l.ProductId);
                if (p.StockQuantity < l.Quantity)
                    return BadRequest(new
                    {
                        message = $"Sản phẩm \"{p.ProductName}\" chỉ còn {p.StockQuantity} trong kho."
                    });
            }

            // Khách hàng thân thiết (không bắt buộc)
            Customer? customer = null;
            string phone = (req.CustomerPhone ?? string.Empty).Trim();
            if (phone.Length > 0)
            {
                customer = await _db.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phone);
                if (customer == null)
                    return BadRequest(new { message = "Không tìm thấy khách hàng với số điện thoại này." });
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                CashierUsername = User.Identity?.Name ?? req.CashierUsername ?? "unknown",
                CustomerId = customer?.CustomerId
            };

            foreach (var l in lines)
            {
                var p = products.First(x => x.ProductId == l.ProductId);
                p.StockQuantity -= l.Quantity;   // trừ kho

                order.Details.Add(new OrderDetail
                {
                    ProductId = p.ProductId,
                    Quantity = l.Quantity,
                    UnitPrice = p.Price          // giá lấy từ DB, không tin giá client gửi lên
                });
            }

            order.TotalAmount = order.Details.Sum(d => d.UnitPrice * d.Quantity);

            int pointsEarned = 0;
            if (customer != null)
            {
                pointsEarned = (int)(order.TotalAmount / AmountPerPoint);
                customer.RewardPoints += pointsEarned;
                customer.MembershipRank = CustomersController.RankByPoints(customer.RewardPoints);
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();   // lưu hóa đơn + trừ kho + cộng điểm trong 1 giao dịch

            return Ok(new
            {
                orderId = order.OrderId,
                totalAmount = order.TotalAmount,
                pointsEarned,
                customerName = customer?.CustomerName,
                rewardPoints = customer?.RewardPoints,
                membershipRank = customer?.MembershipRank
            });
        }
    }
}
