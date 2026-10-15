using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        // Dữ liệu biểu đồ cột: (ngày, doanh thu)
        private readonly List<(DateTime Day, decimal Revenue)> _chartData = new();

        public FormQuickReport()
        {
            InitializeComponent();

            // Lớp bảo vệ cấp Action: chỉ Admin mới được bấm nút xem báo cáo
            bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin",
                StringComparison.OrdinalIgnoreCase);
            btnRunReport.Enabled = isAdmin;

            btnRunReport.Click += btnRunReport_Click;
            Load += FormQuickReport_Load;
        }

        private async void FormQuickReport_Load(object? sender, EventArgs e)
        {
            // Tự động xem báo cáo của ngày hôm nay khi mở màn hình
            if (btnRunReport.Enabled)
                await RunReportAsync();
        }

        private async void btnRunReport_Click(object? sender, EventArgs e)
        {
            await RunReportAsync();
        }

        private async Task RunReportAsync()
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                btnRunReport.Enabled = false;

                string date = dateTimePicker1.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var res = await ApiClientService.Client.GetAsync($"reports/daily?date={date}");

                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Không lấy được báo cáo (mã {(int)res.StatusCode}).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                var root = doc.RootElement;

                int totalOrders = root.GetProperty("totalOrders").GetInt32();
                decimal totalRevenue = root.GetProperty("totalRevenue").GetDecimal();
                decimal avg = root.TryGetProperty("averageOrderValue", out var avgEl) ? avgEl.GetDecimal() : 0m;
                int itemsSold = root.TryGetProperty("itemsSold", out var itEl) ? itEl.GetInt32() : 0;

                string best = "---";
                if (root.TryGetProperty("bestSellerName", out var nameEl) &&
                    nameEl.ValueKind == JsonValueKind.String)
                {
                    int qty = root.GetProperty("bestSellerQuantity").GetInt32();
                    best = $"{nameEl.GetString()} ({qty})";
                }

                lblTotalOrders.Text = totalOrders.ToString("N0", Vn);
                lblTotalRevenue.Text = $"{totalRevenue.ToString("N0", Vn)} đ";
                lblAvgOrder.Text = $"{avg.ToString("N0", Vn)} đ";
                lblBestSeller.Text = best;
                lblTopTitle.Text = $"Top 5 sản phẩm bán chạy  •  Đã bán {itemsSold:N0} sản phẩm";

                // Top 5 sản phẩm
                dgvTop.Rows.Clear();
                if (root.TryGetProperty("topProducts", out var topEl))
                {
                    int rank = 1;
                    foreach (var t in topEl.EnumerateArray())
                    {
                        dgvTop.Rows.Add(
                            rank++,
                            t.GetProperty("productName").GetString(),
                            t.GetProperty("quantity").GetInt32(),
                            t.GetProperty("revenue").GetDecimal().ToString("N0", Vn));
                    }
                    dgvTop.ClearSelection();
                }

                // Doanh thu 7 ngày
                _chartData.Clear();
                if (root.TryGetProperty("last7Days", out var weekEl))
                {
                    foreach (var d in weekEl.EnumerateArray())
                    {
                        _chartData.Add((d.GetProperty("date").GetDateTime(), d.GetProperty("revenue").GetDecimal()));
                    }
                }
                pnlChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunReport.Enabled = string.Equals(SessionManager.CurrentRole, "Admin",
                    StringComparison.OrdinalIgnoreCase);
            }
        }

        // =====================================================
        // VẼ BIỂU ĐỒ CỘT DOANH THU 7 NGÀY
        // =====================================================
        private void pnlChart_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var rect = pnlChart.ClientRectangle;
            using (var titleFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(Color.FromArgb(35, 40, 55)))
                g.DrawString("Doanh thu 7 ngày gần nhất", titleFont, titleBrush, 16, 14);

            if (_chartData.Count == 0)
            {
                using var f = new Font("Segoe UI", 10F, FontStyle.Italic);
                TextRenderer.DrawText(g, "Chưa có dữ liệu - bấm \"Xem báo cáo\"", f, rect,
                    Color.Gray, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                return;
            }

            var area = new Rectangle(16, 56, rect.Width - 32, rect.Height - 56 - 36);
            if (area.Width <= 0 || area.Height <= 0) return;

            decimal max = _chartData.Max(x => x.Revenue);
            if (max <= 0) max = 1;

            // Đường lưới ngang
            using (var gridPen = new Pen(Color.FromArgb(232, 234, 240)))
            {
                for (int i = 0; i <= 4; i++)
                {
                    int y = area.Bottom - (int)(area.Height * (i / 4f));
                    g.DrawLine(gridPen, area.Left, y, area.Right, y);
                }
            }

            int n = _chartData.Count;
            float slot = area.Width / (float)n;
            float barW = Math.Min(70f, slot * 0.55f);
            DateTime selected = _chartData[^1].Day;

            using var valueFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var dayFont = new Font("Segoe UI", 9F);

            for (int i = 0; i < n; i++)
            {
                var (day, rev) = _chartData[i];
                float h = (float)(rev / max) * (area.Height - 22);
                float x = area.Left + slot * i + (slot - barW) / 2f;
                var bar = new RectangleF(x, area.Bottom - h, barW, Math.Max(h, rev > 0 ? 3f : 0f));

                bool isSelected = day == selected;
                var c1 = isSelected ? Color.FromArgb(34, 170, 84) : Color.FromArgb(120, 170, 225);
                var c2 = isSelected ? Color.FromArgb(24, 130, 64) : Color.FromArgb(41, 100, 180);

                if (bar.Height > 0)
                {
                    using var br = new System.Drawing.Drawing2D.LinearGradientBrush(
                        new RectangleF(bar.X, bar.Y - 1, bar.Width, bar.Height + 2), c1, c2,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    g.FillRectangle(br, bar);
                }

                string label = rev >= 1_000_000m ? $"{rev / 1_000_000m:0.#} tr"
                             : rev >= 1_000m ? $"{rev / 1_000m:0.#}k"
                             : rev.ToString("0");
                var size = g.MeasureString(label, valueFont);
                g.DrawString(label, valueFont, Brushes.Black, x + (barW - size.Width) / 2f, bar.Y - size.Height - 1);

                string dayText = day.ToString("dd/MM");
                var dSize = g.MeasureString(dayText, dayFont);
                using var dayBrush = new SolidBrush(isSelected ? Color.FromArgb(24, 130, 64) : Color.Gray);
                g.DrawString(dayText, dayFont, dayBrush, x + (barW - dSize.Width) / 2f, area.Bottom + 8);
            }
        }
    }

    // Panel vẽ mượt, không nhấp nháy (dùng cho biểu đồ)
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
