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

using System.Timers;

namespace PODApp.ViewModels
{
    [Activity(Label = "Progress Bar", Theme = "@style/Theme.DesignDemo")]
    public class ProgressBarCode : Activity
    {
        ProgressBar progressBar;
        Timer _timer;
        int _countSeconds;
        object _lock = new object();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ProgressBar);
            progressBar = FindViewById<ProgressBar>(Resource.Id.ProsBar);

            progressBar.Max = 2;
            progressBar.Progress = 0;

            _timer = new System.Timers.Timer();
            _countSeconds = 2;
            _timer.Enabled = true;
            _timer.Interval = 2;
            _timer.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            _countSeconds -= 1;

            RunOnUiThread(() =>
            {
                progressBar.IncrementProgressBy(2);
                CheckProgress(progressBar.Progress);
            });
        }

        public void CheckProgress(int progress)
        {
            lock (_lock)
            {
                if (progress >= 1)
                {
                    progressBar.ProgressDrawable.
                    SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.Multiply);

                    Toast.MakeText(this, "Navigate to next", ToastLength.Long).Show();
                    _timer.Dispose();
                    StartActivity(typeof(PodDBPackingSlipNoDetailsCode));        // Navigate to another activity
                }
                //if (progress < 10)
                //{
                //    progressBar.ProgressDrawable.
                //    SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.Multiply);
                //}
            }
        }
    }
}