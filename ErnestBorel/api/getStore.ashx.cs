using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getStore
    /// </summary>
    public class getStore : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            string type = context.Request["type"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;

            string city_idx = context.Request["id"];
            city_idx = (String.IsNullOrEmpty(city_idx)) ? "hong_kong" : city_idx;

            //For XML generate 
            bool addSplit = (String.IsNullOrEmpty(context.Request["addSplit"])) ? false : true;

            JObject output = DBHelper.getStore(lang, city_idx, addSplit, type);
            output["ts"] = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000000;


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