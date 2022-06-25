using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DTO.DTO_Login
{
    internal class Account
    {
        public int ID_Account { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ID_Person { get; set; }
        public int ID_Position { get; set; }
    }
}
