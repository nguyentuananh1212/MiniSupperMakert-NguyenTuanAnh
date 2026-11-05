# 🛒 BÁO CÁO TOÀN DIỆN DỰ ÁN HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI FAMILY FRESH
## HỌC PHẦN: LẬP TRÌNH ỨNG DỤNG VỚI .NET CORE VÀ CƠ SỞ DỮ LIỆU SQL SERVER
### BÁO CÁO CHUYÊN ĐỀ BUỔI 5: ADO.NET NÂNG CAO, TỐI ƯU TRUY VẤN VỚI STORED PROCEDURE VÀ ĐIỀU KHIỂN GIAO DỊCH POS CHECKOUT CHUẨN ACID

---

## 📋 MỤC LỤC CHI TIẾT

1. TỔNG QUAN HỆ THỐNG VÀ BỐI CẢNH NGHIỆP VỤ BÁN LẺ
2. MỤC TIÊU HỌC TẬP VÀ CHUẨN ĐẦU RA CHUYÊN ĐỀ BUỔI 5
3. KIẾN TRÚC TỔNG THỂ 3 LỚP VÀ LUỒNG ĐIỀU KHIỂN DỮ LIỆU
4. CÔNG NGHỆ NỀN TẢNG ÁP DỤNG TRONG TOÀN BỘ GIẢI PHÁP
5. CƠ CẤU TỔ CHỨC THƯ MỤC VÀ TRÁCH NHIỆM TỪNG PHÂN HỆ
6. THIẾT KẾ CƠ SỞ DỮ LIỆU VÀ MÔ HÌNH QUAN HỆ CÁC BẢNG
7. TIẾT 1: BẢN CHẤT KIẾN TRÚC ADO.NET VÀ 5 THÀNH PHẦN CỐT LÕI
8. BẢO MẬT DỮ LIỆU VÀ KỸ THUẬT TRIỆT TIÊU LỖ HỔNG SQL INJECTION
9. SO SÁNH CHUYÊN SÂU 3 PHƯƠNG THỨC THỰC THI CỦA SQLCOMMAND
10. TIẾT 2: API TRA CỨU MÃ VẠCH SIÊU TỐC VÀ HAI MÔ HÌNH TRUY XUẤT DỮ LIỆU
11. TIẾT 3: TỐI ƯU HÓA HIỆU NĂNG BÁO CÁO DOANH THU BẰNG STORED PROCEDURE
12. TIẾT 4: NỀN TẢNG CHUẨN ACID VÀ BẢN CHẤT GIAO DỊCH SQLTRANSACTION
13. TIẾT 5: HIỆN THỰC HÓA API THANH TOÁN POS CHECKOUT LIÊN HOÀN 4 BẢNG
14. NGHIỆP VỤ MỞ RỘNG: QUY TRÌNH HỦY ĐƠN HÀNG VÀ TỰ ĐỘNG HOÀN TRẢ TỒN KHO
15. PHÂN HỆ MỚI: GIAO DIỆN QUẢN LÝ ĐƠN HÀNG TRÊN WINDOWS FORMS
16. MA TRẬN PHÂN QUYỀN VAI TRÒ 2 LỚP TRÊN TOÀN HỆ THỐNG
17. HƯỚNG DẪN CÀI ĐẶT, CẤU HÌNH VÀ VẬN HÀNH TỪ ĐẦU
18. DANH MỤC TẤT CẢ CÁC ĐIỂM CUỐI DỊCH VỤ WEB API VÀ ĐẶC TẢ GIAO THỨC
19. BỘ KỊCH BẢN KIỂM THỬ THỰC TẾ TRÊN SWAGGER VÀ PHÒNG LAB
20. BẢNG PHÂN TÍCH VÀ PHƯƠNG PHÁP KHẮC PHỤC CÁC LỖI KỸ THUẬT THƯỜNG GẶP
21. BẢNG TỰ KIỂM ĐỊNH VÀ BỘ TIÊU CHÍ NGHIỆM THU CHUẨN RUBRIC BUỔI 5
22. ĐỊNH HƯỚNG MỞ RỘNG VÀ NÂNG CẤP HỆ THỐNG
23. THÔNG TIN SINH VIÊN VÀ BẢN QUYỀN ĐỒ ÁN

---

## 1. TỔNG QUAN HỆ THỐNG VÀ BỐI CẢNH NGHIỆP VỤ BÁN LẺ

Trong thời đại số hóa ngành bán lẻ, các chuỗi cửa hàng tiện lợi và siêu thị mini đòi hỏi hệ thống bán hàng tại quầy phải đáp ứng các yêu cầu khắt khe:
- Tốc độ phản hồi tức thì: Tại quầy thu ngân trong giờ cao điểm, nhân viên thu ngân sử dụng súng đọc mã vạch quét liên tục qua các mặt hàng. Thời gian máy quét tra cứu thông tin sản phẩm và hiển thị lên lưới giỏ hàng phải diễn ra trong khoảng thời gian dưới một trăm mili-giây.
- Tính toàn vẹn dữ liệu tuyệt đối: Khi tiến hành bấm nút thanh toán một giỏ hàng gồm nhiều món đồ, hệ thống phải thực thi đồng thời một chuỗi hành động ghi dữ liệu gồm tạo hóa đơn, lưu chi tiết món mua, trừ số lượng tồn kho và cộng điểm thưởng khách hàng. Nếu bất kỳ một bước nào thất bại, dữ liệu phải được hoàn tác sạch sẽ về trạng thái ban đầu, tuyệt đối không để xảy ra tình trạng tiền khách đã trả nhưng kho hàng chưa được trừ.
- Phân quyền minh bạch và bảo mật: Hệ thống phân chia ranh giới quyền hạn rõ ràng giữa các nhóm nhân sự gồm Ban quản lý cửa hàng (Admin), Nhân viên quầy thu ngân (Cashier) và Nhân viên phụ trách xuất nhập kho (Warehouse), bảo đảm dữ liệu tài chính không bị xem lén hay can thiệp trái phép.

Đồ án MiniSupermarket Family Fresh được thiết kế để giải quyết trọn vẹn toàn bộ các bài toán nghiệp vụ trên thông qua sự kết hợp nhịp nhàng giữa nền tảng ứng dụng máy trạm Windows Forms và dịch vụ phụ trợ ASP.NET Core Web API kết nối hệ quản trị cơ sở dữ liệu Microsoft SQL Server.

---

## 2. MỤC TIÊU HỌC TẬP VÀ CHUẨN ĐẦU RA CHUYÊN ĐỀ BUỔI 5

Sau khi hoàn thành nội dung thực hành của Buổi 5, sinh viên làm chủ các năng lực:
- Hiểu rõ bản chất kiến trúc kết nối dữ liệu của thư viện ADO.NET trong ứng dụng .NET 8, phân biệt vai trò của từng thành phần cốt lõi.
- Vận dụng thành thạo luồng đọc nhanh một chiều SqlDataReader để xây dựng dịch vụ tra cứu thông tin hàng hóa theo mã vạch với thời gian phản hồi siêu tốc.
- Phân biệt sâu sắc giữa mô hình kết nối liên tục (Connected Mode) và mô hình ngắt kết nối nạp bảng ảo vào bộ nhớ RAM (Disconnected Mode với DataTable và SqlDataAdapter).
- Làm chủ kỹ thuật tối ưu hóa truy vấn bằng Stored Procedure biên dịch sẵn trên SQL Server, kết hợp gọi thủ tục lưu trữ từ xa để kết xuất báo cáo doanh thu ca trực.
- Nắm vững bốn đặc tính sống còn của chuẩn ACID, điều khiển vòng đời giao dịch dữ liệu qua các phương thức BeginTransaction, Commit và Rollback của đối tượng SqlTransaction.
- Hiện thực hóa thành công nghiệp vụ thanh toán giỏ hàng POS liên hoàn 4 bảng dữ liệu, bảo đảm tính toàn vẹn và triệt tiêu hoàn toàn rác dữ liệu khi có lỗi phát sinh.
- Xây dựng hoàn chỉnh phân hệ Quản lý đơn hàng trên giao diện Windows Forms hỗ trợ hiển thị danh sách hóa đơn, xem chi tiết mặt hàng và hủy đơn hàng hoàn kho tự động.

