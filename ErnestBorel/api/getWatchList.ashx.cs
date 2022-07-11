using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getWatchList
    /// </summary>
    public class getWatchList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            string watchList = context.Request["watch"];
            JArray output = new JArray();

            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            watchList = (String.IsNullOrEmpty(watchList)) ? "" : watchList;

            if (!string.IsNullOrEmpty(watchList) && watchList != "null")
            {
                JObject json = JObject.Parse(watchList);

                foreach (var watch in json.Properties())
                {
                    WatchDetail _res = DBHelper.getWatchDetail(watch.Name, lang);
                    output.Add(JToken.FromObject(_res));
                }
            }

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
