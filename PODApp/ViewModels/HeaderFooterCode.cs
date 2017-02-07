
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
    [Activity(Label = "HelloToolbar", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HeaderFooterCode : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.HeaderFooter);

            var toolbar = FindViewById<Toolbar>(Resource.Id.PodHFtoolbar);

            //Toolbar will now take on default actionbar characteristics
            SetActionBar(toolbar);

            ActionBar.Title = "Hello from Toolbar";


            var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);

            toolbarBottom.Title = "Photo Editing";
            toolbarBottom.InflateMenu(Resource.Menu.photo_edit);
            toolbarBottom.MenuItemClick += (sender, e) => {
                Toast.MakeText(this, "Bottom toolbar pressed: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            };
        }

        /// <Docs>The options menu in which you place your items.</Docs>
        /// <returns>To be added.</returns>
        /// <summary>
        /// This is the menu for the Toolbar/Action Bar to use
        /// </summary>
        /// <param name="menu">Menu.</param>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                MenuInflater.Inflate(Resource.Menu.home, menu);
                return base.OnCreateOptionsMenu(menu);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "OnCreateOptionsMenu Error Handling", ToastLength.Short).Show();
                //I didn't understand
                return false;
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
                return base.OnOptionsItemSelected(item);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "OnCreateOptionsMenu Error Handling", ToastLength.Short).Show();
                // I didn't Understand 
                return true;
            }
        }
    }
}