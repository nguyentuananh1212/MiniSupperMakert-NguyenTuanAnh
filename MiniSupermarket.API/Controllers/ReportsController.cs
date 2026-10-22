using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]   // Chỉ Admin được xem dữ liệu tài chính
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _db;

        public ReportsController(SupermarketDbContext db)
        {
            _db = db;
        }

        // GET api/reports/daily?date=2026-10-15
        [HttpGet("daily")]
        public async Task<IActionResult> Daily([FromQuery] DateTime? date)
        {
            DateTime from = (date ?? DateTime.Today).Date;
            DateTime to = from.AddDays(1);

            var orders = _db.Orders.AsNoTracking()
                .Where(o => o.OrderDate >= from && o.OrderDate < to);

            int totalOrders = await orders.CountAsync();
            decimal totalRevenue = totalOrders == 0 ? 0 : await orders.SumAsync(o => o.TotalAmount);

            // Tổng số sản phẩm đã bán trong ngày
            int itemsSold = await _db.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.OrderDate >= from && d.Order.OrderDate < to)
                .SumAsync(d => (int?)d.Quantity) ?? 0;

            // Top 5 sản phẩm bán chạy trong ngày
            var topRaw = await _db.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.OrderDate >= from && d.Order.OrderDate < to)
                .GroupBy(d => d.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Quantity * x.UnitPrice)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToListAsync();

            var topIds = topRaw.Select(t => t.ProductId).ToList();
            var names = await _db.Products.AsNoTracking()
                .Where(p => topIds.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId, p => p.ProductName);

            var topProducts = topRaw.Select(t => new
            {
                productName = names.TryGetValue(t.ProductId, out var n) ? n : $"#{t.ProductId}",
                quantity = t.Quantity,
                revenue = t.Revenue
            }).ToList();

            string? bestName = topProducts.FirstOrDefault()?.productName;
            int bestQty = topProducts.FirstOrDefault()?.quantity ?? 0;

            // Doanh thu 7 ngày gần nhất (kết thúc tại ngày được chọn)
            DateTime weekFrom = from.AddDays(-6);
            var perDay = await _db.Orders.AsNoTracking()
                .Where(o => o.OrderDate >= weekFrom && o.OrderDate < to)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new { Day = g.Key, Revenue = g.Sum(x => x.TotalAmount) })
                .ToListAsync();

            var last7Days = Enumerable.Range(0, 7)
                .Select(i => weekFrom.AddDays(i))
                .Select(d => new
                {
                    date = d,
                    revenue = perDay.FirstOrDefault(x => x.Day == d)?.Revenue ?? 0m
                })
                .ToList();

            return Ok(new
            {
                date = from,
                totalOrders,
                totalRevenue,
                averageOrderValue = totalOrders == 0 ? 0m : Math.Round(totalRevenue / totalOrders, 0),
                itemsSold,
                bestSellerName = bestName,
                bestSellerQuantity = bestQty,
                topProducts,
                last7Days
            });
        }
    }
}
