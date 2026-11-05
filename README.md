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

MiniSupermarket Family Fresh là ứng dụng quản lý siêu thị mini được xây dựng theo kiến trúc 3 lớp (3-Tier Architecture):

- **Tầng Giao diện (Presentation):** Windows Forms – cung cấp giao diện bán hàng POS và quản lý nội bộ.
- **Tầng Xử lý (Business Logic):** ASP.NET Core Web API – xử lý logic nghiệp vụ, bảo mật JWT Token, kiểm soát phân quyền theo vai trò.
- **Tầng Dữ liệu (Data Access):** Entity Framework Core kết hợp ADO.NET thuần – giao tiếp và thao tác trực tiếp với SQL Server.

---

## 2. Kiến trúc hệ thống

Ứng dụng hoạt động theo mô hình Client – Server thông qua giao thức HTTP có gắn JWT Bearer Token:

- Giao diện Windows Forms gửi yêu cầu đến ASP.NET Core Web API.
- Web API tiếp nhận, xác thực vai trò và xử lý dữ liệu với cơ sở dữ liệu SQL Server thông qua Entity Framework Core hoặc các đối tượng ADO.NET thuần.

---

## 3. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Backend API | ASP.NET Core 8 Web API |
| Giao diện | Windows Forms (.NET 8) |
| ORM | Entity Framework Core 8 |
| ADO.NET thuần | SqlConnection, SqlCommand, SqlDataReader, SqlTransaction |
| Bảo mật | JWT Bearer Token, thuật toán mã hóa mật khẩu BCrypt |
| Cơ sở dữ liệu | Microsoft SQL Server |
| Kiểm tra API | Swagger / OpenAPI |

---

## 4. Cấu trúc thư mục

**Dự án MiniSupermarket.API (Backend Web API):**
- Thư mục Controllers: Chứa các bộ điều khiển xử lý nghiệp vụ như AuthController (đăng nhập, cấp JWT), ProductsController (quản lý sản phẩm qua EF Core), AdoProductController (tra cứu mã vạch qua ADO.NET), CategoriesController (quản lý nhóm hàng), CustomersController (khách hàng và tích điểm), UsersController (quản trị tài khoản), CheckoutController (thanh toán POS bọc trong SqlTransaction), OrdersController (quản lý danh sách đơn hàng), ReportsController (báo cáo doanh thu qua Stored Procedure).
- Thư mục Models: Chứa các lớp thực thể Order, OrderDetail, Product, Category, Customer, User, OrderStatus.
- Thư mục DTOs: Chứa các lớp truyền tải dữ liệu giữa client và máy chủ.
- Thư mục Data: Chứa lớp SupermarketDbContext quản lý kết nối EF Core.
- Thư mục Migrations: Chứa toàn bộ lịch sử khởi tạo và cập nhật cấu trúc cơ sở dữ liệu.

**Dự án MiniSupermarket.WinForms (Frontend Windows Forms):**
- FormLogin: Màn hình đăng nhập xác thực và nhận JWT Token.
- FormMainShell: Khung làm việc chính chứa thanh điều hướng sidebar và kiểm soát phân quyền hiển thị.
- FormPOS: Quầy bán hàng và tính tiền thu ngân.
- FormOrderManagement: Màn hình quản lý đơn hàng, xem chi tiết và hủy đơn.
- FormProductManagement: Màn hình quản lý danh sách sản phẩm và tồn kho.
- FormCategoryManagement: Màn hình quản lý danh mục nhóm hàng.
- FormCustomerManagement: Màn hình quản lý thông tin khách hàng và điểm tích lũy.
- FormUserManagement: Màn hình quản trị người dùng và phân quyền hệ thống.
- FormQuickReport: Màn hình xem nhanh báo cáo doanh thu.
- SessionManager: Lưu trữ thông tin phiên làm việc và mã JWT Token.
- Thư mục Services: Chứa ApiClientService cấu hình HttpClient tự động gắn Bearer Token.

---

## 5. Cơ sở dữ liệu & Migration

### Các bảng chính

