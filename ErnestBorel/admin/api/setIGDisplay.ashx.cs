using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel.admin.api
{
    /// <summary>
    /// Summary description for setIGDisplay
    /// </summary>
    public class setIGDisplay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            HttpServerUtility server = context.Server;

            response.ContentType = "text/plain";

            string idx_photo = String.IsNullOrEmpty(request["idx_photo"]) ? null : request["idx_photo"];
            bool is_display = false;

            if (!String.IsNullOrEmpty(request["is_display"]))
            {
                Boolean.TryParse(request["is_display"], out is_display);
            }

            //Session check

            string result = "{\"success\":0,\"msg\":\"Missing params\"}";
            if (idx_photo != null)
            {
                DBHelper.setIGDisplay(idx_photo, is_display);
                result = "{\"success\":1,\"msg\":\"OK\"}";
            }

            response.Write(result);
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