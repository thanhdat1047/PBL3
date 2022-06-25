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

namespace QuanLyThuVienSach.GUI.GUI_BOOKSALESMAN
{
    public partial class Saleman : Form
    {

        public Saleman()
        {
            InitializeComponent();
            MovePanel(btnResume);
        }
        int movex, movey, move;
        public int ID_AccountL { get; set; }
        public int ID_PersonL { get; set; }
        //ham nay tao sua
        public Saleman(int ID_account, int ID_person)
        {
            InitializeComponent();
            ID_AccountL = ID_account;
            ID_PersonL = ID_person;
            ReloadResume();
            MovePanel(btnResume);
        }

        //-------------------------------------------------------------- SET DESIGN

        #region SetDesign
        private void MovePanel(Control c)
        {
            PanelSale.Height = (c.Height - 15);
            PanelSale.Top = (c.Top + 8);
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
        //ham nay tao sua
        private void bt_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Resume

        private void btnResume_Click(object sender, EventArgs e)
        {
            ReloadResume();
            MovePanel(btnResume);
            Page_SaleMan.SetPage("Resume");
        }
        private void bt_EditResume_Click(object sender, EventArgs e)
        {

            EditResume f = new EditResume(ID_PersonL);
            f.d = new EditResume.Mydel(ReloadResume);
            f.ShowDialog();
        }
        //ham nay tao sua
        private void ReloadResume()
        {
            SetResume(BLL_Member.Instance.GetMemberByID(ID_PersonL));
        }



        private void bt_ChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword(ID_AccountL).ShowDialog();
        }

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
        #endregion

        #region Infor
        private void btnInfor_Click(object sender, EventArgs e)
        {
            MovePanel(btnInfor);
            Page_SaleMan.SetPage("Information");
            Showinfor();

        }
        //ham nay tao sua
        private void btnSearchInfor_Click(object sender, EventArgs e)
        {
            dgvinfor.DataSource = BLL_Member.Instance.FindPerson_BLL(txtSearchInfor.Text);
        }

        //ham nay tao sua
        public void Showinfor()
        {
            dgvinfor.DataSource = BLL_Member.Instance.GetMemberIsCustomer();
            dgvinfor.Columns[0].HeaderText = "ID";
            dgvinfor.Columns[0].Width = 100;
            dgvinfor.Columns[1].HeaderText = "Name";
            dgvinfor.Columns[2].HeaderText = "Gender";
            dgvinfor.Columns[2].Width = 100;
            dgvinfor.Columns[3].HeaderText = "Date of Birth";
            dgvinfor.Columns[4].HeaderText = "Address";
            dgvinfor.Columns[4].Width = 100;
            dgvinfor.Columns[5].HeaderText = "Email";
            dgvinfor.Columns[5].Width = 200;
            dgvinfor.Columns[6].HeaderText = "Phone number";
            dgvinfor.Columns[7].HeaderText = "Position";
            dgvinfor.Columns[8].HeaderText = "ID Account";
        }

        int idcus = 0;
        int idcusold = 0;
        private void dgvinfor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idcusold = idcus;
            int idPerson = Convert.ToInt32(dgvinfor.SelectedRows[0].Cells["ID_Person"].Value);
            idcus = idPerson;
            Person p = BLL_Person.Instance.GetPersonByID(idPerson);

            textName.Text = p.Name_Person;
            textAddress.Text = p.Address;
            textEmail.Text = p.Email;
            textGender.Text = p.Gender;
            textPhone.Text = p.PhoneNumber;
            DatePicker1.Value = p.DateOfBirth;

        }
        bool isChange = false;
        private void btnOkinfor_Click(object sender, EventArgs e)
        {
            //ok
            
            if (idcus != 0)
            {
                Page_SaleMan.SetPage("Manage Books");
                showBook();
                int idPerson = Convert.ToInt32(dgvinfor.SelectedRows[0].Cells["ID_Person"].Value);

                int idcusnew = idPerson;
                if(idcusold != idcusnew && idcusold !=0 && isChange == true )
                {
                    
                    DialogResult dialogResult = MessageBox.Show("     Are you sure change your id ?", "Confirm ", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        BLL_Sach.Instance.RemoveList_Oder();
                        
                    }
                       
                }
                else
                {

                    isChange = true;
                }
               
            }
            else
            {
                MessageBox.Show("Chua Du Thong Tin");

            }


        }
        //ham nay tao sua
        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            //create

