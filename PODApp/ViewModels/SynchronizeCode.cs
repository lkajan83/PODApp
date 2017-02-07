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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using PODApp.Models;
using Newtonsoft.Json;
using System.Text;
using PODApp.Models;
using PODApp.Controls;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity( Label = "Web Service", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SynchronizeCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Synchronize);

            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            AccountSetting.LoadSetting();

            ImageView NavSyn_BaktoHme = FindViewById<ImageView>(Resource.Id.NavSyn_BaktoHme);
            NavSyn_BaktoHme.Click += NavSyn_BaktoHme_Click;


            ImageView NavSyn_PodiHme = FindViewById<ImageView>(Resource.Id.NavSyn_PodiHme);
            NavSyn_PodiHme.Click += NavSyn_PodiHme_Click;

            ImageView NavSyn_PodStg = FindViewById<ImageView>(Resource.Id.NavSyn_PodStg);
            NavSyn_PodStg.Click += NavSyn_PodStg_Click;


            Button SychronizeWeb = FindViewById<Button>(Resource.Id.SychronizeWeb);
            SychronizeWeb.Click += SychronizeWeb_Click;

            ImageView BtnPODUploadWeb = FindViewById<ImageView>(Resource.Id.BtnPODUploadWeb);
            BtnPODUploadWeb.Click += BtnPODUploadWeb_Click;

            TextView NavHeader_UNE = FindViewById<TextView>(Resource.Id.NavHeader_UNE);
            NavHeader_UNE.Text = Models.LoginUsers_tmp.UserName;

            //SetContentView(Resource.Layout.Nav_header);
            //TextView navheader_username = FindViewById<TextView>(Resource.Id.navheader_username);
            //navheader_username.Click += Navheader_username_Click;
            ////navheader_username.Text = LoginTable_tmp.username;

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
        /// <summary>
        /// online upload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPODUploadWeb_Click(object sender, EventArgs e)
        {
            try
            {
                PODApp.Controls.PodDb_CustPackingSlipJour obj_db = new PODApp.Controls.PodDb_CustPackingSlipJour();
                //GetAllCustPackingSlipJour
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.BaseAddress = new Uri("http://podapiservice.cloudapp.net/badges/GetAllCustPackingSlipJour");
                client.BaseAddress = new Uri("http://localhost:61685/badges/GetAllCustPackingSlipJour");               
                client.DefaultRequestHeaders.Accept.Clear();
                List<CustPackingSlipJour> obj_lst = obj_db.GetAllCustPackingSlipJour();
                foreach (var item in obj_lst)
                {
                    http://localhost:61685/Badges/GetAllCustPackingSlipJour
                    var json = JsonConvert.SerializeObject(item);
                    StringContent queryString = new StringContent(json);
                    var stringContent = new StringContent(json,
                             UnicodeEncoding.UTF8,
                             "application/json");

                    try
                    {
                        var response = client.PostAsync(new Uri("http://localhost:61685/badges/PostUpdateBadge"), stringContent).Result;
                        //var response = client.PostAsync(new Uri("http://podapiservice.cloudapp.net/badges/PostUpdateBadge"), stringContent).Result;
                        //var response = client.GetAsync(new Uri("http://podapiservice.cloudapp.net/badges/GetAllCustPackingSlipJour")).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Toast.MakeText(this, "Sync has done", ToastLength.Short).Show();
                            StartActivity(typeof(PodPackingListFilterListCode));
                            //StartActivity(typeof(ProgressBarCode));
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, "Sync has not  done", ToastLength.Short).Show();
                    }
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "BtnPODUploadWeb_Click Error Handing", ToastLength.Short).Show();
                return;
            }

        }

        private void SychronizeWeb_Click(object sender, EventArgs e)
        {
           
        }

        private void NavSyn_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        private void NavSyn_PodiHme_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }


        private void NavSyn_BaktoHme_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
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