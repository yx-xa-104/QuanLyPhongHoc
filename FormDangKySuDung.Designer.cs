namespace QuanLyPhongHoc
{
    partial class FormDangKySuDung
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
            cboPhongHoc = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            dtpBatDau = new DateTimePicker();
            label3 = new Label();
            dtpKetThuc = new DateTimePicker();
            label4 = new Label();
            txtMucDich = new TextBox();
            btnDangKy = new Button();
            SuspendLayout();
            // 
            // cboPhongHoc
            // 
            cboPhongHoc.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cboPhongHoc.FormattingEnabled = true;
            cboPhongHoc.Location = new Point(19, 39);
            cboPhongHoc.Name = "cboPhongHoc";
            cboPhongHoc.Size = new Size(200, 29);
            cboPhongHoc.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(19, 15);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 1;
            label1.Text = "Chọn phòng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(19, 83);
            label2.Name = "label2";
            label2.Size = new Size(136, 21);
            label2.TabIndex = 2;
            label2.Text = "Thời gian bắt đầu";
            // 
            // dtpBatDau
            // 
            dtpBatDau.CalendarFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpBatDau.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpBatDau.Format = DateTimePickerFormat.Custom;
            dtpBatDau.Location = new Point(19, 107);
            dtpBatDau.Name = "dtpBatDau";
            dtpBatDau.Size = new Size(200, 23);
            dtpBatDau.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(266, 83);
            label3.Name = "label3";
            label3.Size = new Size(140, 21);
            label3.TabIndex = 4;
            label3.Text = "Thời gian kết thúc";
            // 
            // dtpKetThuc
            // 
            dtpKetThuc.CalendarFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpKetThuc.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpKetThuc.Format = DateTimePickerFormat.Custom;
            dtpKetThuc.Location = new Point(266, 107);
            dtpKetThuc.Name = "dtpKetThuc";
            dtpKetThuc.Size = new Size(200, 23);
            dtpKetThuc.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(19, 151);
            label4.Name = "label4";
            label4.Size = new Size(140, 21);
            label4.TabIndex = 6;
            label4.Text = "Mục đích sử dụng";
            // 
            // txtMucDich
            // 
            txtMucDich.BorderStyle = BorderStyle.FixedSingle;
            txtMucDich.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMucDich.Location = new Point(19, 175);
            txtMucDich.Multiline = true;
            txtMucDich.Name = "txtMucDich";
            txtMucDich.ScrollBars = ScrollBars.Vertical;
            txtMucDich.Size = new Size(219, 119);
            txtMucDich.TabIndex = 7;
            // 
            // btnDangKy
            // 
            btnDangKy.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangKy.Location = new Point(195, 317);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(94, 47);
            btnDangKy.TabIndex = 8;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // FormDangKySuDung
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 393);
            Controls.Add(btnDangKy);
            Controls.Add(txtMucDich);
            Controls.Add(label4);
            Controls.Add(dtpKetThuc);
            Controls.Add(label3);
            Controls.Add(dtpBatDau);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cboPhongHoc);
            Name = "FormDangKySuDung";
            Text = "FormDangKySuDung";
            Load += FormDangKySuDung_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cboPhongHoc;
        private Label label1;
        private Label label2;
        private DateTimePicker dtpBatDau;
        private Label label3;
        private DateTimePicker dtpKetThuc;
        private Label label4;
        private TextBox txtMucDich;
        private Button btnDangKy;
    }
}