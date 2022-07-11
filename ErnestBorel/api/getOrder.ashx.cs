using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Kitchen;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getOrder
    /// </summary>
    public class getOrder : ErnestBorel.Distributor.ApiHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            context.Response.ContentType = "application/json";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            try
            {
                string data = context.Request["data"];
                JObject json = JObject.Parse(data);
                string email = json["email"].ToObject<string>();
                string idcard = json["idcard"].ToObject<string>();
                string e_email = CryptoHelper.encryptAES(email, DBHelper.defaultSKey, DBHelper.fixedIV);
                string lang = json["lang"].ToObject<string>();
                
                var orders = DBHelper.getOrder(e_email, idcard, lang);
                output.data = orders;

                if(orders.Count > 0)
                {
                    output.status = (int)StatusType.success;
                }

            }
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }

            Helper.writeOutput(output);

        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}