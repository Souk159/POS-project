using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace POS
{
    internal class classConnectDatabase
    {
        public string strcon = "data source=LAPTOP-JL7A7VB3\\SQLEXPRESS; initial catalog=Mini2CPR; integrated security=true";
        public SqlConnection conn = new SqlConnection();
        public SqlDataAdapter da = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        public SqlCommand cmd = new SqlCommand();

        public void ConnectDatabase()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.ConnectionString = strcon;
            conn.Open();

        }
    }
}
