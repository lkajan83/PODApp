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
using SQLite;
using System.IO;
using PODApp.Models;

namespace PODApp.ViewModels
{
    [Activity(Label = "AdministratorRegisterCode")]
    public class AdministratorRegisterCode : Activity
    {
        EditText txtAdminusername;
        EditText txtAdminPassword;
        Button btnAdmincreate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AdministratorRegister);

            // Create your application here  
            btnAdmincreate = FindViewById<Button>(Resource.Id.btn_admin_reg_create);
            txtAdminusername = FindViewById<EditText>(Resource.Id.txt_admin_reg_username);
            txtAdminPassword = FindViewById<EditText>(Resource.Id.txt_admin_reg_password);
            btnAdmincreate.Click += BtnAdmincreate_Click;
        }

        private void BtnAdmincreate_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "adminuser.db");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<AdminLoginTable>();
                AdminLoginTable tbl = new AdminLoginTable();
                tbl.username = txtAdminusername.Text;
                tbl.password = txtAdminPassword.Text;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}