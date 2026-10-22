namespace MiniSupermarket.WinForms
{
    partial class FormPOS
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            var navy = Color.FromArgb(24, 30, 48);
            var green = Color.FromArgb(34, 170, 84);
            var red = Color.FromArgb(200, 40, 50);
            var blue = Color.FromArgb(41, 100, 180);
            var textDark = Color.FromArgb(35, 40, 55);
            var textGray = Color.FromArgb(100, 105, 120);

            pnlLeft = new Panel();
            pnlScan = new TableLayoutPanel();
            labelBarcode = new Label();
            pnlBarcodeLine = new Panel();
            txtBarcode = new TextBox();
            dgvCart = new DataGridView();
            colProductId = new DataGridViewTextBoxColumn();
            colProductName = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            pnlCartBar = new Panel();
            lblCartStatus = new Label();
            lblHint = new Label();
            flowCartButtons = new FlowLayoutPanel();
            btnDeleteRow = new Button();
            btnPlus = new Button();
            btnMinus = new Button();

            pnlRight = new Panel();
            tblRight = new TableLayoutPanel();
            groupBoxCustomer = new Panel();
            labelCustomerTitle = new Label();
            labelCustomerPhone = new Label();
            txtCustomerPhone = new TextBox();
            lblCustomerName = new Label();
            pnlTotal = new Panel();
            labelTotal = new Label();
            lblTotalAmount = new Label();
            labelCashReceived = new Label();
            pnlCashLine = new Panel();
            txtCashReceived = new TextBox();
            tblQuickCash = new TableLayoutPanel();
            btnExact = new Button();
            btn50k = new Button();
            btn100k = new Button();
            btn200k = new Button();
            btn500k = new Button();
            pnlChange = new Panel();
            labelChange = new Label();
            lblChange = new Label();
            btnCheckout = new Button();
            btnClearCart = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnlLeft.SuspendLayout();
            pnlScan.SuspendLayout();
            pnlBarcodeLine.SuspendLayout();
            pnlCartBar.SuspendLayout();
            flowCartButtons.SuspendLayout();
            pnlRight.SuspendLayout();
            tblRight.SuspendLayout();
            groupBoxCustomer.SuspendLayout();
            pnlTotal.SuspendLayout();
            pnlCashLine.SuspendLayout();
            tblQuickCash.SuspendLayout();
            pnlChange.SuspendLayout();
            SuspendLayout();

            // ================= KHU VỰC TRÁI =================
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Padding = new Padding(16, 14, 8, 14);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Controls.Add(dgvCart);      // Fill: add trước
            pnlLeft.Controls.Add(pnlCartBar);   // Bottom
            pnlLeft.Controls.Add(pnlScan);      // Top

            // pnlScan: "Quét mã vạch:" + ô nhập có gạch chân
            pnlScan.BackColor = Color.White;
            pnlScan.ColumnCount = 2;
            pnlScan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            pnlScan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlScan.RowCount = 1;
            pnlScan.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlScan.Dock = DockStyle.Top;
            pnlScan.Height = 64;
            pnlScan.Margin = new Padding(0);
            pnlScan.Padding = new Padding(16, 0, 16, 0);
            pnlScan.Name = "pnlScan";
            pnlScan.Controls.Add(labelBarcode, 0, 0);
            pnlScan.Controls.Add(pnlBarcodeLine, 1, 0);

            labelBarcode.Dock = DockStyle.Fill;
            labelBarcode.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelBarcode.ForeColor = textDark;
            labelBarcode.Name = "labelBarcode";
            labelBarcode.Text = "Quét mã vạch:";
            labelBarcode.TextAlign = ContentAlignment.MiddleLeft;

            pnlBarcodeLine.BackColor = blue;          // gạch chân 2px màu xanh
            pnlBarcodeLine.Dock = DockStyle.Fill;
            pnlBarcodeLine.Margin = new Padding(0, 14, 0, 14);
            pnlBarcodeLine.Padding = new Padding(0, 0, 0, 2);
            pnlBarcodeLine.Name = "pnlBarcodeLine";
            pnlBarcodeLine.Controls.Add(txtBarcode);

            txtBarcode.BackColor = Color.White;
            txtBarcode.BorderStyle = BorderStyle.None;
            txtBarcode.Dock = DockStyle.Fill;
            txtBarcode.Font = new Font("Segoe UI", 14F);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.TabIndex = 0;
            txtBarcode.KeyDown += txtBarcode_KeyDown;

            // dgvCart
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.AllowUserToResizeRows = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCart.GridColor = Color.FromArgb(232, 234, 240);
            dgvCart.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCart.ColumnHeadersHeight = 42;
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = navy,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                SelectionBackColor = navy,
                SelectionForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0)
            };
            dgvCart.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = textDark,
                Font = new Font("Segoe UI", 11F),
                SelectionBackColor = Color.FromArgb(220, 232, 250),
                SelectionForeColor = textDark,
                Padding = new Padding(8, 0, 8, 0)
            };
            dgvCart.RowTemplate.Height = 40;
            dgvCart.RowHeadersVisible = false;
            dgvCart.MultiSelect = false;
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Name = "dgvCart";
            dgvCart.TabIndex = 1;
            dgvCart.Columns.AddRange(new DataGridViewColumn[] { colProductId, colProductName, colPrice, colQuantity, colTotal });

            colProductId.HeaderText = "Mã SP";
            colProductId.Name = "colProductId";
            colProductId.ReadOnly = true;
            colProductId.Width = 90;
            colProductId.SortMode = DataGridViewColumnSortMode.NotSortable;

            colProductName.HeaderText = "Tên Sản Phẩm";
            colProductName.Name = "colProductName";
            colProductName.ReadOnly = true;
            colProductName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProductName.SortMode = DataGridViewColumnSortMode.NotSortable;

            colPrice.HeaderText = "Đơn Giá";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            colPrice.Width = 130;
            colPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
            colPrice.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };
            colPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            colQuantity.HeaderText = "SL";
            colQuantity.Name = "colQuantity";
            colQuantity.ReadOnly = true;
            colQuantity.Width = 70;
            colQuantity.SortMode = DataGridViewColumnSortMode.NotSortable;
            colQuantity.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };
            colQuantity.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTotal.HeaderText = "Thành Tiền";
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            colTotal.Width = 150;
            colTotal.SortMode = DataGridViewColumnSortMode.NotSortable;
            colTotal.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            colTotal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            // pnlCartBar: trạng thái giỏ + nút -, +, Xóa dòng + dòng phím tắt
            pnlCartBar.BackColor = Color.White;
            pnlCartBar.Dock = DockStyle.Bottom;
            pnlCartBar.Height = 74;
            pnlCartBar.Name = "pnlCartBar";
            pnlCartBar.Padding = new Padding(16, 6, 16, 6);
            pnlCartBar.Controls.Add(lblCartStatus);      // Fill: add trước
            pnlCartBar.Controls.Add(flowCartButtons);    // Right
            pnlCartBar.Controls.Add(lblHint);            // Bottom

            lblCartStatus.AutoSize = false;
            lblCartStatus.Dock = DockStyle.Fill;
            lblCartStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCartStatus.ForeColor = textDark;
            lblCartStatus.Name = "lblCartStatus";
            lblCartStatus.Text = "Giỏ hàng trống";
            lblCartStatus.TextAlign = ContentAlignment.MiddleLeft;

            flowCartButtons.Dock = DockStyle.Right;
            flowCartButtons.FlowDirection = FlowDirection.LeftToRight;
            flowCartButtons.Name = "flowCartButtons";
            flowCartButtons.Width = 290;
            flowCartButtons.WrapContents = false;
            flowCartButtons.Padding = new Padding(0, 2, 0, 0);
            flowCartButtons.Controls.Add(btnMinus);
            flowCartButtons.Controls.Add(btnPlus);
            flowCartButtons.Controls.Add(btnDeleteRow);

            StyleOutline(btnMinus, "−", Color.FromArgb(60, 65, 80), Color.FromArgb(200, 204, 214), 12F);
            btnMinus.Name = "btnMinus"; btnMinus.Size = new Size(50, 38); btnMinus.Margin = new Padding(0, 0, 8, 0); btnMinus.TabStop = false;
            btnMinus.Click += btnMinus_Click;

            StyleOutline(btnPlus, "+", Color.FromArgb(60, 65, 80), Color.FromArgb(200, 204, 214), 12F);
            btnPlus.Name = "btnPlus"; btnPlus.Size = new Size(50, 38); btnPlus.Margin = new Padding(0, 0, 8, 0); btnPlus.TabStop = false;
            btnPlus.Click += btnPlus_Click;

            StyleOutline(btnDeleteRow, "Xóa dòng (Del)", red, red, 9.5F);
            btnDeleteRow.Name = "btnDeleteRow"; btnDeleteRow.Size = new Size(150, 38); btnDeleteRow.Margin = new Padding(0); btnDeleteRow.TabStop = false;
            btnDeleteRow.Click += btnDeleteRow_Click;

            lblHint.AutoSize = false;
            lblHint.Dock = DockStyle.Bottom;
            lblHint.Height = 22;
            lblHint.Font = new Font("Segoe UI", 8.5F);
            lblHint.ForeColor = textGray;
            lblHint.Name = "lblHint";
            lblHint.Text = "F2: ô quét mã  •  F9: thanh toán  •  + / -: đổi số lượng  •  Delete: xóa dòng";
            lblHint.TextAlign = ContentAlignment.MiddleLeft;

            // ================= KHU VỰC PHẢI =================
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Width = 400;
            pnlRight.Padding = new Padding(8, 14, 16, 14);
            pnlRight.Name = "pnlRight";
            pnlRight.Controls.Add(tblRight);

            tblRight.ColumnCount = 1;
            tblRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblRight.Dock = DockStyle.Fill;
            tblRight.Name = "tblRight";
            tblRight.RowCount = 9;
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));   // 0 Khách hàng
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));  // 1 Tổng tiền
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));   // 2 label tiền khách đưa
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));   // 3 ô nhập
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));   // 4 nút nhanh
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));  // 5 tiền thừa
            tblRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));   // 6 đệm
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));   // 7 thanh toán
            tblRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));   // 8 hủy
            tblRight.Controls.Add(groupBoxCustomer, 0, 0);
            tblRight.Controls.Add(pnlTotal, 0, 1);
            tblRight.Controls.Add(labelCashReceived, 0, 2);
            tblRight.Controls.Add(pnlCashLine, 0, 3);
            tblRight.Controls.Add(tblQuickCash, 0, 4);
            tblRight.Controls.Add(pnlChange, 0, 5);
            tblRight.Controls.Add(btnCheckout, 0, 7);
            tblRight.Controls.Add(btnClearCart, 0, 8);

            // Khách hàng
            groupBoxCustomer.BackColor = Color.White;
            groupBoxCustomer.Dock = DockStyle.Fill;
            groupBoxCustomer.Margin = new Padding(0, 0, 0, 10);
            groupBoxCustomer.Name = "groupBoxCustomer";
            groupBoxCustomer.Padding = new Padding(10, 4, 10, 4);
            groupBoxCustomer.Controls.Add(lblCustomerName);
            groupBoxCustomer.Controls.Add(txtCustomerPhone);
            groupBoxCustomer.Controls.Add(labelCustomerPhone);
            groupBoxCustomer.Controls.Add(labelCustomerTitle);

            labelCustomerTitle.AutoSize = false;
            labelCustomerTitle.Dock = DockStyle.Top;
            labelCustomerTitle.Height = 22;
            labelCustomerTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            labelCustomerTitle.ForeColor = textDark;
            labelCustomerTitle.Name = "labelCustomerTitle";
            labelCustomerTitle.Text = "Khách hàng";

            labelCustomerPhone.AutoSize = true;
            labelCustomerPhone.Font = new Font("Segoe UI", 9F);
            labelCustomerPhone.ForeColor = textGray;
            labelCustomerPhone.Location = new Point(10, 33);
            labelCustomerPhone.Name = "labelCustomerPhone";
            labelCustomerPhone.Text = "Số điện thoại:";

            txtCustomerPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomerPhone.BorderStyle = BorderStyle.FixedSingle;
            txtCustomerPhone.Font = new Font("Segoe UI", 10F);
            txtCustomerPhone.Location = new Point(100, 29);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT, nhấn Enter";
            txtCustomerPhone.Size = new Size(270, 25);
            txtCustomerPhone.TabIndex = 2;
            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;

            lblCustomerName.AutoSize = false;
            lblCustomerName.Dock = DockStyle.Bottom;
            lblCustomerName.Height = 26;
            lblCustomerName.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblCustomerName.ForeColor = textGray;
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Text = "Khách vãng lai";
            lblCustomerName.TextAlign = ContentAlignment.MiddleLeft;

            // Tổng tiền
            pnlTotal.BackColor = Color.White;
            pnlTotal.Dock = DockStyle.Fill;
            pnlTotal.Margin = new Padding(0, 0, 0, 10);
            pnlTotal.Name = "pnlTotal";
            pnlTotal.Padding = new Padding(14, 10, 14, 8);
            pnlTotal.Controls.Add(lblTotalAmount);
            pnlTotal.Controls.Add(labelTotal);

            labelTotal.AutoSize = false;
            labelTotal.Dock = DockStyle.Top;
            labelTotal.Height = 28;
            labelTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotal.ForeColor = textDark;
            labelTotal.Name = "labelTotal";
            labelTotal.Text = "TỔNG TIỀN CẦN TRẢ";

            lblTotalAmount.AutoSize = false;
            lblTotalAmount.Dock = DockStyle.Fill;
            lblTotalAmount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTotalAmount.ForeColor = green;
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;

            // Tiền khách đưa
            labelCashReceived.AutoSize = false;
            labelCashReceived.Dock = DockStyle.Fill;
            labelCashReceived.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCashReceived.ForeColor = textDark;
            labelCashReceived.Margin = new Padding(0);
            labelCashReceived.Name = "labelCashReceived";
            labelCashReceived.Text = "TIỀN KHÁCH ĐƯA";
            labelCashReceived.TextAlign = ContentAlignment.BottomLeft;

            pnlCashLine.BackColor = Color.FromArgb(190, 195, 205);
            pnlCashLine.Dock = DockStyle.Fill;
            pnlCashLine.Margin = new Padding(0, 4, 0, 6);
            pnlCashLine.Padding = new Padding(0, 0, 0, 1);
            pnlCashLine.Name = "pnlCashLine";
            pnlCashLine.Controls.Add(txtCashReceived);

            txtCashReceived.BackColor = Color.White;
            txtCashReceived.BorderStyle = BorderStyle.None;
            txtCashReceived.Dock = DockStyle.Fill;
            txtCashReceived.Font = new Font("Segoe UI", 14F);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.Text = "0";
            txtCashReceived.TextAlign = HorizontalAlignment.Right;
            txtCashReceived.TabIndex = 3;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;
            txtCashReceived.Enter += txtCashReceived_Enter;

            // Nút tiền nhanh
            tblQuickCash.ColumnCount = 5;
            for (int i = 0; i < 5; i++)
                tblQuickCash.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblQuickCash.Dock = DockStyle.Fill;
            tblQuickCash.Margin = new Padding(0, 0, 0, 8);
            tblQuickCash.Name = "tblQuickCash";
            tblQuickCash.RowCount = 1;
            tblQuickCash.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblQuickCash.Controls.Add(btnExact, 0, 0);
            tblQuickCash.Controls.Add(btn50k, 1, 0);
            tblQuickCash.Controls.Add(btn100k, 2, 0);
            tblQuickCash.Controls.Add(btn200k, 3, 0);
            tblQuickCash.Controls.Add(btn500k, 4, 0);

            StyleQuick(btnExact, "Vừa đủ", "btnExact", true, blue);
            StyleQuick(btn50k, "50k", "btn50k", false, blue);
            StyleQuick(btn100k, "100k", "btn100k", false, blue);
            StyleQuick(btn200k, "200k", "btn200k", false, blue);
            StyleQuick(btn500k, "500k", "btn500k", false, blue);
            btnExact.Click += btnQuickCash_Click;
            btn50k.Click += btnQuickCash_Click;
            btn100k.Click += btnQuickCash_Click;
            btn200k.Click += btnQuickCash_Click;
            btn500k.Click += btnQuickCash_Click;

            // Tiền thừa
            pnlChange.BackColor = Color.White;
            pnlChange.Dock = DockStyle.Fill;
            pnlChange.Margin = new Padding(0);
            pnlChange.Name = "pnlChange";
            pnlChange.Padding = new Padding(14, 10, 14, 8);
            pnlChange.Controls.Add(lblChange);
            pnlChange.Controls.Add(labelChange);

            labelChange.AutoSize = false;
            labelChange.Dock = DockStyle.Top;
            labelChange.Height = 26;
            labelChange.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelChange.ForeColor = textDark;
            labelChange.Name = "labelChange";
            labelChange.Text = "TIỀN THỪA TRẢ LẠI";

            lblChange.AutoSize = false;
            lblChange.Dock = DockStyle.Fill;
            lblChange.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblChange.ForeColor = Color.Black;
            lblChange.Name = "lblChange";
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;

            // THANH TOÁN / HỦY
            btnCheckout.BackColor = green;
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.Dock = DockStyle.Fill;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 145, 72);
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Margin = new Padding(0, 0, 0, 8);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.TabIndex = 4;
            btnCheckout.TabStop = false;
            btnCheckout.Text = "THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;

            btnClearCart.BackColor = Color.White;
            btnClearCart.Cursor = Cursors.Hand;
            btnClearCart.Dock = DockStyle.Fill;
            btnClearCart.FlatAppearance.BorderColor = red;
            btnClearCart.FlatAppearance.BorderSize = 1;
            btnClearCart.FlatAppearance.MouseOverBackColor = Color.FromArgb(253, 236, 237);
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnClearCart.ForeColor = red;
            btnClearCart.Margin = new Padding(0);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.TabIndex = 5;
            btnClearCart.TabStop = false;
            btnClearCart.Text = "HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;

            // ================= FORM =================
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(pnlLeft);    // Fill
            Controls.Add(pnlRight);   // Right
            Name = "FormPOS";
            Text = "Bán hàng - POS";
            Load += FormPOS_Load;

            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            pnlLeft.ResumeLayout(false);
            pnlScan.ResumeLayout(false);
            pnlBarcodeLine.ResumeLayout(false);
            pnlBarcodeLine.PerformLayout();
            pnlCartBar.ResumeLayout(false);
            flowCartButtons.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            tblRight.ResumeLayout(false);
            groupBoxCustomer.ResumeLayout(false);
            groupBoxCustomer.PerformLayout();
            pnlTotal.ResumeLayout(false);
            pnlCashLine.ResumeLayout(false);
            pnlCashLine.PerformLayout();
            tblQuickCash.ResumeLayout(false);
            pnlChange.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Nút viền (outline) dùng cho  -, +, Xóa dòng
        private static void StyleOutline(Button b, string text, Color fore, Color border, float fontSize)
        {
            b.BackColor = Color.White;
            b.Cursor = Cursors.Hand;
            b.FlatAppearance.BorderColor = border;
            b.FlatAppearance.BorderSize = 1;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            b.ForeColor = fore;
            b.Text = text;
            b.UseVisualStyleBackColor = false;
        }

        // Nút tiền nhanh: Vừa đủ / 50k / 100k ...
        private static void StyleQuick(Button b, string text, string name, bool primary, Color blue)
        {
            b.BackColor = Color.White;
            b.Cursor = Cursors.Hand;
            b.Dock = DockStyle.Fill;
            b.FlatAppearance.BorderColor = primary ? blue : Color.FromArgb(215, 218, 226);
            b.FlatAppearance.BorderSize = 1;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            b.ForeColor = primary ? blue : Color.FromArgb(35, 40, 55);
            b.Margin = new Padding(0, 0, 4, 0);
            b.Name = name;
            b.TabStop = false;
            b.Text = text;
            b.UseVisualStyleBackColor = false;
        }

        #endregion

        private Panel pnlLeft;
        private TableLayoutPanel pnlScan;
        private Panel pnlBarcodeLine;
        private Label labelBarcode;
        private TextBox txtBarcode;

        private DataGridView dgvCart;
        private DataGridViewTextBoxColumn colProductId;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colTotal;

        private Panel pnlCartBar;
        private Label lblCartStatus;
        private Label lblHint;
        private FlowLayoutPanel flowCartButtons;
        private Button btnMinus;
        private Button btnPlus;
        private Button btnDeleteRow;

        private Panel pnlRight;
        private TableLayoutPanel tblRight;
        private Panel groupBoxCustomer;
        private Label labelCustomerTitle;
        private Label labelCustomerPhone;
        private TextBox txtCustomerPhone;
        private Label lblCustomerName;

        private Panel pnlTotal;
        private Label labelTotal;
        private Label lblTotalAmount;

        private Label labelCashReceived;
        private Panel pnlCashLine;
        private TextBox txtCashReceived;
        private TableLayoutPanel tblQuickCash;
        private Button btnExact;
        private Button btn50k;
        private Button btn100k;
        private Button btn200k;
        private Button btn500k;

        private Panel pnlChange;
        private Label labelChange;
        private Label lblChange;

        private Button btnCheckout;
        private Button btnClearCart;
    }
}
