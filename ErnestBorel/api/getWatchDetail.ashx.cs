using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getWatchDetail
    /// </summary>
    public class getWatchDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            string watch = context.Request["watch"];

            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            watch = (String.IsNullOrEmpty(watch)) ? "" : watch;

            BasicOutput output = new BasicOutput();
            try
            {
                output.data = DBHelper.getWatchDetail(watch, lang);
                output.status = (int)StatusType.success;
            }
            catch (Distributor.WatchDetailNotFound ex)
            {
                output.data = null;
            }
            finally
            {
                context.Response.Write(JsonConvert.SerializeObject(output));
            }

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
