using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _db;
        private readonly ILogger<AuthController> _logger;
        private readonly PasswordHasher<User> _hasher = new();

        public AuthController(
            IConfiguration configuration,
            SupermarketDbContext db,
            ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _db = db;
            _logger = logger;
        }

        // Endpoint Đăng nhập: POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            string username = (request.Username ?? string.Empty).Trim();

            // 1. Đăng nhập bằng tài khoản trong database
            try
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

                if (user != null)
                {
                    if (!user.IsActive)
                        return Unauthorized(new { success = false, message = "Tài khoản đã bị khóa!" });

                    bool passwordOk;

                    if (string.IsNullOrEmpty(user.PasswordHash))
                    {
                        // Tài khoản mẫu (seed) chưa có mã băm: so khớp cột Password,
                        // đúng thì băm luôn và lưu lại để lần sau đăng nhập bình thường.
                        passwordOk = !string.IsNullOrEmpty(user.Password) && user.Password == request.Password;
                        if (passwordOk)
                        {
                            user.PasswordHash = _hasher.HashPassword(user, request.Password);
                            await _db.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        passwordOk = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password)
                                     != PasswordVerificationResult.Failed;
                    }

                    if (passwordOk)
                    {
                        var token = GenerateJwtToken(user.Username, user.Role);
                        return Ok(new { success = true, token = token, role = user.Role, fullName = user.FullName });
                    }

                    return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });
                }
            }
            catch (Exception ex)
            {
                // Database chưa sẵn sàng: vẫn cho đăng nhập bằng tài khoản mẫu bên dưới
                _logger.LogWarning(ex, "Không truy vấn được bảng Users, dùng tài khoản mẫu.");
            }

            // 2. Tài khoản mẫu dự phòng (để tạo các tài khoản đầu tiên qua màn hình Quản trị)
            if (username == "admin" && request.Password == "123456")
            {
                var token = GenerateJwtToken("admin", "Admin");
                return Ok(new { success = true, token = token, role = "Admin" });
            }
            else if (username == "cashier" && request.Password == "123456")
            {
                var token = GenerateJwtToken("cashier", "Cashier");
                return Ok(new { success = true, token = token, role = "Cashier" });
            }

            return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });
        }

        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // Lấy khóa bí mật từ appsettings.json
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Thời hạn token là 2 tiếng
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}