using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVienSach.DAL.DAL_ADMIN
{
    internal class DBHelper
    {
        private string cnnstring;

        private static DBHelper _Instance;
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DBHelper();
                }
                return _Instance;
            }
            private set { }
        }
        private DBHelper()
        {
            //cnnstring = @"Data Source=DESKTOP-BP0TSS8;Initial Catalog=QuanLyThuVienSach;Integrated Security=True";
            cnnstring = @"Data Source=LAPTOP-TUNGSDPF\SQLEXPRESS;Initial Catalog=QuanLyThuVienSach;Integrated Security=True";
           // cnnstring = @"Data Source=HAUTRI\SQLEXPRESS;Initial Catalog=QuanLyThuVienSach;Integrated Security=True";
        }
        public bool ExecuteDB(string query)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnstring))
                {
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public DataTable GetRecord(string query)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnstring))
                {
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cnn.Open();
                    da.Fill(dt);
                    cnn.Close();
                    return dt;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
   
}
