using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getLocation
    /// </summary>
    public class getLocationFull : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            locationOutput output = new locationOutput();
            string lang = context.Request["lang"];
            string type = context.Request["type"];
            string idx_city = context.Request["id"];
            bool is_pos = false;
            bool is_aftersales = false;

            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            idx_city = (String.IsNullOrEmpty(idx_city)) ? "hong_kong" : idx_city;
            type = (String.IsNullOrEmpty(type)) ? "all" : type;

            if(type == "pos")
            {
                is_pos = true;
            }

            if (type == "network")
            {
                is_aftersales = true;
            }

            output.data = new DataTable();
            DBHelper.getLocation(lang, idx_city, ref output.data, is_pos, is_aftersales);
            
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