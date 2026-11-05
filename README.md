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

Ứng dụng giao tiếp theo mô hình **Client – Server** qua giao thức HTTP với JWT Bearer Token:

- **WinForms** gửi request lên **ASP.NET Core Web API**
- **Web API** xử lý nghiệp vụ, xác thực quyền và thao tác với **SQL Server** thông qua EF Core hoặc ADO.NET thuần

---

## 3. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Backend API | ASP.NET Core 8 Web API |
| Giao diện | Windows Forms (.NET 8) |
| ORM | Entity Framework Core 8 |
| ADO.NET thuần | SqlConnection, SqlCommand, SqlDataReader, SqlTransaction |
| Bảo mật | JWT Bearer Token, BCrypt password hashing |
| Cơ sở dữ liệu | Microsoft SQL Server |
| Kiểm tra API | Swagger / OpenAPI |

---

## 4. Cấu trúc thư mục

**MiniSupermarket.API** – Backend Web API:
- `Controllers/AuthController.cs` – Đăng nhập, cấp JWT Token
- `Controllers/ProductsController.cs` – Quản lý sản phẩm (EF Core)
- `Controllers/AdoProductController.cs` – Tra cứu Barcode (ADO.NET thuần)
- `Controllers/CategoriesController.cs` – Quản lý danh mục
- `Controllers/CustomersController.cs` – Khách hàng & tích điểm thưởng
- `Controllers/UsersController.cs` – Quản trị tài khoản người dùng
- `Controllers/CheckoutController.cs` – Thanh toán POS (ADO.NET + SqlTransaction)
- `Controllers/OrdersController.cs` – Quản lý đơn hàng (EF Core)
- `Controllers/ReportsController.cs` – Báo cáo doanh thu (Stored Procedure)
- `Models/` – Các lớp thực thể: Order, OrderDetail, Product, Category, Customer, User, OrderStatus
- `Dtos/` – Các lớp DTO trao đổi dữ liệu với client
- `Data/SupermarketDbContext.cs` – DbContext EF Core
- `Migrations/` – Lịch sử migration cơ sở dữ liệu

**MiniSupermarket.WinForms** – Frontend Windows Forms:
- `FormLogin.cs` – Đăng nhập + lấy JWT Token
- `FormMainShell.cs` – Khung chính + sidebar điều hướng + phân quyền
- `FormPOS.cs` – Quầy bán hàng POS
- `FormOrderManagement.cs` – ✅ Quản lý đơn hàng *(Mới – Buổi 5)*
- `FormProductManagement.cs` – Quản lý sản phẩm
- `FormCategoryManagement.cs` – Quản lý danh mục
- `FormCustomerManagement.cs` – Quản lý khách hàng
- `FormUserManagement.cs` – Quản trị tài khoản
- `FormQuickReport.cs` – Báo cáo nhanh doanh thu
- `SessionManager.cs` – Lưu phiên đăng nhập + JWT
- `Services/ApiClientService.cs` – HttpClient tự động gắn Bearer Token

---

## 5. Cơ sở dữ liệu & Migration

### Các bảng chính

| Bảng | Mô tả |
|---|---|
| Products | Sản phẩm, giá bán, tồn kho, mã vạch |
| Categories | Danh mục nhóm hàng |
| Customers | Khách hàng thân thiết, điểm thưởng, hạng thành viên |
| Users | Tài khoản đăng nhập, vai trò (Admin/Cashier/Warehouse) |
| Orders | Hóa đơn bán hàng: mã, thu ngân, tổng tiền, trạng thái |
| OrderDetails | Chi tiết từng sản phẩm trong hóa đơn: số lượng, đơn giá |

### Các Migration đã thực hiện

| Migration | Nội dung |
|---|---|
| InitialCreateDatabase | Tạo bảng Products, Categories, Users |
| AddCustomersTable | Thêm bảng Customers & điểm thưởng |
| AddOrdersTables | Thêm bảng Orders & OrderDetails |
| UpdateCreateDatabase | Cập nhật cấu trúc bảng |
| FamilyFresh | Cập nhật dữ liệu danh mục Family Fresh |

---

## 6. Hướng dẫn cài đặt & chạy

### Yêu cầu môi trường
- .NET SDK 8.0 trở lên
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2022 hoặc VS Code