---

## 3. KIẾN TRÚC TỔNG THỂ 3 LỚP VÀ LUỒNG ĐIỀU KHIỂN DỮ LIỆU

Hệ thống được tổ chức theo kiến trúc ba lớp tiêu chuẩn công nghiệp nhằm tách bạch rõ ràng giữa giao diện hiển thị, xử lý nghiệp vụ và truy xuất dữ liệu:

### 3.1. Tầng Giao diện người dùng (Presentation Layer - Windows Forms)
- Là ứng dụng máy trạm cài đặt trên máy tính quầy bán hàng và máy tính văn phòng của người quản lý.
- Xây dựng theo kiến trúc một cửa sổ vỏ bọc duy nhất (Single-Form Shell) mang tên FormMainShell, bên trái là thanh điều hướng chức năng, bên trên là thanh thông tin phiên đăng nhập và bên phải là vùng chứa để nhúng các màn hình nghiệp vụ con.
- Đảm nhận nhiệm vụ nhận tương tác từ chuột, bàn phím, súng quét mã vạch và hiển thị trực quan các biểu bảng, danh sách hàng hóa và lưới hóa đơn.
- Giao tiếp với tầng trung gian hoàn toàn qua giao thức mạng HTTP chuẩn RESTful bằng HttpClient, tự động đính kèm mã định danh JWT Bearer Token trong tiêu đề của mỗi yêu cầu gửi đi.

### 3.2. Tầng Nghiệp vụ và Dịch vụ (Business Logic & Service Layer - ASP.NET Core Web API)
- Là dịch vụ máy chủ độc lập lắng nghe các cổng kết nối mạng an toàn.
- Đảm nhiệm việc xác thực danh tính người dùng thông qua cơ chế mã hóa mật khẩu và phát hành mã JWT Token có ký số điện tử.
- Kiểm soát phân quyền dựa trên vai trò trước khi cho phép dữ liệu đi vào các bộ điều khiển xử lý.
- Thực hiện kiểm tra tính hợp lệ của giỏ hàng, tính toán tiền thanh toán, áp dụng công thức tích lũy điểm thưởng và chuyển đổi điểm thưởng trừ tiền hóa đơn.
- Điều phối các giao dịch dữ liệu nhiều bảng nhằm đảm bảo đáp ứng chuẩn ACID.

### 3.3. Tầng Truy cập Dữ liệu và Lưu trữ (Data Access Layer & Database Storage)
- Cơ sở dữ liệu quan hệ Microsoft SQL Server lưu trữ bền vững toàn bộ dữ liệu hoạt động của siêu thị.
- Ứng dụng kết hợp linh hoạt hai công nghệ truy xuất dữ liệu:
  - Entity Framework Core Code-First: Sử dụng cho các tác vụ quản trị thông thường đòi hỏi tính linh hoạt cao như thêm sửa xóa nhóm hàng, hồ sơ khách hàng, phân quyền người dùng và quản lý trạng thái hóa đơn.
  - ADO.NET thuần túy kết hợp SqlTransaction: Sử dụng chuyên biệt cho các điểm nóng về hiệu năng và an toàn giao dịch tại quầy thu ngân như quét mã vạch siêu tốc, báo cáo doanh thu tổng hợp qua Stored Procedure và chuỗi hành động thanh toán trừ kho khép kín.

---

## 4. CÔNG NGHỆ NỀN TẢNG ÁP DỤNG TRONG TOÀN BỘ GIẢI PHÁP

- Nền tảng thực thi: Microsoft .NET 8 phiên bản hỗ trợ dài hạn (Long Term Support).
- Ngôn ngữ lập trình: C# phiên bản 12 với cú pháp an toàn kiểu dữ liệu và cấu trúc lập trình bất đồng bộ.
- Framework Web API: ASP.NET Core 8 Web API tích hợp tài liệu hóa tự động bằng Swagger và OpenAPI.
- Giao diện máy trạm: Windows Forms trên .NET 8 hỗ trợ xử lý đồ họa phân giải cao, quản lý sự kiện bàn phím cho súng quét mã vạch.
- Hệ quản trị cơ sở dữ liệu: Microsoft SQL Server 2019 / 2022 và công cụ quản trị SQL Server Management Studio.
- Bộ kết nối dữ liệu: Thư viện kết nối dữ liệu tầng thấp Microsoft.Data.SqlClient cho ADO.NET và Entity Framework Core 8 phiên bản phát triển theo mô hình tiếp cận mã nguồn trước (Code-First).
- Cơ chế bảo mật và xác thực: Mã định danh JSON Web Token kết hợp thuật toán băm mật khẩu một chiều BCrypt.

---

## 5. CƠ CẤU TỔ CHỨC THƯ MỤC VÀ TRÁCH NHIỆM TỪNG PHÂN HỆ

Toàn bộ giải pháp bao gồm hai dự án thành phần được sắp xếp khoa học:

### 5.1. Dự án MiniSupermarket.API (Backend Web API)
- Thư mục Controllers:
  - AuthController: Tiếp nhận tài khoản và mật khẩu, thẩm định tính hợp lệ và sinh mã JWT Token trả về cho máy trạm.
  - ProductsController: Cung cấp các thao tác quản lý sản phẩm thông thường thông qua ngữ cảnh Entity Framework Core.
  - AdoProductController: Cung cấp API tra cứu mã vạch siêu tốc bằng luồng SqlDataReader và API tải danh sách sản phẩm lưu đệm vào bảng ảo DataTable.
  - CategoriesController: Quản lý nhóm hàng hóa, phân loại sản phẩm.
  - CustomersController: Quản lý thông tin khách hàng, số điện thoại, điểm thưởng và thăng hạng thành viên.
  - UsersController: Quản lý nhân viên nội bộ, cấp phát quyền hạn và trạng thái kích hoạt tài khoản.
  - CheckoutController: Chịu trách nhiệm thực thi giao dịch thanh toán quầy POS liên hoàn nhiều bảng và giao dịch hủy đơn hàng hoàn kho bọc kín trong SqlTransaction.
  - OrdersController: Cung cấp các thao tác quản lý danh sách đơn hàng, xem chi tiết mặt hàng và cập nhật trạng thái đơn hàng.
  - ReportsController: Thực thi Stored Procedure tổng hợp doanh thu theo từng nhân viên thu ngân.
