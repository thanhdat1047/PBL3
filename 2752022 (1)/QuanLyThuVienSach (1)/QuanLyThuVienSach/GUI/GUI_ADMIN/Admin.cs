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

namespace QuanLyThuVienSach.GUI
{
    public partial class Admin : Form
    {
        int movex, movey, move;
        public int ID_AccountL { get; set; }
        public int ID_PersonL { get; set; }
        public Admin()
        {
            InitializeComponent();
            MovePanel(bt_Resume);
        }
        public Admin(int ID_account, int ID_person)
        {
            InitializeComponent();
            ID_AccountL = ID_account;
            ID_PersonL = ID_person;
            MovePanel(bt_Resume);
            ReloadResume();
        }
        string _ThuocTinh = "TenTacGia";

        //-------------------------------------------------------------- DESIGN

        #region Design
        private void MovePanel(Control c)
        {
            Panel_Admin.Height = (c.Height - 15);
            Panel_Admin.Top = (c.Top + 8);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            movex = e.X;
            movey = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movex, MousePosition.Y - movey);
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }
        private void cbb_Positon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        //------------------------------------------------------------------ RESUME

        #region RESEME

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
            MovePanel(bt_Resume);
            Page_Admin.SetPage("Resume");
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
       
        #endregion

        //-------------------------------------------------------------- MEMBER

        #region Member

        private void bt_Members_Click(object sender, EventArgs e)
        {
            ClearTextBoxMember();
            MovePanel(bt_Members);
            Page_Admin.SetPage("Members");
            ShowMembers();
            tb_PublishingYear.MaxLength = 4;
            
        }

