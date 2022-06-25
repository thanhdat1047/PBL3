using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_Bill
    {
        private static DAL_Bill _Instance;
        public static DAL_Bill Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Bill();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Bill() { }

        public DataTable GetAllBill_DAL()
        {
            string query = "Select * FROM HoaDon";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetAllBill_DAL(DateTime from ,DateTime to)
        {
            string query = $"Select * FROM HoaDon WHERE NgayLap >= '{from}' and NgayLap <= '{to}'";
            return DBHelper.Instance.GetRecord(query);
        }
        public DataTable GetBillByID_DAL(int ID)
        {
            string query = $"Select * FROM ChiTietHoaDon WHERE MaHoaDon = {ID}";
            return DBHelper.Instance.GetRecord(query);
        }

        public void DeleteBill_DAL(int ID)
        {     
            
            string query = $"Delete from ChiTietHoaDon where MaHoaDon = {ID}";
            DBHelper.Instance.ExecuteDB(query);

            string query1 = $"Delete from HoaDon where MaHoaDon = {ID}";
            DBHelper.Instance.ExecuteDB(query1);
        }

        public List<Bill_Detail> GetListBill_Detail(int MaHoaDon)
        {
            List<Bill_Detail> data = new List<Bill_Detail>();
            string query = $"select * from ChiTietHoaDon where MaHoaDon = {MaHoaDon}";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(Bill_DetailByDataRow(i));
            }
            return data;
        }
        public Bill_Detail Bill_DetailByDataRow(DataRow i)
        {
            return new Bill_Detail
            {
                MaHoaDon = Convert.ToInt32(i["MaHoaDon"].ToString()),
                MaSach = Convert.ToInt32(i["MaSach"].ToString()),
                SoLuong = Convert.ToInt32(i["SoLuong"]),
                MucGiamGia = Convert.ToDouble(i["MucGiamGia"]),
                Total = Convert.ToDecimal(i["TongTien"])
            };
        }
        public List<Sach_View> GetListSach_DAL()
        {
            List<Sach_View> data = new List<Sach_View>();
            string query = "select * from Sach";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(GetSachByDataRow(i));
            }
            return data;
        }
        public Sach_View GetSachByDataRow(DataRow i)
        {
            return new Sach_View
            {
                MaSach = Convert.ToInt32(i["MaSach"].ToString()),
                TenSach = i["TenSach"].ToString(),
                TheLoai = i["TheLoai"].ToString(),
                TenTacGia = i["TenTacGia"].ToString(),
                SoLanTaiBan = Convert.ToInt32(i["SolanTaiBan"].ToString()),
                NamXuatBan = i["NamXuatBan"].ToString(),
                GiaNhap = Convert.ToInt32(i["GiaNhap"].ToString()),
                GiaBan = Convert.ToInt32(i["GiaBan"].ToString())
            };
        }

        //----------------------------------------------------

        int MaHoaDon = 0;
        public void GetMaHD(int idperson)
        {
            int MaHD = 0;
            foreach (DataRow dr in GetIMHDBill_DAL(idperson).Rows)
            {
                MaHD = Convert.ToInt32(dr["MaHoaDon"].ToString());
            }
            MaHoaDon = MaHD;
        }
        public int SelectMHD(int idPerson)
        {
            GetMaHD(idPerson);
            return MaHoaDon;
        }
       
        public DataTable GetIMHDBill_DAL(int IDperson)
        {
            string query = $"Select MaHoaDon FROM HoaDon WHERE  ID_Person = {IDperson} ";
            return DBHelper.Instance.GetRecord(query);
        }

        public bool CheckTimeSale(int MaSach)
        {
            bool check = true;
            
            string query = $"select NgayKetThuc from SachKhuyenMai where SachKhuyenMai.MaSach = {MaSach} "; 
            foreach( DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
               var t  = Convert.ToDateTime(i["NgayKetThuc"].ToString());
               if (DateTime.Now > t)
                    check = false;
               else
                    check = true;
            }
          
            return check;


        }
        public float GetMucGiamgiaCus(int MaSach)
        {
            float mgg = 0;
         
            if(CheckTimeSale(MaSach))
            {
                string query = $"select MucGiamGia from SachKhuyenMai where SachKhuyenMai.MaSach = {MaSach} ";

                foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
                {
                    var t = (float)Convert.ToDouble(i["MucGiamGia"].ToString());
                    if (t != 0)
                    {
                        mgg = t;
                        break;
                    }
                }
            }
            
            return mgg;
        }
        public int CheckBuy(List<Sach_View_Cus> data)
        {
            int IsB = 0;
            foreach (var i in data)
            {
                if (i.SoLuongMua > DAL_Sach.Instance.GetNumber(i.MaSach))
                {
                    IsB = i.MaSach;
                    break;
                }

            }
            return IsB;
        }
        public void CreateBill(List<Sach_View_Cus> data, int ID_person)
        {
            Double ToTal = 0;
            Decimal Tong = Convert.ToDecimal(ToTal);
            DateTime g = DateTime.Now;
            string date = $"{g.Year}-{g.Month}-{g.Day}";
            string query = $"INSERT INTO dbo.HoaDon VALUES ('{date}',{0},{ID_person})";
            DBHelper.Instance.ExecuteDB(query);
            GetMaHD(ID_person);
            foreach (var i in data)
            {
                float mgg= GetMucGiamgiaCus(i.MaSach);
                string t = "0." + mgg * 10;
                string query2 = $"insert into ChiTietHoaDon VAlues({MaHoaDon},{i.MaSach},{i.SoLuongMua},{t},{(float)((i.GiaBan - (i.GiaBan * GetMucGiamgiaCus(i.MaSach))) * i.SoLuongMua)})";
                DBHelper.Instance.ExecuteDB(query2);
                ToTal = ToTal + ((i.GiaBan - (i.GiaBan * GetMucGiamgiaCus(i.MaSach))) * i.SoLuongMua);
            }

            string query3 = $"update HoaDon set TongTien = {(float)ToTal}, ID_Person ={ID_person}  where MaHoaDon = {MaHoaDon}";
            DBHelper.Instance.ExecuteDB(query3);

            foreach (var i in data)
            {
                string query4 = $"update Kho set TongSoLuong = TongSoLuong - {i.SoLuongMua} where MaSach = {i.MaSach}";
                DBHelper.Instance.ExecuteDB(query4);
            }
        }
        public DataTable GetIDPersonBill_DAL(DateTime from, DateTime to, int ID)
        {
            string query = $"Select * FROM HoaDon WHERE ( NgayLap >= '{from}' AND NgayLap <= '{to}' AND ID_Person = {ID} ) ";
            return DBHelper.Instance.GetRecord(query);
        }

        //-------------------------------------------------------------------
        //SaleMan
        public DataTable SearchBillByID_DAL(int ID)
        {
            string query = $"Select * FROM HoaDon WHERE MaHoaDon = {ID}";
            return DBHelper.Instance.GetRecord(query);
        }

        public void CreatenewBill_DAL()
        {
            DateTime g = DateTime.Now;
            string query = $"INSERT INTO dbo.HoaDon VALUES ('{g}',{0},{0})";
            DBHelper.Instance.ExecuteDB(query);
        }

        public DataTable GetAllBilDetail()
        {
            string query = "SELECT MaHoaDon,sach.MaSach,sach.TenSach,SoLuong,MucGiamGia,TongTien FROM dbo.ChiTietHoaDon, dbo.Sach where sach.MaSach = ChiTietHoaDon.MaSach";
            return DBHelper.Instance.GetRecord(query);

        }

       

    }
}
