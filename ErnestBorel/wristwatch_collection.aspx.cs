using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel
{
    public partial class wristwatch_collection : WebBasePage
    {
        public string type = "";
        public string col_ref = "";
        public string collectionObj = "[]";
        public collectionObj collectionDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            DataTable collections= new DataTable();
            DataTable watches = new DataTable();

            #region check variable
            if (!String.IsNullOrEmpty(Request["type"]))
            {
                type = Request["type"];
            }
            else
            {
                type = "couple";
            }
            

            if (!String.IsNullOrEmpty(Request["collection"]))
            {
                col_ref = Request["collection"];
            }
            #endregion

            #region Get Collection List
            DBHelper.getLatestCollection(0, lang, type, out collections, true);
            colRepeater.DataSource = collections;
            colRepeater.DataBind();
            #endregion

            #region Get This Collection Detail
            col_ref = (String.IsNullOrEmpty(col_ref)) ? (string)collections.Rows[0]["col_ref"] : col_ref;
            collectionDetails = DBHelper.getCollectionDetail(lang, col_ref);
            collectionDetails.type_name = Properties.Resources.ResourceManager.GetString("watchtype_" + type + "_" + lang);

            DBHelper.getWatchByCollection(lang, col_ref, out watches);
            watches.Columns.Add("image", typeof(string));
            watches.Columns.Add("url_model", typeof(string));
            foreach (DataRow r in watches.Rows)
            {
                
                string imgPath = "";
                string url = ((string)r["idx_watch"]).Replace("-", "_");
                r["url_model"] = url;

                string img_url = Server.MapPath("/images/watches/" + url + "_s.png");
                imgPath = File.Exists(img_url) ? url : "noimage";
                r["image"] = imgPath;

            }
            watchRepeater.DataSource = watches;
            watchRepeater.DataBind();

            string hero_image_path = "/images/watches/banner/watch_header_" + type + "_" + col_ref + ".jpg";
            if (File.Exists(Server.MapPath(hero_image_path)))
            {
                collectionDetails.hero_image = "<img src='" + hero_image_path + "'>";
            }
            #endregion


            

        }
    }
}