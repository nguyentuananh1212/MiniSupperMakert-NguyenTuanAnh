🛒 ĐỒ ÁN HỆ THỐNG QUẢN LÝ CỬA HÀNG TIỆN LỢI - SIÊU THỊ MINI FAMILY FRESH📌 BÀI THỰC HÀNH BUỔI 4: THIẾT KẾ KIẾN TRÚC GIAO DIỆN WINFORMS SHELL & ĐIỀU HƯỚNG PHÂN QUYỀN (RBAC)🎯 1. Mục tiêu bài họcKiến trúc Single-Form (WinForms Shell): Xây dựng giao diện một khung duy nhất sử dụng Thanh điều hướng bên trái (Left Sidebar Panel) và Vùng hiển thị nghiệp vụ trung tâm (Main Content Panel).   Kỹ thuật nhúng Form động (Dynamic Child Forms): Nhúng trực tiếp các cửa sổ giao diện con vào vùng chứa trung tâm (panelMainContent) thay vì mở nhiều cửa sổ mới rườm rà.   Phân quyền dựa trên vai trò (Role-Based Access Control - RBAC): Kiểm soát hiển thị các nút chức năng và điều hướng giao diện phù hợp với 3 vai trò chính: Admin, Cashier (Thu ngân), và Warehouse (Thủ kho).   🏗️ 2. Mô hình kiến trúc giao diện (Shell Architecture)┌─────────────────────────────────────────────────────────────────────────────┐
│                             FormMainShell                                   │
├────────────────────┬────────────────────────────────────────────────────────┤
│  panelSidebar      │  panelTopHeader (Breadcrumb & User Info)               │
│  (Width = 230)     ├────────────────────────────────────────────────────────┤
│  - Logo Family Fresh│                                                       │
│  - btnPOS          │  panelMainContent                                      │
│  - btnCategory     │  (Dock = Fill)                                         │
│  - btnProduct      │                                                        │
│  - btnCustomer     │  [Vùng nhúng động các Form con / UserControl]          │
│  - btnReports      │                                                        │
│  - btnUserManage   │                                                        │
│  ───────────────   │                                                        │
│  - panelUserFooter │                                                        │
│    (Tên & Logout)  │                                                        │
└────────────────────┴────────────────────────────────────────────────────────┘
```[cite: 1]

---

## 📊 3. Ma trận phân quyền & Điều hướng theo Vai trò

| Vai trò (Role) | Form mở mặc định | Các Form được cấp phép mở | Quyền hạn bị ngăn chặn |
| :--- | :--- | :--- | :--- |
| **Admin** | `FormCategoryManagement` | Tất cả màn hình | Không bị chặn (Toàn quyền hệ thống) |
| **Cashier** | `FormPOS` / `FormCustomer` | `FormPOS`, `FormCustomerManagement` | Quản lý kho, sửa giá, xem báo cáo doanh thu, quản lý tài khoản |
| **Warehouse** | `FormProductManagement` | `FormProductManagement`, `FormCategoryManagement` | Bán hàng POS, xem doanh thu, tạo tài khoản, quản lý khách hàng |[cite: 1]

---

## 🚀 4. Quy trình thiết kế & Triển khai giao diện

### Bước 1: Thiết kế khung `FormMainShell`
1. Tạo một Windows Form chính đặt tên là `FormMainShell`[cite: 1].
2. Cấu hình kích thước chuẩn HD (`1280x720`) và hiển thị giữa màn hình[cite: 1].
3. Bố trí 3 khu vực chính sử dụng các Panel điều khiển:
   * **`panelSidebar`**: Đặt cố định lề trái (Dock Left) chứa logo, các nút điều hướng chức năng và nút đăng xuất[cite: 1].
   * **`panelTopHeader`**: Đặt cố định lề trên (Dock Top) hiển thị tiêu đề màn hình hiện tại và thông tin nhân viên đăng nhập[cite: 1].
   * **`panelMainContent`**: Lấp đầy khoảng trống còn lại (Dock Fill) làm vùng chứa để nhúng các giao diện chức năng[cite: 1].

### Bước 2: Xây dựng cơ chế nhúng Form động
1. Tạo quy trình dọn dẹp Form con cũ trước khi hiển thị Form mới[cite: 1].
2. Bỏ các thuộc tính cửa sổ độc lập (thiết lập `TopLevel = false` và loại bỏ viền Windows) đối với Form con cần nhúng[cite: 1].
3. Đặt Form con lấp đầy vùng chứa trung tâm (`Dock = Fill`) và thêm vào tập hợp điều khiển của `panelMainContent`[cite: 1].
4. Cập nhật trạng thái đổi màu nổi bật cho nút menu đang được chọn trên Sidebar và đồng bộ tiêu đề trên thanh Header[cite: 1].

### Bước 3: Thiết lập Logic Phân quyền giao diện
1. Nhận thông tin vai trò (`Role`) của người dùng từ phiên đăng nhập[cite: 1].
2. Kiểm tra vai trò để thực hiện ẩn/hiện các nút menu tương ứng trên Sidebar[cite: 1].
3. Tự động chuyển hướng đến màn hình chức năng mặc định phù hợp với nhiệm vụ của từng vai trò ngay sau khi đăng nhập thành công[cite: 1].

---

## 📂 5. Danh sách các Form con nghiệp vụ (Child Forms)

1. **`FormPOS` (Màn hình Bán hàng Quầy POS):**
   * Cho phép quét mã vạch sản phẩm tự động thêm vào giỏ[cite: 1].
   * Tính toán tổng tiền, tiếp nhận tiền khách đưa và tính tiền thừa[cite: 1].
   * Thực hiện thanh toán và hoàn tất đơn hàng[cite: 1].
2. **`FormProductManagement` (Quản lý Sản phẩm & Tồn kho):**
   * Tra cứu, thêm, sửa, xóa thông tin sản phẩm[cite: 1].
   * Quản lý số lượng tồn kho và phân loại theo nhóm hàng[cite: 1].
3. **`FormUserManagement` (Quản trị Tài khoản & Phân quyền):**
   * Quản lý danh sách nhân viên, khởi tạo tài khoản mới[cite: 1].
   * Phân gán vai trò (`Admin`, `Cashier`, `Warehouse`) và quản lý trạng thái tài khoản[cite: 1].

---

## ✅ 6. Bảng kiểm tra kết quả (Checklist)

- [ ] **Khung giao diện thống nhất:** Toàn bộ ứng dụng vận hành trong một cửa sổ duy nhất (`FormMainShell`), không bị mở nhiều cửa sổ rời rạc[cite: 1].
- [ ] **Kiểm tra phân quyền thành công:**
  - Tài khoản nhóm `Cashier` ➔ Sidebar chỉ xuất hiện chức năng Bán hàng (POS) và Khách hàng[cite: 1].
  - Tài khoản nhóm `Warehouse` ➔ Sidebar chỉ xuất hiện chức năng Sản phẩm và Danh mục[cite: 1].
  - Tài khoản nhóm `Admin` ➔ Hiển thị đầy đủ toàn bộ nút chức năng[cite: 1].
- [ ] **Trải nghiệm người dùng:** Nhấp chọn nút nào thì nút đó đổi màu nổi bật, tiêu đề ở Header thay đổi đúng với tên màn hình nghiệp vụ tương ứng[cite: 1].

---

## 📝 7. Bài tập mở rộng
* Xây dựng thêm **Màn hình Báo cáo Doanh thu Nhanh (`FormQuickReport`)**[cite: 1].
* Thiết lập cơ chế kiểm soát bảo vệ 2 lớp: Vừa ẩn nút trên Sidebar, vừa kiểm tra lại vai trò khi người dùng thực hiện thao tác xem báo cáo tài chính (chỉ cho phép vai trò `Admin`)[cite: 1].

---

## 👤 THÔNG TIN TÁC GIẢ
* **Họ và tên:** Nguyễn Tuấn Anh
* **Lớp:** CCQ2411D
* **Đề tài:** Xây dựng Ứng dụng Quản lý Cửa hàng Tiện lợi với ASP.NET Core và Windows 
