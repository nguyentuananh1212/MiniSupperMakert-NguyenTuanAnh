/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: DTOs dùng cho quản lý đơn hàng (GET, PUT)
 */
namespace MiniSupermarket.API.Dtos
{
    public class OrderDto
    {
        public int      OrderId          { get; set; }
        public DateTime OrderDate        { get; set; }
        public string   CashierUsername  { get; set; } = string.Empty;
        public int?     CustomerId       { get; set; }
        public decimal  TotalAmount      { get; set; }
        public int      Status           { get; set; }   // 0=Pending,1=Completed,2=Cancelled
        public List<OrderDetailDto> Details { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int     OrderDetailId { get; set; }
        public int     ProductId     { get; set; }
        public int     Quantity      { get; set; }
        public decimal UnitPrice     { get; set; }
    }

    public class UpdateOrderRequest
    {
        public int Status { get; set; }   // 0=Pending, 1=Completed, 2=Cancelled
    }
}
