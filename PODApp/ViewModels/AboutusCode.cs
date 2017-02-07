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
using Android.Webkit;

using PODApp.Controls;
using PODApp.Models;
using PODApp.ViewModels;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "About us", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AboutusCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Aboutus);
                       
            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            AccountSetting.LoadSetting();

            ImageView NavAbotUsBaK = FindViewById<ImageView>(Resource.Id.NavAbotUsBaK);
            NavAbotUsBaK.Click += NavAbotUsBaK_Click;

            ImageView NavAboutUs_PodiSyn = FindViewById<ImageView>(Resource.Id.NavAboutUs_PodiSyn);
            NavAboutUs_PodiSyn.Click += NavAboutUs_PodiSyn_Click;

            ImageView NavAboutUs_PodiHme = FindViewById<ImageView>(Resource.Id.NavAboutUs_PodiHme);
            NavAboutUs_PodiHme.Click += NavAboutUs_PodiHme_Click;

            ImageView NavAboutUs_PodStg = FindViewById<ImageView>(Resource.Id.NavAboutUs_PodStg);
            NavAboutUs_PodStg.Click += NavAboutUs_PodStg_Click;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// NAV ABOUT - BACK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAbotUsBaK_Click(object sender, EventArgs e)
        {
          
            base.OnBackPressed();
        }

        /// <summary>
        /// NAV SETTING
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAboutUs_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterCollapseCode));
        }

        /// <summary>
        /// HOME IMAGE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAboutUs_PodiHme_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }

        /// <summary>
        /// NAV SYN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAboutUs_PodiSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }

        /// <summary>
        /// Navigation View
        /// </summary>
        /// <param name="navigationView"></param>
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
                Toast.MakeText(this, "NavigationView Error Handling", ToastLength.Short).Show();
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

        /// <summary>
        /// METHOD SETTING ON/OFF
        /// </summary>
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
    }
}