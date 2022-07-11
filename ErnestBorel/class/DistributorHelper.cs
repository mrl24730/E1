using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ErnestBorel.Distributor
{
    public static class DistributorLogin
    {
        public static readonly string LoginPwd = ConfigurationManager.AppSettings["distributorLoginPwd"];
        public static readonly string loginPage = "index.aspx";

        public static bool IsValidAccess()
        {

            return HttpContext.Current.Session["distributor_loggedin"] != null;

        }
    }

    public partial class AfterLoginPage : System.Web.UI.Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!DistributorLogin.IsValidAccess()) Response.Redirect(DistributorLogin.loginPage);
        }
    }

    public abstract class ApiHandler : IHttpHandler, IRequiresSessionState
    {
        public abstract bool IsReusable { get; }

        public virtual void ProcessRequest(HttpContext context)
        {
            if (!DistributorLogin.IsValidAccess())
            {
                context.Response.ContentType = "application/json";
                context.Response.ContentEncoding = Encoding.UTF8;

                var output = new BasicOutput();
                output.status = (int)StatusType.noAuthorization;
                output.message = "Not Authorized";
                context.Response.Write(JsonConvert.SerializeObject(output));

                context.Response.Flush();
                context.Response.End();
            }
        }
    }

    public class WatchDetailNotFound : Exception
    {
        public WatchDetailNotFound() : base() { }
        public WatchDetailNotFound(string message) : base(message) { }
        public WatchDetailNotFound(string message, Exception innerException)
            : base(message, innerException) { }
    }


}