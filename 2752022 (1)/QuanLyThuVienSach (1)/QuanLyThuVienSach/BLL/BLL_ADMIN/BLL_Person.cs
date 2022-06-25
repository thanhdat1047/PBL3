using QuanLyThuVienSach.DAL.DAL_ADMIN;
using QuanLyThuVienSach.DTO.DTO_ADMIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.BILL.BILL_ADMIN
{
    class BLL_Person
    {
        private static BLL_Person _Instance;
        public static BLL_Person Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Person();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_Person() { }

        public DataTable GetAllMembers_BLL()
        {
            return DAL_Member.Instance.GetAllMembers_DAL();
        }

        public DataTable FindPerson_BLL(string txt)
        {
            return DAL_Member.Instance.FindPerson_DAL(txt);
        }

        public void DeleteMember_BLL(List<int> ListDel)
        {
            foreach (int ID_Person in ListDel)
            {
                DAL_Member.Instance.DeleteMembers_DAL(ID_Person);
            }
        }
        public void UpdateMember_BLL(Member member)
        {
            DAL_Member.Instance.UpdateMember_DAL(member);
        }
        public List<Member> ListAllMembers_BLL()
        {
            List<Member> list = new List<Member>();
            foreach (DataRow i in GetAllMembers_BLL().Rows)
            {
                list.Add(new Member
                {
                    ID_Person = Convert.ToInt32(i["ID_Person"]),
                    Name_Person = i["Name_Person"].ToString(),
                    Gender = i["Gender"].ToString(),
                    DateOfBirth = Convert.ToDateTime(i["DateOfBirth"]),
                    Address = i["Address"].ToString(),
                    Email = i["Email"].ToString(),
                    PhoneNumber = i["PhoneNumber"].ToString(),
                    Name_Position = i["Name_Position"].ToString(),

                });
            }
            return list;
        }

        public Member GetMemberByID(int ID_Person, int ID_Account)
        {
            Member member = new Member();
            foreach (Member i in ListAllMembers_BLL())
            {
                if (i.ID_Person == ID_Person && i.ID_Account == ID_Account)
                {
                    member = i;
                }
            }
            return member;
        }


        public void AddMember_BLL(Member member)
        {
            DAL_Member.Instance.AddMember_DAL(member);
        }

        public void UpdatePerson_BLL(Person person)
        {
            DAL_Member.Instance.UpdatePerson(person);
        }

        public void UpdateAccount_BLL(Account account)
        {
            DAL_Member.Instance.UpdateAccount(account);
        }

        public Person GetPersonByID(int ID)
        {
            Person person = new Person();
            foreach (Member i in ListAllMembers_BLL())
            {
                if (i.ID_Person == ID)
                {
                    person.Address = i.Address;
                    person.PhoneNumber = i.PhoneNumber;
                    person.ID_Person = ID;
                    person.DateOfBirth = i.DateOfBirth;
                    person.Email = i.Email;
                    person.Gender = i.Gender;
                    person.Name_Person = i.Name_Person;
                }
            }
            return person;
        }

        public Account GetAccountByID(int ID)
        {
            Account account = new Account();
            foreach (Member i in ListAllMembers_BLL())
            {
                if (i.ID_Account == ID)
                {
                    account.ID_Account = ID;
                    account.UserName = i.UserName;
                    account.Password = i.Password;

                }
            }
            return account;
        }




    }
}
