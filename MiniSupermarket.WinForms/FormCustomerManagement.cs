using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // DTO riêng cho form khách hàng
        public class CustomerRow
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string? Address { get; set; }
            public int RewardPoints { get; set; }
            public string MembershipRank { get; set; } = string.Empty;
        }

        public FormCustomerManagement()
        {
            InitializeComponent();
            SetupGrid();

            // Chỉ Admin mới được xóa khách hàng (API cũng chặn ở lớp 2)
            btnDelete.Enabled = string.Equals(SessionManager.CurrentRole, "Admin",
                StringComparison.OrdinalIgnoreCase);

            // Nút đổi điểm thưởng khách hàng trực tiếp
            var btnRedeemPoints = new Button
            {
                Name = "btnRedeemPoints",
                Text = "🎁  Đổi điểm thưởng",
                Location = new Point(17, 453),
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
            await LoadCustomersAsync();
        }

        // =====================================================
        // TẢI / TÌM KIẾM
        // =====================================================
        private async Task LoadCustomersAsync(string? keyword = null)
        {
            try
            {
                string url = string.IsNullOrWhiteSpace(keyword)
                    ? "customers"
                    : $"customers/search?keyword={Uri.EscapeDataString(keyword.Trim())}";

                var res = await ApiClientService.Client.GetAsync(url);

                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Không tải được danh sách khách hàng.\n" + await ReadErrorAsync(res),
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var list = await res.Content.ReadFromJsonAsync<List<CustomerRow>>() ?? new List<CustomerRow>();
                dgvCustomers.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            await LoadCustomersAsync(txtSearch.Text);
        }

        private async void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await LoadCustomersAsync(txtSearch.Text);
            }
        }

        private async void btnReload_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
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
                MessageBox.Show("Họ tên và số điện thoại không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string phone = txtPhone.Text.Trim();
            if (phone.Length < 9 || phone.Length > 11 || !phone.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải gồm 9-11 chữ số!", "Cảnh báo",
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
            if (!ValidateInputs()) return;

            if (await SendAsync(() => ApiClientService.Client.PostAsJsonAsync("customers", BuildPayload()),
                    "Thêm khách hàng thành công!"))
            {
                ClearInputs();
                await LoadCustomersAsync();
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
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
                await LoadCustomersAsync(txtSearch.Text);
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
