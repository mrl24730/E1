using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kitchen;
using Newtonsoft.Json;

namespace ErnestBorel.admin
{
    public partial class IR_pressList : System.Web.UI.Page
    {

        public int cntAllReleases = 0, cntPublished = 0, cntPending=0, cntWithheld = 0;
        public List<Tuple<int, int>> list_IRCnt = new List<Tuple<int,int>>();
        public List<IR_masterRecord> list_IRrec = new List<IR_masterRecord>();
        public string output = "[]";
        public string SystemTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["logined"] == null || Session["admin"] == null || (string)Session["admin"] != "irAdmin")
            {
                Response.Redirect("index.aspx");
            }

            SystemTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");

            DBHelper.get_IRsummary(ref list_IRCnt);

            foreach (Tuple<int, int> irRec in list_IRCnt)
            {
                cntAllReleases += irRec.Item2;
                switch (irRec.Item1)
                {
                    case (int)IR_status.published:
                        cntPublished = irRec.Item2;
                        break;
                    case (int)IR_status.pending:
                        cntPending = irRec.Item2;
                        break;
                    case (int)IR_status.withheld:
                        cntWithheld = irRec.Item2;
                        break;
                }
            }


            DBHelper.get_IRList(ref list_IRrec);
            int numLang = Enum.GetNames(typeof(IR_lang)).Length;

            foreach (IR_masterRecord rec in list_IRrec)
            {
                rec.ir_statusDisplay = ((IR_status)rec.ir_status).ToString();
                int intLangFlag = rec.ir_langFlag;
                rec.ir_langStr = "[";

                for (int i = 0; i < numLang; i++)
                {
                    if ((intLangFlag & (1 << i)) != 0)
                    {
                        rec.ir_langStr += " " + ((IR_lang)(i)).ToString();
                    }
                }
                rec.ir_langStr += " ]";
            }


            output = JsonConvert.SerializeObject(list_IRrec);
            
        }



    }
}