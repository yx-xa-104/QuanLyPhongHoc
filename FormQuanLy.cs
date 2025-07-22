using System;
using System.Windows.Forms;
using System.Drawing;

namespace QuanLyPhongHoc
{
    public partial class FormQuanLy : Form
    {
        private DAL.PhongHocDAL phongHocDAL = new DAL.PhongHocDAL();
        private readonly Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font cellFont = new Font("Segoe UI", 10);

        private bool isDataLoading = false;

        public FormQuanLy()
        {
            InitializeComponent();
        }

        // Tải dữ liệu từ CSDL và hiển thị lên DataGridView
        public void LoadData()
        {
            if (isDataLoading) return;
            isDataLoading = true;

            // Lưu ID của phòng đang chọn
            object selectedId = dgvPhongHoc.CurrentRow?.Cells["ID"].Value;

            // Tạm ngắt sự kiện để tránh lỗi lặp vô tận
            dgvPhongHoc.SelectionChanged -= dgvPhongHoc_SelectionChanged;

            dgvPhongHoc.DataSource = phongHocDAL.GetAll();

            // Tìm và chọn lại dòng có ID đã lưu
            if (selectedId != null)
            {
                foreach (DataGridViewRow row in dgvPhongHoc.Rows)
                {
                    if (row.Cells["ID"].Value.Equals(selectedId))
                    {
                        row.Selected = true;
                        dgvPhongHoc.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }

            // Gắn lại sự kiện
            dgvPhongHoc.SelectionChanged += dgvPhongHoc_SelectionChanged;

            if (dgvPhongHoc.CurrentRow != null)
            {
                UpdateDetailsPanel(dgvPhongHoc.CurrentRow);
            }

            isDataLoading = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
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
        private void ClearInputs()
        {
            txtID.Text = "";
            txtTenPhong.Text = "";
            txtSucChua.Text = "";
            txtMoTa.Text = "";
            cboTrangThai.SelectedIndex = 0;
            txtTenPhong.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
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

        private void FormQuanLy_Load_1(object sender, EventArgs e)
        {
            cboTrangThai.Items.AddRange(new object[] { "Trống", "Đang sử dụng" });
            cboTrangThai.SelectedIndex = 0;

            SetupDataGridView();

            dgvPhongHoc.CellClick -= dgvPhongHoc_CellClick_1;
            dgvPhongHoc.SelectionChanged += dgvPhongHoc_SelectionChanged;
            LoadData();
        }

        private void dgvPhongHoc_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timerRefreshData_Tick(object sender, EventArgs e)
        {
            //.Yêu cầu CSDL tự cập nhật trạng thái
            phongHocDAL.CapNhatTrangThaiTuDong();
            // Tải lại dữ liệu mới nhất lên lưới
            LoadData();
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

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // load lại data grid view
            LoadData();
        }
        private void UpdateDetailsPanel(DataGridViewRow row)
        {
            if (row == null) return;
            txtID.Text = row.Cells["ID"].Value.ToString();
            txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
            txtSucChua.Text = row.Cells["SucChua"].Value.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            string trangThaiFromGrid = row.Cells["TrangThai"].Value.ToString();
            int index = cboTrangThai.FindStringExact(trangThaiFromGrid);
            cboTrangThai.SelectedIndex = (index != -1) ? index : 0;
        }
        private void dgvPhongHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (isDataLoading || dgvPhongHoc.CurrentRow == null) return;
            UpdateDetailsPanel(dgvPhongHoc.CurrentRow);
        }
    }
}