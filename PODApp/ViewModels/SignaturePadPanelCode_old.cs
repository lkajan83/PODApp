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

namespace PODApp.ViewModels
{
    [Activity(Label = "PODApp", Icon = "@drawable/ArrowBack", Theme = "@style/Theme.DesignDemo")]
    public class SignaturePadPanelCode : AppCompatActivity
    {
        System.Drawing.PointF[] points;
        PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignaturePadPanel);

            ImageView btnFSigPadBack = FindViewById<ImageView>(Resource.Id.btnFSigPadBack);
            btnFSigPadBack.Click += BtnFSigPadBack_Click;


            SignaturePadView signature = FindViewById<SignaturePadView>(Resource.Id.signatureView);


            if (true)
            { // Customization activated
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
                caption.SetPadding(caption.PaddingLeft, 1, caption.PaddingRight, 25);
            }

            // Get our button from the layout resource,
            // and attach an event to it
            ImageView btnSave = FindViewById<ImageView>(Resource.Id.btnSave);
            btnSave.Click += delegate {
                if (signature.IsBlank)
                {//Display the base line for the user to sign on.
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetMessage("No signature to save.");
                    alert.SetNeutralButton("Okay", delegate { });
                    alert.Create().Show();
                }
                points = signature.Points;

                //points = signature.Points;
                var image = signature.GetImage();
                byte[] sigimage = null;

                MemoryStream stream = new MemoryStream();
                image.Compress(Bitmap.CompressFormat.Png, 0, stream);
                sigimage = stream.ToArray();

                string result = dbr.UpdateSignatureImage(PODApp.Models.CustPackingSlipJour1.Recid, sigimage);
                Toast.MakeText(this, result, ToastLength.Short).Show();

            };
            btnSave.Dispose();
            //StartActivity(typeof(PodDBPackingSlipNoDetailsCode));

            ImageView btnLoad = FindViewById<ImageView>(Resource.Id.btnLoad);
            btnLoad.Click += delegate {
                if (points != null)
                    signature.LoadPoints(points);
            };
            btnLoad.Dispose();

        }

        private void BtnFSigPadBack_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBPackingSlipNoDetailsCode));
        }
    }
}