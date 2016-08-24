using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebFormsService.Models;
using WebFormsService.Storage;

namespace Repo
{
    public class MailRepository : Repository
    {
        public static String TABLE_NAME = "Emails";

        public static String HAS_ATTACHMENT = "HasAttachment";
        public static String RECIPIENTS = "Recipients";
        public static String SENDER = "Sender";
        public static String SENDER_NAME = "SenderName";
        public static String FILES = "Files";
        public static String BODY = "Body";
        public static String ID = "Id";
        public static String HAS_SENT = "hasSent";
        public static String PASSWD = "Passwd";

        TextFile file;
        ImageFile imageF;

        public long Insert(Mail email)
        {
            Dictionary<String, Object> contentValues = new Dictionary<string, Object>();

            string[] fImage = new string[email.files.Count() + 1];
            fImage[0] = createTXTFile(email);
            int x = 1;
            foreach (File f in email.files)
            {

                fImage[x++] = createImageFile(f.blob, f.name);
            }
            contentValues.Add(FILES, string.Join(",", (fImage)));
            contentValues.Add(SENDER_NAME, email.sender.name);
            contentValues.Add(SENDER, email.sender.emailAddress);
            contentValues.Add(RECIPIENTS, string.Join(",", email.recipients));
            contentValues.Add(BODY, email.body);
            contentValues.Add(PASSWD, email.sender.passwd);

            return this.insert(TABLE_NAME, contentValues);
        }


        public long Update(Mail email)
        {
            Dictionary<String, Object> contentValues = new Dictionary<string, object> {{HAS_SENT, "1"}};

            String[] conditions = new String[]
            {
                ID + " = '" + email.id + "'" 
            };

            return this.update(TABLE_NAME, contentValues,conditions);
        }



        public Mail[] Select(string[] selection, string[] selectionArgs)
        {
            SqlCommand cmd = new SqlCommand(base.buildWhereClause(selection, selectionArgs, TABLE_NAME));
            DataTable dt = base.query(cmd);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            Mail[] sentItems = new Mail[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                String[] files_blob = dr[FILES].ToString().Split(',');
                List<File> files = files_blob.Select(f => new File()
                {
                    name = f
                }).ToList();

                Sender sender = new Sender();
                sender.emailAddress = dr[SENDER].ToString();
                sender.passwd = dr[PASSWD].ToString();

                String rec = dr[RECIPIENTS].ToString();
                Mail mailVM = new Mail(dr[BODY].ToString(), rec, sender, files);
                mailVM.id = Convert.ToInt32(dr[ID]);
                sentItems[i++] = mailVM;
            }

            return sentItems;
        }

        private string createImageFile(string blob, string name)
        {
            imageF = new ImageFile();
            imageF.fileName = name;
            imageF.path = System.IO.Path.GetDirectoryName(string.Format("~/App_Data"));
            imageF.Write(blob);
            return imageF.fileName;
        }

        private string createTXTFile(Mail email)
        {
            file = new TextFile();
            int index = 0;
            string fechaForName = email.form.txtFecha;

            while (fechaForName.IndexOf('-') != -1)
            {
                index = fechaForName.IndexOf('-');
                fechaForName = fechaForName.Remove(index, 1);
            }
            while (fechaForName.IndexOf(':') != -1)
            {
                index = fechaForName.IndexOf(':');
                fechaForName = fechaForName.Remove(index, 1);
            }
            file.fileName = ("WebformsLog_" + fechaForName + email.sender.CSRCode).Trim();
            file.ext = ".txt";
            file.path = System.IO.Path.GetDirectoryName(string.Format("~/App_Data"));
            file.Write(email);

            return (file.fileName + file.ext).Trim();
        }
    }
}