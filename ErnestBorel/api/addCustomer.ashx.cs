using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for addCustomer
    /// </summary>
    public class addCustomer : ErnestBorel.Distributor.ApiHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            Customer input = new Customer();

            try
            {
                string data = context.Request["data"];
                input = JsonConvert.DeserializeObject<Customer>(data);
                input.email = input.email.ToLower();
            }
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }
            
            int idx_customer = DBHelper.checkCustomer(input);
            if (idx_customer == 0)
            {
                //new customer
                idx_customer = DBHelper.insertCustomer(input);
            }

            if (idx_customer > 0)
            {
                output.status = (int)StatusType.success;
                output.data = idx_customer;
            }
            else
            {
                output.message = "Error during insert";
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