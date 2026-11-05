/*
 * Họ tên sinh viên: Nguyễn Tuấn Anh
 * Mô tả: Màn hình Báo cáo Quản trị: Dashboard 4 thẻ KPI tổng quan, Biểu đồ cột so sánh doanh thu theo ngày và Top 5 bán chạy (Tiêu chí 44, 45, 46, 48)
 */
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        // Dữ liệu biểu đồ cột: (ngày, doanh thu, số đơn)
        private readonly List<(DateTime Day, decimal Revenue, int Orders)> _chartData = new();

        // Lưu trữ Top 5 phục vụ xuất file Excel
        private readonly List<(int Rank, string Barcode, string ProductName, int Quantity, decimal Revenue)> _topProductsData = new();

        public FormQuickReport()
        {
            InitializeComponent();
            pnlChart.Resize += (s, e) => pnlChart.Invalidate();

            // Mặc định khoảng thời gian 7 ngày gần nhất (từ 6 ngày trước đến hôm nay)
            dateTimePicker1.Value = DateTime.Today.AddDays(-6);
            dateTimePicker2.Value = DateTime.Today;

            // Lớp bảo vệ cấp Action: chỉ Admin mới được bấm nút xem và xuất báo cáo
            bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin",
                StringComparison.OrdinalIgnoreCase);
            btnRunReport.Enabled = isAdmin;
            btnExportExcel.Enabled = isAdmin;

            btnRunReport.Click += btnRunReport_Click;
            btnExportExcel.Click += btnExportExcel_Click;
            Load += FormQuickReport_Load;
        }

        private async void FormQuickReport_Load(object? sender, EventArgs e)
        {
            // Tự động tải dữ liệu báo cáo khi mở màn hình
            if (btnRunReport.Enabled)
                await RunReportAsync();
        }

        private async void btnRunReport_Click(object? sender, EventArgs e)
        {
            await RunReportAsync();
        }

        // =========================================================================
        // TIÊU CHÍ 44, 45, 46: TỔNG HỢP BÁO CÁO TỪ CÁC ENDPOINT API
        // =========================================================================
        private async Task RunReportAsync()
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnRunReport.Enabled = false;
                btnExportExcel.Enabled = false;

                // -------------------------------------------------------------
                // 1. TIÊU CHÍ 44: Gọi API GET api/reports/dashboard-summary
                // Nạp 4 thẻ số liệu to, nổi bật trên đỉnh màn hình
                // -------------------------------------------------------------
                try
                {
                    var sumRes = await ApiClientService.Client.GetAsync("reports/dashboard-summary");
                    if (sumRes.IsSuccessStatusCode)
                    {
                        using var sumDoc = JsonDocument.Parse(await sumRes.Content.ReadAsStringAsync());
                        var rootSum = sumDoc.RootElement;

                        decimal todayRev = rootSum.GetProperty("todayRevenue").GetDecimal();
                        int totOrders = rootSum.GetProperty("totalOrders").GetInt32();
                        int newCust = rootSum.GetProperty("newCustomers").GetInt32();
                        int lowStock = rootSum.GetProperty("lowStockCount").GetInt32();

                        // Thẻ 1: Doanh thu hôm nay (VNĐ)
                        lblTotalRevenue.Text = $"{todayRev.ToString("N0", Vn)} đ";

                        // Thẻ 2: Tổng số hóa đơn đã xuất
                        lblTotalOrders.Text = totOrders.ToString("N0", Vn);

                        // Thẻ 3: Số lượng khách hàng thành viên đã đăng ký
                        lblAvgOrder.Text = $"{newCust} thành viên";

                        // Thẻ 4: Số mặt hàng sắp hết hàng (tồn kho <= 10)
                        lblBestSeller.Text = $"{lowStock} mặt hàng";
                        lblBestSeller.ForeColor = lowStock > 0 ? Color.FromArgb(220, 50, 50) : Color.FromArgb(135, 80, 190);
                    }
                }
                catch
                {
                    // Fallback an toàn nếu có lỗi
                }

                // -------------------------------------------------------------
                // 2. TIÊU CHÍ 45: Gọi API biểu đồ doanh thu theo khoảng ngày
                // GET api/reports/revenue-by-date-chart?fromDate=...&toDate=...
                // Gom nhóm GROUP BY CAST(OrderDate AS DATE)
                // -------------------------------------------------------------
                string fromStr = dateTimePicker1.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string toStr = dateTimePicker2.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                var chartRes = await ApiClientService.Client.GetAsync(
                    $"reports/revenue-by-date-chart?fromDate={fromStr}&toDate={toStr}");

                _chartData.Clear();
                if (chartRes.IsSuccessStatusCode)
                {
                    using var chartDoc = JsonDocument.Parse(await chartRes.Content.ReadAsStringAsync());
                    if (chartDoc.RootElement.TryGetProperty("data", out var dataEl))
                    {
                        foreach (var d in dataEl.EnumerateArray())
                        {
                            DateTime dt = d.GetProperty("date").GetDateTime();
                            decimal rev = d.GetProperty("totalRevenue").GetDecimal();
                            int ord = d.GetProperty("totalOrders").GetInt32();
                            _chartData.Add((dt, rev, ord));
                        }
                    }
                }

                // Nếu không có dữ liệu trả về từ API theo khoảng ngày, tự bù các ngày trong khoảng với giá trị 0
                if (_chartData.Count == 0)
                {
                    DateTime cur = dateTimePicker1.Value.Date;
                    DateTime end = dateTimePicker2.Value.Date;
                    while (cur <= end)
                    {
                        _chartData.Add((cur, 0m, 0));
                        cur = cur.AddDays(1);
                    }
                }

                pnlChart.Invalidate();

                // -------------------------------------------------------------
                // 3. TIÊU CHÍ 46: Thống kê Top 5 Sản phẩm Bán chạy nhất (Best-Sellers)
                // GET api/reports/top-selling-linq
                // Dùng LINQ GroupBy(od => od.ProductId) -> OrderByDescending -> Take(5)
                // -------------------------------------------------------------
                dgvTop.Rows.Clear();
                _topProductsData.Clear();

                var topRes = await ApiClientService.Client.GetAsync("reports/top-selling-linq");
                if (topRes.IsSuccessStatusCode)
                {
                    using var topDoc = JsonDocument.Parse(await topRes.Content.ReadAsStringAsync());
                    if (topDoc.RootElement.TryGetProperty("data", out var topEl))
                    {
                        int rank = 1;
                        foreach (var t in topEl.EnumerateArray())
                        {
                            string barcode = t.TryGetProperty("barcode", out var bc) ? bc.GetString() ?? "" : "";
                            string pName = t.GetProperty("productName").GetString() ?? "";
                            int qty = t.GetProperty("totalSoldQuantity").GetInt32();
                            decimal rev = t.GetProperty("totalRevenue").GetDecimal();

                            _topProductsData.Add((rank, barcode, pName, qty, rev));
                            dgvTop.Rows.Add(rank++, pName, qty, rev.ToString("N0", Vn));
                        }
                        dgvTop.ClearSelection();
                    }
                }

                lblTopTitle.Text = $"Top 5 sản phẩm bán chạy nhất hệ thống (Best-Sellers)";
                lblSubtitle.Text = $"Cập nhật lúc: {DateTime.Now:HH:mm:ss dd/MM/yyyy}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin",
                    StringComparison.OrdinalIgnoreCase);
                btnRunReport.Enabled = isAdmin;
                btnExportExcel.Enabled = isAdmin;
            }
        }

        // =========================================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO DOANH THU & HIỆU SUẤT RA EXCEL / CSV
        // Nghiệp vụ: Quản lý bấm nút "Xuất Excel" tải danh sách doanh thu ra tệp
        // Kỹ thuật: Xuất chuỗi CSV với chuẩn mã hóa UTF-8 BOM
                // =========================================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO DOANH THU & HIỆU SUẤT RA EXCEL / CSV
        // Nghiệp vụ: Quản lý bấm nút "Xuất Excel" tải danh sách doanh thu ra tệp
        // Kỹ thuật: Xuất chuỗi CSV với chuẩn mã hóa UTF-8 BOM hiển thị chuẩn tiếng Việt
        // =========================================================================
        private async void btnExportExcel_Click(object? sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xuất dữ liệu tài chính của siêu thị!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                btnExportExcel.Enabled = false;

                // Nếu dữ liệu biểu đồ hoặc danh sách Top chưa được nạp, tự động tải dữ liệu trước
                if (_chartData.Count == 0 && _topProductsData.Count == 0)
                {
                    await RunReportAsync();
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "Tệp CSV (Excel hỗ trợ) (*.csv)|*.csv|Tất cả tệp (*.*)|*.*",
                    FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    Title = "Xuất báo cáo doanh thu & hiệu suất siêu thị ra Excel/CSV"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                // Lấy thêm báo cáo doanh thu theo từng thu ngân từ API
                string cashierReportCsv = "";
                try
                {
                    string fromStr = dateTimePicker1.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string toStr = dateTimePicker2.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var cashierRes = await ApiClientService.Client.GetAsync(
                        $"reports/revenue-by-date-range?fromDate={fromStr}&toDate={toStr}");

                    if (cashierRes.IsSuccessStatusCode)
                    {
                        using var doc = JsonDocument.Parse(await cashierRes.Content.ReadAsStringAsync());
                        if (doc.RootElement.TryGetProperty("data", out var dataEl) && dataEl.GetArrayLength() > 0)
                        {
                            var sbCashier = new System.Text.StringBuilder();
                            sbCashier.AppendLine("--- 3. DOANH THU THEO TỪNG THU NGÂN ---");
                            sbCashier.AppendLine("Tài khoản thu ngân,Số hóa đơn đã xuất,Điểm thưởng tích lũy,Tổng doanh thu (VNĐ)");
                            foreach (var c in dataEl.EnumerateArray())
                            {
                                string uName = c.GetProperty("cashierUsername").GetString() ?? "";
                                int ords = c.GetProperty("totalOrders").GetInt32();
                                int pts = c.GetProperty("totalPointsAwarded").GetInt32();
                                decimal rev = c.GetProperty("totalRevenue").GetDecimal();
                                sbCashier.AppendLine($"\"{uName.Replace("\"", "\"\"")}\",{ords},{pts},{rev.ToString("0", CultureInfo.InvariantCulture)}");
                            }
                            sbCashier.AppendLine();
                            cashierReportCsv = sbCashier.ToString();
                        }
                    }
                }
                catch
                {
                    // Tiếp tục nếu endpoint thu ngân gặp lỗi
                }

                var sb = new System.Text.StringBuilder();

                // 1. Tiêu đề báo cáo
                sb.AppendLine("BÁO CÁO DOANH THU & HIỆU SUẤT HOẠT ĐỘNG SIÊU THỊ MINI FAMILY FRESH");
                sb.AppendLine($"Thời gian xuất báo cáo:,{DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine($"Người lập báo cáo:,\"{SessionManager.CurrentUsername ?? "Admin"}\" (Vai trò: {SessionManager.CurrentRole ?? "Admin"})");
                sb.AppendLine($"Khoảng thời gian thống kê:,Từ ngày {dateTimePicker1.Value:dd/MM/yyyy} đến ngày {dateTimePicker2.Value:dd/MM/yyyy}");
                sb.AppendLine();

                // 2. Thẻ chỉ số tổng quan (KPI Summary Cards)
                sb.AppendLine("--- 1. CHỈ SỐ TỔNG QUAN (KPI SUMMARY) ---");
                sb.AppendLine("Chỉ số,Giá trị");
                sb.AppendLine($"Doanh thu hôm nay (VNĐ),\"{lblTotalRevenue.Text}\"");
                sb.AppendLine($"Tổng số hóa đơn đã xuất,\"{lblTotalOrders.Text}\"");
                sb.AppendLine($"Khách hàng thành viên đã đăng ký,\"{lblAvgOrder.Text}\"");
                sb.AppendLine($"Số mặt hàng sắp hết hàng (tồn kho <= 10),\"{lblBestSeller.Text}\"");
                sb.AppendLine();

                // 3. Doanh thu theo từng ngày
                sb.AppendLine("--- 2. DOANH THU THEO TỪNG NGÀY TRONG KHOẢNG THỜI GIAN ---");
                sb.AppendLine("Ngày,Số lượng đơn hàng,Doanh thu (VNĐ)");
                foreach (var row in _chartData)
                {
                    sb.AppendLine($"{row.Day:dd/MM/yyyy},{row.Orders},{row.Revenue.ToString("0", CultureInfo.InvariantCulture)}");
                }
                sb.AppendLine($"TỔNG CỘNG,,{_chartData.Sum(x => x.Revenue).ToString("0", CultureInfo.InvariantCulture)}");
                sb.AppendLine();

                // 4. Doanh thu theo thu ngân (nếu có)
                if (!string.IsNullOrEmpty(cashierReportCsv))
                {
                    sb.Append(cashierReportCsv);
                }

                // 5. Top 5 sản phẩm bán chạy nhất
                int topSectionIdx = string.IsNullOrEmpty(cashierReportCsv) ? 3 : 4;
                sb.AppendLine($"--- {topSectionIdx}. TOP 5 SẢN PHẨM BÁN CHẠY NHẤT (BEST-SELLERS) ---");
                sb.AppendLine("Thứ hạng,Mã vạch,Tên sản phẩm,Số lượng đã bán,Tổng doanh thu (VNĐ)");
                foreach (var top in _topProductsData)
                {
                    string pName = $"\"{top.ProductName.Replace("\"", "\"\"")}\"";
                    string bCode = $"\"{top.Barcode}\"";
                    sb.AppendLine($"{top.Rank},{bCode},{pName},{top.Quantity},{top.Revenue.ToString("0", CultureInfo.InvariantCulture)}");
                }

                // Ghi file với UTF-8 BOM để Excel hiển thị tiếng Việt có dấu chuẩn 100%
                var utf8Bom = new System.Text.UTF8Encoding(true);
                await System.IO.File.WriteAllTextAsync(sfd.FileName, sb.ToString(), utf8Bom);

                var openConfirm = MessageBox.Show(
                    $"Đã xuất thành công báo cáo doanh thu ra tệp:\n{sfd.FileName}\n\nBạn có muốn mở tệp bằng Microsoft Excel ngay bây giờ?",
                    "Xuất báo cáo thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (openConfirm == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin",
                    StringComparison.OrdinalIgnoreCase);
                btnExportExcel.Enabled = isAdmin;
            }
        }

        private void pnlChart_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var rect = pnlChart.ClientRectangle;
            string chartTitle = $"Biểu đồ so sánh doanh thu từng ngày ({dateTimePicker1.Value:dd/MM} - {dateTimePicker2.Value:dd/MM})";

            using (var titleFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(Color.FromArgb(35, 40, 55)))
                g.DrawString(chartTitle, titleFont, titleBrush, 16, 14);

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
            float barW = Math.Min(60f, Math.Max(16f, slot * 0.65f));
            DateTime selected = _chartData[^1].Day;

            using var valueFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using var dayFont = new Font("Segoe UI", 8.5F);

            for (int i = 0; i < n; i++)
            {
                var (day, rev, ord) = _chartData[i];
                float h = (float)(rev / max) * (area.Height - 24);
                float x = area.Left + slot * i + (slot - barW) / 2f;
                var bar = new RectangleF(x, area.Bottom - h, barW, Math.Max(h, rev > 0 ? 4f : 0f));

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

                // Nhãn giá trị trên đỉnh cột
                string label = rev >= 1_000_000m ? $"{rev / 1_000_000m:0.#} tr"
                             : rev >= 1_000m ? $"{rev / 1_000m:0.#}k"
                             : rev > 0 ? rev.ToString("0") : "0";
                var size = g.MeasureString(label, valueFont);
                g.DrawString(label, valueFont, Brushes.Black, x + (barW - size.Width) / 2f, bar.Y - size.Height - 2);

                // Nhãn ngày dưới chân cột
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
