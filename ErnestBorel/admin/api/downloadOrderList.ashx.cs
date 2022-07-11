using Kitchen;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel.admin.api
{
    /// <summary>
    /// Summary description for downloadOrderList
    /// </summary>
    public class downloadOrderList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session["logined"] != null)
            {
                int year = 0;
                int month = 0;
                int day = 0;
                string[] dateFromAry = context.Request["df"].Split('-');
                int.TryParse(dateFromAry[0], out year);
                int.TryParse(dateFromAry[1], out month);
                int.TryParse(dateFromAry[2], out day);
                DateTime targetDateFrom = new DateTime(year, month, day);

                string[] dateToAry = context.Request["dt"].Split('-');
                int.TryParse(dateToAry[0], out year);
                int.TryParse(dateToAry[1], out month);
                int.TryParse(dateToAry[2], out day);
                DateTime targetDateTo = new DateTime(year, month, day);
                targetDateTo = targetDateTo.AddDays(1).AddMilliseconds(-1);

                try
                {
                    HttpResponse Response = context.Response;
                    Response.Clear();
                    Response.Charset = "";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=OrderSummary.xlsx");
                    
                    DataTable dt = DBHelper.getOrderDatatable(targetDateFrom, targetDateTo);
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet wsDt = pck.Workbook.Worksheets.Add("Order_Summary");
                        
                        wsDt.Cells["A1"].LoadFromDataTable(dt, true);
                        wsDt.Cells.AutoFitColumns();
                        string DateCellFormat = "mm/dd/yyyy hh:mm";
                        wsDt.Cells["B:B"].Style.Numberformat.Format = DateCellFormat;
                        wsDt.Cells["B:B"].AutoFitColumns();

                        string[] headerText =
                        {
                            "订单编号", "订单日期", "客户", "型号", "订单数量", "单价RMB", "单价HKD", "单价CHF", "折扣",
                            "合计折扣后金额", "合计数量", "合计零售价RMB", "合计零售价HKD", "合计零售价CHF", "折扣后RMB",
                            "折扣后HKD", "折扣后CHF", "客户姓名", "郵箱地址", "電話號碼"
                        };
                        for (int i = 0; i < headerText.Length; i++)
                        {
                            wsDt.Cells[1, i + 1].Value = headerText[i];
                        }

                        Response.BinaryWrite(pck.GetAsByteArray());
                    }

                    Response.Flush();
                    Response.End();
                }catch(Exception ex)
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
