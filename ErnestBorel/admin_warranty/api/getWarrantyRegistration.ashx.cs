using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel.admin_warranty.api
{
    /// <summary>
    /// Summary description for getWarrantyRegistration
    /// </summary>
    public class getWarrantyRegistration : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["logined_warranty_checker"] != null || context.Session["logined_warranty_admin"] != null)
            {
                string From_str = String.IsNullOrWhiteSpace(context.Request["From"]) ? null : context.Request["From"];
                string To_str = String.IsNullOrWhiteSpace(context.Request["To"]) ? null : context.Request["To"];

                if (From_str != null && To_str != null)
                {
                    From_str = From_str + " 00:00:00";
                    To_str = To_str + " 23:59:59";
                    DateTime From, To;
                    if (DateTime.TryParse(From_str, out From) && DateTime.TryParse(To_str, out To))
                    {

                        /* 
                           ===== Strange !!!! Can't open the excel file unless using text editor to save once..... so... bye  =====
                        var grid = new GridView();
                        grid.DataSource = DBHelper.searchWarranty(From, To);
                        grid.DataBind();


                        context.Response.ClearContent();
                        context.Response.Buffer = true;
                        context.Response.AddHeader("content-disposition", String.Format("attachment; filename=RegisteredWarranty-From{0}-To{1}.xls", From.ToString("yyyy-MM-dd"), To.ToString("yyyy-MM-dd")));
                        context.Response.ContentType = "application/ms-excel";

                        context.Response.Charset = "";
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        grid.RenderControl(htw);
                        context.Response.Output.Write(sw.ToString());
                        context.Response.Flush();
                        context.Response.End();
                        */

                        HttpResponse Response = context.Response;
                        Response.Clear();
                        Response.Charset = "";
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", String.Format("attachment; filename=RegisteredWarranty-From{0}-To{1}.xlsx", From.ToString("yyyy-MM-dd"), To.ToString("yyyy-MM-dd")));


                        DataTable dt = DBHelper.searchWarranty(From, To);
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet wsDt = pck.Workbook.Worksheets.Add("SearchResult");
                            wsDt.Cells["A1"].LoadFromDataTable(dt, true);
                            wsDt.Cells.AutoFitColumns();

                            Response.BinaryWrite(pck.GetAsByteArray());
                        }

                        Response.Flush();
                        Response.End();


                        /*using (var ms = new MemoryStream())
                        {
                            ExcelLibrary.DataSetHelper.CreateWorkbook(ms, );

                            context.Response.Clear();
                            context.Response.ContentType = "application/force-download";
                            context.Response.AddHeader("content-disposition", "attachment; filename=name_you_file.xls");
                            context.Response.BinaryWrite(ms.ToArray());
                            context.Response.End();

                        }*/

                    }
                    else
                    {

                    }


                }
                else
                {

                }
            }
            else
            {

            }
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