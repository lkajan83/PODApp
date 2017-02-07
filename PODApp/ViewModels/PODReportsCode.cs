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
using PODApp.Controls;
using PODApp.Models;
using PODApp.Bootstrap;

using Android.Database;
using SQLite;

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Content.PM;

namespace PODApp.ViewModels
{
   
    [Activity(Label = "Report", Icon = "@drawable/ArrowBack", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PODReportsCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PODReports);
            AccountSetting.LoadSetting();

            Button PodReportextBtn = FindViewById<Button>(Resource.Id.PodReportextBtn);
            PodReportextBtn.Click += PodReportextBtn_Click;

            //packing slip
            Button PodBtnCusPkn = FindViewById<Button>(Resource.Id.PodBtnCusPkn);
            PodBtnCusPkn.Click += PodBtnCusPkn_Click;

            //packing slip line
            Button PodBtnCusPknRepLneOny = FindViewById<Button>(Resource.Id.PodBtnCusPknRepLneOny);
            PodBtnCusPknRepLneOny.Click += PodBtnCusPknRepLneOny_Click;

            //Login Details
            Button PodBtnCusPknRepLogin = FindViewById<Button>(Resource.Id.PodBtnCusPknRepLogin);
            PodBtnCusPknRepLogin.Click += PodBtnCusPknRepLogin_Click;

            //Packing slip drak and drop 
            Button PKDragDropMap = FindViewById<Button>(Resource.Id.PKDragDropMap);
            PKDragDropMap.Click += PKDragDropMap_Click;
        }

        private void PodBtnCusPknRepLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ReportLoginCode));
        }

        private void PKDragDropMap_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDraggableListView));
        }

        private void PodBtnCusPkn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ReportPackingSlipJourCode));
        }


        private void PodBtnCusPknRepLneOny_Click(object sender, EventArgs e)
        {
            
            StartActivity(typeof(ReportPackingSlipJourLineCode));
        }
        private void PodReportextBtn_Click(object sender, EventArgs e)
        {
           
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