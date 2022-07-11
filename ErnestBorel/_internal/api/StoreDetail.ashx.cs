using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for StoreDetail
    /// </summary>
    public class StoreDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = 0;
            BasicOutput output = new BasicOutput();


            int.TryParse(context.Request["id"], out id);

            if (id == 0)
            {
                output.message = "Invaild Id";
                Helper.writeOutput(output);
                context.Response.End();
            }

            StoreModel model = DBHelper.getStore(id);

            output.status = (int)StatusType.success;
            output.data = model;
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