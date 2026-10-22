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

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

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
            // QUAN HỆ ORDER - ORDERDETAIL - PRODUCT - CUSTOMER
            // =====================================================

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Order)
                .WithMany(o => o.Details)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // SEED CATEGORY
            // =====================================================

            modelBuilder.Entity<Category>().HasData(

    new Category
    {
        CategoryId = 1,
        CategoryName = "Bánh kẹo & Đồ ăn vặt",
        Description = "Bánh quy, snack, kẹo, chocolate và các món ăn vặt"
    },

    new Category
    {
        CategoryId = 2,
        CategoryName = "Nước giải khát",
        Description = "Nước ngọt, nước có ga, nước tăng lực và đồ uống đóng lon"
    },

    new Category
    {
        CategoryId = 3,
        CategoryName = "Nước uống đóng chai",
        Description = "Nước suối, nước khoáng và nước tinh khiết đóng chai"
    },

    new Category
    {
        CategoryId = 4,
        CategoryName = "Sữa & Đồ uống dinh dưỡng",
        Description = "Sữa tươi, sữa hộp, sữa chua và đồ uống dinh dưỡng"
    },

    new Category
    {
        CategoryId = 5,
        CategoryName = "Mì & Thực phẩm ăn liền",
        Description = "Mì gói, mì ly, cháo, phở và thực phẩm ăn liền"
    },

    new Category
    {
        CategoryId = 6,
        CategoryName = "Thực phẩm đóng hộp",
        Description = "Cá hộp, thịt hộp, pate và thực phẩm đóng hộp"
    },

    new Category
    {
        CategoryId = 7,
        CategoryName = "Đồ ăn nhanh",
        Description = "Sandwich, hamburger, xúc xích và các món ăn tiện lợi"
    },

    new Category
    {
        CategoryId = 8,
        CategoryName = "Cà phê & Trà",
        Description = "Cà phê hòa tan, cà phê lon, trà đóng chai và trà túi lọc"
    },

    new Category
    {
        CategoryId = 9,
        CategoryName = "Gia vị & Thực phẩm khô",
        Description = "Nước mắm, nước tương, đường, dầu ăn và gia vị"
    },

    new Category
    {
        CategoryId = 10,
        CategoryName = "Chăm sóc cá nhân",
        Description = "Kem đánh răng, bàn chải, dầu gội, sữa tắm và đồ vệ sinh cá nhân"
    },

    new Category
    {
        CategoryId = 11,
        CategoryName = "Chăm sóc sức khỏe",
        Description = "Khẩu trang, băng cá nhân, nước rửa tay và sản phẩm chăm sóc sức khỏe"
    },

    new Category
    {
        CategoryId = 12,
        CategoryName = "Đồ dùng gia đình",
        Description = "Khăn giấy, túi rác, nước rửa chén và các sản phẩm gia dụng"
    },

    new Category
    {
        CategoryId = 13,
        CategoryName = "Văn phòng phẩm",
        Description = "Bút, vở, giấy và dụng cụ học tập, văn phòng"
    },

    new Category
    {
        CategoryId = 14,
        CategoryName = "Thực phẩm đông lạnh",
        Description = "Kem, xúc xích, há cảo và các sản phẩm đông lạnh"
    },

    new Category
    {
        CategoryId = 15,
        CategoryName = "Rau củ & Trái cây tươi",
        Description = "Rau củ, trái cây và thực phẩm tươi tại Family Fresh"
    }

);


            // =====================================================
            // SEED PRODUCT
            // =====================================================

            modelBuilder.Entity<Product>().HasData(

    // Category 1 - Bánh kẹo & Đồ ăn vặt
    new Product
    {
        ProductId = 1,
        Barcode = "893850001001",
        ProductName = "Bánh quy Cosy",
        Price = 18000,
        StockQuantity = 50,
        CategoryId = 1
    },
    new Product
    {
        ProductId = 2,
        Barcode = "893850001002",
        ProductName = "Snack khoai tây Oishi",
        Price = 12000,
        StockQuantity = 60,
        CategoryId = 1
    },
    new Product
    {
        ProductId = 3,
        Barcode = "893850001003",
        ProductName = "Kẹo dẻo Alpenliebe",
        Price = 15000,
        StockQuantity = 40,
        CategoryId = 1
    },

    // Category 2 - Nước giải khát
    new Product
    {
        ProductId = 4,
        Barcode = "893850002001",
        ProductName = "Coca-Cola lon 330ml",
        Price = 10000,
        StockQuantity = 100,
        CategoryId = 2
    },
    new Product
    {
        ProductId = 5,
        Barcode = "893850002002",
        ProductName = "Pepsi lon 330ml",
        Price = 10000,
        StockQuantity = 100,
        CategoryId = 2
    },
    new Product
    {
        ProductId = 6,
        Barcode = "893850002003",
        ProductName = "Sting dâu 330ml",
        Price = 10000,
        StockQuantity = 80,
        CategoryId = 2
    },

    // Category 3 - Nước suối
    new Product
    {
        ProductId = 7,
        Barcode = "893850003001",
        ProductName = "Nước suối Aquafina 500ml",
        Price = 6000,
        StockQuantity = 120,
        CategoryId = 3
    },
    new Product
    {
        ProductId = 8,
        Barcode = "893850003002",
        ProductName = "Nước suối Lavie 500ml",
        Price = 6000,
        StockQuantity = 100,
        CategoryId = 3
    },
    new Product
    {
        ProductId = 9,
        Barcode = "893850003003",
        ProductName = "Nước suối Dasani 500ml",
        Price = 6000,
        StockQuantity = 90,
        CategoryId = 3
    },

    // Category 4 - Sữa & sản phẩm từ sữa
    new Product
    {
        ProductId = 10,
        Barcode = "893850004001",
        ProductName = "Sữa tươi Vinamilk 180ml",
        Price = 8000,
        StockQuantity = 80,
        CategoryId = 4
    },
    new Product
    {
        ProductId = 11,
        Barcode = "893850004002",
        ProductName = "Sữa tươi TH True Milk 180ml",
        Price = 9000,
        StockQuantity = 70,
        CategoryId = 4
    },
    new Product
    {
        ProductId = 12,
        Barcode = "893850004003",
        ProductName = "Sữa chua Vinamilk",
        Price = 7000,
        StockQuantity = 60,
        CategoryId = 4
    },

    // Category 5 - Mì gói & Thực phẩm ăn liền
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
        ProductName = "Mì Omachi Sườn Hầm Ngũ Quả",
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

    // Category 6 - Đồ hộp
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

    // Category 7 - Thực phẩm chế biến sẵn
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
        ProductName = "Hamburger bò Family Fresh",
        Price = 30000,
        StockQuantity = 20,
        CategoryId = 7
    },

    // Category 8 - Cà phê & Trà
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

    // Category 9 - Gia vị & Dầu ăn
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
        ProductName = "Dầu ăn Neptune 1L",
        Price = 45000,
        StockQuantity = 40,
        CategoryId = 9
    },

    // Category 10 - Chăm sóc cá nhân
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

    // Category 11 - Sức khỏe
    new Product
    {
        ProductId = 31,
        Barcode = "893850011001",
        ProductName = "Khẩu trang y tế 4 lớp",
        Price = 25000,
        StockQuantity = 100,
        CategoryId = 11
    },
    new Product
    {
        ProductId = 32,
        Barcode = "893850011002",
        ProductName = "Băng cá nhân cá nhân",
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

    // Category 12 - Đồ dùng gia đình
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

    // Category 13 - Văn phòng phẩm
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
        ProductName = "Bút chì 2B Thiên Long",
        Price = 5000,
        StockQuantity = 70,
        CategoryId = 13
    },

    // Category 14 - Thực phẩm đông lạnh
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

    // Category 15 - Rau củ quả & Trái cây
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
                    Address = "25 Nguyễn Huệ, Phường Bến Nghé, TP. Hồ Chí Minh",
                    RewardPoints = 120,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0912345678",
                    Address = "148 Võ Văn Tần, Phường Võ Thị Sáu, TP. Hồ Chí Minh",
                    RewardPoints = 50,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Minh Cường",
                    PhoneNumber = "0923456789",
                    Address = "72 Nguyễn Thị Thập, Phường Tân Quy, TP. Hồ Chí Minh",
                    RewardPoints = 350,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0934567890",
                    Address = "186 Võ Văn Ngân, Phường Bình Thọ, TP. Hồ Chí Minh",
                    RewardPoints = 20,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Minh Đức",
                    PhoneNumber = "0945678901",
                    Address = "42 Điện Biên Phủ, Phường 22, TP. Hồ Chí Minh",
                    RewardPoints = 800,
                    MembershipRank = "Kim cương"
                },

                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hạnh",
                    PhoneNumber = "0956789012",
                    Address = "315 Cộng Hòa, Phường 13, TP. Hồ Chí Minh",
                    RewardPoints = 180,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Quốc Huy",
                    PhoneNumber = "0967890123",
                    Address = "89 Quang Trung, Phường 10, TP. Hồ Chí Minh",
                    RewardPoints = 420,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Ngọc Lan",
                    PhoneNumber = "0978901234",
                    Address = "156 Thành Thái, Phường 12, TP. Hồ Chí Minh",
                    RewardPoints = 75,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Phan Thanh Long",
                    PhoneNumber = "0989012345",
                    Address = "214 Nguyễn Trãi, Phường 3, TP. Hồ Chí Minh",
                    RewardPoints = 260,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Ngô Thị Mai",
                    PhoneNumber = "0990123456",
                    Address = "58 Lạc Long Quân, Phường 10, TP. Hồ Chí Minh",
                    RewardPoints = 620,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Đỗ Văn Nam",
                    PhoneNumber = "0881234567",
                    Address = "127 Phan Đình Phùng, Phường 17, TP. Hồ Chí Minh",
                    RewardPoints = 15,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Vũ Thị Oanh",
                    PhoneNumber = "0872345678",
                    Address = "96 Hậu Giang, Phường 11, TP. Hồ Chí Minh",
                    RewardPoints = 290,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Đinh Minh Phúc",
                    PhoneNumber = "0863456789",
                    Address = "235 Tỉnh Lộ 10, Phường Bình Trị Đông, TP. Hồ Chí Minh",
                    RewardPoints = 950,
                    MembershipRank = "Kim cương"
                },

                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Mai Thị Quỳnh",
                    PhoneNumber = "0854567890",
                    Address = "74 Lũy Bán Bích, Phường Tân Thành, TP. Hồ Chí Minh",
                    RewardPoints = 110,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Trương Hoàng Sơn",
                    PhoneNumber = "0845678901",
                    Address = "318 Nguyễn Văn Quá, Phường Đông Hưng Thuận, TP. Hồ Chí Minh",
                    RewardPoints = 45,
                    MembershipRank = "Chuẩn"
                }
            );

            modelBuilder.Entity<User>().HasData(
        new User
        {
            UserId = 1,
            Username = "admin01",
            Password = "123456",
            FullName = "Nguyễn Quản Trị",
            Role = "Admin"
        },

        new User
        {
            UserId = 2,
            Username = "admin02",
            Password = "123456",
            FullName = "Trần Giám Đốc",
            Role = "Admin"
        },

        new User
        {
            UserId = 3,
            Username = "cashier01",
            Password = "123456",
            FullName = "Lê Thu Ngân",
            Role = "Cashier"
        },

        new User
        {
            UserId = 4,
            Username = "cashier02",
            Password = "123456",
            FullName = "Phạm Bán Hàng",
            Role = "Cashier"
        },

        new User
        {
            UserId = 5,
            Username = "cashier03",
            Password = "123456",
            FullName = "Hoàng Thu Ngân",
            Role = "Cashier"
        },

        new User
        {
            UserId = 6,
            Username = "cashier04",
            Password = "123456",
            FullName = "Vũ Thị Quầy",
            Role = "Cashier"
        },

        new User
        {
            UserId = 7,
            Username = "cashier05",
            Password = "123456",
            FullName = "Đỗ Bán Lẻ",
            Role = "Cashier"
        },

        new User
        {
            UserId = 8,
            Username = "ware01",
            Password = "123456",
            FullName = "Ngô Quản Kho",
            Role = "Warehouse"
        },

        new User
        {
            UserId = 9,
            Username = "ware02",
            Password = "123456",
            FullName = "Bùi Kiểm Kê",
            Role = "Warehouse"
        },

        new User
        {
            UserId = 10,
            Username = "ware03",
            Password = "123456",
            FullName = "Dương Thủ Kho",
            Role = "Warehouse"
        },

        new User
        {
            UserId = 11,
            Username = "ware04",
            Password = "123456",
            FullName = "Lý Nhập Hàng",
            Role = "Warehouse"
        },

        new User
        {
            UserId = 12,
            Username = "admin_backup",
            Password = "123456",
            FullName = "Đặng Hỗ Trợ",
            Role = "Admin"
        },

        new User
        {
            UserId = 13,
            Username = "cashier06",
            Password = "123456",
            FullName = "Hồ Ca Chiều",
            Role = "Cashier"
        },

        new User
        {
            UserId = 14,
            Username = "ware05",
            Password = "123456",
            FullName = "Trương Vận Chuyển",
            Role = "Warehouse"
        },

        new User
        {
            UserId = 15,
            Username = "supervisor",
            Password = "123456",
            FullName = "Mai Giám Sát",
            Role = "Admin"
        }
    );
        }

    }
}
