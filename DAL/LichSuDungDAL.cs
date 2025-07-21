using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongHoc.DAL
{
    public class LichSuDungDAL
    {
        private DatabaseConnection dbConnection = new DatabaseConnection();

        public bool KiemTraTrungLich(int idPhong, DateTime batDauMoi, DateTime ketThucMoi)
        {
            string query = "SELECT COUNT(*) FROM LichSuDung WHERE ID_Phong = @ID_Phong AND (@BatDauMoi < ThoiGianKetThuc) AND (@KetThucMoi > ThoiGianBatDau)";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Phong", idPhong);
                cmd.Parameters.AddWithValue("@BatDauMoi", batDauMoi);
                cmd.Parameters.AddWithValue("@KetThucMoi", ketThucMoi);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool DangKy(DTO.LichSuDung lich)
        {
            string query = "INSERT INTO LichSuDung(ID_Phong, ID_NguoiDung, ThoiGianBatDau, ThoiGianKetThuc, MucDich) VALUES (@ID_Phong, @ID_NguoiDung, @ThoiGianBatDau, @ThoiGianKetThuc, @MucDich)";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Phong", lich.ID_Phong);
                cmd.Parameters.AddWithValue("@ID_NguoiDung", lich.ID_NguoiDung);
                cmd.Parameters.AddWithValue("@ThoiGianBatDau", lich.ThoiGianBatDau);
                cmd.Parameters.AddWithValue("@ThoiGianKetThuc", lich.ThoiGianKetThuc);
                cmd.Parameters.AddWithValue("@MucDich", lich.MucDich);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetLichSuChiTiet()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT
                                ls.ID,
                                p.TenPhong,
                                n.HoTen AS NguoiDangKy,
                                ls.ThoiGianBatDau,
                                ls.ThoiGianKetThuc,
                                ls.MucDich
                             FROM LichSuDung ls
                             JOIN PhongHoc p ON ls.ID_Phong = p.ID
                             JOIN NguoiDung n ON ls.ID_NguoiDung = n.ID
                             ORDER BY ls.ThoiGianBatDau DESC";
            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}