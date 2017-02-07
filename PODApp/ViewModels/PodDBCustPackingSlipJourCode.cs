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
using Android.Util;
using Android.Content.PM;

namespace PODApp.ViewModels
{
   [Activity(Icon = "@drawable/ArrowBack", Label = "PACKING SLIP DETAILS", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
   public class PodDBCustPackingSlipJourCode : AppCompatActivity
    {
         DrawerLayout drawerLayout;
         NavigationView navigationView;

         JavaList<CustPackingSlipJour> spaceCrafts = new JavaList<CustPackingSlipJour>();
 
        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PodDBCustPackingSlipJour);

 
            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            //CREATE TABLE
            ImageView CustPSlipJourCT = FindViewById<ImageView>(Resource.Id.CustPSlipJourCT);
            CustPSlipJourCT.Click += CustPSlipJourCT_Click;

            //INSERT RECORD
            ImageView CustPSlipJourIR = FindViewById<ImageView>(Resource.Id.CustPSlipJourIR);
            CustPSlipJourIR.Click += CustPSlipJourIR_Click;

            ImageView CustPSBack2mMn = FindViewById<ImageView>(Resource.Id.CustPSBack2mMn);
            CustPSBack2mMn.Click += CustPSBack2mMn_Click;

            //Navigator Back Image
            ImageView NavPSJ_ImgBke = FindViewById<ImageView>(Resource.Id.NavPSJ_ImgBke);
            NavPSJ_ImgBke.Click += NavPSJ_ImgBke_Click;

            EditText PkSip_DateDisplay = FindViewById<EditText>(Resource.Id.PkSip_DateDisplay);
            Button PckSlp_InsertDate = FindViewById<Button>(Resource.Id.PckSlp_InsertDate);
            PckSlp_InsertDate.Click += PckSlp_InsertDate_Click;

            //EditText txtCPSJDeviceId = FindViewById<EditText>(Resource.Id.txtCPSJDeviceId);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

        }
        /// <summary>
        /// Date and Time controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PckSlp_InsertDate_Click(object sender, EventArgs e)
        {     
            EditText PkSip_DateDisplay = FindViewById<EditText>(Resource.Id.PkSip_DateDisplay);
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                PkSip_DateDisplay.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }



        /// <summary>
        /// BACK IMAGEVIEW CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavPSJ_ImgBke_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        /// <summary>
        /// BACK IMAGEVIEW CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSBack2mMn_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        /// <summary>
        /// CREATE DATABASE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void CustPSlipJourDB_Click(object sender, EventArgs e)
        //{
        //    PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
        //    var result = dbr.CreatePodDb_CustPackingSlipJour();
        //    Toast.MakeText(this, result, ToastLength.Short).Show();
        //}

        /// <summary>
        /// CREATE TABLE  IMAGEVIEW CONTROL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSlipJourCT_Click(object sender, EventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            var result = dbr.TableCreatePodDb_CustPackingSlipJour();
            Toast.MakeText(this, result, ToastLength.Short).Show();

            DBA_PackingSlipAttachment dbr1 = new DBA_PackingSlipAttachment();
            var result1 = dbr1.CT_PackingSlipAttachment();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }



        /// <summary>
        /// VALIDATION CHECK 
        /// </summary>
        /// <returns></returns>
        public bool checkvalidtion()
        {
            bool var = true;

            EditText txtCPSJPackingSlipId = FindViewById<EditText>(Resource.Id.txtCPSJPackingSlipId);
            EditText txtCPSJDeviceId = FindViewById<EditText>(Resource.Id.txtCPSJDeviceId);
            EditText txtCPSJStatus = FindViewById<EditText>(Resource.Id.txtCPSJStatus);
            EditText PkSip_DateDisplay = FindViewById<EditText>(Resource.Id.PkSip_DateDisplay);
            EditText txtCPSJDeliveryName = FindViewById<EditText>(Resource.Id.txtCPSJDeliveryName);
            EditText txtCPSJDelDesc = FindViewById<EditText>(Resource.Id.txtCPSJDelDesc);
            EditText txtCPSJDelAdd = FindViewById<EditText>(Resource.Id.txtCPSJDelAdd);
            EditText txtCPSJDelPCode = FindViewById<EditText>(Resource.Id.txtCPSJDelPCode);
            EditText txtCPSJVolume = FindViewById<EditText>(Resource.Id.txtCPSJVolume);
            EditText txtCPSJWeight = FindViewById<EditText>(Resource.Id.txtCPSJWeight);
            EditText txtCPSJNoUnit = FindViewById<EditText>(Resource.Id.txtCPSJNoUnit);
            
            if (txtCPSJPackingSlipId.Text == "" || txtCPSJPackingSlipId.Text == null )
            {
                var = false;
                Toast.MakeText(this, "Please Enter Packing Slip Id", ToastLength.Short).Show();
            }

            if (txtCPSJDeviceId.Text == "" || txtCPSJDeviceId.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Device ID", ToastLength.Short).Show();
            }
            if (txtCPSJStatus.Text == "" || txtCPSJStatus.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Status", ToastLength.Short).Show();
            }

            if (PkSip_DateDisplay.Text == "" || PkSip_DateDisplay.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Date And Time", ToastLength.Short).Show();
            }

            else if (txtCPSJDeliveryName.Text == "" || txtCPSJDeliveryName.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Delivery Name", ToastLength.Short).Show();
            }
            else if (txtCPSJDelDesc.Text == "" || txtCPSJDelDesc.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Description Name", ToastLength.Short).Show();
            }
            else if (txtCPSJDelAdd.Text == "" || txtCPSJDelAdd.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Delivery Address", ToastLength.Short).Show();
            }
            else if (txtCPSJDelPCode.Text == "" || txtCPSJDelPCode.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Delivery Code", ToastLength.Short).Show();
            }
            else if (txtCPSJVolume.Text == "" || txtCPSJVolume.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Volume", ToastLength.Short).Show();
            }
            else if (txtCPSJWeight.Text == "" || txtCPSJWeight.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Weight", ToastLength.Short).Show();
            }
            else if (txtCPSJNoUnit.Text == "" || txtCPSJNoUnit.Text == null)
            {
                var = false;
                Toast.MakeText(this, "Please Enter Unit", ToastLength.Short).Show();
            }
            else
            {
                var = true;
            }

            return var;
        }



