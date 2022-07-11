using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

using Kitchen;

namespace ErnestBorel.admin_warranty
{
    /// <summary>
    /// Summary description for UpdateCase
    /// </summary>
    public class UpdateCase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();
            DateTime start = DateTime.ParseExact("2016-02-01 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact("2016-02-29 23:59:59", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            DataSet ds = DBHelper.searchWarranty(start, end);
            DataTable _table = ds.Tables[0];

            string updateString = "UPDATE tbl_warranty_registration SET Name = '{name}', Email = '{email}' WHERE idx_warranty = {idx}; ";
            string sql = "";

            foreach (DataRow r in _table.Rows)
            {
                long idx = (long)r["Idx"];
                WarrantyRecord record = new WarrantyRecord();
                record.Name = ((string)r["Name"]).ToUpper();
                record.Email = ((string)r["Email"]).ToLower();
                record.Name = CryptoHelper.encryptAES(record.Name, DBHelper.defaultSKey);
                if (!String.IsNullOrEmpty(record.Email))
                {
                    record.Email = CryptoHelper.encryptAES(record.Email, DBHelper.defaultSKey);
                }

                FastReplacer fr = new FastReplacer("{", "}");
                fr.Append(updateString);
                fr.Replace("{name}", record.Name);
                fr.Replace("{email}", record.Email);
                fr.Replace("{idx}", idx.ToString());
                sql += fr.ToString();
            }

            int count = DBHelper.updateWarranty(sql);
            output.data = "Updated " + count + " records";
            Helper.writeOutput(output);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}