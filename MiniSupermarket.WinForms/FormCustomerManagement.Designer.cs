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
            this.SuspendLayout();
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.Location = new System.Drawing.Point(0, 60);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(640, 440);
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
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelTop);
            this.Name = "FormCustomerManagement";
            this.Text = "FormCustomerManagement";
            this.Load += new System.EventHandler(this.FormCustomerManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
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
    }
}
