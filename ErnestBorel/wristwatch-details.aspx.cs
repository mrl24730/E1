using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kitchen;

namespace ErnestBorel
{
    public partial class wristwatch_details1 : WebBasePage
    {
        public watchObj obj = new watchObj();

        protected void Page_Load(object sender, EventArgs e)
        {
            //get variable
            DataTable tbl_collection = new DataTable();
            DataTable tbl_watch = new DataTable();


            if (String.IsNullOrEmpty(Request["type"]) || String.IsNullOrEmpty(Request["collection"]))
            {
                //query not enough
                Response.Redirect("wristwatch/");
                Response.End();
            }


            #region Input Variable
            obj.type = Request["type"];
            obj.col_ref = Request["collection"];

            if (!String.IsNullOrEmpty(Request["model"]))
            {
                obj.idx_watch = Request["model"];
            }
            #endregion

            #region Collection Info
            var collectionInfo = DBHelper.getCollectionDetail(lang, obj.col_ref);
            obj.col_name = collectionInfo.name;

            #endregion
            
            //Watch details
            #region Watch Details
            DBHelper.getWatchByCollection(lang, obj.col_ref, out tbl_watch);
            tbl_watch.Columns.Add("image", typeof(string));
            tbl_watch.Columns.Add("url_model", typeof(string));
            tbl_watch.Columns.Add("matching_male", typeof(string));
            tbl_watch.Columns.Add("matching_female", typeof(string));
            tbl_watch.Columns.Add("matching_male_url", typeof(string));
            tbl_watch.Columns.Add("matching_female_url", typeof(string));
            foreach (DataRow r in tbl_watch.Rows)
            {
                string imgPath = "";
                string url = ((string)r["idx_watch"]).Replace("-", "_");
                r["url_model"] = url;

                string img_url = Server.MapPath("/images/watches/" + url + "_t.png");
                imgPath = File.Exists(img_url) ? url : "noimage";
                r["image"] = imgPath;

                r["watch_spec"] = ((string)r["watch_spec"]).Replace("\r\n", "</li><li>");
                r["watch_spec"] = ((string)r["watch_spec"]).Replace("\n", "</li><li>");
                
                if (r["watch_oldmodel"] != null && !String.IsNullOrEmpty((string)r["watch_oldmodel"]))
                {
                    r["watch_oldmodel"] = "<br>(" + ((string)r["watch_oldmodel"]) + ")";
                }
                else
                {
                    r["watch_oldmodel"] = "";
                }

                    if (r["watch_matching"] != DBNull.Value && r["watch_matching"] != null && !String.IsNullOrEmpty((string)r["watch_matching"]))
                {
                    if (((string)r["watch_matching"]).IndexOf("L") >= 0)
                    {
                        r["matching_female"] = (string)r["watch_matching"];
                        r["matching_female_url"] = ((string)r["watch_matching"]).Replace("-", "_");
                        
                        r["matching_male"] = (string)r["idx_watch"];
                        r["matching_male_url"] = url;
                    }
                    else
                    {
                        r["matching_male"] = (string)r["watch_matching"];
                        r["matching_male_url"] = ((string)r["watch_matching"]).Replace("-", "_");
                        
                        r["matching_female"] = (string)r["idx_watch"];
                        r["matching_female_url"] = url;
                        
                    }
                    
                }
                string female_url = Server.MapPath("/images/watches/" + r["matching_female_url"] + "_t.png");
                string male_url = Server.MapPath("/images/watches/" + r["matching_male_url"] + "_t.png");

                if (!File.Exists(male_url) || !File.Exists(female_url))
                {
                    r["watch_matching"] = "";
                    r["matching_male"] = "";
                    r["matching_male_url"] = "";
                    r["matching_female"] = "";
                    r["matching_female_url"] = "";
                }
                        

            }

            if (String.IsNullOrEmpty(obj.idx_watch))
            {
                obj.idx_watch = (string)tbl_watch.Rows[0]["idx_watch"];
            }

            infoRepeater.DataSource = tbl_watch;
            infoRepeater.DataBind();

            thumbRepeater.DataSource = tbl_watch;
            thumbRepeater.DataBind();
            #endregion

            //generate movement, page title and breadcrumb
            #region Title, breadcrumb
            obj.type_lang = Properties.Resources.ResourceManager.GetString("watchfamily_" + obj.type + "_" + lang);
            obj.title = Properties.Resources.ResourceManager.GetString("title_" + lang);
            obj.title = String.Format(obj.title, obj.col_name);

            string breadcrumb = @"<a href='/'>{home}</a> &gt; <a href='wristwatch/'>{watch}</a> &gt; 
                                    <a href='wristwatch_collection/{type}/'>{movement}</a> &gt; 
                                    <a href='wristwatch_collection/{type}/{col_ref}/'>{collection}</a> &gt; <span id='model'>{model}</span> ";

            FastReplacer fr = new FastReplacer("{", "}");
            fr.Append(breadcrumb);
            fr.Replace("{home}", Properties.Resources.ResourceManager.GetString("bc_home_" + lang));
            fr.Replace("{watch}", Properties.Resources.ResourceManager.GetString("bc_watch_" + lang));
            fr.Replace("{type}", obj.type);
            fr.Replace("{movement}", obj.type_lang);
            fr.Replace("{col_ref}", obj.col_ref);
            fr.Replace("{collection}", obj.col_name);
            fr.Replace("{model}", obj.idx_watch);
            obj.breadcrumb = fr.ToString();
            fr = null;
            #endregion

            


        }
    }
}