            FormAddMember f = new FormAddMember(ID_PersonL);
            f.d = new FormAddMember.Mydel(Showinfor);
            f.ShowDialog();

        }

        #endregion

        #region history
        private void btnHistory_Click(object sender, EventArgs e)
        {

            MovePanel(btnHistory);
            Page_SaleMan.SetPage("History");
            Showhis();

        }
        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            //ok time 
            if (DatetimefromHis.Value > DatetimeToHis.Value)
            {

                MessageBox.Show("Nhap ngay sai, vui long nhap lai !!!");
            }
            else
            {
                dgvHis.DataSource = null;

                dgvHis.DataSource = BLL_LichSuThanhToan.Instance.GetLSBSbyDateAndID(ID_PersonL, DatetimefromHis.Value, DatetimeToHis.Value);
                dgvHis.Columns[0].HeaderText = "ID History";

                dgvHis.Columns[1].HeaderText = "ID Person";
                dgvHis.Columns[2].HeaderText = "Name's Saleman";

                dgvHis.Columns[3].HeaderText = "ID Bill";
                dgvHis.Columns[4].HeaderText = "Date";

                dgvHis.Columns[5].HeaderText = "Total";
                lablePrint.Text = BLL_LichSuThanhToan.Instance.GetTotal(ID_PersonL, DatetimefromHis.Value, DatetimeToHis.Value).ToString();
            }

        }
        public void Showhis()
        {
            dgvHis.DataSource = null;
            dgvHis.DataSource = BLL_LichSuThanhToan.Instance.GetALLLichSuBanSachByID(ID_PersonL);
            dgvHis.Columns[0].HeaderText = "ID History";

            dgvHis.Columns[1].HeaderText = "ID Person";
            dgvHis.Columns[2].HeaderText = "Name's Saleman";

            dgvHis.Columns[3].HeaderText = "ID Bill";
            dgvHis.Columns[4].HeaderText = "Date";

            dgvHis.Columns[5].HeaderText = "Total";
        }
        #endregion

        #region Bill
        private void btnBill_Click(object sender, EventArgs e)
        {
            MovePanel(btnBill);
            Page_SaleMan.SetPage("Bill");
            ShowBill();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int MaHD = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value);
            dgvChiTiet.DataSource = BLL_Bill.Instance.GetListBill_Detail_Views(MaHD);

        }

        private void bt_BillOk_Click(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = BLL_Bill.Instance.SearchAllBillDate_BLL(DatePicker_1Bill.Value, DatePicker_2Bill.Value);

        }



        public void ShowBill()
        {
            dgvHoaDon.DataSource = BLL_Bill.Instance.GetAllBill_BLL();
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = BLL_Bill.Instance.SearchBillByID_BLL(Convert.ToUInt16(txtSearch.Text));
        }


        private void btnChose_Click(object sender, EventArgs e)
        {
            if (dgvInforBook.SelectedRows.Count > 0)
            {
                bt_Buy.Enabled = true;
                bt_Remove.Enabled = true;
                foreach (DataGridViewRow i in dgvInforBook.SelectedRows)
                {
                    var t = i.Cells["MaSach"].Value.ToString();
                    BLL_Sach.Instance.Getdatapage2(t);
                }
            }
        }
        private void ToExcel(DataGridView dataGridView1, string fileName)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Quản lý thu vien";

                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //gọi hàm ToExcel() với tham số là dtgDSHS và filename từ SaveFileDialog
                ToExcel(dgvChiTiet, saveFileDialog1.FileName);
            }
        }
        #endregion

        #region Createbill


            private void bt_Buy_Click(object sender, EventArgs e)
            {
                DialogResult dialogResult = MessageBox.Show("     Are you sure Buy ?", "Confirm delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if(idcus != 0 )
                    {
                        dgv3.DataSource = 0;
                        List<Sach_View_Cus> data = BLL_Sach.Instance.DataPage2();
                        BLL_Bill.Instance.CreateBill(data, idcus);

                        int MHD = BLL_Bill.Instance.SelectMHD(idcus);
                        BLL_LichSuThanhToan.Instance.CreateNew(ID_PersonL, MHD);

                        BLL_Sach.Instance.RemoveList_Oder();
                        isChange = false;
                        idcus = 0;
                        ShowBill();
                        MovePanel(btnBill);
                        Page_SaleMan.SetPage("Bill");
                }
                else
                {
                    MessageBox.Show("Chua du thong tin ");
                }
                   
                   

                }
            }

        private void bt_Remove_Click(object sender, EventArgs e)
        {
            if (dgv3.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow item in dgv3.SelectedRows)
                {
                    var t = Convert.ToInt32(item.Cells["MaSach"].Value.ToString());
                    BLL_Sach.Instance.DelSachinPage2(t);
                }
            }
            ShowPage3();

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

        private void bt_Payments_Click(object sender, EventArgs e)
        {
            Page_SaleMan.SetPage("CreateBill");
            ShowPage3();
        }

        #endregion

        #region ManagerBook
        private void btnManager_Click(object sender, EventArgs e)
        {

            MovePanel(btnManager);
            Page_SaleMan.SetPage("Manage Books");
            showBook();
        }
        public void showBook()
        {
            dgvInforBook.DataSource = BLL_Sach.Instance.Getallbookdetail();
            dgvInforBook.Columns[0].HeaderText = "ID Book";
            dgvInforBook.Columns[1].HeaderText = "Name Book";
            dgvInforBook.Columns[1].Width = 150;
            dgvInforBook.Columns[2].HeaderText = "Category";
            dgvInforBook.Columns[2].Width = 100;
            dgvInforBook.Columns[3].HeaderText = "Author";
            dgvInforBook.Columns[4].HeaderText = "Year";
            dgvInforBook.Columns[5].HeaderText = "Price";
        }

        private void bt_ViewCart_Click(object sender, EventArgs e)
        {           
            Page_SaleMan.SetPage("CreateBill");
            ShowPage3();
        }

        private void btReloand_Member_Click(object sender, EventArgs e)
        {
            showBook();
        }

        //ham nay tao sua
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
        private void bunifuButton22_Click(object sender, EventArgs e)
        {

            //findsach
            string txt = tb_SearchBookMana.Text;
            dgvInforBook.DataSource = BLL_Sach.Instance.FindSach_BLL(txt);

        }
        #endregion


       

    }

}

