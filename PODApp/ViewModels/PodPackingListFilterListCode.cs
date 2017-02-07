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

using Android.Database;
using SQLite;

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Content.PM;
using PODApp.Models;
using PODApp.Controls;
using Cirrious.CrossCore.Plugins;

namespace PODApp.ViewModels
{
    [Activity(Label = "PACKING SLIP FILTER", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PodPackingListFilterListCode : AppCompatActivity
    {
        private SearchView PackingListFiltersv;
        private ListView PackingListFilterlv;
        //why did you change SpaceCrafts to SpaceCrafts
        JavaList<CustPackingSlipJour> spaceCraftsJavaList = new JavaList<CustPackingSlipJour>();
        //Do we have to specific SpaceCraft List bottom below
        List<CustPackingSlipJour> spaceCrafts = new List<CustPackingSlipJour>();
        ArrayAdapter adapter;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        //STEP - 50
        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // PAGE LOAD LAYOUT ACTIVITY
            SetContentView(Resource.Layout.PodPackingListFilterList);

            //Initialize UI
            this.InitializeUI();



        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeUI()
        {

            //SAVE SETTING LOAD
            AccountSetting.LoadSetting();
            //LIST VIW CONTROL
            PackingListFilterlv = FindViewById<ListView>(Resource.Id.PackingListFilterlv);
            PackingListFilterlv.ItemClick += PackingListFilterlv_ItemClick;
            //FnClickEvents();
            PackingListFilterlv.LongClick += PackingListFilterlv_LongClick;
            PackingListFilterlv.FastScrollEnabled = true;
            PackingListFiltersv = FindViewById<SearchView>(Resource.Id.PackingListFiltersv);
            PackingListFiltersv.QueryTextChange += PackingListFiltersv_QueryTextChange;

            //RETRIVE AUTO LIST
            PackingListRetrive();

            this.adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCraftsJavaList);
            PackingListFilterlv.Adapter = new ListViewPackingSlip(this, spaceCrafts);

            //WHY THIS CONTROL SHOULD NOT USE HERE
            // PackingListFilterlv.SetAdapter(adapter);

            PackingListFilterlv.FastScrollEnabled = true;
            //PackingListFilterlv.SetMinimumHeight(300);
            PackingListFilterlv.SetBackgroundColor(Color.White);


            //RETRIVE 
            ImageView PackingListFilter = FindViewById<ImageView>(Resource.Id.PackingListFilter);
            PackingListFilter.Click += PackingListFilter_Click;

            //MAIN MENU
            ImageView PodPSFL_PodiHme = FindViewById<ImageView>(Resource.Id.PodPSFL_PodiHme);
            PodPSFL_PodiHme.Click += PodPSFL_PodiHme_Click;

            //SETTING 
            ImageView PodPSFL_PodStg = FindViewById<ImageView>(Resource.Id.PodPSFL_PodStg);
            PodPSFL_PodStg.Click += PodPSFL_PodStg_Click;

            //BACK
            ImageView Nav_ImgBke = FindViewById<ImageView>(Resource.Id.Nav_ImgBke);
            Nav_ImgBke.Click += Nav_ImgBke_Click;

            //DRAWERLAYOUT 
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);

        }

        private void PackingListFilterlv_LongClick(object sender, View.LongClickEventArgs e)
        {

        }

        /// <summary>
        /// IMAGE BACK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nav_ImgBke_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
            base.OnBackPressed();
        }

        /// <summary>
        /// HOME - NAVIGATION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodPSFL_PodiHme_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }

