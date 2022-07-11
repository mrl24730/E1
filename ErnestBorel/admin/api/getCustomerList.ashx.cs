using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel.admin.api
{
    /// <summary>
    /// Summary description for getCustomerList
    /// </summary>
    public class getCustomerList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;

            output.message = "";
            if (context.Session["logined"] != null)
            {
                

                try
                {
                    output.status = (int)StatusType.success;
                    output.data = DBHelper.getCustomerList();

                }
                catch (Exception e)
                {
                    output.message = "Input error: " + e.Message;
                    Helper.writeOutput(output);
                    response.End();
                }
            }
            else
            {
                output.status = (int)StatusType.noAuthorization;
                output.message = "Login required.";
            }

            Helper.writeOutput(output);
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