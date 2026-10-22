/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: Các lớp DTO phục vụ nghiệp vụ thanh toán giỏ hàng POS
 */
namespace MiniSupermarket.API.Models
{
    // DTO biểu diễn từng dòng sản phẩm trong giỏ hàng
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // DTO nhận yêu cầu thanh toán tổng thể từ máy POS
    public class CheckoutRequestDto
    {
        // Tài khoản thu ngân đang đứng ca trực
        public string CashierUsername { get; set; } = string.Empty;

        // Mã định danh khách hàng (nếu là khách vãng lai, truyền CustomerId = 15 hoặc ID khách lẻ)
        public int CustomerId { get; set; }

        // Danh sách các món hàng cần thanh toán
        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
