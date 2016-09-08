﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace ShoppingApp.Models
{
    public class ImageUploadValidator
    {
        public bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            // Check if there is an object
            if (file == null)
                return false;
            //Check size: must be less than 2 MB and greater than 1 KB
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                        ImageFormat.Png.Equals(img.RawFormat) ||
                        ImageFormat.Gif.Equals(img.RawFormat);
                }
            }

            catch
            {
                return false;
            }
        }
    }
}