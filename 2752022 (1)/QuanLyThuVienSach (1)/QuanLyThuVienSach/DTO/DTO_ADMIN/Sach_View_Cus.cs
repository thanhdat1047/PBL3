using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_ADMIN
{
    internal class Sach_View_Cus
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string TheLoai { get; set; }
        public string TenTacGia { get; set; }
        public int SoLanTaiBan { get; set; }
        public string NamXuatBan { get; set; }
        public double GiaBan { get; set; }
        public int SoLuongMua { get; set; }

    }
}