| Tên bảng | Ý nghĩa nghiệp vụ |
|---|---|
| Products | Quản lý thông tin sản phẩm, giá niêm yết, số lượng tồn kho, mã vạch |
| Categories | Quản lý danh mục nhóm hàng hóa |
| Customers | Quản lý khách hàng thân thiết, số điểm thưởng tích lũy, hạng thành viên |
| Users | Quản lý tài khoản đăng nhập, mật khẩu mã hóa và vai trò hệ thống |
| Orders | Quản lý thông tin hóa đơn bán hàng, thu ngân phụ trách, tổng tiền, trạng thái đơn |
| OrderDetails | Quản lý chi tiết từng món hàng trong đơn, số lượng mua, đơn giá tại thời điểm bán |

### Các đợt Migration tiêu biểu

- InitialCreateDatabase: Khởi tạo các bảng sản phẩm, danh mục và người dùng.
- AddCustomersTable: Bổ sung bảng khách hàng và trường điểm thưởng.
- AddOrdersTables: Bổ sung bảng hóa đơn và chi tiết hóa đơn.
- UpdateCreateDatabase: Hoàn thiện ràng buộc quan hệ giữa các thực thể.
- FamilyFresh: Đồng bộ bộ danh mục thương hiệu Family Fresh.

---

## 6. Hướng dẫn cài đặt & chạy

### Yêu cầu môi trường
- Bộ công cụ .NET SDK phiên bản 8.0 trở lên.
- Hệ quản trị cơ sở dữ liệu Microsoft SQL Server (LocalDB hoặc SQL Express).
- Môi trường phát triển Visual Studio 2022 hoặc Visual Studio Code.

### Các bước thực hiện
- Bước 1: Mở tệp cấu hình appsettings.json trong dự án API để cập nhật chuỗi kết nối DefaultConnection trỏ tới máy chủ SQL Server thực tế.
- Bước 2: Cập nhật cơ sở dữ liệu bằng công cụ Entity Framework Core CLI.
- Bước 3: Mở SQL Server Management Studio để tạo thủ tục lưu trữ sp_GetRevenueByCashier phục vụ chức năng báo cáo doanh thu theo thu ngân.
- Bước 4: Khởi chạy dự án MiniSupermarket.API để kích hoạt Web API và giao diện Swagger UI.
- Bước 5: Khởi chạy dự án MiniSupermarket.WinForms và đăng nhập bằng tài khoản quản trị viên.

---

## 7. Danh sách API Endpoints

### Xác thực hệ thống
- POST /api/auth/login: Đăng nhập hệ thống và trả về JWT Token.

### Quản lý sản phẩm
- GET /api/products: Lấy toàn bộ danh sách sản phẩm qua Entity Framework Core.
- GET /api/products/{id}: Lấy thông tin chi tiết một sản phẩm theo mã định danh.
- POST /api/products: Thêm mới một sản phẩm vào cơ sở dữ liệu.
- PUT /api/products/{id}: Cập nhật thông tin sản phẩm hiện có.
- DELETE /api/products/{id}: Xóa sản phẩm khỏi hệ thống.
- GET /api/ado-products/barcode/{barcode}: Tra cứu nhanh thông tin sản phẩm theo mã vạch bằng ADO.NET thuần.

### Nghiệp vụ bán hàng và đơn hàng
- POST /api/checkout: Thực hiện thanh toán đơn hàng POS, bọc kín toàn bộ thao tác trong SqlTransaction đảm bảo chuẩn ACID.
- DELETE /api/checkout/{orderId}: Hủy đơn hàng và tự động hoàn trả số lượng tồn kho qua SqlTransaction.
- GET /api/orders: Lấy danh sách tất cả các đơn hàng kèm chi tiết mặt hàng.
- GET /api/orders/{id}: Lấy thông tin chi tiết của một đơn hàng cụ thể.
- PUT /api/orders/{id}: Cập nhật trạng thái xử lý của đơn hàng.
- DELETE /api/orders/{id}: Hủy đơn hàng và cập nhật trạng thái hủy.

### Quản lý khách hàng
- GET /api/customers: Lấy danh sách khách hàng thân thiết.
- POST /api/customers: Thêm mới hồ sơ khách hàng.
- PUT /api/customers/{id}: Cập nhật thông tin khách hàng.

