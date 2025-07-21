using QuanLyPhongHoc.DAL;
using QuanLyPhongHoc.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class MainForm : Form
    {
        private DAL.PhongHocDAL phongHocDAL = new DAL.PhongHocDAL();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (CurrentUserSession.User == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng hệ thống!");
                this.Close();
                return;
            }
            lblUserInfo.Text = $"Xin chào: {CurrentUserSession.User.HoTen} | Quyền: {CurrentUserSession.User.Quyen}";
            if (CurrentUserSession.User.Quyen == "Giáo viên")
            {
                quảnLýToolStripMenuItem.Enabled = false;
            }
            timerAutoUpdate.Start();
        }

        private void timerAutoUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                // Gọi phương thức tự động cập nhật CSDL
                phongHocDAL.CapNhatTrangThaiTuDong();

                // Kiểm tra xem FormQuanLy có đang mở không để làm mới dữ liệu
                Form formQuanLy = Application.OpenForms["FormQuanLy"];
                if (formQuanLy != null)
                {
                    (formQuanLy as FormQuanLy)?.LoadData();
                }
            }
            catch (Exception ex)
            {
                timerAutoUpdate.Stop();
                MessageBox.Show("Lỗi tự động cập nhật trạng thái: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Button Click Events       

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQuanLy formQuanLy = new FormQuanLy();
            formQuanLy.ShowDialog();
        }

        private void đăngKýSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangKySuDung formDangKy = new FormDangKySuDung();
            formDangKy.ShowDialog();
        }

        private void lịchSửSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLichSu formLichSu = new FormLichSu();
            formLichSu.ShowDialog();
        }
        private void đăngXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Xóa thông tin phiên đăng nhập hiện tại
            CurrentUserSession.User = null;

            // Tìm form đăng nhập đã bị ẩn
            Form formDangNhap = Application.OpenForms["FormDangNhap"];
            if (formDangNhap != null)
            {
                // Hiển thị lại form đăng nhập
                formDangNhap.Show();
            }
            else // Trường hợp dự phòng nếu form đăng nhập đã bị đóng
            {
                FormDangNhap newLogin = new FormDangNhap();
                newLogin.Show();
            }

            // Ẩn form chính đi
            this.Hide();
        }
        #endregion


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}