using QuanLyThuVienSach.BILL.BILL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach.GUI.GUI_ADMIN
{
    public partial class FormAddSale : Form
    {
        public delegate void Mydel();
        public Mydel d { get; set; }
        public FormAddSale()
        {
            InitializeComponent();
            LoadAdd();
        }
        private void btn_Exit1_Click(object sender, EventArgs e)
        {
            this.Close();      
        }

        private void LoadAdd()
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            foreach (int i in BLL_SachKhuyenMai.Instance.GetMaSachBySKM())
            {
                list1.Add(i);
                
            }

            foreach (int i in BLL_Sach.Instance.GetAllMaSach_BLL())
            {
                list2.Add(i);
            }

            IEnumerable<int> list = list2.Except(list1);

            cb_MaSachAdd.Items.Clear();
            foreach (int i in list)
            {
                cb_MaSachAdd.Items.Add(i);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            BLL_SachKhuyenMai.Instance.AddSachKhuyenMai_BLL(GetSachKhuyenMai());
            d();
        }

        private SachKhuyenMai GetSachKhuyenMai()
        { 
            SachKhuyenMai sachKhuyenMai = new SachKhuyenMai();
            sachKhuyenMai.MaSach = Convert.ToInt32(cb_MaSachAdd.SelectedItem.ToString());
            sachKhuyenMai.NgayBatDau = DatePicker_Start.Value;
            sachKhuyenMai.NgayKetThuc  = DatePicker_END.Value;
            sachKhuyenMai.MucGiamGia = Convert.ToDouble(tb_Discount.Text);

            return sachKhuyenMai;
        
        }

        private void cb_MaSachAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_Price.Text = (BLL_Sach.Instance.GetSachByID(Convert.ToInt32(cb_MaSachAdd.SelectedItem.ToString())).GiaBan).ToString();
            if (tb_Discount.Text != "")
            {
                double Discount = Convert.ToDouble(tb_Discount.Text);
                double Price = Convert.ToDouble(tb_Price.Text);
                double PriceAfterDiscount = Discount * Price;
                tb_PriceAfterDiscount.Text = PriceAfterDiscount.ToString();
            }

        }
        private void tb_Discount_TextChange(object sender, EventArgs e)
        {
            try {
                double Discount = Convert.ToDouble(tb_Discount.Text);
                double Price = Convert.ToDouble(tb_Price.Text);
                double PriceAfterDiscount = Discount * Price;
                tb_PriceAfterDiscount.Text = PriceAfterDiscount.ToString();
            }
            catch (Exception)
            {
                return;
            }

        }

        private void tb_Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
