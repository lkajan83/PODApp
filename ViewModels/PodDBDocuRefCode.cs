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
    [Activity(Label = "PodDBDocuRefCode")]
    public class PodDBDocuRefCode : Activity
    {
        private ListView PodDocReflv;
        JavaList<DocuRef> spaceCrafts = new JavaList<DocuRef>();
        ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PodDBDocuRef);

            //Initialize UI
            this.InitializeUI();

            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            PodDocReflv.Adapter = adapter;

            //CREATE DATABASE 
            Button DocRefCD = FindViewById<Button>(Resource.Id.DocRefCD);
            DocRefCD.Click += DocRefCD_Click;

            //CREATE TABLE
            Button DocRefCT = FindViewById<Button>(Resource.Id.DocRefCT);
            DocRefCT.Click += DocRefCT_Click;

            //INSERT RECORD
            Button DocRefIR = FindViewById<Button>(Resource.Id.DocRefIR);
            DocRefIR.Click += DocRefIR_Click;

            //RETRIVE RECORD
            Button DocRefRR = FindViewById<Button>(Resource.Id.DocRefRR);
            DocRefRR.Click += DocRefRR_Click;

            //DELETE RECORD
            Button DocRefDR = FindViewById<Button>(Resource.Id.DocRefDR);
            DocRefDR.Click += DocRefDR_Click;
        }


        /// <summary>
        /// CREATE DATABASE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocRefCD_Click(object sender, EventArgs e)
        {
            PodDb_DocuRef dbr = new PodDb_DocuRef();
            var result = dbr.CreatePodDb_PodDbDocuRef();
            Toast.MakeText(this, result, ToastLength.Short).Show();

        }

        /// <summary>
        /// CREATE TABLE 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocRefCT_Click(object sender, EventArgs e)
        {
            PodDb_DocuRef dbr = new PodDb_DocuRef();
            var result = dbr.TableCreatePodDb_PodDbDocuRef();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        /// <summary>
        /// INSERT RECORD 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocRefIR_Click(object sender, EventArgs e)
        {
            EditText txtDRPackingSlipId = FindViewById<EditText>(Resource.Id.txtDRPackingSlipId);
            EditText txtDRLocation = FindViewById<EditText>(Resource.Id.txtDRLocation);

            PodDb_DocuRef dbr = new PodDb_DocuRef();
            string result = dbr.InsertPodDbDocuRef(txtDRPackingSlipId.Text
                                                    + txtDRLocation.Text);

            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        /// <summary>
        /// RETRIVE RECORDS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocRefRR_Click(object sender, EventArgs e)
        {
            //PodDb_DocuRef dbr = new PodDb_DocuRef();
            //var result = dbr.RetrivePodDbDocuRef();
            //Toast.MakeText(this, result, ToastLength.Short).Show();



            PodDb_DocuRef dbr = new PodDb_DocuRef();
            TableQuery<DocuRef> result = dbr.RetrivePodDbDocuRef();
            PopulateGrid(result);
        }

        //Retrive  Grid view 
        private void PopulateGrid(TableQuery<DocuRef> locations)
        {
            //PodLogLocationgv.Clear();

            foreach (DocuRef location in locations)
                spaceCrafts.Add(location);

            PodDocReflv.Adapter = adapter;
        }



        /// <summary>
        /// DELETE RECORDS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocRefDR_Click(object sender, EventArgs e)
        {
            PodDb_DocuRef dbr = new PodDb_DocuRef();
            EditText txtDRRecIdId = FindViewById<EditText>(Resource.Id.txtDRRecIdId);
            string result = dbr.DeletePodDbDocuRef(int.Parse(txtDRRecIdId.Text));
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
        private void InitializeUI()
        {
            PodDocReflv = FindViewById<ListView>(Resource.Id.PodDocReflv);
        }
    }
}