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
using System.Collections.Generic;
using Android.Graphics;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Icon = "@drawable/ArrowBack", Label = "PodDraggableListView", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PodDraggableListView : AppCompatActivity
    {
        ArrayAdapter adapter;
        // public static List<string> items { get; set; }
        private DraggableListView PodDrggLitViw;
        JavaList<CustPackingSlipJour> spaceCrafts = new JavaList<CustPackingSlipJour>();

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        private SearchView PodPdl_Search;
        public ListView list = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // SetContentView(Resource.Layout.PodDraggableListView);
            SetContentView(Resource.Layout.PodDraggableListView);
            AccountSetting.LoadSetting();
            PodDrggLitViw = FindViewById<DraggableListView>(Resource.Id.PodDrggLitViw);

            //list.Adapter =;
            var list1 = FindViewById<DraggableListView>(Resource.Id.PodDrggLitViw);
            list = FindViewById<ListView>(Resource.Id.PodDrggLitViw);

            list.ItemClick += List_ItemClick;

            if (AccountSetting.podsortdraggableList == null)
                AccountSetting.podsortdraggableList = PackingListRetrive();

            list.Adapter = new DraggableListAdapter(this, AccountSetting.podsortdraggableList);

            // Adapter ?????? if i common line below i could drag and drop 
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            //PodDrggLitViw.Adapter = adapter;

            //Search View
            PodPdl_Search = FindViewById<SearchView>(Resource.Id.PodPdl_Search);
            PodPdl_Search.QueryTextChange += PodPdl_Search_QueryTextChange;

            //refresh image
            ImageView PodPdL_PodRefres = FindViewById<ImageView>(Resource.Id.PodPdL_PodRefres);
            //PodPdL_PodRefres.Click += PodPdL_PodRefres_Click;

            //Back Button 
            ImageView Nav_DLVPdlB2B = FindViewById<ImageView>(Resource.Id.Nav_DLVPdlB2B);
            Nav_DLVPdlB2B.Click += Nav_DLVPdlB2B_Click;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }

        private void Nav_DLVPdlB2B_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }

        //private void PodPdL_PodRefres_Click(object sender, EventArgs e)
        //{
        //    PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
        //    List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
        //    PopulateGrid(result);            
        //}
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        private void PopulateGridDragDrop(string searchTerm)
        {
                adapter.Clear();
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                List<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour(searchTerm);

                foreach (CustPackingSlipJour location in result)
                    spaceCrafts.Add(location);
                PodDrggLitViw.Adapter = adapter;
        }

        /// <summary>
        /// PopulateGrid
        /// </summary>
        /// <param name="locations"></param>
        private void PopulateGridDragDrop(List<CustPackingSlipJour> locations)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            spaceCrafts.Clear();
            if (locations != null)
            {
                foreach (CustPackingSlipJour location in locations)
                    spaceCrafts.Add
                        (location);
            }
                PodDrggLitViw.Adapter = adapter;           
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PodRefresh()
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            PopulateGrid(result);

        }

        private void PodPdl_Search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchTerm = e.NewText;
            this.PopulateGridDrag(searchTerm);       
        }

        private void PopulateGridDrag(string searchTerm)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour(searchTerm);
            AccountSetting.podsortdraggableList = PopulateGrid(result);
            list.Adapter = new DraggableListAdapter(this, AccountSetting.podsortdraggableList);          
        }

        private void PopulateGridDrag(List<CustPackingSlipJour> locations)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            spaceCrafts.Clear();
            if (locations != null)
            {
                foreach (CustPackingSlipJour location in locations)
                    spaceCrafts.Add
                        (location);
            }
            PodDrggLitViw.Adapter = adapter;
            
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            CustPackingSlipJour1.PackingSlipId = result[e.Position].PackingSlipId;
            StartActivity(typeof(PodDBPackingSlipNoDetailsCode));           
        }

        /// <summary>
        /// NAVIGAION CONTROL CODE
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
                Toast.MakeText(this, "NavigationItemSelected Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            navigationView.InflateMenu(Resource.Menu.nav_menu);
            return true;
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

        private List<CustPackingSlipJour> PackingListRetrive()
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodRetriveCustPackingSlipJour();
            return PopulateGrid(result);
        }
        /// <summary>
        /// PopulateGrid
        /// </summary>
        /// <param name="searchTerm"></param>
        private void PopulateGrid(string searchTerm)
        {
            try
            {
                adapter.Clear();
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                List<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour(searchTerm);

                foreach (CustPackingSlipJour location in result)
                    spaceCrafts.Add(location);
                PodDrggLitViw.Adapter = adapter;

            }
            catch (Exception)
            {
                Toast.MakeText(this, "PopulateGrid Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        private List<CustPackingSlipJour> PopulateGrid(List<CustPackingSlipJour> locations)
        {
            try
            {
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                spaceCrafts.Clear();
                List<CustPackingSlipJour> lst = new List<CustPackingSlipJour>();
                if (locations != null)
                {
                    foreach (CustPackingSlipJour location in locations)
                        lst.Add
                            (location);
                    //spaceCrafts.OrderByDescending(x => x.PackingSlipId).ToArray();
                }
                return lst;
                // spaceCrafts = locations.ToList();
                //PodDrggLitViw.Adapter = adapter;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "PopulateGrid Error Handling", ToastLength.Short).Show();
                return null;
            }
        }

        //private List<string> PopulateGrid(List<CustPackingSlipJour> locations)
        //{
        //    try
        //    {
        //        PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
        //        spaceCrafts.Clear();
        //        if (locations != null)
        //        {
        //            foreach (CustPackingSlipJour location in locations)
        //                spaceCrafts.Add
        //                    (location);
        //            //spaceCrafts.OrderByDescending(x => x.PackingSlipId).ToArray();
        //        }
        //        // spaceCrafts = locations.ToList();
        //        PodDrggLitViw.Adapter = adapter;
        //    }
        //    catch (Exception)
        //    {
        //        Toast.MakeText(this, "PopulateGrid Error Handling", ToastLength.Short).Show();
        //        return null;
        //    }
        //}
    }



    /// <summary>
    /// DraggableListAdapter /////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public class DraggableListAdapter : BaseAdapter, IDraggableListAdapter
    {
        public static List<CustPackingSlipJour> Items { get; set; }


        public int mMobileCellPosition { get; set; }

        Activity context;

        public DraggableListAdapter(Activity context, List<CustPackingSlipJour> items) : base()
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
            //View cell = convertView;
            //if (cell == null)
            //{
            //    cell = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
            //    cell.SetMinimumHeight(150);
            //    cell.SetBackgroundColor(Color.White);
            //    //cell.SetBackgroundColor(Color.DarkViolet);
            //}

            //var text = cell.FindViewById<TextView>(Android.Resource.Id.Text1);
            //if (text != null)
            //{
            //    text.Text = Items[position].ToString();
            //}

            //cell.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
            //cell.TranslationY = 0;

            //return cell;


            var item = Items[position];

            View view = convertView;
            // if (view == null) // no view to re-use, create new
            view = context.LayoutInflater.Inflate(Resource.Layout.PackSlipListView, null);
            var text1 = view.FindViewById<TextView>(Resource.Id.PKLVPod_PakID);
            var text2 = view.FindViewById<TextView>(Resource.Id.PKLVPod_PostCode);
            //ImageView Image = view.FindViewById<ImageView>(Resource.Id.PKLVPod_ImDrV);
            string[] str = Items[position].ToString().Split(' ');
            if (text1!=null)
            {
                text1.Text = item.PackingSlipId;
                    //str[0];
            }
            if(text2!=null)
            {
                text2.Text = item.DeliveryPostCode;
                    //str[2];
            }
            // Do i need to Display Image as delplay View Design 
            //view.FindViewById<ImageView>(Resource.Id.PKLVPod_ImDrV);

            //view.FindViewById<TextView>(Resource.Id.text2).LayoutParameters.Width = 100;
            //view.FindViewById<TextView>(Resource.Id.text2).Text = "kajan";
            //view.FindViewById<TextView>(Resource.Id.text2).LayoutParameters.Width = 100;
            //view.FindViewById<TextView>(Resource.Id.text2).Text = "kajan";

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


    
        //private void MethodPODNavCollapse()
        //{
        //    if (AccountSetting.podmodelsort)
        //    {
        //       AccountSetting.podcollapse = true;
        //        AccountSetting.podsortdraggableList = Items;
        //    }
        //    else
        //    {
        //       AccountSetting.podcollapse = false;
        //    }
        //}
    }



    /////////////////////////////////////////////////////////// newcontrol
    //public class ReorderListViewAdapter : BaseAdapter, IReorderListAdapter
    //{
    //    private readonly List _items;
    //    private readonly Activity _context;

    //    public ReorderListViewAdapter(Activity context, List items)
    //    {
    //        _context = context;
    //        _items = items;
    //    }

    //    public List GetReordedData()
    //    {
    //        return _items;
    //    }

    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        var item = _items[position];
    //        View view = convertView;
    //        if (view == null) // no view to re-use, create new
    //            view = _context.LayoutInflater.Inflate(Resource.Layout.CustomItemView, null);
    //        view.FindViewById(Resource.Id.Text1).Text = item.Title;
    //        view.FindViewById(Resource.Id.Text2).Text = item.SubTitle;
    //        return view;
    //    }

    //    public override int Count
    //    {
    //        get { return _items.Count; }
    //    }

    //    public override ListDataItem this[int position]
    //    {
    //        get { return _items[position]; }
    //    }

    //    //The Adapter must support stable id's. 
    //    //The stable (unique) id's are implemented in the ViewModel (property Id)
    //    public override long GetItemId(int position)
    //    {
    //        if (position < 0 || position >= _items.Count)
    //        {
    //            return -1;
    //        }
    //        var item = GetItem(position).Cast();
    //        return item.Id;
    //    }

    //    public override bool HasStableIds
    //    {
    //        get { return true; }
    //    }

    //    #region --[IReorderListAdapter]--

    //    public void Swap(int index1, int index2)
    //    {
    //        var temp = _items[index1];
    //        _items[index1] = _items[index2];
    //        _items[index2] = temp;
    //        NotifyDataSetChanged();
    //    }

    //    public bool CanMove(int index)
    //    {
    //        return true;
    //    }

    //    #endregion
    //}

    //public static class ObjectTypeHelper
    //{
    //    public static T Cast(this Java.Lang.Object obj) where T : class
    //    {
    //        var propertyInfo = obj.GetType().GetProperty("Instance");
    //        return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
    //    }
    //}
    //////////////////////////////////////////////////////////////////////////

}