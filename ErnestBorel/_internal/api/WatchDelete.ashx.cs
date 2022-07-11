using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for WatchDelete
    /// </summary>
    public class WatchDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();

            string id = "";
            id = context.Request["id"];

            if (String.IsNullOrEmpty(id))
            {
                output.message = "ID invalid";
                Helper.writeOutput(output);
                context.Response.End();
            }

            bool isDeleted = DBHelper.deleteWatch(id);
            if (isDeleted)
            {
                output.status = (int)StatusType.success;
            }
            else
            {
                output.message = "Record not found";
            }

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