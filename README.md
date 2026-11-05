# 🛒 MiniSupermarket – Nguyễn Tuấn Anh

> **Hệ thống Quản lý Siêu thị Mini** – Bài tập thực hành môn Lập trình Ứng dụng .NET Core  
> Sinh viên: **Nguyễn Tuấn Anh**  
> Nhánh: **Buổi 5 – ADO.NET Nâng cao, Stored Procedure & Giao dịch POS Checkout**  
> Nền tảng: **.NET 8 · ASP.NET Core Web API · Windows Forms · Entity Framework Core · ADO.NET · SQL Server**

---

## 📋 Mục lục

1. [Giới thiệu dự án](#1-giới-thiệu-dự-án)
2. [Kiến trúc hệ thống](#2-kiến-trúc-hệ-thống)
3. [Công nghệ sử dụng](#3-công-nghệ-sử-dụng)
4. [Cấu trúc thư mục](#4-cấu-trúc-thư-mục)
5. [Cơ sở dữ liệu & Migration](#5-cơ-sở-dữ-liệu--migration)
6. [Hướng dẫn cài đặt & chạy](#6-hướng-dẫn-cài-đặt--chạy)
7. [Danh sách API Endpoints](#7-danh-sách-api-endpoints)
8. [Các chức năng chính](#8-các-chức-năng-chính)
9. [Phân quyền người dùng](#9-phân-quyền-người-dùng)
10. [Nội dung Buổi 5 – ADO.NET Nâng cao & Giao dịch POS](#10-nội-dung-buổi-5--adonet-nâng-cao--giao-dịch-pos)
11. [Checklist tự đánh giá](#11-checklist-tự-đánh-giá)

---

## 1. Giới thiệu dự án

**MiniSupermarket Family Fresh** là ứng dụng quản lý siêu thị mini được xây dựng theo kiến trúc **3 lớp (3-Tier Architecture)**:

- **Tầng Giao diện (Presentation):** Windows Forms – giao diện bán hàng POS và quản lý nội bộ.
- **Tầng Xử lý (Business Logic):** ASP.NET Core Web API – xử lý nghiệp vụ, bảo mật JWT, phân quyền theo vai trò.
- **Tầng Dữ liệu (Data Access):** Entity Framework Core + ADO.NET thuần – kết nối SQL Server.

---

## 2. Kiến trúc hệ thống

```
┌─────────────────────────────────────┐
│   WinForms Client (Presentation)   │
│  FormLogin · FormPOS · FormMainShell│
│  FormOrderManagement · FormReports  │
└───────────────┬─────────────────────┘
                │ HTTP + JWT Bearer Token
                ▼
┌─────────────────────────────────────┐
│   ASP.NET Core Web API (Port 5237) │
│  Controllers · JWT Auth · EF Core  │
│  ADO.NET · SqlTransaction (ACID)   │
└───────────────┬─────────────────────┘
                │ SQL Connection
                ▼
┌─────────────────────────────────────┐
│   SQL Server – MiniSupermarketDb   │
│  Orders · Products · Customers     │
│  Categories · Users · OrderDetails │
└─────────────────────────────────────┘
```

---

## 3. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Backend API | ASP.NET Core 8 Web API |
| Giao diện | Windows Forms (.NET 8) |
| ORM | Entity Framework Core 8 |
| ADO.NET thuần | `SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlTransaction` |
| Bảo mật | JWT Bearer Token, BCrypt password hashing |
| CSDL | Microsoft SQL Server |
| Kiểm tra API | Swagger / OpenAPI |

---

## 4. Cấu trúc thư mục

```
MiniSupperMakert-NguyenTuanAnh/
│
├── MiniSupermarket.API/                  # Backend Web API
│   ├── Controllers/
│   │   ├── AuthController.cs             # Đăng nhập, cấp JWT
│   │   ├── ProductsController.cs         # Quản lý sản phẩm (EF Core)
│   │   ├── AdoProductController.cs       # Tra cứu Barcode (ADO.NET thuần)
│   │   ├── CategoriesController.cs       # Quản lý danh mục
│   │   ├── CustomersController.cs        # Khách hàng & tích điểm thưởng
│   │   ├── UsersController.cs            # Quản trị tài khoản người dùng
│   │   ├── CheckoutController.cs         # Thanh toán POS (ADO.NET + SqlTransaction)
│   │   ├── OrdersController.cs           # Quản lý đơn hàng (EF Core)
│   │   └── ReportsController.cs          # Báo cáo doanh thu (Stored Procedure)
│   ├── Models/
│   │   ├── Order.cs · OrderDetail.cs
│   │   ├── Product.cs · Category.cs
│   │   ├── Customer.cs · User.cs
│   │   └── OrderStatus.cs
│   ├── Dtos/
│   │   └── OrderManagementDto.cs
│   ├── Data/
│   │   └── SupermarketDbContext.cs
│   └── Migrations/                       # EF Core migrations
│
└── MiniSupermarket.WinForms/            # Frontend Windows Forms
    ├── FormLogin.cs                      # Đăng nhập + lấy JWT Token
    ├── FormMainShell.cs                  # Khung chính + sidebar điều hướng
    ├── FormPOS.cs                        # Quầy bán hàng POS
    ├── FormOrderManagement.cs            # ✅ Quản lý đơn hàng (MỚI – Buổi 5)
    ├── FormProductManagement.cs          # Quản lý sản phẩm
    ├── FormCategoryManagement.cs         # Quản lý danh mục
    ├── FormCustomerManagement.cs         # Quản lý khách hàng
    ├── FormUserManagement.cs             # Quản trị tài khoản
    ├── FormQuickReport.cs                # Báo cáo nhanh doanh thu
    ├── SessionManager.cs                 # Lưu phiên đăng nhập + JWT
    └── Services/ApiClientService.cs      # HttpClient gắn Bearer Token tự động
```

---

## 5. Cơ sở dữ liệu & Migration

### Các bảng chính

| Bảng | Mô tả |
|---|---|
| `Products` | Sản phẩm, giá bán, tồn kho, mã vạch |
| `Categories` | Danh mục nhóm hàng |
| `Customers` | Khách hàng thân thiết, điểm thưởng, hạng thành viên |
| `Users` | Tài khoản đăng nhập, vai trò (Admin/Cashier/Warehouse) |
| `Orders` | Hóa đơn bán hàng (OrderId, CashierUsername, TotalAmount, **Status**) |
| `OrderDetails` | Chi tiết từng sản phẩm trong hóa đơn (ProductId, Quantity, UnitPrice) |

### Chạy Migration

```bash
cd MiniSupermarket.API
dotnet ef database update
```

---

## 6. Hướng dẫn cài đặt & chạy

### Yêu cầu môi trường
- .NET SDK 8.0+
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2022 hoặc VS Code

### Bước 1 – Cấu hình chuỗi kết nối

Mở `MiniSupermarket.API/appsettings.json`, cập nhật:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Bước 2 – Tạo cơ sở dữ liệu

```bash
cd MiniSupermarket.API
dotnet ef database update
```

### Bước 3 – Tạo Stored Procedure trên SQL Server

Mở **SSMS** và chạy script sau:

```sql
CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    SELECT
        CashierUsername,
        COUNT(*)         AS TotalOrders,
        SUM(TotalAmount) AS TotalRevenue
    FROM Orders
    WHERE Status != 2   -- Bỏ qua đơn đã hủy
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC
END
```

### Bước 4 – Chạy Web API

```bash
cd MiniSupermarket.API
dotnet run
# API chạy tại : https://localhost:5237
# Swagger UI  : https://localhost:5237/swagger
```

### Bước 5 – Chạy WinForms

```bash
cd MiniSupermarket.WinForms
dotnet run
```

> **Tài khoản mặc định:** `admin` / `admin123` (vai trò Admin)

---

## 7. Danh sách API Endpoints

### 🔐 Xác thực
| Method | Endpoint | Mô tả |
|---|---|---|
| POST | `/api/auth/login` | Đăng nhập, trả về JWT Token |

### 📦 Sản phẩm
| Method | Endpoint | Mô tả |
|---|---|---|
| GET | `/api/products` | Lấy danh sách sản phẩm (EF Core) |
| GET | `/api/products/{id}` | Lấy sản phẩm theo ID |
| POST | `/api/products` | Thêm sản phẩm mới |
| PUT | `/api/products/{id}` | Cập nhật thông tin sản phẩm |
| DELETE | `/api/products/{id}` | Xoá sản phẩm |
| GET | `/api/ado-products/barcode/{barcode}` | ⚡ Tra cứu nhanh bằng mã vạch (**ADO.NET**) |

### 🛒 Bán hàng & Đơn hàng
| Method | Endpoint | Mô tả |
|---|---|---|
| POST | `/api/checkout` | Thanh toán POS (**ADO.NET + SqlTransaction ACID**) |
| DELETE | `/api/checkout/{orderId}` | Hủy đơn + hoàn trả kho (**SqlTransaction**) |
| GET | `/api/orders` | Danh sách tất cả đơn hàng + chi tiết |
| GET | `/api/orders/{id}` | Chi tiết một đơn hàng |
| PUT | `/api/orders/{id}` | Cập nhật trạng thái đơn hàng |
| DELETE | `/api/orders/{id}` | Hủy đơn hàng (EF Core + Transaction) |

### 👥 Khách hàng
| Method | Endpoint | Mô tả |
|---|---|---|
| GET | `/api/customers` | Danh sách khách hàng thân thiết |
| POST | `/api/customers` | Thêm khách hàng mới |
| PUT | `/api/customers/{id}` | Cập nhật thông tin khách hàng |

### 📊 Báo cáo
| Method | Endpoint | Mô tả |
|---|---|---|
| GET | `/api/reports/revenue` | Báo cáo doanh thu theo thu ngân (**Stored Procedure**) |

---

## 8. Các chức năng chính

### 🖥️ Quầy bán hàng POS
- Quét mã vạch → thêm sản phẩm vào giỏ hàng tự động
- Hỗ trợ phím tắt: `F2` (focus ô barcode), `F9` (thanh toán), `+/-` (tăng/giảm số lượng), `Delete` (xoá dòng)
- Nhận diện khách hàng thân thiết qua số điện thoại
- Đổi điểm thưởng để giảm tiền hóa đơn (1 điểm = 1.000 VNĐ)
- Thanh toán bảo vệ bởi **SqlTransaction ACID**: tạo Order + OrderDetails + trừ kho + tích điểm trong 1 giao dịch

### 📋 Quản lý đơn hàng ✅ *(Mới – Buổi 5)*
- Xem danh sách toàn bộ đơn hàng: mã, ngày lập, thu ngân, tổng tiền, trạng thái
- Click dòng → xem chi tiết từng sản phẩm trong đơn (mã SP, số lượng, đơn giá, thành tiền)
- Hủy đơn hàng → tự động khôi phục tồn kho và điểm thưởng khách hàng
- Trạng thái đơn: `Pending (Chờ)` / `Completed (Hoàn thành)` / `Cancelled (Đã hủy)`

### 👤 Quản lý khách hàng
- Thêm/sửa/xoá khách hàng thân thiết
- Tích điểm tự động: 10.000 VNĐ = 1 điểm thưởng
- Hạng thành viên tự động: Đồng → Bạc → Vàng → Kim cương

### 📊 Báo cáo doanh thu
- Báo cáo doanh thu phân theo từng thu ngân (gọi **Stored Procedure** `sp_GetRevenueByCashier`)

---

## 9. Phân quyền người dùng

| Vai trò | Quyền truy cập |
|---|---|
| **Admin** | Toàn quyền: POS · Danh mục · Sản phẩm · Khách hàng · Báo cáo · Tài khoản · **Quản lý đơn hàng** |
| **Cashier** (Thu ngân) | Chỉ truy cập: Quầy bán hàng POS |
| **Warehouse** (Kho) | Chỉ truy cập: Quản lý Danh mục + Sản phẩm |

> Phân quyền được kiểm tra **2 lớp**: (1) Ẩn/hiện nút sidebar theo role; (2) Kiểm tra lại khi click trước khi mở form.

---

## 10. Nội dung Buổi 5 – ADO.NET Nâng cao & Giao dịch POS

### 🔹 Tiết 1 – Kiến trúc ADO.NET cơ bản

**5 đối tượng cốt lõi:**

| Đối tượng ADO.NET | Hình tượng thực tế | Vai trò kỹ thuật |
|---|---|---|
| `SqlConnection` | Đường dây điện thoại tới siêu thị | Mở/đóng kết nối vật lý tới SQL Server |
| `SqlCommand` | Tờ đơn đặt hàng | Chứa câu lệnh SQL / tên Stored Procedure |
| `SqlParameter` | Chi tiết ghi rõ ràng, an toàn | Truyền tham số – **chống SQL Injection** |
| `SqlDataReader` | Băng chuyền quét mã tính tiền | Đọc dữ liệu forward-only, tiết kiệm RAM |
| `DataTable` / `SqlDataAdapter` | Thùng hàng đóng gói gửi về nhà | Đọc ngắt kết nối, lưu toàn bộ vào bộ nhớ |

**3 phương thức thực thi SqlCommand:**

```csharp
// 1. Trả về 1 giá trị đơn (tổng tiền, số lượng, ID mới...)
var total = (decimal)cmd.ExecuteScalar();

// 2. Thực thi INSERT / UPDATE / DELETE, trả về số dòng bị ảnh hưởng
int rowsAffected = cmd.ExecuteNonQuery();

// 3. Đọc nhiều dòng dữ liệu (forward-only, nhanh nhất)
using var reader = cmd.ExecuteReader();
while (reader.Read())
{
    var id   = reader.GetInt32(0);
    var name = reader.GetString(1);
}
```

**Quy tắc vàng – luôn dùng `using`:**
```csharp
using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    // ... dùng connection ...
} // Tự động Close() và Dispose() dù có ngoại lệ
```

---

### 🔹 Tiết 2 – Tra cứu Barcode với ADO.NET thuần

```csharp
// GET /api/ado-products/barcode/{barcode}
// Dùng SqlDataReader để tra cứu siêu nhanh < 100ms
using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    string sql = @"SELECT ProductId, ProductName, Price, StockQuantity
                   FROM Products
                   WHERE Barcode = @Barcode";

    using SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Barcode", barcode); // Chống SQL Injection

    using SqlDataReader reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        return Ok(new {
            productId   = reader.GetInt32(0),
            productName = reader.GetString(1),
            price       = reader.GetDecimal(2),
            stock       = reader.GetInt32(3)
        });
    }
    return NotFound();
}
```

---

### 🔹 Tiết 3 – Stored Procedure báo cáo doanh thu

**Tạo Stored Procedure trên SSMS:**

```sql
CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    SELECT
        CashierUsername,
        COUNT(*)         AS TotalOrders,
        SUM(TotalAmount) AS TotalRevenue
    FROM Orders
    WHERE Status != 2
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC
END
```

**Gọi Stored Procedure từ C#:**

```csharp
using SqlCommand cmd = new SqlCommand("sp_GetRevenueByCashier", conn);
cmd.CommandType = CommandType.StoredProcedure;
using SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    string cashier = reader.GetString(0);
    int    orders  = reader.GetInt32(1);
    decimal revenue = reader.GetDecimal(2);
}
```

---

### 🔹 Tiết 4 – Chuẩn ACID & SqlTransaction

| Tiêu chí | Ý nghĩa |
|---|---|
| **A**tomicity – Nguyên tử | Tất cả bước thành công hoặc không có bước nào được lưu |
| **C**onsistency – Nhất quán | CSDL luôn hợp lệ trước và sau giao dịch |
| **I**solation – Cô lập | Các giao dịch đồng thời không can thiệp lẫn nhau |
| **D**urability – Bền vững | Sau `Commit`, dữ liệu lưu vĩnh viễn dù mất điện |

```csharp
SqlTransaction transaction = connection.BeginTransaction();
try
{
    // Bước 1: Tạo Orders
    // Bước 2: Thêm OrderDetails
    // Bước 3: Trừ StockQuantity
    // Bước 4: Cộng điểm thưởng Customers

    transaction.Commit();   // ✅ Lưu tất cả vĩnh viễn
}
catch (Exception ex)
{
    transaction.Rollback(); // ❌ Hoàn tác sạch sẽ, không để lại dữ liệu rác
    return BadRequest(new { message = "Lỗi: " + ex.Message });
}
```

---

### 🔹 Tiết 5 – API Checkout POS & Hủy đơn hàng

#### Luồng Thanh toán (`POST /api/checkout`)

```
1. Kiểm tra tồn kho từng sản phẩm trong giỏ
       ↓
2. BeginTransaction()
       ↓
3. INSERT vào bảng Orders (tạo hóa đơn tổng)
       ↓
4. INSERT từng dòng vào bảng OrderDetails
       ↓
5. UPDATE StockQuantity -= Quantity (trừ kho)
       ↓
6. UPDATE RewardPoints += điểm mới (tích điểm khách hàng)
       ↓
7. Commit() → Trả về thành công
   hoặc Rollback() → Không để lại dữ liệu rác
```

#### Luồng Hủy đơn hàng (`DELETE /api/checkout/{orderId}`)

```
1. Lấy thông tin đơn hàng + điểm thưởng đã tích
       ↓
2. Lấy danh sách sản phẩm trong OrderDetails
       ↓
3. BeginTransaction()
       ↓
4. UPDATE StockQuantity += Quantity (hoàn trả kho)
       ↓
5. UPDATE RewardPoints -= điểm đã tích (trừ điểm khách hàng)
       ↓
6. DELETE OrderDetails (xóa chi tiết trước)
       ↓
7. DELETE Orders (xóa hóa đơn tổng)
       ↓
8. Commit() → Hủy thành công
   hoặc Rollback() → An toàn, dữ liệu nguyên vẹn
```

---

## 11. Checklist tự đánh giá

| STT | Hạng mục kiểm tra | Yêu cầu đạt chuẩn | Kết quả |
|---|---|---|---|
| 1 | **ADO.NET cơ bản** (Tiết 1) | Dùng khối `using`, `SqlParameter` chống SQL Injection, hiểu `ExecuteScalar/NonQuery/Reader` | ✅ Đạt |
| 2 | **Tra cứu Barcode** (Tiết 2) | `GET /api/ado-products/barcode/{barcode}` trả kết quả < 100ms bằng `SqlDataReader` | ✅ Đạt |
| 3 | **Stored Procedure** (Tiết 3) | Tạo thành công `sp_GetRevenueByCashier` trên SSMS, gọi từ API Reports | ✅ Đạt |
| 4 | **Chuẩn ACID** (Tiết 4) | Trình bày được 4 tiêu chí ACID, dùng `BeginTransaction`, `Commit`, `Rollback` | ✅ Đạt |
| 5 | **API Checkout** (Tiết 5) | `POST /api/checkout` thực hiện đồng thời: tạo Order + OrderDetails + trừ kho + tích điểm | ✅ Đạt |
| 6 | **Kiểm chứng Rollback** (Tiết 5) | Đặt hàng vượt tồn kho → API lỗi → Rollback, không để lại dữ liệu rác trong CSDL | ✅ Đạt |
| 7 | **Bài tập mở rộng** (Tiết 5) | Hoàn thành API Hủy đơn + hoàn trả tồn kho tự động bằng `SqlTransaction` | ✅ Đạt |
| 8 | **Quản lý đơn hàng UI** (Bổ sung) | Form WinForms xem danh sách, chi tiết từng dòng và hủy đơn hàng | ✅ Đạt |

---

## 📌 Ghi chú kỹ thuật

- Toàn bộ mật khẩu được mã hóa bằng **BCrypt** trước khi lưu vào CSDL.
- **JWT Token** hết hạn sau 8 giờ; WinForms tự động gắn vào header mỗi request.
- Mọi thao tác ghi quan trọng (checkout, hủy đơn) được bảo vệ bởi **SqlTransaction** đảm bảo chuẩn ACID.
- Phân quyền kiểm tra **2 lớp**: ẩn/hiện nút UI (lớp 1) + chặn logic khi click (lớp 2).
- Nút **Quản lý đơn hàng** chỉ hiển thị cho vai trò **Admin** trên sidebar.

---

*© 2026 – Nguyễn Tuấn Anh – Bài tập thực hành Lập trình Ứng dụng .NET Core – Buổi 5*
