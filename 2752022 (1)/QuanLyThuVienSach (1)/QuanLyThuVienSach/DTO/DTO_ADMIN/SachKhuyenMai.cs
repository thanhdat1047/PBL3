using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_ADMIN
{
    internal class SachKhuyenMai
    {
        public int ID_SachKhuyenMai { get; set; }
        public int MaSach{ get; set; }
        public double MucGiamGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public double Gia { get; set; }

    }
}
