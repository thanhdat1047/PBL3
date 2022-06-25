using QuanLyThuVienSach.DAL.DAL_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    internal class BLL_LichSuNhapSach
    {
        private static BLL_LichSuNhapSach _Instance;
        public static BLL_LichSuNhapSach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_LichSuNhapSach();
                }
                return _Instance;
            }
            private set { }
        }
        public DataTable GetAllLichSuNhapSach_BLL()
        {
            return DAL_LichSuNhapSach.Instance.GetAllLichSuNhapSach_DAL();
        }

        public DataTable GetAllLichSuNhapSach_BLL(DateTime from , DateTime to)
        {
           return DAL_LichSuNhapSach.Instance.GetAllLichSuNhapSach_DAL(from,to);
        }


    }
}
