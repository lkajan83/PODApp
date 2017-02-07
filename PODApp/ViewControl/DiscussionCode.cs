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

namespace PODApp.ViewModels
{

    [Activity(Label = "Discussion", Theme = "@style/Theme.DesignDemo")]
    public class DiscussionCode : AppCompatActivity
    {

        DrawerLayout drawerLayout;
        NavigationView navigationView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Discussion);           

            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);

            ImageView Diss_Nav_ImgVBK = FindViewById<ImageView>(Resource.Id.Diss_Nav_ImgVBK);
            Diss_Nav_ImgVBK.Click += Diss_Nav_ImgVBK_Click;

            ImageView Dis_PodNaviSyn = FindViewById<ImageView>(Resource.Id.Dis_PodNaviSyn);
            Dis_PodNaviSyn.Click += Dis_PodNaviSyn_Click;

            ImageView Dis_PodNavHme = FindViewById<ImageView>(Resource.Id.Dis_PodNavHme);
            Dis_PodNavHme.Click += Dis_PodNavHme_Click;

            ImageView Dis_PodNavStg = FindViewById<ImageView>(Resource.Id.Dis_PodNavStg);
            Dis_PodNavStg.Click += Dis_PodNavStg_Click;
        }

        private void Dis_PodNavStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        private void Dis_PodNavHme_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));
        }

        private void Dis_PodNaviSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }

        private void Diss_Nav_ImgVBK_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));
        }

        void setupDrawerContent(NavigationView navigationView)
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            navigationView.InflateMenu(Resource.Menu.nav_menu);
            return true;

        }

    }
}