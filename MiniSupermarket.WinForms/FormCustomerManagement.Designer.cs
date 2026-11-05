namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.panelPagination = new System.Windows.Forms.Panel();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.cboPageSize = new System.Windows.Forms.ComboBox();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.lblSort = new System.Windows.Forms.Label();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblPoints = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.panelPagination.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPagination
            // 
            this.panelPagination.BackColor = System.Drawing.Color.FromArgb(248, 249, 251);
            this.panelPagination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPagination.Controls.Add(this.lblPageSize);
            this.panelPagination.Controls.Add(this.cboPageSize);
            this.panelPagination.Controls.Add(this.lblTotalRecords);
            this.panelPagination.Controls.Add(this.lblSort);
            this.panelPagination.Controls.Add(this.cboSort);
            this.panelPagination.Controls.Add(this.btnFirstPage);
            this.panelPagination.Controls.Add(this.btnPrevPage);
            this.panelPagination.Controls.Add(this.lblPageInfo);
            this.panelPagination.Controls.Add(this.btnNextPage);
            this.panelPagination.Controls.Add(this.btnLastPage);
            this.panelPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPagination.Location = new System.Drawing.Point(0, 495);
            this.panelPagination.Name = "panelPagination";
            this.panelPagination.Size = new System.Drawing.Size(640, 45);
            this.panelPagination.TabIndex = 3;
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPageSize.ForeColor = System.Drawing.Color.FromArgb(70, 75, 85);
            this.lblPageSize.Location = new System.Drawing.Point(10, 14);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(51, 15);
            this.lblPageSize.TabIndex = 0;
            this.lblPageSize.Text = "Hiển thị:";
            // 
            // cboPageSize
            // 
            this.cboPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboPageSize.FormattingEnabled = true;
            this.cboPageSize.Items.AddRange(new object[] { "5", "10", "20", "50" });
            this.cboPageSize.Location = new System.Drawing.Point(64, 10);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.Size = new System.Drawing.Size(50, 23);
            this.cboPageSize.TabIndex = 1;
            this.cboPageSize.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_SelectedIndexChanged);
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalRecords.ForeColor = System.Drawing.Color.FromArgb(70, 75, 85);
            this.lblTotalRecords.Location = new System.Drawing.Point(118, 14);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(78, 15);
            this.lblTotalRecords.TabIndex = 2;
            this.lblTotalRecords.Text = "| Tổng số: 0 KH";
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSort.ForeColor = System.Drawing.Color.FromArgb(70, 75, 85);
            this.lblSort.Location = new System.Drawing.Point(210, 14);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(52, 15);
            this.lblSort.TabIndex = 3;
            this.lblSort.Text = "Sắp xếp:";
            // 
            // cboSort
            // 
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSort.FormattingEnabled = true;
            this.cboSort.Items.AddRange(new object[] {
                "Mặc định (Mã KH)",
                "Điểm cao nhất (VIP)",
                "Điểm thấp nhất",
                "Tên A - Z"
            });
            this.cboSort.Location = new System.Drawing.Point(265, 10);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(120, 23);
            this.cboSort.TabIndex = 4;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirstPage.BackColor = System.Drawing.Color.White;
            this.btnFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstPage.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnFirstPage.Location = new System.Drawing.Point(395, 8);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(34, 27);
            this.btnFirstPage.TabIndex = 5;
            this.btnFirstPage.Text = "⏮";
            this.btnFirstPage.UseVisualStyleBackColor = false;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevPage.BackColor = System.Drawing.Color.White;
            this.btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnPrevPage.Location = new System.Drawing.Point(433, 8);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(34, 27);
            this.btnPrevPage.TabIndex = 6;
            this.btnPrevPage.Text = "◀";
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(41, 100, 180);
            this.lblPageInfo.Location = new System.Drawing.Point(471, 12);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(86, 20);
            this.lblPageInfo.TabIndex = 7;
            this.lblPageInfo.Text = "Trang 1 / 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.BackColor = System.Drawing.Color.White;
            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.Location = new System.Drawing.Point(561, 8);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(34, 27);
            this.btnNextPage.TabIndex = 8;
            this.btnNextPage.Text = "▶";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastPage.BackColor = System.Drawing.Color.White;
            this.btnLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLastPage.Location = new System.Drawing.Point(599, 8);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(34, 27);
            this.btnLastPage.TabIndex = 9;
            this.btnLastPage.Text = "⏭";
            this.btnLastPage.UseVisualStyleBackColor = false;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.Location = new System.Drawing.Point(0, 60);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(640, 435);
            this.dgvCustomers.TabIndex = 0;
            this.dgvCustomers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomers_CellClick);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.btnReload);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 2;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(15, 21);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(100, 15);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tên hoặc SĐT:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(120, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(260, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(395, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(505, 14);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(100, 30);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = System.Drawing.Color.White;
            this.panelDetail.Controls.Add(this.lblId);
            this.panelDetail.Controls.Add(this.txtId);
            this.panelDetail.Controls.Add(this.lblName);
            this.panelDetail.Controls.Add(this.txtName);
            this.panelDetail.Controls.Add(this.lblPhone);
            this.panelDetail.Controls.Add(this.txtPhone);
            this.panelDetail.Controls.Add(this.lblAddress);
            this.panelDetail.Controls.Add(this.txtAddress);
            this.panelDetail.Controls.Add(this.lblPoints);
            this.panelDetail.Controls.Add(this.txtPoints);
            this.panelDetail.Controls.Add(this.btnAdd);
            this.panelDetail.Controls.Add(this.btnUpdate);
            this.panelDetail.Controls.Add(this.btnDelete);
            this.panelDetail.Controls.Add(this.btnClear);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDetail.Location = new System.Drawing.Point(640, 60);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(360, 440);
            this.panelDetail.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(17, 10);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(100, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Mã khách hàng";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(17, 30);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(325, 23);
            this.txtId.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 65);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Họ và tên khách hàng";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(17, 85);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(325, 23);
            this.txtName.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(17, 120);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(100, 15);
            this.lblPhone.TabIndex = 0;
            this.lblPhone.Text = "Số điện thoại";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(17, 140);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(325, 23);
            this.txtPhone.TabIndex = 1;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(17, 175);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(100, 15);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Địa chỉ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(17, 195);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(325, 23);
            this.txtAddress.TabIndex = 1;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(17, 230);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(100, 15);
            this.lblPoints.TabIndex = 0;
            this.lblPoints.Text = "Điểm tích lũy / Hạng thẻ";
            // 
            // txtPoints
            // 
            this.txtPoints.Location = new System.Drawing.Point(17, 250);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(325, 23);
            this.txtPoints.TabIndex = 1;
            this.txtId.ReadOnly = true;
            this.txtPoints.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(17, 285);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(325, 36);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Thêm khách hàng";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(17, 327);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(325, 36);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(17, 369);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(325, 36);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Xóa khách hàng";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(17, 411);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(325, 36);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Nhập mới / Bỏ chọn";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FormCustomerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 540);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.panelPagination);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelTop);
            this.Name = "FormCustomerManagement";
            this.Text = "Quản lý khách hàng";
            this.Load += new System.EventHandler(this.FormCustomerManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            this.panelPagination.ResumeLayout(false);
            this.panelPagination.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelPagination;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.ComboBox cboPageSize;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnLastPage;
    }
}
