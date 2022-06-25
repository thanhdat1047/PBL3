using QuanLyThuVienSach.GUI;
using QuanLyThuVienSach.GUI.GUI_CUSTOMER;
using QuanLyThuVienSach.GUI.GUI_THUKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienSach
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            Application.Run(new Login());
 
        }
    }
}
