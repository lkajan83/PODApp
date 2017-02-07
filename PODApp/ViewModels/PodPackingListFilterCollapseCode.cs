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
using PODApp.Controls;

namespace PODApp.ViewModels
{
    [Activity(Label = "PACKING SLIP", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PodPackingListFilterCollapseCode : AppCompatActivity
    {
        private SearchView PkgFilSvCollapse;
        private ExpandableListView PkgFilLvCollapse;
        private ExpandableListView expListView;
        private ImageView PkgRefshCollapse;
        JavaList<CustPackingSlipJour> spaceCrafts = new JavaList<CustPackingSlipJour>();
        JavaList<CustPackingSlipJour> tableItems = new JavaList<CustPackingSlipJour>();
        ArrayAdapter adapter;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PodPackingListFilterCollapse);

            //Initialize UI
            this.InitializeUI();

            AccountSetting.LoadSetting();

            // EXPAND LISTVIEW ADAPTER
            this.adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            var adapter = new CollapseScreenAdapter(this, spaceCrafts);

            PkgFilLvCollapse.SetAdapter(adapter);

            PkgFilLvCollapse.ItemClick += PkgFilLvCollapse_ItemClick;
            FnClickEvents();

            PkgFilLvCollapse.FastScrollEnabled = true;

            PkgFilLvCollapse.SetMinimumHeight(300);
            PkgFilLvCollapse.SetBackgroundColor(Color.White);

            ImageView Nav_ImgBkeCollapse = FindViewById<ImageView>(Resource.Id.Nav_ImgBkeCollapse);
            Nav_ImgBkeCollapse.Click += Nav_ImgBkeCollapse_Click;

            //SEARCH VIEW 
            PkgFilSvCollapse = FindViewById<SearchView>(Resource.Id.PkgFilSvCollapse);
            PkgFilSvCollapse.QueryTextChange += PkgFilSvCollapse_QueryTextChange;

            PackingListrRveCollapse();

            //SETTING 
            ImageView PodPsColl_PodStg = FindViewById<ImageView>(Resource.Id.PodPsColl_PodStg);
            PodPsColl_PodStg.Click += PodPsColl_PodStg_Click;

            //HOME
            ImageView PodPsColl_PodiHme = FindViewById<ImageView>(Resource.Id.PodPsColl_PodiHme);
            PodPsColl_PodiHme.Click += PodPsColl_PodiHme_Click;

            //REFRESH
            PkgRefshCollapse = FindViewById<ImageView>(Resource.Id.PkgRefshCollapse);
            PkgRefshCollapse.Click += PkgRefshCollapse_Click;

            //ACTION BAR DRAWER TOGGLE ------------------------------------------------------------------------------
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            //ACTION BAR DRAWER TOGGLE ---------------------------------------------------------------------------       

        }
        

        private void PodPsColl_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ActSettingCode));
        }

        private void PodPsColl_PodiHme_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PODAppHomePageCode));
        }
        private void Nav_ImgBkeCollapse_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        /// <summary>
        ///Auto  REFRESH PACKINGSLIP STRING METHOD
        /// </summary>
        private void PackingListrRveCollapse()
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            PopulateGrid(result);
        }
        /// <summary>
        /// REFRESH IMAGE VIEW 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PkgRefshCollapse_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
            //PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            //List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            //PopulateGrid(result);
        }

        /// <summary>
        /// SEARCH VIEW - PopulateGrid(string searchTerm)
        /// </summary>
        /// <param name="searchTerm"></param>
        private void PopulateGrid(string searchTerm)
        {
            try
            {
                adapter.Clear();
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                List<CustPackingSlipJour> result = dbr.ExpandSrchCustPackingSlipJour(searchTerm);

                foreach (CustPackingSlipJour location in result)
                    spaceCrafts.Add(location);
                PkgFilLvCollapse.Adapter = adapter;
            }
            catch (Exception)
            {
                //Toast.MakeText(this, "PopulateGrid Exception Error", ToastLength.Short).Show();
                return;
            }

        }

        /// <summary>
        /// PopulateGrid(List<CustPackingSlipJour> locations)
        /// </summary>
        /// <param name="locations"></param>
        /// i neeed to check it
        private void PopulateGrid(List<CustPackingSlipJour> locations)
        {
            try
            {
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                spaceCrafts.Clear();
                if (locations != null)
                {
                    foreach (CustPackingSlipJour location in locations)
                        spaceCrafts.Add(location);
                }
                PkgFilLvCollapse.Adapter = adapter;
            }
            catch (Exception)
            {
                //Toast.MakeText(this, "PopulateGrid Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        /// <summary>
        /// SEARCH VIEW 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PkgFilSvCollapse_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchTerm = e.NewText;
            this.PopulateGrid(searchTerm);
        }

        /// <summary>
        ///  ListView Adapter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void FnClickEvents()
        {
            //Listening to child item selection
            PkgFilLvCollapse.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e)
            {
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                var value = e.GroupPosition;
                List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
                CustPackingSlipJour1.PackingSlipId = result[value].PackingSlipId;
                StartActivity(typeof(PodDBPackingSlipNoDetailsCode));
            };
        }


        private void PkgFilLvCollapse_ItemClick(object sender, ExpandableListView.ItemClickEventArgs e)
        {
            string selectedFromList = PkgFilLvCollapse.GetItemAtPosition(e.Position).ToString();
            CustPackingSlipJour1.PackingSlipId = selectedFromList;
            StartActivity(typeof(PodDBPackingSlipNoDetailsCode));

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


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            navigationView.InflateMenu(Resource.Menu.nav_menu);
            return true;
        }
        /// <summary>
        /// InitializeUI
        /// </summary>
        private void InitializeUI()
        {
            PkgFilLvCollapse = FindViewById<ExpandableListView>(Resource.Id.PkgFilLvCollapse);   
        }
        
    }
    /// <summary>
    /// I need to try Expandable Draggable ListView 
    /// </summary>
   

    public class CollapseScreenAdapter : BaseExpandableListAdapter
    {
        // Context, usually set to the activity:
        private readonly Context _context;

        // List of produce objects ("vegetables", "fruits", "herbs"):
        private readonly JavaList<CustPackingSlipJour> _produce;

        public CollapseScreenAdapter(Context context, JavaList<CustPackingSlipJour> produce)
        {
            _context = context;
            _produce = produce;
        }

        public override bool HasStableIds
        {
            // Indexes are used for IDs:
            get { return true; }
        }

        //---------------------------------------------------------------------------------------
        // Group methods:

        public override long GetGroupId(int groupPosition)
        {
            // The index of the group is used as its ID:
            return groupPosition;
        }

        public override int GroupCount
        {
            // Return the number of produce ("vegetables", "fruits", "herbs") objects:
            get { return _produce.Count; }
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            //// Recycle a previous view if provided:
            //var view = convertView;
            //// If no recycled view, inflate a new view as a simple expandable list item 1:
            //if (view == null)
            //{
            //    var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            //    view = inflater.Inflate(Android.Resource.Layout.SimpleExpandableListItem1, null);
            //}
            //// Grab the produce object ("vegetables", "fruits", etc.) at the group position:
            //var produce = _produce[groupPosition];
            //// Get the built-in first text view and insert the group name ("Vegetables", "Fruits", etc.):
            //TextView textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            //textView.Text = produce.ToString();
            //return view;

            var item = _produce[groupPosition];

            View view = convertView;
            // if (view == null) // no view to re-use, create new
            var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            view = inflater.Inflate(Resource.Layout.PackSlipListViewCollapse, null);
            // view = inflater.Inflate(Android.Resource.Layout.PackSlipListView, null);
            var text1 = view.FindViewById<TextView>(Resource.Id.CollPKLVPod_PakID);
            var text2 = view.FindViewById<TextView>(Resource.Id.CollPKLVPod_PostCode);
            //ImageView Image = view.FindViewById<ImageView>(Resource.Id.PKLVPod_ImDrV);

            if (text1 != null)
            {
                text1.Text = _produce[groupPosition].PackingSlipId;
            }
            if (text2 != null)
            {
                text2.Text = _produce[groupPosition].DeliveryPostCode;
            }

            //view.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
            //view.TranslationY = 0;
            return view;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        //---------------------------------------------------------------------------------------
        // Child methods:

        public override long GetChildId(int groupPosition, int childPosition)
        {
            // The index of the child is used as its ID:
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            // Return the number of children (produce item objects) in the group (produce object):
            var produce = _produce[groupPosition];
            return 1;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            // Recycle a previous view if provided:
            var view = convertView;

            // If no recycled view, inflate a new view as a simple expandable list item 2:
            if (view == null)
            {
                var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Android.Resource.Layout.SimpleExpandableListItem2, null);
            }

            // Grab the produce object ("vegetables", "fruits", etc.) at the group position:
            var produce = _produce[groupPosition];

            // Extract the produce item object ("bananas", "apricots", etc.) at the child position:
            // var produceItem = produce.ProduceItems[childPosition];

            // Get the built-in first text view and insert the child name ("Bananas", "Apricots", etc.):
            TextView textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = "Name: " + produce.DeliveryName.ToString();

            //// Reuse the textView to insert the number of produce units into the child's second text field:
            //textView = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            //textView.Text = produceItem.Count.ToString() + " units";

            return view;
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }

}



