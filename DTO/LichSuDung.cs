using System;

namespace QuanLyPhongHoc.DTO
{
    public class LichSuDung
    {
        public int ID { get; set; }
        public int ID_Phong { get; set; }
        public int ID_NguoiDung { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string MucDich { get; set; }
    }
}