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
using Uri = Android.Net.Uri;
using SQLite;
using Android.Media;
using PODApp.Controls;

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Icon = "@drawable/ArrowBack", Label = "PodPackingSlipAttachmenCode", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PodPackingSlipAttachmenCode : AppCompatActivity
    {
        //private ListView PodL_ListView;
        JavaList<PackingSlipAttachment> spaceCrafts = new JavaList<PackingSlipAttachment>();
        ArrayAdapter adapter;


        DrawerLayout drawerLayout;
        NavigationView navigationView;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";

        public static readonly int PickImageId = 1000;
        ImageView Attnt_Attachments;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Uri uri = data.Data;
                Attnt_Attachments.SetImageURI(uri);
            }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.PodPackingSlipAttachment);
           
            //Initialize UI
            this.InitializeUI();

            
        }

        private void InitializeUI()
        {
            Attnt_Attachments_Database();
            Attnt_Attachments_Table();
            AccountSetting.LoadSetting();

            ImageView Attnt_Slp_ImgBke = FindViewById<ImageView>(Resource.Id.Attnt_Slp_ImgBke);
            EditText Attnt_PackingSlipId = FindViewById<EditText>(Resource.Id.Attnt_PackingSlipId);
            Attnt_Attachments = FindViewById<ImageView>(Resource.Id.Attnt_Attachments);

            Button Attnt_BtnAttachments = FindViewById<Button>(Resource.Id.Attnt_BtnAttachments);
            Attnt_BtnAttachments.Click += Attnt_BtnAttachments_Click;

            Switch Attnt_Signature = FindViewById<Switch>(Resource.Id.Attnt_Signature);
            Attnt_Signature.CheckedChange += Attnt_Signature_CheckedChange;


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


        //CREATE DATABASE 
        private void Attnt_Attachments_Database()
        {
            //DBA_PackingSlipAttachment dbr = new DBA_PackingSlipAttachment();
            //var result = dbr.DB_PackingSlipAttachment();
            //Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        //CREATE TABLE
        private void Attnt_Attachments_Table()
        {
            DBA_PackingSlipAttachment dbr = new DBA_PackingSlipAttachment();
            var result = dbr.CT_PackingSlipAttachment();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
        private void Attnt_BtnAttachments_Click(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        private void Attnt_Signature_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var toast = Toast.MakeText(this, "Your answer is " +
             (e.IsChecked ? "correct" : "incorrect"), ToastLength.Short);
            toast.Show();
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch
            {
                Toast.MakeText(this, " navigationView.InflateMenu Error Handling", ToastLength.Short).Show();
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