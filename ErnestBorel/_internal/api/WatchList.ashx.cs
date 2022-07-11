using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for WatchList
    /// </summary>
    public class WatchList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();

            string Lang = context.Request["lang"];
            string Id = context.Request["id"];
            
            #region Check Variable
            if (String.IsNullOrEmpty(Id) || String.IsNullOrEmpty(Lang))
            {
                output.status = (int)StatusType.error;
                output.message = "input variable not enough";
                Helper.writeOutput(output);
                context.Response.End();
            }
            #endregion
            
            DataTable _table = DBHelper.searchWatch(Lang, Id);
            var col = DBHelper.getCollectionDetail(Lang, Id);
            output.message = col.name;
            output.data = _table;
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