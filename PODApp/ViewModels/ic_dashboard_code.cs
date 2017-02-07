using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "ic_dashboard_code", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ic_dashboard_code : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ic_dashboard);

            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);

            ////Toolbar will now take on default actionbar characteristics
            //SetActionBar(toolbar);

            //ActionBar.Title = "Hello from Toolbar";


            //var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);

            //toolbarBottom.Title = "Photo Editing";
            //toolbarBottom.InflateMenu(Resource.Menu.photo_edit);
            //toolbarBottom.MenuItemClick += (sender, e) => {
            //    Toast.MakeText(this, "Bottom toolbar pressed: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            //};
        }

        ///// <Docs>The options menu in which you place your items.</Docs>
        ///// <returns>To be added.</returns>
        ///// <summary>
        ///// This is the menu for the Toolbar/Action Bar to use
        ///// </summary>
        ///// <param name="menu">Menu.</param>
        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.home, menu);
        //    return base.OnCreateOptionsMenu(menu);
        //}
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
        //    return base.OnOptionsItemSelected(item);
        //}
    }
}


