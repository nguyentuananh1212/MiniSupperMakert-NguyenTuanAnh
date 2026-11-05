/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: FormOrderManagement - Quản lý danh sách đơn hàng, xem chi tiết, hủy đơn
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormOrderManagement : Form
    {
        // DTO cục bộ khớp với API trả về
        private class OrderDetailRow
        {
            public int    OrderDetailId { get; set; }
            public int    ProductId     { get; set; }
            public int    Quantity      { get; set; }
            public decimal UnitPrice    { get; set; }
        }

        private class OrderRow
        {
            public int      OrderId          { get; set; }
            public DateTime OrderDate        { get; set; }
            public string   CashierUsername  { get; set; } = string.Empty;
            public int?     CustomerId       { get; set; }
            public decimal  TotalAmount      { get; set; }
            public int      Status           { get; set; }   // 0=Pending,1=Completed,2=Cancelled
            public List<OrderDetailRow> Details { get; set; } = new();
        }

        private static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        private DataGridView dgvOrders      = new DataGridView();
        private DataGridView dgvDetails     = new DataGridView();
        private Button       btnRefresh     = new Button();
        private Button       btnCancelOrder = new Button();
        private Label        lblOrdersTitle = new Label();
        private Label        lblDetailTitle = new Label();
        private Panel        pnlTop         = new Panel();
        private Panel        pnlBottom      = new Panel();

        private List<OrderRow> _orders = new();

        public FormOrderManagement()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ----- Nhãn tiêu đề -----
            lblOrdersTitle.Text      = "DANH SÁCH ĐƠN HÀNG";
            lblOrdersTitle.Font      = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            lblOrdersTitle.AutoSize  = true;
            lblOrdersTitle.Location  = new System.Drawing.Point(8, 8);

            lblDetailTitle.Text      = "CHI TIẾT ĐƠN HÀNG";
            lblDetailTitle.Font      = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
            lblDetailTitle.AutoSize  = true;
            lblDetailTitle.Location  = new System.Drawing.Point(8, 8);

            // ----- Nút làm mới -----
            btnRefresh.Text          = "🔄 Làm mới";
            btnRefresh.Size          = new System.Drawing.Size(110, 32);
            btnRefresh.Location      = new System.Drawing.Point(8, 36);
            btnRefresh.BackColor     = System.Drawing.Color.FromArgb(41, 100, 180);
            btnRefresh.ForeColor     = System.Drawing.Color.White;
            btnRefresh.FlatStyle     = FlatStyle.Flat;
            btnRefresh.Click        += async (s, e) => await LoadOrdersAsync();

            // ----- Nút hủy đơn (chỉ Admin mới hiện) -----
            bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);
            btnCancelOrder.Visible   = isAdmin;
            btnCancelOrder.Text      = "❌ Hủy đơn hàng";
            btnCancelOrder.Size      = new System.Drawing.Size(140, 32);
            btnCancelOrder.Location  = new System.Drawing.Point(126, 36);
            btnCancelOrder.BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
            btnCancelOrder.ForeColor = System.Drawing.Color.White;
            btnCancelOrder.FlatStyle = FlatStyle.Flat;
            btnCancelOrder.Click    += async (s, e) => await CancelSelectedOrderAsync();

            // ----- Grid đơn hàng -----
            dgvOrders.Anchor        = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrders.Location      = new System.Drawing.Point(8, 76);
            dgvOrders.Size          = new System.Drawing.Size(900, 260);
            dgvOrders.ReadOnly      = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect   = false;
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

            // Cột
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId",       HeaderText = "Mã ĐH",         DataPropertyName = "OrderId",         Width = 60  });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDate",      HeaderText = "Ngày lập",       DataPropertyName = "OrderDate",        Width = 140 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCashier",   HeaderText = "Thu ngân",       DataPropertyName = "CashierUsername",  Width = 130 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCustomer",  HeaderText = "Mã KH",          DataPropertyName = "CustomerId",       Width = 70  });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal",     HeaderText = "Tổng tiền (đ)",  DataPropertyName = "TotalAmountText",  Width = 130 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus",    HeaderText = "Trạng thái",     DataPropertyName = "StatusText",       Width = 110 });

            // ----- Grid chi tiết -----
            dgvDetails.Anchor        = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvDetails.Location      = new System.Drawing.Point(8, 36);
            dgvDetails.Size          = new System.Drawing.Size(900, 220);
            dgvDetails.ReadOnly      = true;
            dgvDetails.AutoGenerateColumns = false;
            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDtlId",    HeaderText = "Mã chi tiết",   DataPropertyName = "OrderDetailId",    Width = 90  });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDtlProd",  HeaderText = "Mã SP",          DataPropertyName = "ProductId",        Width = 70  });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDtlQty",   HeaderText = "Số lượng",       DataPropertyName = "Quantity",         Width = 90  });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDtlPrice", HeaderText = "Đơn giá (đ)",    DataPropertyName = "UnitPriceText",    Width = 120 });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDtlTotal", HeaderText = "Thành tiền (đ)", DataPropertyName = "LineTotalText",    Width = 130 });

            // ----- Panel trên (đơn hàng) -----
            pnlTop.Dock   = DockStyle.Top;
            pnlTop.Height = 350;
            pnlTop.Controls.Add(lblOrdersTitle);
            pnlTop.Controls.Add(btnRefresh);
            pnlTop.Controls.Add(btnCancelOrder);
            pnlTop.Controls.Add(dgvOrders);

            // ----- Panel dưới (chi tiết) -----
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.Controls.Add(lblDetailTitle);
            pnlBottom.Controls.Add(dgvDetails);

            // ----- Form -----
            this.Text          = "Quản lý đơn hàng";
            this.ClientSize    = new System.Drawing.Size(920, 640);
            this.MinimumSize   = new System.Drawing.Size(800, 500);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
            this.Load += async (s, e) => await LoadOrdersAsync();

            this.ResumeLayout(false);
        }

        // =====================================================
        // TẢI DANH SÁCH ĐƠN HÀNG
        // =====================================================
        private async Task LoadOrdersAsync()
        {
            try
            {
                _orders = await ApiClientService.Client
                    .GetFromJsonAsync<List<OrderRow>>("orders")
                    ?? new List<OrderRow>();

                var display = _orders.Select(o => new
                {
                    o.OrderId,
                    OrderDate       = o.OrderDate.ToString("dd/MM/yyyy HH:mm", Vn),
                    o.CashierUsername,
                    CustomerId      = o.CustomerId.HasValue ? o.CustomerId.ToString() : "(Khách lẻ)",
                    TotalAmountText = o.TotalAmount.ToString("N0", Vn),
                    StatusText      = o.Status switch
                    {
                        0 => "Chờ xử lý",
                        1 => "Hoàn thành",
                        2 => "Đã hủy",
                        _ => "Không xác định"
                    }
                }).ToList();

                dgvOrders.DataSource = display;
                dgvDetails.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách đơn hàng:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // HIỂN THỊ CHI TIẾT KHI CHỌN DÒNG
        // =====================================================
        private void DgvOrders_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null) return;

            int idx = dgvOrders.CurrentRow.Index;
            if (idx < 0 || idx >= _orders.Count) return;

            var order = _orders[idx];
            var detailDisplay = order.Details.Select(d => new
            {
                d.OrderDetailId,
                d.ProductId,
                d.Quantity,
                UnitPriceText = d.UnitPrice.ToString("N0", Vn),
                LineTotalText = (d.UnitPrice * d.Quantity).ToString("N0", Vn)
            }).ToList();

            dgvDetails.DataSource = detailDisplay;
        }

        // =====================================================
        // HỦY ĐƠN HÀNG
        // =====================================================
        private async Task CancelSelectedOrderAsync()
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ Quản trị viên (Admin) mới có quyền hủy đơn hàng!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (dgvOrders.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để hủy.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idx = dgvOrders.CurrentRow.Index;
            if (idx < 0 || idx >= _orders.Count) return;

            var order = _orders[idx];

            if (order.Status == 2)
            {
                MessageBox.Show("Đơn hàng này đã được hủy trước đó.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn hủy đơn hàng #{order.OrderId} không?\n" +
                "Tồn kho sẽ được khôi phục tự động.",
                "Xác nhận hủy đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var res = await ApiClientService.Client
                    .DeleteAsync($"orders/{order.OrderId}");

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Đã hủy đơn hàng #{order.OrderId} thành công.", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadOrdersAsync();
                }
                else
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    MessageBox.Show($"Hủy đơn hàng thất bại:\n{msg}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
