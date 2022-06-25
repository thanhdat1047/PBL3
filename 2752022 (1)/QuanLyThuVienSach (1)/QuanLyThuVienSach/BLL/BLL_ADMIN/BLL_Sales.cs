using QuanLyThuVienSach.DTO.DTO_ADMIN;
using QuanLyThuVienSach.DAL.DAL_ADMIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    internal class BLL_Sales
    {
        private static BLL_Sales _Instance;
        public static BLL_Sales Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Sales();
                }
                return _Instance;
            }
            private set { }
        }

        public List<Sales> GetAllSales_BLL()
        {
            return DAL_Sales.Instance.GetAllSales();
        }

        public List<int> GetAllID_Bill_BLL(int ID_Sales)
        {
            return DAL_Sales.Instance.GetAllID_Bill(ID_Sales);

        }

    }
}
