using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongHoc.DAL
{
    public class LichSuDungDAL
    {
        private DatabaseConnection dbConnection = new DatabaseConnection();

        /// <summary>
        /// Kiểm tra xem có lịch đăng ký nào khác bị trùng trong khoảng thời gian yêu cầu cho một phòng cụ thể không.
        /// </summary>
        /// <param name="idPhong">ID của phòng cần kiểm tra</param>
        /// <param name="batDauMoi">Thời gian bắt đầu của lịch đăng ký mới</param>
        /// <param name="ketThucMoi">Thời gian kết thúc của lịch đăng ký mới</param>
        /// <returns>True nếu có trùng lịch, False nếu không có</returns>
        public bool KiemTraTrungLich(int idPhong, DateTime batDauMoi, DateTime ketThucMoi)
        {
            // Thời gian bắt đầu của lịch mới phải trước thời gian kết thúc của lịch cũ,
            // Thời gian kết thúc của lịch mới phải sau thời gian bắt đầu của lịch cũ.
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
    }
}