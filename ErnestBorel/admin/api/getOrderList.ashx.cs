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
    public class getOrderList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
				OrderAList orderList = new OrderAList();

                int year = 0;
                int month = 0;
                int day = 0;
                string[] dateFromAry = context.Request["df"].Split('-');
                int.TryParse(dateFromAry[0], out year);
                int.TryParse(dateFromAry[1], out month);
                int.TryParse(dateFromAry[2], out day);
                DateTime targetDateFrom = new DateTime(year, month, day);

                string[] dateToAry = context.Request["dt"].Split('-');
                int.TryParse(dateToAry[0], out year);
                int.TryParse(dateToAry[1], out month);
                int.TryParse(dateToAry[2], out day);
                DateTime targetDateTo = new DateTime(year, month, day);
                targetDateTo = targetDateTo.AddDays(1).AddMilliseconds(-1);

                string companyName_aes = context.Request["name"] ?? "";
                if (!string.IsNullOrEmpty(companyName_aes))
                {
                    companyName_aes = CryptoHelper.encryptAES(context.Request["name"], DBHelper.defaultSKey, DBHelper.fixedIV);
                }
                string email_aes = context.Request["email"] ?? "";
                if (!string.IsNullOrEmpty(email_aes)){
                    email_aes = CryptoHelper.encryptAES(context.Request["email"], DBHelper.defaultSKey, DBHelper.fixedIV);
                }

                try
				{
                    
                    orderList.ttlOrder = DBHelper.getTotalOrder();
					if (orderList.ttlOrder > 0)
					{
						DBHelper.getOrderList(ref orderList.orderList, targetDateFrom, targetDateTo, email_aes, companyName_aes);
					}
					output.status = (int)StatusType.success;
					output.data = orderList;

				}
				catch (Exception e)
				{
					output.message = "Input error: " + e.Message;
					Helper.writeOutput(output);
					response.End();
				}
			} else
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
