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
using PODApp.mCode.mDataBase;
using SQLite;

namespace PODApp.ViewModels
{
    [Activity(Label = "PodDBLogisticsLocationCode")]
    public class PodDBLogisticsLocationCode : Activity
    {

        private ListView PodLogLocationlv;
        JavaList<LogisticsLocation> spaceCrafts = new JavaList<LogisticsLocation>();
        ArrayAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.PodDBLogisticsLocation);

            //Initialize UI
            this.InitializeUI();


            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            PodLogLocationlv.Adapter = adapter;
                     

            //CREATE DATABASE 
            Button LogLocationCD = FindViewById<Button>(Resource.Id.LogLocationCD);
            LogLocationCD.Click += LogLocationCD_Click;

            //CREATE TABLE
            Button LogLocationCT = FindViewById<Button>(Resource.Id.LogLocationCT);
            LogLocationCT.Click += LogLocationCT_Click;

            //INSERT RECORD
            Button LogLocationIR = FindViewById<Button>(Resource.Id.LogLocationIR);
            LogLocationIR.Click += LogLocationIR_Click;

            //RETRIVE RECORD
            Button LogLocationRR = FindViewById<Button>(Resource.Id.LogLocationRR);
            LogLocationRR.Click += LogLocationRR_Click;

            //DELETE RECORD
            Button LogLocationDR = FindViewById<Button>(Resource.Id.LogLocationDR);
            LogLocationDR.Click += LogLocationDR_Click;
        }


        /// <summary>
        /// CREATE DATABASE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogLocationCD_Click(object sender, EventArgs e)
        {
            PodDb_LogisticsLocation dbr = new PodDb_LogisticsLocation();
            var result = dbr.DbCreatePodDb_LogisticsLocation();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        /// <summary>
        /// CREATE TABLE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogLocationCT_Click(object sender, EventArgs e)
        {
            PodDb_LogisticsLocation dbr = new PodDb_LogisticsLocation();
            var result = dbr.CreateTablePodDb_LogisticsLocation();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
        /// <summary>
        /// INSERT RECORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogLocationIR_Click(object sender, EventArgs e)
        {
            EditText txtLLLogLocation = FindViewById<EditText>(Resource.Id.txtLLLogLocation);
            EditText txtLLLogDescription = FindViewById<EditText>(Resource.Id.txtLLLogDescription);

            PodDb_LogisticsLocation dbr = new PodDb_LogisticsLocation();
            string result = dbr.InsertLogisticsLocation(txtLLLogLocation.Text
                                                    + txtLLLogDescription.Text);

            Toast.MakeText(this, result, ToastLength.Short).Show();

        }
        /// <summary>
        /// RETRIVE THE ROCORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogLocationRR_Click(object sender, EventArgs e)
        {
            PodDb_LogisticsLocation dbr = new PodDb_LogisticsLocation();
            TableQuery<LogisticsLocation> result = dbr.RetriveLogisticsLocation();
            //Toast.MakeText(this, result, ToastLength.Short).Show();
            PopulateGrid(result);
        }
        //Retrive  Grid view 
        private void PopulateGrid(TableQuery<LogisticsLocation> locations)
        {
            //PodLogLocationgv.Clear();
            foreach(LogisticsLocation location in locations)
            spaceCrafts.Add(location);
            PodLogLocationlv.Adapter = adapter;
        }

        /// <summary>
        /// DELETE RECORD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogLocationDR_Click(object sender, EventArgs e)
        {
            PodDb_LogisticsLocation dbr = new PodDb_LogisticsLocation();
            EditText txtLLRecId = FindViewById<EditText>(Resource.Id.txtLLRecId);
            string result = dbr.DeleteLogisticsLocation(int.Parse(txtLLRecId.Text));
            Toast.MakeText(this, result, ToastLength.Short).Show();

        }

        private void InitializeUI() {
            PodLogLocationlv = FindViewById<ListView>(Resource.Id.PodLogLocationlv);
        }
    }
}