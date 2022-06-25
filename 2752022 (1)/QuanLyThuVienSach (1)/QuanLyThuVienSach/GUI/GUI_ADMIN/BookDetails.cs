
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
    public partial class Book_Details : Form
    {
        int move;
        int movex;
        int movey;
        public Book_Details()
        {
            InitializeComponent();
        }
        public Book_Details(int ID_sach)
        {
            InitializeComponent();
            
            ShowBook_Details(ID_sach);         
        }
        private void ShowBook_Details(int ID)
        {
            Sach_View sach =  BLL_Sach.Instance.GetSachByID(ID);
            tb_Author.Text = sach.TenTacGia.ToString();
            tb_Cetegory.Text = sach.TheLoai;
            tb_IDBook.Text = ID.ToString();
            tb_ImportPrice.Text = sach.GiaNhap.ToString();
            tb_SellingPrice.Text = sach.GiaBan.ToString();
            tb_Reprints.Text = sach.SoLanTaiBan.ToString();
            tb_Year.Text = sach.NamXuatBan;
            tb_NameBook.Text = sach.TenSach;         
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            movex = e.X;
            movey = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movex, MousePosition.Y - movey);
            }    
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }
    }
}
