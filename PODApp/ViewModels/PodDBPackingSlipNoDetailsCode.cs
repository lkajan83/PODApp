using System;

using Android.App;
using Android.OS;
using Android.Widget;
using PODApp.Models;

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDBPackingSlipNoDetailsCode")]
    public class PodDBPackingSlipNoDetailsCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PodDBPackingSlipNoDetails);

            //Initialize UI
            this.InitializeUI();

            Button BtnSearchDBPS = FindViewById<Button>(Resource.Id.BtnSearchDBPS);
            BtnSearchDBPS.Click += BtnSearchDBPS_Click;

            Button BtnPSNDProcessPod = FindViewById<Button>(Resource.Id.BtnPSNDProcessPod);
            BtnPSNDProcessPod.Click += BtnPSNDProcessPod_Click;


        }



        /// <summary>
        /// SEARCH 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchDBPS_Click(object sender, EventArgs e)
        {

            EditText txtEditPSNDDelRecId = FindViewById<EditText>(Resource.Id.txtEditPSNDDelRecId);
            EditText txtEditPSNDDelpPkSlipNo = FindViewById<EditText>(Resource.Id.txtEditPSNDDelpPkSlipNo);
            EditText txtEditPSNDDelName = FindViewById<EditText>(Resource.Id.txtEditPSNDDelName);
            EditText txtEditPSNDDelDes = FindViewById<EditText>(Resource.Id.txtEditPSNDDelDes);
            EditText txtEditPSNDDelAdd = FindViewById<EditText>(Resource.Id.txtEditPSNDDelAdd);
            EditText txtEditPSNDPostCode = FindViewById<EditText>(Resource.Id.txtEditPSNDPostCode);
            EditText txtEditPSNDVolume = FindViewById<EditText>(Resource.Id.txtEditPSNDVolume);
            EditText txtEditPSNDNetWeight = FindViewById<EditText>(Resource.Id.txtEditPSNDNetWeight);
            EditText txtEditPSNDQty = FindViewById<EditText>(Resource.Id.txtEditPSNDQty);


            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            txtEditPSNDDelpPkSlipNo.Text = dbr.CustPackingSlipJourrrRecId(int.Parse(txtEditPSNDDelRecId.Text));


        }
        /// <summary>
        /// UPDATE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPSNDProcessPod_Click(object sender, EventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            EditText txtEditPSNDDelRecId = FindViewById<EditText>(Resource.Id.txtEditPSNDDelRecId);
            EditText txtEditPSNDDelpPkSlipNo = FindViewById<EditText>(Resource.Id.txtEditPSNDDelpPkSlipNo);
            string result = dbr.UpdateCustPackingSlipJour(int.Parse(txtEditPSNDDelRecId.Text), txtEditPSNDDelpPkSlipNo.Text);
            Toast.MakeText(this, result, ToastLength.Short).Show();

        }
        /// <summary>
        /// UPDATE 
        /// </summary>
        private void InitializeUI()
        {
            //PodDBCustPackSlipJourlv = FindViewById<ListView>(Resource.Id.PodDBCustPackSlipJourlv);
        }
    }
}