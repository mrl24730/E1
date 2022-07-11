using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for CityList
    /// </summary>
    public class CitySearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();

            string Lang = context.Request["lang"];
            string Name = context.Request["name"];
            string Id = context.Request["id"];
            string condition = "";


            if (!String.IsNullOrEmpty(Id))
            {
                condition += " AND idx_city = '" + Id + "'";
            }

            if (!String.IsNullOrEmpty(Name))
            {
                condition += " AND city_name like N'%" + Name + "%'";
            }

            if (!String.IsNullOrEmpty(Lang) && Lang != "all")
            {
                condition += " AND idx_lang = '" + Lang+ "'";
            }
            

            string sql = @"SELECT * From tbl_city WHERE 1=1 {0}";
            sql = String.Format(sql, condition);

            DataTable _table = DBHelper.searchCity(sql);

            output.data = _table;
            //output.message = sql;
            output.status = (int)StatusType.success;
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