        /// <summary>
        /// SETTINGS NAVIGATION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodPSFL_PodStg_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, " I am in ActSettingCode", ToastLength.Short).Show();
            var ActSettCde = new Intent(this, typeof(ActSettingCode));
            ActSettCde.PutExtra("Parent", "PodPackingListFilterListCode");
            StartActivity(ActSettCde);

        }

        /// <summary>
        /// AUTO RETRIEVE LIST VIEW
        /// </summary>
        private void PackingListRetrive()
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            PopulateGrid(result);
        }
        /// <summary>
        /// RETRIVE  IMAGE CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackingListFilter_Click(object sender, EventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            PopulateGrid(result);
        }

        //void FnClickEvents()
        //{
        //    //Listening to child item selection
        //    PackingListFilterlv.ChildClick += delegate (object sender, ListViewPackingSlip.ChildClickEventArgs e)
        //    {
        //        PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
        //        var value = e.GroupPosition;
        //        List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
        //        CustPackingSlipJour1.PackingSlipId = result[value].PackingSlipId;
        //        StartActivity(typeof(PodDBPackingSlipNoDetailsCode));
        //    };
        //}

        /// <summary>
        /// LISTVIEW CLICK CONTROL - PackingListFilterlv.ItemClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackingListFilterlv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            //string[] selectedFromList = PackingListFilterlv.GetItemAtPosition(e.Position).ToString;
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            CustPackingSlipJour1.PackingSlipId = result[e.Position].PackingSlipId;
            StartActivity(typeof(PodDBPackingSlipNoDetailsCode));

        }



        /// <summary>
        /// SEARCH VIEW CONTROL - PackingListFiltersv.QueryTextChange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackingListFiltersv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchTerm = e.NewText;
            this.PopulateGrid(searchTerm);
        }


        /// <summary>
        /// PopulateGrid - (string searchTerm)
        /// </summary>
        /// <param name="searchTerm"></param>
        private void PopulateGrid(string searchTerm)
        {
            spaceCrafts.Clear();
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour(searchTerm);

            foreach (CustPackingSlipJour location in result)
                spaceCrafts.Add(location);
            PackingListFilterlv.Adapter = new ListViewPackingSlip(this, spaceCrafts);
        }


        /// <summary>
        /// PopulateGrid
        /// </summary>
        /// <param name="locations"></param>
        private void PopulateGrid(List<CustPackingSlipJour> locations)
        {
            spaceCrafts.Clear(); //if I want i can keep it
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            spaceCrafts.Clear();
            if (locations != null)
            {
                foreach (CustPackingSlipJour location in locations)
                    spaceCrafts.Add(location);
            }
            PackingListFilterlv.Adapter = new ListViewPackingSlip(this, spaceCrafts);
        }



        /// <summary>
        /// NAVIGAION CONTROL CODE
        /// </summary>
        /// <param name="navigationView"></param>
        void setupDrawerContent(NavigationView navigationView)
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
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            navigationView.InflateMenu(Resource.Menu.nav_menu);
            return true;
        }
        private void MethodSettingsOnOff()
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
    }
    public class ListViewPackingSlip : BaseAdapter
    {
        public static List<CustPackingSlipJour> Items { get; set; }


        public int mMobileCellPosition { get; set; }

        Activity context;

        public ListViewPackingSlip(Activity context, List<CustPackingSlipJour> items) : base()
        {
            Items = items;
            this.context = context;
            mMobileCellPosition = int.MinValue;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = Items[position];

            View view = convertView;            
            view = context.LayoutInflater.Inflate(Resource.Layout.PackingSlipListViewPlain, null);
            var text1 = view.FindViewById<TextView>(Resource.Id.LstVWPKLVPod_PakID);
            var text2 = view.FindViewById<TextView>(Resource.Id.LstVWPKLVPod_PostCode);
            //ImageView Image = view.FindViewById<ImageView>(Resource.Id.PKLVPod_ImDrV);
            string[] str = Items[position].ToString().Split(' ');
            if (text1 != null)
            {
               text1.Text = Items[position].PackingSlipId;                
            }
            if (text2 != null)
            {
              text2.Text = Items[position].DeliveryPostCode;             
            }
           
            //Dragable Code
            view.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
            view.TranslationY = 0;

            return view;
        }

        public override int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public void SwapItems(int indexOne, int indexTwo)
        {
            var oldValue = Items[indexOne];
            Items[indexOne] = Items[indexTwo];
            Items[indexTwo] = oldValue;
            mMobileCellPosition = indexTwo;
            NotifyDataSetChanged();
            AccountSetting.podsortdraggableList = Items;
        }

        public bool CanMove(int index)
        {
            return true;
        }

    }
}