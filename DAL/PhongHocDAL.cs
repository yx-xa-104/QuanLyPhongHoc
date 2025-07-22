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
        public void CapNhatTrangThaiTuDong()
        {
            // Đảm bảo phòng luôn có trạng thái đúng tại thời điểm hiện tại.
            string query = @"
        -- B1: Reset tất cả các phòng không được sử dụng về 'Trống'.
        UPDATE PhongHoc
        SET TrangThai = 'Trống'
        WHERE ID NOT IN (
            SELECT DISTINCT ID_Phong FROM LichSuDung
            WHERE GETDATE() BETWEEN ThoiGianBatDau AND ThoiGianKetThuc
        );

        -- B2: Cập nhật tất cả các phòng đang được sử dụng thành 'Đang sử dụng'.
        UPDATE PhongHoc
        SET TrangThai = 'Đang sử dụng'
        WHERE ID IN (
            SELECT DISTINCT ID_Phong FROM LichSuDung
            WHERE GETDATE() BETWEEN ThoiGianBatDau AND ThoiGianKetThuc
        );
    ";

            using (var conn = dbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}