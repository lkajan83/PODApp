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
    public class AccountNewSettingsCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AccountNewSettings);

            //Initialize UI
            this.InitializeUI();
         }

        private void InitializeUI()
        {
            AccountSetting.LoadSetting();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);


            ImageView NavAccSet_B2ASet = FindViewById<ImageView>(Resource.Id.NavAccSet_B2ASet);
            NavAccSet_B2ASet.Click += NavAccSet_B2ASet_Click;
            ImageView PodResetSaveBtn = FindViewById<ImageView>(Resource.Id.PodResetSaveBtn);
            PodResetSaveBtn.Click += PodResetSaveBtn_Click;
            EditText PodResetOldPWD = FindViewById<EditText>(Resource.Id.PodResetOldPWD);
            EditText PodResetNewPWD = FindViewById<EditText>(Resource.Id.PodResetNewPWD);
            EditText PodResetConPWD = FindViewById<EditText>(Resource.Id.PodResetConPWD);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////         


        }
        /// <summary>
        /// save Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodResetSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                EditText PodResetOldPWD = FindViewById<EditText>(Resource.Id.PodResetOldPWD);
                EditText PodResetNewPWD = FindViewById<EditText>(Resource.Id.PodResetNewPWD);
                EditText PodResetConPWD = FindViewById<EditText>(Resource.Id.PodResetConPWD);

                if (PodResetNewPWD.Text == PodResetConPWD.Text)
                {
                    if (LoginUsers_tmp.Password == PodResetOldPWD.Text)
                    {
                        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                        //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticsLocation");
                        var db = new SQLiteConnection(dbPath);

                        LoginUsers item = new LoginUsers();
                        item.DeviceId = LoginUsers_tmp.DeviceId;
                        item.Password = PodResetNewPWD.Text;
                        item.UserName = LoginUsers_tmp.UserName;
                        db.Update(item);
                        Toast.MakeText(this, "Password Updated", ToastLength.Short).Show();
                        StartActivity(typeof(PodCameraScreenCode));
                    }
                    else
                    {
                        Toast.MakeText(this, "Invalid Old Password", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Paasword and Confirm Password Should Match", ToastLength.Short).Show();
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "PodResetSave Image Exception Error", ToastLength.Short).Show();
                return;
            }

        }

        /// <summary>
        /// BACK BUTTON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAccSet_B2ASet_Click(object sender, EventArgs e)
        {            
            base.OnBackPressed();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                Toast.MakeText(this, "AccountNew SettingNavigationItemSelected Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        /// <summary>
        /// METHOD SETTING ON / OFF
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
            catch(Exception)
            {
                Toast.MakeText(this, "OnCreateOptionsMenu Exception Error", ToastLength.Short).Show();
                return false;
            }
        }
    }
}