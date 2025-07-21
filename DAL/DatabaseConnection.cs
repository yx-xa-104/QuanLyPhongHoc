using System.Data.SqlClient;

namespace QuanLyPhongHoc.DAL
{
    public class DatabaseConnection
    {
        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=QuanLyPhongHocDB;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}