using ErnestBorel.Distributor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for loginDistibutor
    /// </summary>
    public class loginDistibutor : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput
            {
                status = 99
            };
            if (context.Request.Form["password"].ToLower() == DistributorLogin.LoginPwd)
            {
                context.Session["distributor_loggedin"] = "1";
                output.status = (int) StatusType.success;
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