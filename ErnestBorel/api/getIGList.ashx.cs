using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getIGList
    /// </summary>
    public class getIGList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            HttpServerUtility server = context.Server;

            bool isAdminMode = false;
            int paging = 0;
            if (request["adminMode"] == InstagramHelper.APIadminKey)
            {
                isAdminMode = true;
            }

            if (request["paging"] != null)
            {
                Int32.TryParse(request["paging"], out paging);
            }

            string idx_photo = String.IsNullOrEmpty(request["idx_photo"]) ? "" : request["idx_photo"];

            response.Write(JsonConvert.SerializeObject(DBHelper.getIGList(isAdminMode, paging, idx_photo), Formatting.Indented));
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