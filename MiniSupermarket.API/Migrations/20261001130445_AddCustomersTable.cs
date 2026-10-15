using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Snack, bánh quy, kẹo dẻo, chocolate");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát", "Nước ngọt, nước có ga, nước tăng lực" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước suối & Nước đóng chai", "Nước lọc, nước khoáng, nước tinh khiết đóng chai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Đồ uống dinh dưỡng", "Sữa tươi, sữa hộp, sữa chua, thức uống dinh dưỡng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì & Thực phẩm ăn liền", "Mì gói, mì ly, cháo ăn liền, phở ăn liền" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 6, "Thực phẩm đóng hộp", "Cá hộp, thịt hộp, pate, rau củ đóng hộp" },
                    { 7, "Đồ ăn nhanh", "Xúc xích, sandwich, hamburger, cơm hộp" },
                    { 8, "Cà phê & Trà", "Cà phê hòa tan, cà phê lon, trà túi lọc" },
                    { 9, "Gia vị & Thực phẩm khô", "Nước mắm, nước tương, đường, muối, gia vị" },
                    { 10, "Đồ dùng cá nhân", "Bàn chải, kem đánh răng, dầu gội, sữa tắm" },
                    { 11, "Chăm sóc sức khỏe", "Khẩu trang, băng cá nhân, nước rửa tay" },
                    { 12, "Đồ dùng gia đình", "Khăn giấy, túi rác, nước rửa chén" },
                    { 13, "Văn phòng phẩm", "Bút, vở, giấy, dụng cụ học tập" },
                    { 14, "Đông lạnh", "Kem, thực phẩm đông lạnh, xúc xích" },
                    { 15, "Trái cây & Thực phẩm tươi", "Trái cây, rau củ và thực phẩm tươi" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, "Quận 1, TP. Hồ Chí Minh", "Nguyễn Văn An", "Bạc", "0901234567", 120 },
                    { 2, "Quận 3, TP. Hồ Chí Minh", "Trần Thị Bình", "Chuẩn", "0912345678", 50 },
                    { 3, "Quận 7, TP. Hồ Chí Minh", "Lê Minh Cường", "Vàng", "0923456789", 350 },
                    { 4, "Thủ Đức, TP. Hồ Chí Minh", "Phạm Thị Dung", "Chuẩn", "0934567890", 20 },
                    { 5, "Quận Bình Thạnh, TP. Hồ Chí Minh", "Hoàng Minh Đức", "Kim cương", "0945678901", 800 },
                    { 6, "Quận Tân Bình, TP. Hồ Chí Minh", "Võ Thị Hạnh", "Bạc", "0956789012", 180 },
                    { 7, "Quận Gò Vấp, TP. Hồ Chí Minh", "Đặng Quốc Huy", "Vàng", "0967890123", 420 },
                    { 8, "Quận 10, TP. Hồ Chí Minh", "Bùi Ngọc Lan", "Chuẩn", "0978901234", 75 },
                    { 9, "Quận 5, TP. Hồ Chí Minh", "Phan Thanh Long", "Bạc", "0989012345", 260 },
                    { 10, "Quận 11, TP. Hồ Chí Minh", "Ngô Thị Mai", "Vàng", "0990123456", 620 },
                    { 11, "Quận Phú Nhuận, TP. Hồ Chí Minh", "Đỗ Văn Nam", "Chuẩn", "0881234567", 15 },
                    { 12, "Quận 6, TP. Hồ Chí Minh", "Vũ Thị Oanh", "Bạc", "0872345678", 290 },
                    { 13, "Quận Bình Tân, TP. Hồ Chí Minh", "Đinh Minh Phúc", "Kim cương", "0863456789", 950 },
                    { 14, "Quận Tân Phú, TP. Hồ Chí Minh", "Mai Thị Quỳnh", "Bạc", "0854567890", 110 },
                    { 15, "Quận 12, TP. Hồ Chí Minh", "Trương Hoàng Sơn", "Chuẩn", "0845678901", 45 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893850001001", 1, 12000m, "Bánh Oreo Chocolate", 50 },
                    { 2, "893850001002", 1, 15000m, "Snack khoai tây Lay's", 40 },
                    { 3, "893850001003", 1, 25000m, "Kẹo dẻo Haribo", 30 },
                    { 4, "893850002001", 2, 10000m, "Coca Cola 330ml", 100 },
                    { 5, "893850002002", 2, 10000m, "Pepsi 330ml", 100 },
                    { 6, "893850002003", 2, 10000m, "Sting Dâu 330ml", 80 },
                    { 7, "893850003001", 3, 6000m, "Aquafina 500ml", 120 },
                    { 8, "893850003002", 3, 6000m, "Lavie 500ml", 100 },
                    { 9, "893850003003", 3, 6000m, "Dasani 500ml", 90 },
                    { 10, "893850004001", 4, 35000m, "Vinamilk Sữa tươi", 50 },
                    { 11, "893850004002", 4, 36000m, "TH True Milk", 45 },
                    { 12, "893850004003", 4, 25000m, "Yakult", 40 },
                    { 13, "893850005001", 5, 5000m, "Mì Hảo Hảo Tôm Chua Cay", 200 },
                    { 14, "893850005002", 5, 12000m, "Mì Omachi", 100 },
                    { 15, "893850005003", 5, 10000m, "Cháo ăn liền Vifon", 60 },
                    { 16, "893850006001", 6, 28000m, "Cá hộp 3 Cô Gái", 40 },
                    { 17, "893850006002", 6, 22000m, "Pate gan hộp", 35 },
                    { 18, "893850006003", 6, 18000m, "Đậu Hà Lan đóng hộp", 30 },
                    { 19, "893850007001", 7, 10000m, "Xúc xích CP", 70 },
                    { 20, "893850007002", 7, 25000m, "Sandwich thịt nguội", 25 },
                    { 21, "893850007003", 7, 30000m, "Hamburger bò", 20 },
                    { 22, "893850008001", 8, 30000m, "Cà phê hòa tan G7", 50 },
                    { 23, "893850008002", 8, 12000m, "Cà phê lon Birdy", 60 },
                    { 24, "893850008003", 8, 10000m, "Trà xanh C2", 70 },
                    { 25, "893850009001", 9, 35000m, "Nước mắm Nam Ngư", 40 },
                    { 26, "893850009002", 9, 18000m, "Nước tương Chinsu", 45 },
                    { 27, "893850009003", 9, 22000m, "Đường tinh luyện", 50 },
                    { 28, "893850010001", 10, 30000m, "Kem đánh răng P/S", 40 },
                    { 29, "893850010002", 10, 45000m, "Dầu gội Sunsilk", 35 },
                    { 30, "893850010003", 10, 50000m, "Sữa tắm Lifebuoy", 30 },
                    { 31, "893850011001", 11, 25000m, "Khẩu trang y tế", 100 },
                    { 32, "893850011002", 11, 15000m, "Băng cá nhân", 50 },
                    { 33, "893850011003", 11, 45000m, "Nước rửa tay Lifebuoy", 35 },
                    { 34, "893850012001", 12, 25000m, "Khăn giấy Pulppy", 60 },
                    { 35, "893850012002", 12, 35000m, "Nước rửa chén Sunlight", 40 },
                    { 36, "893850012003", 12, 20000m, "Túi rác tự hủy", 45 },
                    { 37, "893850013001", 13, 5000m, "Bút bi Thiên Long", 100 },
                    { 38, "893850013002", 13, 25000m, "Vở Campus", 50 },
                    { 39, "893850013003", 13, 5000m, "Bút chì 2B", 70 },
                    { 40, "893850014001", 14, 12000m, "Kem Merino", 50 },
                    { 41, "893850014002", 14, 55000m, "Xúc xích đông lạnh", 30 },
                    { 42, "893850014003", 14, 45000m, "Há cảo đông lạnh", 25 },
                    { 43, "893850015001", 15, 65000m, "Táo Fuji", 25 },
                    { 44, "893850015002", 15, 30000m, "Chuối tiêu", 30 },
                    { 45, "893850015003", 15, 40000m, "Cam sành", 30 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Snack, bánh quy, kẹo dẻo");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
