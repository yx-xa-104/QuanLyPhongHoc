using QuanLyPhongHoc.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class FormDangNhap : System.Windows.Forms.Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin đăng nhập
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DAL.NguoiDungDAL nguoiDungDAL = new DAL.NguoiDungDAL();
            DTO.NguoiDung user = nguoiDungDAL.KiemTraDangNhap(tenDangNhap, matKhau);

            if (user != null)
            {
                // Đăng nhập thành công, lưu thông tin user
                CurrentUserSession.User = user;

                // Mở MainForm và đóng form đăng nhập
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide(); // Ẩn form đăng nhập thay vì đóng
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
