using LinqToExcel;
using LinqToExcel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace ErnestBorel.admin_warranty.api
{
    /// <summary>
    /// Summary description for processStatus
    /// </summary>
    public class processStatus : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            BasicOutput output = new BasicOutput();

            if (context.Session["logined_warranty_admin"] != null)
            {
                string status = String.IsNullOrWhiteSpace(context.Request["Status"]) ? null : context.Request["Status"];

                if (status == "Start")
                {
                    string guid = String.IsNullOrWhiteSpace(context.Request["Guid"]) ? null : context.Request["Guid"];
                    string type = String.IsNullOrWhiteSpace(context.Request["Type"]) ? null : context.Request["Type"];
                    string SavedLocation = context.Server.MapPath("..\\temp") + "\\" + guid;

                    UploadWatcher.Reset();
                    output.status = 1;

                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        try
                        {
                            UploadWatcher.status = "Processing";
                            appendData(type, SavedLocation);

                        }
                        catch (Exception ex)
                        {
                            UploadWatcher.status = "Error";
                            UploadWatcher.message = ex.Message;
                            // Do something
                        }

                    }).Start();

                }
                else if (status == "Monitor")
                {
                    output.status = 1;
                    output.data = UploadWatcher.getObj();
                }
            }
            else
            {
                output.status = 999;
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

        private bool appendData(string xlsType, string path)
        {
            var excel = new ExcelQueryFactory(path);
            excel.TrimSpaces = TrimSpacesType.Both;

            //foreach (var item in list)    System.Diagnostics.Debug.WriteLine(item.CitySC);


            switch (xlsType)
            {
                case "CountryCity":
                    var CountryCityList = excel.Worksheet<CountryCityXlsRow>().Where(x => x.CountryEN != "").ToList();
                    UploadWatcher.ttl = CountryCityList.Count();
                    DBHelper.add_CountryCity(CountryCityList);
                    break;
                case "CaseNum":
                    var CaseNumList = excel.Worksheet<CaseNumXlsRow>().Where(x => x.CaseNum != "").ToList();
                    UploadWatcher.ttl = CaseNumList.Count();
                    DBHelper.add_CaseNum(CaseNumList);
                    break;
                case "ModelNum":
                    var ModelNumList = excel.Worksheet<ModelNumXlsRow>().Where(x => x.ModelNum != "").ToList();
                    UploadWatcher.ttl = ModelNumList.Count();
                    DBHelper.add_ModelNum(ModelNumList);
                    break;
                case "WarrantyNum":
                    var WarrantyNumList = excel.Worksheet<WarrantyNumXlsRow>().Where(x => x.WarrantyNum != "").ToList();
                    UploadWatcher.ttl = WarrantyNumList.Count();
                    DBHelper.add_WarrantyNum(WarrantyNumList);
                    break;
                default:
                    break;

            }

            UploadWatcher.status = "Complete";
            return true;

        }
    }
}