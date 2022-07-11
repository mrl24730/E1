using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for StoreRestore
    /// </summary>
    public class StoreRestore : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();

            int id = 0;
            int.TryParse(context.Request["id"], out id);

            if (id == 0)
            {
                output.message = "ID invalid";
                Helper.writeOutput(output);
                context.Response.End();
            }

            bool isRestored = DBHelper.restoreStore(id);
            if (isRestored)
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