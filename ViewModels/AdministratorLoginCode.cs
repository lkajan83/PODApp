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
using PODApp.ViewModels;
using PODApp.Models;
using System.IO;
using SQLite;

namespace PODApp.ViewModels
{
    [Activity(Label = "AdministratorLoginCode")]
    public class AdministratorLoginCode :  Activity
    {
                EditText txtAdminusername;
                EditText txtAdminPassword;
                Button btnAdmincreate;
                Button btnAdminsign;
    protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.AdministratorLogin);
            // Get our button from the layout resource,  
            // and attach an event to it  
            btnAdminsign = FindViewById<Button>(Resource.Id.btnAdminlogin);
            btnAdmincreate = FindViewById<Button>(Resource.Id.btnAdminregister);
            txtAdminusername = FindViewById<EditText>(Resource.Id.txtAdminusername);
            txtAdminPassword = FindViewById<EditText>(Resource.Id.txtAdminpwd);
            btnAdmincreate.Click += BtnAdmincreate_Click;
            btnAdminsign.Click += BtnAdminsign_Click;
            CreateAdminDB();
        }

 

        private void BtnAdmincreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AdministratorRegisterCode));
        }
       private void BtnAdminsign_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "adminuser.db"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<AdminLoginTable>(); //Call Table  
                var data1 = data.Where(x => x.username == txtAdminusername.Text && x.password == txtAdminPassword.Text).FirstOrDefault(); //Linq Query  
                if (data1 != null)
                {
                    //Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                    StartActivity(typeof(PodDbCreateCode));
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        public string CreateAdminDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "adminuser.db"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}