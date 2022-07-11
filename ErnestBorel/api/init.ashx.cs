using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for init
    /// </summary>
    public class init : IHttpHandler
    {
        private Stream path;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            string device = context.Request["device"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            initOutput output = new initOutput();
            output.ts = 0;
            output.banner = new List<string>();
            output.watches = new Dictionary<string, dynamic>();
            output.news = new List<dynamic>();
            output.families = new List<dynamic>();

            int banner_w = 640, banner_h = 1136;
            int watch_w = 180, watch_h = 180;
            int new_w = 180, new_h = 180;
            string domain_cn = Global.domain_cn;
            DataTable _table = new DataTable();
            string imgPath = domain_cn + "/api/image.ashx?w={0}&h={1}&file={2}";


            initHomeImg homeImg = new initHomeImg();
            #region read home image path, text file
            string basepath = HttpContext.Current.Server.MapPath("~/");
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(basepath + "api\\home_img_path.txt"))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            string filecontent = sb.ToString();
            try{
                homeImg = JsonConvert.DeserializeObject<initHomeImg>(filecontent);
            }
            catch { }
            #endregion

            //generate banner
            string banner1 = "home/"+ homeImg.iphone + "_" + lang+".jpg";
            if(device == "2")
            {
                banner1 = "home/" + homeImg.android + "_" + lang +".jpg";
            }            
            output.banner.Add(domain_cn + "/images/" + banner1);


            //generate watch list
            List<initWatch> automatic = new List<initWatch>();
            List<initWatch> quartz = new List<initWatch>();
            _table = DBHelper.getInitWatch(lang);
            foreach (DataRow r in _table.Rows)
            {
                initWatch watch = new initWatch();
                watch.id = ((int)r["idx_collection"]).ToString();
                watch.name = (string)r["col_name"];
                watch.img = String.Format(imgPath, watch_w, watch_h, "watches/"+(string)r["col_image"]);
                watch.family = (string)r["col_movement"];

                /*
                if ((string)r["col_movement"] == "automatic")
                {
                    automatic.Add(watch);
                }
                else
                {
                    quartz.Add(watch);
                }
                */
                automatic.Add(watch);

            }
            output.watches.Add("automatic", automatic);
            output.watches.Add("quartz", quartz);


            // Family list
            _table = DBHelper.getFamilyLang(lang);
            int familyImg_w = 600;
            int familyImg_h = 361;
            foreach (DataRow r in _table.Rows){
                initFamily family = new initFamily();
                family.id = (string)r["idx_family"];
                family.desc = (string)r["family_desc"];
                family.name = (string)r["family_name"];
                family.img = String.Format(imgPath, familyImg_w, familyImg_h, "families/" + (string)r["family_img"]);
                output.families.Add(family);
            }

            //generate news list
            _table = DBHelper.getInitNews(lang);
            foreach (DataRow r in _table.Rows)
            {
                initNews news = new initNews();
                news.id = ((int)r["idx_news"]).ToString();
                news.date = ((DateTime)r["news_date"]).ToString("yyyy-MM-dd");
                news.title = ((string)r["news_title"]).Replace("<br>", "\n").Replace("<br/>", "\n"); 
                news.file = String.Format(imgPath, new_w, new_h, "latest_news/" + (string)r["news_image1"]+ ".jpg");
                output.news.Add(news);
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
