using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Uri = Android.Net.Uri;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "PickImageFromGalleryCode", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PickImageFromGalleryCode : Activity
    {
        public static readonly int PickImageId = 1000;
        ImageView _imageView;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Uri uri = data.Data;
                _imageView.SetImageURI(uri);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.PickImageFromGallery);
            _imageView = FindViewById<ImageView>(Resource.Id.PodPickImageView);
            //_imageView = FindViewById<ImageView>(Resource.Id.PodPickImageView);
            Button PodPickImageBtn = FindViewById<Button>(Resource.Id.PodPickImageBtn);
            PodPickImageBtn.Click += PodPickImageBtn_Click;
        }

       void PodPickImageBtn_Click(object sender, EventArgs eventArgs)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }       
    }
}