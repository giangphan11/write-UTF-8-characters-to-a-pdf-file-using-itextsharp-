using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ConnectionAccess
    {
        protected SqlConnection conn;
        string strconn = "Server=GIANGPHAN; Database=CSDL_QuanLyThuVien; User=sa; Password=20061998;MultipleActiveResultSets=True";

        public void openConnection()
        {
            if(conn==null)
                conn = new SqlConnection(strconn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
        public void closeConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

    }
}
