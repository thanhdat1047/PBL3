using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVienSach.DAL.DAL_ADMIN;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    internal class BLL_LichSuThanhToan
    {
        private static BLL_LichSuThanhToan _Instance; 
        public static BLL_LichSuThanhToan Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_LichSuThanhToan();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_LichSuThanhToan() { }

        public DataTable GetALLLichSuBanSachByID(int IDPerson)
        {
            return DAL_LichSuThanhToan.Instance.GetAllHISByID_DAL(IDPerson);
        }

        public DataTable GetLSBSbyDate(DateTime from , DateTime to )
        {
            return DAL_LichSuThanhToan.Instance.GetAllHISbyDATE_DAL(from, to);
        }
       
        public void CreateNew (int IDPerson, int MHD)
        {
            DAL_LichSuThanhToan.Instance.CreateHis_DAL(IDPerson,MHD);
        }

        public DataTable GetALLLS()
        {
            return DAL_LichSuThanhToan.Instance.GetAllHIS_DAL();
        }

        public string GetTotal(int Idperson, DateTime from , DateTime to)
        {
            return DAL_LichSuThanhToan.Instance.GetTotal(Idperson,from,to); 
        }
        public DataTable GetLSBSbyDateAndID(int IDperson,DateTime from, DateTime to)
        {
            return DAL_LichSuThanhToan.Instance.GetAllHISbyDATEandPerson_DAL(IDperson,from, to);
        }



    }
    
}
