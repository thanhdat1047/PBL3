using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_LichSuThanhToan
    {
        private static DAL_LichSuThanhToan _Instance;
        public static DAL_LichSuThanhToan Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_LichSuThanhToan();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_LichSuThanhToan() { }
        public DataTable GetAllHIS_DAL()
        {
            string query = $"select ID_LichSuThanhToan,Person.ID_Person, Person.Name_Person,HoaDon.MaHoaDon, HoaDon.NgayLap,HoaDon.TongTien from LichSuThanhToan ,HoaDon, PerSon "
            + " where LichSuThanhToan.ID_Person = Person.ID_Person"
            + " AND LichSuThanhToan.MaHoaDon = HoaDon.MaHoaDon ";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetAllHISByID_DAL(int IDPerson)
        {
            string query = $"select ID_LichSuThanhToan,Person.ID_Person, Person.Name_Person,HoaDon.MaHoaDon, HoaDon.NgayLap,HoaDon.TongTien from LichSuThanhToan ,HoaDon, PerSon "
            + $" where LichSuThanhToan.ID_Person = Person.ID_Person "
            + $" AND Person.ID_Person = {IDPerson} "
            + " AND LichSuThanhToan.MaHoaDon = HoaDon.MaHoaDon ";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetAllHISbyDATE_DAL(DateTime from, DateTime to)
        {
      
            string query = $"select ID_LichSuThanhToan,Person.ID_Person, Person.Name_Person,HoaDon.MaHoaDon, HoaDon.NgayLap,HoaDon.TongTien from LichSuThanhToan ,HoaDon, PerSon "
            + "where LichSuThanhToan.ID_Person = Person.ID_Person"
            + "AND LichSuThanhToan.MaHoaDon = HoaDon.MaHoaDon"
            + $"AND HoaDon.NgayLap >= '{from}' and HoaDon.NgayLap <= '{to}'";
            return DBHelper.Instance.GetRecord(query);
        }

        public void CreateHis_DAL(int Idperson, int MaHoadon)
        {
            string query = $"INSERT INTO dbo.LichSuThanhToan VALUES( {Idperson}, {MaHoadon})";
            DBHelper.Instance.ExecuteDB(query);
        }

        public string GetTotal (int IDperson,DateTime from ,DateTime to )
        {
           
            string query = "select SUM(HoaDon.TongTien) AS total from LichSuThanhToan ,HoaDon, PerSon "
                + " WHERE LichSuThanhToan.ID_Person = Person.ID_Person "
                + " AND LichSuThanhToan.MaHoaDon = HoaDon.MaHoaDon "
                + $" AND Person.ID_Person = {IDperson} "
                + $" AND HoaDon.NgayLap >= '{from}' and HoaDon.NgayLap <= '{to}' ";

            string total ="";
            foreach(DataRow row in DBHelper.Instance.GetRecord(query).Rows )
            {
                total = row["total"].ToString();
            }

            return total;    
        }
        public DataTable GetAllHISbyDATEandPerson_DAL(int Idperson,DateTime from, DateTime to)
        {
            
            string query = $"select ID_LichSuThanhToan,Person.ID_Person, Person.Name_Person,HoaDon.MaHoaDon, HoaDon.NgayLap,HoaDon.TongTien from LichSuThanhToan ,HoaDon, PerSon "
            + " where LichSuThanhToan.ID_Person = Person.ID_Person"
            + " AND LichSuThanhToan.MaHoaDon = HoaDon.MaHoaDon"
            + $" AND Person.ID_Person = {Idperson} "
            + $" AND HoaDon.NgayLap >= '{from}' and HoaDon.NgayLap <= '{to}'";
            return DBHelper.Instance.GetRecord(query);
        }
    }
}
