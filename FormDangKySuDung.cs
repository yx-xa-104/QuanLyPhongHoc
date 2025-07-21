using QuanLyPhongHoc.DAL;
using QuanLyPhongHoc.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyPhongHoc
{
    public partial class FormDangKySuDung : Form
    {
        public FormDangKySuDung()
        {
            InitializeComponent();
        }

        private void FormDangKySuDung_Load(object sender, EventArgs e)
        {
            var danhSachPhong = PhongHocDAL.GetAll();

            // Gán danh sách này làm nguồn dữ liệu cho ComboBox
            cboPhongHoc.DataSource = danhSachPhong;

            // Cấu hình ComboBox để hiển thị tên phòng và giữ lại ID
            cboPhongHoc.DisplayMember = "TenPhong";
            cboPhongHoc.ValueMember = "ID";

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
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

            // Lấy thông tin từ giao diện
            int idPhong = (int)cboPhongHoc.SelectedValue;
            string mucDich = txtMucDich.Text.Trim();

            try
            {
                // Kiểm tra xem có bị trùng lịch hay không
                if (LichSuDungDAL.KiemTraTrungLich(idPhong, thoiGianBatDau, thoiGianKetThuc))
                {
                    MessageBox.Show("Phòng đã có người đăng ký trong khoảng thời gian này. Vui lòng chọn thời gian khác.", "Trùng lịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // Nếu không trùng lịch, tiến hành đăng ký
                LichSuDung lichDangKyMoi = new LichSuDung
                {
                    ID_Phong = idPhong,
                    ID_NguoiDung = CurrentUserSession.User.ID, // Lấy ID của người dùng đang đăng nhập
                    ThoiGianBatDau = thoiGianBatDau,
                    ThoiGianKetThuc = thoiGianKetThuc,
                    MucDich = mucDich
                };

                if (LichSuDungDAL.DangKy(lichDangKyMoi))
                {
                    MessageBox.Show("Đăng ký phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form sau khi đăng ký thành công
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
