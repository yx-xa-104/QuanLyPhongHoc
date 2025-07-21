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
    public partial class FormQuanLy : Form
    {
        public FormQuanLy()
        {
            InitializeComponent();
        }

        private DAL.PhongHocDAL phongHocDAL = new DAL.PhongHocDAL();
        private void FormQuanLy_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dgvPhongHoc.DataSource = phongHocDAL.GetAll();
        }

        // Hiển thị thông tin phòng học khi click vào dòng trong DataGridView
        private void dgvPhongHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhongHoc.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
                txtSucChua.Text = row.Cells["SucChua"].Value.ToString();
                txtTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
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
                    SucChua = int.Parse(txtSucChua.Text), // Cần kiểm tra định dạng số
                    TrangThai = txtTrangThai.Text,
                    MoTa = txtMoTa.Text
                };

                if (phongHocDAL.Them(phong))
                {
                    MessageBox.Show("Thêm phòng học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // 3. Tải lại dữ liệu mới lên DataGridView
                    ClearInputs(); // 4. Xóa trắng các ô nhập liệu
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
            // Kiểm tra xem người dùng đã chọn phòng học nào chưa
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một phòng học để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra các trường thông tin cần thiết
            try
            {
                DTO.PhongHoc phong = new DTO.PhongHoc
                {
                    ID = int.Parse(txtID.Text),
                    TenPhong = txtTenPhong.Text.Trim(),
                    SucChua = int.Parse(txtSucChua.Text),
                    TrangThai = txtTrangThai.Text,
                    MoTa = txtMoTa.Text
                };

                // Kiểm tra tên phòng không được để trống
                if (phongHocDAL.Sua(phong))
                {
                    MessageBox.Show("Cập nhật phòng học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
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
            txtID.Text = "";
            txtTenPhong.Text = "";
            txtSucChua.Text = "";
            txtTrangThai.Text = "";
            txtMoTa.Text = "";
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

            // Hỏi xác nhận trước khi xóa
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
    }

}