- Thư mục Models: Chứa các lớp thực thể ánh xạ cấu trúc bảng như Order, OrderDetail, Product, Category, Customer, User và kiểu liệt kê OrderStatus.
- Thư mục DTOs: Chứa các lớp đối tượng chuyên biệt phục vụ đóng gói dữ liệu trao đổi giữa client và server như LoginRequestDto, CheckoutRequestDto, CartItemCheckoutDto, OrderDto, OrderDetailDto, CashierRevenueDto.
- Thư mục Data: Chứa lớp SupermarketDbContext kế thừa DbContext quản lý cấu hình kết nối và thiết lập quan hệ khóa ngoại giữa các bảng.
- Thư mục Migrations: Chứa toàn bộ các tệp mã nguồn lưu vết thay đổi cấu trúc bảng qua từng giai đoạn phát triển.
- Thư mục Scripts: Chứa các kịch bản T-SQL khởi tạo Stored Procedure trên SQL Server.

### 5.2. Dự án MiniSupermarket.WinForms (Frontend Windows Forms)
- FormLogin: Cửa sổ đăng nhập tiếp nhận tên người dùng, mật mã, gọi API xác thực và lưu JWT Token vào bộ nhớ tạm.
- FormMainShell: Cửa sổ khung điều hướng chính, quản lý thanh menu phân quyền bên trái, thanh thông tin phiên làm việc bên trên và nhúng động các cửa sổ con vào vùng trung tâm.
- FormPOS: Quầy bán hàng và thanh toán trực tiếp, hỗ trợ quét mã vạch, chọn khách hàng thân thiết, áp dụng điểm thưởng và thanh toán hóa đơn.
- FormOrderManagement: Giao diện quản lý toàn diện các hóa đơn đã xuất, hiển thị danh sách đơn ở bảng phía trên, chi tiết từng món đồ ở bảng phía dưới và nút bấm hủy đơn hàng hoàn kho an toàn.
- FormProductManagement: Giao diện quản trị danh mục sản phẩm, cập nhật giá bán niêm yết và số lượng hàng tồn kho.
- FormCategoryManagement: Giao diện quản lý các nhóm ngành hàng trong siêu thị.
- FormCustomerManagement: Giao diện quản lý hội viên, số điện thoại liên lạc và điểm tích lũy mua sắm.
- FormUserManagement: Giao diện quản lý danh sách nhân sự và phân bổ vai trò làm việc trong hệ thống.
- FormQuickReport: Màn hình kết xuất và hiển thị nhanh bảng biểu doanh thu phục vụ người quản lý.
- SessionManager: Lớp tĩnh lưu trữ thông tin phiên làm việc hiện hành gồm tên đăng nhập, họ tên đầy đủ, vai trò và mã JWT Token.
- Thư mục Services: Chứa ApiClientService quản lý đối tượng HttpClient duy nhất dùng chung, tự động thiết lập tiêu đề ủy quyền Authorization Bearer Token.

---

## 6. THIẾT KẾ CƠ SỞ DỮ LIỆU VÀ MÔ HÌNH QUAN HỆ CÁC BẢNG

Cơ sở dữ liệu MiniSupermarketDb được chuẩn hóa với các bảng dữ liệu có quan hệ chặt chẽ:

### 6.1. Bảng Categories (Nhóm hàng hóa)
- CategoryId: Khóa chính số nguyên tự tăng, định danh nhóm hàng.
- CategoryName: Tên nhóm hàng như Đồ uống giải khát, Bánh kẹo ăn vặt, Nhu yếu phẩm gia đình.
- Description: Mô tả chi tiết nhóm hàng.

### 6.2. Bảng Products (Sản phẩm)
- ProductId: Khóa chính số nguyên tự tăng, định danh sản phẩm.
- Barcode: Chuỗi mã vạch duy nhất dùng cho súng quét tại quầy.
- ProductName: Tên đầy đủ của mặt hàng.
- Price: Đơn giá niêm yết bán lẻ cho khách hàng.
- StockQuantity: Số lượng tồn kho thực tế hiện diện tại siêu thị.
- CategoryId: Khóa ngoại liên kết tới bảng Categories.

### 6.3. Bảng Customers (Khách hàng thân thiết)
- CustomerId: Khóa chính số nguyên tự tăng, định danh khách hàng.
- CustomerName: Họ tên khách hàng.
- PhoneNumber: Số điện thoại sử dụng làm mã tra cứu hội viên tại quầy.
- RewardPoints: Tổng số điểm thưởng tích lũy lũy kế đang sở hữu.
- MembershipRank: Hạng hội viên tự động (Đồng, Bạc, Vàng, Kim Cương).

### 6.4. Bảng Users (Tài khoản người dùng)
- UserId: Khóa chính số nguyên tự tăng.
- Username: Tên đăng nhập duy nhất vào hệ thống.
- PasswordHash: Chuỗi băm mật khẩu được tạo bởi thuật toán mã hóa BCrypt.
- FullName: Họ và tên nhân viên phụ trách.
- Role: Vai trò đảm nhiệm trong hệ thống (Admin, Cashier, Warehouse).
- IsActive: Trạng thái cho phép hoặc tạm khóa tài khoản.

### 6.5. Bảng Orders (Hóa đơn bán lẻ)
- OrderId: Khóa chính số nguyên tự tăng, định danh hóa đơn.
- OrderCode: Mã hóa đơn duy nhất phục vụ in ấn phiếu tính tiền.
- OrderDate: Thời điểm giao dịch thanh toán thành công.
- CashierUsername: Tên tài khoản thu ngân chịu trách nhiệm lập đơn hàng.
- CustomerId: Khóa ngoại trỏ đến khách hàng mua (có thể nhận giá trị rỗng nếu là khách mua lẻ vãng lai).
- TotalAmount: Số tiền thực tế khách hàng phải trả sau khi đã cấn trừ chiết khấu đổi điểm.
- Status: Trạng thái hóa đơn theo kiểu liệt kê số nguyên (0 biểu thị Chờ xử lý, 1 biểu thị Hoàn thành thành công, 2 biểu thị Đã hủy).

### 6.6. Bảng OrderDetails (Chi tiết hóa đơn)
- OrderDetailId: Khóa chính số nguyên tự tăng định danh từng dòng sản phẩm.
- OrderId: Khóa ngoại liên kết tới hóa đơn mẹ trong bảng Orders.
- ProductId: Khóa ngoại liên kết tới sản phẩm trong bảng Products.
- Quantity: Số lượng sản phẩm mua trong dòng này.
- UnitPrice: Đơn giá sản phẩm tại thời điểm giao dịch phát sinh, đảm bảo báo cáo tài chính về sau không bị sai lệch khi giá niêm yết sản phẩm bị điều chỉnh.

---

## 7. TIẾT 1: BẢN CHẤT KIẾN TRÚC ADO.NET VÀ 5 THÀNH PHẦN CỐT LÕI

### 7.1. Khái niệm và vị trí của ADO.NET
ADO.NET là bộ thư viện tầng thấp cung cấp cơ chế kết nối và trao đổi dữ liệu có cấu trúc giữa mã nguồn C# và hệ quản trị cơ sở dữ liệu quan hệ. Khác với ORM che giấu phần lớn câu lệnh phía sau, ADO.NET cho phép lập trình viên toàn quyền kiểm soát chuỗi kết nối, thời điểm mở đóng kênh mạng và câu lệnh SQL gửi trực tiếp tới máy chủ.

