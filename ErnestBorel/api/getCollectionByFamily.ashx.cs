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
    /// Summary description for getCollectionByFamily
    /// </summary>
    public class getCollectionByFamily : IHttpHandler
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
                string family = json["family"].ToObject<string>();
                string lang = json["lang"].ToObject<string>();
                bool is_sale = true;

                DataTable _table = new DataTable();
                DBHelper.GetCollectionByFamily(lang, family, out _table, is_sale);

                output.data = _table;
                output.status = _table.Rows.Count > 0 ? (int)StatusType.success : (int)StatusType.error;
            }
            catch (Exception e)
            {
                output.message = e.Message;
            }
            finally
            {
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
