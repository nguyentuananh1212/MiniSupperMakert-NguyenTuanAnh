/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: Controller Báo cáo - Kết hợp EF Core và ADO.NET Stored Procedure
 * Tiết 3: Đầy đủ sp_GetRevenueByCashier, sp_GetRevenueByDateRange (Bài 3.1), sp_GetTop5SellingProducts (Bài 3.2)
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _db;
        private readonly IConfiguration _configuration;

        public ReportsController(SupermarketDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        // =========================================================================
        // BÁO CÁO EF CORE (CŨ): GET api/reports/daily?date=2026-10-15
        // =========================================================================
        [HttpGet("daily")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Daily([FromQuery] DateTime? date)
        {
            DateTime from = (date ?? DateTime.Today).Date;
            DateTime to = from.AddDays(1);

            var orders = _db.Orders.AsNoTracking()
                .Where(o => o.OrderDate >= from && o.OrderDate < to);

            int totalOrders = await orders.CountAsync();
            decimal totalRevenue = totalOrders == 0 ? 0 : await orders.SumAsync(o => o.TotalAmount);

            int itemsSold = await _db.OrderDetails.AsNoTracking()
                .Where(d => d.Order!.OrderDate >= from && d.Order.OrderDate < to)
                .SumAsync(d => (int?)d.Quantity) ?? 0;

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

        // =========================================================================
        // TIẾT 3: GET: api/reports/revenue-by-cashier
        // Chức năng: Gọi Stored Procedure sp_GetRevenueByCashier qua SqlDataReader
        // =========================================================================
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            // 1. Khởi tạo SqlConnection trong khối using đảm bảo tự động đóng kết nối
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 2. Gán CommandText là CHÍNH XÁC tên Stored Procedure trên SQL Server
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    // 3. ĐIỂM KHOA HỌC QUAN TRỌNG: Thiết lập CommandType là StoredProcedure
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        // 4. Mở kết nối vật lý tới SQL Server
                        connection.Open();

                        // 5. Thực thi bằng ExecuteReader(): Nhận về luồng dữ liệu forward-only
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 6. Vòng lặp đọc từng dòng kết quả của Stored Procedure
                            while (reader.Read())
                            {
                                // Ánh xạ từng cột dữ liệu từ reader vào đối tượng DTO
                                var item = new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"])
                                        : 0
                                };
                                reportList.Add(item);
                            }
                        } // reader tự động Close() tại đây
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                        });
                    }
                }
            } // connection tự động Close() và Dispose() tại đây

            // 7. Trả về mã 200 OK kèm danh sách báo cáo đã tổng hợp
            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.1: GET: api/reports/revenue-by-date-range?fromDate=...&toDate=...
        // Chức năng: Gọi Stored Procedure sp_GetRevenueByDateRange (có 2 tham số)
        // =========================================================================
        [HttpGet("revenue-by-date-range")]
        public IActionResult GetRevenueByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return BadRequest(new { message = "Từ ngày (fromDate) không được lớn hơn Đến ngày (toDate)!" });
            }

            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Gán 2 tham số an toàn qua SqlParameter
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"])
                                        : 0
                                };
                                reportList.Add(item);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                fromDate = fromDate.ToString("yyyy-MM-dd HH:mm:ss"),
                toDate = toDate.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.2: GET: api/reports/top-selling
        // Chức năng: Gọi Stored Procedure sp_GetTop5SellingProducts
        // =========================================================================
        [HttpGet("top-selling")]
        public IActionResult GetTop5SellingProducts()
        {
            var topList = new List<TopSellingProductDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetTop5SellingProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new TopSellingProductDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    TotalSoldQuantity = Convert.ToInt32(reader["TotalSoldQuantity"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                                };
                                topList.Add(item);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                topCount = topList.Count,
                data = topList
            });
        }
    }
}
