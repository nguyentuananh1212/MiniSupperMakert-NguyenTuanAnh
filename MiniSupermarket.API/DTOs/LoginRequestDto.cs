/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: DTO tiếp nhận dữ liệu đăng nhập từ Client
 */
namespace MiniSupermarket.API.DTOs
{
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
