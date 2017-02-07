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
using PODApp.Controls;
using SQLite;
using Android.Media;


using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace PODApp.ViewModels
{
    [Activity(Label = "ReportPackingSlipJourLineCode", Icon = "@drawable/ArrowBack")]
    public class ReportPackingSlipJourLineCode : Activity
    {
        private ListView ReptPodL_ListView;
        JavaList<CustPackingSlipJourLine> spaceCrafts = new JavaList<CustPackingSlipJourLine>();
        ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ReportPackingSlipJourLine);
            AccountSetting.LoadSetting();

            //allready veriable in private 
            //ListView ReptPodL_ListView = FindViewById<ListView>(Resource.Id.ReptPodL_ListView);
            ReptPodL_ListView = FindViewById<ListView>(Resource.Id.ReptPodL_ListView);
            //ReptPodL_ListView.Click += ReptPodL_ListView_Click;

            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            ReptPodL_ListView.Adapter = adapter;

            Button ReptPodL_ImgReCpslPrt = FindViewById<Button>(Resource.Id.ReptPodL_ImgReCpslPrt);
            ReptPodL_ImgReCpslPrt.Click += ReptPodL_ImgReCpslPrt_Click;
        }

        private void ReptPodL_ImgReCpslPrt_Click(object sender, EventArgs e)
        {
            ReportPckSlipJourLine();
        }

       private void ReportPckSlipJourLine()
            {
                PodDb_CustPackingSlipJourLine dbr = new PodDb_CustPackingSlipJourLine();
                TableQuery<CustPackingSlipJourLine> result = dbr.RR_CustPackingSlipJourLine();
                PopulateGrid(result);
            }

        private void PopulateGrid(TableQuery<CustPackingSlipJourLine> locations)
        {
            foreach (CustPackingSlipJourLine location in locations)
                spaceCrafts.Add(location);
            ReptPodL_ListView.Adapter = adapter;
        }

        //private void ReptPodL_ListView_Click(object sender, EventArgs e)
        //{
        //    Toast.MakeText(this, "List View", ToastLength.Short).Show();
        //}


        private void MethodSettingsOnOff()
        {
            try
            {
                // True ManualShort = True CusCollapse
                if (AccountSetting.podmodelsort || AccountSetting.podcollapse)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "PodDraggableListView");
                    StartActivity(NavManualSrt);
                }

                // True ManualShort = false CusCollapse
                else if ((AccountSetting.podcollapse && AccountSetting.podmodelsort))
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "PodDraggableListView");
                    StartActivity(NavManualSrt);
                }

                // True ManualShort = false CusCollapse
                else if ((AccountSetting.podmodelsort && AccountSetting.podcollapse))
                {
                    var NavManualSrt = new Intent(this, typeof(PodDraggableListView));
                    NavManualSrt.PutExtra("Parent", "PodDraggableListView");
                    StartActivity(NavManualSrt);
                }
                // False ManualShort = False CusCollapse
                else
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = false;
                    NavManualSrt.PutExtra("Parent", "PodDraggableListView");
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