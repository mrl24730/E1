using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getCityList
    /// </summary>
    public class getCityList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            string type = context.Request["type"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;

            //            var output = DBHelper.getCityList(lang, type);
            var output = DBHelper.getCityList(lang, type);
            output.ts = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000000;

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