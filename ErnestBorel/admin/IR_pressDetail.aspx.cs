using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

namespace ErnestBorel.admin
{
    public partial class IR_pressDetail : System.Web.UI.Page
    {
        //public List<IR_detailRecord> list_irDetails = new List<IR_detailRecord>();
        public IR_masterRecord ir_masterRec = new IR_masterRecord();

        public string str_releaseDate = DateTime.Now.ToString("yyyy-MM-dd");
        public bool isNew = false, isWithdrawn = false;
        public int masterRec_idx = 0;
        public string outputString = "[]";
        public string SystemTime = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            if (Session["logined"] == null || Session["admin"] == null || (string)Session["admin"] != "irAdmin")
            {
                Response.Redirect("index.aspx");
            }

            SystemTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");

            //check query string
            ir_masterRec.list = new List<IR_detailRecord>();
            int.TryParse(Request["rec_idx"], out masterRec_idx);

            if (masterRec_idx == 0) int.TryParse(Request.Form["input_recIdx"], out masterRec_idx);  

            if (Request["act"] == "create")
            {
                isNew = true;
                ir_masterRec.ir_releaseDate = ir_masterRec.ir_lastUpdated = DateTime.Now.Date;
            }
            else if (masterRec_idx != 0 || Request.Form["isNewRec"] != null)
            {

                if (Request.Form["input_withdraw"] != null && masterRec_idx == Convert.ToInt32(Request.Form["input_withdraw"]) && masterRec_idx != 0)
                {
                    //withdraw record
                    DBHelper.withdraw_IRRecords(masterRec_idx);
                }
                else if (Request.Form["input_reactivate"] != null && masterRec_idx == Convert.ToInt32(Request.Form["input_reactivate"]) && masterRec_idx != 0 )
                {
                    //withdraw record
                    DBHelper.withdraw_IRRecords(masterRec_idx, true);
                }
                else if (Request.Form["input_recIdx"] != null || Request.Form["isNewRec"] != null)
                {
                    //save record;
                    var save_status = AdminHelper.SaveIRrecords(HttpContext.Current, ref ir_masterRec, ref masterRec_idx);
                    ir_masterRec = new IR_masterRecord();
                    ir_masterRec.list = new List<IR_detailRecord>();
                }

                DBHelper.get_IRRecords(ref ir_masterRec, masterRec_idx);
                str_releaseDate = ir_masterRec.ir_releaseDate.ToString("yyyy-MM-dd");

                isWithdrawn = ir_masterRec.ir_status == (int)IR_status.withheld;
            }
            else
            {
                //Response.Redirect("IR_pressList.aspx");
            }

            ir_masterRec.ir_statusDisplay = (isNew)? "---" : ((IR_status)ir_masterRec.ir_status).ToString();
            outputString = JsonConvert.SerializeObject(ir_masterRec);

        }
    }
}