### 7.2. Hình tượng so sánh đời thường của 5 đối tượng trung tâm
Để sinh viên dễ hình dung cơ chế hoạt động, quy trình này giống như việc đặt hàng từ xa tới Siêu thị Mini:
- SqlConnection tương tự như đường dây điện thoại kết nối tới siêu thị: Trước khi muốn mua hàng hay hỏi giá, bạn bắt buộc phải quay số gọi tới siêu thị. Nếu không mở máy bằng lệnh Open, bạn không thể trao đổi bất kỳ thông tin nào. Khi nói chuyện xong, bạn phải cúp máy bằng lệnh Close để người khác còn gọi vào.
- SqlCommand tương tự như tờ đơn đặt hàng hoặc lời yêu cầu cụ thể: Sau khi máy đã thông, bạn cần đọc yêu cầu của mình như kiểm tra giá hộp sữa, lấy thêm hai gói mì hoặc hủy món đồ này.
- SqlParameter tương tự như chi tiết món hàng được ghi rõ ràng an toàn: Khi đọc tên hàng, bạn đọc chính xác mã vạch cụ thể thay vì nói mập mờ. Điều này giúp nhân viên siêu thị không nhầm lẫn và ngăn kẻ gian chèn lệnh giả mạo phá hoại kho hàng (chống lỗi SQL Injection).
- SqlDataReader tương tự như băng chuyền quét mã tính tiền siêu tốc: Nhân viên siêu thị đưa từng món đồ lướt qua máy quét mã vạch. Hàng chạy tới đâu, máy quét đọc tới đó theo một chiều tiến về phía trước. Cách này cực nhẹ, không chiếm diện tích mặt bàn và rất ít tốn RAM máy tính.
- DataTable và SqlDataAdapter tương tự như thùng hàng đóng hộp mang về nhà: Toàn bộ đồ được nhân viên đóng sẵn vào một thùng các-tông rồi chuyển về tận nhà bạn thông qua nhân viên giao hàng. Lúc này cuộc gọi đã kết thúc, bạn có thể ngồi mở thùng hàng ra xem đi xem lại mà không phụ thuộc vào siêu thị nữa.

### 7.3. Quy tắc an toàn bắt buộc với khối lệnh Using
Đối tượng kết nối mạng vật lý SqlConnection đại diện cho tài nguyên phần cứng quý giá của máy chủ. Luôn luôn bọc đối tượng kết nối trong cấu trúc khối lệnh using. Khi hoàn thành công việc hoặc ngay cả khi xảy ra sự cố ngoại lệ bất ngờ, kết nối luôn được bộ thu gom rác tự động đóng lại và hoàn trả về vùng đệm kết nối (Connection Pool), triệt tiêu hoàn toàn nguy cơ nghẽn mạng cơ sở dữ liệu.

---

## 8. BẢO MẬT DỮ LIỆU VÀ KỸ THUẬT TRIỆT TIÊU LỖ HỔNG SQL INJECTION

### 8.1. Hiểm họa khôn lường từ việc cộng chuỗi SQL
Lỗi bảo mật nghiêm trọng phổ biến nhất của các ứng dụng quản lý là việc ghép trực tiếp chuỗi ký tự do người dùng nhập từ bàn phím vào câu lệnh truy vấn SQL. Nếu kẻ xấu cố tình điền vào ô tìm kiếm hoặc ô mật mã chuỗi ký tự chứa các dấu nháy đơn kèm mệnh đề điều kiện luôn đúng hoặc câu lệnh xóa bảng, câu lệnh thực thi gửi xuống SQL Server sẽ bị bẻ cong cấu trúc logic ban đầu, cho phép kẻ xâm nhập vượt qua hàng rào đăng nhập hoặc phá hủy toàn bộ cơ sở dữ liệu.

### 8.2. Cơ chế bảo vệ triệt để bằng SqlParameter
Để triệt tiêu hoàn toàn nguy cơ trên, lập trình viên bắt buộc phải sử dụng cơ chế tham số hóa. Các tham số được đặt tên bắt đầu bằng ký tự a còng đại diện cho vị trí dữ liệu trong câu truy vấn. Sau đó, giá trị từ người dùng được truyền vào thông qua tập hợp tham số của đối tượng lệnh bằng phương thức gán giá trị an toàn.
Hệ quản trị cơ sở dữ liệu sẽ phân tích cú pháp câu lệnh trước, coi toàn bộ giá trị truyền vào thuần túy là dữ liệu thô, không thể biến thành mã lệnh thực thi, ngăn chặn tuyệt đối một trăm phần trăm mọi nguy cơ tấn công SQL Injection.

---

## 9. SO SÁNH CHUYÊN SÂU 3 PHƯƠNG THỨC THỰC THI CỦA SQLCOMMAND

Tùy thuộc vào bản chất của câu truy vấn gửi xuống cơ sở dữ liệu, đối tượng SqlCommand cung cấp ba phương thức thực thi riêng biệt:

### 9.1. Phương thức ExecuteScalar
- Kiểu dữ liệu trả về: Đối tượng tổng quát object, lập trình viên cần ép kiểu tường minh về số nguyên, số thực hoặc chuỗi tùy trường hợp.
- Bản chất nghiệp vụ: Chỉ lấy về giá trị của đúng một ô duy nhất nằm tại dòng đầu tiên và cột đầu tiên của tập kết quả trả về, toàn bộ các dòng và cột phía sau đều bị bỏ qua.
- Tình huống áp dụng: Sử dụng cho các câu lệnh tính toán tổng hợp như đếm số lượng bản ghi (hàm COUNT), tính tổng doanh thu (hàm SUM), hoặc hứng giá trị mã khóa chính tự động tăng vừa sinh ra qua mệnh đề OUTPUT INSERTED.

### 9.2. Phương thức ExecuteNonQuery
- Kiểu dữ liệu trả về: Số nguyên int biểu thị số lượng dòng dữ liệu thực tế bị tác động và thay đổi trong cơ sở dữ liệu.
- Bản chất nghiệp vụ: Dùng để thực thi các câu lệnh mang tính chất sửa đổi trạng thái bảng dữ liệu mà không cần nhận về bất kỳ bảng danh sách nào.
- Tình huống áp dụng: Sử dụng cho các câu lệnh thêm mới dòng dữ liệu (INSERT), cập nhật điều chỉnh thông tin (UPDATE) và xóa bỏ bản ghi (DELETE).

### 9.3. Phương thức ExecuteReader
- Kiểu dữ liệu trả về: Một đối tượng luồng đọc dữ liệu một chiều SqlDataReader.
- Bản chất nghiệp vụ: Mở một đường ống đọc dữ liệu trực tiếp từ máy chủ theo từng bản ghi tuần tự.
- Tình huống áp dụng: Sử dụng cho các câu lệnh truy vấn dữ liệu dạng bảng danh sách gồm nhiều hàng và nhiều cột như duyệt danh sách sản phẩm, xuất dữ liệu khách hàng hoặc đọc báo cáo.

---

## 10. TIẾT 2: API TRA CỨU MÃ VẠCH SIÊU TỐC VÀ HAI MÔ HÌNH TRUY XUẤT DỮ LIỆU

### 10.1. Tại sao thao tác quét mã vạch POS đòi hỏi ADO.NET thuần?
Khi nhân viên thu ngân quét liên tục súng đọc mã vạch qua các món hàng tại quầy bán lẻ, nếu ứng dụng phải khởi tạo ngữ cảnh ORM cồng kềnh, phân tích cây quan hệ phức tạp và theo dõi lịch sử thay đổi của từng thực thể, màn hình quầy thu ngân sẽ bị trễ và đơ giật. Bằng việc xây dựng API tra cứu bằng ADO.NET thuần kết hợp SqlDataReader, hệ thống chỉ mở luồng đọc một dòng dữ liệu duy nhất và đóng ngay lập tức, đưa thời gian phản hồi xuống mức tức thì dưới một trăm mili-giây.

