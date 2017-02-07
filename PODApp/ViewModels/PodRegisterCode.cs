using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using SQLite;
using PODApp.Models;
using System.Text.RegularExpressions;
using PODApp.Controls;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "PodRegisterCode", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class PodRegisterCode : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        ImageView btncreate, PodRegLogInBck;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PodRegister);


            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            //USER NAME 
            txtusername = FindViewById<EditText>(Resource.Id.txt_reg_username);

            //PASSWORD
            txtPassword = FindViewById<EditText>(Resource.Id.txt_reg_password);

            //CREATE DATABASE IMAGE
            btncreate = FindViewById<ImageView>(Resource.Id.btn_reg_create);
            btncreate.Click += Btncreate_Click;

            //BACK IMAGE VIEW
            PodRegLogInBck = FindViewById<ImageView>(Resource.Id.PodRegLogInBck);
            PodRegLogInBck.Click += PodRegLogInBck_Click;

            //CREATE USER DATABASE
            TablePODAppLoginUsers();

            // CREATE USER TABLE
            CreatePODAppLoginUsers();

            //TABLE PACKING SLIP
            TablePODAppPackSlip();


            //CREATE TABLE PACKING SLIPLINE
            TablePODAppPackSlipLine();


            //CREATE TABLE - PACKING SLIP ATTACHMENT
            TablePackingSlipAttachment();
        }

        private void TablePODAppPackSlip()
        {
            PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            var result = dbr.TableCreatePodDb_CustPackingSlipJour();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        /// <summary>
        /// CREATE TABLE - PACKING SLIP 
        /// </summary>
        private void TablePODAppPackSlipLine()
        {
            PodDb_CustPackingSlipJourLine dbr = new PodDb_CustPackingSlipJourLine();
            var result = dbr.CT_CustPackingSlipJourLine();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        /// <summary>
        /// CREATE TABLE - CREATE TABLE - PACKING SLIP ATTACHEMT 
        /// </summary>
        private void TablePackingSlipAttachment()
        {
            DBA_PackingSlipAttachment dbr = new DBA_PackingSlipAttachment();
            var result = dbr.CT_PackingSlipAttachment();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
        private void TablePODAppLoginUsers()
        {
            DBA_LoginUsers dbr = new DBA_LoginUsers();
            var result = dbr.PODApp_CtUsers();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        public void CreatePODAppLoginUsers()
        {
            DBA_LoginUsers dbr = new DBA_LoginUsers();
            var result = dbr.PODApp_CtUsers();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        //CREATE LOGIN USERS
        private void Btncreate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtusername.Text != "" && txtusername.Text != null && txtPassword.Text != "" && txtPassword.Text != null)
                {

                    if (isValidEmail(txtusername.Text))
                    {                        
                        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers");
                        var db = new SQLiteConnection(dbPath);

                        //string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LoginUsers");
                        //var db = new SQLiteConnection(dbPath);

                        db.CreateTable<LoginUsers>();
                        LoginUsers tbl = new LoginUsers();
                        tbl.UserName = txtusername.Text;
                        tbl.Password = txtPassword.Text;
                        db.Insert(tbl);
                        Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
                        var PodRegCde = new Intent(this, typeof(MainActivity));
                        PodRegCde.PutExtra("Parent", "PodRegisterCode");
                        StartActivity(PodRegCde);
                    }
                    else
                    {
                        Toast.MakeText(this, "Please Enter Valid EmailID...", ToastLength.Short).Show();
                    }

                }
                else
                {
                    Toast.MakeText(this, "Please Enter username and password...,", ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        /// <summary>
        ///BACK BUTTON 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void PodRegLogInBck_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }


        /// <summary>
        /// CREATE USER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

      
     

        private void MethodSettingsOnOff()
        {
            try
            {
                if (AccountSetting.podcollapse == true && AccountSetting.podmodelsort == true)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else if (AccountSetting.podcollapse == true && AccountSetting.podmodelsort == false)
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else if (AccountSetting.podcollapse == false && AccountSetting.podmodelsort == true)
                {
                    var NavManualSrt = new Intent(this, typeof(PodDraggableListView));
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
                else
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = false;
                    NavManualSrt.PutExtra("Parent", "ActSettingCode");
                    StartActivity(NavManualSrt);
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Create Database", ToastLength.Short).Show();
                return;
            }
        }
    }
}