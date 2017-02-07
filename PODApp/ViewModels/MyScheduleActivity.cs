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
    public class MyScheduleActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.MySchedule);

            //TextView textview = new TextView(this);
            //textview.Text = "This is the My Schedule tab";
            //SetContentView(textview);
        }
    }
}