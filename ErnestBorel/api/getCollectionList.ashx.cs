using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getCollectionList
    /// </summary>
    public class getCollectionList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string collection = context.Request["idx_collection"];
            bool is_sale = false;

            if (!String.IsNullOrEmpty(context.Request["is_sale"]))
            {
                try
                {
                    is_sale = (Convert.ToInt32(context.Request["is_sale"]) == 1);
                }
                catch { }
            }

            BasicOutput output = new BasicOutput();
            bool isValidInput = !(String.IsNullOrEmpty(collection) || string.IsNullOrWhiteSpace(collection));
            if (isValidInput)
            {
                JObject list = DBHelper.getCollectionList(collection, is_sale);
                
                output.data = list;
                output.status = (int)StatusType.success;
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
