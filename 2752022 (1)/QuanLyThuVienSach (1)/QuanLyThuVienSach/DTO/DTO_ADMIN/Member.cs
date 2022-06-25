using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_ADMIN
{
    internal class Member
    {
        public int ID_Account { get; set; }
        public int ID_Person { get; set; }
        public int ID_Position { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }  
        public string Name_Person { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name_Position { get; set; }

    }
}