        private void ClearTextBoxMember()
        {
            tb_UserName.Text = "";
            tb_Password.Text = "";
            cbb_Positon.Text = "";
            tb_ID.Text = "";
            tb_Name.Text = "";
            tb_Anddress.Text = "";
            tb_Email.Text = "";
            tb_Phone.Text = "";
            RadioButton_Male.Checked = false;
            RadioButton_Female.Checked = false;

        }
        private void Set_Member(Member member)
        {
            tb_UserName.Text = member.UserName.ToString();
            tb_Password.Text = member.Password.ToString();
            cbb_Positon.Text = cbb_Positon.Items[member.ID_Position - 1].ToString();
            tb_ID.Text = member.ID_Person.ToString();
            tb_Name.Text = member.Name_Person.ToString();
            tb_Anddress.Text = member.Address.ToString();
            tb_Email.Text = member.Email.ToString();
            tb_Phone.Text = member.PhoneNumber.ToString();
            DatePicker_DateOfBirth.Value = member.DateOfBirth;

            if (member.Gender == "Male")
            {
                RadioButton_Male.Checked = true;
                RadioButton_Female.Checked = false;
            }
            else
            {
                RadioButton_Male.Checked = false;
                RadioButton_Female.Checked = true;
            }
        }
        private Member GetMember()
        {
            Member member = new Member();
            member.ID_Person = Convert.ToInt32(tb_ID.Text.ToString());
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
            member.UserName = tb_UserName.Text.ToString();
            member.Password = tb_Password.Text.ToString();
            int i = cbb_Positon.SelectedIndex + 1;
            member.ID_Position = i;
            return member;
        }
        public void ShowMembers()
        {
            DataGridView_Members.DataSource = BLL_Member.Instance.GetAllMembers_View_BLL();
            DataGridView_Members.Columns[0].HeaderText = "ID";
            DataGridView_Members.Columns[0].Width = 100;
            DataGridView_Members.Columns[1].HeaderText = "Name";
            DataGridView_Members.Columns[2].HeaderText = "Gender";
            DataGridView_Members.Columns[2].Width = 100;
            DataGridView_Members.Columns[3].HeaderText = "Date of Birth";
            DataGridView_Members.Columns[4].HeaderText = "Address";
            DataGridView_Members.Columns[4].Width = 100;
            DataGridView_Members.Columns[5].HeaderText = "Email";
            DataGridView_Members.Columns[5].Width = 200;
            DataGridView_Members.Columns[6].HeaderText = "Phone number";
            DataGridView_Members.Columns[7].HeaderText= "Position";
        }
        private void DataGridView_Members_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_Members.SelectedRows.Count != 0)
            {
                int ID_Person = Convert.ToInt32(DataGridView_Members.SelectedRows[0].Cells["ID_Person"].Value);
                Member member = BLL_Member.Instance.GetMemberByID(ID_Person);
                Set_Member(member);
            }
        }
        private void p_Look_MouseUp(object sender, MouseEventArgs e)
        {
            tb_Password.PasswordChar = '*';
        }

        private void p_Look_MouseDown(object sender, MouseEventArgs e)
        {
            tb_Password.PasswordChar = '\0';
        }
        private void bt_AddMember_Click(object sender, EventArgs e)
        {
            FormAddMember f = new FormAddMember();
            f.d = new FormAddMember.Mydel(ShowMembers);
            f.Show();
        }
        private void bt_DeleteMembers_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("      Are you sure delete Person ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (DataGridView_Members.SelectedRows.Count > 0)
                {
                    List<int> listDel = new List<int>();
                    foreach (DataGridViewRow i in DataGridView_Members.SelectedRows)
                    {
                        listDel.Add(Convert.ToInt32(i.Cells["ID_Person"].Value));
                    }
                    BLL_Member.Instance.DeleteMember_BLL(listDel);
                }
                ShowMembers();
            }
        }
        private void bt_Save_Click(object sender, EventArgs e)
        {
            if (DataGridView_Members.SelectedRows != null)
            {
                BLL_Member.Instance.UpdateMember_BLL(GetMember());
                ShowMembers();
            }
        }
        private void btReloand_Member_Click(object sender, EventArgs e)
        {
            ClearTextBoxMember();
            ShowMembers();
        }
        private void Search_Members_Click(object sender, EventArgs e)
        {
            ClearTextBoxMember();
            DataGridView_Members.DataSource = BLL_Member.Instance.FindPerson_BLL(tb_SearchMember.Text);
            DataGridView_Members.Columns[0].HeaderText = "ID";
            DataGridView_Members.Columns[0].Width = 100;
            DataGridView_Members.Columns[1].HeaderText = "Name";
            DataGridView_Members.Columns[2].HeaderText = "Gender";
            DataGridView_Members.Columns[2].Width = 100;
            DataGridView_Members.Columns[3].HeaderText = "Date of Birth";
            DataGridView_Members.Columns[4].HeaderText = "Address";
            DataGridView_Members.Columns[4].Width = 100;
            DataGridView_Members.Columns[5].HeaderText = "Email";
            DataGridView_Members.Columns[5].Width = 200;
            DataGridView_Members.Columns[6].HeaderText = "Phone number";
            DataGridView_Members.Columns[7].HeaderText = "Position";
        }
        private void tb_PublishingYear_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        //--------------------------------------------------------- BOOK

        #region BOOK
        
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
            MovePanel(bt_ManageBooks);
            Page_Admin.SetPage("Manage Books");
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

        #endregion

        //------------------------------------------------------------------------- SALE

        #region SALE

        public void ShowSachKhuyenMai()
        {
            DataGridView_SachKhuyenMai.DataSource = BLL_SachKhuyenMai.Instance.GetAllSachKhuyenMai_DAL();
            DataGridView_SachKhuyenMai.Columns[0].HeaderText = "ID Detail";
            DataGridView_SachKhuyenMai.Columns[1].HeaderText = "ID Book";
            DataGridView_SachKhuyenMai.Columns[2].HeaderText = "Discount";
            DataGridView_SachKhuyenMai.Columns[3].HeaderText = "Price";
            DataGridView_SachKhuyenMai.Columns[4].HeaderText = "Price after discount";
            DataGridView_SachKhuyenMai.Columns[5].HeaderText = "Start";
            DataGridView_SachKhuyenMai.Columns[6].HeaderText = "End";
            SetCombobox_SKM();
        }

        public void SetCombobox_SKM()
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

            cb_MaSach.Items.Clear();
            foreach (int i in list)
            {
                cb_MaSach.Items.Add(i);
            }
        }
        private SachKhuyenMai GetSachKhuyenMai()
        {
            SachKhuyenMai sachKM = new SachKhuyenMai();
            sachKM.ID_SachKhuyenMai = Convert.ToInt32(tb_IdDetail.Text);
            sachKM.MaSach = Convert.ToInt32(cb_MaSach.Text);
            sachKM.MucGiamGia = Convert.ToDouble(tb_Discount.Text);
            sachKM.NgayBatDau = DatePicker_StartDetail.Value;
            sachKM.NgayKetThuc = DatePicker_EndDetail.Value;
            sachKM.Gia = Convert.ToInt32(tb_Price.Text);
            return sachKM;
        }
        private void SetDetail_SKM(SachKhuyenMai sachKM)
        {
            tb_IdDetail.Text = sachKM.ID_SachKhuyenMai.ToString();
            cb_MaSach.Text = sachKM.MaSach.ToString();
            tb_Discount.Text = sachKM.MucGiamGia.ToString();
            tb_Price.Text = sachKM.Gia.ToString();
            tb_PriceAfterDiscount.Text = Convert.ToDouble((1 - sachKM.MucGiamGia) * sachKM.Gia).ToString();
            DatePicker_StartDetail.Value = sachKM.NgayBatDau;
            DatePicker_EndDetail.Value = sachKM.NgayKetThuc;
        }
        private void bt_AddSale_Click(object sender, EventArgs e)
        {
            FormAddSale f = new FormAddSale();
            f.d = new FormAddSale.Mydel(ShowSachKhuyenMai);
            f.ShowDialog();
        }
        private void bt_Sale_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Sale);
            Page_Admin.SetPage("Sale");
            ShowSachKhuyenMai();
        }

        private void DataGridView_SachKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (DataGridView_SachKhuyenMai.SelectedRows.Count != 0)
                {
                    int ID = Convert.ToInt32(DataGridView_SachKhuyenMai.SelectedRows[0].Cells["ID_SachKhuyenMai"].Value);
                    SachKhuyenMai sachKM = BLL_SachKhuyenMai.Instance.GetSachKhuyenMaiByID(ID);
                    SetDetail_SKM(sachKM);

                }
            }
        }

        private void bt_DeleteDiscount_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("      Are you sure delete Detail ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (DataGridView_SachKhuyenMai.SelectedRows.Count > 0)
                {
                    List<string> list = new List<string>();
                    foreach (DataGridViewRow i in DataGridView_SachKhuyenMai.SelectedRows)
                    {
                        list.Add(i.Cells["ID_SachKhuyenMai"].Value.ToString());
                    }
                    BLL_SachKhuyenMai.Instance.DeleteSachKhuyenMai_BLL(list);
                }
                ShowSachKhuyenMai();
            }
        }

        private void bt_SaveDiscount_Click(object sender, EventArgs e)
        {

            try
            {
                if (DataGridView_SachKhuyenMai.SelectedRows != null)
                {
                    foreach (int i in BLL_Sach.Instance.GetAllMaSach_BLL())
                    {
                        if (i != GetSachKhuyenMai().MaSach)
                        {
                            BLL_SachKhuyenMai.Instance.UpdateSachKhuyenMai_BLL(GetSachKhuyenMai());
                            ShowSachKhuyenMai();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kiểm tra lại Discount", "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            } 
        }
      
        private void cb_MaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_Price.Text = (BLL_Sach.Instance.GetSachByID(Convert.ToInt32(cb_MaSach.SelectedItem.ToString())).GiaBan).ToString();
            if (tb_Discount.Text != "")
            {
                double Discount = Convert.ToDouble(tb_Discount.Text);
                double Price = Convert.ToDouble(tb_Price.Text);
                double PriceAfterDiscount = (1- Discount) * Price;
                tb_PriceAfterDiscount.Text = PriceAfterDiscount.ToString();
            }                 
        }

        private void bt_ReloandSale_Click(object sender, EventArgs e)
        {
            ShowSachKhuyenMai();
        }

        private void tb_Discount_TextChange(object sender, EventArgs e)
        {
            try
            {
                double Discount = Convert.ToDouble(tb_Discount.Text);
                double Price = Convert.ToDouble(tb_Price.Text);
                double PriceAfterDiscount = (1 - Discount) * Price;
                tb_PriceAfterDiscount.Text = PriceAfterDiscount.ToString();
            }
            catch (Exception)
            {
                return;
            }

        }
        private void cb_MaSach_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void bt_BookDetails_Click(object sender, EventArgs e)
        {
            if (DataGridView_SachKhuyenMai.SelectedRows.Count != 0)
            {
                int ID_Sach = Convert.ToInt32(DataGridView_SachKhuyenMai.SelectedRows[0].Cells["MaSach"].Value);

                if (BLL_Sach.Instance.CheckStateSach_BLL(ID_Sach))
                {
                    new Book_Details(ID_Sach).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thông tin sách này đã bị xóa");
                }
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

        #endregion

        //------------------------------------------------------------------ HISTORY

        #region History
        private void bt_History_Click(object sender, EventArgs e)
        {
            MovePanel(bt_History);
            Page_Admin.SetPage("History");
            ShowHistory();
        }
        private void Reloand_History_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void ShowHistory()
        {
            DataGridView_History.DataSource = BLL_LichSuNhapSach.Instance.GetAllLichSuNhapSach_BLL();
            DataGridView_History.Columns[0].HeaderText = "ID";
            DataGridView_History.Columns[1].HeaderText = "ID Book";
            DataGridView_History.Columns[2].HeaderText = "Number of Book";
            DataGridView_History.Columns[3].HeaderText = "TIME";
            DataGridView_History.Columns[4].HeaderText = "ID Member";
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            DateTime from = DatePicker_1History.Value;
            DateTime to = DatePicker_2History.Value;
            if (from <= to)
            {
                DataGridView_History.DataSource = BLL_LichSuNhapSach.Instance.GetAllLichSuNhapSach_BLL(from, to);
                DataGridView_History.Columns[0].HeaderText = "ID";
                DataGridView_History.Columns[1].HeaderText = "ID Book";
                DataGridView_History.Columns[2].HeaderText = "Number of Book";
                DataGridView_History.Columns[3].HeaderText = "TIME";
                DataGridView_History.Columns[4].HeaderText = "ID Member";
            }
            else
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu hoặc ngày kết thúc");
            }    
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            if (DataGridView_History.SelectedRows.Count != 0)
            {
                int ID_Sach = Convert.ToInt32(DataGridView_History.SelectedRows[0].Cells["MaSach"].Value);
                if (BLL_Sach.Instance.CheckStateSach_BLL(ID_Sach))
                {
                    new Book_Details(ID_Sach).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thông tin sách này đã bị xóa");
                }
            }
      

        }


        #endregion

        //-------------------------------------------------------------- STATISTICS

        #region STATISTICS
        private void bt_Statistics_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Statistics);
            Page_Admin.SetPage("Statistics");
            ShowStatistics();
        }
        private void Reloand_Statistics_Click(object sender, EventArgs e)
        {
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            Statistics statistics = BLL_Statistics.Instance.GetStatistics();
            tb_SoLuongSachNhap.Text = statistics.SoSachNhap.ToString();
            tb_SoLuongSachBan.Text = statistics.SoSachBan.ToString();
            tb_ChiPhi.Text = statistics.ChiPhi.ToString();
            tb_DoanhThu.Text = statistics.DoanhThu.ToString();
            tb_SoLuongHoaDon.Text = statistics.SoHoaDon.ToString();
            //---DESIGN %

            #region design
            int a = statistics.SoSachBan;
            int b = statistics.SoSachNhap;
            double c = 0;
            if (b != 0 && a < b)
            {
                c = (Math.Round((double)a / b, 3) * 100);
                CircleProgress_SoLuongDaBan.Value = Convert.ToInt32(c);
            }
            if (b != 0 && a > b)
            {
                CircleProgress_SoLuongDaBan.Value = 100;
            }
            if(b==0)
            {
                CircleProgress_SoLuongDaBan.Value = 0;
            }

            decimal x = statistics.DoanhThu;
            decimal y = statistics.ChiPhi;
            double z = 0;
            if (y != 0 && x < y)
            {
                z = (Math.Round(Convert.ToDouble(x / y), 3) * 100);
                CircleProgress_DoanhThu.Value = Convert.ToInt32(z);
            }
            if (y != 0 && x > y)
            {
                CircleProgress_DoanhThu.Value = 100;
            }
            if(y==0)
            {
                CircleProgress_DoanhThu.Value = 0;
            }

            chart1.Series["Revenue"].Points.Clear();
            chart1.Series["Expense"].Points.Clear();

            DatePickerStatistics_Start.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            DatePickerStatistics_End.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);     

            int month = DatePickerStatistics_Start.Value.Month;
            int count = 0;
            foreach (decimal i in BLL_Statistics.Instance.GetDoanhThuTheoThang_BLL(DatePickerStatistics_Start.Value, DatePickerStatistics_End.Value))
            {
                chart1.Series["Revenue"].Points.AddXY($"{month}", i);
                chart1.Series["Revenue"].Points[count].Label = i.ToString();
                month++;
                count++;
            }
            month = DatePickerStatistics_Start.Value.Month;
            foreach (decimal i in BLL_Statistics.Instance.GetChiPhiTheoThang_BLL(DatePickerStatistics_Start.Value, DatePickerStatistics_End.Value))
            {
                chart1.Series["Expense"].Points.AddXY($"{month}", i);
                month++;
            }


            #endregion

            //----------
        }
        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if(DatePickerStatistics_Start.Value <= DatePickerStatistics_End.Value)
            {
                Statistics statistics = BLL_Statistics.Instance.GetStatistics(DatePickerStatistics_Start.Value, DatePickerStatistics_End.Value);
                tb_SoLuongSachNhap.Text = statistics.SoSachNhap.ToString();
                tb_SoLuongSachBan.Text = statistics.SoSachBan.ToString();
                tb_ChiPhi.Text = statistics.ChiPhi.ToString();
                tb_DoanhThu.Text = statistics.DoanhThu.ToString();
                tb_SoLuongHoaDon.Text = statistics.SoHoaDon.ToString();
                //---DESIGN %

                #region design
                int a = statistics.SoSachBan;
                int b = statistics.SoSachNhap;
                double c = 0;
                if (b != 0 && a < b)
                {
                    c = (Math.Round((double)a / b, 3) * 100);
                    CircleProgress_SoLuongDaBan.Value = Convert.ToInt32(c);
                }
                if (b != 0 && a > b)
                {
                    CircleProgress_SoLuongDaBan.Value = 100;
                }
                if (b == 0)
                {
                    CircleProgress_SoLuongDaBan.Value = 0;
                }

                decimal x = statistics.DoanhThu;
                decimal y = statistics.ChiPhi;
                double z = 0;
                if (y != 0 && x < y)
                {
                    z = (Math.Round(Convert.ToDouble(x / y), 3) * 100);
                    CircleProgress_DoanhThu.Value = Convert.ToInt32(z);
                }
                if (y != 0 && x > y)
                {
                    CircleProgress_DoanhThu.Value = 100;
                }
                if (y == 0)
                {
                    CircleProgress_DoanhThu.Value = 0;
                }

                chart1.Series["Revenue"].Points.Clear();
                chart1.Series["Expense"].Points.Clear();
       
                int month = DatePickerStatistics_Start.Value.Month;
                int count = 0;

                foreach (decimal i in BLL_Statistics.Instance.GetDoanhThuTheoThang_BLL(DatePickerStatistics_Start.Value, DatePickerStatistics_End.Value))
                {
                    chart1.Series["Revenue"].Points.AddXY($"{month}", i);
                    chart1.Series["Revenue"].Points[count].Label = i.ToString();
                    month++;
                    count++;
                }

                month = DatePickerStatistics_Start.Value.Month;
                foreach (decimal i in BLL_Statistics.Instance.GetChiPhiTheoThang_BLL(DatePickerStatistics_Start.Value, DatePickerStatistics_End.Value))
                {
                    chart1.Series["Expense"].Points.AddXY($"{month}", i);
                    month++;    
                }

                #endregion

                //----------
            }
            else { MessageBox.Show("Kiểm tra lại ngày bắt đầu hoặc ngày kết thúc"); }

        }


        #endregion

        //------------------------------------------------------------------- BILL

        #region BILL
        private void bt_Bill_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Bill);
            Page_Admin.SetPage("Bill");
            ShowAllBill();
        }

        private void Reloand_Bill_Click(object sender, EventArgs e)
        {
            ShowAllBill();
            DataGridView_DetailBill.DataSource = 0;
        } 

        private void bt_Detail_Click(object sender, EventArgs e)
        {
            if (DataGridView_Bill.SelectedRows.Count != 0)
            {
                int MaHoaDon = 0;
                foreach (DataGridViewRow i in DataGridView_Bill.SelectedRows)
                {
                MaHoaDon = Convert.ToInt32(i.Cells["MaHoaDon"].Value);
                }
                ShowAllBill_Detail(MaHoaDon);
            }  
        }

        private void ShowAllBill_Detail(int MaHoaDon)
        {
            DataGridView_DetailBill.DataSource = BLL_Bill.Instance.GetListBill_Detail_Views(MaHoaDon);
            DataGridView_DetailBill.Columns[0].HeaderText = "ID Bill";
            DataGridView_DetailBill.Columns[1].HeaderText = "ID Book";
            DataGridView_DetailBill.Columns[2].HeaderText = "Name Book";
            DataGridView_DetailBill.Columns[3].HeaderText = "Number";
            DataGridView_DetailBill.Columns[4].HeaderText = "Discount";
            DataGridView_DetailBill.Columns[5].HeaderText = "Total";
        }

        private void ShowAllBill()
        {
            DataGridView_Bill.DataSource = BLL_Bill.Instance.GetAllBill_BLL();
            DataGridView_Bill.Columns[0].HeaderText = "ID Bill";
            DataGridView_Bill.Columns[1].HeaderText = "Date";
            DataGridView_Bill.Columns[2].HeaderText = "Total";
            DataGridView_Bill.Columns[3].HeaderText = "ID Member";
        }

        private void bt_BillOk_Click(object sender, EventArgs e)
        {
            DateTime from = DatePicker_1Bill.Value;
            DateTime to   = DatePicker_2Bill.Value;

            if(from <= to)
            {
                DataGridView_DetailBill.DataSource = 0;
                DataGridView_Bill.DataSource = BLL_Bill.Instance.GetAllBill_BLL(from,to);
                DataGridView_Bill.Columns[0].HeaderText = "ID Bill";
                DataGridView_Bill.Columns[1].HeaderText = "DATE";
                DataGridView_Bill.Columns[2].HeaderText = "Amount";
                DataGridView_Bill.Columns[3].HeaderText = "ID Member";
            } 
            else
            {
                MessageBox.Show("Kiểm tra lại ngày bắt đầu hoặc ngày kết thúc");
            }    
        }

   
        #endregion

        //-----------------------------------------------------------------------------SALES

        #region Sales

        private void bt_Sales_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Sales);
            Page_Admin.SetPage("Sales");
            ShowAllSales();
        }

        public void ShowAllSales()
        {
            dgv_Sales.DataSource = BLL_Sales.Instance.GetAllSales_BLL();
            dgv_Sales.Columns[0].HeaderText = "ID Sales";
            dgv_Sales.Columns[1].HeaderText = "Name";
            dgv_Sales.Columns[2].HeaderText = "Total Revenue";
            dgv_Sales.Columns[3].HeaderText = "Total Invoidce";
        }

  
        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            ShowAllSales();
            DataGridView_Sales_Bill.DataSource = 0;
        }

        private void dgv_Sales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Sales.SelectedRows.Count != 0)
            {
                int ID_Sales = Convert.ToInt32(dgv_Sales.SelectedRows[0].Cells[0].Value);
                List<Bill_Detail_View> list = new List<Bill_Detail_View>();

                foreach (int i in BLL_Sales.Instance.GetAllID_Bill_BLL(ID_Sales))
                {
                   list.AddRange(BLL_Bill.Instance.GetListBill_Detail_Views(i));    
                }
                DataGridView_Sales_Bill.DataSource = list;
                DataGridView_Sales_Bill.Columns[0].HeaderText = "ID Bill";
                DataGridView_Sales_Bill.Columns[1].HeaderText = "ID Book";
                DataGridView_Sales_Bill.Columns[2].HeaderText = "Name Book";
                DataGridView_Sales_Bill.Columns[3].HeaderText = "Number";
                DataGridView_Sales_Bill.Columns[4].HeaderText = "Discount";
                DataGridView_Sales_Bill.Columns[5].HeaderText = "Total";
            }
        }


        private void bt_Sort_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                List<Sales> list = BLL_Sales.Instance.GetAllSales_BLL();
                if (comboBox1.SelectedIndex == 0)
                {
                    list.Sort((x, y) => x.TongTien.CompareTo(y.TongTien));
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    list.Sort((x, y) => x.SoHoaDon.CompareTo(y.SoHoaDon));
                }
                dgv_Sales.DataSource = list;
            }


        }

        #endregion


    }
}
