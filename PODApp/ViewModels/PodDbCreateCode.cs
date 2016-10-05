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
using PODApp.ViewModels;

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDbCreateCode")]
    public class PodDbCreateCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.PodDbCreate);

            //Button -  CustPackingSlipJour
            Button BtnClickCustPackingSlipJour = FindViewById<Button>(Resource.Id.BtnClickCustPackingSlipJour);
            BtnClickCustPackingSlipJour.Click += BtnClickCustPackingSlipJour_Click;

            //Button -  LogisticPostalAddress
            Button BtnClickLogisticPosAdd = FindViewById<Button>(Resource.Id.BtnClickLogisticPosAdd);
            BtnClickLogisticPosAdd.Click += BtnClickLogisticPosAdd_Click;

            //Button - LogisticsLocation
            Button BtnClickLogisticsLocation = FindViewById<Button>(Resource.Id.BtnClickLogisticsLocation);
            BtnClickLogisticsLocation.Click += BtnClickLogisticsLocation_Click;

            //Button - DocuRef
            Button BtnClickDocuRef = FindViewById<Button>(Resource.Id.BtnClickDocuRef);
            BtnClickDocuRef.Click += BtnClickDocuRef_Click;

            //
            Button BtnClickDasBoardPSDB = FindViewById<Button>(Resource.Id.BtnClickDasBoardPSDB);
            BtnClickDasBoardPSDB.Click += BtnClickDasBoardPSDB_Click;

            //Button
            Button BtnClickPackingSlipDetails = FindViewById<Button>(Resource.Id.BtnClickPackingSlipDetails);
            BtnClickPackingSlipDetails.Click += BtnClickPackingSlipDetails_Click;


            //Button 
            Button BtnClickPodPackingListFilterList = FindViewById<Button>(Resource.Id.BtnClickPodPackingListFilterList);
            BtnClickPodPackingListFilterList.Click += BtnClickPodPackingListFilterList_Click;
        }

        private void BtnClickPodPackingListFilterList_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));
        }

        private void BtnClickPackingSlipDetails_Click(object sender, EventArgs e)
        {
             StartActivity(typeof(PodDBPackingSlipNoDetailsCode));
        }

        /// <summary>
        /// DASHBOARD WITH PACKING SLIP ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClickDasBoardPSDB_Click(object sender, EventArgs e)
        {
            //StartActivity(typeof(PodDashBoardCode));
            StartActivity(typeof(SQLiteDatabasePodCode));
        }
        /// <summary>
        /// Code Button - DocuRef
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClickDocuRef_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBDocuRefCode));
        }

        /// <summary>
        /// Code Button - LogisticsLocation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClickLogisticsLocation_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBLogisticsLocationCode));
        }

        /// <summary>
        /// Button -  CustPackingSlipJour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClickCustPackingSlipJour_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBCustPackingSlipJourCode));
        }
        /// <summary>
        /// Code Button -  LogisticPostalAddress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClickLogisticPosAdd_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodDBLogisticPostalAddressCode));            
        }
    }
}