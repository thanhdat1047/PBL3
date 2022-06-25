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
    internal class BLL_SachKhuyenMai
    {
        private static BLL_SachKhuyenMai _Instance;
        public static BLL_SachKhuyenMai Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_SachKhuyenMai();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_SachKhuyenMai()
        {
        }
        public DataTable GetAllSachKhuyenMai_DAL()
        {
            return DAL_SachKhuyenMai.Instance.GetAllSachKhuyenMai();
        }

        public List<SachKhuyenMai> ListAllSachKhuyenMai()
        {
            List<SachKhuyenMai> list = new List<SachKhuyenMai>();
            foreach (DataRow i in GetAllSachKhuyenMai_DAL().Rows)
            {
                list.Add(GetSachKhuyenMai(i));
            }
            return list;
        }

        public SachKhuyenMai GetSachKhuyenMai(DataRow i)
        {
            SachKhuyenMai sachKM = new SachKhuyenMai();
            sachKM.ID_SachKhuyenMai = Convert.ToInt32(i[0]);
            sachKM.MaSach = Convert.ToInt32(i[1]);
            sachKM.MucGiamGia = Convert.ToDouble(i[2]);
            sachKM.Gia = Convert.ToDouble(i[3]);
            sachKM.NgayBatDau = Convert.ToDateTime(i[5]);
            sachKM.NgayKetThuc = Convert.ToDateTime(i[6]);
            return sachKM;
        }

        public SachKhuyenMai GetSachKhuyenMaiByID(int ID)
        {
            SachKhuyenMai sachKM = new SachKhuyenMai();
            foreach (SachKhuyenMai i in ListAllSachKhuyenMai())
            {
                if (i.ID_SachKhuyenMai == ID)
                {
                    sachKM = i;
                }
            }
            return sachKM;
        }

        public void DeleteSachKhuyenMai_BLL(List<string> List_ID)
        {
            foreach (string i in List_ID)
            {
                DAL_SachKhuyenMai.Instance.DeleteSachKhuyenMai_DAL(i);
            }
        }

        public void UpdateSachKhuyenMai_BLL(SachKhuyenMai sachKM)
        {
            DAL_SachKhuyenMai.Instance.UpdateSachKhuyenMai_DAL(sachKM);
        }

        public List<int> GetMaSachBySKM()
        { 
            List<int> list = new List<int>();
            foreach (SachKhuyenMai i in ListAllSachKhuyenMai())
            { 
            list.Add(i.MaSach);
            }
            return list;
        }

        public void AddSachKhuyenMai_BLL(SachKhuyenMai sachKM)
        {
            DAL_SachKhuyenMai.Instance.AddSachKhuyenMai_DAL(sachKM);
        }

        //-----------------------------------------------------------------------
        public double GetGiamGia_BLL(int MaSach)
        {
            double GiamGia = DAL_Bill.Instance.GetMucGiamgiaCus(MaSach);
            return GiamGia;

        }

    }
}