### Báo cáo thống kê
- GET /api/reports/revenue: Kết xuất báo cáo doanh thu ca trực theo thu ngân thông qua Stored Procedure.

---

## 8. Các chức năng chính

### Quầy bán hàng POS
- Tự động nhận diện và đưa sản phẩm vào giỏ hàng ngay khi quét mã vạch.
- Trang bị hệ thống phím tắt thao tác nhanh: F2 để trỏ vào ô nhập mã vạch, F9 để thanh toán, phím cộng hoặc trừ để tăng giảm số lượng, phím Delete để xóa món hàng khỏi giỏ.
- Hỗ trợ tra cứu hội viên qua số điện thoại để hưởng ưu đãi.
- Áp dụng cơ chế đổi điểm thưởng trừ trực tiếp vào tổng tiền thanh toán (tỷ lệ 1 điểm tương đương 1.000 VNĐ).
- Thực thi quy trình thanh toán toàn vẹn qua SqlTransaction: sinh hóa đơn, lưu chi tiết món hàng, trừ kho sản phẩm và cộng điểm tích lũy trong cùng một giao dịch.

### Quản lý đơn hàng
- Hiển thị danh sách tổng thể các hóa đơn gồm mã đơn, thời gian lập, thu ngân đứng ca, tổng tiền và trạng thái đơn hàng.
- Xem danh sách chi tiết các mặt hàng đã mua tương ứng với từng hóa đơn được chọn.
- Cho phép hủy đơn hàng hợp lệ kèm cơ chế tự động bù trả lại số lượng tồn kho và điều chỉnh điểm thưởng khách hàng.
- Theo dõi các trạng thái đơn hàng: Chờ xử lý, Hoàn thành, Đã hủy.

### Quản lý khách hàng
- Thêm mới, chỉnh sửa thông tin khách hàng thành viên.
- Tự động tích lũy điểm thưởng theo giá trị hóa đơn (cứ mỗi 10.000 VNĐ thanh toán được tích 1 điểm).
- Tự động nâng hạng thành viên dựa trên điểm tích lũy (Đồng, Bạc, Vàng, Kim cương).

### Báo cáo doanh thu
- Thống kê tổng số đơn hàng đã hoàn tất và tổng doanh thu thu về theo từng nhân viên thu ngân qua Stored Procedure sp_GetRevenueByCashier.

---

## 9. Phân quyền người dùng

Hệ thống phân quyền được kiểm soát chặt chẽ qua 2 lớp (lớp 1 ẩn hiện nút trên thanh điều hướng và lớp 2 kiểm tra vai trò trước khi khởi tạo form):

- **Vai trò Admin (Quản trị viên):** Được cấp toàn quyền truy cập tất cả các phân hệ gồm Bán hàng POS, Quản lý Danh mục, Quản lý Sản phẩm, Quản lý Khách hàng, Quản lý Đơn hàng, Báo cáo Doanh thu và Quản trị Tài khoản.
- **Vai trò Cashier (Thu ngân):** Được cấp quyền truy cập các màn hình phục vụ trực tiếp ca bán lẻ gồm Bán hàng POS, Quản lý Danh mục, Quản lý Sản phẩm và Quản lý Đơn hàng (Hóa đơn).
- **Vai trò Warehouse (Thủ kho):** Được cấp quyền thao tác trên hai phân hệ Quản lý Danh mục và Quản lý Sản phẩm để phục vụ công tác kiểm kê và xuất nhập hàng.

---

## 10. Nội dung Buổi 5 – ADO.NET Nâng cao & Giao dịch POS

### Tiết 1 – Kiến trúc ADO.NET cơ bản

Năm thành phần cốt lõi của thư viện ADO.NET:
- SqlConnection: Đóng vai trò như đường dây liên lạc vật lý kết nối trực tiếp từ mã nguồn .NET đến máy chủ SQL Server.
- SqlCommand: Đóng vai trò như tờ yêu cầu cụ thể, chứa câu lệnh truy vấn SQL hoặc tên Stored Procedure cần thực thi.
- SqlParameter: Đóng vai trò truyền tải tham số dữ liệu một cách an toàn, cô lập giá trị nhập vào để triệt tiêu hoàn toàn nguy cơ tấn công phá hoại qua lỗ hổng SQL Injection.
- SqlDataReader: Đóng vai trò luồng đọc dữ liệu một chiều tiến về phía trước với tốc độ xử lý tức thì và tiêu tốn tối thiểu bộ nhớ RAM.
- DataTable và SqlDataAdapter: Đóng vai trò mô hình làm việc ngắt kết nối, cho phép nạp trọn vẹn dữ liệu bảng vào bộ nhớ RAM để xử lý độc lập mà không cần duy trì kết nối mạng liên tục.

