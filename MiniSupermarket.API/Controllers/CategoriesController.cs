using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // Yêu cầu chung: Phải đăng nhập mới được gọi các API trong Controller này
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Dữ liệu nhóm hàng nay lấy từ SQL Server (cùng nguồn với bảng Products)
        // để mã nhóm hàng khi thêm/sửa sản phẩm luôn hợp lệ.
        private readonly SupermarketDbContext _context;

        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ: Lấy toàn bộ danh sách nhóm hàng (GET /api/categories) - Mọi user đã đăng nhập đều xem được
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Categories.AsNoTracking().OrderBy(c => c.CategoryId).ToListAsync();
            return Ok(list);
        }

        // 2. READ: Lấy chi tiết một nhóm hàng theo ID (GET /api/categories/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng!" });
            }
            return Ok(cat);
        }

        // 3. SEARCH: Tìm kiếm nhóm hàng theo từ khóa (GET /api/categories/search?keyword=...)
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });
            }
            var result = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryName.Contains(keyword))
                .ToListAsync();
            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng (POST /api/categories) - Admin, Warehouse
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] Category newCat)
        {
            if (string.IsNullOrWhiteSpace(newCat.CategoryName))
            {
                return BadRequest(new { message = "Tên không được trống!" });
            }
            newCat.CategoryId = 0; // để SQL Server tự tăng
            _context.Categories.Add(newCat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newCat.CategoryId }, newCat);
        }

        // 5. UPDATE: Cập nhật thông tin nhóm hàng (PUT /api/categories/{id}) - Admin, Warehouse
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updateCat)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần sửa!" });
            }
            if (string.IsNullOrWhiteSpace(updateCat.CategoryName))
            {
                return BadRequest(new { message = "Tên không được trống!" });
            }
            cat.CategoryName = updateCat.CategoryName;
            cat.Description = updateCat.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6. DELETE: Xóa nhóm hàng theo ID (DELETE /api/categories/{id}) - Admin, Warehouse
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound(new { message = "Không tìm thấy nhóm hàng cần xóa!" });
            }

            try
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { message = "Không thể xóa: nhóm hàng đang có sản phẩm." });
            }
        }
    }
}
