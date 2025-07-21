using QuanLyPhongHoc.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class MainForm : Form
    {
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
            CurrentUserSession.User = null;
            Form formDangNhap = Application.OpenForms["FormDangNhap"];
            if (formDangNhap != null)
            {
                formDangNhap.Show();
            }
            else
            {
                FormDangNhap newLogin = new FormDangNhap();
                newLogin.Show();
            }
            this.Hide();
        }
        #endregion


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Hủy việc đóng form
                }
                else
                {
                    Application.Exit(); // Thoát ứng dụng
                }
            }
        }
    }
}