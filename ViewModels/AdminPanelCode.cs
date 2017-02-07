using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using PODApp.Models;
using PODApp.Services;

namespace PODApp.ViewModels
{
    [Activity(Label = "AdminPanelCode")]
    public class AdminPanelCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AdminPanel);

            Button Pod_Administrator = FindViewById<Button>(Resource.Id.Pod_Administrator);
            Pod_Administrator.Click += Pod_Administrator_Click;

            Button Pod_Users = FindViewById<Button>(Resource.Id.Pod_Users);
            Pod_Users.Click += Pod_Users_Click;
        }
        private void Pod_Administrator_Click(object sender, EventArgs e)
        {
            //StartActivity(typeof(PodDbCreateCode));
            StartActivity(typeof(AdministratorLoginCode));
        }

        private void Pod_Users_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodPackingListFilterListCode));
        }


    }
}