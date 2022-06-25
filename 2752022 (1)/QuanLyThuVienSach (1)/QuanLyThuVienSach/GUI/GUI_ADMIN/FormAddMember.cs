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
    public partial class FormAddMember : Form
    {
        public delegate void Mydel();
        public Mydel d { get; set; }
        public FormAddMember()
        {
             InitializeComponent();  
        }

        //ham nay tao sua
        int Id_Person { get; set; }
        
        public FormAddMember(int id_person)
        {
            InitializeComponent();
            Id_Person = id_person;
            SetComboboxSale();

        }

        //ham nay tao sua
        private bool CheckSale(int IDperson)
        {
            bool check = false; 
            Member m = new Member();
            m = BLL_Member.Instance.GetMemberByID(IDperson);
            if (m.ID_Position == 3)
            {
                check = true;
            }
            return check;
        }

        //ham nay tao sua
        private void SetComboboxSale ()
        {
            if(CheckSale(Id_Person))
            {
                comboBox_Position.Text = "Khac Hang";
                comboBox_Position.Enabled = false;
            }
            
        }
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_AddMember_Click(object sender, EventArgs e)
        {
            Member member = new Member();
            member.UserName = tb_UserName.Text.ToString();
            member.Password = tb_Password.Text.ToString();
            if(CheckSale(Id_Person))
            {
                member.ID_Position = 4;

            }
            else { member.ID_Position = comboBox_Position.SelectedIndex + 1; }
           
            member.Name_Person = tb_Name.Text.ToString();
            member.Address = tb_Anddress.Text.ToString();
            member.Email = tb_Email.Text.ToString();
            member.PhoneNumber = tb_Phone.Text.ToString();
            member.DateOfBirth = DatePicker_DateOfBirth.Value;

            if (RadioButton_Male.Checked == true)
            {
                member.Gender = "Male";
            }
            else
            {
                member.Gender = "Female";
            }

            BLL_Member.Instance.AddMember_BLL(member);
            d();
            this.Close();
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            tb_Anddress.Text = "";
            tb_Email.Text = "";
            tb_Name.Text = "";
            tb_Password.Text = "";
            tb_Phone.Text = "";
            tb_UserName.Text = "";
            comboBox_Position.Text = "";
            RadioButton_Femalee.Checked = false ;
            RadioButton_Male.Checked = false;
            
        }

        private void tb_Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
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
