using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

using Kitchen;

namespace ErnestBorel
{
	public class Helper
	{

        public static void writeOutput(Object obj, bool isIgnoreNull = false)
        {
            if (obj != null)
            {
                if (isIgnoreNull)
                {
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                }
                else
                {
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(obj));
                }
            }
        }

        public static string getLang()
        {
            string lang = "sc";
            string url = HttpContext.Current.Request.Url.PathAndQuery;

            if (url.IndexOf("/en/") > -1)
            {
                lang = "en";
            }
            else if (url.IndexOf("/tc/") > -1)
            {
                lang = "tc";
            }
            else if (url.IndexOf("/fr/") > -1)
            {
                lang = "fr";
            }
            else if (url.IndexOf("/jp/") > -1)
            {
                lang = "jp";
            }

            return lang;
        }

        //generate an encrypted access token for cookie
        public static void setCookie(int dayExp, string name, string val = null, double? minExp = null)
        {
            /*
            HttpCookie cookie = new HttpCookie(name) { Value = val, Expires = DateTime.UtcNow.AddDays(dayExp) };
           
            HttpContext.Current.Response.Cookies.Add(cookie);
            */

            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = val;
            if (dayExp == 0 && minExp != null)
            {
                cookie.Expires = DateTime.UtcNow.AddMinutes((double)minExp);
            }
            else
            {
                cookie.Expires = DateTime.UtcNow.AddDays(dayExp);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public static string getCookie(string name)
        {
            string str = null;
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie != null) str = cookie.Value;

            return str;
        }

        public static void removeCookie(string name) {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetIPAddress(bool getAll = false)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;

            if (context != null)
            {
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        if (getAll)
                        {
                            return ipAddress;
                        }
                        return addresses[0];
                    }
                }

                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                return "Unknown - no context";
            }
        }


        public static string SendSMS(string phone)
        {
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
                    //13147515594
                    //13149999999
                    Dictionary<string, object> retData = api.SendTemplateSMS(phone, "99923", datas);
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

            return ret;

        }


        private static string getDictionaryData(Dictionary<string, object> data)
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
    }
}