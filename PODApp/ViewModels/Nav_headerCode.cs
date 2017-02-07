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
using PODApp.Controls;
using PODApp.Models;
using PODApp.Bootstrap;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "Nav_headerCode", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Nav_headerCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Nav_header);
            TextView navheader_username = FindViewById<TextView>(Resource.Id.navheader_username);
            navheader_username.Text = LoginUsers_tmp.UserName;

        }
    }
}