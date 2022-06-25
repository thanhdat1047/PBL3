using QuanLyThuVienSach.DAL.DAL_Login;
using QuanLyThuVienSach.DTO.DTO_Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BLL_Login
{
    internal class BLL_Login
    {
        private static BLL_Login _Instance;
        public static BLL_Login Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Login();
                }
                return _Instance;
            }
            private set { }
        }

        private BLL_Login() { }
        public List<Account> GetAllAccount_BLL()
        {
            List<Account> list = new List<Account>();
            foreach (DataRow i in DAL_Login.Instance.GetAllAccount_DAL().Rows)
            {
                Account account = new Account();
                account.ID_Account = Convert.ToInt32(i[0]);
                account.UserName = i[1].ToString();
                account.Password = i[2].ToString();
                account.ID_Position = Convert.ToInt32(i[3]);
                account.ID_Person = Convert.ToInt32(i[4]);
                list.Add(account);
            }
            return list;
        }
        public void AddAccount_BLL(Account account)
        {
            DAL_Login.Instance.AddAccount_DAL(account);
        }
        public int GetIDAccount_BLL()
        {
            return DAL_Login.Instance.GetIDAccount_DAL();
        }
        public void DeleteAccount_BLL(int id)
        {
            DAL_Login.Instance.DeleteAccount_DAL(id);
        }  
        public void AddPerson(Person person)
        {
            DAL_Login.Instance.AddPerson_DAL(person);
        }

    }

}
