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

using PODApp.Models;
using PODApp.Controls;
using PODApp.ViewModels;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Icon = "@drawable/ArrowBack",Label = "HomePage", Theme = "@style/Base.Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HomeCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Home);
            AccountSetting.LoadSetting();

            //BACK IMAGEVIEW 
            ImageView Nav_HmeBACK = FindViewById<ImageView>(Resource.Id.Nav_HmeBACK);
            Nav_HmeBACK.Click += Nav_HmeBACK_Click;

            //SYN IMAGEVIEW
            ImageView PodNaviSyn = FindViewById<ImageView>(Resource.Id.PodNaviSyn);
            PodNaviSyn.Click += PodNaviSyn_Click;

            //HOME IMAGEVIEW
            ImageView PodNavHme = FindViewById<ImageView>(Resource.Id.PodNavHme);
            PodNavHme.Click += PodNavHme_Click;

            //SETTING IMAGEVIEW
            ImageView PodNavStg = FindViewById<ImageView>(Resource.Id.PodNavStg);
            PodNavStg.Click += PodNavStg_Click;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);


            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// SYN IMAGEVIEW  CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodNaviSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }

        /// <summary>
        /// SETTING IMAGEVIEW  CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodNavStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        /// <summary>
        /// HOME IMAGEVIEW CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodNavHme_Click(object sender, EventArgs e)
        {            
            MethodSettingsOnOff();
        }

  
        /// <summary>
        /// BACK IMAGEVIEW CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nav_HmeBACK_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();

        }

        /// <summary>
        /// NAVIGATION VIEW CONTROL
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
            catch
            {
                Toast.MakeText(this, "NavigationItemSelected Error Handling", ToastLength.Short).Show();
                return;
            }
        }


        /// <summary>
        /// NAVIGATION VIEW CONTROL
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
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
        /// METOD OF SETTING CONTROL
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