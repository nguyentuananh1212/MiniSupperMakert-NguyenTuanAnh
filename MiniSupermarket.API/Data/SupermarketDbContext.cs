using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================================================
            // QUAN HỆ CATEGORY - PRODUCT
            // 1 Category có nhiều Product
            // =====================================================

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // SEED CATEGORY
            // =====================================================

            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Bánh kẹo & Đồ ăn vặt",
                    Description = "Snack, bánh quy, kẹo dẻo, chocolate"
                },

                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Nước giải khát",
                    Description = "Nước ngọt, nước có ga, nước tăng lực"
                },

                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Nước suối & Nước đóng chai",
                    Description = "Nước lọc, nước khoáng, nước tinh khiết đóng chai"
                },

                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Sữa & Đồ uống dinh dưỡng",
                    Description = "Sữa tươi, sữa hộp, sữa chua, thức uống dinh dưỡng"
                },

                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Mì & Thực phẩm ăn liền",
                    Description = "Mì gói, mì ly, cháo ăn liền, phở ăn liền"
                },

                new Category
                {
                    CategoryId = 6,
                    CategoryName = "Thực phẩm đóng hộp",
                    Description = "Cá hộp, thịt hộp, pate, rau củ đóng hộp"
                },

                new Category
                {
                    CategoryId = 7,
                    CategoryName = "Đồ ăn nhanh",
                    Description = "Xúc xích, sandwich, hamburger, cơm hộp"
                },

                new Category
                {
                    CategoryId = 8,
                    CategoryName = "Cà phê & Trà",
                    Description = "Cà phê hòa tan, cà phê lon, trà túi lọc"
                },

                new Category
                {
                    CategoryId = 9,
                    CategoryName = "Gia vị & Thực phẩm khô",
                    Description = "Nước mắm, nước tương, đường, muối, gia vị"
                },

                new Category
                {
                    CategoryId = 10,
                    CategoryName = "Đồ dùng cá nhân",
                    Description = "Bàn chải, kem đánh răng, dầu gội, sữa tắm"
                },

                new Category
                {
                    CategoryId = 11,
                    CategoryName = "Chăm sóc sức khỏe",
                    Description = "Khẩu trang, băng cá nhân, nước rửa tay"
                },

                new Category
                {
                    CategoryId = 12,
                    CategoryName = "Đồ dùng gia đình",
                    Description = "Khăn giấy, túi rác, nước rửa chén"
                },

                new Category
                {
                    CategoryId = 13,
                    CategoryName = "Văn phòng phẩm",
                    Description = "Bút, vở, giấy, dụng cụ học tập"
                },

                new Category
                {
                    CategoryId = 14,
                    CategoryName = "Đông lạnh",
                    Description = "Kem, thực phẩm đông lạnh, xúc xích"
                },

                new Category
                {
                    CategoryId = 15,
                    CategoryName = "Trái cây & Thực phẩm tươi",
                    Description = "Trái cây, rau củ và thực phẩm tươi"
                }
            );


            // =====================================================
            // SEED PRODUCT
            // =====================================================

            modelBuilder.Entity<Product>().HasData(

                // Category 1
                new Product
                {
                    ProductId = 1,
                    Barcode = "893850001001",
                    ProductName = "Bánh Oreo Chocolate",
                    Price = 12000,
                    StockQuantity = 50,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 2,
                    Barcode = "893850001002",
                    ProductName = "Snack khoai tây Lay's",
                    Price = 15000,
                    StockQuantity = 40,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 3,
                    Barcode = "893850001003",
                    ProductName = "Kẹo dẻo Haribo",
                    Price = 25000,
                    StockQuantity = 30,
                    CategoryId = 1
                },


                // Category 2
                new Product
                {
                    ProductId = 4,
                    Barcode = "893850002001",
                    ProductName = "Coca Cola 330ml",
                    Price = 10000,
                    StockQuantity = 100,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 5,
                    Barcode = "893850002002",
                    ProductName = "Pepsi 330ml",
                    Price = 10000,
                    StockQuantity = 100,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 6,
                    Barcode = "893850002003",
                    ProductName = "Sting Dâu 330ml",
                    Price = 10000,
                    StockQuantity = 80,
                    CategoryId = 2
                },


                // Category 3
                new Product
                {
                    ProductId = 7,
                    Barcode = "893850003001",
                    ProductName = "Aquafina 500ml",
                    Price = 6000,
                    StockQuantity = 120,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 8,
                    Barcode = "893850003002",
                    ProductName = "Lavie 500ml",
                    Price = 6000,
                    StockQuantity = 100,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 9,
                    Barcode = "893850003003",
                    ProductName = "Dasani 500ml",
                    Price = 6000,
                    StockQuantity = 90,
                    CategoryId = 3
                },


                // Category 4
                new Product
                {
                    ProductId = 10,
                    Barcode = "893850004001",
                    ProductName = "Vinamilk Sữa tươi",
                    Price = 35000,
                    StockQuantity = 50,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 11,
                    Barcode = "893850004002",
                    ProductName = "TH True Milk",
                    Price = 36000,
                    StockQuantity = 45,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 12,
                    Barcode = "893850004003",
                    ProductName = "Yakult",
                    Price = 25000,
                    StockQuantity = 40,
                    CategoryId = 4
                },


                // Category 5
                new Product
                {
                    ProductId = 13,
                    Barcode = "893850005001",
                    ProductName = "Mì Hảo Hảo Tôm Chua Cay",
                    Price = 5000,
                    StockQuantity = 200,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 14,
                    Barcode = "893850005002",
                    ProductName = "Mì Omachi",
                    Price = 12000,
                    StockQuantity = 100,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 15,
                    Barcode = "893850005003",
                    ProductName = "Cháo ăn liền Vifon",
                    Price = 10000,
                    StockQuantity = 60,
                    CategoryId = 5
                },


                // Category 6
                new Product
                {
                    ProductId = 16,
                    Barcode = "893850006001",
                    ProductName = "Cá hộp 3 Cô Gái",
                    Price = 28000,
                    StockQuantity = 40,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 17,
                    Barcode = "893850006002",
                    ProductName = "Pate gan hộp",
                    Price = 22000,
                    StockQuantity = 35,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 18,
                    Barcode = "893850006003",
                    ProductName = "Đậu Hà Lan đóng hộp",
                    Price = 18000,
                    StockQuantity = 30,
                    CategoryId = 6
                },


                // Category 7
                new Product
                {
                    ProductId = 19,
                    Barcode = "893850007001",
                    ProductName = "Xúc xích CP",
                    Price = 10000,
                    StockQuantity = 70,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 20,
                    Barcode = "893850007002",
                    ProductName = "Sandwich thịt nguội",
                    Price = 25000,
                    StockQuantity = 25,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 21,
                    Barcode = "893850007003",
                    ProductName = "Hamburger bò",
                    Price = 30000,
                    StockQuantity = 20,
                    CategoryId = 7
                },


                // Category 8
                new Product
                {
                    ProductId = 22,
                    Barcode = "893850008001",
                    ProductName = "Cà phê hòa tan G7",
                    Price = 30000,
                    StockQuantity = 50,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 23,
                    Barcode = "893850008002",
                    ProductName = "Cà phê lon Birdy",
                    Price = 12000,
                    StockQuantity = 60,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 24,
                    Barcode = "893850008003",
                    ProductName = "Trà xanh C2",
                    Price = 10000,
                    StockQuantity = 70,
                    CategoryId = 8
                },


                // Category 9
                new Product
                {
                    ProductId = 25,
                    Barcode = "893850009001",
                    ProductName = "Nước mắm Nam Ngư",
                    Price = 35000,
                    StockQuantity = 40,
                    CategoryId = 9
                },

                new Product
                {
                    ProductId = 26,
                    Barcode = "893850009002",
                    ProductName = "Nước tương Chinsu",
                    Price = 18000,
                    StockQuantity = 45,
                    CategoryId = 9
                },

                new Product
                {
                    ProductId = 27,
                    Barcode = "893850009003",
                    ProductName = "Đường tinh luyện",
                    Price = 22000,
                    StockQuantity = 50,
                    CategoryId = 9
                },


                // Category 10
                new Product
                {
                    ProductId = 28,
                    Barcode = "893850010001",
                    ProductName = "Kem đánh răng P/S",
                    Price = 30000,
                    StockQuantity = 40,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 29,
                    Barcode = "893850010002",
                    ProductName = "Dầu gội Sunsilk",
                    Price = 45000,
                    StockQuantity = 35,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 30,
                    Barcode = "893850010003",
                    ProductName = "Sữa tắm Lifebuoy",
                    Price = 50000,
                    StockQuantity = 30,
                    CategoryId = 10
                },


                // Category 11
                new Product
                {
                    ProductId = 31,
                    Barcode = "893850011001",
                    ProductName = "Khẩu trang y tế",
                    Price = 25000,
                    StockQuantity = 100,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 32,
                    Barcode = "893850011002",
                    ProductName = "Băng cá nhân",
                    Price = 15000,
                    StockQuantity = 50,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 33,
                    Barcode = "893850011003",
                    ProductName = "Nước rửa tay Lifebuoy",
                    Price = 45000,
                    StockQuantity = 35,
                    CategoryId = 11
                },


                // Category 12
                new Product
                {
                    ProductId = 34,
                    Barcode = "893850012001",
                    ProductName = "Khăn giấy Pulppy",
                    Price = 25000,
                    StockQuantity = 60,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 35,
                    Barcode = "893850012002",
                    ProductName = "Nước rửa chén Sunlight",
                    Price = 35000,
                    StockQuantity = 40,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 36,
                    Barcode = "893850012003",
                    ProductName = "Túi rác tự hủy",
                    Price = 20000,
                    StockQuantity = 45,
                    CategoryId = 12
                },


                // Category 13
                new Product
                {
                    ProductId = 37,
                    Barcode = "893850013001",
                    ProductName = "Bút bi Thiên Long",
                    Price = 5000,
                    StockQuantity = 100,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 38,
                    Barcode = "893850013002",
                    ProductName = "Vở Campus",
                    Price = 25000,
                    StockQuantity = 50,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 39,
                    Barcode = "893850013003",
                    ProductName = "Bút chì 2B",
                    Price = 5000,
                    StockQuantity = 70,
                    CategoryId = 13
                },


                // Category 14
                new Product
                {
                    ProductId = 40,
                    Barcode = "893850014001",
                    ProductName = "Kem Merino",
                    Price = 12000,
                    StockQuantity = 50,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 41,
                    Barcode = "893850014002",
                    ProductName = "Xúc xích đông lạnh",
                    Price = 55000,
                    StockQuantity = 30,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 42,
                    Barcode = "893850014003",
                    ProductName = "Há cảo đông lạnh",
                    Price = 45000,
                    StockQuantity = 25,
                    CategoryId = 14
                },


                // Category 15
                new Product
                {
                    ProductId = 43,
                    Barcode = "893850015001",
                    ProductName = "Táo Fuji",
                    Price = 65000,
                    StockQuantity = 25,
                    CategoryId = 15
                },

                new Product
                {
                    ProductId = 44,
                    Barcode = "893850015002",
                    ProductName = "Chuối tiêu",
                    Price = 30000,
                    StockQuantity = 30,
                    CategoryId = 15
                },

                new Product
                {
                    ProductId = 45,
                    Barcode = "893850015003",
                    ProductName = "Cam sành",
                    Price = 40000,
                    StockQuantity = 30,
                    CategoryId = 15
                }
            );
            // =========================
            // CUSTOMER
            // =========================

            modelBuilder.Entity<Customer>().HasData(

                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn An",
                    PhoneNumber = "0901234567",
                    Address = "Quận 1, TP. Hồ Chí Minh",
                    RewardPoints = 120,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0912345678",
                    Address = "Quận 3, TP. Hồ Chí Minh",
                    RewardPoints = 50,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Minh Cường",
                    PhoneNumber = "0923456789",
                    Address = "Quận 7, TP. Hồ Chí Minh",
                    RewardPoints = 350,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0934567890",
                    Address = "Thủ Đức, TP. Hồ Chí Minh",
                    RewardPoints = 20,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Minh Đức",
                    PhoneNumber = "0945678901",
                    Address = "Quận Bình Thạnh, TP. Hồ Chí Minh",
                    RewardPoints = 800,
                    MembershipRank = "Kim cương"
                },

                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hạnh",
                    PhoneNumber = "0956789012",
                    Address = "Quận Tân Bình, TP. Hồ Chí Minh",
                    RewardPoints = 180,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Quốc Huy",
                    PhoneNumber = "0967890123",
                    Address = "Quận Gò Vấp, TP. Hồ Chí Minh",
                    RewardPoints = 420,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Ngọc Lan",
                    PhoneNumber = "0978901234",
                    Address = "Quận 10, TP. Hồ Chí Minh",
                    RewardPoints = 75,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Phan Thanh Long",
                    PhoneNumber = "0989012345",
                    Address = "Quận 5, TP. Hồ Chí Minh",
                    RewardPoints = 260,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Ngô Thị Mai",
                    PhoneNumber = "0990123456",
                    Address = "Quận 11, TP. Hồ Chí Minh",
                    RewardPoints = 620,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Đỗ Văn Nam",
                    PhoneNumber = "0881234567",
                    Address = "Quận Phú Nhuận, TP. Hồ Chí Minh",
                    RewardPoints = 15,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Vũ Thị Oanh",
                    PhoneNumber = "0872345678",
                    Address = "Quận 6, TP. Hồ Chí Minh",
                    RewardPoints = 290,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Đinh Minh Phúc",
                    PhoneNumber = "0863456789",
                    Address = "Quận Bình Tân, TP. Hồ Chí Minh",
                    RewardPoints = 950,
                    MembershipRank = "Kim cương"
                },

                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Mai Thị Quỳnh",
                    PhoneNumber = "0854567890",
                    Address = "Quận Tân Phú, TP. Hồ Chí Minh",
                    RewardPoints = 110,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Trương Hoàng Sơn",
                    PhoneNumber = "0845678901",
                    Address = "Quận 12, TP. Hồ Chí Minh",
                    RewardPoints = 45,
                    MembershipRank = "Chuẩn"
                }
            );
        }

    }
}
