using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//for converting IP address string to and Uint 
using System.Net;
using System.Net.Mail;

namespace Kitchen
{
    public static class AppHelper
    {

        //public static string mailserver = "103.26.121.194";
        //public static string mailserver = "103.26.121.220";
        public static string mailserver = "mail.ernestborel.ch";

        //replace fullname with Xxxxxxxxx - 1st letter is capital X
        public static string hideName(string name)
        {

            string hideName = "";
            string croxx = "Xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            int nameLen = name.Length;
            int spaceAt = name.IndexOf(" ");

            if (nameLen == 0) return "";

            bool firstCharIsChinese = name.ToCharArray()[0] > 255;
            if (firstCharIsChinese && nameLen > 1)
            {
                hideName = "X" + name.Substring(1, nameLen - 1);
            }
            else if (spaceAt > 0)
            {
                hideName = croxx.Substring(0, spaceAt) + name.Substring(spaceAt, nameLen - spaceAt);
            }
            else if (nameLen > 5)
            {
                hideName = croxx.Substring(0, 3) + name.Substring(3, nameLen - 3);
            }
            else
            {
                hideName = name;
            }

            return hideName;
        }

        //convert string IPv4 to Uint - use 32bit uint for caching is less expesnive
        public static uint IP2UInt(string addr)
        {
            uint intAddress = (uint)BitConverter.ToInt32(IPAddress.Parse(addr).GetAddressBytes(), 0);
            return intAddress;
        }

        //convert int IPv4 to String
        public static string Uint2IP(uint intAddress)
        {
            string ipAddress = new IPAddress(BitConverter.GetBytes(intAddress)).ToString();
            return ipAddress;
        }

        //calculate a int count of seconds since a reference date - time (UTC)
        public static int secondSince(DateTime refDate)
        {
            int sec = 0;
            DateTime now = DateTime.Now;

            sec = (int)((now.Ticks - refDate.Ticks) / 10000000);

            return sec;
        }

        public static int secondSince()
        {
            DateTime refDate = new DateTime(DateTime.UtcNow.Year,1,1,0,0,0).ToUniversalTime();
            return secondSince(refDate);
        }

        //Return a datetime object given the int seconds elapsed
        public static void SecondElapsedToDate(int secondElapsed, DateTime refdate, out DateTime dt)
        {
            dt = refdate.ToUniversalTime().AddSeconds(secondElapsed);
        }
        public static void SecondElapsedToDate(int secondElapsed, out DateTime dt)
        {
            DateTime refDate = new DateTime(DateTime.UtcNow.Year, 1, 1, 0, 0, 0).ToUniversalTime();
            SecondElapsedToDate(secondElapsed, refDate, out dt);
        }

        //Checks the input integer's min / max is ok, if not returns 0, default range 999,999,999 to 111,111,111
        //overload -----------------------------------
        public static int intBounds(string str)
        {
            return intBounds(str, 999999999, 111111111);
        }

        public static int intBounds(int val)
        {
            return intBounds(val, 999999999, 111111111);
        }

        public static int intBounds(string str, int max, int min)
        {
            int val = 0;
            try
            {
                val = Convert.ToInt32(str);
                val = intBounds(val, max, min);
            }
            catch
            {
                //something's wrong!
            }
            return val;
        }

        public static int intBounds(int val, int max, int min)
        {
            if (isIntBounds(val, max, min))
            {
                return val;
            }
            else
            {
                return 0;
            }
        }

        public static bool isIntBounds(int val)
        {
            return isIntBounds(val, 999999999, 111111111);
        }

        public static bool isIntBounds(int val, int max, int min)
        {
            return Math.Max(Math.Min(val, max), min) == val;
        }

        //function to send email
        public static bool sendMail(string[] to, string from, string subject, string body, string fromname = "", string _mailServer=null, string[] bcc = null)
        {
            bool sent = false;
            int i = 0;

            string smtpServer = mailserver;
            MailMessage mail = new MailMessage();
            MailAddress sender = new MailAddress(from, fromname, System.Text.Encoding.UTF8);
            mail.From = sender;

            for (i = 0; i < to.Length; i++)
            {
                mail.To.Add(to[i]);
            }

            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.IsBodyHtml = true;
            mail.Body = body;

            if (bcc != null)
            {
                for (i = 0; i < bcc.Length; i++)
                {
                    mail.Bcc.Add(bcc[i]);
                }
            }

            if (_mailServer == null && mailserver == null)
            {
                throw new ArgumentNullException("mailserver", "No default mail server and the mailserver parameter is null.");
            }
            else
            {
                if (_mailServer != null) smtpServer = _mailServer;
                SmtpClient smtp = new SmtpClient(smtpServer, 2025);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("order@ernestborel.ch", "8Gemeinu");

                try
                {
                    smtp.Send(mail);
                    sent = true;
                }
                catch 
                {
                    sent = false;
                }
                finally
                {
                    smtp.Dispose();
                }

            }


            return sent;
        }

        //stop current application
        public static bool stopApp()
        {
            bool done = true;
            try
            {
                System.Web.HttpRuntime.UnloadAppDomain();
            }
            catch
            {
                done = false;
            }

            return done;
        }

    }


}
