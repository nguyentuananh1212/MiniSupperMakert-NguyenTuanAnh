using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private static readonly Color SidebarColor = Color.FromArgb(24, 30, 48);
        private static readonly Color ActiveColor = Color.FromArgb(41, 100, 180);

        // Form con đang hiển thị trong panelMainContent
        private Form? _activeForm = null;

        public FormMainShell()
        {
            InitializeComponent();
        }

        private void FormMainShell_Load(object sender, EventArgs e)
        {
            // 1. Thông tin phiên đăng nhập
            lblUserInfo.Text =
                $"Nhân viên: {(string.IsNullOrWhiteSpace(SessionManager.CurrentFullName) ? SessionManager.CurrentUsername : $"{SessionManager.CurrentFullName} ({SessionManager.CurrentUsername})")} | " +
                $"Vai trò: [{SessionManager.CurrentRole}]";

            // 2. Phân quyền giao diện (role không hợp lệ -> đóng form)
            if (!ApplyRolePermissions(SessionManager.CurrentRole))
                return;

            // 3. Mở màn hình mặc định theo vai trò
            OpenDefaultScreenByRole(SessionManager.CurrentRole);
        }

        // =====================================================
        // NHÚNG FORM CON VÀO PANEL MAIN
        // =====================================================

        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton)
        {
            HighlightActiveButton(senderButton);
            lblTitle.Text = screenTitle;

            panelMainContent.SuspendLayout();
            try
            {
                // Đóng + giải phóng form cũ
                var old = _activeForm;
                _activeForm = null;
                panelMainContent.Controls.Clear();
                old?.Close();
                old?.Dispose();

                _activeForm = childForm;

                // Nhúng form con: không viền, không cửa sổ riêng, TRÀN KÍN vùng nội dung
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.ControlBox = false;
                childForm.StartPosition = FormStartPosition.Manual;
                childForm.Margin = Padding.Empty;
                childForm.Dock = DockStyle.Fill;

                panelMainContent.Controls.Add(childForm);
                panelMainContent.Tag = childForm;

                childForm.Show();
                childForm.BringToFront();
            }
            finally
            {
                panelMainContent.ResumeLayout(true);
            }
        }

        // =====================================================
        // HIGHLIGHT NÚT MENU ĐANG CHỌN
        // =====================================================

        private void HighlightActiveButton(Button activeButton)
        {
            foreach (Control ctrl in panelSidebar.Controls)
            {
                if (ctrl is Button btn)
                    btn.BackColor = SidebarColor;
            }

            activeButton.BackColor = ActiveColor;
        }

        // =====================================================
        // PHÂN QUYỀN (Admin / Cashier / Warehouse)
        // =====================================================

        /// <summary>
        /// Nguồn sự thật duy nhất cho quyền truy cập.
        /// Dùng chung cho cả ẩn nút (lớp 1) và chặn logic khi click (lớp 2).
        /// </summary>
        private bool CanAccess(Button btn, string? role)
        {
            switch ((role ?? string.Empty).Trim().ToUpperInvariant())
            {
                case "ADMIN":
                    return true;

                case "CASHIER":
                    return btn == btnPOS || btn == btnCustomer;

                case "WAREHOUSE":
                    return btn == btnCategory || btn == btnProduct;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Lớp 1: ẩn/hiện nút trên sidebar. Trả về false nếu role không hợp lệ.
        /// </summary>
        private bool ApplyRolePermissions(string? role)
        {
            var menus = new[] { btnPOS, btnCategory, btnProduct, btnCustomer, btnReports, btnUserManage };

            if (!menus.Any(b => CanAccess(b, role)))
            {
                MessageBox.Show(
                    "Tài khoản chưa được cấp quyền hạn hợp lệ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return false;
            }

            foreach (var b in menus)
                b.Visible = CanAccess(b, role);

            return true;
        }

        /// <summary>
        /// Điều hướng vào màn hình đúng chuyên môn của từng vai trò.
        /// </summary>
        private void OpenDefaultScreenByRole(string? role)
        {
            switch ((role ?? string.Empty).Trim().ToUpperInvariant())
            {
                // Gọi trực tiếp handler thay vì PerformClick(): PerformClick
                // không đáng tin cậy khi form còn đang trong sự kiện Load.
                case "ADMIN":
                case "WAREHOUSE":
                    btnCategory_Click(btnCategory, EventArgs.Empty);
                    break;

                case "CASHIER":
                    btnPOS_Click(btnPOS, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// Lớp 2: kiểm tra quyền lần nữa trước khi mở form.
        /// </summary>
        private void Navigate(Button btn, Func<Form> createForm, string title)
        {
            if (!CanAccess(btn, SessionManager.CurrentRole))
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập chức năng này!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            OpenChildForm(createForm(), title, btn);
        }

        // =====================================================
        // SỰ KIỆN CLICK CÁC NÚT SIDEBAR
        // =====================================================

        private void btnPOS_Click(object sender, EventArgs e) =>
            Navigate(btnPOS, () => new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)");

        private void btnCategory_Click(object sender, EventArgs e) =>
            Navigate(btnCategory, () => new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG");

        private void btnProduct_Click(object sender, EventArgs e) =>
            Navigate(btnProduct, () => new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG");

        private void btnCustomer_Click(object sender, EventArgs e) =>
            Navigate(btnCustomer, () => new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM");

        private void btnReports_Click(object sender, EventArgs e) =>
            Navigate(btnReports, () => new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT");

        private void btnUserManage_Click(object sender, EventArgs e) =>
            Navigate(btnUserManage, () => new FormUserManagement(), "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN HỆ THỐNG");

        // =====================================================
        // ĐĂNG XUẤT
        // =====================================================

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất phiên làm việc?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            // Xóa phiên làm việc
            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentUsername = string.Empty;
            SessionManager.CurrentFullName = string.Empty;
            SessionManager.CurrentRole = string.Empty;

            Hide();
            new FormLogin().ShowDialog();
            Close();
        }

        // =====================================================
        // KHI ĐÓNG FORM
        // =====================================================

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _activeForm?.Close();
            _activeForm = null;

            base.OnFormClosed(e);
        }
    }
}