### Bước 1 – Cấu hình chuỗi kết nối
Mở file `MiniSupermarket.API/appsettings.json`, cập nhật `Server` và `Database` cho phù hợp với máy.

### Bước 2 – Tạo cơ sở dữ liệu
Chạy lệnh `dotnet ef database update` trong thư mục `MiniSupermarket.API`.

### Bước 3 – Tạo Stored Procedure trên SSMS
Mở SQL Server Management Studio và tạo procedure `sp_GetRevenueByCashier` để tổng hợp doanh thu theo từng thu ngân.

### Bước 4 – Chạy Web API
Chạy dự án `MiniSupermarket.API`. API sẽ chạy tại `https://localhost:5237` và Swagger UI tại `/swagger`.

### Bước 5 – Chạy WinForms
Chạy dự án `MiniSupermarket.WinForms`.

> **Tài khoản mặc định:** `admin` / `admin123` (vai trò Admin)

---

## 7. Danh sách API Endpoints

### 🔐 Xác thực
| Method | Endpoint | Mô tả |
|---|---|---|
| POST | /api/auth/login | Đăng nhập, trả về JWT Token |

### 📦 Sản phẩm
| Method | Endpoint | Mô tả |
|---|---|---|
| GET | /api/products | Lấy danh sách sản phẩm (EF Core) |
| GET | /api/products/{id} | Lấy sản phẩm theo ID |
| POST | /api/products | Thêm sản phẩm mới |
| PUT | /api/products/{id} | Cập nhật thông tin sản phẩm |
| DELETE | /api/products/{id} | Xoá sản phẩm |
| GET | /api/ado-products/barcode/{barcode} | ⚡ Tra cứu nhanh bằng mã vạch (ADO.NET) |

### 🛒 Bán hàng & Đơn hàng
| Method | Endpoint | Mô tả |
|---|---|---|
| POST | /api/checkout | Thanh toán POS (ADO.NET + SqlTransaction ACID) |
| DELETE | /api/checkout/{orderId} | Hủy đơn + hoàn trả kho (SqlTransaction) |
| GET | /api/orders | Danh sách tất cả đơn hàng kèm chi tiết |
| GET | /api/orders/{id} | Chi tiết một đơn hàng |
| PUT | /api/orders/{id} | Cập nhật trạng thái đơn hàng |
| DELETE | /api/orders/{id} | Hủy đơn hàng (EF Core + Transaction) |

### 👥 Khách hàng
| Method | Endpoint | Mô tả |
|---|---|---|
| GET | /api/customers | Danh sách khách hàng thân thiết |
| POST | /api/customers | Thêm khách hàng mới |
| PUT | /api/customers/{id} | Cập nhật thông tin khách hàng |

### 📊 Báo cáo
| Method | Endpoint | Mô tả |
|---|---|---|
| GET | /api/reports/revenue | Báo cáo doanh thu theo thu ngân (Stored Procedure) |

---

## 8. Các chức năng chính

### 🖥️ Quầy bán hàng POS
- Quét mã vạch → thêm sản phẩm vào giỏ hàng tự động
- Hỗ trợ phím tắt: **F2** (focus ô barcode), **F9** (thanh toán), **+/-** (tăng/giảm số lượng), **Delete** (xoá dòng)
- Nhận diện khách hàng thân thiết qua số điện thoại
- Đổi điểm thưởng để giảm tiền hóa đơn (1 điểm = 1.000 VNĐ)
- Thanh toán bảo vệ bởi **SqlTransaction ACID**: tạo Order + OrderDetails + trừ kho + tích điểm trong 1 giao dịch

### 📋 Quản lý đơn hàng ✅ *(Mới – Buổi 5)*
- Xem danh sách toàn bộ đơn hàng: mã, ngày lập, thu ngân, tổng tiền, trạng thái
- Click dòng → xem chi tiết từng sản phẩm trong đơn (mã SP, số lượng, đơn giá, thành tiền)
- Hủy đơn hàng → tự động khôi phục tồn kho và điểm thưởng khách hàng
- Trạng thái đơn: **Pending** (Chờ xử lý) / **Completed** (Hoàn thành) / **Cancelled** (Đã hủy)

