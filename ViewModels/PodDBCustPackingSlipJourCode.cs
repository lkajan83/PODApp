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

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDBCustPackingSlipJourCode")]
    public class PodDBCustPackingSlipJourCode : Activity
    {
        
        private ListView PodDBCustPackSlipJourlv;
        JavaList<CustPackingSlipJour> spaceCrafts = new JavaList<CustPackingSlipJour>();
        ArrayAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PodDBCustPackingSlipJour);

            //Initialize UI
            this.InitializeUI();

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

            //DELETE RECORDS
            Button CustPSlipJourDR = FindViewById<Button>(Resource.Id.CustPSlipJourDR);
            CustPSlipJourDR.Click += CustPSlipJourDR_Click;

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
            EditText txtCPSJDeviceId = FindViewById<EditText>(Resource.Id.txtCPSJRecId);
            EditText txtCPSJPackingSlipId = FindViewById<EditText>(Resource.Id.txtCPSJPackingSlipId);
            EditText txtCPSJQty = FindViewById<EditText>(Resource.Id.txtCPSJQty);
            EditText txtCPSJVolume = FindViewById<EditText>(Resource.Id.txtCPSJVolume);
            EditText txtCPSJWeight = FindViewById<EditText>(Resource.Id.txtCPSJWeight);
            EditText txtCPSJDeliveryName = FindViewById<EditText>(Resource.Id.txtCPSJDeliveryName);

            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            string result = dbr.InsertCustPackingSlipJour(txtCPSJDeviceId.Text,
                                                    txtCPSJPackingSlipId.Text,
                                                    txtCPSJQty.Text,
                                                    txtCPSJVolume.Text,
                                                    txtCPSJWeight.Text,
                                                    txtCPSJDeliveryName.Text);

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
            // PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            // var result = dbr.ViewCustPackingSlipJour(null);
            //// Toast.MakeText(this, result, ToastLength.Short).Show();

            //PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            //TableQuery<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour();
            //PopulateGrid(result);
        }

        ////Retrive  Grid view 
        //private void PopulateGrid(TableQuery<CustPackingSlipJour> locations)
        //{
        //    //PodLogLocationgv.Clear();
        //    foreach (CustPackingSlipJour location in locations)
        //        spaceCrafts.Add(location);
        //    PodDBCustPackSlipJourlv.Adapter = adapter;
        //}

        /// <summary>
        /// DELETE RECORDS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustPSlipJourDR_Click(object sender, EventArgs e)
        {
            //PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            //EditText txtCPSJRecId = FindViewById<EditText>(Resource.Id.txtCPSJRecId);
            //string result = dbr.DeleteCustPackingSlipJour(int.Parse(txtCPSJRecId.Text));
            //Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        private void InitializeUI()
        {
            PodDBCustPackSlipJourlv = FindViewById<ListView>(Resource.Id.PodDBCustPackSlipJourlv);
        }
    }
}