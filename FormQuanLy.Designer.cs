namespace QuanLyPhongHoc
{
    partial class FormQuanLy
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
            groupBox1 = new GroupBox();
            dgvPhongHoc = new DataGridView();
            groupBox2 = new GroupBox();
            cboTrangThai = new ComboBox();
            btnHuy = new Button();
            btnXoa = new Button();
            btnSua = new Button();
            btnThem = new Button();
            txtMoTa = new TextBox();
            txtSucChua = new TextBox();
            txtTenPhong = new TextBox();
            txtID = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPhongHoc).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvPhongHoc);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(751, 426);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Danh sách phòng học";
            // 
            // dgvPhongHoc
            // 
            dgvPhongHoc.AllowUserToAddRows = false;
            dgvPhongHoc.AllowUserToDeleteRows = false;
            dgvPhongHoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhongHoc.Dock = DockStyle.Fill;
            dgvPhongHoc.Location = new Point(3, 29);
            dgvPhongHoc.Name = "dgvPhongHoc";
            dgvPhongHoc.ReadOnly = true;
            dgvPhongHoc.Size = new Size(745, 394);
            dgvPhongHoc.TabIndex = 0;
            dgvPhongHoc.CellClick += dgvPhongHoc_CellClick_1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cboTrangThai);
            groupBox2.Controls.Add(btnHuy);
            groupBox2.Controls.Add(btnXoa);
            groupBox2.Controls.Add(btnSua);
            groupBox2.Controls.Add(btnThem);
            groupBox2.Controls.Add(txtMoTa);
            groupBox2.Controls.Add(txtSucChua);
            groupBox2.Controls.Add(txtTenPhong);
            groupBox2.Controls.Add(txtID);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(769, 15);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(370, 423);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông tin chi tiết";
            // 
            // cboTrangThai
            // 
            cboTrangThai.BackColor = SystemColors.Control;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(6, 221);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(358, 29);
            cboTrangThai.TabIndex = 14;
            // 
            // btnHuy
            // 
            btnHuy.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHuy.Location = new Point(280, 365);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(87, 35);
            btnHuy.TabIndex = 13;
            btnHuy.Text = "Huỷ";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Firebrick;
            btnXoa.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXoa.Location = new Point(188, 365);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(87, 35);
            btnXoa.TabIndex = 12;
            btnXoa.Text = "Xoá";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.DodgerBlue;
            btnSua.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSua.Location = new Point(96, 365);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(87, 35);
            btnSua.TabIndex = 11;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.LimeGreen;
            btnThem.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnThem.Location = new Point(4, 365);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(87, 35);
            btnThem.TabIndex = 10;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // txtMoTa
            // 
            txtMoTa.BackColor = SystemColors.Control;
            txtMoTa.BorderStyle = BorderStyle.FixedSingle;
            txtMoTa.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMoTa.Location = new Point(6, 277);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(358, 77);
            txtMoTa.TabIndex = 9;
            // 
            // txtSucChua
            // 
            txtSucChua.BackColor = SystemColors.Control;
            txtSucChua.BorderStyle = BorderStyle.FixedSingle;
            txtSucChua.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSucChua.Location = new Point(6, 165);
            txtSucChua.Name = "txtSucChua";
            txtSucChua.Size = new Size(358, 29);
            txtSucChua.TabIndex = 7;
            // 
            // txtTenPhong
            // 
            txtTenPhong.BackColor = SystemColors.Control;
            txtTenPhong.BorderStyle = BorderStyle.FixedSingle;
            txtTenPhong.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTenPhong.Location = new Point(6, 109);
            txtTenPhong.Name = "txtTenPhong";
            txtTenPhong.Size = new Size(358, 29);
            txtTenPhong.TabIndex = 6;
            // 
            // txtID
            // 
            txtID.BorderStyle = BorderStyle.FixedSingle;
            txtID.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtID.Location = new Point(6, 53);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(358, 29);
            txtID.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(6, 253);
            label5.Name = "label5";
            label5.Size = new Size(54, 21);
            label5.TabIndex = 4;
            label5.Text = "Mô tả";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 197);
            label4.Name = "label4";
            label4.Size = new Size(87, 21);
            label4.TabIndex = 3;
            label4.Text = "Trạng thái";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 141);
            label3.Name = "label3";
            label3.Size = new Size(80, 21);
            label3.TabIndex = 2;
            label3.Text = "Sức chứa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(0, 85);
            label2.Name = "label2";
            label2.Size = new Size(91, 21);
            label2.TabIndex = 1;
            label2.Text = "Tên phòng";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 29);
            label1.Name = "label1";
            label1.Size = new Size(27, 21);
            label1.TabIndex = 0;
            label1.Text = "ID";
            // 
            // FormQuanLy
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormQuanLy";
            Text = "FormQuanLy";
            Load += FormQuanLy_Load_1;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPhongHoc).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dgvPhongHoc;
        private GroupBox groupBox2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtMoTa;
        private TextBox txtSucChua;
        private TextBox txtTenPhong;
        private TextBox txtID;
        private Button btnHuy;
        private Button btnXoa;
        private Button btnSua;
        private Button btnThem;
        private ComboBox cboTrangThai;
    }
}