### 10.2. So sánh đối chiếu hai mô hình Connected Mode và Disconnected Mode

| Tiêu chí so sánh | Mô hình Kết nối trực tiếp (Connected Mode) | Mô hình Ngắt kết nối (Disconnected Mode) |
|---|---|---|
| Đối tượng đại diện | SqlDataReader | DataTable, DataSet |
| Thành phần trung gian | SqlCommand thực thi ExecuteReader | SqlDataAdapter điều phối hàm Fill |
| Trạng thái kênh mạng | Kênh SqlConnection bắt buộc mở liên tục suốt quá trình đọc dữ liệu | Kênh SqlConnection mở tạm để nạp dữ liệu rồi tự động đóng ngay lập tức |
| Mức độ chiếm dụng RAM | Cực kỳ ít vì chỉ đọc tuần tự từng dòng lướt qua bộ đệm | Nhiều hơn vì lưu giữ toàn bộ dữ liệu bảng vào bộ nhớ RAM máy tính |
| Tốc độ đọc | Đạt tốc độ tối đa nhờ cơ chế duyệt một chiều tiến về phía trước | Đạt tốc độ trung bình do phải cấu trúc hóa dữ liệu vào các ô bảng ảo |
| Khả năng chỉnh sửa | Chỉ đọc, tuyệt đối không cho phép chỉnh sửa dữ liệu trên luồng | Cho phép thêm, sửa, xóa các dòng dữ liệu ngoại tuyến trên RAM |
| Tình huống ứng dụng | Quét mã vạch quầy bán hàng, kết xuất báo cáo hàng chục ngàn dòng | Đổ dữ liệu lên lưới hiển thị giao diện người dùng, làm việc khi mất mạng |

---

## 11. TIẾT 3: TỐI ƯU HÓA HIỆU NĂNG BÁO CÁO DOANH THU BẰNG STORED PROCEDURE

### 11.1. Bản chất và ưu điểm vượt trội của Stored Procedure trong doanh nghiệp
Stored Procedure là một tập hợp các câu lệnh T-SQL phức tạp đã được biên dịch sẵn và lưu trữ trực tiếp bên trong cơ sở dữ liệu SQL Server. So với việc ứng dụng C# gửi một chuỗi câu lệnh SQL thô qua đường truyền mạng, việc gọi Stored Procedure đem lại các lợi thế chiến lược:
- Tối ưu hóa kế hoạch thực thi: SQL Server biên dịch sẵn mã lệnh và lưu kế hoạch thực thi vào bộ đệm, bỏ qua các bước phân tích cú pháp ở các lần gọi tiếp theo, giúp báo cáo chạy nhanh hơn rõ rệt.
- Tiết kiệm băng thông đường truyền: Ứng dụng chỉ cần gửi duy nhất tên của thủ tục thay vì gửi một đoạn mã truy vấn dài nhiều chục dòng.
- Phân tách trách nhiệm chuyên môn: Bộ phận quản trị cơ sở dữ liệu có thể chủ động tối ưu công thức tính toán doanh thu mà không cần phải biên dịch lại hay bàn giao lại toàn bộ mã nguồn của phần mềm máy trạm.
- Tăng cường bảo mật: Cho phép phân quyền thực thi thủ tục cho nhân viên mà không cần cấp quyền truy cập trực tiếp vào các bảng dữ liệu nhạy cảm như Orders.

### 11.2. Cấu hình gọi Stored Procedure trên C#
Để kích hoạt thủ tục lưu trữ sp_GetRevenueByCashier từ mã nguồn C#, điểm mấu chốt là phải thiết lập thuộc tính CommandType của đối tượng lệnh thành kiểu StoredProcedure. Nếu bỏ quên thiết lập này, SQL Server sẽ hiểu nhầm tên thủ tục là một đoạn truy vấn SQL thông thường và ném ra lỗi cú pháp.

---

## 12. TIẾT 4: NỀN TẢNG CHUẨN ACID VÀ BẢN CHẤT GIAO DỊCH SQLTRANSACTION

### 12.1. Thách thức lớn khi thanh toán không có Transaction bảo vệ
Hãy hình dung kịch bản thanh toán tại quầy:
Bước một, hệ thống tạo bản ghi hóa đơn mới. Bước hai, lưu các chi tiết món hàng. Bước ba, thực hiện trừ số lượng tồn kho của các sản phẩm đã mua.
Nếu không có cơ chế giao dịch, giả sử bước một và bước hai đã hoàn tất nhưng đến bước ba hệ thống bị mất điện đột ngột hoặc sản phẩm thứ hai bị phát hiện đã hết sạch hàng trong kho thực tế:
Hóa đơn vẫn tồn tại, tiền đã tính cho khách nhưng tồn kho không được cập nhật, dẫn tới số liệu kế toán và kho hàng bị sai lệch nghiêm trọng, làm phát sinh dữ liệu rác không thể kiểm toán.

### 12.2. Phân tích bốn trụ cột của chuẩn ACID
Chuẩn ACID là thước đo bắt buộc đối với mọi hệ thống thanh toán thương mại:
- A - Atomicity (Tính nguyên tử - Tất cả hoặc không có gì): Toàn bộ chuỗi thao tác thanh toán được xem như một hạt nguyên tử không thể chia cắt. Nếu có bất kỳ một thao tác nào bị lỗi, tất cả các thao tác trước đó đều bị hoàn tác triệt để.
- C - Consistency (Tính nhất quán): Dữ liệu luôn chuyển dịch từ trạng thái hợp lệ này sang trạng thái hợp lệ khác, không bao giờ xuất hiện tình trạng một hóa đơn tồn tại mà không có dòng chi tiết hoặc tồn kho bị âm vô lý.
- I - Isolation (Tính cô lập): Các giao dịch thanh toán diễn ra song song tại nhiều quầy thu ngân khác nhau hoạt động hoàn toàn độc lập, quầy này không nhìn thấy dữ liệu tạm thời chưa chốt của quầy kia.
- D - Durability (Tính bền vững): Khi giao dịch đã được xác nhận thành công, dữ liệu được ghi đĩa vĩnh viễn, ngay cả khi máy chủ gặp sự cố sập nguồn ngay sau đó thì dữ liệu giao dịch vẫn được bảo toàn nguyên vẹn.

### 12.3. Vòng đời ba giai đoạn của đối tượng SqlTransaction
Quá trình điều khiển giao dịch bằng SqlTransaction vận hành qua ba phương thức cốt lõi:
- Khởi tạo giao dịch bằng BeginTransaction: Được kích hoạt từ kết nối SqlConnection đã mở, phát tín hiệu cho SQL Server đưa các thao tác tiếp theo vào bộ đệm giao dịch tạm thời.
- Xác nhận ghi đĩa bằng Commit: Chỉ được gọi khi toàn bộ chuỗi câu lệnh trong khối xử lý chạy suôn sẻ không phát sinh ngoại lệ nào, lúc này dữ liệu mới chính thức được ghi vĩnh viễn vào các bảng.
- Hoàn tác an toàn bằng Rollback: Đặt bên trong khối bắt lỗi ngoại lệ catch, lập tức xóa bỏ toàn bộ mọi thao tác ghi tạm thời trước đó, đưa cơ sở dữ liệu trở về trạng thái sạch sẽ như trước khi bắt đầu.

