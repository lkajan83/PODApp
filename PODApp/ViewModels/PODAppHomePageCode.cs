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
using Android.Graphics;
using Android.Content.PM;


namespace PODApp.ViewModels
{
    [Activity(Label = "HOME PAGE", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PODAppHomePageCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PODAppHomePage);           

            //Initialize UI
            this.InitializeUI();           
        }

        private void InitializeUI()
        {
            AccountSetting.LoadSetting();
            //BACK 2 BACK
            ImageView PodAppHmePge_Back2Back = FindViewById<ImageView>(Resource.Id.PodAppHmePge_Back2Back);

            //DEVICE ID 
            TextView NavHeader_DviesID = FindViewById<TextView>(Resource.Id.NavHeader_DviesID);
            NavHeader_DviesID.Text = Models.LoginUsers_tmp.DeviceId.ToString();
          
            //SETTING 
            ImageView PodAppHmePge_PodStg = FindViewById<ImageView>(Resource.Id.PodAppHmePge_PodStg);
            //HOME
            ImageView PodAppHmePge_PodiHme = FindViewById<ImageView>(Resource.Id.PodAppHmePge_PodiHme);
            //Syn
            ImageView PodAppHmePge_PodiSynUpload = FindViewById<ImageView>(Resource.Id.PodAppHmePge_PodiSynUpload);

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
                Toast.MakeText(this, "Synchronize NavigationItemSelected", ToastLength.Short).Show();
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

            navigationView.InflateMenu(Resource.Menu.nav_menu);
            return true;

        }
    }
}