
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebFormsService.Repository;

namespace Repo
{
    public class ClientesRepository : Repository
    {
        public static string TABLE_NAME = "Clientes";

        /*FIELDS*/
        public static string PAIS = "Pais";
        public static string NOMBRE = "Nombres";
        public static string NUMERO = "Numero";
        public static string ID = "ID";

        public List<Clients> GetAll()
        {
            SqlCommand command = new SqlCommand(
                    "SELECT * FROM " + TABLE_NAME + ";"
                );

            DataTable dt = base.query(command);

            return (from DataRow dr in dt.Rows select new Clients(dr[PAIS].ToString(), dr[NOMBRE].ToString(), dr[NUMERO].ToString())).ToList();
        }

        public List<Clients> Get(String[] whereClause, String[] selection)
        {

            SqlCommand command = new SqlCommand(base.buildWhereClause(selection, whereClause, TABLE_NAME));
            DataTable dt = base.query(command);

            return (from DataRow dr in dt.Rows select new Clients(selection == null 
                    || selection.Any(PAIS.Contains) ? dr[PAIS].ToString() : "", selection == null 
                    || selection.Any(NOMBRE.Contains) ? dr[NOMBRE].ToString() : "", selection == null 
                    || selection.Any(NUMERO.Contains) ? dr[NUMERO].ToString() : "")).ToList();
        }
    }
}