---

## 13. TIẾT 5: HIỆN THỰC HÓA API THANH TOÁN POS CHECKOUT LIÊN HOÀN 4 BẢNG

API xử lý thanh toán quầy thu ngân là hạt nhân quan trọng nhất của hệ thống, điều phối bốn bước xử lý liên hoàn được bọc kín trong một giao dịch SqlTransaction duy nhất:

### Bước 1: Kiểm tra tính hợp lệ và thẩm định tồn kho thực tế
Trước khi tiến hành ghi bất kỳ dữ liệu nào, hệ thống duyệt qua từng món hàng trong giỏ, đọc số lượng tồn kho thực tế trong bảng Products. Nếu số lượng mua vượt quá số lượng hàng tồn hiện có, hệ thống chủ động ném ra ngoại lệ logic để kích hoạt quy trình Rollback ngay lập tức, kèm thông báo lỗi rõ ràng nêu đích danh tên sản phẩm bị thiếu hàng.

### Bước 2: Khởi tạo hóa đơn tổng trong bảng Orders
Tạo bản ghi hóa đơn mới chứa mã hóa đơn duy nhất, thời gian lập đơn, tên thu ngân phụ trách, mã khách hàng thân thiết và tổng số tiền thanh toán thực tế.
Kỹ thuật then chốt: Sử dụng mệnh đề OUTPUT INSERTED.OrderId kết hợp phương thức ExecuteScalar để thu nhận lại ngay lập tức giá trị mã khóa chính vừa được hệ thống tự động sinh ra trong cùng phiên giao dịch, phục vụ liên kết cho bước tiếp theo.

### Bước 3: Lưu chi tiết mặt hàng và trừ tồn kho
Duyệt qua từng món đồ trong danh sách mua:
- Thêm mới một dòng vào bảng OrderDetails ghi nhận mã hóa đơn cha, mã sản phẩm, số lượng mua, đơn giá niêm yết và thành tiền của dòng.
- Thực thi câu lệnh cập nhật khấu trừ số lượng tồn kho tương ứng trong bảng Products bằng phép trừ an toàn.

### Bước 4: Tích lũy điểm thưởng và Xác nhận giao dịch
Nếu giao dịch có sự tham gia của khách hàng thân thiết có số điện thoại hợp lệ, hệ thống tính toán số điểm thưởng mới theo tỷ lệ cứ mỗi mười ngàn đồng thanh toán được tặng một điểm, thực hiện câu lệnh cập nhật cộng điểm thưởng vào bảng Customers.
Cuối cùng, phương thức Commit được gọi để xác nhận toàn bộ bốn bước thành công trọn vẹn, trả về thông báo phản hồi thành công cùng mã hóa đơn tới ứng dụng máy trạm.

---

## 14. NGHIỆP VỤ MỞ RỘNG: QUY TRÌNH HỦY ĐƠN HÀNG VÀ TỰ ĐỘNG HOÀN TRẢ TỒN KHO

Nhằm giải quyết tình huống khách hàng đổi trả hoặc nhân viên thu ngân nhập sai thông tin cần hủy phiếu, hệ thống xây dựng quy trình hủy đơn hàng an toàn qua phương thức xóa bọc trong SqlTransaction:
- Kiểm tra tính hợp lệ của hóa đơn cần hủy, đọc thông tin khách hàng và số điểm thưởng đã tặng trong hóa đơn đó.
- Truy xuất toàn bộ danh sách chi tiết các mặt hàng cùng số lượng đã mua trong bảng OrderDetails của đơn này.
- Bắt đầu phiên giao dịch: Thực hiện duyệt qua danh sách mặt hàng, thực thi câu lệnh cập nhật hoàn trả cộng ngược số lượng lại vào cột tồn kho của bảng Products.
- Khấu trừ hoàn trả số điểm thưởng đã tích của khách hàng tương ứng trong bảng Customers, bảo đảm số dư điểm không bị âm.
- Thực hiện xóa các dòng chi tiết trong OrderDetails trước để không vi phạm ràng buộc khóa ngoại, sau đó thực hiện xóa hoặc cập nhật trạng thái hủy vĩnh viễn trên bảng Orders.
- Gọi Commit để chốt việc hủy đơn và hoàn kho an toàn vào cơ sở dữ liệu.

---

## 15. PHÂN HỆ MỚI: GIAO DIỆN QUẢN LÝ ĐƠN HÀNG TRÊN WINDOWS FORMS

Nhằm phục vụ công tác tra cứu và giám sát của ban quản trị cùng nhân viên thu ngân, một giao diện chuyên biệt mang tên FormOrderManagement được bổ sung vào hệ thống:

### 15.1. Bố cục giao diện hiển thị hai tầng (Master - Detail)
- Tầng trên (Master View): Sử dụng bảng lưới dgvOrders hiển thị danh sách các hóa đơn bán hàng gồm mã hóa đơn, thời điểm thanh toán, nhân viên lập đơn, mã hội viên, tổng số tiền thanh toán thực tế và trạng thái của đơn hàng.
- Tầng dưới (Detail View): Sử dụng bảng lưới dgvDetails hiển thị chi tiết các mặt hàng trong đơn hàng đang được chọn gồm mã dòng chi tiết, mã sản phẩm, số lượng mua, đơn giá tại thời điểm bán và tổng thành tiền của dòng. Khi người dùng nhấp chuột vào bất kỳ đơn hàng nào ở tầng trên, lưới ở tầng dưới sẽ tự động cập nhật ngay lập tức danh sách các món đồ tương ứng.

### 15.2. Thao tác điều khiển tiện ích
- Nút Làm mới dữ liệu: Gọi API để nạp lại danh sách hóa đơn mới nhất từ cơ sở dữ liệu về bộ nhớ.
- Nút Hủy đơn hàng: Cho phép hủy đơn hàng đang được chọn trên lưới. Trước khi thực thi, hệ thống hiển thị hộp thoại xác nhận cảnh báo người dùng. Khi người dùng đồng ý, phần mềm gửi yêu cầu hủy tới Web API, tự động kích hoạt tiến trình hoàn trả tồn kho và làm mới lại giao diện.

---

## 16. MA TRẬN PHÂN QUYỀN VAI TRÒ 2 LỚP TRÊN TOÀN HỆ THỐNG

### 16.1. Cơ chế bảo vệ hai lớp (Two-Layer Defense)
- Lớp 1 - Kiểm soát hiển thị giao diện: Ngay sau khi nhân viên đăng nhập thành công, hệ thống đọc vai trò từ phiên làm việc và thực hiện ẩn hoặc hiện các nút bấm trên thanh điều hướng bên trái. Nhân viên không có quyền sẽ không nhìn thấy nút bấm chức năng.
- Lớp 2 - Kiểm soát logic điều hướng: Khi một nút bấm được kích hoạt, phương thức điều hướng tiếp tục kiểm tra lại vai trò của người dùng trong bộ nhớ phiên làm việc một lần nữa trước khi cho phép khởi tạo và nhúng cửa sổ nghiệp vụ. Điều này ngăn chặn triệt để các hành vi gọi lệnh ngầm ngoài ý muốn.

### 16.2. Bảng phân quyền chi tiết cho ba vai trò

