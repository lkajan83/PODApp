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

using System.Threading;
using PODApp.Models;
using PODApp.Controls;
using PODApp.ViewModels;
using System.IO;
using SQLite;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "Account Settings", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AccountSettingsCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private object txtAcSetUName;
        private object txtAcSetPwd;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AccountSettings);

            //Initialize UI
            this.InitializeUI();


        }
        private void InitializeUI()
        {

            AccountSetting.LoadSetting();

            TextView txtAcSetUName = FindViewById<TextView>(Resource.Id.txtAcSetUName);
            txtAcSetUName.Text = LoginUsers_tmp.UserName;

            TextView txtAcSetPwd = FindViewById<TextView>(Resource.Id.txtAcSetPwd);
            txtAcSetPwd.Text = LoginUsers_tmp.Password;

            ImageView txtAcSet_PodCngPwd = FindViewById<ImageView>(Resource.Id.txtAcSet_PodCngPwd);
            txtAcSet_PodCngPwd.Click += TxtAcSet_PodCngPwd_Click;

            ImageView txtAcSet_PodBack = FindViewById<ImageView>(Resource.Id.txtAcSet_PodBack);
            txtAcSet_PodBack.Click += TxtAcSet_PodBack_Click;

            ImageView ImgAcSet_NavB2AC = FindViewById<ImageView>(Resource.Id.ImgAcSet_NavB2AC);
            ImgAcSet_NavB2AC.Click += ImgAcSet_NavB2AC_Click;


            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////

        }
        /// <summary>
        /// BACK BUTTON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgAcSet_NavB2AC_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        /// <summary>
        /// I THING NEED TO DELETE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TxtAcSet_PodBack_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        /// <summary>
        /// ACCOUNT SETTING PASSWORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAcSet_PodCngPwd_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountNewSettingsCode));
        }
        
        /// <summary>
        /// NAVIGATION VIEW
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
                Toast.MakeText(this, "Account Setting NavigationItemSelected Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        /// <summary>
        /// METHOD NAVIGATION SETTING 
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "OnCreateOptionsMenu navigationView Exception Error", ToastLength.Short).Show();
                return false;
            }

        }
    }
}