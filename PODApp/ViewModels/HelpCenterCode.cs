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
using PODApp.Models;
using PODApp.Controls;
using Android.Database;
using SQLite;

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

using Android.Webkit;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Icon = "@drawable/ArrowBack", Label = "Help Center", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HelpCenterCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        //STEP - 50
        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.HelpCenter);


            ImageView NavHelpCen_PodiSyn = FindViewById<ImageView>(Resource.Id.NavHelpCen_PodiSyn);
            NavHelpCen_PodiSyn.Click += NavHelpCen_PodiSyn_Click;

            ImageView NavHelpCen_PodiHme = FindViewById<ImageView>(Resource.Id.NavHelpCen_PodiHme);
            NavHelpCen_PodiHme.Click += NavHelpCen_PodiHme_Click;

            ImageView NavHelpCen_PodStg = FindViewById<ImageView>(Resource.Id.NavHelpCen_PodStg);
            NavHelpCen_PodStg.Click += NavHelpCen_PodStg_Click;

            ImageView Nav_HelpCenB2B = FindViewById<ImageView>(Resource.Id.Nav_HelpCenB2B);
            Nav_HelpCenB2B.Click += Nav_HelpCenB2B_Click;

            //WebView NavHelpCenwebview = FindViewById<WebView>(Resource.Id.NavHelpCenwebview);
            //NavHelpCenwebview.Settings.JavaScriptEnabled = true;
            //NavHelpCenwebview.LoadUrl("http://www.wlv.ac.uk");

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

        private void Nav_HelpCenB2B_Click(object sender, EventArgs e)
        {
            //StartActivity(typeof(PodPackingListFilterListCode));
            base.OnBackPressed();
        }

        private void NavHelpCen_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        private void NavHelpCen_PodiHme_Click(object sender, EventArgs e)
        {
            MethodPODNavCollapse();
            MethodPODNavManualSrt();
            MethodPODNavPictuResn();
        }


        private void NavHelpCen_PodiSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
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
                Toast.MakeText(this, "NavigationItemSelected Error Handling", ToastLength.Short).Show();
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

                ToggleButton CusCollapse = FindViewById<ToggleButton>(Resource.Id.CusCollapse);
                ToggleButton PicManulSrt = FindViewById<ToggleButton>(Resource.Id.PicManulSrt);
                if (PicManulSrt.Checked || CusCollapse.Checked)
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
                    NavManualSrt.PutExtra("Parent", "HelpCenterCode");
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
                Toast.MakeText(this, "OnCreateOptionsMenu Error Handling", ToastLength.Short).Show();
                return false;
            }

        }
    }
}