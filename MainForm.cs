using QuanLyPhongHoc.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                // Nếu chưa đăng nhập mà mở form này thì đóng lại
                MessageBox.Show("Vui lòng đăng nhập để sử dụng hệ thống!");
                this.Close();
                return;
            }

            // Hiển thị thông tin người dùng
            lblUserInfo.Text = $"Xin chào: {CurrentUserSession.User.HoTen} | Quyền: {CurrentUserSession.User.Quyen}";

            // Phân quyền chức năng trên MenuStrip
            if (CurrentUserSession.User.Quyen == "Giáo viên")
            {
                quảnLýToolStripMenuItem.Enabled = false; // Vô hiệu hóa menu Quản lý
            }
            // Admin thì mặc định được dùng tất cả
        }

        // Code cho menu Đăng xuất
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentUserSession.User = null; // Xóa session
                                            // Hiển thị lại form đăng nhập
                                            // Cần tìm form đăng nhập đã ẩn và show lại
            Application.OpenForms["FormDangNhap"].Show();
            this.Close();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblUserInfo_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

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
            // Đóng form hiện tại
            this.Close();

            // Hiển thị lại form đăng nhập
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
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Khi MainForm đóng, nếu người dùng không chủ động đăng xuất thì hiển thị lại form đăng nhập.
            Form formDangNhap = Application.OpenForms["FormDangNhap"];
            if (formDangNhap != null && formDangNhap.Visible == false)
            {
                formDangNhap.Show();
            }
        }
    }
}
