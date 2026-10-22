/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: DTO hứng dữ liệu kết quả thống kê từ Stored Procedure doanh thu ca bán lẻ
 */
namespace MiniSupermarket.API.Models
{
    public class CashierRevenueDto
    {
        // Tên đăng nhập của thu ngân chịu trách nhiệm ca bán
        public string CashierUsername { get; set; } = string.Empty;

        // Tổng số lượng đơn hàng thu ngân đã xử lý thành công
        public int TotalOrders { get; set; }

        // Tổng số tiền thu về cho siêu thị (VND)
        public decimal TotalRevenue { get; set; }

        // Tổng điểm thưởng đã tích lũy cho khách hàng
        public int TotalPointsAwarded { get; set; }
    }
}
