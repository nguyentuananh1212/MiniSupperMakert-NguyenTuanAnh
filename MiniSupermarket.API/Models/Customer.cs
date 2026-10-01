using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Customers")]
    public class Customer
    {
        // Khóa chính - tự tăng IDENTITY(1,1)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        // Tên khách hàng - bắt buộc, NVARCHAR(100)
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        // Số điện thoại - bắt buộc, VARCHAR(15)
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Địa chỉ - không bắt buộc, NVARCHAR(200)
        [StringLength(200)]
        public string? Address { get; set; }

        // Điểm tích lũy - mặc định 0
        public int RewardPoints { get; set; } = 0;

        // Hạng thành viên - mặc định "Chuẩn"
        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}
