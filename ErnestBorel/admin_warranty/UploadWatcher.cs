using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel.admin_warranty
{
    public class UploadWatcher
    {
        public static string status = "Idle";
        public static int ttl = 0;
        public static int current = 0;
        public static string message = "";

        public static void Reset()
        {
            status = "Idle";
            ttl = 0;
            current = 0;
        }

        public static object getObj()
        {
            return new
            {
                status = status,
                ttl = ttl,
                current = current,
                message = message
            };
        }
    }
}