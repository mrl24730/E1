using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for WatchDetail
    /// </summary>
    public class WatchDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = "";
            BasicOutput output = new BasicOutput();

            id = context.Request["id"];

            if (String.IsNullOrEmpty(id))
            {
                output.message = "Invaild Id";
                Helper.writeOutput(output);
                context.Response.End();
            }
            
            WatchModel model = DBHelper.getWatch(id);
            CollectionModel col = DBHelper.getCollection(model.idx_collection);
            model.collection = col.name_en;
            model.lastupdate = model.lastupdate.AddHours(8);

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