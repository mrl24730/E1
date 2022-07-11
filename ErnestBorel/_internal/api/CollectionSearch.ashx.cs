using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for CollectionSearch
    /// </summary>
    public class CollectionSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();

            string Lang = context.Request["lang"];
            string Type = context.Request["type"];
            string Name = context.Request["name"];
            string Col_ref = context.Request["col_ref"];
            string condition = "";

            #region Check Variable
            if (String.IsNullOrEmpty(Lang))
            {
                output.status = (int)StatusType.error;
                output.message = "input variable not enough";
                Helper.writeOutput(output);
                context.Response.End();
            }
            #endregion

            if (!String.IsNullOrEmpty(Name))
            {
                condition += "AND col_name like N'%" + Name + "%'";
            }

            if (!String.IsNullOrEmpty(Col_ref))
            {
                condition += "AND col_ref like N'%" + Col_ref + "%'";
            }

            if (!String.IsNullOrEmpty(Type))
            {
                condition += "AND col_movement = '" + Type+ "'";
            }


            string sql = @"SELECT * From vw_collection WHERE is_deleted = 0 AND idx_lang = '{0}' {1}";
            sql = String.Format(sql, Lang, condition);

            DataTable _table = DBHelper.searchStore(sql);

            output.data = _table;
            output.message = sql;
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