using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Web.Helpers;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace prjWeb.Models
{
    public class CCustomerFactory
    {

        public List<CCustomer> queryAll()
        {
            string sql = "SELECT * FROM tCustomer ";
            return queryBySql(sql, null);
        }

        public CCustomer queryById(int fId)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            List<CCustomer> list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }

        private List<CCustomer> queryBySql(string sql, List<SqlParameter> paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=MVCdbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CCustomer x = new CCustomer()
                {
                    fEmail = reader["fEmail"].ToString(),
                    fPhone = reader["fPhone"].ToString(),
                    fName = reader["fName"].ToString(),
                    fId = reader["fid"].ToString(),
                    fAddress = reader["fAddress"].ToString(),
                    fPassword = reader["fPassword"].ToString()
                };
                list.Add(x);
            }
            con.Close();
            return list;
        }

        public void update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " UPDATE tCustomer SET ";  //sql語法前後加空白，防止出錯
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "fName=@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "fPhone=@K_FPhone,";
                paras.Add(new SqlParameter("K_FPhone", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "fEmail=@K_FEmail,";
                paras.Add(new SqlParameter("K_FEmail", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "fAddress=@K_Faddress,";
                paras.Add(new SqlParameter("K_Faddress", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "fPassword=@K_FPassword,";
                paras.Add(new SqlParameter("K_FPassword", (object)p.fPassword));
            }
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " WHERE fId=@K_FID ";
            paras.Add(new SqlParameter("K_FID", (object)p.fId));
            executeSql(sql, paras);
        }

        public void delete(int id)
        {
            string sql = " DELETE FROM tCustomer WHERE fId=@K_FID ";  //sql語法前後加空白，防止出錯

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)id));
            executeSql(sql, paras);
        }

        private static void executeSql(string sql, List<SqlParameter> para)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=MVCdbDemo;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(para.ToArray());
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void create(CCustomer p)
        {
            string sql = "INSERT INTO tCustomer(";
            sql += "fName,";
            sql += "fPhone,";
            sql += "fEmail,";
            sql += "fAddress,";
            sql += "fPassword";
            sql += ")VALUES(";

            sql += "@K_FNAME,";
            sql += "@K_FPHONE, ";
            sql += "@K_EMAIL, ";
            sql += "@K_ADDRESS, ";
            sql += "@K_PASSWORD) ";

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter(("@K_FNAME"), ifNull(p.fName)));
            paras.Add(new SqlParameter(("@K_FPHONE"), ifNull(p.fPhone)));
            paras.Add(new SqlParameter(("@K_EMAIL"), ifNull(p.fEmail)));
            paras.Add(new SqlParameter(("@K_ADDRESS"), ifNull(p.fAddress)));
            paras.Add(new SqlParameter(("@K_PASSWORD"), ifNull(p.fPassword)));
            executeSql(sql, paras);
        }

        private object ifNull(object p)
        {
            if (string.IsNullOrEmpty((string)p))
                return DBNull.Value;
            else
                return p;
        }

        public void create老師的(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "Insert Into tCustomer(";
            if (!string.IsNullOrEmpty(p.fName))
                sql += "fName,";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += "fPhone,";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += "fEmail,";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += "fAddress,";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += "fPassword,";
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")VALUES(";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@K_FPhone,";
                paras.Add(new SqlParameter("K_FPhone", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@K_FEmail,";
                paras.Add(new SqlParameter("K_FEmail", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@K_Faddress,";
                paras.Add(new SqlParameter("K_Faddress", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@K_FPassword,";
                paras.Add(new SqlParameter("K_FPassword", (object)p.fPassword));
            }
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            executeSql(sql, paras);
        }
    }
}
