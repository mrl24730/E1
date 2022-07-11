using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getDistributorSearch
    /// </summary>
    public class getDistributorSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.message = "";

            try
            {
                string data = context.Request["data"];
                JObject json = JObject.Parse(data);
                string keyword = json["keyword"].ToObject<string>();
                string lang = json["lang"].ToObject<string>();
                bool is_sale = true;
                DataTable _table = new DataTable();
                DBHelper.getWatchByKeyword(lang, keyword, out _table, is_sale);

                output.data =_table;
                output.status = (int)StatusType.success;
            }catch(Exception e)
            {
                output.status = (int)StatusType.error;
            }
            finally {
                response.Write(JsonConvert.SerializeObject(output));
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
