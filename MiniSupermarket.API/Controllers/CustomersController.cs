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
    [Authorize(Roles = "Admin,Cashier")]   // Thủ kho (Warehouse) không được truy cập khách hàng
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _db;

        public CustomersController(SupermarketDbContext db)
        {
            _db = db;
        }

        public class CustomerRequest
        {
            [Required, StringLength(100)]
            public string CustomerName { get; set; } = string.Empty;

            [Required, StringLength(15)]
            public string PhoneNumber { get; set; } = string.Empty;

            [StringLength(200)]
            public string? Address { get; set; }
        }

        // Hạng thẻ tính theo điểm tích lũy (khớp với dữ liệu mẫu)
        public static string RankByPoints(int points)
        {
            if (points >= 800) return "Kim cương";
            if (points >= 300) return "Vàng";
            if (points >= 100) return "Bạc";
            return "Chuẩn";
        }

        // =====================================================
        // GET api/customers/paged?keyword=...&pageNumber=1&pageSize=5
        // (Buổi 6: Bài tập 6.1 - Phân trang khách hàng sắp xếp RewardPoints giảm dần)
        // =====================================================
        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedCustomers(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5;

            var query = _db.Customers
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(search)
                                      || c.PhoneNumber.Contains(search));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            if (totalPages == 0) totalPages = 1;

            var items = await query
                .OrderByDescending(c => c.RewardPoints)
                .ThenBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PagedResult<Customer>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // =====================================================
        // GET api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Customers.AsNoTracking().OrderBy(c => c.CustomerId).ToListAsync();
            return Ok(list);
        }

        // GET api/customers/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);
            return c == null ? NotFound(new { message = "Không tìm thấy khách hàng." }) : Ok(c);
        }

        // GET api/customers/phone/0901234567   (dùng cho màn hình POS)
        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            var c = await _db.Customers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.PhoneNumber == phone.Trim());
            return c == null ? NotFound(new { message = "Không tìm thấy khách hàng với số điện thoại này." }) : Ok(c);
        }

        // GET api/customers/search?keyword=...
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });

            keyword = keyword.Trim();
            var list = await _db.Customers.AsNoTracking()
                .Where(c => c.CustomerName.Contains(keyword) || c.PhoneNumber.Contains(keyword))
                .OrderBy(c => c.CustomerId)
                .ToListAsync();
            return Ok(list);
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequest req)
        {
            string phone = req.PhoneNumber.Trim();

            if (await _db.Customers.AnyAsync(c => c.PhoneNumber == phone))
                return Conflict(new { message = "Số điện thoại đã được đăng ký." });

            var entity = new Customer
            {
                CustomerName = req.CustomerName.Trim(),
                PhoneNumber = phone,
                Address = string.IsNullOrWhiteSpace(req.Address) ? null : req.Address.Trim(),
                RewardPoints = 0,
                MembershipRank = RankByPoints(0)
            };

            _db.Customers.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = entity.CustomerId }, entity);
        }

        // PUT api/customers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerRequest req)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (entity == null)
                return NotFound(new { message = "Không tìm thấy khách hàng." });

            string phone = req.PhoneNumber.Trim();
            if (await _db.Customers.AnyAsync(c => c.PhoneNumber == phone && c.CustomerId != id))
                return Conflict(new { message = "Số điện thoại đã được dùng cho khách hàng khác." });

            entity.CustomerName = req.CustomerName.Trim();
            entity.PhoneNumber = phone;
            entity.Address = string.IsNullOrWhiteSpace(req.Address) ? null : req.Address.Trim();

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/customers/5   (chỉ Admin)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (entity == null)
                return NotFound(new { message = "Không tìm thấy khách hàng." });

            try
            {
                _db.Customers.Remove(entity);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { message = "Không thể xóa: khách hàng đã có hóa đơn." });
            }
        }
        public class RedeemPointsRequest
        {
            [Range(1, 1000000, ErrorMessage = "Số điểm đổi phải lớn hơn 0.")]
            public int Points { get; set; }

            public string? Note { get; set; } = "Đổi điểm thưởng trừ tiền / quà tặng";
        }

        // POST api/customers/5/redeem-points
        // Tiêu chí 47: Đổi điểm thưởng tích lũy (1 điểm = 1.000đ)
        [HttpPost("{id:int}/redeem-points")]
        public async Task<IActionResult> RedeemPoints(int id, [FromBody] RedeemPointsRequest req)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (entity == null)
                return NotFound(new { message = "Không tìm thấy khách hàng." });

            if (req.Points <= 0)
                return BadRequest(new { message = "Số điểm đổi phải lớn hơn 0." });

            if (entity.RewardPoints < req.Points)
            {
                return BadRequest(new
                {
                    message = $"Khách hàng chỉ có {entity.RewardPoints} điểm, không đủ để đổi {req.Points} điểm."
                });
            }

            entity.RewardPoints -= req.Points;
            entity.MembershipRank = RankByPoints(entity.RewardPoints);
            await _db.SaveChangesAsync();

            decimal discountValue = req.Points * 1000m; // 1 điểm = 1.000 VNĐ
            return Ok(new
            {
                success = true,
                message = $"Đổi thành công {req.Points} điểm thưởng (tương đương {discountValue:N0} đ)!",
                customerId = entity.CustomerId,
                customerName = entity.CustomerName,
                phoneNumber = entity.PhoneNumber,
                pointsRedeemed = req.Points,
                discountValue = discountValue,
                remainingPoints = entity.RewardPoints,
                membershipRank = entity.MembershipRank,
                note = req.Note
            });
        }
    }
}