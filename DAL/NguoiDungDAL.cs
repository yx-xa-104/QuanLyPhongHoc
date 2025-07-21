using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongHoc.DAL
{
    public class NguoiDungDAL
    {
        // kết nối đến cơ sở dữ liệu
        private DatabaseConnection dbConnection = new DatabaseConnection();

        // kiểm tra đăng nhập
        public DTO.NguoiDung KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            DTO.NguoiDung user = null;
            string query = "SELECT ID, TenDangNhap, HoTen, Quyen FROM NguoiDung WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            using (SqlConnection conn = dbConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new DTO.NguoiDung
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                            HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                            Quyen = reader.GetString(reader.GetOrdinal("Quyen"))
                        };
                    }
                }
            }
            return user;
        }
    }
}