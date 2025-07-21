using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongHoc.DTO
{
    // Kiểm tra đăng nhập của người dùng hiện tại
    public static class CurrentUserSession
    {
        public static DTO.NguoiDung User { get; set; }
    }
}