| Chức năng nghiệp vụ | Quản trị viên (Admin) | Thu ngân (Cashier) | Thủ kho (Warehouse) |
|---|:---:|:---:|:---:|
| Bán hàng và Quẹt thẻ POS | Được phép | Được phép | Bị ngăn chặn |
| Quản lý Nhóm ngành hàng | Được phép | Được phép | Được phép |
| Quản lý Thông tin Sản phẩm | Được phép | Được phép | Được phép |
| Quản lý Hóa đơn Đơn hàng | Được phép | Được phép | Bị ngăn chặn |
| Quản lý Hội viên Khách hàng | Được phép | Bị ngăn chặn | Bị ngăn chặn |
| Xem Báo cáo Doanh thu | Được phép | Bị ngăn chặn | Bị ngăn chặn |
| Quản trị Tài khoản Hệ thống | Được phép | Bị ngăn chặn | Bị ngăn chặn |

---

## 17. HƯỚNG DẪN CÀI ĐẶT, CẤU HÌNH VÀ VẬN HÀNH TỪ ĐẦU

### 17.1. Chuẩn bị môi trường máy tính
- Cài đặt bộ phần mềm lập trình Microsoft Visual Studio 2022 phiên bản Community hoặc Professional, có tích hợp gói công cụ phát triển ứng dụng .NET Desktop Development và ASP.NET and Web Development.
- Cài đặt hệ quản trị cơ sở dữ liệu Microsoft SQL Server bản Developer hoặc Express kèm công cụ đồ họa SQL Server Management Studio.

### 17.2. Thiết lập cơ sở dữ liệu
- Mở tệp cấu hình appsettings.json trong dự án API, điều chỉnh tham số Server trong chuỗi kết nối DefaultConnection cho khớp với tên máy chủ SQL Server cục bộ.
- Mở cửa sổ dòng lệnh tại thư mục dự án API và thực thi lệnh cập nhật cơ sở dữ liệu để tạo đầy đủ các bảng dữ liệu mẫu.
- Khởi động công cụ SQL Server Management Studio, kết nối vào cơ sở dữ liệu MiniSupermarketDb và thực thi kịch bản tạo thủ tục lưu trữ sp_GetRevenueByCashier.

### 17.3. Khởi chạy hệ thống
- Khởi động dự án Web API trước để mở cổng dịch vụ mạng và giao diện kiểm thử Swagger UI.
- Khởi động dự án Windows Forms, xuất hiện màn hình đăng nhập.
- Đăng nhập thử nghiệm bằng tài khoản quản trị viên để trải nghiệm đầy đủ tất cả các phân hệ chức năng, hoặc đăng nhập bằng tài khoản thu ngân để kiểm tra giới hạn phân quyền chỉ thấy quầy POS, danh mục, sản phẩm và hóa đơn đơn hàng.

---

## 18. DANH MỤC TẤT CẢ CÁC ĐIỂM CUỐI DỊCH VỤ WEB API VÀ ĐẶC TẢ GIAO THỨC

### 18.1. Nhóm Xác thực và Ủy quyền (Authentication)
- POST /api/auth/login: Tiếp nhận thông tin tài khoản và mật mã, thẩm định băm BCrypt và phát hành chuỗi mã hóa JWT Bearer Token.

### 18.2. Nhóm Sản phẩm và Tra cứu siêu tốc (Products & ADO.NET Lookup)
- GET /api/products: Lấy danh sách toàn bộ mặt hàng qua Entity Framework Core.
- GET /api/products/{id}: Tra cứu thông tin một sản phẩm theo mã khóa chính.
- POST /api/products: Tiếp nhận dữ liệu để tạo mới một mặt hàng.
- PUT /api/products/{id}: Cập nhật thông tin chi tiết của mặt hàng hiện có.
- DELETE /api/products/{id}: Xóa mặt hàng khỏi hệ thống.
- GET /api/ado-products/barcode/{barcode}: Điểm cuối chuyên trách tra cứu siêu tốc bằng mã vạch sử dụng luồng đọc SqlDataReader trong ADO.NET thuần.
- GET /api/ado-products/disconnected-cache: Nạp dữ liệu sản phẩm ngoại tuyến vào bảng ảo DataTable phục vụ lưu đệm.

### 18.3. Nhóm Bán hàng và Giao dịch Đơn hàng (Checkout & Orders)
- POST /api/checkout: Điểm cuối xử lý chuỗi giao dịch thanh toán POS bảo vệ bởi SqlTransaction chuẩn ACID gồm tạo hóa đơn, lưu chi tiết món, trừ kho và tích điểm.
- DELETE /api/checkout/{orderId}: Điểm cuối hủy đơn hàng và tự động hoàn trả số lượng hàng vào kho bằng SqlTransaction.
- GET /api/orders: Lấy danh sách toàn bộ các hóa đơn đã xuất kèm chi tiết mặt hàng bên trong.
- GET /api/orders/{id}: Lấy thông tin chi tiết của một hóa đơn cụ thể.
- PUT /api/orders/{id}: Cập nhật trạng thái xử lý của hóa đơn.
- DELETE /api/orders/{id}: Hủy hóa đơn bán hàng.

### 18.4. Nhóm Khách hàng thân thiết (Customers)
- GET /api/customers: Lấy danh sách hội viên tích điểm.
- POST /api/customers: Tạo mới hồ sơ khách hàng thành viên.
- PUT /api/customers/{id}: Điều chỉnh thông tin cá nhân của khách hàng.

### 18.5. Nhóm Báo cáo doanh thu (Reports)
- GET /api/reports/revenue-by-cashier: Điểm cuối thực thi thủ tục lưu trữ sp_GetRevenueByCashier để xuất dữ liệu thống kê tổng số đơn và tổng doanh thu theo từng nhân viên thu ngân.

---

## 19. BỘ KỊCH BẢN KIỂM THỬ THỰC TẾ TRÊN SWAGGER VÀ PHÒNG LAB

### 19.1. Kịch bản 1: Thanh toán đơn hàng thành công (Giao dịch Commit trọn vẹn)
- Mục tiêu: Kiểm tra quy trình thanh toán vận hành trơn tru khi giỏ hàng có số lượng mua hợp lệ nằm trong hạn mức hàng tồn kho.
- Thao tác: Gửi yêu cầu thanh toán giỏ hàng chứa sản phẩm có sẵn trong kho kèm mã hội viên thân thiết.
- Kết quả mong đợi: Hệ thống phản hồi mã trạng thái 200 OK, sinh ra mã hóa đơn mới, trả về tổng số tiền thanh toán chính xác và thông báo số điểm thưởng đã được cộng.
- Đối soát trên cơ sở dữ liệu: Kiểm tra bảng Orders thấy dòng hóa đơn mới, bảng OrderDetails có đủ các dòng sản phẩm, bảng Products thấy số lượng tồn kho bị khấu trừ chính xác và bảng Customers ghi nhận điểm thưởng tăng lên.

