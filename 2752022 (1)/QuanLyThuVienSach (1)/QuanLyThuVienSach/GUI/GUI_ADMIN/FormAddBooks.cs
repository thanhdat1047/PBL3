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
    public partial class FormAddBooks : Form
    {
        public delegate void Mydel();

        public int ID_Person;
        public Mydel d { get; set; }
        public FormAddBooks()
        {
            InitializeComponent();
        }
        public FormAddBooks(int ID_person)
        {
            this.ID_Person = ID_person;
            InitializeComponent();
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_AddBook_Click(object sender, EventArgs e)
        {
            BLL_Sach.Instance.AddSach_BLL(GetSach(),ID_Person);
            d();
        }
        private Sach_View GetSach()
        {
            Sach_View sach = new Sach_View();
            sach.TongSoLuong = Convert.ToInt32(tb_NumberOfBook.Text.ToString());
            sach.TheLoai = tb_Cetegory.Text;
            sach.TenTacGia = tb_Author.Text;
            sach.GiaNhap = Convert.ToInt32(tb_ImportPrice.Text.ToString());
            sach.GiaBan = Convert.ToUInt32(tb_SellingPrice.Text);
            sach.NamXuatBan = tb_Year.Text;
            sach.SoLanTaiBan = Convert.ToInt32(tb_Reprints.Text);
            sach.TenSach = tb_NameBook.Text;
            return sach;
        }

        private void bt_ClearBook_Click(object sender, EventArgs e)
        {
            tb_NumberOfBook.Text = "";
            tb_Cetegory.Text = "";
            tb_Cetegory.Text = "";
            tb_Author.Text = "";
            tb_ImportPrice.Text = "";
            tb_SellingPrice.Text = "";
            tb_Year.Text = "";
            tb_Reprints.Text = "";
            tb_NameBook.Text = "";
        }
    }
}