//public class ExpandDraggableListAdapter : BaseAdapter, IDraggableListAdapter
//{
//    public static List<string> Items { get; set; }


//    public int mMobileCellPosition { get; set; }

//    Activity context;

//    public ExpandDraggableListAdapter(Activity context, List<string> items) : base()
//    {
//        Items = items;
//        this.context = context;
//        mMobileCellPosition = int.MinValue;
//    }

//    public override Java.Lang.Object GetItem(int position)
//    {
//        return Items[position];
//    }

//    public override long GetItemId(int position)
//    {
//        return position;
//    }

//    public override View GetView(int position, View convertView, ViewGroup parent)
//    {
//        View cell = convertView;
//        if (cell == null)
//        {
//            cell = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
//            cell.SetMinimumHeight(150);
//            cell.SetBackgroundColor(Color.White);
//            //cell.SetBackgroundColor(Color.DarkViolet);
//        }

//        var text = cell.FindViewById<TextView>(Android.Resource.Id.Text1);
//        if (text != null)
//        {
//            text.Text = Items[position].ToString();
//        }

//        cell.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
//        cell.TranslationY = 0;

//        return cell;
//    }

//    public override int Count
//    {
//        get
//        {
//            return Items.Count;
//        }
//    }

//    public void SwapItems(int indexOne, int indexTwo)
//    {
//        var oldValue = Items[indexOne];
//        Items[indexOne] = Items[indexTwo];
//        Items[indexTwo] = oldValue;
//        mMobileCellPosition = indexTwo;
//        NotifyDataSetChanged();
//        AccountSetting.podsortdraggableList = Items;

//    }

//}