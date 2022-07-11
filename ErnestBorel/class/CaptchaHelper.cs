using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace ErnestBorel
{
	public class CaptchaHelper
	{
        public static byte[] GetCaptchaBytes(HttpContext context, int iWidth = 300, int iHeight = 200)
        {
 
            Random oRandom = new Random();

            int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
            int[] aTextColor = new int[] { 0, 0, 0 };
            //int[] aFontEmSizes = new int[] { 100, 105, 110, 115, 120 };
            int[] aFontEmSizes = new int[] { 12, 16, 14, 8, 10 };

            string[] aFontNames = new string[] { "Comic Sans MS", "Arial", "Times New Roman", "Georgia", "Verdana", "Geneva" };
            FontStyle[] aFontStyles = new FontStyle[] { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Underline };
            HatchStyle[] aHatchStyles = new HatchStyle[] { HatchStyle.BackwardDiagonal, HatchStyle.Cross, 
                HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
                HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
                HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, 
                HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
                HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
                HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
                HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
                HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
                HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
                HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
                HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
                HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
                HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
                HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag};

            //Get Captcha in Session
            string sCaptchaText;

            context.Session["Captcha"] = sCaptchaText = oRandom.Next(1111, 9999).ToString();



            //Creates an output Bitmap
            Bitmap oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
            Graphics oGraphics = Graphics.FromImage(oOutputBitmap);
            oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Create a Drawing area
            RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
            Brush oBrush = default(Brush);

            //Draw background (Lighter colors RGB 100 to 255)
            oBrush = new HatchBrush(aHatchStyles[oRandom.Next(aHatchStyles.Length - 1)],
                Color.FromArgb(oRandom.Next(100, 255), oRandom.Next(100, 255), oRandom.Next(100, 255)),
                Color.White);
            oGraphics.FillRectangle(oBrush, oRectangleF);

            System.Drawing.Drawing2D.Matrix oMatrix = new System.Drawing.Drawing2D.Matrix();
            int i = 0;
            int iChars = sCaptchaText.Length + 1;
            int gutter = iWidth / iChars / 2;

            for (i = 0; i <= sCaptchaText.Length - 1; i++)
            {
                oMatrix.Reset();

                int x = iWidth / iChars * i + gutter;
                int y = iHeight / 2;

                //Rotate text Random
                oMatrix.RotateAt(oRandom.Next(-30, 30), new PointF(x, y));
                oGraphics.Transform = oMatrix;

                //Draw the letters with Random Font Type, Size and Color
                oGraphics.DrawString
                (
                    //Text
                sCaptchaText.Substring(i, 1),
                    //Random Font Name and Style
                new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
                   aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
                   aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                new SolidBrush(Color.FromArgb(oRandom.Next(0, 150),
                   oRandom.Next(0, 150), oRandom.Next(0, 150))),
                x,
                oRandom.Next(1, 10)
                );
                oGraphics.ResetTransform();
            }
            
            MemoryStream oMemoryStream = new MemoryStream();
            oOutputBitmap.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] oBytes = oMemoryStream.GetBuffer();

            oOutputBitmap.Dispose();
            oMemoryStream.Close();

            return oBytes;
        }

        public static bool isValidCaptcha(HttpContext context,string userTxt)
        {
            return (userTxt == (string) context.Session["Captcha"]);
        }
	}
}