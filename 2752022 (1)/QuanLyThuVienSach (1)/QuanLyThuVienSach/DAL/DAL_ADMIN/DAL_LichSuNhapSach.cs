using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_LichSuNhapSach
    {
        private static DAL_LichSuNhapSach _Instance;
        public static DAL_LichSuNhapSach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_LichSuNhapSach();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_LichSuNhapSach() { }

        public DataTable GetAllLichSuNhapSach_DAL()
        {
            string query =
                " SELECT ID_LichSuNhapSach,MaSach,SoLuong,NgayNhap,ID_Person" +
                " FROM LichSuNhapSach";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetAllLichSuNhapSach_DAL(DateTime From, DateTime To)
        {
            string query =
             " SELECT ID_LichSuNhapSach,MaSach,SoLuong,NgayNhap,ID_Person" +
             " FROM LichSuNhapSach" +
            $" WHERE NgayNhap >= '{From}' and NgayNhap <= '{To}' ";
            return DBHelper.Instance.GetRecord(query);
        }
        // ---------------stocker
        public DataTable ShowLichSuNhapSach()
        {
            string query = "Select ID_LichSuNhapSach,Sach.MaSach, Sach.TenSach,SoLuong ,LichSuNhapSach.NgayNhap, Person.Name_PerSon as Ten_Nguoi_Lap from LichSuNhapSach,Sach,PerSon where LichSuNhapSach.MaSach = Sach.MaSach and Person.ID_Person = LichSuNhapSach.ID_Person";
            return DBHelper.Instance.GetRecord(query);
        }

    }
}