Ba phương thức thực thi của đối tượng SqlCommand:
- ExecuteScalar: Dùng khi câu lệnh truy vấn chỉ trả về đúng một ô kết quả duy nhất như đếm số lượng bản ghi hoặc tính tổng tiền.
- ExecuteNonQuery: Dùng khi thực hiện các thao tác ghi dữ liệu như thêm mới, sửa đổi hoặc xóa bỏ và nhận về số dòng bị tác động.
- ExecuteReader: Dùng khi cần đọc duyệt danh sách dữ liệu gồm nhiều dòng và nhiều cột.

Nguyên tắc bắt buộc: Luôn sử dụng khối lệnh using cho đối tượng kết nối SqlConnection để đảm bảo tài nguyên kết nối luôn được đóng và giải phóng tự động khi hoàn thành tác vụ.

### Tiết 2 – Tra cứu Barcode với ADO.NET thuần

Chức năng tra cứu sản phẩm theo mã vạch tại quầy POS đòi hỏi thời gian phản hồi tức thì dưới 100 mili-giây. Thay vì sử dụng ORM cồng kềnh, hệ thống sử dụng đối tượng SqlDataReader trong ADO.NET thuần kết hợp tham số hóa an toàn qua SqlParameter để đọc trực tiếp một bản ghi duy nhất, mang lại hiệu năng cao và độ ổn định tối đa cho ca thu ngân.

### Tiết 3 – Stored Procedure báo cáo doanh thu

Để tối ưu hóa tốc độ và giảm thiểu băng thông mạng, câu lệnh tính toán tổng hợp doanh thu theo từng thu ngân được đóng gói sẵn thành thủ tục lưu trữ sp_GetRevenueByCashier trên SQL Server. Khi gọi từ phía ứng dụng C#, đối tượng SqlCommand được thiết lập kiểu thực thi StoredProcedure, giúp tận dụng kế hoạch thực thi biên dịch sẵn của hệ quản trị cơ sở dữ liệu.

### Tiết 4 – Chuẩn ACID & SqlTransaction

Giao dịch cơ sở dữ liệu là chuỗi thao tác liên hoàn đòi hỏi tính toàn vẹn tuyệt đối theo 4 tiêu chí ACID:
- Atomicity (Tính nguyên tử): Toàn bộ chuỗi thao tác được coi là một khối thống nhất, tất cả cùng thành công hoặc không có thao tác nào được lưu lại.
- Consistency (Tính nhất quán): Dữ liệu luôn chuyển dịch từ trạng thái hợp lệ này sang trạng thái hợp lệ khác, không phát sinh dữ liệu mồ côi hay dữ liệu rác.
- Isolation (Tính cô lập): Các giao dịch thực thi đồng thời từ các quầy bán hàng khác nhau không can thiệp và không đọc dữ liệu tạm thời của nhau.
- Durability (Tính bền vững): Khi giao dịch đã được xác nhận lưu thành công, dữ liệu được ghi đĩa vĩnh viễn và không bị thất thoát ngay cả khi xảy ra sự cố mất điện máy chủ.

Quy trình điều khiển giao dịch bằng SqlTransaction bao gồm việc kích hoạt phiên bảo vệ bằng BeginTransaction, xác nhận lưu vĩnh viễn bằng Commit và hoàn tác sạch sẽ toàn bộ trạng thái dữ liệu bằng Rollback khi xuất hiện bất kỳ ngoại lệ nào.

### Tiết 5 – API Checkout POS & Hủy đơn hàng

