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
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "Message", Theme = "@style/Base.Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]

    public class MessagesCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Messages);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);

            ImageView Nav_MsgIV93 = FindViewById<ImageView>(Resource.Id.Nav_MsgIV93);
            Nav_MsgIV93.Click += Nav_MsgIV93_Click;

            ImageView Mess_PodNaviSyn = FindViewById<ImageView>(Resource.Id.Mess_PodNaviSyn);
            Mess_PodNaviSyn.Click += Mess_PodNaviSyn_Click;

            ImageView Mess_PodNavHme = FindViewById<ImageView>(Resource.Id.Mess_PodNavHme);
            Mess_PodNavHme.Click += Mess_PodNavHme_Click;

            ImageView Mess_PodNavStg = FindViewById<ImageView>(Resource.Id.Mess_PodNavStg);
            Mess_PodNavStg.Click += Mess_PodNavStg_Click;

        }

        private void Mess_PodNavStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        private void Mess_PodNavHme_Click(object sender, EventArgs e)
        {
           
        }

        private void Mess_PodNaviSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }

        private void Nav_MsgIV93_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));
        }

        //private void BtnNavMessNext_Click(object sender, EventArgs e)
        //{
        //    StartActivity(typeof(HomeCode));
        //}

        void setupDrawerContent(NavigationView navigationView)
        {
            try
            {
                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    e.MenuItem.SetChecked(true);
                    if (e.MenuItem.ToString() == "Home Page")
                    {
                        StartActivity(typeof(PodPackingListFilterListCode));
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch
            {
                Toast.MakeText(this, "OnCreateOptionMenu", ToastLength.Short).Show();
                return false;
            }

        }

    }
}