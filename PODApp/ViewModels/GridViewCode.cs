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
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "GridViewCode", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GridViewCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GridView);
            var gridview = FindViewById<GridView>(Resource.Id.gridview);
            gridview.Adapter = new ImageAdapter(this);

            gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
                Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            };
        }
    }
}