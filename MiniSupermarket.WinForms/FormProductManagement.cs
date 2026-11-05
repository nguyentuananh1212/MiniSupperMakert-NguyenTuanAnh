/*
 * Họ tên sinh viên: Nguyen Tuan Anh
 * Mô tả: Code-behind xử lý hiển thị danh sách phân trang, tìm kiếm và CRUD sản phẩm (Buổi 6)
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    // Lớp nhận dữ liệu DTO từ API về Client (khớp với Backend Buổi 6)
    public class ProductItemViewDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CategoryComboDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class PagedViewResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new();
    }

    public partial class FormProductManagement : Form
    {
        private int _currentPage = 1;
        private readonly int _pageSize = 5;
        private int _totalPages = 1;

        public FormProductManagement()
        {
            InitializeComponent();
            SetupGrid();
            ApplyRolePermissions();
        }

        private void ApplyRolePermissions()
        {
            bool isAdmin = string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);
            bool isWarehouse = string.Equals(SessionManager.CurrentRole, "Warehouse", StringComparison.OrdinalIgnoreCase);
            bool canModify = isAdmin || isWarehouse;

            // Thu ngân (Cashier) chỉ được xem, ẩn toàn bộ chức năng Thêm, Sửa, Xóa
            btnAdd.Visible = canModify;
            btnUpdate.Visible = canModify;
            btnDelete.Visible = canModify;

            if (!canModify)
            {
                txtBarcode.ReadOnly = true;
                txtProductName.ReadOnly = true;
                nudPrice.Enabled = false;
                nudStock.Enabled = false;
                cboCategory.Enabled = false;

                btnLoad.Location = new System.Drawing.Point(15, 385);
                btnLoad.Size = new System.Drawing.Size(300, 38);
                btnLoad.Text = "🔄  Làm mới / Bỏ chọn";
            }
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 70 });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Barcode", HeaderText = "Mã vạch", Width = 130 });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên sản phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Đơn giá",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockQuantity",
                HeaderText = "Tồn kho",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "CategoryName", HeaderText = "Nhóm hàng", Width = 160 });

            // Tiêu chí 43: Bắt sự kiện CellFormatting của DataGridView trong WinForms
            dgvProducts.CellFormatting += dgvProducts_CellFormatting;
        }

        // =====================================================
        // TẢI DỮ LIỆU
        // =====================================================

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            await LoadCategoriesComboAsync(); // Nạp nhóm hàng vào ComboBox
            await LoadProductsPagedAsync();   // Nạp bảng sản phẩm phân trang
        }

        // 1. Tải danh sách nhóm hàng lên ComboBox (cboCategory và cboFilterCategory)
        private async Task LoadCategoriesComboAsync()
        {
            try
            {
                var categories = await ApiClientService.Client
                    .GetFromJsonAsync<List<CategoryComboDto>>("categories") ?? new List<CategoryComboDto>();

                // ComboBox chọn nhóm hàng khi Thêm / Sửa
                cboCategory.DataSource = new List<CategoryComboDto>(categories);
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                // ComboBox lọc nhóm hàng (Bài tập 6.2)
                var filterList = new List<CategoryComboDto>
                {
                    new CategoryComboDto { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" }
                };
                filterList.AddRange(categories);
                cboFilterCategory.DataSource = filterList;
                cboFilterCategory.DisplayMember = "CategoryName";
                cboFilterCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Gọi API phân trang và hiển thị dữ liệu lên DataGridView
        private async Task LoadProductsPagedAsync()
        {
            try
            {
                string search = txtSearchBarcode.Text.Trim();
                int categoryId = (cboFilterCategory.SelectedItem as CategoryComboDto)?.CategoryId ?? 0;

                string url = $"products/paged?keyword={Uri.EscapeDataString(search)}&pageNumber={_currentPage}&pageSize={_pageSize}";
                if (categoryId > 0)
                {
                    url += $"&categoryId={categoryId}";
                }

                var result = await ApiClientService.Client.GetFromJsonAsync<PagedViewResult<ProductItemViewDto>>(url);
                if (result != null)
                {
                    _currentPage = result.CurrentPage;
                    _totalPages = result.TotalPages == 0 ? 1 : result.TotalPages;

                    // Hiển thị dữ liệu lên bảng
                    dgvProducts.DataSource = result.Items;

                    // Cập nhật nhãn phân trang
                    lblPaging.Text = $"Trang {_currentPage} / {_totalPages} (Tổng: {result.TotalItems} SP)";

                    // Tiêu chí 43: Huy hiệu & Cảnh báo Hàng sắp hết kho trên thanh trạng thái
                    int lowStockOnPage = result.Items.Count(p => p.StockQuantity <= 10);
                    lblLowStockBadge.Text = lowStockOnPage > 0
                        ? $"⚠️ Cảnh báo: Có {lowStockOnPage} SP sắp hết kho (tồn ≤ 10)!"
                        : "✅ Tồn kho trang này an toàn";
                    lblLowStockBadge.ForeColor = lowStockOnPage > 0 ? Color.FromArgb(200, 30, 30) : Color.FromArgb(46, 160, 67);

                    // Bật/tắt nút điều hướng
                    btnPrev.Enabled = _currentPage > 1;
                    btnNext.Enabled = _currentPage < _totalPages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu sản phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Sự kiện chuyển trang
        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadProductsPagedAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadProductsPagedAsync();
            }
        }

        // 4. Sự kiện tìm kiếm & lọc
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1; // Khi tìm kiếm từ khóa mới, luôn quay về trang 1
            await LoadProductsPagedAsync();
        }

        private async void txtSearchBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _currentPage = 1;
                await LoadProductsPagedAsync();
            }
        }

        private async void cboFilterCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboFilterCategory.SelectedIndex < 0) return;
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        // =====================================================
        // CHỌN DÒNG TRÊN LƯỚI -> ĐỔ LÊN FORM NHẬP LIỆU
        // =====================================================

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is not ProductItemViewDto p) return;

            txtId.Text = p.ProductId.ToString();
            txtBarcode.Text = p.Barcode;
            txtProductName.Text = p.ProductName;
            nudPrice.Value = Math.Min(p.Price, nudPrice.Maximum);
            nudStock.Value = Math.Min(p.StockQuantity, (int)nudStock.Maximum);
            if (p.CategoryId > 0)
            {
                cboCategory.SelectedValue = p.CategoryId;
            }
        }

        // =====================================================
        // THÊM / SỬA / XÓA SẢN PHẨM QUA API
        // =====================================================

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã vạch!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBarcode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrice.Focus();
                return false;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Thêm mới sản phẩm qua API
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(SessionManager.CurrentRole, "Warehouse", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chức năng Thêm mới chỉ dành cho Quản trị viên!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!ValidateInputs()) return;

            var newProd = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = (int)cboCategory.SelectedValue!
            };

            var res = await ApiClientService.Client.PostAsJsonAsync("products", newProd);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                await LoadProductsPagedAsync();
            }
            else
            {
                string detail = await res.Content.ReadAsStringAsync();
                MessageBox.Show($"Thêm thất bại. Mã lỗi: {res.StatusCode}\n{detail}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Cập nhật sản phẩm qua API
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(SessionManager.CurrentRole, "Warehouse", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chức năng Cập nhật chỉ dành cho Quản trị viên!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong danh sách để cập nhật!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            var updated = new
            {
                ProductId = id,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = (int)cboCategory.SelectedValue!
            };

            var res = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", updated);
            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsPagedAsync();
            }
            else
            {
                string detail = await res.Content.ReadAsStringAsync();
                MessageBox.Show($"Cập nhật thất bại. Mã lỗi: {res.StatusCode}\n{detail}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Xóa sản phẩm qua API
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(SessionManager.CurrentRole, "Warehouse", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chức năng Xóa sản phẩm chỉ dành cho Quản trị viên!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trong danh sách để xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm #{id} ({txtProductName.Text})?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var res = await ApiClientService.Client.DeleteAsync($"products/{id}");
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadProductsPagedAsync();
                }
                else
                {
                    string detail = await res.Content.ReadAsStringAsync();
                    MessageBox.Show($"Xóa thất bại. Mã lỗi: {res.StatusCode}\n{detail}", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Nút làm mới danh sách
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtSearchBarcode.Clear();
            if (cboFilterCategory.Items.Count > 0) cboFilterCategory.SelectedIndex = 0;
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            txtBarcode.Focus();
        }

        // =====================================================
        // TIÊU CHÍ 43: HUY HIỆU & CẢNH BÁO HÀNG SẮP HẾT KHO
        // Nghiệp vụ: Trên lưới DataGridView, các sản phẩm có số lượng tồn <= 10
        // sẽ được tự động tô màu nền đỏ/cam cảnh báo.
        // Kỹ thuật: Bắt sự kiện CellFormatting của DataGridView:
        // nếu cột StockQuantity <= 10 thì đổi e.CellStyle.BackColor = Color.LightCoral
        // =====================================================
        private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count) return;

            // Kiểm tra cột StockQuantity (Tồn kho)
            if (dgvProducts.Columns[e.ColumnIndex]?.DataPropertyName == "StockQuantity" && e.Value != null)
            {
                if (e.CellStyle != null && int.TryParse(e.Value.ToString(), out int qty) && qty <= 10)
                {
                    e.CellStyle.BackColor = Color.LightCoral;
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.SelectionBackColor = Color.IndianRed;
                    e.CellStyle.SelectionForeColor = Color.White;
                    e.CellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);
                }
            }
        }

        // =====================================================
        // TIÊU CHÍ 48: XUẤT BÁO CÁO DANH MỤC RA EXCEL / CSV
        // Nghiệp vụ: Quản lý có thể bấm nút "Xuất Excel" để tải danh sách
        // toàn bộ mặt hàng kèm số lượng tồn kho ra tệp .xlsx / .csv
        // Kỹ thuật: Xuất chuỗi CSV với UTF-8 BOM trực tiếp phía WinForms
        // =====================================================
        private async void btnExportExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                btnExportExcel.Enabled = false;
                string search = txtSearchBarcode.Text.Trim();
                int categoryId = (cboFilterCategory.SelectedItem as CategoryComboDto)?.CategoryId ?? 0;

                // Tải danh sách đầy đủ để xuất Excel
                string url = $"products/paged?keyword={Uri.EscapeDataString(search)}&pageNumber=1&pageSize=1000";
                if (categoryId > 0)
                {
                    url += $"&categoryId={categoryId}";
                }

                var result = await ApiClientService.Client.GetFromJsonAsync<PagedViewResult<ProductItemViewDto>>(url);
                var items = result?.Items ?? new List<ProductItemViewDto>();

                if (items.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu sản phẩm để xuất file!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "Tệp CSV (Excel hỗ trợ) (*.csv)|*.csv|Tất cả tệp (*.*)|*.*",
                    FileName = $"DanhSachSanPham_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    Title = "Xuất danh mục sản phẩm kèm tồn kho ra Excel/CSV"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var sb = new System.Text.StringBuilder();
                    // Tiêu đề cột
                    sb.AppendLine("Mã sản phẩm,Mã vạch,Tên sản phẩm,Đơn giá (VNĐ),Số lượng tồn kho,Nhóm hàng,Cảnh báo tồn kho");

                    foreach (var item in items)
                    {
                        string alert = item.StockQuantity <= 10 ? "SẮP HẾT HÀNG (<=10)" : "Bình thường";
                        string prodName = $"\"{item.ProductName.Replace("\"", "\"\"")}\"";
                        string catName = $"\"{item.CategoryName.Replace("\"", "\"\"")}\"";
                        string barcode = $"\"{item.Barcode}\"";
                        sb.AppendLine($"{item.ProductId},{barcode},{prodName},{item.Price},{item.StockQuantity},{catName},{alert}");
                    }

                    // Ghi file với UTF-8 BOM để Excel hiển thị tiếng Việt có dấu chuẩn 100%
                    var utf8Bom = new System.Text.UTF8Encoding(true);
                    await System.IO.File.WriteAllTextAsync(sfd.FileName, sb.ToString(), utf8Bom);

                    var openConfirm = MessageBox.Show(
                        $"Đã xuất thành công {items.Count} sản phẩm ra tệp:\n{sfd.FileName}\n\nBạn có muốn mở tệp ngay bây giờ?",
                        "Xuất Excel thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (openConfirm == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExportExcel.Enabled = true;
            }
        }
    }
}