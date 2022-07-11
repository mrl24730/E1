using LinqToExcel;
using LinqToExcel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ErnestBorel.admin_warranty.api
{
    /// <summary>
    /// Summary description for uploadWarrantySetting
    /// </summary>
    public class uploadWarrantySetting : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            BasicOutput output = new BasicOutput();

            if (context.Session["logined_warranty_admin"] != null)
            {
                var httpPostedFile = context.Request.Files["FileUpload"];
                string SettingType = String.IsNullOrWhiteSpace(context.Request["SettingType"]) ? null : context.Request["SettingType"];

                if (httpPostedFile != null && SettingType != null)
                {
                    string fn = System.IO.Path.GetFileName(httpPostedFile.FileName);
                    string ext = System.IO.Path.GetExtension(httpPostedFile.FileName);

                    if (ext == ".xls" || ext == ".xlsx")
                    {
                        string guid = Guid.NewGuid().ToString() + ext;
                        string SaveLocation = context.Server.MapPath("..\\temp") + "\\" + guid;

                        try
                        {
                            httpPostedFile.SaveAs(SaveLocation);
                            int ttl = gatherData(SettingType, SaveLocation);

                            Dictionary<string, string> dict = new Dictionary<string, string>()
                            {
                                {"Total" ,ttl.ToString()},
                                {"Type" ,SettingType},
                                {"Guid" ,guid},

                            };

                            output.data = dict;
                            output.status = 1;

                        }
                        catch (Exception ex)
                        {
                            output.message = "Unable to save :" + ex.Message;
                            output.status = 2;
                            //Note: Exception.Message returns a detailed message that describes the current exception. 
                            //For security reasons, we do not recommend that you return Exception.Message to end users in 
                            //production environments. It would be better to put a generic error message. 
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
            }
            else
            {
                output.status = 5;
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

        private int gatherData(string xlsType, string path)
        {
            int count = 0;
            var excel = new ExcelQueryFactory(path);
            excel.TrimSpaces = TrimSpacesType.Both;
            switch (xlsType)
            {
                case "CountryCity":
                    count = excel.Worksheet<CountryCityXlsRow>().Where(x => x.CountryEN != "").Count();
                    
                    break;
                case "CaseNum":
                    count = excel.Worksheet<CaseNumXlsRow>().Where(x => x.CaseNum != "").Count();
                    break;
                case "ModelNum":
                    count = excel.Worksheet<ModelNumXlsRow>().Where(x => x.ModelNum != "").Count();
                    break;
                case "WarrantyNum":
                    count = excel.Worksheet<WarrantyNumXlsRow>().Where(x => x.WarrantyNum != "").Count();
                    break;
                default:
                    break;

            }

            return count;
        }

        
    }
}