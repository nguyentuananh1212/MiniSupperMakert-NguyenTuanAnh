using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    // Hóa đơn bán hàng tại quầy POS
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        // Thời điểm lập hóa đơn
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Thu ngân lập hóa đơn
        [Required]
        [StringLength(50)]
        public string CashierUsername { get; set; } = string.Empty;

        // Khách hàng thân thiết (null = khách lẻ)
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public virtual Customer? Customer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    }
}
