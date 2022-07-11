using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Resources;
using Newtonsoft.Json;

namespace ErnestBorel
{
    public partial class wristwatch_type : WebBasePage
    {

        public pageObj obj = new pageObj();

        public string ladyObj = "[]";
        public string coupleObj = "[]";
        public string casualObj = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            string type = "";

            DataTable collectionLady= new DataTable();
            DataTable collectionCouple= new DataTable();
            DataTable collectionCasual = new DataTable();

            string[] LadyNames;
            string[] CoupleNames;
            string[] CasualNames;
            string collectionNames = "";


            //get variable
            #region Variable
            /*
            if (!String.IsNullOrEmpty(Request["type"]))
            {
                type = Request["type"];
                type = type.Replace("/", "").ToLower();
            }
            else
            {
                type = "all";                
            }


            //get latest collection from DB
            switch (type)
            {
                case "automatic":
                    DBHelper.getLatestCollection(0, lang, "automatic", out collectionAuto, true);
                    break;
                case "quartz":
                    DBHelper.getLatestCollection(0, lang, "quartz", out collectionQuartz, true);
                    break;
                case "all":
                    DBHelper.getLatestCollection(0, lang, "automatic", out collectionAuto, true);
                    DBHelper.getLatestCollection(0, lang, "quartz", out collectionQuartz, true);
                    break;
            }
            */
            #endregion

            DBHelper.getLatestCollection(0, lang, "lady", out collectionLady, true);
            DBHelper.getLatestCollection(0, lang, "couple", out collectionCouple, true);
            DBHelper.getLatestCollection(0, lang, "casual", out collectionCasual, true);

            ladyObj = JsonConvert.SerializeObject(collectionLady);
            coupleObj = JsonConvert.SerializeObject(collectionCouple);
            casualObj = JsonConvert.SerializeObject(collectionCasual);

            //generate page title, keyword and desc
            LadyNames = collectionLady.AsEnumerable().Select(r => r.Field<string>("col_name")).ToArray();
            collectionNames = collectionNames + String.Join(",", LadyNames);
            CoupleNames = collectionCouple.AsEnumerable().Select(r => r.Field<string>("col_name")).ToArray();
            collectionNames = collectionNames + String.Join(",", CoupleNames);
            CasualNames = collectionCasual.AsEnumerable().Select(r => r.Field<string>("col_name")).ToArray();
            collectionNames = collectionNames + String.Join(",", CasualNames);
            obj.metaKeyword = collectionNames;



            //assign datatable to repeater
            /*
            if (collectionAuto.Rows.Count == 0)
            {
                autoRepeater.Visible = false;
            }
            else
            {
                autoRepeater.DataSource = collectionAuto;
                autoRepeater.DataBind();
                AutoNames = collectionAuto.AsEnumerable().Select(r => r.Field<string>("col_name")).ToArray();
                collectionNames = collectionNames + String.Join(",", AutoNames);
            }

            if (collectionQuartz.Rows.Count == 0)
            {
                quartzRepeater.Visible = false;
            }
            else
            {
                quartzRepeater.DataSource = collectionQuartz;
                quartzRepeater.DataBind();
                QuartzNames = collectionQuartz.AsEnumerable().Select(r => r.Field<string>("col_name")).ToArray();
                collectionNames = collectionNames + String.Join(",", QuartzNames);
            }
            */
            
             
            
            

            
        }
    }
}