using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Kitchen;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for warrantyRegistration
    /// </summary>
    public class warrantyRegistration : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            BasicOutput output = new BasicOutput();

            //Server side validation
            bool isValid = true;
            string ModelNum = String.IsNullOrWhiteSpace(context.Request["ModelNum"]) ? null : context.Request["ModelNum"];
            string CaseNum = String.IsNullOrWhiteSpace(context.Request["CaseNum"]) ? null : context.Request["CaseNum"];
            string WarrantyNum = String.IsNullOrWhiteSpace(context.Request["WarrantyNum"]) ? null : context.Request["WarrantyNum"];
            string Name = String.IsNullOrWhiteSpace(context.Request["Name"]) ? null : context.Request["Name"];
            string Title = String.IsNullOrWhiteSpace(context.Request["Title"]) ? null : context.Request["Title"];
            string Email = String.IsNullOrWhiteSpace(context.Request["Email"]) ? null : context.Request["Email"];
            string Ccode = String.IsNullOrWhiteSpace(context.Request["Ccode"]) ? null : context.Request["Ccode"];
            string Phone = String.IsNullOrWhiteSpace(context.Request["Phone"]) ? null : context.Request["Phone"];
            string Dop = String.IsNullOrWhiteSpace(context.Request["Dop"]) ? null : context.Request["Dop"];
            string InvNum = String.IsNullOrWhiteSpace(context.Request["InvNum"]) ? null : context.Request["InvNum"];
            string Country = String.IsNullOrWhiteSpace(context.Request["Country"]) ? null : context.Request["Country"];
            string City = String.IsNullOrWhiteSpace(context.Request["City"]) ? null : context.Request["City"];
            string Captcha = String.IsNullOrWhiteSpace(context.Request["Captcha"]) ? null : context.Request["Captcha"];
            //string Subscribed = String.IsNullOrWhiteSpace(context.Request["Subscribe"]) ? null : context.Request["Subscribe"];
            bool IsSubscribed = String.IsNullOrEmpty(context.Request["Subscribe"]) ? false : true;

            string originalName = "";
            DateTime DopDT = DateTime.UtcNow;

            Dictionary<string, string> errorMsgs = new Dictionary<string, string>();

            #region Validation

            if (ModelNum == null)
            {
                isValid = false;
                errorMsgs.Add("ModelNum", "m1");
            }
            else if (!DBHelper.GetWarrantyModel(ModelNum))
            {
                isValid = false;
                errorMsgs.Add("ModelNum", "m2");
            }
            else
            {
                ModelNum = ModelNum.ToUpper().Replace("－", "-");
            }
            

            if (CaseNum != null)
            {
                if (!DBHelper.GetWarrantyCase(CaseNum))
                {
                    isValid = false;
                    errorMsgs.Add("CaseNum", "m2");
                }
                else
                {
                    CaseNum = CaseNum.ToUpper();
                }

            }
            

            if (WarrantyNum == null)
            {
                isValid = false;
                errorMsgs.Add("WarrantyNum", "m1");
            }
            else if (!DBHelper.GetWarrantyCard(WarrantyNum))
            {
                isValid = false;
                errorMsgs.Add("WarrantyNum", "m2");
            }

            if (Name == null)
            {
                isValid = false;
                errorMsgs.Add("Name", "m1");
            }
            else
            {
                Name = Name.ToUpper();
            }
            

            if (Title == null)
            {
                isValid = false;
                errorMsgs.Add("Title", "m1");
            }

            if (Email != null)
            {
                if (!RegexUtilities.IsValidEmail(Email))
                {
                    isValid = false;
                    errorMsgs.Add("Email", "m2");
                }
                else
                {
                    Email = Email.ToLower();
                }
            }
            

            //Country Code -> +852 +853 +86
            if (Ccode == null)
            {
                isValid = false;
                errorMsgs.Add("Ccode", "m1");

            }

            if (Phone == null)
            {
                isValid = false;
                errorMsgs.Add("Phone", "m1");

            }

            if (Dop == null)
            {
                isValid = false;
                errorMsgs.Add("Dop", "m1");
            }
            else
            {
                try
                {
                    DateTime dt =DateTime.ParseExact("11/11/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime dtMax = DateTime.ParseExact("10/01/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DopDT = DateTime.Parse(Dop);

                    if (DopDT < dt || DopDT > dtMax)
                    {
                        isValid = false;
                        errorMsgs.Add("Dop", "m3");
                    }
                }
                catch
                {
                    isValid = false;
                    errorMsgs.Add("Dop", "m2");
                }
            }

            
            if (!String.IsNullOrEmpty(InvNum))
            {
                InvNum = InvNum.ToUpper();
            }
            

            if (Country == null)
            {
                isValid = false;
                errorMsgs.Add("Country", "m1");
            }

            if (City == null)
            {
                isValid = false;
                errorMsgs.Add("City", "m1");
            }

#if (DEBUG)
            if (Captcha != "1234")
            {
                isValid = false;
                errorMsgs.Add("Captcha", "m1");
            }
#else
            if (Captcha == null)
            {
                isValid = false;
                errorMsgs.Add("Captcha", "m1");
            }
            else if (!CaptchaHelper.isValidCaptcha(context,Captcha))
            {
                isValid = false;
                errorMsgs.Add("Captcha", "m2");
            }
#endif


                //clear Captchat session no matter pass / fail
                context.Session["Captcha"] = null;
            
#endregion

            if (isValid)
            {
                try
                {
                    WarrantyRegistration warrantyReg = new WarrantyRegistration()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        ModelNum = ModelNum,
                        CaseNum = CaseNum,
                        WarrantyNum = WarrantyNum,

                        Name = Name,
                        Title = Title,
                        Phone = Ccode + " " + Phone,
                        Email = Email,
                        Dop = DopDT,

                        InvNum = InvNum,
                        Country = Country,
                        City = City,
                        IsSubscribed = IsSubscribed

                    };

                    originalName = warrantyReg.Name;
                    warrantyReg.Name = CryptoHelper.encryptAES(warrantyReg.Name, DBHelper.defaultSKey);
                    //warrantyReg.Phone = CryptoHelper.encryptAES(warrantyReg.Phone, DBHelper.defaultSKey);
                    if (!String.IsNullOrEmpty(warrantyReg.Email))
                    {
                        warrantyReg.Email = CryptoHelper.encryptAES(warrantyReg.Email, DBHelper.defaultSKey);
                    }

                    //Check if DB already have exactly same records
                    //if exist, just don't insert into DB, but still display "Thank You Page" to user
                    var NameList = DBHelper.CheckRegistration(warrantyReg);
                    bool isExist = false;
                    if (NameList.Count > 0)
                    {
                        foreach(string n in NameList) { 
                            string decryptName = CryptoHelper.decryptAES(n, DBHelper.defaultSKey);
                            if(decryptName == originalName)
                            {
                                //same info and same name!
                                isExist = true;
                            }
                        }
                    }

                    if (!isExist)
                    {
                        string ret = "";
                        if(Ccode == "+86")
                            ret = Helper.SendSMS(Phone);

                        warrantyReg.SmsResult = ret;
                        DBHelper.AddRegistration(warrantyReg);
                    }

                    output.message = (isExist) ? "yes" : "no";
                    output.status = 1;

                }
                catch(Exception ex)
                {
                    output.message = "Unable Insert To DB: " + ex.Message;
                }
                


            }
            else
            {
                output.data = errorMsgs;
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
