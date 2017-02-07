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
    [Activity]
    public class WhatsOnActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                TextView textview = new TextView(this);
                textview.Text = "This is WhatsOn tab";
                SetContentView(textview);            
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "OnCreate Exception Error", ToastLength.Short).Show();
                return;
            }
}

    }
}