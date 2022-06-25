using QuanLyThuVienSach.BILL.BLL_Login;
using QuanLyThuVienSach.DAL.DAL_Login;
using QuanLyThuVienSach.DTO.DTO_Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach.GUI
{
    public partial class SighUp : Form
    {
        public SighUp()
        {
            InitializeComponent();
            tb_PASSWORD.MaxLength = 30;
            tb_USERNAME.MaxLength = 30;
        }
        private void tb_PASSWORD_Click(object sender, EventArgs e)
        {
            if (tb_USERNAME.Text == "")
            {
                tb_USERNAME.PlaceholderText = "USERNAME OR EMAIL";
            }
            tb_PASSWORD.PlaceholderText = "";
            tb_PASSWORD.PasswordChar = '•';
        }
        private void bt_Show_MouseDown(object sender, MouseEventArgs e)
        {
            if (tb_PASSWORD.Text != "")
            {
                tb_PASSWORD.PasswordChar = '\0';
            }
        }
        private void bt_Show_MouseUp(object sender, MouseEventArgs e)
        {
            if (tb_PASSWORD.Text != "")
            {
                tb_PASSWORD.PasswordChar = '•';
            }
        }
        private void tb_USERNAME_Click(object sender, EventArgs e)
        {
            tb_USERNAME.PlaceholderText = "";
            if (tb_PASSWORD.Text == "")
            {
                tb_PASSWORD.PasswordChar = '\0';
                tb_PASSWORD.PlaceholderText = "PASSWORD";
            }
        }
        private void lb_LogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
        }
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
     
        private void bt_SighUp_Click(object sender, EventArgs e)
        {
            bool check = true;
            string username = tb_USERNAME.Text;
            foreach (Account i in BLL_Login.Instance.GetAllAccount_BLL())
            {
                if(i.UserName == username)
                {
                    MessageBox.Show(" UserName đã được sử dụng, vui lòng nhập username khác");
                    tb_USERNAME.Text = "";
                    tb_PASSWORD.Text = "";
                    check = false;
                }            
            }   
            if(check == true)
            {
                Account account = new Account();
                account.UserName = tb_USERNAME.Text;
                account.Password = tb_PASSWORD.Text;
                DAL_Login.Instance.AddAccount_DAL(account);
                this.Close();
                new SighUpNext(BLL_Login.Instance.GetIDAccount_BLL()).Show();
            }    
            
        }
    }
}
