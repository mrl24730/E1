using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getLocation
    /// </summary>
    public class getLocation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            string type = context.Request["type"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;

            //            var output = DBHelper.getCityList(lang, type);
            var output = DBHelper.getLocation(lang, type);
            output.ts = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000000;

            Helper.writeOutput(output, true);
            //context.Response.Write(JsonConvert.SerializeObject(output));
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