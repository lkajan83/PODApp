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
using PODApp.Services;
using SQLite;
using Android.Media;

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDBCustPackingSlipJourCode")]
    public class PodDBCustPackingSlipJourCode : Activity
    {
        

        JavaList<CustPackingSlipJour> spaceCrafts = new JavaList<CustPackingSlipJour>();
      //  private ArrayAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PodDBCustPackingSlipJour);

            //CREATE DATABASE  - CustPackingSlipJour 

            Button CustPSlipJourDB = FindViewById<Button>(Resource.Id.CustPSlipJourDB);
            CustPSlipJourDB.Click += CustPSlipJourDB_Click;

            //CREATE TABLE
            Button CustPSlipJourCT = FindViewById<Button>(Resource.Id.CustPSlipJourCT);
            CustPSlipJourCT.Click += CustPSlipJourCT_Click;

            //INSERT RECORD
            Button CustPSlipJourIR = FindViewById<Button>(Resource.Id.CustPSlipJourIR);
            CustPSlipJourIR.Click += CustPSlipJourIR_Click;

            //RETRIEVE RECORD 
            Button CustPSlipJourRR = FindViewById<Button>(Resource.Id.CustPSlipJourRR);
            CustPSlipJourRR.Click += CustPSlipJourRR_Click;
        }     


        /// <summary>
        /// CREATE DATABASE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSlipJourDB_Click(object sender, EventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            var result = dbr.CreatePodDb_CustPackingSlipJour();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }


        /// <summary>
        /// CREATE TABLE  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSlipJourCT_Click(object sender, EventArgs e)
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            var result = dbr.TableCreatePodDb_CustPackingSlipJour();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }


        /// <summary>
        /// INSERT RECORD - FUNCTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSlipJourIR_Click(object sender, EventArgs e)
        {    
            EditText txtCPSJPackingSlipId = FindViewById<EditText>(Resource.Id.txtCPSJPackingSlipId);
            EditText txtCPSJDeliveryName = FindViewById<EditText>(Resource.Id.txtCPSJDeliveryName);
            EditText txtCPSJDelDesc = FindViewById<EditText>(Resource.Id.txtCPSJDelDesc);
            EditText txtCPSJDelAdd = FindViewById<EditText>(Resource.Id.txtCPSJDelAdd);
            EditText txtCPSJDelPCode = FindViewById<EditText>(Resource.Id.txtCPSJDelPCode);
            EditText txtCPSJVolume = FindViewById<EditText>(Resource.Id.txtCPSJVolume);
            EditText txtCPSJWeight = FindViewById<EditText>(Resource.Id.txtCPSJWeight);
            EditText txtCPSJNoUnit = FindViewById<EditText>(Resource.Id.txtCPSJNoUnit);
            //Image ImgCPSJIView = FindViewById<Image>(Resource.int.ImgCPSJIView);

      
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            string result = dbr.InsertCustPackingSlipJour(txtCPSJPackingSlipId.Text, 
                txtCPSJDeliveryName.Text,
                txtCPSJDelDesc.Text,
                txtCPSJDelAdd.Text,
                txtCPSJDelPCode.Text,
                txtCPSJVolume.Text,
                txtCPSJWeight.Text,
                txtCPSJNoUnit.Text);

            Toast.MakeText(this, result, ToastLength.Short).Show();

        }

 
        /// <summary>
        /// RETRIEVE RECORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSlipJourRR_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));

        }

    }
}