using QuanLyThuVienSach.BILL.BILL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using QuanLyThuVienSach.GUI.GUI_ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach.GUI.GUI_THUKHO
{
    public partial class Stockerr : Form
    {
        public int ID_AccountL { get; set; }
        public int ID_PersonL { get; set; }
        public Stockerr()
        {
            InitializeComponent();
        }

        public Stockerr(int ID_account, int ID_person)
        {
            InitializeComponent();
            ID_AccountL = ID_account;
            ID_PersonL = ID_person;
            ReloadResume();
        }

        string _ThuocTinh = "TenTacGia";

        private void SetResume(Member member)
        {
            LableQ.Text = member.Name_Person;
            tb_NameResume.Text = member.Name_Person;
            tb_PhoneResume.Text = member.PhoneNumber;
            tb_PositionResume.Text = member.Name_Position;
            tb_AnddressResume.Text = member.Address;
            tb_EmailResume.Text = member.Email;
            DateTime d = member.DateOfBirth;
            tb_DateOfBirthResume.Text = $"{d.Day}/{d.Month}/{d.Year}";
            tb_GenderResume.Text = member.Gender;
        }

        private void bt_Resume_Click(object sender, EventArgs e)
        {
            ReloadResume();
            TabPage_Stocker.SetPage("tabPage1");
        }
        private void ReloadResume()
        {
            SetResume(BLL_Member.Instance.GetMemberByID(ID_PersonL));
        }

        private void bt_EditResume_Click(object sender, EventArgs e)
        {
            EditResume f = new EditResume(ID_PersonL);
            f.d = new EditResume.Mydel(ReloadResume);
            f.ShowDialog();
        }

        private void bt_ChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword(ID_AccountL).ShowDialog();

        }

        //---------------------------------------

        public void ShowManageBook()
        {
            ClearTextBoxBook();
            DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach();
            DesignColumnHeader();
        }

        public void ClearTextBoxBook()
        {
            tb_Book.Text = "";
            tb_Cetegory.Text = "";
            tb_Author.Text = "";
            tb_IDBook.Text = "";
            tb_ImportPrice.Text = "";
            tb_SellingPrice.Text = "";
            tb_PublishingYear.Text = "";
            tb_NumberOfReprints.Text = "";
            tb_NameBook.Text = "";

        }

        public void DesignColumnHeader()
        {
            DataGridView_Book.Columns[0].HeaderText = "ID Book";
            DataGridView_Book.Columns[1].HeaderText = "Name Book";
            DataGridView_Book.Columns[1].Width = 150;
            DataGridView_Book.Columns[2].HeaderText = "Category";
            DataGridView_Book.Columns[2].Width = 100;
            DataGridView_Book.Columns[3].HeaderText = "Author";
            DataGridView_Book.Columns[4].HeaderText = "Number of reprints";
            DataGridView_Book.Columns[5].HeaderText = "Year";
            DataGridView_Book.Columns[6].HeaderText = "Import Price";
            DataGridView_Book.Columns[7].HeaderText = "Selling Price";
            DataGridView_Book.Columns[8].HeaderText = "Book";

        }

        private void SetCombobox(string ThuocTinh)
        {
            comboBox_Find.Items.Clear();
            comboBox_Find.Items.Add("All");
            if (ThuocTinh != "")
            {
                foreach (string i in BLL_Sach.Instance.GetAllThongTinAD_ThuocTinh_BLL(ThuocTinh).Distinct())
                {
                    comboBox_Find.Items.Add(i);
                }
            }
            comboBox_Find.SelectedIndex = 0;
        }
        private void SetDetail_Sach(Sach_View sach)
        {
            tb_Book.Text = sach.TongSoLuong.ToString();
            tb_Cetegory.Text = sach.TheLoai.ToString();
            tb_Author.Text = sach.TenTacGia.ToString();
            tb_IDBook.Text = sach.MaSach.ToString();
            tb_ImportPrice.Text = sach.GiaNhap.ToString();
            tb_SellingPrice.Text = sach.GiaBan.ToString();
            tb_PublishingYear.Text = sach.NamXuatBan.ToString();
            tb_NumberOfReprints.Text = sach.SoLanTaiBan.ToString();
            tb_NameBook.Text = sach.TenSach.ToString();
        }
        private Sach_View GetSach()
        {
            Sach_View sach = new Sach_View();
            sach.TongSoLuong = Convert.ToInt32(tb_Book.Text.ToString());
            sach.TheLoai = tb_Cetegory.Text;
            sach.TenTacGia = tb_Author.Text;
            sach.MaSach = Convert.ToInt32(tb_IDBook.Text.ToString());
            sach.GiaNhap = Convert.ToInt32(tb_ImportPrice.Text.ToString());
            sach.GiaBan = Convert.ToUInt32(tb_SellingPrice.Text);
            sach.NamXuatBan = tb_PublishingYear.Text;
            sach.SoLanTaiBan = Convert.ToInt32(tb_NumberOfReprints.Text);
            sach.TenSach = tb_NameBook.Text;
            return sach;
        }
        private void bt_ManageBooks_Click(object sender, EventArgs e)
        {
            TabPage_Stocker.SetPage("tabPage2");
            ShowManageBook();
            SetCombobox("");
        }
        private void bt_SaveBook_Click(object sender, EventArgs e)
        {
            if (tb_PublishingYear.Text.Length == 4)
            {
                if (DataGridView_Book.SelectedRows != null)
                {
                    BLL_Sach.Instance.UpdateSach_BLL(GetSach(), ID_PersonL);
                    ShowManageBook();
                }
            }
            else
            {
                MessageBox.Show("Kiểm tra lại năm suất bản", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView_Book_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (DataGridView_Book.SelectedRows.Count != 0)
                {
                    int ID_Sach = Convert.ToInt32(DataGridView_Book.SelectedRows[0].Cells["MaSach"].Value);
                    Sach_View sach = BLL_Sach.Instance.GetSachByID(ID_Sach);
                    SetDetail_Sach(sach);
                }
            }
        }
        private void bt_DeleteBook_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("      Are you sure delete Book ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (DataGridView_Book.SelectedRows.Count > 0)
                {
                    List<int> list = new List<int>();
                    foreach (DataGridViewRow i in DataGridView_Book.SelectedRows)
                    {
                        list.Add(Convert.ToInt32(i.Cells["MaSach"].Value));
                    }
                    BLL_Sach.Instance.DelSach_BLL(list);
                }
                ShowManageBook();
            }
        }
        private void bt_AddBookS_Click(object sender, EventArgs e)
        {
            FormAddBooks f = new FormAddBooks(ID_PersonL);
            f.d = new FormAddBooks.Mydel(ShowManageBook);
            f.ShowDialog();
        }
        private void bt_FindBook_Click(object sender, EventArgs e)
        {
            ClearTextBoxBook();
            string Find = tb_SearchBook.Text.ToString();
            if (Find != "")
            {

                DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach(tb_SearchBook.Text, _ThuocTinh, comboBox_Find.SelectedItem.ToString());
                DesignColumnHeader();
            }
            else
            {
                if (comboBox_Find.Text != "All")
                {
                    DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach(_ThuocTinh, comboBox_Find.SelectedItem.ToString());
                }
                else
                {
                    DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach();
                }
                DesignColumnHeader();
            }
        }
        private void comboBox_TheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBoxBook();
            if (comboBox_Find.Text != "All")
            {
                DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach(_ThuocTinh, comboBox_Find.SelectedItem.ToString());
            }
            else
            {
                DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSach();
            }
            DesignColumnHeader();
        }
        private void bt_Author_Click(object sender, EventArgs e)
        {
            SetCombobox("Author");
            _ThuocTinh = "TenTacGia";
            comboBox_Find.SelectedIndex = 0;
        }

        private void bt_Year_Click(object sender, EventArgs e)
        {
            SetCombobox("Year");
            _ThuocTinh = "NamXuatBan";
            comboBox_Find.SelectedIndex = 0;
        }

        private void bt_Cetegory_Click(object sender, EventArgs e)
        {
            SetCombobox("Cetegory");
            _ThuocTinh = "TheLoai";
            comboBox_Find.SelectedIndex = 0;
        }

        private void bt_ReloandBook_Click(object sender, EventArgs e)
        {
            ClearTextBoxBook();
            SetCombobox("");
            ShowManageBook();
        }




        //----------------------------------------------------

        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}
