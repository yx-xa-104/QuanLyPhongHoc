using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongHoc.DAL
{
    public class PhongHocDAL
    {
        private DatabaseConnection dbConnection = new DatabaseConnection();

        // Lấy tất cả phòng học
        public List<DTO.PhongHoc> GetAll()
        {
            List<DTO.PhongHoc> phongHocList = new List<DTO.PhongHoc>();
            string query = "SELECT ID, TenPhong, SucChua, TrangThai, MoTa FROM PhongHoc";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DTO.PhongHoc ph = new DTO.PhongHoc
                        {
                            ID = reader.GetInt32(0),
                            TenPhong = reader.GetString(1),
                            SucChua = reader.GetInt32(2),
                            TrangThai = reader.GetString(3),
                            MoTa = reader.GetString(4)
                        };
                        phongHocList.Add(ph);
                    }
                }
            }
            return phongHocList;
        }

        // Thêm phòng học mới
        public bool Them(DTO.PhongHoc phong)
        {
            string query = "INSERT INTO PhongHoc(TenPhong, SucChua, TrangThai, MoTa) VALUES (@TenPhong, @SucChua, @TrangThai, @MoTa)";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
                cmd.Parameters.AddWithValue("@SucChua", phong.SucChua);
                cmd.Parameters.AddWithValue("@TrangThai", phong.TrangThai);
                cmd.Parameters.AddWithValue("@MoTa", phong.MoTa);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Sửa thông tin phòng học
        public bool Sua(DTO.PhongHoc phong)
        {
            string query = "UPDATE PhongHoc SET TenPhong = @TenPhong, SucChua = @SucChua, TrangThai = @TrangThai, MoTa = @MoTa WHERE ID = @ID";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", phong.ID);
                cmd.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
                cmd.Parameters.AddWithValue("@SucChua", phong.SucChua);
                cmd.Parameters.AddWithValue("@TrangThai", phong.TrangThai);
                cmd.Parameters.AddWithValue("@MoTa", phong.MoTa);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa phòng học
        public bool Xoa(int id)
        {
            string query = "DELETE FROM PhongHoc WHERE ID = @ID";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateStatus(int idPhong, string trangThaiMoi)
        {
            string query = "UPDATE PhongHoc SET TrangThai = @TrangThai WHERE ID = @ID";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", idPhong);
                cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public void CapNhatTrangThaiTuDong()
        {
            // Sử dụng GETDATE() để lấy thời gian hiện tại của server SQL
            string query = @"
        -- B1: Chuyển các phòng 'Đã đặt trước' thành 'Đang sử dụng' nếu đến giờ
        UPDATE ph
        SET ph.TrangThai = 'Đang sử dụng'
        FROM PhongHoc ph
        JOIN LichSuDung ls ON ph.ID = ls.ID_Phong
        WHERE 
            ph.TrangThai = 'Đã đặt trước' AND
            GETDATE() BETWEEN ls.ThoiGianBatDau AND ls.ThoiGianKetThuc;

        -- B2: Chuyển các phòng 'Đang sử dụng' thành 'Trống' nếu đã hết giờ
        -- Logic này cần cẩn thận để không chuyển phòng có lịch đặt ngay sau đó
        UPDATE ph
        SET ph.TrangThai = 'Trống'
        FROM PhongHoc ph
        WHERE
            ph.TrangThai = 'Đang sử dụng' AND
            NOT EXISTS (
                SELECT 1 FROM LichSuDung ls 
                WHERE ls.ID_Phong = ph.ID AND GETDATE() BETWEEN ls.ThoiGianBatDau AND ls.ThoiGianKetThuc
            );
    ";

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}