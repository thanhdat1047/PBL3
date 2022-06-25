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

namespace QuanLyThuVienSach.GUI
{
    public partial class EditResume : Form
    {
        public int ID_Person { get; set; }

        public delegate void Mydel();
        public Mydel d { get; set; }
        public EditResume()
        {
            InitializeComponent();
        }
        public EditResume(int ID_person)
        {
            InitializeComponent();
            ID_Person = ID_person;
            SetPerson(BLL_Member.Instance.GetPersonByID(ID_Person));
        }
        private void SetPerson(Person person)
        {
            tb_Address.Text = person.Address;
            tb_Email.Text = person.Email;
            tb_Name.Text = person.Name_Person;
            tb_Phone.Text = person.PhoneNumber;
            if (person.Gender == "Male")
            {
                Rb_Male.Checked = true;
                Rb_Female.Checked = false;
            }
            else
            {
                Rb_Male.Checked = false;
                Rb_Female.Checked = true;
            }
            DatePicker_DOB.Value = person.DateOfBirth;

        }
        private Person GetPerson()
        {
            Person person = new Person();
            person.ID_Person = ID_Person;
            person.Name_Person = tb_Name.Text;
            person.Email = tb_Email.Text;
            person.Address = tb_Address.Text;
            person.PhoneNumber = tb_Phone.Text;

            if (Rb_Male.Checked == true)
            {
                person.Gender = "Male";
            }
            else
            {
                person.Gender = "Female";
            }

            person.DateOfBirth = DatePicker_DOB.Value;
            return person;
        }
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            tb_Address.Text = "";
            tb_Email.Text = "";
            tb_Name.Text = "";
            tb_Phone.Text = "";
        }
        private void bt_Save_Click(object sender, EventArgs e)
        {
            BLL_Member.Instance.UpdatePerson_BLL(GetPerson());
            d();
            this.Close();
        }
    }
}
