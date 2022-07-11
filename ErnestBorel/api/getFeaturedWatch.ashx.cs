using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getFeaturedWatch
    /// </summary>
    public class getFeaturedWatch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.success;
            output.data = DBHelper.getFeaturedWatch(lang);

            context.Response.Write(JsonConvert.SerializeObject(output));
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
