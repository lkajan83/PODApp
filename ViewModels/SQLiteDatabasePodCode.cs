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
using PODApp.mCode.mDataObject;
using PODApp.mCode.mDataBase;
using Android.Database;

namespace PODApp.ViewModels
{
    [Activity(Label = "SQLiteDatabasePodCode")]
    public class SQLiteDatabasePodCode : Activity
    {
        private GridView gv;
        private SearchView sv;
        private EditText nameEditText;
        private Button saveBtn, RetrieveBtn;
        JavaList<SpaceCraft> spaceCrafts = new JavaList<SpaceCraft>();
        ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SQLiteDatabasePOD);

            //Initialize UI
            this.InitializeUI();

            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            gv.Adapter = adapter;

            //Event Save
            saveBtn.Click += SaveBtn_Click;
            RetrieveBtn.Click += RetrieveBtn_Click;
            gv.ItemClick += Gv_ItemClick;
            sv.QueryTextChange += Sv_QueryTextChange;

        }

        private void Sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchTerm = e.NewText;
            this.GetSpaceCrafts(searchTerm);
        }

        private void Gv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, spaceCrafts[e.Position].ToString(),ToastLength.Short).Show();
        }

        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            GetSpaceCrafts(null);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Save(nameEditText.Text);
        }

        private void InitializeUI()
        {
            gv = FindViewById<GridView>(Resource.Id.gv);
            sv = FindViewById<SearchView>(Resource.Id.sv);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            saveBtn = FindViewById<Button>(Resource.Id.saveBtn);
            RetrieveBtn = FindViewById<Button>(Resource.Id.RetrieveBtn);

        }

        // SAVE
        private void Save(string name)
        {
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            bool saved = db.Add(name);
            db.CloseDB();
            if(saved)
            {
                nameEditText.Text = "";
            }
            else
            {
                Toast.MakeText(this, "unable To save", ToastLength.Short).Show();
            }

            GetSpaceCrafts(null);
        }


        //Retrive 
        private void GetSpaceCrafts(string searchTerm)
        {
            spaceCrafts.Clear();
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            ICursor c = db.Retrieve(searchTerm);
            SpaceCraft s = null;
            if(c!=null)
            {
                while (c.MoveToNext())
                {
                    int id = c.GetInt(0);
                    string name = c.GetString(1);

                    s = new SpaceCraft();
                    s.Id = id;
                    s.Name = name;
                    spaceCrafts.Add(s);
                }

            }
            db.CloseDB();
            gv.Adapter = adapter;
        }

    }
}