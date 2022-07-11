using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ErnestBorel.admin
{
    public partial class WatchList : System.Web.UI.Page
    {
        public string output = "[]";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable _table = new DataTable();
            DBHelper.getWatchByLang("sc", out _table);
            //int[] autoList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };
            List<WatchModel> list = new List<WatchModel>();

            DataTable _auto;
            DataTable _quartz;
            DBHelper.getLatestCollection(0, "sc", "automatic", out _auto);
            DBHelper.getLatestCollection(0, "sc", "quartz", out _quartz);
            Dictionary<int, string> autoList = new Dictionary<int, string>();
            Dictionary<int, string> quartzList = new Dictionary<int, string>();

            foreach (DataRow r in _auto.Rows)
            {
                autoList.Add((int)r["idx_collection"], (string)r["col_name"]);
            }

            foreach (DataRow r in _quartz.Rows)
            {
                quartzList.Add((int)r["idx_collection"], (string)r["col_name"]);
            }

            foreach (DataRow r in _table.Rows)
            {
                WatchModel model = new WatchModel();
                int cid = (int)r["idx_collection"];
                
                if (autoList.ContainsKey(cid))
                {
                    model.idx_collection= 1;
                    model.collection = autoList[cid];
                }
                else if(quartzList.ContainsKey(cid))
                {
                    model.idx_collection = 2;
                    model.collection = quartzList[cid];
                }

                model.id = (string)r["idx_watch"];
                list.Add(model);
            }

            output = JsonConvert.SerializeObject(list);

        }
    }
}