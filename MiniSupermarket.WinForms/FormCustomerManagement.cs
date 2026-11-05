using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // DTO riêng cho từng dòng khách hàng trên giao diện
        public class CustomerRow
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string? Address { get; set; }
            public int RewardPoints { get; set; }
            public string MembershipRank { get; set; } = string.Empty;
        }

        // DTO hứng kết quả phân trang từ Web API (api/customers/paged)
        public class PagedResponse<T>
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
            public List<T> Items { get; set; } = new();
        }

        // Trạng thái phân trang và tìm kiếm
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPages = 1;
        private int _totalItems = 0;
        private string _keyword = string.Empty;
        private string _sortBy = "id";
        private bool _isDescending = false;
        private bool _isLoading = false;

        public FormCustomerManagement()
        {
            InitializeComponent();
            SetupGrid();

            bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);

            // Các chức năng Thêm, Sửa, Xóa chỉ Admin mới hiện
            btnAdd.Visible = isAdmin;
            btnUpdate.Visible = isAdmin;
            btnDelete.Visible = isAdmin;

            // Nút đổi điểm thưởng khách hàng trực tiếp
            var btnRedeemPoints = new Button
            {
                Name = "btnRedeemPoints",
                Text = "🎁  Đổi điểm thưởng",
                Location = isAdmin ? new Point(17, 453) : new Point(17, 285),
                Size = new Size(325, 36),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRedeemPoints.FlatAppearance.BorderSize = 0;
            btnRedeemPoints.Click += btnRedeemPoints_Click;
            panelDetail.Controls.Add(btnRedeemPoints);

            if (!isAdmin)
            {
                // Với Thu ngân (Cashier), đưa nút Làm mới/Bỏ chọn lên ngay dưới nút Đổi điểm
                btnClear.Location = new Point(17, 327);
                txtName.ReadOnly = true;
                txtPhone.ReadOnly = true;
                txtAddress.ReadOnly = true;
            }
        }

        private void SetupGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "CustomerId", HeaderText = "Mã KH", Width = 70 });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "CustomerName", HeaderText = "Họ và tên", Width = 180 });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "PhoneNumber", HeaderText = "Số điện thoại", Width = 120 });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Address",
                HeaderText = "Địa chỉ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "RewardPoints", HeaderText = "Điểm", Width = 70 });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "MembershipRank", HeaderText = "Hạng thẻ", Width = 100 });
        }

        private async void FormCustomerManagement_Load(object? sender, EventArgs e)
        {
            if (cboPageSize.Items.Count > 0)
                cboPageSize.SelectedIndex = 0; // Mặc định 5 dòng/trang

            if (cboSort.Items.Count > 0)
                cboSort.SelectedIndex = 0;     // Mặc định sắp xếp theo Mã KH

            await LoadCustomersAsync();
        }

        // =====================================================
        // TẢI DỮ LIỆU PHÂN TRANG TỪ WEB API
        // =====================================================
        private async Task LoadCustomersAsync()
        {
            if (_isLoading) return;
            _isLoading = true;

            try
            {
                string url = $"customers/paged?pageNumber={_currentPage}&pageSize={_pageSize}";
                if (!string.IsNullOrWhiteSpace(_keyword))
                {
                    url += $"&keyword={Uri.EscapeDataString(_keyword.Trim())}";
                }
                if (!string.IsNullOrEmpty(_sortBy))
                {
                    url += $"&sortBy={_sortBy}&isDescending={_isDescending}";
                }

                var res = await ApiClientService.Client.GetAsync(url);

                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không tải được danh sách khách hàng.\n" + await ReadErrorAsync(res),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var paged = await res.Content.ReadFromJsonAsync<PagedResponse<CustomerRow>>();
                if (paged != null)
                {
                    _currentPage = paged.CurrentPage;
                    _pageSize = paged.PageSize;
                    _totalItems = paged.TotalItems;
                    _totalPages = Math.Max(1, paged.TotalPages);

                    dgvCustomers.DataSource = paged.Items;

                    // Cập nhật thông số thanh điều hướng phân trang
                    lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}";
                    lblTotalRecords.Text = $"| Tổng số: {_totalItems} KH";

                    btnFirstPage.Enabled = _currentPage > 1;
                    btnPrevPage.Enabled = _currentPage > 1;
                    btnNextPage.Enabled = _currentPage < _totalPages;
                    btnLastPage.Enabled = _currentPage < _totalPages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // =====================================================
        // ĐIỀU HƯỚNG PHÂN TRANG
        // =====================================================
        private async void btnFirstPage_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage = 1;
                await LoadCustomersAsync();
            }
        }

        private async void btnPrevPage_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadCustomersAsync();
            }
        }

        private async void btnNextPage_Click(object? sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadCustomersAsync();
            }
        }

        private async void btnLastPage_Click(object? sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage = _totalPages;
                await LoadCustomersAsync();
            }
        }

        private async void cboPageSize_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (int.TryParse(cboPageSize.SelectedItem?.ToString(), out int sz) && sz != _pageSize)
            {
                _pageSize = sz;
                _currentPage = 1;
                await LoadCustomersAsync();
            }
        }

        private async void cboSort_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (cboSort.SelectedIndex)
            {
                case 1: // Điểm cao nhất (VIP)
                    _sortBy = "points";
                    _isDescending = true;
                    break;
                case 2: // Điểm thấp nhất
                    _sortBy = "points";
                    _isDescending = false;
                    break;
                case 3: // Tên A - Z
                    _sortBy = "name";
                    _isDescending = false;
                    break;
                default: // Mặc định (Mã KH)
                    _sortBy = "id";
                    _isDescending = false;
                    break;
            }
            _currentPage = 1;
            await LoadCustomersAsync();
        }

        // =====================================================
        // TÌM KIẾM VÀ TẢI LẠI
        // =====================================================
        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            _keyword = txtSearch.Text.Trim();
            _currentPage = 1;
            await LoadCustomersAsync();
        }

        private async void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _keyword = txtSearch.Text.Trim();
                _currentPage = 1;
                await LoadCustomersAsync();
            }
        }

        private async void btnReload_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            _keyword = string.Empty;
            _currentPage = 1;
            await LoadCustomersAsync();
        }

        // =====================================================
        // CHỌN DÒNG -> ĐỔ LÊN Ô NHẬP
        // =====================================================
        private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCustomers.Rows[e.RowIndex].DataBoundItem is CustomerRow c)
            {
                txtId.Text = c.CustomerId.ToString();
                txtName.Text = c.CustomerName;
                txtPhone.Text = c.PhoneNumber;
                txtAddress.Text = c.Address ?? string.Empty;
                txtPoints.Text = $"{c.RewardPoints} điểm - {c.MembershipRank}";
            }
        }

        // =====================================================
        // THÊM / SỬA / XÓA
        // =====================================================
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Số điện thoại!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!txtPhone.Text.Trim().All(char.IsDigit) || txtPhone.Text.Trim().Length < 9)
            {
                MessageBox.Show("Số điện thoại không hợp lệ (phải là dãy số từ 9 chữ số trở lên)!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private object BuildPayload() => new
        {
            CustomerName = txtName.Text.Trim(),
            PhoneNumber = txtPhone.Text.Trim(),
            Address = txtAddress.Text.Trim()
        };

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chức năng Thêm khách hàng chỉ dành cho Quản trị viên (Admin)!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!ValidateInputs()) return;

            if (await SendAsync(() => ApiClientService.Client.PostAsJsonAsync("customers", BuildPayload()),
                    "Thêm khách hàng thành công!"))
            {
                ClearInputs();
                _currentPage = 1;
                await LoadCustomersAsync();
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chức năng Cập nhật khách hàng chỉ dành cho Quản trị viên (Admin)!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Hãy chọn một khách hàng trong danh sách để cập nhật!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            if (await SendAsync(() => ApiClientService.Client.PutAsJsonAsync($"customers/{id}", BuildPayload()),
                    "Cập nhật khách hàng thành công!"))
            {
                await LoadCustomersAsync();
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chức năng Xóa khách hàng chỉ dành cho Quản trị viên (Admin)!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Hãy chọn một khách hàng trong danh sách để xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Xác nhận xóa khách hàng \"{txtName.Text}\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (await SendAsync(() => ApiClientService.Client.DeleteAsync($"customers/{id}"),
                    "Đã xóa khách hàng!"))
            {
                ClearInputs();
                if (dgvCustomers.Rows.Count <= 1 && _currentPage > 1)
                    _currentPage--;

                await LoadCustomersAsync();
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtPoints.Clear();
            dgvCustomers.ClearSelection();
            txtName.Focus();
        }

        // =====================================================
        // HÀM DÙNG CHUNG: GỬI REQUEST + THÔNG BÁO KẾT QUẢ
        // =====================================================
        private async Task<bool> SendAsync(Func<Task<HttpResponseMessage>> request, string successMessage)
        {
            try
            {
                var res = await request();

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(successMessage, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                MessageBox.Show($"Thao tác thất bại (mã {(int)res.StatusCode}).\n" + await ReadErrorAsync(res),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private static async Task<string> ReadErrorAsync(HttpResponseMessage res)
        {
            string body = await res.Content.ReadAsStringAsync();
            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("message", out var m))
                    return m.GetString() ?? body;
            }
            catch { /* không phải JSON */ }

            if (res.StatusCode == System.Net.HttpStatusCode.Forbidden)
                return "Bạn không có quyền thực hiện thao tác này.";

            return body.Length > 300 ? body[..300] + "..." : body;
        }

        // =====================================================
        // ĐỔI ĐIỂM THƯỞNG KHÁCH HÀNG
        // =====================================================
        private async void btnRedeemPoints_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text) || !int.TryParse(txtId.Text, out int customerId))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần đổi điểm trong danh sách!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = (dgvCustomers.DataSource as List<CustomerRow>)?.FirstOrDefault(x => x.CustomerId == customerId);
            int currentPoints = row?.RewardPoints ?? 0;

            if (currentPoints <= 0)
            {
                MessageBox.Show($"Khách hàng {txtName.Text} hiện chưa có điểm tích lũy nào để đổi.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string prompt = $"Khách hàng: {txtName.Text}\n" +
                            $"Điểm hiện có: {currentPoints} điểm\n" +
                            $"Tỷ lệ quy đổi: 1 điểm = 1.000 VNĐ tiền mặt / voucher quà tặng.\n\n" +
                            $"Nhập số điểm cần đổi (1 - {currentPoints}):";

            string input = ShowInputDialog("Đổi điểm thưởng khách hàng", prompt, Math.Min(50, currentPoints).ToString());
            if (string.IsNullOrWhiteSpace(input)) return;

            if (!int.TryParse(input, out int points) || points <= 0 || points > currentPoints)
            {
                MessageBox.Show($"Số điểm không hợp lệ! Vui lòng nhập số nguyên từ 1 đến {currentPoints}.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var payload = new
            {
                Points = points,
                Note = "Đổi điểm thưởng trực tiếp tại quầy CSKH"
            };

            var ok = await SendAsync(
                () => ApiClientService.Client.PostAsJsonAsync($"customers/{customerId}/redeem-points", payload),
                $"Đổi thành công {points} điểm!\n" +
                $"Giá trị tiền mặt/voucher nhận được: {(points * 1000).ToString("N0", new CultureInfo("vi-VN"))} VNĐ.\n" +
                $"Số điểm còn lại: {currentPoints - points} điểm.");

            if (ok)
            {
                await LoadCustomersAsync();
                ClearInputs();
            }
        }

        private static string ShowInputDialog(string caption, string prompt, string defaultValue)
        {
            using Form form = new Form();
            form.Text = caption;
            form.Width = 420;
            form.Height = 220;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            Label lbl = new Label() { Left = 20, Top = 15, Text = prompt, AutoSize = true };
            TextBox txt = new TextBox() { Left = 20, Top = 105, Width = 360, Text = defaultValue };
            Button btnOk = new Button() { Text = "Xác nhận đổi", Left = 170, Width = 110, Top = 140, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(41, 100, 180), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            Button btnCancel = new Button() { Text = "Hủy bỏ", Left = 290, Width = 90, Top = 140, DialogResult = DialogResult.Cancel };

            form.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? txt.Text.Trim() : string.Empty;
        }
    }
}
