🛒 ĐỒ ÁN HỆ THỐNG QUẢN LÝ CỬA HÀNG TIỆN LỢI - SIÊU THỊ MINI FAMILY FRESH📌 BÀI THỰC HÀNH BUỔI 4: THIẾT KẾ KIẾN TRÚC GIAO DIỆN WINFORMS SHELL & ĐIỀU HƯỚNG PHÂN QUYỀN (RBAC)🎯 1. Mục tiêu bài họcKiến trúc Single-Form (WinForms Shell): Xây dựng giao diện một khung duy nhất sử dụng Left Sidebar Panel và Main Content Panel.   Kỹ thuật nhúng Form động (Dynamic Child Forms): Nhúng trực tiếp các Form con (TopLevel = false, Dock = Fill) vào vùng chứa panelMainContent thay vì mở cửa sổ mới rườm rà.   Phân quyền dựa trên vai trò (Role-Based Access Control - RBAC): Ẩn/hiện và kiểm soát điều hướng tương ứng với 3 vai trò: Admin, Cashier (Thu ngân), Warehouse (Thủ kho).   🏗️ 2. Mô hình kiến trúc giao diện (Shell Architecture)┌─────────────────────────────────────────────────────────────────────────────┐
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

## 👥 3. Danh sách tài khoản kiểm thử phân quyền (15 Accounts)

| STT | Username | Mật khẩu | Họ và tên nhân viên | Vai trò (Role) | Phạm vi chức năng cho phép |
| :---: | :--- | :---: | :--- | :---: | :--- |
| **1** | `admin01` | `123456` | Nguyễn Quản Trị | **Admin** | Toàn quyền toàn bộ hệ thống |
| **2** | `admin02` | `123456` | Trần Giám Đốc | **Admin** | Toàn quyền toàn bộ hệ thống |
| **3** | `cashier01` | `123456` | Lê Thu Ngân | **Cashier** | Màn hình POS, Khách hàng thành viên |
| **4** | `cashier02` | `123456` | Phạm Bán Hàng | **Cashier** | Màn hình POS, Khách hàng thành viên |
| **5** | `cashier03` | `123456` | Hoàng Thu Ngân | **Cashier** | Màn hình POS, Khách hàng thành viên |
| **6** | `cashier04` | `123456` | Vũ Thị Quầy | **Cashier** | Màn hình POS, Khách hàng thành viên |
| **7** | `cashier05` | `123456` | Đỗ Bán Lẻ | **Cashier** | Màn hình POS, Khách hàng thành viên |
| **8** | `ware01` | `123456` | Ngô Quản Kho | **Warehouse** | Sản phẩm, Nhóm hàng, Nhập xuất kho |
| **9** | `ware02` | `123456` | Bùi Kiểm Kê | **Warehouse** | Sản phẩm, Nhóm hàng, Nhập xuất kho |
| **10** | `ware03` | `123456` | Dương Thủ Kho | **Warehouse** | Sản phẩm, Nhóm hàng, Nhập xuất kho |
| **11** | `ware04` | `123456` | Lý Nhập Hàng | **Warehouse** | Sản phẩm, Nhóm hàng, Nhập xuất kho |
| **12** | `admin_backup`| `123456` | Đặng Hỗ Trợ | **Admin** | Toàn quyền toàn bộ hệ thống |
| **13** | `cashier06` | `123456` | Hồ Ca Chiều | **Cashier** | Màn hình POS, Khách hàng thành viên |
| **14** | `ware05` | `123456` | Trương Vận Chuyển | **Warehouse** | Sản phẩm, Nhóm hàng, Nhập xuất kho |
| **15** | `supervisor`| `123456` | Mai Giám Sát | **Admin** | Toàn quyền toàn bộ hệ thống |[cite: 1]

---

## 📊 4. Ma trận phân quyền & Điều hướng theo Vai trò

| Vai trò (Role) | Form mở mặc định | Các Form được cấp phép mở | Quyền hạn bị ngăn chặn |
| :--- | :--- | :--- | :--- |
| **Admin** | `FormCategoryManagement` | Tất cả màn hình | Không bị chặn (Toàn quyền hệ thống) |
| **Cashier** | `FormPOS` / `FormCustomer` | `FormPOS`, `FormCustomerManagement` | Quản lý kho, sửa giá, xem báo cáo doanh thu, quản lý tài khoản |
| **Warehouse** | `FormProductManagement` | `FormProductManagement`, `FormCategoryManagement` | Bán hàng POS, xem doanh thu, tạo tài khoản, quản lý khách hàng |[cite: 1]

---

## 🚀 5. Hướng dẫn cài đặt & Triển khai từng bước

### Bước 1: Thiết kế giao diện khung `FormMainShell.cs`
1. Tạo Windows Form `FormMainShell.cs` trong project quản lý cửa hàng[cite: 1].
2. Cấu hình thuộc tính: `Size` = `1280, 720`, `StartPosition` = `CenterScreen`[cite: 1].
3. Tạo 3 vùng chứa chính:
   * `panelSidebar`: `Dock = Left`, `Width = 230`, `BackColor = 24, 30, 48`[cite: 1].
   * `panelTopHeader`: `Dock = Top`, `Height = 60`, `BackColor = White`[cite: 1].
   * `panelMainContent`: `Dock = Fill`, `BackColor = 244, 245, 247`[cite: 1].
