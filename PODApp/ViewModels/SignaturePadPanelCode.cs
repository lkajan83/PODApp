using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using PODApp.Models;
using SignaturePad;
using System.IO;
using SignaturePad;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using System.Threading;
using PODApp.Controls;
using Java.IO;
using Android.Provider;
using Uri = Android.Net.Uri;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "SIGNATURE", Icon = "@drawable/ArrowBack", Theme = "@style/Base.Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SignaturePadPanelCode : AppCompatActivity
    {
        System.Drawing.PointF[] points;
        PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignaturePadPanel);
            AccountSetting.LoadSetting();

            //ImageView btnFSigPadBack = FindViewById<ImageView>(Resource.Id.btnFSigPadBack);
            //btnFSigPadBack.Click += BtnFSigPadBack_Click;

            ImageView btnPodiSignClr = FindViewById<ImageView>(Resource.Id.btnPodiSignClr);
            btnPodiSignClr.Click += BtnPodiSignClr_Click;


            signature = FindViewById<SignaturePadView>(Resource.Id.signatureView);


            if (true)
            { // Customization activated
                try
                {
                    View root = FindViewById<View>(Resource.Id.rootView);

                    // Activate this to internally use a bitmap to store the strokes
                    // (good for frequent-redraw situations, bad for memory footprint)
                    // signature.UseBitmapBuffer = true;

                    signature.Caption.Text = "Authorization Signature";
                    signature.Caption.SetTypeface(Typeface.Serif, TypefaceStyle.BoldItalic);
                    signature.Caption.SetTextSize(global::Android.Util.ComplexUnitType.Sp, 16f);
                    signature.SignaturePrompt.Text = ">>";
                    signature.SignaturePrompt.SetTypeface(Typeface.SansSerif, TypefaceStyle.Normal);
                    signature.SignaturePrompt.SetTextSize(global::Android.Util.ComplexUnitType.Sp, 32f);
                    signature.BackgroundColor = Color.Rgb(255, 255, 200); // a light yellow.
                    signature.StrokeColor = Color.Black;

                    signature.BackgroundImageView.SetImageResource(Resource.Drawable.logo_galaxy_black_64);
                    signature.BackgroundImageView.SetAlpha(16);
                    signature.BackgroundImageView.SetAdjustViewBounds(true);

                    var layout = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    layout.AddRule(LayoutRules.CenterInParent);
                    layout.SetMargins(20, 20, 20, 20);
                    signature.BackgroundImageView.LayoutParameters = layout;

                    // You can change paddings for positioning...
                    var caption = signature.Caption;
                    caption.SetPadding(caption.PaddingLeft, 1, caption.PaddingRight, 5);
                }
                catch (Exception)
                {
                    Toast.MakeText(this, " Customization activated Error Handling", ToastLength.Short).Show();
                    return;
                }
            }

            // Get our button from the layout resource,
            // and attach an event to it
            ImageView btnSave = FindViewById<ImageView>(Resource.Id.btnSave);
            btnSave.Click += BtnSave_Click;
            btnSave.Dispose();
            //StartActivity(typeof(PodDBPackingSlipNoDetailsCode));

            ImageView btnLoad = FindViewById<ImageView>(Resource.Id.btnLoad);
            btnLoad.Click += delegate
            {
                if (points != null)
                    signature.LoadPoints(points);
            };
            btnLoad.Dispose();

        }

        private void BtnPodiSignClr_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "I Have write any method", ToastLength.Short).Show();
        }

        /// <summary>
        /// Save Image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveImage();
            //var ProgDiaglog = ProgressDialog.Show(this, "Please wait..", "Signature is being saved...", true);
            //new Thread(new ThreadStart(delegate
            //{
            //    RunOnUiThread(() => { SaveImage(); });
            //    RunOnUiThread(() => { ProgDiaglog.Hide(); });

            //})).Start();

        }
        /// <summary>
        /// Save Image Method
        /// </summary>
        private void SaveImage()
        {
            try
            {
                if (signature.IsBlank)
                {
                    //Display the base line for the user to sign on.
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetMessage("No signature to save.");
                    alert.SetNeutralButton("Okay", delegate { });
                    alert.Create().Show();
                }
                points = signature.Points;

                //points = signature.Points;
                var image = signature.GetImage();

                //byte[] sigimage = null;

                //MemoryStream stream = new MemoryStream();
                //image.Compress(Bitmap.CompressFormat.Png, 0, stream);
                ////image.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                //sigimage = stream.ToArray();

                CustPackingSlipJour1.SignatureImg = image;

                byte[] sigimage = null;
                Bitmap bmp = signature.GetImage();
                MemoryStream stream = new MemoryStream();
                bmp.Compress(Bitmap.CompressFormat.Png, 0, stream);
                //image.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                sigimage = stream.ToArray();
                // var bytes = stream.WriteByte();

                //you can create a new file name "test.BMP" in sdcard folder.
                //File f = new File(Environment.
                //                        + File.separator + "test.bmp")

                App._dir = new Java.IO.File(
                    Android.OS.Environment.GetExternalStoragePublicDirectory(
                        Android.OS.Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId + "_Sig");
                if (!App._dir.Exists())
                {
                    App._dir.Mkdirs();
                }
                else
                {
                    App._dir.Delete();
                    App._dir.Mkdirs();
                }


                App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}_Sig.jpg", Guid.NewGuid()));



                App._file.CreateNewFile();
                //write the bytes in file
                FileOutputStream fo = new FileOutputStream(App._file);
                fo.Write(sigimage);
                DBA_PackingSlipAttachment dbr = new DBA_PackingSlipAttachment();
                string result1 = dbr.UpdatePackingSlipAttachment_Signature (CustPackingSlipJour1.PackingSlipId, true);

                // remember close de FileOutput
                fo.Close();
                Toast.MakeText(this, "Signature Saved Successfully", ToastLength.Short).Show();





                //Intent intent = new Intent();
                //intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
                //StartActivityForResult(intent, 0);


                // Toast.MakeText(this, "Check me ", ToastLength.Short).Show();
                //var PodPkgSpDel = new Intent(this, typeof(PodDBPackingSlipNoDetailsCode));
                //PodPkgSpDel.PutExtra("Parent", "SignaturePadPanelCode");
                //StartActivity(PodPkgSpDel);

                //StartActivity(typeof(PodDBPackingSlipNoDetailsCode));

                //Toast.MakeText(this, "Before Database in SaveImage", ToastLength.Short).Show();
                //string result = dbr.UpdateSignatureImage(CustPackingSlipJour1.Recid, sigimage);
                //Toast.MakeText(this, result, ToastLength.Short).Show();
                StartActivity(typeof(PodDBPackingSlipNoDetailsCode));
            }
            catch (Exception)
            {
                Toast.MakeText(this, "SaveImage Method", ToastLength.Short).Show();
                return;
            }

        }
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

                // Display in ImageView. We will resize the bitmap to fit the display
                // Loading the full sized image will consume to much memory 
                // and cause the application to crash.

                //int height = 0;
                //int width = 0;
                //if (AccountSetting.PodPicReson == 0)
                //{
                //    height = 365;
                //    width = 242;
                //}
                //else if (AccountSetting.PodPicReson == 1)
                //{
                //    height = 500;
                //    width = 500;
                //}
                //else
                //{
                //    height = 960;
                //    width = 644;
                //}

                //App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
                ////ExportBitmapAsPNG(App.bitmap);

                ////image binging 
                //Podimages.Add(App.bitmap);
                //if (App.bitmap != null)
                //{
                //    _imageView.SetImageBitmap(App.bitmap);
                //    App.bitmap = null;
                //}
                // Dispose of the Java side bitmap.
                GC.Collect();
            }
            catch (Exception)
            {
                Toast.MakeText(this, "PodCamera", ToastLength.Short).Show();
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

        #region private
        private SignaturePadView signature;
        #endregion
    }
}
