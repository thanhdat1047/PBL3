using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_ADMIN
{
    internal class Bill_Detail
    {
        public int MaHoaDon { get; set; }
        public int MaSach { get; set; }
        public int SoLuong { get; set; }
        public double MucGiamGia { get; set; }
        public Decimal Total { get; set; }

    }
}
