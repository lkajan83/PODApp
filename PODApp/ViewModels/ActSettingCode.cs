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
using Android.Webkit;

using PODApp.ViewModels;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "Account Setting", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]

    public class ActSettingCode : AppCompatActivity 
    {
        
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        string myString;
        bool myBool;

   
        string PodPackingListFilterCollapseCode = "PackingListFilterCollapse";
        string PodDraggableListView = "DraggableListView";
        string PodPackingListFilterListCode = "PackingListFilterList";
       


        ToggleButton CusCollapse;
        ToggleButton PicManulSrt;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ActSetting);

            //Initialize UI
            this.InitializeUI();

        }

        private void InitializeUI()
        {
            //ACCOUNT SETTING
            AccountSetting.LoadSetting();

            //CUSTOMER LINE
            CusCollapse = FindViewById<ToggleButton>(Resource.Id.CusCollapse);
            CusCollapse.Click += CusCollapse_Click;
            CusCollapse.Checked = AccountSetting.podcollapse;

            //MANUAL SORT
            PicManulSrt = FindViewById<ToggleButton>(Resource.Id.PicManulSrt);
            PicManulSrt.Click += PicManulSrt_Click;
            PicManulSrt.Checked = AccountSetting.podmodelsort;

            //BACK IMAGE
            ImageView NavAbotUsBaK = FindViewById<ImageView>(Resource.Id.NavAbotUsBaK);
            NavAbotUsBaK.Click += NavAbotUsBaK_Click;

            //SYNRINIZE IMAGE
            ImageView NavSet_PodiSyn = FindViewById<ImageView>(Resource.Id.NavSet_PodiSyn);
            NavSet_PodiSyn.Click += NavSet_PodiSyn_Click;

            //HOME IMAGE
            ImageView NavSet_PodiHme = FindViewById<ImageView>(Resource.Id.NavSet_PodiHme);
            NavSet_PodiHme.Click += NavSet_PodiHme_Click;

            //SETTING IMAGE
            ImageView NavSet_PodStg = FindViewById<ImageView>(Resource.Id.NavSet_PodStg);
            NavSet_PodStg.Click += NavSet_PodStg_Click;

            //PICTURE RESOLUTION 
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            spinner.SetSelection(AccountSetting.PodPicReson);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        /// <summary>
        /// SETTING 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavSet_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountSettingsCode));
        }

        /// <summary>
        /// HOME 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavSet_PodiHme_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }

        /// <summary>
        /// SYNRONISE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavSet_PodiSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }


        /// <summary>
        /// BACK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavAbotUsBaK_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }


        /// <summary>
        /// CUSTOMER LINE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CusCollapse_Click(object sender, EventArgs e)
        {
            CusCollapse = FindViewById<ToggleButton>(Resource.Id.CusCollapse);
            AccountSetting.podcollapse = CusCollapse.Checked;
            AccountSetting.SaveSetting();           
        }

        /// <summary>
        /// MANUAL SHORT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicManulSrt_Click(object sender, EventArgs e)
        {
            {
                PicManulSrt = FindViewById<ToggleButton>(Resource.Id.PicManulSrt);
                AccountSetting.podmodelsort = PicManulSrt.Checked;
                AccountSetting.SaveSetting();
            }
        }
        /// <summary>
        /// PICTURE RESOLUTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                AccountSetting.PodPicReson = e.Position;
                Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
                if (spinner.Count > 0)
                {
                    string toast = string.Format("The Picture Resolution Size is {0}", spinner.GetItemAtPosition(e.Position));
                    AccountSetting.SaveSetting();
                }
                else
                {
                    Toast.MakeText(this, "Picture Size is not Selected", ToastLength.Short).Show();
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "spinner_ItemSelected Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        /// <summary>
        /// METHOD OF NAV SETTING 
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
      




        /// <summary>
        /// NAVIGATION MENU
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
                Toast.MakeText(this, "NavigationView Exception Error", ToastLength.Short).Show();
                return;
            }
        }      
 
        /// <summary>
        /// NAVIGATION MENU
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
                Toast.MakeText(this, "OnCreateOptionsMenu Exception Error", ToastLength.Short).Show();
                return false;
            }

        }
    }
}