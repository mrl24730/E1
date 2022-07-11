using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kitchen;
using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for addOrder
    /// </summary>
    public class addOrder : ErnestBorel.Distributor.ApiHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            HashSet<string> modelSet = new HashSet<string>();
            Dictionary<string, JObject> itemSpec = new Dictionary<string, JObject>();

            Order input = new Order();

            #region get POST variable
            try
            {
                string data = context.Request["data"];
                decimal totallPrice = 0;
                decimal totallPrice_hkd = 0;
                decimal totallPrice_chf = 0;
                input = JsonConvert.DeserializeObject<Order>(data);
                JObject json = JObject.Parse(data);
                IList<JToken> watches = json["watches"].Children().ToList();
                IList<OrderItem> orderItems = new List<OrderItem>();

                foreach (JToken watch in watches)
                {
                    OrderItem orderItem = watch.ToObject<OrderItem>();
                    orderItems.Add(orderItem);
                    modelSet.Add(orderItem.idx_watch);
                }

                foreach (string model in modelSet)
                {
                    itemSpec.Add(model, DBHelper.getItemSpec(model));
                }

                foreach (OrderItem orderItem in orderItems)
                {
                    var _spec = itemSpec[orderItem.idx_watch];
                    
                    totallPrice += (decimal)_spec["price"] * orderItem.qty;
                    totallPrice_hkd += (decimal)_spec["price_hkd"] * orderItem.qty;
                    totallPrice_chf += (decimal)_spec["price_chf"] * orderItem.qty;
                }
                DateTime orderDateHKT = DateTime.Now;
                input.order_date = orderDateHKT;
                //input.idx_customer = json["idx_customer"].ToObject<int>();
                //input.discount = json["discount"].ToObject<int>();
                input.price = totallPrice;
                input.price_hkd = totallPrice_hkd;
                input.price_chf = totallPrice_chf;
                input.ip_address = HttpContext.Current.Request.UserHostAddress;
                input.d_price = totallPrice - (totallPrice * (input.discount / 100m));
                input.d_price_hkd = totallPrice_hkd - (totallPrice_hkd * (input.discount / 100m));
                input.d_price_chf = totallPrice_chf - (totallPrice_chf * (input.discount / 100m));

                int? idx_order = DBHelper.insertOrder(input);
                List<bool> isSuccess = new List<bool>();

                if (idx_order != null)
                {
                    foreach (OrderItem orderItem in orderItems)
                    {
                        var _spec = itemSpec[orderItem.idx_watch];
                        isSuccess.Add(DBHelper.insertOrderItem(idx_order, (decimal)_spec["price"], (decimal)_spec["price_hkd"], (decimal)_spec["price_chf"], orderItem));
                    }
                }

                if (isSuccess.Contains(false))
                {
                    DBHelper.rollbackOrder(idx_order);
                    output.message = "Error during insert";
                }
                else
                {
                    #region Send purchase order email
                    
                    Customer cust = DBHelper.getCustomerById(input.idx_customer);
                    OrderEmailTemplate email = new OrderEmailTemplate();
                    string lang = Helper.getLang();
                    string orderNumberDisplay = orderDateHKT.ToString("yyyyMMdd") + idx_order;
                    email.subject = Properties.Resources.ResourceManager.GetString("emailsubject_" + lang);
                    email.subject = String.Format(email.subject, orderNumberDisplay);

                    string UnitPrice = Properties.Resources.ResourceManager.GetString("UnitPrice_" + lang);
                    string Qty = Properties.Resources.ResourceManager.GetString("Qty_" + lang);
                    string SubTotal = Properties.Resources.ResourceManager.GetString("Subtotal_" + lang);
                    string OrderSummary = Properties.Resources.ResourceManager.GetString("OrderSummary_" + lang);
                    string Total = Properties.Resources.ResourceManager.GetString("Total_" + lang);
                    string TotalQty = Properties.Resources.ResourceManager.GetString("TotalQty_" + lang);
                    string Discount = Properties.Resources.ResourceManager.GetString("Discount_" + lang);
                    string Remark = Properties.Resources.ResourceManager.GetString("Remark_" + lang);
                    string YourOrder = Properties.Resources.ResourceManager.GetString("YourOrder_" + lang);
                    string OrderNumber = Properties.Resources.ResourceManager.GetString("OrderNumber_" + lang);
                    string DiscountTotal = Properties.Resources.ResourceManager.GetString("DiscountTotal_" + lang);
                    Dictionary<string, string> gender = new Dictionary<string, string>();
                    Dictionary<string, string> watch_type = new Dictionary<string, string>();
                    gender["F"] = Properties.Resources.ResourceManager.GetString("Gender_F_" + lang);
                    gender["M"] = Properties.Resources.ResourceManager.GetString("Gender_M_" + lang);
                    gender["N"] = Properties.Resources.ResourceManager.GetString("Gender_N_" + lang);
                    watch_type["automatic"] = Properties.Resources.ResourceManager.GetString("watchtype_automatic_" + lang);
                    watch_type["quartz"] = Properties.Resources.ResourceManager.GetString("watchtype_quartz_" + lang);

                    string domain = "http://" + HttpContext.Current.Request.Url.Authority;
                    //Item HTML
                    string itemHTML = "";
                    string price_suffix = "";
                    string price_prefix = "¥";
                    string discount_suffix = "折";
                    decimal emailTotal = input.price;
                    decimal emailDiscount = input.d_price;
                    int totalQty = 0;

                    if (lang == "en")
                    {
                        price_suffix = "_chf";
                        price_prefix = "₣";
                        emailTotal = input.price_chf;
                        emailDiscount = input.d_price_chf;
                        discount_suffix = "% off";
                    }
                    else if (lang == "tc")
                    {
                        price_suffix = "_hkd";
                        price_prefix = "$";
                        emailTotal = input.price_hkd;
                        emailDiscount = input.d_price_hkd;
                    }

                    FastReplacer fr = new FastReplacer("{", "}");
                    
                    foreach (OrderItem orderItem in orderItems)
                    {
                        var _spec = itemSpec[orderItem.idx_watch];
                        fr = new FastReplacer("[", "]");
                        fr.Append(email.itemHtml);
                        fr.Replace("[CollectionName]", Global.collections[lang][(int)_spec["idx_collection"]].name);
                        fr.Replace("[ImgPath]", domain+"/images/watches/"+(orderItem.idx_watch.Replace("-","_"))+"_s.png");
                        fr.Replace("[ModelNumber]", orderItem.idx_watch);
                        fr.Replace("[Gender]", gender[(string)_spec["watch_gender"]]);
                        fr.Replace("[WatchType]", watch_type[(string)_spec["watch_type"]]);
                        fr.Replace("[UnitPrice]", UnitPrice + ": " + price_prefix + ((decimal)_spec["price" + price_suffix]).ToString("#,##0.#"));
                        fr.Replace("[Qty]", Qty + ": " + orderItem.qty);
                        fr.Replace("[SubTotal]", SubTotal + ": " + price_prefix + ((orderItem.qty * (decimal)_spec["price" + price_suffix]).ToString("#,##0.#")));
                        itemHTML += fr.ToString();
                        fr = null;
                        totalQty += orderItem.qty;
                    }


                    fr = new FastReplacer("[", "]");
                    fr.Append(email.templateHtml);
                    fr.Replace("[YourOrder]", YourOrder);
                    fr.Replace("[OrderNumberTxt]", OrderNumber);
                    fr.Replace("[OrderNumber]", orderNumberDisplay);
                    fr.Replace("[OrderDate]", orderDateHKT.ToString("yyyy-MM-dd HH:mm:ss"));
                    fr.Replace("[OrderSummary]", OrderSummary);
                    fr.Replace("[TotalPriceTxt]", Total);
                    fr.Replace("[DiscountTotalTxt]", DiscountTotal);
                    fr.Replace("[TotalQtyTxt]", TotalQty);
                    fr.Replace("[DiscountTxt]", Discount);
                    fr.Replace("[TotalPrice]", price_prefix + emailTotal.ToString("#,##0.#"));
                    fr.Replace("[TotalQty]", totalQty.ToString());

                    
                    if (input.discount > 0) {
                        int displayDiscount = PrecentConvert(input.discount);
                        if (lang == "en")
                        {
                            displayDiscount = input.discount;
                        }
                        fr.Replace("[Discount]", "(" + displayDiscount + discount_suffix + ")<br>-" + price_prefix + (emailTotal - emailDiscount).ToString("#,##0.#"));
                    }
                    else
                    {
                        fr.Replace("[Discount]", "---");
                    }
                                       
                    
                    fr.Replace("[DiscountPrice]", price_prefix + emailDiscount.ToString("#,##0.#"));
                    fr.Replace("[Remark]", Remark);
                    fr.Replace("[ItemList]", itemHTML);
                    email.body = fr.ToString();

                    bool SuccessSent = AppHelper.sendMail(new string[] { cust.email }, email.fromEmail, email.subject, email.body, email.fromName);
                    #endregion

                    if (SuccessSent)
                    {
                        output.data = orderNumberDisplay;
                        output.status = (int)StatusType.success;
                    }
                    else
                    {
                        output.message = "Send email error";
                        output.status = (int)StatusType.error;
                    }
                }

                Helper.writeOutput(output);

            }
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }
            #endregion

        }

        private int PrecentConvert(int precent)
        {
            if (precent == 0) return 0;
            if(precent % 10 == 0)
            {
                return 10 - precent / 10;
            }
            return 100 - precent;
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}