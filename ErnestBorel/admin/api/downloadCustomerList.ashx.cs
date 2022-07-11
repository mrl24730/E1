using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel.admin.api
{
    /// <summary>
    /// Summary description for downloadCustomerList
    /// </summary>
    public class downloadCustomerList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["logined"] != null)
            {
                try
                {
                    HttpResponse Response = context.Response;
                    Response.Clear();
                    Response.Charset = "";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=Customer.xlsx");

                    DataTable dt = DBHelper.getCustomerDatatable();
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet wsDt = pck.Workbook.Worksheets.Add("Customer");
                        wsDt.Cells["A1"].LoadFromDataTable(dt, true);
                        wsDt.Cells.AutoFitColumns();
                        string DateCellFormat = "mm/dd/yyyy hh:mm";
                        wsDt.Cells["F:F"].Style.Numberformat.Format = DateCellFormat;

                        Response.BinaryWrite(pck.GetAsByteArray());
                    }

                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {

                }

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
