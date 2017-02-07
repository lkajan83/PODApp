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

using SQLite;
using Android.Media;
using PODApp.Controls;


using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    //[Activity(Label = "PACKING SLIP LINE DETAILS", Icon = "@drawable/ArrowBack")]
    //public class PodDBCustPackingSlipJourLineCode : Activity
    [Activity(Icon = "@drawable/ArrowBack", Label = "PodDBCustPackingSlipJourLineCode", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
   public class PodDBCustPackingSlipJourLineCode : AppCompatActivity
    {
        private ListView PodL_ListView;
        JavaList<CustPackingSlipJourLine> spaceCrafts = new JavaList<CustPackingSlipJourLine>();
        ArrayAdapter adapter;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PodDBCustPackingSlipJourLine);
            AccountSetting.LoadSetting();

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            AccountSetting.LoadSetting();

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);

            //Initialize UI
            this.InitializeUI();

            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            PodL_ListView.Adapter = adapter;

            EditText PodL_CPJLineId = FindViewById<EditText>(Resource.Id.PodL_CPJLineId);
            EditText PodL_PackingSlipId = FindViewById<EditText>(Resource.Id.PodL_PackingSlipId);
            EditText PodL_Item = FindViewById<EditText>(Resource.Id.PodL_Item);
            EditText PodL_Description = FindViewById<EditText>(Resource.Id.PodL_Description);
            EditText PodL_Unit = FindViewById<EditText>(Resource.Id.PodL_Unit);
            EditText PodL_Qty = FindViewById<EditText>(Resource.Id.PodL_Qty);

            ////CREATE DATABASE
            //ImageView PodL_CreateDatabase = FindViewById<ImageView>(Resource.Id.PodL_CreateDatabase);
            //PodL_CreateDatabase.Click += PodL_CreateDatabase_Click;


            //CREATE TABLE
            ImageView PodL_CreateTable = FindViewById<ImageView>(Resource.Id.PodL_CreateTable);
            PodL_CreateTable.Click += PodL_CreateTable_Click;


            //INSERT RECORDS
            ImageView PodL_InsertRecord = FindViewById<ImageView>(Resource.Id.PodL_InsertRecord);
            PodL_InsertRecord.Click += PodL_InsertRecord_Click;


            //RETRIVE RECORDS
            ImageView PodL_RetrieveRecord = FindViewById<ImageView>(Resource.Id.PodL_RetrieveRecord);
            PodL_RetrieveRecord.Click += PodL_RetrieveRecord_Click;

            //Back Image
            ImageView NavPSJLine_ImgBke = FindViewById<ImageView>(Resource.Id.NavPSJLine_ImgBke);
            NavPSJLine_ImgBke.Click += NavPSJLine_ImgBke_Click;

            //Menu mack  PodL_RetrieveRecord
            //ImageView PodL_PsLB2BMen = FindViewById<ImageView>(Resource.Id.PodL_PsLB2BMen);
            //PodL_PsLB2BMen.Click += PodL_PsLB2BMen_Click;
            EditText PodL_Status = FindViewById<EditText>(Resource.Id.PodL_Status);
            EditText PodL_DateDisplay = FindViewById<EditText>(Resource.Id.PodL_DateDisplay);
            Button PodL_InsertDate = FindViewById<Button>(Resource.Id.PodL_InsertDate);
            PodL_InsertDate.Click += PodL_InsertDate_Click;
        }
        /// <summary>
        /// date and time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodL_InsertDate_Click(object sender, EventArgs e)
        {
            EditText PodL_DateDisplay = FindViewById<EditText>(Resource.Id.PodL_DateDisplay);
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                PodL_DateDisplay.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void NavPSJLine_ImgBke_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        //private void PodL_PsLB2BMen_Click(object sender, EventArgs e)
        //{
        //    base.OnBackPressed();
        //}

        /// <summary>
        /// Synronise 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CusPasLin_PodiSyn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }


        /// <summary>
        /// HOME NAVIGATION - BACK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CusPasLin_PodiHme_Click(object sender, EventArgs e)
        {            

            MethodSettingsOnOff();
        }

        /// <summary>
        /// NAVIGATION - SETTING
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CusPasLin_PodStg_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodSettingsCode));
        }

        /// <summary>
        /// CREATE DATABASE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void PodL_CreateDatabase_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        PodDb_CustPackingSlipJourLine dbr = new PodDb_CustPackingSlipJourLine();
        //        var result = dbr.DB_CustPackingSlipJourLine();
        //        Toast.MakeText(this, result, ToastLength.Short).Show();
        //    }
        //    catch(Exception)
        //    {
        //        Toast.MakeText(this, "PodL_CreateDatabase_Click Error Handling", ToastLength.Short).Show();
        //        return;
        //    }
        //}

        /// <summary>
        /// CREATE TABLE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodL_CreateTable_Click(object sender, EventArgs e)
        {
            try
            {
                PodDb_CustPackingSlipJourLine dbr = new PodDb_CustPackingSlipJourLine();
                var result = dbr.CT_CustPackingSlipJourLine();
                Toast.MakeText(this, result, ToastLength.Short).Show();
            }
            catch (Exception)
            {
                Toast.MakeText(this, "PodL_CreateTable_Click Error Handling", ToastLength.Short).Show();
                return;
            }
        }

        public bool checkCustPakSlpValn()
        {
            bool var = true;
            EditText PodL_PackingSlipId = FindViewById<EditText>(Resource.Id.PodL_PackingSlipId);
            EditText PodL_Status = FindViewById<EditText>(Resource.Id.PodL_Status);
            EditText PodL_DateDisplay = FindViewById<EditText>(Resource.Id.PodL_DateDisplay);
            EditText PodL_Item = FindViewById<EditText>(Resource.Id.PodL_Item);
            EditText PodL_Description = FindViewById<EditText>(Resource.Id.PodL_Description);
            EditText PodL_Unit = FindViewById<EditText>(Resource.Id.PodL_Unit);
            EditText PodL_Qty = FindViewById<EditText>(Resource.Id.PodL_Qty);

            if (PodL_PackingSlipId.Text == "" || PodL_PackingSlipId.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Packing Slip Id", ToastLength.Short).Show();
            }
                   
            else if (PodL_Status.Text == "" || PodL_Status.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Status", ToastLength.Short).Show();
            }
            
            else if (PodL_DateDisplay.Text == "" || PodL_DateDisplay.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Date Display", ToastLength.Short).Show();
            }
            else if (PodL_Item.Text == "" || PodL_Item.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Item", ToastLength.Short).Show();
            }
            else if (PodL_Description.Text == "" || PodL_Description.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Description", ToastLength.Short).Show();
            }
            else if (PodL_Unit.Text == "" || PodL_Unit.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Unit", ToastLength.Short).Show();
            }
            else if (PodL_Qty.Text == "" || PodL_Qty.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Qty", ToastLength.Short).Show();
            }
            else
            {
                var = true;
            }

            return var;
        }
        /// <summary>
        /// INSERT RECORDS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodL_InsertRecord_Click(object sender, EventArgs e)
        {
            if (checkCustPakSlpValn())
            {
                EditText PodL_CPJLineId = FindViewById<EditText>(Resource.Id.PodL_CPJLineId);
                EditText PodL_Status = FindViewById<EditText>(Resource.Id.PodL_Status);
                EditText PodL_DateDisplay = FindViewById<EditText>(Resource.Id.PodL_DateDisplay);
                EditText PodL_PackingSlipId = FindViewById<EditText>(Resource.Id.PodL_PackingSlipId);
                EditText PodL_Item = FindViewById<EditText>(Resource.Id.PodL_Item);
                EditText PodL_Description = FindViewById<EditText>(Resource.Id.PodL_Description);
                EditText PodL_Unit = FindViewById<EditText>(Resource.Id.PodL_Unit);
                EditText PodL_Qty = FindViewById<EditText>(Resource.Id.PodL_Qty);


                PodDb_CustPackingSlipJourLine dbr = new PodDb_CustPackingSlipJourLine();
                string result = dbr.IR_CustPackingSlipJourLine(PodL_PackingSlipId.Text,
                    PodL_Status.Text,
                    DateTime.Parse(PodL_DateDisplay.Text),
                    PodL_Item.Text, 
                    PodL_Description.Text, 
                    PodL_Unit.Text, 
                    PodL_Qty.Text);


                //clear the text after insert the record
                PodL_CPJLineId.Text = "";
                PodL_PackingSlipId.Text = "";
                PodL_Status.Text = "";
                PodL_DateDisplay.Text = "";
                PodL_Item.Text = "";
                PodL_Description.Text = "";
                PodL_Unit.Text = "";
                PodL_Qty.Text = "";


                Toast.MakeText(this, result, ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// RETRIEVE RECORDS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void PodL_RetrieveRecord_Click(object sender, EventArgs e)
        {
            PodDb_CustPackingSlipJourLine dbr = new PodDb_CustPackingSlipJourLine();
            TableQuery<CustPackingSlipJourLine> result = dbr.RR_CustPackingSlipJourLine();
            PopulateGrid(result);
        }

        //Retrive  Grid view 
        private void PopulateGrid(TableQuery<CustPackingSlipJourLine> locations)
        {
            foreach (CustPackingSlipJourLine location in locations)
                spaceCrafts.Add(location);
            PodL_ListView.Adapter = adapter;
        }

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
        private void InitializeUI()
        {
            PodL_ListView = FindViewById<ListView>(Resource.Id.PodL_ListView);
            AccountSetting.LoadSetting();
        }
    }
}