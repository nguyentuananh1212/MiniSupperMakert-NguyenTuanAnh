using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   // => api/products (đổi cho khớp CategoriesController nếu cần)
    [Authorize]                   // mọi người dùng đã đăng nhập đều được XEM
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _db;

        public ProductsController(SupermarketDbContext db)
        {
            _db = db;
        }

        // =====================================================
        // DTO (không trả thẳng entity để tránh vòng lặp JSON
        // và để kèm CategoryName cho lưới sản phẩm)
        // =====================================================

        public class ProductResponse
        {
            public int ProductId { get; set; }
            public string Barcode { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; } = string.Empty;
        }

        public class ProductRequest
        {
            [Required, StringLength(50)]
            public string Barcode { get; set; } = string.Empty;

            [Required, StringLength(200)]
            public string ProductName { get; set; } = string.Empty;

            [Range(0.01, 1_000_000_000)]
            public decimal Price { get; set; }

            [Range(0, 1_000_000)]
            public int StockQuantity { get; set; }

            [Range(1, int.MaxValue)]
            public int CategoryId { get; set; }
        }

        // Join thủ công để không phụ thuộc vào navigation property
        private IQueryable<ProductResponse> BuildQuery()
        {
            return from p in _db.Products.AsNoTracking()
                   join c in _db.Categories.AsNoTracking()
                       on p.CategoryId equals c.CategoryId into cg
                   from c in cg.DefaultIfEmpty()
                   select new ProductResponse
                   {
                       ProductId = p.ProductId,
                       Barcode = p.Barcode,
                       ProductName = p.ProductName,
                       Price = p.Price,
                       StockQuantity = p.StockQuantity,
                       CategoryId = p.CategoryId,
                       CategoryName = c != null ? c.CategoryName : string.Empty
                   };
        }

        // =====================================================
        // =====================================================
        // GET api/products/paged?keyword=sua&categoryId=1&pageNumber=1&pageSize=5
        // (Buổi 6: Tiết 1 & 2 + Bài tập 6.2)
        // =====================================================
        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            // 1. Kiểm tra tham số trang đầu vào hợp lệ
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5;

            // 2. Khởi tạo truy vấn LINQ IQueryable kết hợp Eager Loading (.Include)
            var query = _db.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 3. Lọc theo nhóm hàng (Bài tập 6.2)
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // 4. Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(search)
                                       || p.Barcode.Contains(search));
            }

            // 5. Đếm tổng số lượng bản ghi thỏa mãn điều kiện
            int totalItems = await query.CountAsync();

            // 6. Tính toán tổng số trang
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            if (totalPages == 0) totalPages = 1;

            // 7. Áp dụng toán tử Skip và Take để phân trang trực tiếp dưới SQL Server
            var items = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            // 8. Đóng gói kết quả trả về cho Client
            var response = new PagedResult<ProductItemDto>
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
        // GET api/products
        // =====================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
        {
            var list = await BuildQuery().OrderBy(p => p.ProductId).ToListAsync();
            return Ok(list);
        }

        // =====================================================
        // GET api/products/5
        // =====================================================
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var product = await BuildQuery().FirstOrDefaultAsync(p => p.ProductId == id);
            return product == null ? NotFound(new { message = "Không tìm thấy sản phẩm." }) : Ok(product);
        }

        // =====================================================
        // GET api/products/barcode/8934567890123   (dùng cho màn hình POS)
        // =====================================================
        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<ProductResponse>> GetByBarcode(string barcode)
        {
            var product = await BuildQuery().FirstOrDefaultAsync(p => p.Barcode == barcode);
            return product == null ? NotFound(new { message = "Không tìm thấy sản phẩm có mã vạch này." }) : Ok(product);
        }

        // =====================================================
        // POST api/products   (Admin, Warehouse)
        // =====================================================
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] ProductRequest req)
        {
            string barcode = req.Barcode.Trim();

            if (!await _db.Categories.AnyAsync(c => c.CategoryId == req.CategoryId))
                return BadRequest(new { message = "Nhóm hàng không tồn tại." });

            if (await _db.Products.AnyAsync(p => p.Barcode == barcode))
                return Conflict(new { message = "Mã vạch đã tồn tại." });

            var entity = new Product
            {
                Barcode = barcode,
                ProductName = req.ProductName.Trim(),
                Price = req.Price,
                StockQuantity = req.StockQuantity,
                CategoryId = req.CategoryId
            };

            _db.Products.Add(entity);
            await _db.SaveChangesAsync();

            var created = await BuildQuery().FirstAsync(p => p.ProductId == entity.ProductId);
            return CreatedAtAction(nameof(GetById), new { id = entity.ProductId }, created);
        }

        // =====================================================
        // PUT api/products/5   (Admin, Warehouse)
        // =====================================================
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductRequest req)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (entity == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm." });

            string barcode = req.Barcode.Trim();

            if (!await _db.Categories.AnyAsync(c => c.CategoryId == req.CategoryId))
                return BadRequest(new { message = "Nhóm hàng không tồn tại." });

            if (await _db.Products.AnyAsync(p => p.Barcode == barcode && p.ProductId != id))
                return Conflict(new { message = "Mã vạch đã được dùng cho sản phẩm khác." });

            entity.Barcode = barcode;
            entity.ProductName = req.ProductName.Trim();
            entity.Price = req.Price;
            entity.StockQuantity = req.StockQuantity;
            entity.CategoryId = req.CategoryId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // =====================================================
        // DELETE api/products/5   (Admin, Warehouse)
        // =====================================================
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (entity == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm." });

            try
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                // Thường do sản phẩm đã nằm trong hóa đơn (ràng buộc khóa ngoại)
                return Conflict(new { message = "Không thể xóa: sản phẩm đã phát sinh giao dịch." });
            }
        }
    }
}