### 👤 Quản lý khách hàng
- Thêm/sửa/xoá khách hàng thân thiết
- Tích điểm tự động: 10.000 VNĐ = 1 điểm thưởng
- Hạng thành viên tự động: Đồng → Bạc → Vàng → Kim cương

### 📊 Báo cáo doanh thu
- Báo cáo doanh thu phân theo từng thu ngân thông qua **Stored Procedure** `sp_GetRevenueByCashier`

---

## 9. Phân quyền người dùng

| Chức năng | Admin | Cashier | Warehouse |
|---|:---:|:---:|:---:|
| 🖥️ Bán hàng (POS) | ✅ | ✅ | ❌ |
| 📂 Quản lý Danh mục | ✅ | ✅ | ✅ |
| 📦 Quản lý Sản phẩm | ✅ | ✅ | ✅ |
| 🛒 Quản lý Đơn hàng | ✅ | ✅ | ❌ |
| 👥 Quản lý Khách hàng | ✅ | ❌ | ❌ |
| 📊 Báo cáo Doanh thu | ✅ | ❌ | ❌ |
| 🔑 Quản trị Tài khoản | ✅ | ❌ | ❌ |

> Phân quyền được kiểm tra **2 lớp**: ẩn/hiện nút sidebar (lớp 1) và chặn logic khi click trước khi mở form (lớp 2).

---

## 10. Nội dung Buổi 5 – ADO.NET Nâng cao & Giao dịch POS

### 🔹 Tiết 1 – Kiến trúc ADO.NET cơ bản

**5 đối tượng cốt lõi của ADO.NET:**

| Đối tượng | Hình tượng thực tế | Vai trò kỹ thuật |
|---|---|---|
| SqlConnection | Đường dây điện thoại tới siêu thị | Mở/đóng kết nối vật lý tới SQL Server |
| SqlCommand | Tờ đơn đặt hàng | Chứa câu lệnh SQL hoặc tên Stored Procedure |
| SqlParameter | Chi tiết ghi rõ ràng, an toàn | Truyền tham số – **chống SQL Injection** |
| SqlDataReader | Băng chuyền quét mã tính tiền | Đọc dữ liệu forward-only, tiết kiệm RAM |
| DataTable / SqlDataAdapter | Thùng hàng đóng gói gửi về nhà | Đọc ngắt kết nối, lưu toàn bộ vào bộ nhớ |

**3 phương thức thực thi SqlCommand:**

| Phương thức | Dùng khi nào | Trả về |
|---|---|---|
| ExecuteScalar() | Truy vấn trả về 1 giá trị (tổng tiền, ID mới...) | object |
| ExecuteNonQuery() | INSERT, UPDATE, DELETE | Số dòng bị ảnh hưởng |
| ExecuteReader() | SELECT nhiều dòng dữ liệu | SqlDataReader |

**Quy tắc vàng:** Luôn bọc `SqlConnection` trong khối `using` để đảm bảo kết nối được tự động đóng dù xảy ra ngoại lệ.

---

### 🔹 Tiết 2 – Tra cứu Barcode với ADO.NET thuần

Endpoint `GET /api/ado-products/barcode/{barcode}` sử dụng `SqlDataReader` để tra cứu sản phẩm theo mã vạch với tốc độ dưới 100ms. Điểm mấu chốt là dùng `SqlParameter` thay vì nối chuỗi SQL trực tiếp để **ngăn chặn SQL Injection**.

---

### 🔹 Tiết 3 – Stored Procedure báo cáo doanh thu

Stored Procedure `sp_GetRevenueByCashier` được tạo trực tiếp trên SQL Server Management Studio. Procedure tổng hợp tổng số đơn hàng và doanh thu theo từng thu ngân, loại trừ các đơn đã hủy. Từ phía C#, gọi procedure bằng cách đặt `CommandType = StoredProcedure` thay vì dùng câu SQL thông thường.

---

### 🔹 Tiết 4 – Chuẩn ACID & SqlTransaction

| Tiêu chí | Ý nghĩa |
|---|---|
| **A**tomicity – Nguyên tử | Tất cả bước thành công hoặc không có bước nào được lưu |
| **C**onsistency – Nhất quán | Cơ sở dữ liệu luôn hợp lệ trước và sau giao dịch |
| **I**solation – Cô lập | Các giao dịch đồng thời không can thiệp lẫn nhau |
| **D**urability – Bền vững | Sau Commit, dữ liệu được lưu vĩnh viễn dù mất điện |

