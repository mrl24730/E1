using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for setIGSubscribe
    /// </summary>
    public class setIGSubscribe : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            bool debug = false;

            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            HttpServerUtility server = context.Server;

            string challenge = String.IsNullOrWhiteSpace(request["hub.challenge"]) ? null : request["hub.challenge"];
            string signature = String.IsNullOrWhiteSpace(request.Headers["x-hub-signature"]) ? null : request.Headers["x-hub-signature"];

            if (challenge != null)
            {
                response.Write(challenge);
            }
#if DEBUG
            debug = true;
#endif

            if (signature != null)
            {
                InstagramHelper.checkSubscription(signature, request, debug);
            }
            else
            {
                //Empty Signature
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