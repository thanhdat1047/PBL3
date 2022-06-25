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
    public partial class FormChangePassword : Form
    {
        public int ID_Account { get; set; }
        public FormChangePassword(int ID_account)
        {
            InitializeComponent();
            ID_Account = ID_account;
            tb_CPass.MaxLength = 30;
            tb_NewPass.MaxLength = 30;
            tb_Pass.MaxLength = 30;
        }
        private void btn_Exit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (tb_Pass.Text != "")
            {
                Account account = BLL_Member.Instance.GetAccountByID(ID_Account);

                if (tb_Pass.Text == account.Password)
                {
                    if (tb_NewPass.Text == tb_CPass.Text)
                    {
                        Account account_new = new Account();
                        account_new.ID_Account = ID_Account;
                        account_new.UserName = account.UserName;
                        account_new.Password = tb_NewPass.Text;
                        BLL_Member.Instance.UpdateAccount_BLL(account_new);
                        MessageBox.Show("Thay đổi mật khẩu thành công");
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không khớp");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác ");
                }
            }
        }

    }
}
