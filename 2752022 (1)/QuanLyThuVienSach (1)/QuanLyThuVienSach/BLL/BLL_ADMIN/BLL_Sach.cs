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
    class BLL_Sach
    {
        private static BLL_Sach _Instance;
        public static BLL_Sach Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Sach();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_Sach()
        {

        }

        public List<Sach_View> GetListSach_BLL()
        {
            List<Sach_View> list = new List<Sach_View>();
            foreach (DataRow i in DAL_Sach.Instance.GetAllSach_DAL().Rows)
            {
                list.Add(new Sach_View
                { 
                    MaSach = Convert.ToInt32(i[0]),
                    TenSach = i[1].ToString(),
                    TheLoai = i[2].ToString(),
                    TenTacGia = i[3].ToString(),
                    SoLanTaiBan = Convert.ToInt32(i[4].ToString()),
                    NamXuatBan = Convert.ToString(i[5]),
                    GiaNhap = Convert.ToDouble(i[6].ToString()),
                    GiaBan = Convert.ToDouble(i[7].ToString()),
                    TongSoLuong = Convert.ToInt32(i[8]),

                }); ;
            }
            return list;
        }
        public DataTable GetAllSach()
        {
            return DAL_Sach.Instance.GetAllSach_DAL();
        }

        public Sach_View GetSachByID(int ID_Sach)
        {
            Sach_View sach = new Sach_View();
            foreach (Sach_View i in GetListSach_BLL())
            { 
                if(ID_Sach == i.MaSach)
                {
                    sach = i;
                }
            }
            return sach;
        }

        public void UpdateSach_BLL(Sach_View sach, int ID_Person)
        {
            DAL_Sach.Instance.UpdateSach_DAL(sach, ID_Person);
        }
        public void DelSach_BLL(List<int> MaSach)
        {
            foreach (int i in MaSach)
            {
                DAL_Sach.Instance.DeleteSach_DAL(i);
            }
        }
        public void AddSach_BLL(Sach_View sach,int ID_Person)
        {
            DAL_Sach.Instance.AddSach_DAL(sach,ID_Person);
        }
        public List<int> GetAllMaSach_BLL()
        { 
            List<int> list = new List<int>();
            foreach (Sach_View i in GetListSach_BLL())
            { 
                list.Add(i.MaSach);
            }
            return list;
        }

        public bool CheckStateSach_BLL(int MaSach)
        {
            return DAL_Sach.Instance.CheckStateSach_DAL(MaSach);
        }

        public List<string> GetAllThongTinAD_ThuocTinh_BLL(string ThuocTinh)
        {
            List<string> list = new List<string>();

            foreach (Sach_View l in GetListSach_BLL())
            {
                if (ThuocTinh == "Cetegory")
                {
                    list.Add(l.TheLoai);
                }
                if (ThuocTinh == "Year")
                {
                    list.Add(l.NamXuatBan);
                }
                if (ThuocTinh == "Author")
                {
                    list.Add(l.TenTacGia);
                }
            }
            return list;
        }

        public DataTable GetAllSach(string ThuocTinh, string txt)
        {
            return DAL_Sach.Instance.GetAllSach_DAL(ThuocTinh, txt);
        }
        public DataTable GetAllSach(string Find, string ThuocTinh, string txt)
        {
            return DAL_Sach.Instance.GetAllSach_DAL(Find, ThuocTinh, txt);
        }



        //--------------------------

        public List<string> GetAllThongTin_ThuocTinh_BLL(string ThuocTinh)
        {
            List<string> list = new List<string>();

            foreach (Sach_View l in GetListSach_BLL())
            {
                if (ThuocTinh == "Cetegory" && l.TongSoLuong > 0 )
                {
                    list.Add(l.TheLoai);
                }
                if (ThuocTinh == "Year" && l.TongSoLuong > 0)
                {
                    list.Add(l.NamXuatBan);
                }
                if (ThuocTinh == "Author" && l.TongSoLuong > 0)
                {
                    list.Add(l.TenTacGia);
                }
            }
            return list;
        }

        public DataTable GetAllSachCus(string ThuocTinh, string TheLoai)
        {
            return DAL_Sach.Instance.GetAllSachCus_DAL(ThuocTinh, TheLoai);
        }
        public DataTable GetAllSachCus()
        {
            return DAL_Sach.Instance.GetAllSachCus_DAL();
        }

        public DataTable GetAllSachCus(string Find ,string ThuocTinh,string txt)
        {
            return DAL_Sach.Instance.GetAllSachCus_DAL(Find,ThuocTinh,txt);
        }

        List<Sach_View_Cus> List_Oder = new List<Sach_View_Cus>();
        public List<Sach_View_Cus> DataPage2()
        {
            return List_Oder;
        }
        public int CheckBuy(List<Sach_View_Cus> data)
        {
            return DAL_Bill.Instance.CheckBuy(data);
        }


        public void RemoveList_Oder()
        {
            List_Oder.Clear();
        }
        public List<Sach_View_Cus> GetListSachCus_BLL()
        {
            List<Sach_View_Cus> list = new List<Sach_View_Cus>();
            foreach (DataRow i in DAL_Sach.Instance.GetSachOderCus_DAL().Rows)
            {
                list.Add(new Sach_View_Cus
                {
                    MaSach = Convert.ToInt32(i[0]),
                    TenSach = i[1].ToString(),
                    TheLoai = i[2].ToString(),
                    TenTacGia = i[3].ToString(),
                    SoLanTaiBan = Convert.ToInt32(i[4].ToString()),
                    NamXuatBan = Convert.ToString(i[5]),
                    GiaBan = Convert.ToDouble(i[6].ToString()),
                    SoLuongMua = 1
                });
            }
            return list;
        }
        public Sach_View_Cus GetSachbyMaSach(string MaSach)
        {
            Sach_View_Cus data = new Sach_View_Cus();
            foreach (Sach_View_Cus j in GetListSachCus_BLL())
            {
                if (j.MaSach == Convert.ToInt32(MaSach))
                {
                    data = j;
                }
            }
            return data;
        }
        public void Getdatapage2(string ms)
        {
            Sach_View_Cus d = new Sach_View_Cus();
            Sach_View_Cus k = GetSachbyMaSach(ms);
            Sach_View t = GetSachByID(Convert.ToInt32(ms));
            Boolean check = false;
            foreach (Sach_View_Cus j in List_Oder)
            {
                if (j.MaSach == k.MaSach)
                {
                    if (t.TongSoLuong > j.SoLuongMua)
                        j.SoLuongMua += 1;
                    else
                        j.SoLuongMua = t.TongSoLuong;
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                d.MaSach = k.MaSach;
                d.TenSach = k.TenSach;
                d.TenTacGia = k.TenTacGia;
                d.TheLoai = k.TheLoai;
                d.SoLanTaiBan = k.SoLanTaiBan;
                d.NamXuatBan = k.NamXuatBan;
                d.GiaBan = k.GiaBan;
                d.SoLuongMua = 1;
                List_Oder.Add(d);
                k = null;
            }
        }
        public void Tdatapage2(string ms)
        {
            Sach_View_Cus d = new Sach_View_Cus();
            Sach_View_Cus k = GetSachbyMaSach(ms);
            foreach (Sach_View_Cus j in List_Oder)
            {
                if (j.MaSach == k.MaSach)
                {
                    if (j.SoLuongMua > 1)
                        j.SoLuongMua -= 1;
                    else
                    {
                        DelSachinPage2(Convert.ToInt32(ms));
                    }
                    break;
                }
            }
        }
        public List<Sach_View> GetSachCustomerByID(List<string> MaSach)
        {
            List<Sach_View> sach = new List<Sach_View>();
            foreach (string s in MaSach)
            {
                foreach (Sach_View i in GetListSach_BLL())
                {
                    if (i.MaSach.ToString() == s)
                        sach.Add(i);
                }
            }
            return sach;
        }

        public void DelSachinPage2(int MaSach)
        {
            List<Sach_View_Cus> data = new List<Sach_View_Cus>();
            data = List_Oder;
            int j = 0;
            while (j < data.Count)
            {
                if (MaSach == data[j].MaSach)
                {
                    data.Remove(data[j]);
                    --j;
                }
                j++;
            }
            List_Oder = null;
            List_Oder = data;
        }
        //-------------------------------
        public bool CheckID(List<Sach_View_Cus> data , int t)
        {
            bool check = false; 
            foreach(Sach_View_Cus i in data)
            {
                if(i.MaSach == t )
                {
                    check = true;
                }
            }
            return check;
        }

        List<Sach_View_Cus> data = new List<Sach_View_Cus>();
        double total = 0;
        public double xuly(int idsach)
        {
            List<Sach_View_Cus> data1 = new List<Sach_View_Cus>();
          
            Sach_View_Cus t = GetSachbyMaSach(idsach.ToString());
            double mucgiam = 0;
            
            mucgiam = BLL_SachKhuyenMai.Instance.GetGiamGia_BLL(idsach);

            if(CheckID(data,idsach))
            {
                foreach(Sach_View_Cus i in data1)
                {
                    if(i.MaSach == idsach)
                    {
                        i.SoLuongMua += 1;
                        total += i.GiaBan * (1 - mucgiam);
                    }
                }
            }
            else
            {
                t.SoLuongMua = 1;
                data1.Add(t);
                total += t.GiaBan*(1- mucgiam);
                data = data1;
                t = null;
            }

           
            data = data1;
            return total;
        }

        public List<Sach_View_Cus> dataSale()
        {
            return data;
        }
        public DataTable Getallbookdetail()
        {
            return DAL_Sach.Instance.GetAllBookDetail();

        }
        //-------------------- stocker
        public DataTable ShowLichSuNhapSach()
        {
            return DAL_LichSuNhapSach.Instance.ShowLichSuNhapSach();
        }
        public DataTable FindSach_BLL(string txt)
        {
            return DAL_Sach.Instance.FindSach_DAL(txt);
        }

    }
}
