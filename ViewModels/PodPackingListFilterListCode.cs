using System;
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
    [Activity(Label = "PodPackingListFilterListCode")]
    public class PodPackingListFilterListCode : Activity
    {
        private SearchView PackingListFiltersv;
        private ListView PackingListFilterlv;
        JavaList<CustPackingSlipJour> spaceCrafts = new JavaList<CustPackingSlipJour>();
        ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PodPackingListFilterList);

            //Initialize UI
            this.InitializeUI();
            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            PackingListFilterlv.Adapter = adapter;
            PackingListFiltersv =  FindViewById<SearchView>(Resource.Id.PackingListFiltersv);
            PackingListFiltersv.QueryTextChange += PackingListFiltersv_QueryTextChange;
            //Button  retrive 

            Button PackingListFilterBtn = FindViewById<Button>(Resource.Id.PackingListFilterBtn);
            PackingListFilterBtn.Click += PackingListFilterBtn_Click;


        }

        private void PackingListFiltersv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchTerm = e.NewText;
            this.PopulateGrid(searchTerm);
        }

        private void PackingListFilterBtn_Click(object sender, EventArgs e)
        {
           
            //Toast.MakeText(this, result, ToastLength.Short).Show();
           // PopulateGrid(result);
        }

        //Retrive 
        private void PopulateGrid(string searchTerm)
        {
            adapter.Clear();

            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            List<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour(searchTerm);

            foreach (CustPackingSlipJour location in result)
                spaceCrafts.Add(location);

            PackingListFilterlv.Adapter = adapter;
        }

        private void PopulateGrid(TableQuery<CustPackingSlipJour> locations)
        {
            //PodLogLocationgv.Clear();
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
           //// TableQuery<CustPackingSlipJour> result = dbr.ViewCustPackingSlipJour(null);

           // foreach (CustPackingSlipJour location in result)
           //     spaceCrafts.Add(location);

           // PackingListFilterlv.Adapter = adapter;
        }


        private void InitializeUI()
        {
            PackingListFilterlv = FindViewById<ListView>(Resource.Id.PackingListFilterlv);
        }
    }
}