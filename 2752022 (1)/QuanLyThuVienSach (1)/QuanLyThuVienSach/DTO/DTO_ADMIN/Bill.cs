using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_ADMIN
{
    internal class Bill
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public Decimal TongTien { get; set; }
        public int ID_Person { get; set; }

    }
}
