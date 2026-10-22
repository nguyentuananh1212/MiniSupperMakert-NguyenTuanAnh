using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFamilyFreshCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Bánh quy, snack, kẹo, chocolate và các món ăn vặt");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Description",
                value: "Nước ngọt, nước có ga, nước tăng lực và đồ uống đóng lon");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước uống đóng chai", "Nước suối, nước khoáng và nước tinh khiết đóng chai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "Description",
                value: "Sữa tươi, sữa hộp, sữa chua và đồ uống dinh dưỡng");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "Description",
                value: "Mì gói, mì ly, cháo, phở và thực phẩm ăn liền");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "Description",
                value: "Cá hộp, thịt hộp, pate và thực phẩm đóng hộp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "Description",
                value: "Sandwich, hamburger, xúc xích và các món ăn tiện lợi");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "Description",
                value: "Cà phê hòa tan, cà phê lon, trà đóng chai và trà túi lọc");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                column: "Description",
                value: "Nước mắm, nước tương, đường, dầu ăn và gia vị");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Chăm sóc cá nhân", "Kem đánh răng, bàn chải, dầu gội, sữa tắm và đồ vệ sinh cá nhân" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                column: "Description",
                value: "Khẩu trang, băng cá nhân, nước rửa tay và sản phẩm chăm sóc sức khỏe");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                column: "Description",
                value: "Khăn giấy, túi rác, nước rửa chén và các sản phẩm gia dụng");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                column: "Description",
                value: "Bút, vở, giấy và dụng cụ học tập, văn phòng");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Thực phẩm đông lạnh", "Kem, xúc xích, há cảo và các sản phẩm đông lạnh" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Rau củ & Trái cây tươi", "Rau củ, trái cây và thực phẩm tươi tại Family Fresh" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 18000m, "Bánh quy Cosy" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 12000m, "Snack khoai tây Oishi", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15000m, "Kẹo dẻo Alpenliebe", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ProductName",
                value: "Coca-Cola lon 330ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ProductName",
                value: "Pepsi lon 330ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ProductName",
                value: "Sting dâu 330ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "ProductName",
                value: "Nước suối Aquafina 500ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "ProductName",
                value: "Nước suối Lavie 500ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ProductName",
                value: "Nước suối Dasani 500ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 8000m, "Sữa tươi Vinamilk 180ml", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 9000m, "Sữa tươi TH True Milk 180ml", 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 7000m, "Sữa chua Vinamilk", 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "ProductName",
                value: "Mì Omachi Sườn Hầm Ngũ Quả");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                column: "ProductName",
                value: "Hamburger bò Family Fresh");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 45000m, "Dầu ăn Neptune 1L", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                column: "ProductName",
                value: "Khẩu trang y tế 4 lớp");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                column: "ProductName",
                value: "Băng cá nhân cá nhân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39,
                column: "ProductName",
                value: "Bút chì 2B Thiên Long");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                column: "Description",
                value: "Nước ngọt, nước có ga, nước tăng lực");

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
                column: "Description",
                value: "Sữa tươi, sữa hộp, sữa chua, thức uống dinh dưỡng");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "Description",
                value: "Mì gói, mì ly, cháo ăn liền, phở ăn liền");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "Description",
                value: "Cá hộp, thịt hộp, pate, rau củ đóng hộp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "Description",
                value: "Xúc xích, sandwich, hamburger, cơm hộp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "Description",
                value: "Cà phê hòa tan, cà phê lon, trà túi lọc");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9,
                column: "Description",
                value: "Nước mắm, nước tương, đường, muối, gia vị");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ dùng cá nhân", "Bàn chải, kem đánh răng, dầu gội, sữa tắm" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11,
                column: "Description",
                value: "Khẩu trang, băng cá nhân, nước rửa tay");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12,
                column: "Description",
                value: "Khăn giấy, túi rác, nước rửa chén");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13,
                column: "Description",
                value: "Bút, vở, giấy, dụng cụ học tập");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đông lạnh", "Kem, thực phẩm đông lạnh, xúc xích" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Trái cây & Thực phẩm tươi", "Trái cây, rau củ và thực phẩm tươi" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 12000m, "Bánh Oreo Chocolate" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 15000m, "Snack khoai tây Lay's", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Kẹo dẻo Haribo", 30 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ProductName",
                value: "Coca Cola 330ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ProductName",
                value: "Pepsi 330ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ProductName",
                value: "Sting Dâu 330ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "ProductName",
                value: "Aquafina 500ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "ProductName",
                value: "Lavie 500ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ProductName",
                value: "Dasani 500ml");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 35000m, "Vinamilk Sữa tươi", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 36000m, "TH True Milk", 45 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 25000m, "Yakult", 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "ProductName",
                value: "Mì Omachi");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                column: "ProductName",
                value: "Hamburger bò");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                columns: new[] { "Price", "ProductName", "StockQuantity" },
                values: new object[] { 22000m, "Đường tinh luyện", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                column: "ProductName",
                value: "Khẩu trang y tế");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                column: "ProductName",
                value: "Băng cá nhân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39,
                column: "ProductName",
                value: "Bút chì 2B");
        }
    }
}
