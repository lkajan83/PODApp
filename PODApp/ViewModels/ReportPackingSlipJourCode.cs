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

using Android.Database;
using SQLite;

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace PODApp.ViewModels
{
    [Activity(Label = "ReportPackingSlipJourCode", Icon = "@drawable/ArrowBack")]
    public class ReportPackingSlipJourCode : Activity
    {
        private ListView ReptPodL_ListPk;
        private Button ReptPodL_ImgReCpsl;
        JavaList<CustPackingSlipJourLine> spaceCrafts = new JavaList<CustPackingSlipJourLine>();
        ArrayAdapter adapter;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.ReportPackingSlipJour);
            AccountSetting.LoadSetting();

            ReptPodL_ListPk = FindViewById<ListView>(Resource.Id.ReptPodL_ListPk);

            ReptPodL_ImgReCpsl = FindViewById<Button>(Resource.Id.ReptPodL_ImgReCpsl);
            ReptPodL_ImgReCpsl.Click += ReptPodL_ImgReCpsl_Click;

            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            ReptPodL_ListPk.Adapter = adapter;

        }

        private void ReptPodL_ImgReCpsl_Click(object sender, EventArgs e)
        {
            RetPckSlipJour();
        }

        private void RetPckSlipJour()
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.PodPkReptCustPackingSlipJour();
            PopulateGrid(result);
        }

        private void PopulateGrid(List<CustPackingSlipJour> locations)
        {
            try
            {
                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                spaceCrafts.Clear();
                if (locations != null)
                {
                    foreach (CustPackingSlipJour location in locations)
                        spaceCrafts.Add
                            (location);
                }
                ReptPodL_ListPk.Adapter = adapter;
            }
            catch (Exception)
            {
                Toast.MakeText(this, "PopulateGrid Error Handling", ToastLength.Short).Show();
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

    }
}