using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebFormsService.Models;

namespace Repo
{
    public class ContactsRepository : Repository
    {
        public static string TABLE_NAME = "Contactos";

        /*TABLE FIELDS*/
        public static string NUMERO = "Numero";
        public static string DIRECCIONES = "Direcciones";
        public static string NOMBRES = "Nombres";
        public static string PAIS = "Pais";
        public static string ID = "ID";

        Dictionary<String, String> _contentValues = new Dictionary<string, string>();

        public List<Contacts> GetAll()
        {
            SqlCommand command = new SqlCommand(
                    "SELECT * FROM " + TABLE_NAME
                );

            DataTable dt = base.query(command);

            return (from DataRow dr in dt.Rows select new Contacts(dr[NUMERO].ToString(), dr[DIRECCIONES].ToString(), dr[NOMBRES].ToString(), dr[PAIS].ToString())).ToList();
        }

        public List<Contacts> Get(String[] whereClause, String[] selection)
        {
            String cmd = base.buildWhereClause(selection, whereClause, TABLE_NAME);
            SqlCommand command = new SqlCommand(cmd);
            DataTable dt = base.query(command);

            return (from DataRow dr in dt.Rows select new Contacts(checkSelection(selection, NUMERO) ? dr[NUMERO].ToString() : "", checkSelection(selection, DIRECCIONES) ? dr[DIRECCIONES].ToString() : "", checkSelection(selection, NOMBRES) ? dr[NOMBRES].ToString() : "", checkSelection(selection, PAIS) ? dr[PAIS].ToString() : "")).ToList();
        }

        private static bool checkSelection(string[] selection, string searchValue)
        {
            return selection == null || selection.Any(searchValue.Contains);
        }
    }
}