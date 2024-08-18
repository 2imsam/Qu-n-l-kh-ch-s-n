using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom_04_QLKS
{
    internal class DBConnect
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-OD90OK56;Initial Catalog=QL_KHACHSAN2;Integrated Security=True");

        public DBConnect()
        {

        }
        public void Open()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

        }
        public void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public int getNonQuery(string sql)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

                int kq = cmd.ExecuteNonQuery();
            conn.Close();

            return kq;
        }

        public object getScalar(string sql)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            object kq = cmd.ExecuteScalar();
            conn.Close();
            return kq;
        }

        public DataTable getTable(string sql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            da.Fill(ds);
            return ds.Tables[0];
        }
        public int updateTable(DataTable dtnew, string chuoitruyvan)
        {
            SqlDataAdapter da = new SqlDataAdapter(chuoitruyvan, conn);//dữ liệu cũ chưa cập nhật
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            int kq = da.Update(dtnew);
            return kq;
        }



    }
}
