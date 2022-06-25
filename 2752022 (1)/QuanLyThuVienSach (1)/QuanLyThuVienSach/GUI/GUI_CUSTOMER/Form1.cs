using QuanLyThuVienSach.BILL.BILL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using QuanLyThuVienSach.GUI.GUI_ADMIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach.GUI.GUI_CUSTOMER
{
    public partial class Form1 : Form
    {
        int movex, movey, move;
        public int ID_AccountL { get; set; }
        public int ID_PersonL { get; set; }
       
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(int ID_Account, int ID_Person)
        {
            InitializeComponent();
            ID_AccountL = ID_Account;
            ID_PersonL = ID_Person;
            MovePanel(bt_Resume);
            comboBox_Find.Items.Add("All");
            comboBox_Find.SelectedIndex = 0;
            ReloadResume();
            bt_Buy.Enabled = false;
            bt_Remove.Enabled = false;
            bt_ViewCart.Enabled = false;
            pt_Sale.Visible = false;

        }
        string _ThuocTinh = "TenTacGia";

        #region DESIGN
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
        private void MovePanel(Control c)
        {
            Panel_Cus.Height = (c.Height - 15);
            Panel_Cus.Top = (c.Top + 8);
        }
        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
        private void bt_Resume_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Resume);
            bunifuPages1.SetPage("tabPage1");
        }
        private void bt_ManageBooks_Click(object sender, EventArgs e)
        {

            MovePanel(bt_MgBook);
            bunifuPages1.SetPage("tabPage2");
            ShowManageBook();
        }

        private void SetCombobox(string ThuocTinh)
        {
            comboBox_Find.Items.Clear();
            comboBox_Find.Items.Add("All");
            foreach (string i in BLL_Sach.Instance.GetAllThongTin_ThuocTinh_BLL(ThuocTinh).Distinct())
            {
                comboBox_Find.Items.Add(i);
            }
            ;
        }

        private void bt_ViewCart_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("tabPage3");
            ShowPage3();
        }
        private void bt_Transactionhistory_Click(object sender, EventArgs e)
        {
            MovePanel(bt_Transactionhistory);
            bunifuPages1.SetPage("tabPage4");
            DateTime from = DatePicker_1Bill.Value;
            DateTime to = DatePicker_2Bill.Value;
            if (from <= to)
            {
                DataGridView_DetailBill.DataSource = 0;
                DataGridView_Bill.DataSource = BLL_Bill.Instance.GetIDPersonBill_BLL(from, to, ID_PersonL);
                DataGridView_Bill.Columns[0].HeaderText = "ID Bill";
                DataGridView_Bill.Columns[1].HeaderText = "DATE";
                DataGridView_Bill.Columns[2].HeaderText = "Amount";
                DataGridView_Bill.Columns[3].HeaderText = "ID Member";
            }
        }
        #endregion

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
        private void ReloadResume()
        {
            SetResume(BLL_Member.Instance.GetMemberByID(ID_PersonL));
        }
        private void Design_ColumnHeader()
        {
            DataGridView_Book.Columns[0].HeaderText = "ID Book";
            DataGridView_Book.Columns[1].HeaderText = "Name Book";
            DataGridView_Book.Columns[1].Width = 150;
            DataGridView_Book.Columns[2].HeaderText = "Category";
            DataGridView_Book.Columns[2].Width = 100;
            DataGridView_Book.Columns[3].HeaderText = "Author";
            DataGridView_Book.Columns[4].HeaderText = "Year";
            DataGridView_Book.Columns[5].HeaderText = "Price";
        }
        private void ShowManageBook()
        {
            DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSachCus();
            Design_ColumnHeader();
        }
        private void SetDetail_Sach(Sach_View sach)
        {
            tb_Cetegory.Text = sach.TheLoai.ToString();
            tb_Author.Text = sach.TenTacGia.ToString();
            tb_Price.Text = sach.GiaBan.ToString();
            tb_PublishingYear.Text = sach.NamXuatBan.ToString();
            tb_NumberOfReprints.Text = sach.SoLanTaiBan.ToString();
            tb_NameBook.Text = sach.TenSach.ToString();
            tb_discount.Text = ((float)BLL_SachKhuyenMai.Instance.GetGiamGia_BLL(sach.MaSach)).ToString();
            tb_PrAfterDiscount.Text = (sach.GiaBan * (1 - Convert.ToDouble(tb_discount.Text))).ToString();
        }
        public void ShowPage3()
        {
            dgv3.DataSource = null;
            List<Sach_View_Cus> data = BLL_Sach.Instance.DataPage2();
            dgv3.DataSource = data;
            data = null;
            dgv3.Columns[0].HeaderText = "ID Book";
            dgv3.Columns[1].HeaderText = "Name Book";
            dgv3.Columns[1].Width = 150;
            dgv3.Columns[3].HeaderText = "Category";
            dgv3.Columns[3].Width = 100;
            dgv3.Columns[2].HeaderText = "Author";
            dgv3.Columns[4].HeaderText = "Number of reprints";
            dgv3.Columns[5].HeaderText = "Year";
            dgv3.Columns[6].HeaderText = "Selling Price";
            dgv3.Columns[7].HeaderText = "Number";
        }

        private void ShowBill_Detail(int MaHoaDon)
        {
            DataGridView_DetailBill.DataSource = BLL_Bill.Instance.GetListBill_Detail_Views(MaHoaDon);
            DataGridView_DetailBill.Columns[0].HeaderText = "ID Bill";
            DataGridView_DetailBill.Columns[1].HeaderText = "ID Book";
            DataGridView_DetailBill.Columns[3].HeaderText = "Name Book";
            DataGridView_DetailBill.Columns[4].HeaderText = "Number";
            DataGridView_DetailBill.Columns[5].HeaderText = "Discount";

        }
        private void bt_EditResume_Click(object sender, EventArgs e)
        {
            EditResume f = new EditResume(ID_PersonL);
            f.d = new EditResume.Mydel(ReloadResume);
            f.ShowDialog();
        }
        private void bt_ReloandBook_Click(object sender, EventArgs e)
        {
            ShowManageBook();
        }
        private void bt_ChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword(ID_AccountL).ShowDialog();
        }

        private void bt_FindBook_Click(object sender, EventArgs e)
        {
            string Find = tb_SearchBook.Text.ToString();
            if (Find != "")
            {
                string txt = comboBox_Find.SelectedItem.ToString();
                DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSachCus(Find,_ThuocTinh,txt);
                Design_ColumnHeader();
            } 
        }

        private void DataGridView_Book_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_Book.SelectedRows.Count != 0)
            {
                int ID_Sach = Convert.ToInt32(DataGridView_Book.SelectedRows[0].Cells["MaSach"].Value);
                Sach_View sach = BLL_Sach.Instance.GetSachByID(ID_Sach);

                pt_Sale.Visible = false; 
                float Discount = (float)BLL_SachKhuyenMai.Instance.GetGiamGia_BLL(ID_Sach);
                if (Discount != 0)
                {
                    pt_Sale.Visible = true;
                }  
                
                SetDetail_Sach(sach);
            }
        }
  

        private void bt_Tang_Click(object sender, EventArgs e)
        {
            if (dgv3.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dgv3.SelectedRows)
                {
                    var t = item.Cells["MaSach"].Value.ToString();
                    BLL_Sach.Instance.Getdatapage2(t);
                }
            }
            ShowPage3();
        }

        private void bt_Giam_Click(object sender, EventArgs e)
        {
            if (dgv3.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dgv3.SelectedRows)
                {
                    var t = item.Cells["MaSach"].Value.ToString();
                    BLL_Sach.Instance.Tdatapage2(t);
                }
            }
            ShowPage3();
        }

        private void bt_Remove_Click(object sender, EventArgs e)
        {
            if (dgv3.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dgv3.SelectedRows)
                {
                    int t = Convert.ToInt32(item.Cells["MaSach"].Value.ToString());
                    BLL_Sach.Instance.DelSachinPage2(t);
                }
            }

            List<Sach_View_Cus> data = BLL_Sach.Instance.DataPage2();

            if (data.Count == 0)
            {
                bt_ViewCart.Enabled = false;
            }    

            ShowPage3();

        }

        private void bt_Buy_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("     Are you sure Buy ?", "Confirm delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<Sach_View_Cus> data = BLL_Sach.Instance.DataPage2();
                if (BLL_Bill.Instance.CheckBuy(data) != 0)
                {
                    int idboolnot = BLL_Bill.Instance.CheckBuy(data);
                    MessageBox.Show($" Book {BLL_Sach.Instance.GetSachbyMaSach(idboolnot.ToString()).TenSach} is not in enough quantity");

                }
                else
                {
                    dgv3.DataSource = 0; 
                    BLL_Bill.Instance.CreateBill(data, ID_PersonL);
                    BLL_Sach.Instance.RemoveList_Oder();
                }
                
            }
        }

        private void bt_BillOk_Click(object sender, EventArgs e)
        {
            DateTime from = DatePicker_1Bill.Value;
            DateTime to = DatePicker_2Bill.Value;
            if (from <= to)
            {
                DataGridView_DetailBill.DataSource = 0;
                DataGridView_Bill.DataSource = BLL_Bill.Instance.GetIDPersonBill_BLL(from, to, ID_PersonL);
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

        private void bt_Detail_Click(object sender, EventArgs e)
        {
            if (DataGridView_Bill.SelectedRows.Count != 0)
            {
                int MaHoaDon = 0;
                foreach (DataGridViewRow i in DataGridView_Bill.SelectedRows)
                {
                    MaHoaDon = Convert.ToInt32(i.Cells["MaHoaDon"].Value);
                }
                ShowBill_Detail(MaHoaDon);
            }
        }

        private void comboBox_Find_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridView_Book.DataSource = BLL_Sach.Instance.GetAllSachCus(_ThuocTinh,comboBox_Find.SelectedItem.ToString());
            Design_ColumnHeader();
        }

        private void bt_Author_Click(object sender, EventArgs e)
        {
            SetCombobox("Author");
            _ThuocTinh = "TenTacGia";
            comboBox_Find.SelectedIndex = 0;
        }

        private void bt_Cetegory_Click(object sender, EventArgs e)
        {
            SetCombobox("Cetegory");
            _ThuocTinh = "Theloai";
            comboBox_Find.SelectedIndex = 0;

        }

        private void bt_BACK_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("tabPage2");
            ShowManageBook();
        }

        private void bt_Year_Click(object sender, EventArgs e)
        {
            SetCombobox("Year");
            _ThuocTinh = "NamXuatBan";
            comboBox_Find.SelectedIndex = 0;
        }

        private void bt_AddToCart_Click(object sender, EventArgs e)
        {
            bt_ViewCart.Enabled = true;
            if (DataGridView_Book.SelectedRows.Count > 0)
            {
                bt_Buy.Enabled = true;
                bt_Remove.Enabled = true;
                foreach (DataGridViewRow i in DataGridView_Book.SelectedRows)
                {
                    var t = i.Cells["MaSach"].Value.ToString();
                    BLL_Sach.Instance.Getdatapage2(t);
                }
            }
        }


    }
}
