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
    [Activity(Icon = "@drawable/ArrowBack", Label = "ADMIN DASHBOARD", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AnimationAdminPanelCode : AppCompatActivity
    {

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        int count = 1;

        Button Righttoleft;
        Button bottomtotop;

        Button lefttoright;
        Button toptobottom;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AnimationAdminPanel);

            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            AccountSetting.LoadSetting();


            Righttoleft = FindViewById<Button>(Resource.Id.righttoleft);
            bottomtotop = FindViewById<Button>(Resource.Id.bottomtotop);

            lefttoright = FindViewById<Button>(Resource.Id.lefttoright);
            toptobottom = FindViewById<Button>(Resource.Id.toptobottom);


            Righttoleft.Click += Righttoleft_Click;
            bottomtotop.Click += bottomtotop_Click;

            lefttoright.Click += Lefttoright_Click;
            toptobottom.Click += Toptobottom_Click;


            //SETTING IMAGEVIEW 
            ImageView AnAdPan_PodStg = FindViewById<ImageView>(Resource.Id.AnAdPan_PodStg);
            AnAdPan_PodStg.Click += AnAdPan_PodStg_Click;

            //HOME IMAGEVIEW 
            ImageView AnAdPan_PodiHme = FindViewById<ImageView>(Resource.Id.AnAdPan_PodiHme);
            AnAdPan_PodiHme.Click += AnAdPan_PodiHme_Click;

            //SYN IMAGEVIEW 
            ImageView AnAdPan_PodiSyn = FindViewById<ImageView>(Resource.Id.AnAdPan_PodiSyn);
            AnAdPan_PodiSyn.Click += AnAdPan_PodiSyn_Click;

            //BACK IMAGEVIEW 
            ImageView NavAd_AdminImgBke = FindViewById<ImageView>(Resource.Id.NavAd_AdminImgBke);
            NavAd_AdminImgBke.Click += NavAd_AdminImgBke_Click;

            Button Attnt_InstPackingSlipId = FindViewById<Button>(Resource.Id.Attnt_InstPackingSlipId);
            Attnt_InstPackingSlipId.Click += Attnt_InstPackingSlipId_Click;



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }

        private void Attnt_InstPackingSlipId_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingSlipAttachmenCode));
        }


        /// <summary>
        /// BACK IMAGEVIEW 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAd_AdminImgBke_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }


        private void AnAdPan_PodiSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }

        private void AnAdPan_PodiHme_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }

        private void AnAdPan_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        //PACKING SLIP FILTER LIST
        private void Righttoleft_Click(object sender, EventArgs e)
        {
            try
            {
                Intent intent;
                intent = new Intent(this, typeof(PodPackingListFilterListCode));
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.@Side_in_right, Resource.Animation.@Side_out_left);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "", ToastLength.Short).Show();
                return;
            }
         }

        //PACKING SLIP DETAILS
        private void bottomtotop_Click(object sender, EventArgs e)
        {
            try
            {
                Intent intent;
                intent = new Intent(this, typeof(PodDBCustPackingSlipJourCode));
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.@Pushup_in, Resource.Animation.@Pushup_out);
            }
            catch
            {
                Toast.MakeText(this, "bottomtotop_Click Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        //INSERT PACKING SLIP JOUR
        private void Lefttoright_Click(object sender, EventArgs e)
        {
            try
            {
                Intent intent;
                intent = new Intent(this, typeof(PodDBCustPackingSlipJourLineCode));
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.@Side_in_left, Resource.Animation.@Side_out_right);
            }
            catch
            {
                Toast.MakeText(this, "Lefttoright_Click Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        //INSERT PACKING SLIP JOUR LINE 
        private void Toptobottom_Click(object sender, EventArgs e)
        {
            try
            {
                Intent intent;
                intent = new Intent(this, typeof(PODReportsCode));
                StartActivity(intent);
                OverridePendingTransition(Resource.Animation.@Pushdown_in, Resource.Animation.@Pushdown_out);
            }
            catch
            {
                Toast.MakeText(this, "Toptobottom_Click Error Handling", ToastLength.Short).Show();
                return;
            }
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
            catch
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
                Toast.MakeText(this, "NavigationItemSelected Error Handling", ToastLength.Short).Show();
                return false;
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
    }
}

