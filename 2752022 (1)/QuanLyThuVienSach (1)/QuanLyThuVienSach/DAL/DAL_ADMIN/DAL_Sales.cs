using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DAL_Sales
    {
        private static DAL_Sales _Instance;
        public static DAL_Sales Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Sales();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Sales() { }

        public decimal GetTongTien(int ID_Sales)
        {
            string query = 
                $" SELECT SUM(HoaDon.TongTien) AS TongTien" +
                $" FROM LichSuThanhToan,HoaDon " +
                $" WHERE LichSuThanhToan.MaHoaDon = HoaDon.MaHoaDon " +
                $" AND LichSuThanhToan.ID_Person = {ID_Sales} ";

            decimal TongTien = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if(i["TongTien"].ToString() != "")
                {
                    TongTien = Convert.ToDecimal(i["TongTien"].ToString());
                }    
            }
            return TongTien;
        }

        public int GetSoHoaDon(int ID_Sales)
        {
            string query =
                $" SELECT COUNT(MaHoaDon) AS SoHoaDon" +
                $" FROM LichSuThanhToan" +
                $" WHERE ID_Person = {ID_Sales}";
         
            int SoHoaDon = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if(i["SoHoaDon"].ToString() != "")
                {
                    SoHoaDon = Convert.ToInt32(i["SoHoaDon"].ToString());
                }    
            }
            return SoHoaDon;
        }

        public string GetNameSales(int ID_Sales)
        {
            string query =
                $" SELECT Name_Person" +
                $" FROM Person" +
                $" WHERE ID_Person = {ID_Sales}";

            string NameSales = "";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if (i["Name_Person"].ToString() != "")
                {
                    NameSales = (i["Name_Person"].ToString());
                }
            }
            return NameSales;
        }


        public List<int> GetAllID_Sales()
        {
            List<int> list = new List<int>();

            string query = 
                " SELECT ID_Person " +
                " FROM Person, Account" +
                " WHERE Person.ID_Account = Account.ID_Account" +
                " AND Account.ID_Position = 3";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if (i["ID_Person"].ToString() != "")
                {
                    list.Add(Convert.ToInt32(i["ID_Person"].ToString()));
                }
            }

            return list;
        }

        public List<int> GetAllID_Bill(int ID_Sales)
        {
            List<int> list = new List<int>();

            string query = 
                $" SELECT LichSuThanhToan.MaHoaDon" +
                $" FROM LichSuThanhToan" +
                $" WHERE ID_Person = {ID_Sales}";
      
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                if (i["MaHoaDon"].ToString() != "")
                {
                    list.Add(Convert.ToInt32(i["MaHoaDon"].ToString()));
                }
            }

            return list;
        }



        public Sales GetSales(int ID_Sales)
        {
            Sales sales = new Sales();
            sales.ID = ID_Sales;
            sales.Name = GetNameSales(ID_Sales);
            sales.SoHoaDon = GetSoHoaDon(ID_Sales);
            sales.TongTien = GetTongTien(ID_Sales);
            return sales;
        }


        public List<Sales> GetAllSales()
        {
            List<Sales> List_sales = new List<Sales>();

            foreach(int i in GetAllID_Sales())
            {
                List_sales.Add(GetSales(i));
            }    

            return List_sales;
        }
    }
}
