using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel.admin.api
{
    /// <summary>
    /// Summary description for getOrderDetail
    /// </summary>
    public class getOrderDetail : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                OrderADetail orderDetail = new OrderADetail();


                if (String.IsNullOrEmpty(context.Request["idx_order"]))
                {
                    Helper.writeOutput(output);
                    response.End();
                }
                
                try
                {
                    int idx_order = 0;
                    int.TryParse(context.Request["idx_order"], out idx_order);
                    orderDetail.idx_order = idx_order;

                    
                    DBHelper.getOrderDetail(ref orderDetail);
                    
                    output.status = (int)StatusType.success;
                    output.data = orderDetail;

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