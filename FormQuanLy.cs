using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic; 
using System.Linq; 

namespace QuanLyPhongHoc
{
    public partial class FormQuanLy : Form
    {
        private readonly DAL.PhongHocDAL phongHocDAL = new DAL.PhongHocDAL();
        private readonly Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font cellFont = new Font("Segoe UI", 10);

        public FormQuanLy()
        {
            InitializeComponent();
        }

        private void FormQuanLy_Load_1(object sender, EventArgs e)
        {
            cboTrangThai.Items.AddRange(new object[] { "Trống", "Đang sử dụng" });
            cboTrangThai.SelectedIndex = 0;

            SetupDataGridView();
            LoadData();

            // Gắn sự kiện SelectionChanged để cập nhật khi người dùng click
            dgvPhongHoc.SelectionChanged += dgvPhongHoc_SelectionChanged;
        }

        private void SetupDataGridView()
        {
            dgvPhongHoc.AutoGenerateColumns = false;
            dgvPhongHoc.Columns.Clear();
            dgvPhongHoc.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", DataPropertyName = "ID", HeaderText = "ID", ReadOnly = true, Width = 50 });
            dgvPhongHoc.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenPhong", DataPropertyName = "TenPhong", HeaderText = "Tên Phòng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvPhongHoc.Columns.Add(new DataGridViewTextBoxColumn { Name = "SucChua", DataPropertyName = "SucChua", HeaderText = "Sức Chứa", Width = 80 });
            dgvPhongHoc.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", DataPropertyName = "TrangThai", HeaderText = "Trạng Thái" });
            dgvPhongHoc.Columns.Add(new DataGridViewTextBoxColumn { Name = "MoTa", DataPropertyName = "MoTa", HeaderText = "Mô Tả", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvPhongHoc.ColumnHeadersDefaultCellStyle.Font = headerFont;
            dgvPhongHoc.DefaultCellStyle.Font = cellFont;
        }


        public void LoadData()
        {
            // Tạm ngắt sự kiện để tránh lỗi lặp
            dgvPhongHoc.SelectionChanged -= dgvPhongHoc_SelectionChanged;

            // Lưu ID của phòng đang chọn
            object selectedId = dgvPhongHoc.CurrentRow?.Cells["ID"].Value;

            // Lấy dữ liệu mới và gán cho DataGridView
            var data = phongHocDAL.GetAll();
            dgvPhongHoc.DataSource = data;

            // Tìm và chọn lại dòng có ID đã lưu
            DataGridViewRow rowToSelect = null;
            if (selectedId != null)
            {
                rowToSelect = dgvPhongHoc.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["ID"].Value.Equals(selectedId));
            }

            // Nếu không tìm thấy hoặc không có gì được chọn, chọn dòng đầu tiên
            if (rowToSelect == null && dgvPhongHoc.Rows.Count > 0)
            {
                rowToSelect = dgvPhongHoc.Rows[0];
            }

            // Thực hiện việc chọn và cập nhật chi tiết
            if (rowToSelect != null)
            {
                rowToSelect.Selected = true;
                dgvPhongHoc.CurrentCell = rowToSelect.Cells[0];
                UpdateDetailsPanel(rowToSelect); // Cập nhật chi tiết ngay lập tức
            }
            else
            {
                ClearInputs(); // Nếu không có dữ liệu, xóa trắng các ô
            }

            // Gắn lại sự kiện
            dgvPhongHoc.SelectionChanged += dgvPhongHoc_SelectionChanged;
        }

        private void dgvPhongHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhongHoc.CurrentRow != null)
            {
                UpdateDetailsPanel(dgvPhongHoc.CurrentRow);
            }
        }

        private void UpdateDetailsPanel(DataGridViewRow row)
        {
            if (row == null) return;

            txtID.Text = row.Cells["ID"].Value?.ToString() ?? "";
            txtTenPhong.Text = row.Cells["TenPhong"].Value?.ToString() ?? "";
            txtSucChua.Text = row.Cells["SucChua"].Value?.ToString() ?? "";
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

            string trangThaiFromGrid = row.Cells["TrangThai"].Value?.ToString() ?? "Trống";
            cboTrangThai.SelectedItem = trangThaiFromGrid;
        }

        private void timerRefreshData_Tick(object sender, EventArgs e)
        {
            phongHocDAL.CapNhatTrangThaiTuDong();
            LoadData();
        }

        #region Other Events
        private void ClearInputs()
        {
            txtID.Text = "";
            txtTenPhong.Text = "";
            txtSucChua.Text = "";
            txtMoTa.Text = "";
            cboTrangThai.SelectedIndex = 0;
            txtTenPhong.Focus();
        }
        private void btnHuy_Click(object sender, EventArgs e) => ClearInputs();
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboTrangThai.SelectedItem == null) { MessageBox.Show("Vui lòng chọn trạng thái!"); return; }
            if (string.IsNullOrEmpty(txtTenPhong.Text.Trim())) { MessageBox.Show("Tên phòng không được để trống!"); return; }
            try
            {
                var phong = new DTO.PhongHoc { TenPhong = txtTenPhong.Text.Trim(), SucChua = int.Parse(txtSucChua.Text), TrangThai = cboTrangThai.SelectedItem.ToString(), MoTa = txtMoTa.Text.Trim() };
                if (phongHocDAL.Them(phong)) { MessageBox.Show("Thêm thành công!"); LoadData(); ClearInputs(); } else { MessageBox.Show("Thêm thất bại!"); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text)) { MessageBox.Show("Vui lòng chọn phòng để sửa!"); return; }
            try
            {
                var phong = new DTO.PhongHoc { ID = int.Parse(txtID.Text), TenPhong = txtTenPhong.Text.Trim(), SucChua = int.Parse(txtSucChua.Text), TrangThai = cboTrangThai.SelectedItem.ToString(), MoTa = txtMoTa.Text.Trim() };
                if (phongHocDAL.Sua(phong)) { MessageBox.Show("Sửa thành công!"); LoadData(); ClearInputs(); } else { MessageBox.Show("Sửa thất bại!"); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text)) { MessageBox.Show("Vui lòng chọn phòng để xóa!"); return; }
            if (MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (phongHocDAL.Xoa(int.Parse(txtID.Text))) { MessageBox.Show("Xóa thành công!"); LoadData(); ClearInputs(); } else { MessageBox.Show("Xóa thất bại!"); }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
        #endregion
    }
}