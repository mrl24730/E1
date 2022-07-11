using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal
{
    /// <summary>
    /// Summary description for fastUpdate
    /// </summary>
    public class fastUpdate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string sql_select = "SELECT MAX(idx_country) country, idx_city FROM tbl_store GROUP BY idx_city";

            DataTable table = DBHelper.ExecuteReader2DataTable(sql_select);

            int totalRowAffected = 0;
            foreach(DataRow r in table.Rows)
            {
                string country = (string)r["country"];
                string idx_city = (string)r["idx_city"];
                string sql_update = "UPDATE tbl_city SET idx_country = '" + country + "' WHERE idx_city = '" + idx_city + "'";
                totalRowAffected += DBHelper.ExecuteUpdate(sql_update);
            }

            context.Response.Write("Total updated: " + totalRowAffected + " rows");
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