        private void CustPSlipJourIR_Click(object sender, EventArgs e)
        {
            //validation
            if (checkvalidtion())
            {              
                EditText txtCPSJPackingSlipId = FindViewById<EditText>(Resource.Id.txtCPSJPackingSlipId);
                EditText txtCPSJDeviceId = FindViewById<EditText>(Resource.Id.txtCPSJDeviceId);
                EditText  txtCPSJStatus = FindViewById<EditText>(Resource.Id.txtCPSJStatus);
                EditText PkSip_DateDisplay = FindViewById<EditText>(Resource.Id.PkSip_DateDisplay);
                EditText txtCPSJDeliveryName = FindViewById<EditText>(Resource.Id.txtCPSJDeliveryName);
                EditText txtCPSJDelDesc = FindViewById<EditText>(Resource.Id.txtCPSJDelDesc);
                EditText txtCPSJDelAdd = FindViewById<EditText>(Resource.Id.txtCPSJDelAdd);
                EditText txtCPSJDelPCode = FindViewById<EditText>(Resource.Id.txtCPSJDelPCode);
                EditText txtCPSJVolume = FindViewById<EditText>(Resource.Id.txtCPSJVolume);
                EditText txtCPSJWeight = FindViewById<EditText>(Resource.Id.txtCPSJWeight);
                EditText txtCPSJNoUnit = FindViewById<EditText>(Resource.Id.txtCPSJNoUnit);
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                DBA_PackingSlipAttachment dbr1 = new DBA_PackingSlipAttachment();
                string result = dbr.InsertCustPackingSlipJour(txtCPSJPackingSlipId.Text,
                     int.Parse(txtCPSJDeviceId.Text),
                     txtCPSJStatus.Text,                    
                     DateTime.Parse(PkSip_DateDisplay.Text),
                     txtCPSJDeliveryName.Text,
                     txtCPSJDelDesc.Text,
                     txtCPSJDelAdd.Text,
                     txtCPSJDelPCode.Text,
                     txtCPSJVolume.Text,
                     txtCPSJWeight.Text,
                     txtCPSJNoUnit.Text);
                string result1 = dbr1.IR_PackingSlipAttachment(txtCPSJPackingSlipId.Text,false,false);

                //clear the text
                txtCPSJPackingSlipId.Text = "";
                txtCPSJStatus.Text = "";
                PkSip_DateDisplay.Text = "";
                txtCPSJDeliveryName.Text = "";
                txtCPSJDelDesc.Text = "";
                txtCPSJDelAdd.Text = "";
                txtCPSJDelPCode.Text = "";
                txtCPSJVolume.Text = "";
                txtCPSJWeight.Text = "";
                txtCPSJNoUnit.Text = "";

                Toast.MakeText(this, result, ToastLength.Short).Show();
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
            catch (Exception)
            {
                Toast.MakeText(this, "", ToastLength.Short).Show();
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
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch
            {
                Toast.MakeText(this, "On CreateOptionMenu Error Handling", ToastLength.Short).Show();
                return false;
            }

        }
    
    }

    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string that you desire.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
        Action<DateTime> _dateSelectedHandler = delegate { };

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month,
                                                           currently.Day);
            return dialog;
        }
    }
}