Quy trình thanh toán hóa đơn bán lẻ khép kín gồm các bước:
1. Kiểm tra số lượng tồn kho thực tế của từng sản phẩm trong giỏ hàng.
2. Khởi tạo phiên giao dịch SqlTransaction bảo vệ ACID.
3. Thêm bản ghi hóa đơn tổng vào bảng Orders và lấy mã định danh tự tăng vừa sinh.
4. Thêm từng dòng chi tiết mặt hàng vào bảng OrderDetails.
5. Cập nhật khấu trừ số lượng tồn kho tương ứng trong bảng Products.
6. Cập nhật cộng điểm thưởng tích lũy cho khách hàng trong bảng Customers.
7. Gọi Commit để xác nhận giao dịch thành công trọn vẹn, hoặc tự động kích hoạt Rollback khi giỏ hàng có sản phẩm vượt quá tồn kho để không lưu lại bất kỳ dữ liệu rác nào.

Quy trình hủy đơn hàng và hoàn trả kho:
1. Kiểm tra sự tồn tại của hóa đơn và xác định số điểm thưởng liên quan.
2. Đọc danh sách các mặt hàng đã mua trong đơn.
3. Khởi tạo phiên giao dịch SqlTransaction.
4. Hoàn trả cộng dồn lại số lượng tồn kho cho các sản phẩm trong bảng Products.
5. Khấu trừ lại số điểm thưởng đã cộng cho khách hàng trong bảng Customers.
6. Xóa các dòng chi tiết trong OrderDetails trước để đảm bảo ràng buộc toàn vẹn.
7. Xóa bản ghi hóa đơn trong Orders.
8. Gọi Commit để hoàn tất việc hủy và khôi phục dữ liệu sạch sẽ vào kho.

---

## 11. Checklist tự đánh giá

| STT | Tiêu chí kiểm tra | Yêu cầu đạt chuẩn | Đánh giá |
|---|---|---|---|
| 1 | Kiến thức ADO.NET cơ bản | Sử dụng khối using, tham số SqlParameter chống SQL Injection, vận dụng đúng 3 phương thức Execute | Đạt |
| 2 | Tra cứu Barcode siêu tốc | Endpoint tra cứu mã vạch hoạt động dưới 100ms bằng luồng SqlDataReader | Đạt |
| 3 | Tối ưu bằng Stored Procedure | Tạo thành công thủ tục sp_GetRevenueByCashier trên CSDL và kết xuất báo cáo doanh thu chính xác | Đạt |
| 4 | Nắm vững chuẩn ACID | Trình bày đúng bản chất 4 tiêu chí và cơ chế điều khiển BeginTransaction, Commit, Rollback | Đạt |
| 5 | Hoàn thiện API Checkout | Thực hiện đồng thời tạo đơn, lưu chi tiết, trừ kho và tích điểm trong cùng một giao dịch an toàn | Đạt |
| 6 | Kiểm chứng cơ chế Rollback | Đặt mua vượt quá số lượng tồn kho, hệ thống ném lỗi và hoàn tác sạch dữ liệu không phát sinh bản ghi rác | Đạt |
| 7 | Hoàn thiện API Hủy đơn hàng | Xây dựng thành công chức năng hủy đơn hàng kèm hoàn trả tồn kho tự động bằng SqlTransaction | Đạt |
| 8 | Tích hợp giao diện Quản lý đơn hàng | Cung cấp giao diện WinForms xem danh sách hóa đơn, xem chi tiết và hủy đơn hàng tiện lợi | Đạt |

---

## 📌 Ghi chú kỹ thuật

- Mật khẩu tài khoản được băm và mã hóa an toàn bằng thuật toán BCrypt.
- Mã định danh JWT Token có hiệu lực trong 8 giờ và được WinForms tự động đính kèm vào phần tiêu đề của mỗi yêu cầu gửi đi.
- Mọi thao tác ghi dữ liệu quan trọng đều được đặt trong sự bảo vệ của SqlTransaction nhằm tuân thủ nghiêm ngặt chuẩn ACID.
- Hệ thống phân quyền hoạt động 2 lớp giúp đảm bảo an toàn truy cập ở cả tầng giao diện và tầng logic.
- Chức năng Quản lý đơn hàng được mở quyền cho cả vai trò Admin và Thu ngân (Cashier).

---

Tác giả: Nguyễn Tuấn Anh
MSSV:2124110146
Lớp: CCQ2411D
