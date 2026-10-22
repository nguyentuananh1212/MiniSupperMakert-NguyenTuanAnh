namespace MiniSupermarket.API.Models
{
    public class User
    {
        public bool IsActive { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}