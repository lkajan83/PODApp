using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using PODApp;
using PODApp.Controls;
using PODApp.Models;

namespace PODApp.ViewModels
{

    public static class AppInsert
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }
    [Activity(Label = "InsertCameraScreenCode")]
    public class InsertCameraScreenCode : Activity
    {

        private ImageView _imageView;
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(AppInsert._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            AppInsert.bitmap = AppInsert._file.Path.LoadAndResizeBitmap(width, height);

            //image binging 
            //if (CustPackingSlipJour1.Podimages == null)
            //    CustPackingSlipJour1.Podimages = new List<Bitmap>();

            //CustPackingSlipJour1.Podimages[0] = AppInsert.bitmap;
            if (AppInsert.bitmap != null)
            {
                _imageView.SetImageBitmap(AppInsert.bitmap);
                AppInsert.bitmap = null;
            }
            // Dispose of the Java side bitmap.
            GC.Collect();
        }

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.InsertCameraScreen);

                if (IsThereAnAppToTakePictures())
                {
                    CreateDirectoryForPictures();

                    //save button  edit 
                    Button CamBtnSavePod = FindViewById<Button>(Resource.Id.InsertCamBtnSavePod);
                    CamBtnSavePod.Click += CamBtnSavePod_Click;
                    // save button edit 

                    Button button = FindViewById<Button>(Resource.Id.InsertPODmyButtonCam);
                    _imageView = FindViewById<ImageView>(Resource.Id.InsertPodImageViewCam);
                    button.Click += TakeAPicture;
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "OnCreate Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        /// <summary>
        /// Camera Save Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CamBtnSavePod_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBCustPackingSlipJourCode));
        }

        /// <summary>
        /// CreateDirectoryForPictures
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            try
            {
                AppInsert._dir = new File(
                    Environment.GetExternalStoragePublicDirectory(
                        Environment.DirectoryPictures), "CameraAppDemo");
                if (!AppInsert._dir.Exists())
                {
                    AppInsert._dir.Mkdirs();
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "CreateDirectoryForPictures Error Handling", ToastLength.Short).Show();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            try
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                IList<ResolveInfo> availableActivities =
                    PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
                return availableActivities != null && availableActivities.Count > 0;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "IsThereAnAppToTakePictures Error Handling", ToastLength.Short).Show();
                return false;
            }
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            try
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);

                AppInsert._file = new File(AppInsert._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

                intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(AppInsert._file));

                StartActivityForResult(intent, 0);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "TakeAPicture Error Handling", ToastLength.Short).Show();
                return;
            }

        }
    }
}