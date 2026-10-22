using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "IsActive", "Password", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Nguyễn Quản Trị", false, "123456", "", "Admin", "admin01" },
                    { 2, "Trần Giám Đốc", false, "123456", "", "Admin", "admin02" },
                    { 3, "Lê Thu Ngân", false, "123456", "", "Cashier", "cashier01" },
                    { 4, "Phạm Bán Hàng", false, "123456", "", "Cashier", "cashier02" },
                    { 5, "Hoàng Thu Ngân", false, "123456", "", "Cashier", "cashier03" },
                    { 6, "Vũ Thị Quầy", false, "123456", "", "Cashier", "cashier04" },
                    { 7, "Đỗ Bán Lẻ", false, "123456", "", "Cashier", "cashier05" },
                    { 8, "Ngô Quản Kho", false, "123456", "", "Warehouse", "ware01" },
                    { 9, "Bùi Kiểm Kê", false, "123456", "", "Warehouse", "ware02" },
                    { 10, "Dương Thủ Kho", false, "123456", "", "Warehouse", "ware03" },
                    { 11, "Lý Nhập Hàng", false, "123456", "", "Warehouse", "ware04" },
                    { 12, "Đặng Hỗ Trợ", false, "123456", "", "Admin", "admin_backup" },
                    { 13, "Hồ Ca Chiều", false, "123456", "", "Cashier", "cashier06" },
                    { 14, "Trương Vận Chuyển", false, "123456", "", "Warehouse", "ware05" },
                    { 15, "Mai Giám Sát", false, "123456", "", "Admin", "supervisor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
