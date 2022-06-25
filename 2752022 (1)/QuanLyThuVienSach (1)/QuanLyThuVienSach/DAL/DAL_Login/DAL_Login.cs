using QuanLyThuVienSach.DTO.DTO_Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_Login
{
    internal class DAL_Login
    {
        private static DAL_Login _Instance;
        public static DAL_Login Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_Login();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_Login() { }
        public DataTable GetAllAccount_DAL()
        {
            string query = $"SELECT Account.ID_Account,UserName,Password,ID_Position,ID_Person FROM Account,Person " +
                           $"WHERE Account.ID_Account = Person.ID_Account";
            return DBHelper.Instance.GetRecord(query);
        }
        public void AddAccount_DAL(Account account)
        {
            string query = $"INSERT INTO dbo.Account VALUES( '{account.UserName}', '{account.Password}', 4 , 1 )";
            DBHelper.Instance.ExecuteDB(query);
        }
        public void DeleteAccount_DAL(int ID)
        {
            string query = $"Delete from Account where ID_Account = {ID}";
            DBHelper.Instance.ExecuteDB(query);
        }
        public void AddPerson_DAL(Person person)
        {
            string query1 = "SELECT TOP 1 ID_Account FROM Account ORDER BY ID_Account DESC";
            int ID_Account = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                ID_Account = Convert.ToInt32(i[0]);
            }
            string query2 = $"INSERT INTO dbo.Person VALUES ( '{person.Name_Person}', {Convert.ToInt32(person.Gender)}, '{person.DateOfBirth}', '{person.Address}', '{person.Email}', '{person.PhoneNumber}', {ID_Account})";
            DBHelper.Instance.ExecuteDB(query2);
        }
        public int GetIDAccount_DAL()
        {
            string query1 = "SELECT TOP 1 ID_Account FROM Account ORDER BY ID_Account DESC";
            int ID_Account = 0;
            foreach (DataRow i in DBHelper.Instance.GetRecord(query1).Rows)
            {
                ID_Account = Convert.ToInt32(i[0]);
            }
            return ID_Account;
        }

    }
}
