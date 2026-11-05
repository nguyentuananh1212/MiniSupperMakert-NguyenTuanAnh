/*
 * Họ tên sinh viên: Nguyen Tuan Anh
 * Mô tả: Lớp tĩnh quản lý HttpClient kết nối Web API, tự động gắn JWT Bearer Token (Buổi 6)
 */
using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms.Services
{
    public static class ApiClientService
    {
        // Khởi tạo một phiên bản HttpClient dùng chung cho toàn ứng dụng
        public static HttpClient Client => MiniSupermarket.WinForms.ApiClientService.Client;

        // Phương thức cập nhật Bearer Token sau khi đăng nhập thành công
        public static void AttachToken(string token)
        {
            MiniSupermarket.WinForms.SessionManager.JwtToken = token;
            MiniSupermarket.WinForms.ApiClientService.Client.DefaultRequestHeaders.Authorization = null; // Xóa token cũ
            if (!string.IsNullOrEmpty(token))
            {
                MiniSupermarket.WinForms.ApiClientService.Client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
