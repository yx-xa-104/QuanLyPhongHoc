using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class FormLichSu : Form
    {
        private DAL.LichSuDungDAL lichSuDungDAL = new DAL.LichSuDungDAL();
        public FormLichSu()
        {
            InitializeComponent();
        }

        private void FormLichSu_Load(object sender, EventArgs e)
        {
            try
            {
                dgvLichSu.DataSource = lichSuDungDAL.GetLichSuChiTiet();
                // Tự động điều chỉnh kích thước các cột
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}