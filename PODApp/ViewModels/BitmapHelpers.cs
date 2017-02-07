using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Graphics;
using Android.Util;

namespace PODApp.ViewModels
{
    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            // Main Killer 
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                if (width > 0)
                {
                    inSampleSize = outWidth > outHeight
                                       ? outHeight / height
                                       : outWidth / width;
                }
                else
                {
                    inSampleSize = 1;
                }
            }



            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }


        //////////////////////////////////////////////////////////
        //ImageServiceDroid
        /// //////////////////////////////////////////////////////

        public class ImageServiceDroid : IImageService
        {
            public void ResizeImage(string sourceFile, string targetFile, float maxWidth, float maxHeight)
            {
                if (!File.Exists(targetFile) && File.Exists(sourceFile))
                {
                    // First decode with inJustDecodeBounds=true to check dimensions
                    var options = new BitmapFactory.Options()
                    {
                        InJustDecodeBounds = false,
                        InPurgeable = true,
                    };

                    using (var image = BitmapFactory.DecodeFile(sourceFile, options))
                    {
                        if (image != null)
                        {
                            var sourceSize = new Size((int)image.GetBitmapInfo().Height, (int)image.GetBitmapInfo().Width);

                            var maxResizeFactor = Math.Min(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);

                            string targetDir = System.IO.Path.GetDirectoryName(targetFile);
                            if (!Directory.Exists(targetDir))
                                Directory.CreateDirectory(targetDir);

                            if (maxResizeFactor > 0.9)
                            {
                                File.Copy(sourceFile, targetFile);
                            }
                            else
                            {
                                var width = (int)(maxResizeFactor * sourceSize.Width);
                                var height = (int)(maxResizeFactor * sourceSize.Height);

                                using (var bitmapScaled = Bitmap.CreateScaledBitmap(image, height, width, true))
                                {
                                    using (Stream outStream = File.Create(targetFile))
                                    {
                                        if (targetFile.ToLower().EndsWith("png"))
                                            bitmapScaled.Compress(Bitmap.CompressFormat.Png, 100, outStream);
                                        else
                                            bitmapScaled.Compress(Bitmap.CompressFormat.Jpeg, 95, outStream);
                                    }
                                    bitmapScaled.Recycle();
                                }
                            }

                            image.Recycle();
                        }
                        //else
                        //    // Toast.MakeText(this,"mmmmmmmmmmmm", ToastLength.Short).Show();
                        //   // Log.E("Image scaling failed: " + sourceFile);
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////



    }
}

