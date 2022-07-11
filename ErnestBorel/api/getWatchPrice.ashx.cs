using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getWatchPrice
    /// </summary>
    public class getWatchPrice : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            
            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.success;
            output.data = DBHelper.getWatchPrice(lang);

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
