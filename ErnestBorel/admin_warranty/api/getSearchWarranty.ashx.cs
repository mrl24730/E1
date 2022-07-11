using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

using Kitchen;


namespace ErnestBorel.admin_warranty.api
{
    /// <summary>
    /// Summary description for getSearchWarranty
    /// </summary>
    public class getSearchWarranty : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            BasicOutput output = new BasicOutput();

            if (context.Session["logined_warranty_checker"] != null || context.Session["logined_warranty_admin"] != null)
            {

                string CaseNum = String.IsNullOrWhiteSpace(context.Request["CaseNum"]) ? null : context.Request["CaseNum"];
                string Phone = String.IsNullOrWhiteSpace(context.Request["Phone"]) ? null : context.Request["Phone"];
                string WarrantyNum = String.IsNullOrWhiteSpace(context.Request["WarrantyNum"]) ? null : context.Request["WarrantyNum"];

                if (CaseNum != null || WarrantyNum != null || Phone != null)
                {
                    WarrantyRecord searchWarrantyRecord = new WarrantyRecord()
                    {
                        CaseNum = CaseNum,
                        WarrantyNum = WarrantyNum,
                        Phone = Phone
                    };

                    List<WarrantyRecord> list = DBHelper.searchWarranty(searchWarrantyRecord);

                    //decrypt result
                    foreach (WarrantyRecord r in list)
                    {
                        r.Name = CryptoHelper.decryptAES(r.Name, DBHelper.defaultSKey);
                        if (!String.IsNullOrEmpty(r.Email))
                        {
                            r.Email = CryptoHelper.decryptAES(r.Email, DBHelper.defaultSKey);
                        }
                    }

                    if (list.Count() > 0)
                    {
                        output.data = list;
                        output.status = 1;
                    }
                    else
                    {
                        output.status = 2;
                    }
                }
                else
                {
                    output.status = 3;
                }

            }
            else
            {
                output.status = 4;
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