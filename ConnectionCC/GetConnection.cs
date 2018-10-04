using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionCC
{
    public class GetConnection
    {
        public DataTable execSqlReturnDataTable(string qry, string con)
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(qry, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //public DataTable execProcedureReturnDataTable(string procedureName, string con)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(con))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(qry, conn))
        //        {
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //        }
        //    }
        //    return dt;
        //}

        public string testConnection(string con)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();

                return "connection success";
            }
        }
    }
}
