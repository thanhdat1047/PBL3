using QuanLyThuVienSach.DAL.DAL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    internal class BLL_Statistics
    {
        private static BLL_Statistics _Instance;
        public static BLL_Statistics Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Statistics();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_Statistics()
        {
        }

        public Statistics GetStatistics()
        {
            Statistics statistics = new Statistics();

            statistics.DoanhThu = DAL_Statistics.Instance.GetDoanhThu_DAL();
            statistics.ChiPhi = DAL_Statistics.Instance.GetChiPhi_DAL();
            statistics.SoSachNhap = DAL_Statistics.Instance.GetSoLuongSachNhap_DAL();
            statistics.SoSachBan = DAL_Statistics.Instance.GetSoLuongSachBan_DAL();
            statistics.SoHoaDon = DAL_Statistics.Instance.GetSoLuongHoaDon_DAL();

            return statistics;
        }

        public Statistics GetStatistics(DateTime DateFrom, DateTime DateTo)
        {
            Statistics statistics = new Statistics();
            statistics.DoanhThu = DAL_Statistics.Instance.GetDoanhThu_DAL(DateFrom, DateTo);
            statistics.ChiPhi = DAL_Statistics.Instance.GetChiPhi_DAL(DateFrom, DateTo);
            statistics.SoSachNhap = DAL_Statistics.Instance.GetSoLuongSachNhap_DAL(DateFrom, DateTo);
            statistics.SoSachBan = DAL_Statistics.Instance.GetSoLuongSachBan_DAL(DateFrom, DateTo);
            statistics.SoHoaDon = DAL_Statistics.Instance.GetSoLuongHoaDon_DAL(DateFrom, DateTo);
            return statistics;
        }

        public List<decimal> GetDoanhThuTheoThang_BLL(DateTime DateFrom, DateTime DateTo)
        {
            return DAL_Statistics.Instance.GetDoanhThuTheoThang_DAL(DateFrom, DateTo);
        }

        public List<decimal> GetChiPhiTheoThang_BLL(DateTime DateFrom, DateTime DateTo)
        {
            return DAL_Statistics.Instance.GetChiPhiTheoThang_DAL(DateFrom, DateTo);
        }

    }
}
