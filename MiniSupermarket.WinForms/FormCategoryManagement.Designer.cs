namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            var blue = Color.FromArgb(41, 100, 180);
            var green = Color.FromArgb(34, 170, 84);
            var red = Color.FromArgb(200, 40, 50);

            groupBox1 = new GroupBox();
            btnLoad = new Button();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            groupBox2 = new GroupBox();
            dgvCategories = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            groupBox3 = new GroupBox();
            txtDescription = new TextBox();
            txtCategoryName = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtId = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            pnlButtons = new FlowLayoutPanel();
            pnlRight = new Panel();

            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            groupBox3.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlRight.SuspendLayout();
            SuspendLayout();

            // ===== groupBox1: Tìm kiếm (Dock Top) =====
            groupBox1.Controls.Add(txtKeyword);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(btnLoad);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.5F);
            groupBox1.Height = 70;
            groupBox1.Margin = new Padding(0);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(12, 6, 12, 8);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm";

            // Nút bên phải dùng Dock Right (add trước = dock sau cùng) -> ô nhập tự giãn Fill
            txtKeyword.Dock = DockStyle.Fill;
            txtKeyword.Font = new Font("Segoe UI", 11F);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập từ khóa...";
            txtKeyword.TabIndex = 0;

            btnSearch.Dock = DockStyle.Right;
            btnSearch.Name = "btnSearch";
            btnSearch.Width = 110;
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            btnLoad.Dock = DockStyle.Right;
            btnLoad.Name = "btnLoad";
            btnLoad.Width = 110;
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // ===== pnlRight: chi tiết nhóm hàng + nút (Dock Right) =====
            pnlRight.Controls.Add(groupBox3);   // Fill
            pnlRight.Controls.Add(pnlButtons);  // Bottom
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(8, 0, 0, 0);
            pnlRight.Width = 380;

            groupBox3.Controls.Add(txtDescription);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(txtCategoryName);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(txtId);
            groupBox3.Controls.Add(label1);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Font = new Font("Segoe UI", 9.5F);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(14, 10, 14, 10);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thông tin Nhóm hàng";

            // Các control trong groupBox3 xếp bằng Dock Top (add ngược: dưới cùng add trước)
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("Segoe UI", 10.5F);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.TabIndex = 2;

            label3.AutoSize = false;
            label3.Dock = DockStyle.Top;
            label3.Height = 26;
            label3.Name = "label3";
            label3.Text = "Mô tả";
            label3.TextAlign = ContentAlignment.BottomLeft;

            txtCategoryName.Dock = DockStyle.Top;
            txtCategoryName.Font = new Font("Segoe UI", 10.5F);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.TabIndex = 1;

            label2.AutoSize = false;
            label2.Dock = DockStyle.Top;
            label2.Height = 30;
            label2.Name = "label2";
            label2.Text = "Tên Nhóm hàng";
            label2.TextAlign = ContentAlignment.BottomLeft;

            txtId.Dock = DockStyle.Top;
            txtId.Font = new Font("Segoe UI", 10.5F);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.TabIndex = 0;
            txtId.TextChanged += txtId_TextChanged;

            label1.AutoSize = false;
            label1.Dock = DockStyle.Top;
            label1.Height = 24;
            label1.Name = "label1";
            label1.Text = "Mã ID";
            label1.TextAlign = ContentAlignment.BottomLeft;

            // Nút thao tác
            pnlButtons.Controls.Add(btnAdd);
            pnlButtons.Controls.Add(btnUpdate);
            pnlButtons.Controls.Add(btnDelete);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 64;
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(0, 10, 0, 0);

            StyleAction(btnAdd, "Thêm mới", "btnAdd", green, 3);
            btnAdd.Click += btnAdd_Click;
            StyleAction(btnUpdate, "Cập nhật", "btnUpdate", blue, 4);
            btnUpdate.Click += btnUpdate_Click;
            StyleAction(btnDelete, "Xóa", "btnDelete", red, 5);
            btnDelete.Click += btnDelete_Click;

            // ===== groupBox2: Danh sách (Fill) =====
            groupBox2.Controls.Add(dgvCategories);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 9.5F);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(8, 6, 8, 8);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Danh sách Nhóm hàng";

            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.ColumnHeadersHeight = 38;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = navy,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                SelectionBackColor = navy,
                SelectionForeColor = Color.White
            };
            dgvCategories.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 10.5F),
                SelectionBackColor = Color.FromArgb(220, 232, 250),
                SelectionForeColor = Color.FromArgb(35, 40, 55)
            };
            dgvCategories.RowTemplate.Height = 34;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            dgvCategories.CellContentClick += dgvCategories_CellContentClick;

            dataGridViewTextBoxColumn1.HeaderText = "Mã ID";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn2.HeaderText = "Tên Nhóm hàng";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn3.HeaderText = "Mô tả";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;

            // ===== FORM =====
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(groupBox2);   // Fill (add đầu tiên)
            Controls.Add(pnlRight);    // Right
            Controls.Add(groupBox1);   // Top
            Name = "FormCategoryManagement";
            Padding = new Padding(16, 12, 16, 16);
            Text = "Quản lý Danh mục Nhóm hàng";
            Load += FormCategoryManagement_Load;

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            pnlButtons.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void StyleAction(Button b, string text, string name, Color back, int tab)
        {
            b.BackColor = back;
            b.Cursor = Cursors.Hand;
            b.FlatAppearance.BorderSize = 0;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            b.ForeColor = Color.White;
            b.Margin = new Padding(0, 0, 8, 0);
            b.Name = name;
            b.Size = new Size(112, 40);
            b.TabIndex = tab;
            b.Text = text;
            b.UseVisualStyleBackColor = false;
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private FlowLayoutPanel pnlButtons;
        private Panel pnlRight;

        private Button btnSearch;
        private Button btnLoad;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private TextBox txtKeyword;
        private TextBox txtId;
        private TextBox txtCategoryName;
        private TextBox txtDescription;

        private Label label1;
        private Label label2;
        private Label label3;

        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}
