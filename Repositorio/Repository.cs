using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Configuration;

namespace Repo
{
    public class Repository
    {
        public static string DATABASE_NAME = "forms";
        public static string CONNECTION => ConfigurationManager.ConnectionStrings["forms"].ConnectionString;

        private static SqlConnection openCon()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNECTION;
            conn.Open();

            return conn;
        }

        private static void closeCon(SqlConnection con)
        {
            con.Close();
            con.Dispose();
        }


        public DataTable query(SqlCommand query)
        {
            SqlConnection con = openCon();
            SqlDataAdapter adapter = new SqlDataAdapter();
            query.Connection = con;
            adapter.SelectCommand = query;

            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);


            closeCon(query.Connection);
            if (table != null)
            {
                return table;
            }
            return null;
        }

        public String buildWhereClause(String[] selection, String[] whereClause, String TABLE_NAME)
        {

            StringBuilder cmd = new StringBuilder(
                    "SELECT *  FROM " + TABLE_NAME + " WHERE "
                );

            if (selection != null && selection.Length > 0)
            {
                cmd.Replace("*", String.Join(",", selection));
            }

            if (whereClause.Any())
            {
                cmd.Append(String.Join("AND ", whereClause));
            }

            return cmd.ToString();

        }

        protected long update(String table, Dictionary<String,Object> contentValues,String[] conditions)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(table);
            sql.Append(" SET ");
            Object[] bindArgs = null;
            int size = (contentValues != null && contentValues.Count > 0)
                ? contentValues.Count : 0;
            if(size > 0)
            {
                bindArgs = new Object[size];
                int i = 0;
                foreach(String colName in contentValues.Keys)
                {
                    sql.Append((i > 0) ? "," : "");
                    sql.Append(colName);
                    sql.Append(" = '");
                    sql.Append(contentValues[colName]);
                    sql.Append("'");
                    i++;
                }

                if(conditions.Length > 0)
                {
                    i = 0;
                    foreach(String c in conditions)
                    {
                        sql.Append((i == 0) ? " WHERE " : " ");
                        sql.Append(c);
                        i++;
                    }
                }
            }

            sql.Append(";");
            SqlCommand cmd = new SqlCommand(sql.ToString())
            {
                CommandType = CommandType.Text,
                Connection = openCon()
            };

            long result = cmd.ExecuteNonQuery();
            closeCon(cmd.Connection);
            return result;
        }

        protected long insert(String table, Dictionary<String, Object> values)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT");
            sql.Append(" INTO ");
            sql.Append(table);
            sql.Append('(');

            Object[] bindArgs = null;
            int size = (values != null && values.Count > 0)
                        ? values.Count : 0;
            if (size > 0)
            {
                bindArgs = new Object[size];
                int i = 0;
                foreach (String colName in values.Keys)
                {
                    sql.Append((i > 0) ? "," : "");
                    sql.Append(colName);
                    bindArgs[i++] = values[colName];
                }

                sql.Append(") VALUES (");
                for (i = 0; i < size; i++)
                {
                    sql.Append((i > 0) ? ",'" : "'");
                    sql.Append(bindArgs[i]);
                    sql.Append("'");
                }

                sql.Append(");");
                SqlCommand cmd = new SqlCommand(sql.ToString());
                cmd.CommandType = CommandType.Text;
                cmd.Connection = openCon();

                long result = cmd.ExecuteNonQuery();
                closeCon(cmd.Connection);
                return result;

            }
            else
            {
                throw new ArgumentNullException("Values can not be null");
            }

        }


    }
}