using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCPRestSDK;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for sendSMS
    /// </summary>
    public class sendSMS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();


            string ret = null;
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            bool isInit = api.init("sandboxapp.cloopen.com", "8883");
            api.setAccount("8a48b55152f73add01530c4e479b26c4", "58f2cd1c79ee46a0b3541f82e92a703d");
            api.setAppId("8a216da8550b8ac001550c845ac8008b");
            string[] datas = new string[] { "", "" };

            try
            {
                if (isInit)
                {
                    //13147515594 - EB testing number
                    //13149999999
                    Dictionary<string, object> retData = api.SendTemplateSMS("13147515594", "99923", datas);
                    ret = getDictionaryData(retData);
                    
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            finally
            {
                context.Response.Write(ret);
            }
            
        }


        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" +
                    (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
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