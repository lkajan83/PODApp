using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

using PODApp.Controls;
using PODApp.Models;
using PODApp.Bootstrap;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Icon = "@drawable/ArrowBack", Label = "PODApp", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class NavigationDrawerCode :  AppCompatActivity 
    {

        DrawerLayout drawerLayout;
        NavigationView navigationView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NavigationDrawer);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);


            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
        }
        void setupDrawerContent(NavigationView navigationView)
        {
            try
            {
                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    e.MenuItem.SetChecked(true);
                    if (e.MenuItem.ToString() == "Home Page")
                    {
                        MethodPODNavCollapse();
                        MethodPODNavManualSrt();
                        MethodPODNavPictuResn();
                    }


                    else if (e.MenuItem.ToString() == "Account Settings")
                    {
                        StartActivity(typeof(ActSettingCode));
                    }



                    else if (e.MenuItem.ToString() == "Report a Problem")
                    {
                        StartActivity(typeof(ReportAProblemCode));
                    }

                    else if (e.MenuItem.ToString() == "Help Center")
                    {
                        StartActivity(typeof(HelpCenterCode));
                    }

                    else if (e.MenuItem.ToString() == "About us")
                    {
                        StartActivity(typeof(AboutusCode));
                    }
                    else if (e.MenuItem.ToString() == "Logout")
                    {
                         StartActivity(typeof(MainActivity));
                    }


                    drawerLayout.CloseDrawers();
                };
            }
            catch (Exception)
            {
                Toast.MakeText(this, "NavigationDrawer Error Handing", ToastLength.Short).Show();
                return;
            }
        }

        private void MethodPODNavCollapse()
        {
            try
            {
                // if (m_ParentActivity != PodNavManSet == true)
                if (AccountSetting.podcollapse)
                {
                    var PckSlipNavManSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                   AccountSetting.podcollapse = true;
                    PckSlipNavManSrt.PutExtra("Parent", "PodDBPackingSlipNoDetailsCode");
                    StartActivity(PckSlipNavManSrt);
                }
                else
                {
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    var PckSlipNavManSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                   AccountSetting.podcollapse = false;
                    PckSlipNavManSrt.PutExtra("Parent", "PodDBPackingSlipNoDetailsCode");
                    StartActivity(PckSlipNavManSrt);
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Create Database", ToastLength.Short).Show();
                return;
            }

        }
        private void MethodPODNavPictuResn()
        {
            try
            {
                //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
                //if (AccountSetting.PodPicReson && spinner.Count > 0)
                //{
                //    AccountSetting.PodPicReson = true;
                //}
                //else
                //{
                //    AccountSetting.PodPicReson = false;
                //}
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Picture Resolution Error Handle", ToastLength.Short).Show();
                return;
            }
        }
        private void MethodPODNavManualSrt()
        {
            try
            {

                if (AccountSetting.podmodelsort)
                {
                    var NavManualSrt = new Intent(this, typeof(PodDraggableListView));
                    Toast.MakeText(this, "checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = true;
                    NavManualSrt.PutExtra("Parent", "PodDraggableListView");
                    StartActivity(NavManualSrt);
                }
                else
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = false;
                    NavManualSrt.PutExtra("Parent", "NavigationDrawerCode");
                    StartActivity(NavManualSrt);
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Create Database", ToastLength.Short).Show();
                return;
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "OnCreateOptionMenu", ToastLength.Short).Show();
                return false;
            }
        }
    }
}

