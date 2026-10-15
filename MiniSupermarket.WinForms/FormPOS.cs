using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        // ===== DTO riêng cho màn hình POS (đặt tên riêng để không trùng form khác) =====
        public class PosProduct
        {
            public int ProductId { get; set; }
            public string Barcode { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
        }

        public class PosCustomer
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public int RewardPoints { get; set; }
            public string MembershipRank { get; set; } = string.Empty;
        }

        public class PosCartItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public int StockQuantity { get; set; }
            public decimal TotalPrice => UnitPrice * Quantity;
        }

        private static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        // Giỏ hàng hiện tại và khách hàng thân thiết đang chọn (null = khách lẻ)
        private readonly List<PosCartItem> _cart = new();
        private PosCustomer? _customer = null;

        public FormPOS()
        {
            InitializeComponent();

            // Phím tắt toàn màn hình: F2 / F9 / Delete / + / -
            KeyPreview = true;
            KeyDown += FormPOS_KeyDown;
        }

        private void FormPOS_Load(object? sender, EventArgs e)
        {
            lblTotalAmount.Text = "0 đ";
            lblChange.Text = "0 đ";
            UpdateCartStatus();
            txtBarcode.Focus();
        }

        private void FormPOS_KeyDown(object? sender, KeyEventArgs e)
        {
            bool typing = ActiveControl is TextBox tb && !(tb == txtBarcode && tb.Text.Length == 0);

            switch (e.KeyCode)
            {
                case Keys.F9:
                    e.SuppressKeyPress = true;
                    btnCheckout.PerformClick();
                    break;

                case Keys.F2:
                    e.SuppressKeyPress = true;
                    txtBarcode.Focus();
                    txtBarcode.SelectAll();
                    break;

                case Keys.Delete when !typing:
                    e.SuppressKeyPress = true;
                    DeleteSelectedLine();
                    break;

                case Keys.Add when !typing:
                case Keys.Oemplus when !typing:
                    e.SuppressKeyPress = true;
                    ChangeSelectedQuantity(+1);
                    break;

                case Keys.Subtract when !typing:
                case Keys.OemMinus when !typing:
                    e.SuppressKeyPress = true;
                    ChangeSelectedQuantity(-1);
                    break;
            }
        }

        // =====================================================
        // QUÉT MÃ VẠCH -> THÊM VÀO GIỎ
        // =====================================================
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string barcode = txtBarcode.Text.Trim();
                if (barcode.Length == 0) return;

                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
                txtBarcode.Focus();
            }
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                var res = await ApiClientService.Client
                    .GetAsync($"products/barcode/{Uri.EscapeDataString(barcode)}");

                if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Lỗi tra cứu sản phẩm (mã {(int)res.StatusCode}).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var product = await res.Content.ReadFromJsonAsync<PosProduct>();
                if (product == null) return;

                var existing = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                int newQty = (existing?.Quantity ?? 0) + 1;

                if (newQty > product.StockQuantity)
                {
                    MessageBox.Show(
                        $"Sản phẩm \"{product.ProductName}\" chỉ còn {product.StockQuantity} trong kho!",
                        "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (existing != null)
                {
                    existing.Quantity = newQty;
                }
                else
                {
                    _cart.Add(new PosCartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1,
                        StockQuantity = product.StockQuantity
                    });
                }

                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Vẽ lại lưới giỏ hàng + tổng tiền (giữ nguyên dòng đang chọn)
        private void UpdateCartDisplay(int selectIndex = -1)
        {
            dgvCart.Rows.Clear();

            foreach (var item in _cart)
            {
                dgvCart.Rows.Add(
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice.ToString("N0", Vn),
                    item.Quantity,
                    item.TotalPrice.ToString("N0", Vn));
            }

            if (dgvCart.Rows.Count > 0)
            {
                if (selectIndex < 0 || selectIndex >= dgvCart.Rows.Count)
                    selectIndex = dgvCart.Rows.Count - 1;

                dgvCart.ClearSelection();
                dgvCart.Rows[selectIndex].Selected = true;
                dgvCart.CurrentCell = dgvCart.Rows[selectIndex].Cells[1];
            }

            lblTotalAmount.Text = $"{GetTotal().ToString("N0", Vn)} đ";
            UpdateCartStatus();
            CalculateChange();
        }

        private void UpdateCartStatus()
        {
            int qty = _cart.Sum(x => x.Quantity);
            lblCartStatus.Text = _cart.Count == 0
                ? "Giỏ hàng trống"
                : $"{_cart.Count} mặt hàng  •  {qty} sản phẩm";
        }

        // =====================================================
        // ĐỔI SỐ LƯỢNG / XÓA DÒNG
        // =====================================================
        private int SelectedIndex =>
            dgvCart.CurrentRow?.Index ?? (dgvCart.SelectedRows.Count > 0 ? dgvCart.SelectedRows[0].Index : -1);

        private void ChangeSelectedQuantity(int delta)
        {
            int idx = SelectedIndex;
            if (idx < 0 || idx >= _cart.Count) return;

            var item = _cart[idx];
            int newQty = item.Quantity + delta;

            if (newQty <= 0)
            {
                _cart.RemoveAt(idx);
                UpdateCartDisplay(Math.Min(idx, _cart.Count - 1));
                return;
            }

            if (newQty > item.StockQuantity)
            {
                MessageBox.Show($"Sản phẩm \"{item.ProductName}\" chỉ còn {item.StockQuantity} trong kho!",
                    "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            item.Quantity = newQty;
            UpdateCartDisplay(idx);
        }

        private void DeleteSelectedLine()
        {
            int idx = SelectedIndex;
            if (idx < 0 || idx >= _cart.Count) return;

            _cart.RemoveAt(idx);
            UpdateCartDisplay(Math.Min(idx, _cart.Count - 1));
        }

        private void btnPlus_Click(object? sender, EventArgs e) { ChangeSelectedQuantity(+1); txtBarcode.Focus(); }
        private void btnMinus_Click(object? sender, EventArgs e) { ChangeSelectedQuantity(-1); txtBarcode.Focus(); }
        private void btnDeleteRow_Click(object? sender, EventArgs e) { DeleteSelectedLine(); txtBarcode.Focus(); }

        private decimal GetTotal() => _cart.Sum(x => x.TotalPrice);

        // =====================================================
        // KHÁCH HÀNG THÂN THIẾT
        // =====================================================
        private async void txtCustomerPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await LookupCustomerAsync(txtCustomerPhone.Text.Trim());
            }
        }

        private async Task LookupCustomerAsync(string phone)
        {
            if (phone.Length == 0)
            {
                SetCustomer(null);
                return;
            }

            try
            {
                var res = await ApiClientService.Client
                    .GetAsync($"customers/phone/{Uri.EscapeDataString(phone)}");

                if (res.IsSuccessStatusCode)
                {
                    SetCustomer(await res.Content.ReadFromJsonAsync<PosCustomer>());
                }
                else
                {
                    SetCustomer(null);
                    MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này. Hóa đơn sẽ tính cho khách lẻ.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetCustomer(PosCustomer? customer)
        {
            _customer = customer;

            lblCustomerName.Text = customer == null
                ? "Khách vãng lai"
                : $"{customer.CustomerName}  •  Hạng {customer.MembershipRank} ({customer.RewardPoints} điểm)";
            lblCustomerName.ForeColor = customer == null
                ? Color.FromArgb(100, 105, 120)
                : Color.FromArgb(41, 100, 180);
        }

        // =====================================================
        // TIỀN KHÁCH ĐƯA / TIỀN THỪA
        // =====================================================
        private bool _formattingCash = false;

        private void txtCashReceived_TextChanged(object? sender, EventArgs e)
        {
            if (_formattingCash) return;

            // Tự thêm dấu phân cách hàng nghìn khi gõ: 100000 -> 100.000
            _formattingCash = true;
            try
            {
                string digits = new string(txtCashReceived.Text.Where(char.IsDigit).ToArray());
                string formatted = digits.Length == 0
                    ? "0"
                    : decimal.Parse(digits).ToString("N0", Vn);

                if (txtCashReceived.Text != formatted)
                {
                    txtCashReceived.Text = formatted;
                    txtCashReceived.SelectionStart = txtCashReceived.Text.Length;
                }
            }
            finally
            {
                _formattingCash = false;
            }

            CalculateChange();
        }

        private void txtCashReceived_Enter(object? sender, EventArgs e)
        {
            BeginInvoke(new Action(() => txtCashReceived.SelectAll()));
        }

        // Nút tiền nhanh: Vừa đủ / 50k / 100k / 200k / 500k
        private void btnQuickCash_Click(object? sender, EventArgs e)
        {
            if (sender is not Button b) return;

            decimal value = b.Name switch
            {
                "btnExact" => GetTotal(),
                "btn50k" => 50_000m,
                "btn100k" => 100_000m,
                "btn200k" => 200_000m,
                "btn500k" => 500_000m,
                _ => 0m
            };

            txtCashReceived.Text = value.ToString("N0", Vn);
            txtBarcode.Focus();
        }

        // Lấy số tiền từ ô nhập (chấp nhận 100000, 100.000, 100,000)
        private bool TryGetCash(out decimal cash)
        {
            string digits = new string(txtCashReceived.Text.Where(char.IsDigit).ToArray());
            return decimal.TryParse(digits, out cash);
        }

        private void CalculateChange()
        {
            if (TryGetCash(out decimal cash) && cash > 0)
            {
                decimal change = cash - GetTotal();

                lblChange.Text = change >= 0
                    ? $"{change.ToString("N0", Vn)} đ"
                    : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Black : Color.Red;
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
            }
        }

        // =====================================================
        // THANH TOÁN
        // =====================================================
        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryGetCash(out decimal cash) || cash < GetTotal())
            {
                MessageBox.Show("Tiền khách đưa chưa đủ để thanh toán!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCashReceived.Focus();
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = _customer?.PhoneNumber,
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            try
            {
                btnCheckout.Enabled = false;
                var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(json);

                    int orderId = doc.RootElement.GetProperty("orderId").GetInt32();
                    int points = doc.RootElement.GetProperty("pointsEarned").GetInt32();
                    decimal change = cash - GetTotal();

                    string msg = $"Thanh toán thành công! Mã hóa đơn: #{orderId}\n" +
                                 $"Tiền thừa trả khách: {change.ToString("N0", Vn)} đ";
                    if (_customer != null)
                        msg += $"\nĐiểm tích lũy được cộng: +{points}";

                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetPos();
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại từ máy chủ!\n" +
                        await ReadErrorAsync(response), "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckout.Enabled = true;
            }
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
            catch { /* body không phải JSON */ }

            if (res.StatusCode == System.Net.HttpStatusCode.Forbidden)
                return "Bạn không có quyền thực hiện thao tác này.";

            return body.Length > 300 ? body[..300] + "..." : body;
        }

        // =====================================================
        // HỦY GIỎ HÀNG
        // =====================================================
        private void btnClearCart_Click(object sender, EventArgs e)
        {
            ResetPos();
        }

        // Đưa màn hình POS về trạng thái ban đầu
        private void ResetPos()
        {
            _cart.Clear();
            dgvCart.Rows.Clear();
            UpdateCartStatus();

            lblTotalAmount.Text = "0 đ";
            txtCashReceived.Text = "0";
            lblChange.Text = "0 đ";
            lblChange.ForeColor = Color.Black;

            txtCustomerPhone.Clear();
            SetCustomer(null);

            txtBarcode.Focus();
        }
    }
}
