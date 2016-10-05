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

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDBLogisticPostalAddressCode")]
    public class PodDBLogisticPostalAddressCode : Activity
    {
        private ListView PodDBLogisticPostalAddresslv;
        JavaList<LogisticPostalAddress> spaceCrafts = new JavaList<LogisticPostalAddress>();
        ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PodDBLogisticPostalAddress);

            //Initialize UI
            this.InitializeUI();


            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            PodDBLogisticPostalAddresslv.Adapter = adapter;

            //CREATE DATABASE 
            Button LogPosAddressCD = FindViewById<Button>(Resource.Id.LogPosAddressCD);
            LogPosAddressCD.Click += LogPosAddressCD_Click;


            //CREATE TABLE
            Button LogPosAddressCT = FindViewById<Button>(Resource.Id.LogPosAddressCT);
            LogPosAddressCT.Click += LogPosAddressCT_Click;

            //INSERT RECORD
            Button LogPosAddressIR = FindViewById<Button>(Resource.Id.LogPosAddressIR);
            LogPosAddressIR.Click += LogPosAddressIR_Click;

            //RETRIVE  RECORDS
            Button LogPosAddressRR = FindViewById<Button>(Resource.Id.LogPosAddressRR);
            LogPosAddressRR.Click += LogPosAddressRR_Click;

            //DELETE RECORD
            Button LogPosAddressDR = FindViewById<Button>(Resource.Id.LogPosAddressDR);
            LogPosAddressDR.Click += LogPosAddressDR_Click;
        }

        private void LogPosAddressDR_Click(object sender, EventArgs e)
        {
            PodDb_LogisticPostalAddress dbr = new PodDb_LogisticPostalAddress();
            EditText txtLPARecId = FindViewById<EditText>(Resource.Id.txtLPARecId);
            string result = dbr.DeleteLogisticPostalAddress(int.Parse(txtLPARecId.Text));
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }




        /// <summary>
        /// CREATE DATABASE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogPosAddressCD_Click(object sender, EventArgs e)
        {
            PodDb_LogisticPostalAddress dbr = new PodDb_LogisticPostalAddress();
            var result = dbr.CreatePodDb_LogisticPostalAddress();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
        /// <summary>
        /// CREATE TABLE - FUNCTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogPosAddressCT_Click(object sender, EventArgs e)
        {
            PodDb_LogisticPostalAddress dbr = new PodDb_LogisticPostalAddress();
            var result = dbr.TableCreatePodDb_LogisticPostalAddress();
            Toast.MakeText(this, result, ToastLength.Short).Show();

        }

        /// <summary>
        /// INSERT RECORD - FUNCTION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogPosAddressIR_Click(object sender, EventArgs e)
        {           
            EditText txtLPAAddress = FindViewById<EditText>(Resource.Id.txtLPAAddress);
            EditText txtLPAZipCode = FindViewById<EditText>(Resource.Id.txtLPAZipCode);
            EditText txtLPALocation = FindViewById<EditText>(Resource.Id.txtLPALocation);


            PodDb_LogisticPostalAddress dbr = new PodDb_LogisticPostalAddress();
            string result = dbr.InsertLogisticPostalAddress(txtLPAAddress.Text
                                                    + txtLPAZipCode.Text
                                                    + txtLPALocation.Text);

            Toast.MakeText(this, result, ToastLength.Short).Show();

        }
        /// <summary>
        /// RETRIVE RECORDS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogPosAddressRR_Click(object sender, EventArgs e)
        {
            //PodDb_LogisticPostalAddress dbr = new PodDb_LogisticPostalAddress();
            //var result = dbr.RetriveLogisticPostalAddress();
            //Toast.MakeText(this, result, ToastLength.Short).Show();

            PodDb_LogisticPostalAddress dbr = new PodDb_LogisticPostalAddress();
            TableQuery<LogisticPostalAddress> result = dbr.RetriveLogisticPostalAddress();
            PopulateGrid(result);

        }

        //Retrive  Grid view 
        private void PopulateGrid(TableQuery<LogisticPostalAddress> locations)
        {
            //PodDBLogisticPostalAddresslv.Clear();

            foreach (LogisticPostalAddress location in locations)
                spaceCrafts.Add(locations);

            PodDBLogisticPostalAddresslv.Adapter = adapter;
        }
        private void InitializeUI()
        {
            PodDBLogisticPostalAddresslv = FindViewById<ListView>(Resource.Id.PodDBLogisticPostalAddresslv);
        }
    }
}