using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyPhongHoc.DAL
{
    public class DatabaseConnection
    {
        private readonly string connectionString = "(localdb)\\MSSQLLocalDB (sa);Database=QuanLyPhongHocDB;Trusted_Connection=True;";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
