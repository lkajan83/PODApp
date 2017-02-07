using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using PODApp.Models;

using PODApp.ViewModels;
using System.IO;
using SQLite;
using PODApp.Models;
using PODApp.Controls;
using PODApp.ViewModels;

using Android.Content.PM;

namespace PODApp
{
    [Activity(Label = "PODApp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]

    public class MainActivity : Activity
    {
        private EditText txtusername, txtPassword;
        private ImageView btncreate, btnsign, BtnAccSetts;

        string m_ParentActivity = "";
        string PodNavManSet = "PodNavManSet";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.PODAdminLogin);
           
            //Initialize UI
            this.InitializeUI();
        }

        private void InitializeUI()
        {
            //USER NAME      
            txtusername = FindViewById<EditText>(Resource.Id.txtusername);
           
            //PASSWORD
            txtPassword = FindViewById<EditText>(Resource.Id.txtpwd);

            //LOGIN
            btnsign = FindViewById<ImageView>(Resource.Id.btnlogin);
            btnsign.Click += Btnsign_Click;
         

            //REGISTER
            btncreate = FindViewById<ImageView>(Resource.Id.btnregister);
            btncreate.Click += Btncreate_Click;
            
        } 



        /// <summary>
        /// LOGIN SCREEN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnsign_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginUsers>(); //Call Table  
                var data1 = data.Where(x => x.UserName == txtusername.Text && x.Password == txtPassword.Text).FirstOrDefault(); //Linq Query  
                
                if (data1 != null)
                {
                    LoginUsers_tmp.UserName = txtusername.Text;
                    LoginUsers_tmp.Password = txtPassword.Text;
                    LoginUsers_tmp.DeviceId = data1.DeviceId;
                    if (data1.UserName == "lkajan@gmail.com")
                    {
                        AccountSetting.LoadSetting();
                        StartActivity(typeof(TabHostCode));
                    }
                    else
                    {
                        MethodSettingsOnOff();
                    }
                    //clear the text
                    txtusername.Text = "";
                    txtPassword.Text = "";
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

        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodRegisterCode));
        }


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



        protected void saveset()
        {

            //store
            var prefs = Application.Context.GetSharedPreferences("PODApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("PrefName", "Some value");
            prefEditor.Commit();

        }

        // Function called from OnCreate
        protected void retrieveset()
        {
            //retreive   https://forums.xamarin.com/discussion/8199/how-to-save-user-settings
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var somePref = prefs.GetString("PrefName", null);

            //Show a toast
            RunOnUiThread(() => Toast.MakeText(this, somePref, ToastLength.Long).Show());



        }
          
    }
}
