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
using PODApp.Bootstrap;
using PODApp.Models;

using Android.Webkit;

namespace PODApp.ViewModels
{
    [Activity(Label = "Report a Problem", Theme = "@style/Theme.DesignDemo")] 
    public class ReportAProblemCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        //STEP - 50
        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ReportAProblem);
            AccountSetting.LoadSetting();

            ImageView NavRAP_PodiSyn = FindViewById<ImageView>(Resource.Id.NavRAP_PodiSyn);
            NavRAP_PodiSyn.Click += NavRAP_PodiSyn_Click;

            ImageView NavRAP_PodiHme = FindViewById<ImageView>(Resource.Id.NavRAP_PodiHme);
            NavRAP_PodiHme.Click += NavRAP_PodiHme_Click;

            ImageView NavRAP_PodStg = FindViewById<ImageView>(Resource.Id.NavRAP_PodStg);
            NavRAP_PodStg.Click += NavRAP_PodStg_Click;

            ImageView NavImgB2RAP = FindViewById<ImageView>(Resource.Id.NavImgB2RAP);
            NavImgB2RAP.Click += NavImgB2RAP_Click;

            WebView NavReportProWebview = FindViewById<WebView>(Resource.Id.NavReportProWebview);
            NavReportProWebview.Settings.JavaScriptEnabled = true;
            NavReportProWebview.LoadUrl("https://intelisenseit.atlassian.net/login");

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

        private void NavImgB2RAP_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));
        }

        private void NavRAP_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        private void NavRAP_PodiHme_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }

        private void NavRAP_PodiSyn_Click(object sender, EventArgs e)
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
                        MethodSettingsOnOff();
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
                Toast.MakeText(this, "NavigationView ReportAProblem Error Handling", ToastLength.Short).Show();
                return;
            }
        }


        private void MethodSettingsOnOff()
        {
            try
            {
                if (AccountSetting.podcollapse == true && AccountSetting.podmodelsort == true)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else if (AccountSetting.podcollapse == true && AccountSetting.podmodelsort == false)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else if (AccountSetting.podcollapse == false && AccountSetting.podmodelsort == true)
                {
                    var NavManualSrt = new Intent(this, typeof(PodDraggableListView));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = false;
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
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
            catch(Exception)
            {
                Toast.MakeText(this, "OnCreateOptionMenu", ToastLength.Short).Show();
                return false;
            }

        }
    }
}