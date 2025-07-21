using QuanLyPhongHoc.DAL;
using QuanLyPhongHoc.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class FormDangKySuDung : Form
    {
        // Đối tượng DAL để tương tác với cơ sở dữ liệu
        private PhongHocDAL phongHocDAL = new PhongHocDAL();
        private LichSuDungDAL lichSuDungDAL = new LichSuDungDAL();

        public FormDangKySuDung()
        {
            InitializeComponent();
        }

        private void FormDangKySuDung_Load(object sender, EventArgs e)
        {
            // Nạp danh sách phòng học vào ComboBox khi form được tải
            var danhSachPhong = phongHocDAL.GetAll();
            cboPhongHoc.DataSource = danhSachPhong;
            cboPhongHoc.DisplayMember = "TenPhong";
            cboPhongHoc.ValueMember = "ID";
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (cboPhongHoc.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng học.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime thoiGianBatDau = dtpBatDau.Value;
            DateTime thoiGianKetThuc = dtpKetThuc.Value;

            if (thoiGianKetThuc <= thoiGianBatDau)
            {
                MessageBox.Show("Thời gian kết thúc phải sau thời gian bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idPhong = (int)cboPhongHoc.SelectedValue;
            string mucDich = txtMucDich.Text.Trim();

            try
            {
                // Kiểm tra xem phòng đã có người đăng ký trong khoảng thời gian này chưa
                if (lichSuDungDAL.KiemTraTrungLich(idPhong, thoiGianBatDau, thoiGianKetThuc))
                {
                    MessageBox.Show("Phòng đã có người đăng ký trong khoảng thời gian này. Vui lòng chọn thời gian khác.", "Trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                LichSuDung lichDangKyMoi = new LichSuDung
                {
                    ID_Phong = idPhong,
                    ID_NguoiDung = CurrentUserSession.User.ID,
                    ThoiGianBatDau = thoiGianBatDau,
                    ThoiGianKetThuc = thoiGianKetThuc,
                    MucDich = mucDich
                };

                // Thực hiện đăng ký phòng
                if (lichSuDungDAL.DangKy(lichDangKyMoi))
                {
                    MessageBox.Show("Đăng ký phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký phòng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}