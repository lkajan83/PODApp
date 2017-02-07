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
using SQLite;
using Android.Media;


using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Content.PM;

namespace PODApp.ViewModels
{
   
    [Activity(Label = "ReportLoginCode", Icon = "@drawable/ArrowBack", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]

    public class ReportLoginCode : Activity
    {
        private ListView ReptLogIn_ListView;
        JavaList<LoginUsers> spaceCrafts = new JavaList<LoginUsers>();
        ArrayAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PODReports);

            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            AccountSetting.LoadSetting();
            ListView ReptLogIn_ListView = FindViewById<ListView>(Resource.Id.ReptLogIn_ListView);
            // Adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, spaceCrafts);
            ReptLogIn_ListView.Adapter = adapter;

            Button ReptLogIn_ImgReCpslPrt = FindViewById<Button>(Resource.Id.ReptLogIn_ImgReCpslPrt);

            ReptLogIn_ImgReCpslPrt.Click += ReptLogIn_ImgReCpslPrt_Click;
            Button ReptLogIn_Exit = FindViewById<Button>(Resource.Id.ReptLogIn_Exit);

            ReptLogIn_Exit.Click += ReptLogIn_Exit_Click;

        }

        private void ReptLogIn_Exit_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PODReportsCode));
        }

        private void ReptLogIn_ImgReCpslPrt_Click(object sender, EventArgs e)
        {
            //DBA_LoginUsers dbr = new DBA_LoginUsers();
            //TableQuery<LoginUsers> result = dbr.RRPODAppRBUsersRrt();
            //PopulateGrid(result);
        }

        private void PopulateGrid(TableQuery<LoginUsers> locations)
        {
            foreach (LoginUsers location in locations)
                spaceCrafts.Add(location);
            ReptLogIn_ListView.Adapter = adapter;
        }
    }
}