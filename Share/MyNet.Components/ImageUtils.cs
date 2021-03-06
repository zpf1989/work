﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MyNet.Components
{
    public class ImageUtils
    {
        /// <summary>
        /// 将图片转换成字节
        /// </summary>
        /// <param name="selectPictureFile"></param>
        /// <returns></returns>
        public static Byte[] ImageToByteArray(string selectPictureFile)
        {
            if (!File.Exists(selectPictureFile))
            {
                return null;
            }

            using (Image img = Image.FromFile(selectPictureFile))
            {
                return ImageToByteArray(img);
            }
        }

        public static byte[] ImageToByteArray(Image img)
        {
            if (img == null)
            {
                return null;
            }
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (Bitmap newBitmap = new Bitmap(img))
                {
                    newBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imagedata = ms.GetBuffer();
                    return imagedata;
                }
            }
        }

        public static Image ByteArrayToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 1)
            {
                return null;
            }
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
                if (img == null)
                {
                    return null;
                }
                img = ImageUtils.CopyImage(img, img.Width, img.Height);
                return img;
            }
        }

        /// <summary>
        /// 复制图片
        /// </summary>
        /// <param name="imgSrc">源图片</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static Image CopyImage(Image imgSrc, int width, int height)
        {
            if (imgSrc == null)
            {
                return null;
            }
            int newWidth = (width <= 0) ? imgSrc.Width : width;
            int newHeight = (width <= 0) ? imgSrc.Height : height;

            Bitmap bmp = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            bmp.SetResolution(imgSrc.HorizontalResolution, imgSrc.VerticalResolution);
            using (Graphics g = Graphics.FromImage(bmp))
            {

                // 用白色清空 
                g.Clear(Color.White);

                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 指定高质量、低速度呈现。 
                g.SmoothingMode = SmoothingMode.HighQuality;

                // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
                g.DrawImage(imgSrc, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, imgSrc.Width, imgSrc.Height), GraphicsUnit.Pixel);
            }
            return bmp;
        }

        /// <summary>
        /// base64编码图片
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string Base64Encode(Image img)
        {
            if (img == null)
            {
                return "";
            }
            byte[] bytes = ImageUtils.ImageToByteArray(img);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// base64编码图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string Base64Encode(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (Image img = Image.FromFile(filePath))
            {
                byte[] bytes = ImageUtils.ImageToByteArray(img);

                return Convert.ToBase64String(bytes);
            }

        }

        public static Image Base64Decode(string strBase64)
        {
            if (string.IsNullOrEmpty(strBase64))
            {
                return null;
            }
            try
            {
                var bytes = Convert.FromBase64String(strBase64);
                return ImageUtils.ByteArrayToImage(bytes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
