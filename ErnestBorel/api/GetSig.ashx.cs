using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kitchen;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for GetSig
    /// </summary>
    public class GetSig : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //Sig = MD5(主帐号Id + 主帐号授权令牌 + 时间戳)

            string timestamp = DateTime.UtcNow.AddHours(8).ToString("yyyyMMddHHmmss");
            string sig = "8a48b55152f73add01530c4e479b26c4" + "58f2cd1c79ee46a0b3541f82e92a703d" + timestamp;
            sig = CryptoHelper.HexStr(CryptoHelper.ComputeHash(sig, "MD5"));
            Helper.writeOutput(sig);
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