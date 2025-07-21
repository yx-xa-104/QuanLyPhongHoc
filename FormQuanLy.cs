using System;
using System.Windows.Forms;
using System.Drawing;

namespace QuanLyPhongHoc
{
    public partial class FormQuanLy : Form
    {
        private DAL.PhongHocDAL phongHocDAL = new DAL.PhongHocDAL();

        public FormQuanLy()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            dgvPhongHoc.DataSource = phongHocDAL.GetAll();
            dgvPhongHoc.ReadOnly = true;
            dgvPhongHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            if (cboTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái cho phòng học!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenPhong = txtTenPhong.Text.Trim();
            if (string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Tên phòng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DTO.PhongHoc phong = new DTO.PhongHoc
                {
                    TenPhong = tenPhong,
                    SucChua = int.Parse(txtSucChua.Text),
                    TrangThai = cboTrangThai.SelectedItem.ToString(),
                    MoTa = txtMoTa.Text
                };

                if (phongHocDAL.Them(phong))
                {
                    MessageBox.Show("Thêm phòng học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm phòng học thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Sức chứa phải là một con số!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn phòng nào để sửa chưa (dựa vào txtID)
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một phòng học từ danh sách để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đối tượng PhongHoc từ thông tin ĐÃ ĐƯỢC SỬA trong các ô
                DTO.PhongHoc phong = new DTO.PhongHoc
                {
                    ID = int.Parse(txtID.Text),
                    TenPhong = txtTenPhong.Text.Trim(),
                    SucChua = int.Parse(txtSucChua.Text),
                    TrangThai = cboTrangThai.SelectedItem.ToString(),
                    MoTa = txtMoTa.Text
                };

                // Gọi DAL để cập nhật vào CSDL
                if (phongHocDAL.Sua(phong))
                {
                    MessageBox.Show("Cập nhật phòng học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Tải lại danh sách để thấy thay đổi
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật phòng học thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Sức chứa phải là một con số!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            txtID.Text = ""; // ID sẽ trống khi thêm mới
            txtTenPhong.Text = "";
            txtSucChua.Text = "";
            txtMoTa.Text = "";
            cboTrangThai.SelectedIndex = 0; // Reset về trạng thái "Trống"
            txtTenPhong.Focus(); // Di chuyển con trỏ vào ô Tên phòng
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một phòng học để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng học này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtID.Text);
                    if (phongHocDAL.Xoa(id))
                    {
                        MessageBox.Show("Xóa phòng học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa phòng học thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi. Có thể phòng này đã được đăng ký sử dụng và không thể xóa.\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormQuanLy_Load_1(object sender, EventArgs e)
        {
            // Thêm các mục vào ComboBox Trạng thái
            cboTrangThai.Items.Add("Trống");
            cboTrangThai.Items.Add("Đang sử dụng");
            cboTrangThai.Items.Add("Đã đặt trước");
            cboTrangThai.SelectedIndex = 0; // Mặc định chọn mục đầu tiên

            dgvPhongHoc.DataBindingComplete += dgvPhongHoc_DataBindingComplete;

            LoadData();
        }

        private void dgvPhongHoc_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhongHoc.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
                txtSucChua.Text = row.Cells["SucChua"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();

                string trangThaiFromGrid = row.Cells["TrangThai"].Value.ToString();
                int index = cboTrangThai.FindStringExact(trangThaiFromGrid);
                if (index != -1)
                {
                    cboTrangThai.SelectedIndex = index;
                }
                else
                {
                    cboTrangThai.SelectedIndex = 0; // Nếu không tìm thấy, chọn giá trị mặc định
                }
            }
        }

        private void timerRefreshData_Tick(object sender, EventArgs e)
        {
            //.Yêu cầu CSDL tự cập nhật trạng thái
            phongHocDAL.CapNhatTrangThaiTuDong();
            // Tải lại dữ liệu mới nhất lên lưới
            LoadData();
        }

        private void dgvPhongHoc_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Thiết lập font chữ cho tiêu đề và toàn bộ các ô
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font cellFont = new Font("Segoe UI", 10);

            dgvPhongHoc.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dgvPhongHoc.DefaultCellStyle.Font = cellFont;
        }
    }
}