Quy trình: `BeginTransaction()` → thực hiện các thao tác → `Commit()` nếu thành công hoặc `Rollback()` nếu gặp lỗi.

---

### 🔹 Tiết 5 – API Checkout POS & Hủy đơn hàng

**Luồng Thanh toán** (`POST /api/checkout`):

1. Kiểm tra tồn kho từng sản phẩm trong giỏ hàng
2. Bắt đầu `BeginTransaction()`
3. INSERT vào bảng **Orders** – tạo hóa đơn tổng
4. INSERT từng dòng vào bảng **OrderDetails**
5. UPDATE `StockQuantity` trong **Products** – trừ kho
6. UPDATE `RewardPoints` trong **Customers** – tích điểm thưởng
7. `Commit()` → thành công / `Rollback()` → không để lại dữ liệu rác

**Luồng Hủy đơn hàng** (`DELETE /api/checkout/{orderId}`):

1. Lấy thông tin đơn hàng và điểm thưởng đã tích
2. Lấy danh sách sản phẩm trong **OrderDetails**
3. Bắt đầu `BeginTransaction()`
4. UPDATE `StockQuantity` trong **Products** – hoàn trả kho
5. UPDATE `RewardPoints` trong **Customers** – trừ điểm đã tích
6. DELETE **OrderDetails** – xóa chi tiết trước (tránh vi phạm khóa ngoại)
7. DELETE **Orders** – xóa hóa đơn tổng
8. `Commit()` → hủy thành công / `Rollback()` → an toàn, dữ liệu nguyên vẹn

---

## 11. Checklist tự đánh giá

| STT | Hạng mục kiểm tra | Yêu cầu đạt chuẩn | Kết quả |
|---|---|---|---|
| 1 | **ADO.NET cơ bản** (Tiết 1) | Dùng khối using, SqlParameter chống SQL Injection, hiểu ExecuteScalar / NonQuery / Reader | ✅ Đạt |
| 2 | **Tra cứu Barcode** (Tiết 2) | GET /api/ado-products/barcode/{barcode} trả kết quả dưới 100ms bằng SqlDataReader | ✅ Đạt |
| 3 | **Stored Procedure** (Tiết 3) | Tạo thành công sp_GetRevenueByCashier trên SSMS, gọi từ API Reports | ✅ Đạt |
| 4 | **Chuẩn ACID** (Tiết 4) | Trình bày được 4 tiêu chí ACID, dùng BeginTransaction, Commit, Rollback | ✅ Đạt |
| 5 | **API Checkout** (Tiết 5) | POST /api/checkout thực hiện đồng thời: tạo Order + OrderDetails + trừ kho + tích điểm | ✅ Đạt |
| 6 | **Kiểm chứng Rollback** (Tiết 5) | Đặt hàng vượt tồn kho → API lỗi → Rollback, không để lại dữ liệu rác trong CSDL | ✅ Đạt |
| 7 | **Bài tập mở rộng** (Tiết 5) | Hoàn thành API Hủy đơn + hoàn trả tồn kho tự động bằng SqlTransaction | ✅ Đạt |
| 8 | **Quản lý đơn hàng UI** (Bổ sung) | Form WinForms xem danh sách, chi tiết từng dòng và hủy đơn hàng | ✅ Đạt |

---

## 📌 Ghi chú kỹ thuật

- Toàn bộ mật khẩu được mã hóa bằng **BCrypt** trước khi lưu vào cơ sở dữ liệu.
- **JWT Token** hết hạn sau 8 giờ; WinForms tự động gắn vào header mỗi request.
- Mọi thao tác ghi quan trọng (checkout, hủy đơn) được bảo vệ bởi **SqlTransaction** đảm bảo chuẩn ACID.
- Phân quyền kiểm tra **2 lớp**: ẩn/hiện nút UI (lớp 1) và chặn logic khi click (lớp 2).
- Nút **Quản lý đơn hàng** hiển thị cho vai trò **Admin** và **Cashier** trên sidebar.

---

*© 2026 – Nguyễn Tuấn Anh – Bài tập thực hành Lập trình Ứng dụng .NET Core – Buổi 5*
