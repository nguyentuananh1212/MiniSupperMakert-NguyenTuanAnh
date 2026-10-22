/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: DTO hứng dữ liệu thống kê Top sản phẩm bán chạy nhất từ Stored Procedure (Bài tập 3.2)
 */
namespace MiniSupermarket.API.Models
{
    public class TopSellingProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int TotalSoldQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
