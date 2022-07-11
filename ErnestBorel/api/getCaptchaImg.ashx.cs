using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getCaptchaImg
    /// </summary>
    public class getCaptchaImg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.BinaryWrite(CaptchaHelper.GetCaptchaBytes(context,120,26));
            context.Response.End();
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