### 19.2. Kịch bản 2: Cố tình mua vượt quá hàng tồn kho (Kiểm chứng cơ chế Rollback an toàn)
- Mục tiêu: Minh chứng tính toàn vẹn của chuẩn ACID khi xảy ra sự cố thiếu hàng giữa chừng.
- Thao tác: Gửi yêu cầu mua một giỏ hàng gồm hai sản phẩm, trong đó sản phẩm thứ nhất có số lượng mua bình thường, còn sản phẩm thứ hai cố tình đặt mua số lượng vượt quá số lượng tồn kho thực tế của siêu thị.
- Kết quả mong đợi: Hệ thống phản hồi mã lỗi 400 BadRequest kèm thông báo chi tiết nêu rõ tên sản phẩm không đủ hàng trong kho và thông báo toàn bộ giao dịch đã được hoàn tác an toàn qua Rollback.
- Đối soát trên cơ sở dữ liệu: Kiểm tra bảng Orders tuyệt đối không có hóa đơn rác nào được sinh ra, kiểm tra số lượng tồn kho của sản phẩm thứ nhất vẫn giữ nguyên con số ban đầu, không hề bị trừ một đơn vị nào.

### 19.3. Kịch bản 3: Hủy hóa đơn và hoàn trả tồn kho tự động
- Mục tiêu: Kiểm tra quy trình hoàn trả hàng khi khách đổi ý.
- Thao tác: Gửi yêu cầu hủy một đơn hàng hợp lệ đã thanh toán trước đó.
- Kết quả mong đợi: Hệ thống xử lý thành công, phản hồi thông báo đã hoàn trả kho sạch sẽ.
- Đối soát trên cơ sở dữ liệu: Kiểm tra bảng Products thấy số lượng hàng tồn của các sản phẩm tương ứng trong đơn đã được cộng dồn bù trả lại đúng bằng số lượng đã mua trước đó.

---

## 20. BẢNG PHÂN TÍCH VÀ PHƯƠNG PHÁP KHẮC PHỤC CÁC LỖI KỸ THUẬT THƯỜNG GẶP

| Triệu chứng lỗi thường gặp | Nguyên nhân gốc rễ kỹ thuật | Biện pháp khắc phục triệt để |
|---|---|---|
| Lỗi không tìm thấy thủ tục lưu trữ trên máy chủ SQL | Lập trình viên chưa chạy lệnh tạo thủ tục trên SQL Server Management Studio hoặc chạy nhầm vào database hệ thống master | Mở lại SSMS, chọn đúng tên cơ sở dữ liệu MiniSupermarketDb và thực thi lại kịch bản CREATE PROCEDURE |
| Lỗi cú pháp câu lệnh khi gọi thủ tục lưu trữ từ C# | Lập trình viên quên khai báo thiết lập CommandType là kiểu StoredProcedure trong mã nguồn C# | Bổ sung dòng lệnh gán CommandType bằng StoredProcedure trước khi mở kết nối |
| Lỗi giao diện phần mềm bị treo đơ hoặc khóa dòng dữ liệu | Sau khi mở giao dịch bằng BeginTransaction, khi xảy ra lỗi lập trình viên quên gọi Rollback trong khối catch làm dữ liệu bị khóa vĩnh viễn | Luôn luôn đặt lời gọi Rollback nằm bên trong khối bắt ngoại lệ catch |
| Lỗi ngoại lệ lệnh yêu cầu phải có đối tượng giao dịch | Khi kết nối đã bước vào phiên giao dịch cục bộ, các đối tượng SqlCommand khởi tạo sau đó quên gắn biến transaction vào thuộc tính | Luôn truyền biến transaction vào hàm khởi tạo của mọi SqlCommand tham gia trong phiên |
| Lỗi ép kiểu dữ liệu không hợp lệ | Cột số tiền trong cơ sở dữ liệu là kiểu số thập phân nhưng trong mã C# lại ép kiểu nhầm thành số nguyên | Sử dụng hàm chuyển đổi chuẩn của lớp Convert sang kiểu số thực tương ứng |

---

## 21. BẢNG TỰ KIỂM ĐỊNH VÀ BỘ TIÊU CHÍ NGHIỆM THU CHUẨN RUBRIC BUỔI 5

| STT | Tiêu chí đánh giá chuyên đề Buổi 5 | Yêu cầu chuẩn mực cần đạt | Tình trạng tự đánh giá |
|---|---|---|:---:|
| 1 | Kiến trúc ADO.NET căn bản (Tiết 1) | Hiểu rõ 5 thành phần, sử dụng khối using an toàn, tham số hóa chống SQL Injection, phân biệt đúng 3 hàm Execute | Hoàn thành xuất sắc |
| 2 | Tra cứu Barcode siêu tốc (Tiết 2) | Endpoint tra cứu mã vạch vận hành dưới 100ms bằng SqlDataReader, hiểu cơ chế ngắt kết nối với DataTable | Hoàn thành xuất sắc |
| 3 | Tối ưu bằng Stored Procedure (Tiết 3) | Tạo thành công thủ tục sp_GetRevenueByCashier và xuất báo cáo doanh thu theo thu ngân chuẩn xác | Hoàn thành xuất sắc |
| 4 | Nắm vững chuẩn ACID (Tiết 4) | Trình bày đúng bản chất 4 tiêu chí ACID, điều khiển thành thạo vòng đời BeginTransaction, Commit và Rollback | Hoàn thành xuất sắc |
| 5 | Hoàn thiện API Checkout POS (Tiết 5) | Thực hiện đồng thời tạo đơn, lưu chi tiết, trừ kho và tích điểm trong cùng một giao dịch toàn vẹn | Hoàn thành xuất sắc |
| 6 | Kiểm chứng cơ chế Rollback (Tiết 5) | Thử nghiệm mua hàng vượt tồn kho, hệ thống ném lỗi chính xác và hoàn tác sạch sẽ không để lại rác dữ liệu | Hoàn thành xuất sắc |
| 7 | Nghiệp vụ mở rộng Hủy đơn hàng | Hoàn thiện API hủy đơn kèm tự động hoàn trả số lượng hàng vào kho dữ liệu thông qua SqlTransaction | Hoàn thành xuất sắc |
| 8 | Giao diện Quản lý Đơn hàng | Tích hợp thành công giao diện WinForms xem danh sách hóa đơn hai tầng và hỗ trợ thao tác hủy đơn tiện lợi | Hoàn thành xuất sắc |
| 9 | Hoàn thiện phân quyền Thu ngân | Cập nhật phân quyền giúp tài khoản Cashier xem được đồng thời quầy POS, danh mục, sản phẩm và hóa đơn | Hoàn thành xuất sắc |

---

## 22. ĐỊNH HƯỚNG MỞ RỘNG VÀ NÂNG CẤP HỆ THỐNG

Dựa trên nền tảng kiến trúc vững chắc đã hoàn thành tại Buổi 5, hệ thống có thể tiếp tục được nâng cấp và mở rộng ở các giai đoạn tiếp theo:
- Tích hợp cổng thanh toán trực tuyến: Kết nối API thanh toán không dùng tiền mặt qua mã phản hồi nhanh VietQR, MoMo và VNPay.
- In hóa đơn nhiệt tự động: Kết nối trực tiếp máy in hóa đơn khổ k80 thông qua thư viện in ấn chuyên dụng ngay sau khi giao dịch Commit thành công.
- Quản lý ca trực thu ngân chi tiết: Bổ sung chức năng mở ca đầu ngày, kiểm kê số tiền mặt đầu ca và kết ca đối chiếu tiền thực thu với doanh thu phần mềm.
- Ứng dụng di động dành cho khách hàng: Xây dựng ứng dụng di động cho phép khách hàng tự tra cứu điểm thưởng, xem lịch sử mua hàng và nhận các phiếu giảm giá điện tử.

---

Tác giả: Nguyễn Tuấn Anh
MSSV:2124110146
Lớp: CCQ2411D
