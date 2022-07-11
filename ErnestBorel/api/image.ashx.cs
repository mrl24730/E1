using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for image
    /// </summary>
    public class image : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string w = context.Request["w"];
            string h = context.Request["h"];
            string file = context.Request["file"];
            string[] filepath = file.Split('.');
            string extension = (filepath[(filepath.Length-1)]).ToLower();
            int width = 180, height = 180;
            
            if(!String.IsNullOrEmpty(w)) Int32.TryParse(w, out width);
            if (!String.IsNullOrEmpty(h)) Int32.TryParse(h, out height);

            ImageFormat imgFormat;
            switch (extension)
            {
                case "png":
                    context.Response.ContentType = "image/png";
                    imgFormat = ImageFormat.Png;
                    break;

                case "gif":
                    context.Response.ContentType = "image/gif";
                    imgFormat = ImageFormat.Gif;
                    break;

                case "jpg":
                    context.Response.ContentType = "image/jpeg";
                    imgFormat = ImageFormat.Jpeg;
                    break;

                default:
                    context.Response.ContentType = "image/jpeg";
                    imgFormat = ImageFormat.Jpeg;
                    break;
            }
            

            string path = context.Server.MapPath("~/images/" + file);
            Image image = System.Drawing.Image.FromFile(path);
            int X = image.Width;
            int Y = image.Height;
            if (X > Y)
            {
                height = (int)((width * Y) / X);
            }
            else
            {
                width = (int)((height * X) / Y);
            }

            using (Image thumbnail = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    
                    thumbnail.Save(memoryStream, imgFormat);
                    memoryStream.WriteTo(context.Response.OutputStream);
                    /*
                    Byte[] bytes = new Byte[memoryStream.Length];
                    memoryStream.Position = 0;
                    memoryStream.Read(bytes, 0, (int)bytes.Length);
                    //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    //Image2.ImageUrl = "data:image/png;base64," + base64String;
                    //Image2.Visible = true;
                    */
                }
            }

            image.Dispose();

        }

        public bool ThumbnailCallback()
        {
            return false;
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