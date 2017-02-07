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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Content.Res;
using System.Threading.Tasks;
using PODApp.Models;

using PODApp.ViewModels;
using PODApp.Controls;
using PODApp;
using System.IO;

namespace PODApp.ViewModels
{
    public static class App
    {
        public static Java.IO.File _file;
        public static Java.IO.File _dir;
        public static Bitmap bitmap;
    }

    [Activity(Label = "CAMERA", Theme = "@style/Base.Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PodCameraScreenCode : AppCompatActivity
    {

        private ImageView _imageView;
        List<Bitmap> Podimages = new List<Bitmap>();

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        ///////////////////////////////////////////////////////////////////////////////ResizeImage
        //MethodPODNavPictuResn(); 
        //365 x 242    = 38KB small
        //500 x 500    = 101KB medium
        //960 x 644    = 571KB large
        // MyScheduleActivity OWN PERPOSE OF THIS APPLICATION   
        ///////////////////////////////////////////////////////////////////////////////

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);

                // Make it available in the gallery

                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Uri contentUri = Uri.FromFile(App._file);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);
                DBA_PackingSlipAttachment dbr = new DBA_PackingSlipAttachment();
                string result1 = dbr.UpdatePackingSlipAttachment_Attachment(CustPackingSlipJour1.PackingSlipId, true);

                // Display in ImageView. We will resize the bitmap to fit the display
                // Loading the full sized image will consume to much memory 
                // and cause the application to crash.

                int height = 0;
                int width = 0;
                if (AccountSetting.PodPicReson == 0)
                {
                    height = 365;
                    width = 242;
                }
                else if (AccountSetting.PodPicReson == 1)
                {
                    height = 500;
                    width = 500;
                }
                else
                {
                    height = 960;
                    width = 644;
                }

                App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
                //ExportBitmapAsPNG(App.bitmap);

                //image binging 
                Podimages.Add(App.bitmap);
                if (App.bitmap != null)
                {
                    _imageView.SetImageBitmap(App.bitmap);
                    App.bitmap = null;
                }
                // Dispose of the Java side bitmap.
                GC.Collect();
            }
            catch (Exception)
            {
                Toast.MakeText(this, "PodCamera", ToastLength.Short).Show();
                return;
            }
        }

        // protected override void OnCreate(Bundle bundle)
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.PodCameraScreen);
                AccountSetting.LoadSetting();

                if (IsThereAnAppToTakePictures())
                {
                    CreateDirectoryForPictures();

                    //save button  edit Button 
                    ImageView CamBtnSavePod = FindViewById<ImageView>(Resource.Id.CamBtnSavePod);
                    CamBtnSavePod.Click += CamBtnSavePod_Click;
                    // save button edit 

                    ImageView PODmyButtonCam = FindViewById<ImageView>(Resource.Id.PODmyButtonCam);
                    _imageView = FindViewById<ImageView>(Resource.Id.PodImageViewCam);
                    PODmyButtonCam.Click += TakeAPicture;
                }

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);


                // Create ActionBarDrawerToggle button and add it to the toolbar
                var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
                SetSupportActionBar(toolbar);


                var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
                drawerLayout.SetDrawerListener(drawerToggle);
                drawerToggle.SyncState();

                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                setupDrawerContent(navigationView);

                //Back Button/Image 
                ImageView CamNav_ImgBke = FindViewById<ImageView>(Resource.Id.CamNav_ImgBke);
                CamNav_ImgBke.Click += CamNav_ImgBke_Click;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "OnCreate Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        private void CamNav_ImgBke_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBPackingSlipNoDetailsCode));
        }

        /// <summary>
        /// SAVE CAMERA OPTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CamBtnSavePod_Click(object sender, EventArgs e)
        {
            try
            {

                App._dir = new Java.IO.File(
                    Environment.GetExternalStoragePublicDirectory(
                        Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId);

                var files = App._dir.ListFiles();

                if (files.Length > 10)
                {
                    Toast.MakeText(this, "Length Exceeds more thean 10", ToastLength.Short).Show();
                    //THIS CODE ONLY WORK WHEN USER TRY TO INSERT MORE THAN 10 IMAGE 
                    var LengthExceeds = new Intent(this, typeof(PodDBPackingSlipNoDetailsCode));
                    LengthExceeds.PutExtra("Parent", "PodCameraScreenCode");
                    StartActivity(LengthExceeds);
                }
                else
                {
                    Toast.MakeText(this, "Camera Image Saved Successfully", ToastLength.Short).Show();

                    var CamActivity = new Intent(this, typeof(PodDBPackingSlipNoDetailsCode));
                    CamActivity.PutExtra("Parent", "PodCameraScreenCode");
                    StartActivity(CamActivity);
                }

            }
            catch (Exception)
            {
                Toast.MakeText(this, "Camera Image Save Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        private void CreateDirectoryForPictures()
        {
            try
            {
                App._dir = new Java.IO.File(
                    Environment.GetExternalStoragePublicDirectory(
                        Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId);

                var files = App._dir.ListFiles();

                if (!App._dir.Exists())
                {
                    App._dir.Mkdirs();
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Camera Create Directory Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;  
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {

            App._dir = new Java.IO.File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId);

            var files = App._dir.ListFiles();

            if (files.Length > 10)
            {
                Toast.MakeText(this, "Length Exceeds more thean 10", ToastLength.Short).Show();
                var ImgLengthExceeds = new Intent(this, typeof(PodDBPackingSlipNoDetailsCode));
                ImgLengthExceeds.PutExtra("Parent", "PodCameraScreenCode");
                StartActivity(ImgLengthExceeds);
            }
            else
            {

                Intent intent = new Intent(MediaStore.ActionImageCapture);
                App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
                intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
                StartActivityForResult(intent, 0);
            }
           
        }
        void ExportBitmapAsPNG(Bitmap bitmap)
        {
            App._file.Delete();
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var filePath = new Java.IO.File(
                    Environment.GetExternalStoragePublicDirectory(
                        Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId);
            if (!filePath.Exists())
            {
                filePath.Mkdirs();
            }
            //System.IO.Path.Combine(sdCardPath+ "/Pictures/"+CustPackingSlipJour1.PackingSlipId, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            var stream = new FileStream(filePath.AbsolutePath+"/"+CustPackingSlipJour1.PackingSlipId, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Close();
        }


        void setupDrawerContent(NavigationView navigationView)
        {
            try
            {
                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    e.MenuItem.SetChecked(true);
                    if (e.MenuItem.ToString() == "Home Page")
                    {
                        MethodSettingsOnOff();

                    }
                    else if (e.MenuItem.ToString() == "Administrator")
                    {
                        StartActivity(typeof(TabHostOneCode));
                    }
                    else if (e.MenuItem.ToString() == "Account Settings")
                    {
                        StartActivity(typeof(ActSettingCode));
                    }
                    else if (e.MenuItem.ToString() == "Terms and Policies")
                    {
                        StartActivity(typeof(TermsPoliciesCode));
                    }
                    else if (e.MenuItem.ToString() == "Report a Problem")
                    {
                        StartActivity(typeof(ReportAProblemCode));
                    }
                    else if (e.MenuItem.ToString() == "Help Center")
                    {
                        StartActivity(typeof(HelpCenterCode));
                    }
                    else if (e.MenuItem.ToString() == "About us")
                    {
                        StartActivity(typeof(AboutusCode));
                    }
                    else if (e.MenuItem.ToString() == "Logout")
                    {
                        StartActivity(typeof(MainActivity));
                    }

                    drawerLayout.CloseDrawers();
                };
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Camera NavigationItemSelected Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        private void MethodSettingsOnOff()
        {
            try
            {
                if (AccountSetting.podcollapse == true && AccountSetting.podmodelsort == true)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else if (AccountSetting.podcollapse == true && AccountSetting.podmodelsort == false)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else if (AccountSetting.podcollapse == false && AccountSetting.podmodelsort == true)
                {
                    var NavManualSrt = new Intent(this, typeof(PodDraggableListView));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = false;
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Create Database", ToastLength.Short).Show();
                return;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Camera OnCreateOptionsMenu Exception Error", ToastLength.Short).Show();
                return true;
            }
        }



    }
}
