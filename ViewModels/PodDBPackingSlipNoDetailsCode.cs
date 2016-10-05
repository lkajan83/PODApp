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

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDBPackingSlipNoDetailsCode")]
    public class PodDBPackingSlipNoDetailsCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PodDBPackingSlipNoDetails);

            Button BtnPSNDPodCam = FindViewById<Button>(Resource.Id.BtnPSNDPodCam);
            BtnPSNDPodCam.Click += BtnPSNDPodCam_Click;
        }

        private void BtnPSNDPodCam_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodCameraScreenCode));
        }
    }
}