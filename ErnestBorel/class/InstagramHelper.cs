using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Kitchen;

namespace ErnestBorel
{
	public static class InstagramHelper
	{
        private static readonly string clientID = ConfigurationManager.AppSettings["clientID"];
        private static readonly string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        private static readonly string hashTag = ConfigurationManager.AppSettings["hashTag"];
        public static readonly string APIadminKey = ConfigurationManager.AppSettings["APIadminKey"];


        public static void checkSubscription(string signature, HttpRequest request, bool debug = false)
        {
            string bodyContent = "";
            using (Stream receiveStream = request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    bodyContent = readStream.ReadToEnd();
                }

                if (bodyContent != "" || debug)
                {
                    //x-hub-signature validation
                    if (ConvertToHexadecimal(SignWithHmac(UTF8Encoding.UTF8.GetBytes(bodyContent), UTF8Encoding.UTF8.GetBytes(clientSecret))) == signature || debug)
                    {
                        /*using (StreamWriter sw = File.AppendText(server.MapPath(@"..\log.txt")))
                        {
                            sw.WriteLine("signature OK, Do Something");
                        }*/

                        //OK
                        checkMediaExist();

                    }
                    else
                    {
                        //Incorrect signature
                    }
                }
                else
                {
                    //Empty Body
                }
            }
        }

        private static void checkMediaExist(string maxTagId = "")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("client_id", clientID);

            
            if (maxTagId == "")
            {
                //get min_tag_id from DB
                string minTagId = DBHelper.getIGMinTagId();

                if (minTagId != "") dict.Add("mintagId", minTagId);
            }
            else
            {
                //paging problem in instagram API
                dict.Add("max_tag_id", maxTagId);
            }


            //get recent media
            string result = MakeAPI(String.Format("https://api.instagram.com/v1/tags/{0}/media/recent", hashTag), dict);
            JObject json = null;
            try
            {
                //JSON parser
                json = JObject.Parse(result);                
            }
            catch
            {

            }

            if (json != null)
            {
                if(json["data"].Count() > 0)
                {
                    //linq media_ids
                    List<string> media_ids = ((JArray)json["data"]).Select(p => (string)p["id"]).ToList();

                    //find media_ids in DB
                    DBHelper.getIGNonExist(ref media_ids);

                    if (media_ids.Count > 0)
                    {
                        List<InstagramMediaObj> list = new List<InstagramMediaObj>();

                        //loop and insert + send email to reject
                        foreach(JObject jObj in (JArray) json["data"])
                        {
                            if (media_ids.Contains((string)jObj["id"]))
                            {
                                InstagramMediaObj mediaObj = new InstagramMediaObj();
                                mediaObj.idx_photo = (string)jObj["id"];

                                Int64.TryParse((string)jObj["user"]["id"],out mediaObj.idx_user);

                                mediaObj.username = (string)jObj["user"]["username"];
                                mediaObj.photo_low = (string)jObj["images"]["low_resolution"]["url"];
                                mediaObj.photo_std = (string)jObj["images"]["standard_resolution"]["url"];
                                mediaObj.photo_thumb = (string)jObj["images"]["thumbnail"]["url"];
                                mediaObj.photo_create_date = (string)jObj["caption"]["created_time"];

                                list.Add(mediaObj);

                                //Send mail in here
                                try
                                {
                                    string imgUrl = mediaObj.photo_low;
                                    string RejectUrl = "http://ernestborel.ch/admin/IG_photoList.aspx?idx_photo=" + mediaObj.idx_photo;
                                    string EmailBd = getEmailBody("instagram.html");
                                    AppHelper.sendMail(new string[] { "kin.fok@kitchen-digital.com" }, "it@kitchen-digital.com", "New image subscribed on Instagram", String.Format(EmailBd, imgUrl, RejectUrl));
                                }
                                catch
                                {
                                }

                            }
                        }

                        DBHelper.setIGMedia(list);


                    }

                    if (((JObject)json["pagination"]).Property("min_tag_id") != null && maxTagId == "")
                    {
                        //Update last min_tag_id to DB
                        DBHelper.setIGMinTagId((string)json["pagination"]["min_tag_id"]);
                    }



                    if (((JObject)json["pagination"]).Property("next_max_tag_id") != null)
                    {
                        checkMediaExist((string)json["pagination"]["next_max_tag_id"]);

                    }
                    


                    
                    

                }

            }

            

            

        }


        public static string MakeAPI(string url, Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string key in dict.Keys)
            {
                sb.Append(key + "=" + HttpUtility.UrlEncode(dict[key]) + "&");
            }
            sb.Remove(sb.Length-1, 1); // Remove the final '&'

            WebRequest request = WebRequest.Create(url + "?" + sb.ToString());
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }

            

        }

        public static string getEmailBody(string fname)
        {
            string bd = "No such email template";
            //string subject = "Email Subject";
            try
            {
                bd = File.ReadAllText(HttpContext.Current.Server.MapPath("~/emailTemplate/" + fname)).Replace(Environment.NewLine, "");
                //Match m = Regex.Match(bd, @"<title>\s*(.+?)\s*</title>");
                //if (m.Success) subject = m.Groups[1].Value;

            }
            catch { }
            //return new string[] { subject, bd };
            return bd;
        }

#region Validation Hash
        private static byte[] SignWithHmac(byte[] dataToSign, byte[] keyBody)
        {
            using (var hmacAlgorithm = new System.Security.Cryptography.HMACSHA1(keyBody))
            {
                return hmacAlgorithm.ComputeHash(dataToSign);
            }
        }

        private static string ConvertToHexadecimal(IEnumerable<byte> bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
#endregion

    }

    public class InstagramMediaObj
    {
        public string idx_photo = "";
        public long idx_user = 0;
        public string username = "";
        public string photo_low = "";
        public string photo_std = "";
        public string photo_thumb = "";
        public string photo_create_date = "";
    }

    
}