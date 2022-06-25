using QuanLyThuVienSach.DAL.DAL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    internal class BLL_Bill
    {
        private static BLL_Bill _Instance;
        public static BLL_Bill Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Bill();
                }
                return _Instance;
            }
            private set { }
        }

        public DataTable GetAllBill_BLL()
        {
            return DAL_Bill.Instance.GetAllBill_DAL();
        }
        public DataTable GetAllBill_BLL(DateTime from , DateTime to)
        {
            return DAL_Bill.Instance.GetAllBill_DAL(from,to);
        }

        public DataTable GetBillByID_BLL(int ID)
        {
            return DAL_Bill.Instance.GetBillByID_DAL(ID);
        }

        

        public void DeleteBill_BLL(List<int> list)
        {
            foreach(int id in list)
            {
                DAL_Bill.Instance.DeleteBill_DAL(id);
            }   
        }

        public List<Bill_Detail_View> GetListBill_Detail_Views(int MaHoaDon)
        {
            List<Bill_Detail_View> list = new List<Bill_Detail_View>();

            foreach (Bill_Detail i in DAL_Bill.Instance.GetListBill_Detail(MaHoaDon))
            {
                Bill_Detail_View bdv = new Bill_Detail_View();
                bdv.MaHoaDon = MaHoaDon;
                bdv.MaSach = i.MaSach;
                bdv.SoLuong = i.SoLuong;
                bdv.MucGiamGia = i.MucGiamGia;
                bdv.Total = i.Total;

                foreach (Sach_View sach in DAL_Bill.Instance.GetListSach_DAL())
                {
                    if (sach.MaSach == bdv.MaSach)
                    {
                        bdv.TenSach = sach.TenSach;
                        break;
                    }
                ;
                }
                list.Add(bdv);
            }    
            return list;
        }
        public void CreateBill(List<Sach_View_Cus> s, int Id_person)
        {
            DAL_Bill.Instance.CreateBill(s, Id_person);
        }
        public DataTable GetIDPersonBill_BLL(DateTime from, DateTime to, int ID)
        {
            return DAL_Bill.Instance.GetIDPersonBill_DAL(from, to, ID);
        }

        //-------------------------------SaleMan

        public int SelectMHD (int IDperson)
        {
            return DAL_Bill.Instance.SelectMHD(IDperson);   
        }
        public DataTable SearchBillByID_BLL(int ID)
        {
            return DAL_Bill.Instance.SearchBillByID_DAL(ID);
        }
        public DataTable SearchAllBillDate_BLL(DateTime from, DateTime to)
        {
            return DAL_Bill.Instance.GetAllBill_DAL(from, to);
        }
        public int CheckBuy(List<Sach_View_Cus> data)
        {
            return DAL_Bill.Instance.CheckBuy(data);
        }

        //public int CreatenewBill()
        //{
        //    DAL_Bill.Instance.CreatenewBill_DAL();
        //    int MHD = 0; 
        //    foreach(DataRow i in GetAllBill_BLL().Rows)
        //    {
        //        MHD = Convert.ToInt32( i[0].ToString());
        //    }

        //    return MHD;
        //}
        //public DataTable GetallBilldetail()
        //{
        //    return DAL_Bill.Instance.GetAllBilDetail();

        //}

    }
}
