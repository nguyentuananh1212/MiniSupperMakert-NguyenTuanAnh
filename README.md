🛒 MiniSupermarket – Buổi 3
SQL Server + Entity Framework Core Code-First

Project thực hành xây dựng hệ thống quản lý siêu thị mini với:

Backend: ASP.NET Core Web API
Frontend: Windows Forms
Database: Microsoft SQL Server
ORM: Entity Framework Core
API Testing: Swagger
IDE: Visual Studio 2022
📌 Nội dung Buổi 3
Tích hợp ASP.NET Core Web API với SQL Server.
Sử dụng Entity Framework Core Code-First.
Xây dựng DbContext, Entity và quan hệ giữa các bảng.
Thực hiện EF Core Migrations.
CRUD Categories.
Thực hành CRUD Customers.
Sử dụng LINQ và async/await.
Kết nối WinForms với Web API.
Lưu dữ liệu trực tiếp và lâu dài trên SQL Server.
🗂️ Cấu trúc chính
MiniSupermarket
│
├── MiniSupermarket.API
│   ├── Controllers
│   │   ├── CategoriesController.cs
│   │   └── CustomersController.cs
│   ├── Data
│   │   └── SupermarketDbContext.cs
│   ├── Models
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   └── Customer.cs
│   ├── Migrations
│   ├── Program.cs
│   └── appsettings.json
│
└── MiniSupermarket.WinForms

📦 NuGet Packages

Cài đặt trong MiniSupermarket.API:

Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design

🗄️ Database

Database sử dụng:

MiniSupermarketDb


Các bảng chính:

Users
Categories
Products
Customers
Orders
OrderDetails

⚙️ Cấu hình SQL Server

Chỉnh sửa appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}


Nếu sử dụng SQL Server Express:

Server=.\\SQLEXPRESS


Nếu sử dụng SQL Server Authentication:

Server=.;Database=MiniSupermarketDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;

Không commit password thật vào GitHub.
🔄 EF Core Migration

Mở Package Manager Console và chọn project:

MiniSupermarket.API


Chạy:

Add-Migration InitialCreateDatabase
Update-Database


Khi thêm bảng Customers:

Add-Migration AddCustomersTable
Update-Database

▶️ Chạy Project
1. Chạy Backend

Mở:

MiniSupermarket.API


Nhấn:

F5


Sau đó mở Swagger để kiểm thử API.

2. Chạy WinForms

Chạy:

MiniSupermarket.WinForms


Ứng dụng sẽ gọi API để thực hiện:

Thêm
Sửa
Xóa
Tìm kiếm
Hiển thị dữ liệu
🌐 API chính
Categories
GET     /api/categories
GET     /api/categories/{id}
GET     /api/categories/search?keyword=...
POST    /api/categories
PUT     /api/categories/{id}
DELETE  /api/categories/{id}

Customers
GET     /api/customers
GET     /api/customers/{id}
GET     /api/customers/search?keyword=...
POST    /api/customers
PUT     /api/customers/{id}
DELETE  /api/customers/{id}

✅ Kết quả

Sau khi hoàn thành, dữ liệu được lưu trực tiếp vào SQL Server và không bị mất khi tắt Web API hoặc WinForms.

🚀 Định hướng Buổi 4
Quản lý Products.
CRUD sản phẩm.
Quản lý tồn kho.
Tìm kiếm theo Barcode.
Lọc sản phẩm theo Category.
Sử dụng Include() để lấy dữ liệu quan hệ.

MiniSupermarket – Buổi 3
ASP.NET Core Web API + EF Core + SQL Server + WinForms

Tac Gia: Nguyen Tuan Anh
Lop: CCQ2411D
