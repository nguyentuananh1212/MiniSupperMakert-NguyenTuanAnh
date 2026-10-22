namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
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
            var blue = Color.FromArgb(41, 100, 180);
            var orange = Color.FromArgb(240, 140, 30);
            var purple = Color.FromArgb(135, 80, 190);

            pnlTop = new Panel();
            lblDate = new Label();
            dateTimePicker1 = new DateTimePicker();
            btnRunReport = new Button();
            lblSubtitle = new Label();

            tblMain = new TableLayoutPanel();
            tblCards = new TableLayoutPanel();
            panel1 = new Panel(); lblTotalOrders = new Label(); label1 = new Label(); accent1 = new Panel();
            panel2 = new Panel(); lblTotalRevenue = new Label(); label3 = new Label(); accent2 = new Panel();
            panel4 = new Panel(); lblAvgOrder = new Label(); label7 = new Label(); accent4 = new Panel();
            panel3 = new Panel(); lblBestSeller = new Label(); label5 = new Label(); accent3 = new Panel();

            tblBottom = new TableLayoutPanel();
            pnlChart = new DoubleBufferedPanel();
            pnlTopProducts = new Panel();
            lblTopTitle = new Label();
            dgvTop = new DataGridView();
            colRank = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colRevenue = new DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)dgvTop).BeginInit();
            pnlTop.SuspendLayout();
            tblMain.SuspendLayout();
            tblCards.SuspendLayout();
            tblBottom.SuspendLayout();
            pnlTopProducts.SuspendLayout();
            SuspendLayout();

            // ===== Thanh trên =====
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(lblSubtitle);
            pnlTop.Controls.Add(btnRunReport);
            pnlTop.Controls.Add(dateTimePicker1);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 72;
            pnlTop.Name = "pnlTop";

            lblDate.AutoSize = false;
            lblDate.Dock = DockStyle.Left;
            lblDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(35, 40, 55);
            lblDate.Name = "lblDate";
            lblDate.Padding = new Padding(20, 0, 0, 0);
            lblDate.Text = "📅  Chọn ngày:";
            lblDate.TextAlign = ContentAlignment.MiddleLeft;
            lblDate.Width = 150;

            dateTimePicker1.Font = new Font("Segoe UI", 11F);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(160, 22);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(160, 27);
            dateTimePicker1.TabIndex = 0;

            btnRunReport.BackColor = green;
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(336, 17);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(170, 38);
            btnRunReport.TabIndex = 1;
            btnRunReport.Text = "📊  Xem báo cáo";
            btnRunReport.UseVisualStyleBackColor = false;

            lblSubtitle.AutoSize = false;
            lblSubtitle.Dock = DockStyle.Right;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblSubtitle.ForeColor = Color.FromArgb(100, 105, 120);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Padding = new Padding(0, 0, 20, 0);
            lblSubtitle.Text = "Siêu thị Mini Family Fresh";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            lblSubtitle.Width = 300;

            // ===== Bố cục chính: hàng thẻ KPI + hàng biểu đồ/top =====
            tblMain.ColumnCount = 1;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMain.Dock = DockStyle.Fill;
            tblMain.Name = "tblMain";
            tblMain.Padding = new Padding(12);
            tblMain.RowCount = 2;
            tblMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMain.Controls.Add(tblCards, 0, 0);
            tblMain.Controls.Add(tblBottom, 0, 1);

            tblCards.ColumnCount = 4;
            for (int i = 0; i < 4; i++)
                tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblCards.Dock = DockStyle.Fill;
            tblCards.Margin = new Padding(0);
            tblCards.Name = "tblCards";
            tblCards.RowCount = 1;
            tblCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblCards.Controls.Add(panel1, 0, 0);
            tblCards.Controls.Add(panel2, 1, 0);
            tblCards.Controls.Add(panel4, 2, 0);
            tblCards.Controls.Add(panel3, 3, 0);

            StyleCard(panel1, "panel1", accent1, "accent1", blue, lblTotalOrders, "lblTotalOrders", "0", label1, "label1", "🧾  TỔNG SỐ HÓA ĐƠN", 26F);
            StyleCard(panel2, "panel2", accent2, "accent2", green, lblTotalRevenue, "lblTotalRevenue", "0 đ", label3, "label3", "💰  TỔNG DOANH THU", 26F);
            StyleCard(panel4, "panel4", accent4, "accent4", orange, lblAvgOrder, "lblAvgOrder", "0 đ", label7, "label7", "🛍  TRUNG BÌNH / HÓA ĐƠN", 26F);
            StyleCard(panel3, "panel3", accent3, "accent3", purple, lblBestSeller, "lblBestSeller", "---", label5, "label5", "🏆  BÁN CHẠY NHẤT", 15F);

            tblBottom.ColumnCount = 2;
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblBottom.Dock = DockStyle.Fill;
            tblBottom.Margin = new Padding(0);
            tblBottom.Name = "tblBottom";
            tblBottom.RowCount = 1;
            tblBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblBottom.Controls.Add(pnlChart, 0, 0);
            tblBottom.Controls.Add(pnlTopProducts, 1, 0);

            // Biểu đồ cột 7 ngày (tự vẽ trong sự kiện Paint)
            pnlChart.BackColor = Color.White;
            pnlChart.Dock = DockStyle.Fill;
            pnlChart.Margin = new Padding(8, 8, 8, 0);
            pnlChart.Name = "pnlChart";
            pnlChart.Paint += pnlChart_Paint;
            pnlChart.Resize += (s, e) => pnlChart.Invalidate();

            // Top 5 sản phẩm
            pnlTopProducts.BackColor = Color.White;
            pnlTopProducts.Controls.Add(dgvTop);      // Fill: add trước
            pnlTopProducts.Controls.Add(lblTopTitle); // Top
            pnlTopProducts.Dock = DockStyle.Fill;
            pnlTopProducts.Margin = new Padding(8, 8, 0, 0);
            pnlTopProducts.Name = "pnlTopProducts";
            pnlTopProducts.Padding = new Padding(14, 0, 14, 12);

            lblTopTitle.AutoSize = false;
            lblTopTitle.Dock = DockStyle.Top;
            lblTopTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTopTitle.ForeColor = Color.FromArgb(35, 40, 55);
            lblTopTitle.Height = 48;
            lblTopTitle.Name = "lblTopTitle";
            lblTopTitle.Text = "Top 5 sản phẩm bán chạy trong ngày";
            lblTopTitle.TextAlign = ContentAlignment.MiddleLeft;

            dgvTop.AllowUserToAddRows = false;
            dgvTop.AllowUserToDeleteRows = false;
            dgvTop.AllowUserToResizeRows = false;
            dgvTop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTop.BackgroundColor = Color.White;
            dgvTop.BorderStyle = BorderStyle.None;
            dgvTop.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTop.GridColor = Color.FromArgb(232, 234, 240);
            dgvTop.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTop.ColumnHeadersHeight = 36;
            dgvTop.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTop.EnableHeadersVisualStyles = false;
            dgvTop.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = navy,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                SelectionBackColor = navy,
                SelectionForeColor = Color.White
            };
            dgvTop.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10F),
                SelectionBackColor = Color.FromArgb(220, 232, 250),
                SelectionForeColor = Color.FromArgb(35, 40, 55)
            };
            dgvTop.RowTemplate.Height = 36;
            dgvTop.RowHeadersVisible = false;
            dgvTop.ReadOnly = true;
            dgvTop.MultiSelect = false;
            dgvTop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTop.Dock = DockStyle.Fill;
            dgvTop.Name = "dgvTop";
            dgvTop.TabStop = false;
            dgvTop.Columns.AddRange(new DataGridViewColumn[] { colRank, colName, colQty, colRevenue });

            colRank.HeaderText = "#"; colRank.Name = "colRank"; colRank.Width = 40;
            colRank.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };
            colName.HeaderText = "Sản phẩm"; colName.Name = "colName";
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colQty.HeaderText = "SL"; colQty.Name = "colQty"; colQty.Width = 55;
            colQty.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };
            colRevenue.HeaderText = "Doanh thu"; colRevenue.Name = "colRevenue"; colRevenue.Width = 110;
            colRevenue.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };

            // ===== FORM =====
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(tblMain);   // Fill
            Controls.Add(pnlTop);    // Top
            Name = "FormQuickReport";
            Text = "Báo cáo doanh thu";

            ((System.ComponentModel.ISupportInitialize)dgvTop).EndInit();
            pnlTop.ResumeLayout(false);
            tblMain.ResumeLayout(false);
            tblCards.ResumeLayout(false);
            tblBottom.ResumeLayout(false);
            pnlTopProducts.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Thẻ KPI: nền trắng + dải màu bên trái + tiêu đề nhỏ + số lớn
        private static void StyleCard(Panel card, string name, Panel accent, string accentName, Color color,
            Label value, string valueName, string valueText,
            Label title, string titleName, string titleText, float valueSize)
        {
            card.BackColor = Color.White;
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(8, 8, 8, 8);
            card.Name = name;
            card.Padding = new Padding(0, 10, 12, 10);
            card.Controls.Add(value);   // Fill
            card.Controls.Add(title);   // Top
            card.Controls.Add(accent);  // Left

            accent.BackColor = color;
            accent.Dock = DockStyle.Left;
            accent.Name = accentName;
            accent.Width = 6;

            title.AutoSize = false;
            title.Dock = DockStyle.Top;
            title.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(100, 105, 120);
            title.Height = 28;
            title.Name = titleName;
            title.Padding = new Padding(16, 0, 0, 0);
            title.Text = titleText;
            title.TextAlign = ContentAlignment.MiddleLeft;

            value.AutoSize = false;
            value.Dock = DockStyle.Fill;
            value.Font = new Font("Segoe UI", valueSize, FontStyle.Bold);
            value.ForeColor = color;
            value.Name = valueName;
            value.Padding = new Padding(16, 0, 0, 0);
            value.Text = valueText;
            value.TextAlign = ContentAlignment.MiddleLeft;
        }

        #endregion

        private Panel pnlTop;
        private Label lblDate;
        private Label lblSubtitle;
        private DateTimePicker dateTimePicker1;
        private Button btnRunReport;

        private TableLayoutPanel tblMain;
        private TableLayoutPanel tblCards;
        private TableLayoutPanel tblBottom;

        private Panel panel1, panel2, panel3, panel4;
        private Panel accent1, accent2, accent3, accent4;
        private Label lblTotalOrders, lblTotalRevenue, lblAvgOrder, lblBestSeller;
        private Label label1, label3, label5, label7;

        private DoubleBufferedPanel pnlChart;
        private Panel pnlTopProducts;
        private Label lblTopTitle;
        private DataGridView dgvTop;
        private DataGridViewTextBoxColumn colRank;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colRevenue;
    }
}
