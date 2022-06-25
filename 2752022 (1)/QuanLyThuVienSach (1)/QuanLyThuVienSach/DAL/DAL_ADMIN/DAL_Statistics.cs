using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_Statistics
    {
        private static DAL_Statistics _Instance;
        public static DAL_Statistics Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Statistics();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Statistics() { }
        public decimal GetDoanhThu_DAL()
        {
            int DoanhThu = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord("SELECT SUM(TongTien) FROM HoaDon").Rows)
            {
                if (i[0].ToString() != "")
                {
                    DoanhThu = Convert.ToInt32(i[0]);
                }
            }
            return DoanhThu;
        }
        public decimal GetDoanhThu_DAL(DateTime DateFrom, DateTime DateTo)
        {
            int DoanhThu = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord($"SELECT SUM(TongTien) FROM HoaDon WHERE NgayLap >= '{DateFrom}' AND NgayLap <= '{DateTo}' ").Rows)
            {
                if (i[0].ToString() != "")
                {
                    DoanhThu = Convert.ToInt32(i[0]);
                }
                if (i[0].ToString() == "")
                {
                    DoanhThu = 0;
                }
            }
            return DoanhThu;
        }

        public int GetSoLuongSachBan_DAL()
        {
            int SoSachBan = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord("SELECT SUM(SoLuong) AS SoLuongSachBan FROM ChiTietHoaDon").Rows)
            {
                if (i[0].ToString() != "")
                {
                    SoSachBan = Convert.ToInt32(i[0]);
                }
            }
            return SoSachBan;
        }
        public int GetSoLuongSachBan_DAL(DateTime DateFrom, DateTime DateTo)
        {
            int SoSachBan = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord($"SELECT SUM(SoLuong) AS SoLuongSachBan FROM ChiTietHoaDon,HoaDon" +
                $" WHERE NgayLap >= '{DateFrom}' AND NgayLap <= '{DateTo}' AND" +
                $" ChiTietHoaDon.MaHoaDon = HoaDon.MaHoaDon ").Rows)
            {
                if (i[0].ToString() != "")
                {
                    SoSachBan = Convert.ToInt32(i[0]);
                }
                if (i[0].ToString() == "")
                {
                    SoSachBan = 0;
                }

            }
            return SoSachBan;
        }

        public int GetSoLuongHoaDon_DAL()
        {
            int SoHoaDon = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord("select count(MaHoaDon) from HoaDon").Rows)
            {
                if (i[0].ToString() != "")
                {
                    SoHoaDon = Convert.ToInt32(i[0]);
                }
              
            }
            return SoHoaDon;
        }

        public int GetSoLuongHoaDon_DAL(DateTime DateFrom, DateTime DateTo)
        {
            int SoHoaDon = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord($"select count(MaHoaDon) from HoaDon where NgayLap >= '{DateFrom}' AND NgayLap <= '{DateTo}' ").Rows)
            {
                if (i[0].ToString() != "")
                {
                    SoHoaDon = Convert.ToInt32(i[0]);
                }
                if (i[0].ToString() == "")
                {
                    SoHoaDon = 0;
                }
            }
            return SoHoaDon;
        }

        public List<decimal> GetDoanhThuTheoThang_DAL(DateTime DateFrom, DateTime DateTo)
        {
            List<decimal> DoanhThu = new List<decimal>();

            for(int _month = DateFrom.Month; _month <= DateTo.Month; _month ++ )
            {
                foreach (DataRow i in DBHelper.Instance.GetRecord($"SELECT SUM(TongTien) FROM HoaDon WHERE Month(NgayLap) = {_month}").Rows)
                {
                    if (i[0].ToString() != "")
                    {
                        DoanhThu.Add(Convert.ToDecimal(i[0]));
                    }
                    else
                    {
                        DoanhThu.Add(0);
                    }    
                }
            }
            return DoanhThu;
        }

        public List<decimal> GetChiPhiTheoThang_DAL(DateTime DateFrom, DateTime DateTo)
        {
            List<decimal> ChiPhi = new List<decimal>();

      
            for (int _month = DateFrom.Month; _month <= DateTo.Month; _month++)
            {
                string query = 
                    "  select Sum(ChiPhi) as TongChiPhi from (select LichSuNhapSach.MaSach, SoLuong*GiaNhap as ChiPhi " +
                    $" from Sach, LichSuNhapSach where Sach.MaSach = LichSuNhapSach.MaSach AND Month(NgayNhap) = {_month} ) as TP";

                foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
                {
                    if (i[0].ToString() != "")
                    {
                        ChiPhi.Add(Convert.ToDecimal(i[0]));
                    }
                    else
                    {
                        ChiPhi.Add(0);
                    }
                }
            }
            return ChiPhi;
        }

   
        public decimal GetChiPhi_DAL()
        {
            int ChiPhi = 0;
            string query = "" +
                "select Sum(ChiPhi) as TongChiPhi from (select LichSuNhapSach.MaSach, SoLuong*GiaNhap as ChiPhi " +
                " from Sach, LichSuNhapSach where Sach.MaSach = LichSuNhapSach.MaSach) as TP";

            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if (i[0].ToString() != "")
                {
                    ChiPhi = Convert.ToInt32(i[0]);
                }
            }

            return ChiPhi;
        }

        public decimal GetChiPhi_DAL(DateTime DateFrom, DateTime DateTo)
        {
            int ChiPhi = 0;
            string query = "select Sum(ChiPhi) as TongChiPhi from (select LichSuNhapSach.MaSach, SoLuong*GiaNhap as ChiPhi " +
                $" from Sach, LichSuNhapSach where Sach.MaSach = LichSuNhapSach.MaSach AND NgayNhap >= '{DateFrom}' AND NgayNhap <= '{DateTo}' ) as TP";

            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if (i[0].ToString() != "")
                {
                    ChiPhi = Convert.ToInt32(i[0]);
                }
                if (i[0].ToString() == "")
                {
                    ChiPhi = 0;
                }
            }

            return ChiPhi;
        }

        public int GetSoLuongSachNhap_DAL()
        {
            int SoSachNhap = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord($"SELECT SUM(SoLuong) AS SoLuongSachNhap FROM LichSuNhapSach").Rows)
            {
                if (i[0].ToString() != "")
                {
                    SoSachNhap = Convert.ToInt32(i[0]);
                }
            }
            return SoSachNhap;
        }

        public int GetSoLuongSachNhap_DAL(DateTime DateFrom, DateTime DateTo)
        {
            int SoSachNhap = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord($"SELECT SUM(SoLuong) AS SoLuongSachNhap FROM LichSuNhapSach " +
                $" WHERE NgayNhap >= '{DateFrom}' AND NgayNhap <= '{DateTo}'").Rows)
            {
                if (i[0].ToString() != "")
                {
                    SoSachNhap = Convert.ToInt32(i[0]);
                }
                if (i[0].ToString() == "")
                {
                    SoSachNhap = 0;
                }
            }
            return SoSachNhap;
        }

    }
}