4. Bố trí các nút trên `panelSidebar`: `btnPOS`, `btnCategory`, `btnProduct`, `btnCustomer`, `btnReports`, `btnUserManage`, `btnLogout`[cite: 1].

### Bước 2: Viết mã nguồn nạp Form động (`FormMainShell.cs`)
```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FamilyFresh.WinForms {
    public partial class FormMainShell : Form {
        private Form? _activeForm = null;

        public FormMainShell() {
            InitializeComponent();
        }

        private void FormMainShell_Load(object sender, EventArgs e) {
            lblUserInfo.Text = $"Nhân viên: {SessionManager.CurrentUsername} | Vai trò: [{SessionManager.CurrentRole}]";
            ApplyRolePermissions(SessionManager.CurrentRole);
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton) {
            if (_activeForm != null) {
                _activeForm.Close();
            }
            HighlightActiveButton(senderButton);
            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(childForm);
            panelMainContent.Tag = childForm;
            
            lblTitle.Text = screenTitle;
            childForm.BringToFront();
            childForm.Show();
        }

        private void HighlightActiveButton(Button activeButton) {
            foreach (Control ctrl in panelSidebar.Controls) {
                if (ctrl is Button btn && btn != btnLogout) {
                    btn.BackColor = Color.FromArgb(24, 30, 48);
                }
            }
            activeButton.BackColor = Color.FromArgb(41, 100, 180);
        }
    }
}
```[cite: 1]

### Bước 3: Hiện thực hóa Logic Phân quyền
```csharp
private void ApplyRolePermissions(string role) {
    switch (role.ToUpper()) {
        case "ADMIN":
            btnPOS.Visible = true;
            btnCategory.Visible = true;
            btnProduct.Visible = true;
            btnCustomer.Visible = true;
            btnReports.Visible = true;
            btnUserManage.Visible = true;
            break;
        case "CASHIER":
            btnPOS.Visible = true;
            btnCustomer.Visible = true;
            btnCategory.Visible = false;
            btnProduct.Visible = false;
            btnReports.Visible = false;
            btnUserManage.Visible = false;
            break;
        case "WAREHOUSE":
            btnPOS.Visible = false;
            btnCustomer.Visible = false;
            btnReports.Visible = false;
            btnUserManage.Visible = false;
            btnCategory.Visible = true;
            btnProduct.Visible = true;
            break;
        default:
            MessageBox.Show("Tài khoản chưa được cấp quyền hạn hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
            break;
    }
}
```[cite: 1]

---

## 📂 6. Các Form con nghiệp vụ (Child Forms)

1. **`FormPOS.cs` (Màn hình Thu ngân POS):**
   * Quét mã vạch tự động thêm vào giỏ hàng (`txtBarcode_KeyDown`)[cite: 1].
   * Tự động tính tổng tiền, tiền thừa (`CalculateChange`)[cite: 1].
   * Gọi API thanh toán `orders/checkout`[cite: 1].
2. **`FormProductManagement.cs` (Quản lý Sản phẩm & Tồn kho):**
   * Quản lý CRUD sản phẩm qua API HTTP Async (`GetFromJsonAsync`, `PostAsJsonAsync`, `PutAsJsonAsync`, `DeleteAsync`)[cite: 1].
   * Lọc và xem danh mục nhóm hàng[cite: 1].
3. **`FormUserManagement.cs` (Quản trị Tài khoản & Phân quyền):**
   * Tạo tài khoản mới, phân gán vai trò (`Admin`, `Cashier`, `Warehouse`)[cite: 1].
   * Khóa/mở khóa tài khoản[cite: 1].

---

## ✅ 7. Bảng kiểm tra kết quả (Checklist)

- [ ] **Bố cục Chuẩn:** Ứng dụng chạy mượt mà trong 1 cửa sổ khung (`FormMainShell`), không bật pop-up rời rạc[cite: 1].
- [ ] **Phân quyền chính xác:**
  - Login `cashier01` ➔ Chỉ hiển thị POS & Khách hàng[cite: 1].
  - Login `ware01` ➔ Chỉ hiển thị Danh mục & Sản phẩm[cite: 1].
  - Login `admin01` ➔ Hiển thị đầy đủ chức năng[cite: 1].
- [ ] **Trải nghiệm người dùng (UX):** Nút trên Sidebar nổi bật khi được kích hoạt (`HighlightActiveButton`), tiêu đề header đồng bộ[cite: 1].

---

## 📝 8. Bài tập mở rộng
Tạo **Màn hình Báo cáo Doanh thu Nhanh (`FormQuickReport.cs`)** và bổ sung lớp bảo vệ 2 lớp (vừa ẩn Sidebar vừa chặn logic tại nút bấm):
```csharp
private void btnReports_Click(object sender, EventArgs e) {
    if (SessionManager.CurrentRole != "Admin") {
        MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của cửa hàng!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
    }
    OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT CA BÁN", btnReports);
}
```[cite: 1]

---

## 👤 THÔNG TIN TÁC GIẢ
* **Họ và tên:** Nguyễn Tuấn Anh
* **Lớp:** CCQ2411D
* **Đề tài:** Xây dựng Ứng dụng Quản lý Cửa hàng Tiện lợi với ASP.NET Core và Windows Forms -- Siêu thị Mini Family Fresh
