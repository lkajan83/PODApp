using System;
using Android.App;
using Android.OS;
using Android.Widget;
using PODApp.Models;
using System.IO;
using Android.Media;
using Android.Graphics;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using System.Threading;
using Android.Content.PM;
using PODApp.Controls;
using PODApp.Models;
using PODApp.ViewModels;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace PODApp.ViewModels
{
    [Activity(Label = "PACKING SLIP DETAILS", Theme = "@style/Base.Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]

    public class PodDBPackingSlipNoDetailsCode : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        List<CustPackingSlipJour2> tableItems = new List<CustPackingSlipJour2>();
        ListView listView;

        List<CustPackingSlipJourLine> tableJourLine = new List<CustPackingSlipJourLine>();
        ListView ListCustJourLines;


        //STEP - 50
        string m_ParentActivity = "";
        string PodCameraScreenCode = "PodCameraScreenCode";

        string PodNavManSet = "PodNavManSet";

        string PodSignaturePadPanelCode = "SignaturePadPanelCode";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //STEP - 60
            m_ParentActivity = Intent.GetStringExtra("Parent");

            // Create your application here
            SetContentView(Resource.Layout.PodDBPackingSlipNoDetails);

            AccountSetting.LoadSetting();

            // start site navigation
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);


            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView);

            //Join Table CustPackingSlipJourLine { Description, Unit,Qty, Item }

            ListCustJourLines = FindViewById<ListView>(Resource.Id.ListCustJourLines);

            if (tableJourLine != null)
            {
                foreach (var itemLine in tableJourLine)
                {
                    tableJourLine.Add(new CustPackingSlipJourLine() { Description = itemLine.Description, Unit = itemLine.Unit, Qty = itemLine.Qty, Item = itemLine.Item });
                }
            }
            ListCustJourLines.Adapter = new HomeScreenAdapterLine(this, tableJourLine);


            char[] delimiterChars = { '-' };
            string[] text = CustPackingSlipJour1.PackingSlipId.ToString().Split(delimiterChars);

            //Initialize UI
            this.InitializeUI();

            Bindadata(text[0]);

            //SAVE  IMAGE 
            ImageView BtnPSNDProcessPod = FindViewById<ImageView>(Resource.Id.BtnPSNDProcessPod);
            BtnPSNDProcessPod.Click += BtnPSNDProcessPod_Click;


            //CAMERA IMAGE
            ImageView BtnPSNDPodCam = FindViewById<ImageView>(Resource.Id.BtnPSNDPodCam);
            BtnPSNDPodCam.Click += BtnPSNDPodCam_Click;

            //SIGNATURE IMAGE
            ImageView PodPadSign = FindViewById<ImageView>(Resource.Id.PodPadSign);
            PodPadSign.Click += PodPadSign_Click;

            //SignaturePad IMAGEVIEW 
            ImageView ImgSigView = FindViewById<ImageView>(Resource.Id.ImgSigView);

            //BACK TO MAIN PAGE
            ImageView Nav_PodPsB2PS = FindViewById<ImageView>(Resource.Id.Nav_PodPsB2PS);
            Nav_PodPsB2PS.Click += Nav_PodPsB2PS_Click;

        }


        /// <summary>
        /// LIST VIEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //    throw new NotImplementedException();
            //}
            ////private void ListView_ItemLongClick(object sender, AdapterView.ItemClickEventArgs e)
            //{
            Toast.MakeText(this, "List Item Delete or Update", ToastLength.Short).Show();
            var menu = new PopupMenu(this, (View)sender);
            string selectedFromList = listView.GetItemAtPosition(e.Position).ToString();
            int position = e.Position;
            menu.Inflate(Resource.Menu.popupmenu);
            menu.MenuItemClick += (s, a) =>
            {
                ListView listView;
                App._dir = new Java.IO.File(
                        Environment.GetExternalStoragePublicDirectory(
                        Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId);

                var files = App._dir.ListFiles();
                int i = 0;
                switch (a.Item.ItemId)
                {
                    case Resource.Id.pm_update:
                        foreach (var item in files)
                        {
                            if (position == i)
                            {
                                item.Delete();
                            }
                            i++;
                        }
                        var ListViewUpdate = new Intent(this, typeof(PodCameraScreenCode));
                        ListViewUpdate.PutExtra("Parent", "PodDBPackingSlipNoDetailsCode");
                        StartActivity(ListViewUpdate);
                        Toast.MakeText(this, "Deleted record from database", ToastLength.Short).Show();
                        break;
                    case Resource.Id.pm_delete:
                        foreach (var item in files)
                        {
                            if (position == i)
                            {
                                item.Delete();
                            }
                            i++;
                        }

                        ////Binding imageView
                        listView = FindViewById<ListView>(Resource.Id.List);
                        //  listView.ItemClick += ListView_ItemLongClick;
                        //listView.ItemLongClick += ListView_ItemLongClick;
                        tableItems.Clear();
                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                Uri contentUri = Uri.FromFile(item);
                                tableItems.Add(new CustPackingSlipJour2() { Podimage = contentUri });
                            }
                        }
                        listView.Adapter = new HomeScreenAdapter(this, tableItems);
                        Toast.MakeText(this, "Deleted record from database", ToastLength.Short).Show();
                        break;
                }
            };
            menu.Show();
        }




        /// <summary>
        /// NAV - MENU
        /// </summary>
        /// <param name="navigationView"></param>
        void setupDrawerContent(NavigationView navigationView)
        {
            try
            {
                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    e.MenuItem.SetChecked(true);
                    if (e.MenuItem.ToString() == "Home Page")
                    {
                        MethodSettingsOnOff();
                    }
                    else if (e.MenuItem.ToString() == "Account Settings")
                    {
                        StartActivity(typeof(ActSettingCode));
                    }

                    else if (e.MenuItem.ToString() == "Report a Problem")
                    {
                        StartActivity(typeof(ReportAProblemCode));
                    }

                    else if (e.MenuItem.ToString() == "Help Center")
                    {
                        StartActivity(typeof(HelpCenterCode));
                    }

                    else if (e.MenuItem.ToString() == "About us")
                    {
                        StartActivity(typeof(AboutusCode));
                    }
                    else if (e.MenuItem.ToString() == "Logout")
                    {
                        StartActivity(typeof(MainActivity));
                    }


                    drawerLayout.CloseDrawers();
                };
            }
            catch (Exception)
            {
                Toast.MakeText(this, "NavigationView Packing Detail Exception Error", ToastLength.Short).Show();
                return;
            }
        }


        /// <summary>
        /// NAV - MENU OVERRIDE
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                return true;
            }
            catch (Exception Ex)
            {
                Toast.MakeText(this, "Packing Slip No Details Exception Error", ToastLength.Short).Show();
                return false;
            }
        }

        /// <summary>
        /// BACK BUTTON 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nav_PodPsB2PS_Click(object sender, EventArgs e)
        {
            MethodSettingsOnOff();
        }
        private void PodPadSign_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignaturePadPanelCode));
        }


        /// <summary>
        /// CAMERA BUTTON 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnPSNDPodCam_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodCameraScreenCode));
        }
        /// <summary>
        /// BIND PackingSlipId DETAIL VIEW
        /// </summary>
        /// <param name="PackingSlipId"></param>

        private void Bindadata(string PackingSlipId)
        {
            try
            {
                //EDIT TEXT - PodDBPackingSlipNoDetailsCode
                TextView txtEditPSNDDelRecId = FindViewById<TextView>(Resource.Id.txtEditPSNDDelRecId);
                TextView txtEditPSNDDelpPkSlipNo = FindViewById<TextView>(Resource.Id.txtEditPSNDDelpPkSlipNo);
                TextView txtEditPSNDDelName = FindViewById<TextView>(Resource.Id.txtEditPSNDDelName);
                TextView txtEditPSNDDelDes = FindViewById<TextView>(Resource.Id.txtEditPSNDDelDes);
                TextView txtEditPSNDDelAdd = FindViewById<TextView>(Resource.Id.txtEditPSNDDelAdd);
                TextView txtEditPSNDPostCode = FindViewById<TextView>(Resource.Id.txtEditPSNDPostCode);
                TextView txtEditPSNDVolume = FindViewById<TextView>(Resource.Id.txtEditPSNDVolume);
                TextView txtEditPSNDNetWeight = FindViewById<TextView>(Resource.Id.txtEditPSNDNetWeight);
                TextView txtEditPSNDQty = FindViewById<TextView>(Resource.Id.txtEditPSNDQty);

                //IMAGE VIEW - PodDBPackingSlipNoDetailsCode
                ImageView ImgSigView = FindViewById<ImageView>(Resource.Id.ImgSigView);
                //ImageView ImgSigView = FindViewById<ImageView>(Resource.Id.ImgSigView);

                //EditText - CUST PACKING SLIP JOUR LINE
                TextView PodJN_ITEM = FindViewById<TextView>(Resource.Id.PodJN_ITEM);
                TextView PodJN_DESC = FindViewById<TextView>(Resource.Id.PodJN_DESC);
                TextView PodJN_UNIT = FindViewById<TextView>(Resource.Id.PodJN_UNIT);
                TextView PodJN_Qty = FindViewById<TextView>(Resource.Id.PodJN_Qty);

                byte[] bitmapData = null;


                PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
                CustPackingSlipJour item = new Models.CustPackingSlipJour();

                item = dbr.GetCustPackingSlipJourByPackingSlipId(PackingSlipId);


                List<CustPackingSlipJourLine> itemJourLine = new List<Models.CustPackingSlipJourLine>();
                itemJourLine = dbr.GetCustPackingSlipJourByPackingSlipLineId(PackingSlipId);


                txtEditPSNDDelName.Text = item.DeliveryName;
                txtEditPSNDDelDes.Text = item.DeliveryDescription;
                txtEditPSNDDelAdd.Text = item.DeliveryAddress;
                txtEditPSNDPostCode.Text = item.DeliveryPostCode;
                txtEditPSNDVolume.Text = item.Volume;
                txtEditPSNDNetWeight.Text = item.NetWeight;
                txtEditPSNDQty.Text = item.NoUnit;
                txtEditPSNDDelpPkSlipNo.Text = item.PackingSlipId;
                txtEditPSNDDelRecId.Text = item.RecId.ToString();

                App._dir = new Java.IO.File(
                 Environment.GetExternalStoragePublicDirectory(
                     Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId + "_Sig");

                var files_Sig = App._dir.ListFiles();

                if (files_Sig != null)
                {
                    foreach (var item_Sig in files_Sig)
                    {
                        if (item_Sig != null)
                        {
                            ImgSigView.SetImageBitmap(item_Sig.Path.LoadAndResizeBitmap(100, 100));
                        }
                    }
                }
                else
                {

                    if (m_ParentActivity != PodCameraScreenCode && m_ParentActivity != PodSignaturePadPanelCode)
                    {

                        CustPackingSlipJour1.SignatureImg = null;

                        //Signature Image Loading
                        //CustPackingSlipJour1.Recid = item.RecId;
                        //CustPackingSlipJour item_1 = new Models.CustPackingSlipJour();
                        // item_1 =
                        //dbr.CustPackingSlipJourRecId(item.RecId);
                        foreach (var item_Sig in files_Sig)
                        {
                            if (item_Sig != null)
                            {
                                ImgSigView.SetImageBitmap(item_Sig.Path.LoadAndResizeBitmap(100, 100));
                            }
                        }
                    }
                }


                //CUST PACKING SLIP LIST VIEW
                ListCustJourLines = FindViewById<ListView>(Resource.Id.ListCustJourLines);

                if (tableJourLine != null)
                {
                    foreach (var itemLine in itemJourLine)
                    {
                        tableJourLine.Add(new CustPackingSlipJourLine() { Description = itemLine.Description, Unit = itemLine.Unit, Qty = itemLine.Qty, Item = itemLine.Item });
                    }
                }
                ListCustJourLines.Adapter = new HomeScreenAdapterLine(this, tableJourLine);

                //STEP - 50

                App._dir = new Java.IO.File(
                   Environment.GetExternalStoragePublicDirectory(
                       Environment.DirectoryPictures), CustPackingSlipJour1.PackingSlipId);

                var files = App._dir.ListFiles();

                if (m_ParentActivity != PodCameraScreenCode &&
                        m_ParentActivity != PodSignaturePadPanelCode)
                {
                    //PACKING SLIP IMAGE DETAIL LIST VIEW

                    //dbr.GetCustPackingImageJourByPackingSlipId(item.RecId);

                    //CHANGE HERE 
                    listView = FindViewById<ListView>(Resource.Id.List);
                    if (files != null)
                    {
                        //DELETE HERE
                        //// DELETE HERE 

                        foreach (var item1 in files)
                        {
                            if (item1 != null)
                            {
                                Intent mediaScanIntent = new Intent(item1.AbsolutePath);
                                Uri contentUri = Uri.FromFile(item1);
                                //App.bitmap = App._file.Path.LoadAndResizeBitmap(100, 100);
                                tableItems.Add(new CustPackingSlipJour2() { Podimage = contentUri });
                                //CustPackingSlipJour1.Podimages.Add(contentUri);
                            }
                        }
                    }
                }
                else
                {
                    tableItems.Clear();
                    foreach (var item1 in files)
                    {
                        if (item1 != null)
                        {
                            Uri contentUri = Uri.FromFile(item1);
                            tableItems.Add(new CustPackingSlipJour2() { Podimage = contentUri });
                        }
                    }
                }
                // listView = FindViewById<ListView>(Resource.Id.List);

                listView = FindViewById<ListView>(Resource.Id.List);
                listView.ItemLongClick += ListView_ItemLongClick;
                listView.Adapter = new HomeScreenAdapter(this, tableItems);


                GC.Collect();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Binding Data Exception Error", ToastLength.Short).Show();
                return;
            }
        }

        /// <summary>
        /// UPDATE/SAVE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPSNDProcessPod_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    PodDb_CustPackingSlipJour dbr = new PodDb_CustPackingSlipJour();
            //    TextView txtEditPSNDDelRecId = FindViewById<TextView>(Resource.Id.txtEditPSNDDelRecId);
            //    TextView txtEditPSNDDelpPkSlipNo = FindViewById<TextView>(Resource.Id.txtEditPSNDDelpPkSlipNo);
            //    TextView txtEditPSNDDelName = FindViewById<TextView>(Resource.Id.txtEditPSNDDelName);
            //    TextView txtEditPSNDDelDes = FindViewById<TextView>(Resource.Id.txtEditPSNDDelDes);
            //    TextView txtEditPSNDDelAdd = FindViewById<TextView>(Resource.Id.txtEditPSNDDelAdd);
            //    TextView txtEditPSNDPostCode = FindViewById<TextView>(Resource.Id.txtEditPSNDPostCode);
            //    TextView txtEditPSNDVolume = FindViewById<TextView>(Resource.Id.txtEditPSNDVolume);
            //    TextView txtEditPSNDNetWeight = FindViewById<TextView>(Resource.Id.txtEditPSNDNetWeight);
            //    TextView txtEditPSNDQty = FindViewById<TextView>(Resource.Id.txtEditPSNDQty);

            //    //Image Binding Code
            //    byte[] signbitmapData = null;
            //    ImageView ImgSigView = FindViewById<ImageView>(Resource.Id.ImgSigView);
            //    if (CustPackingSlipJour1.SignatureImg != null)
            //    {
            //        MemoryStream stream = new MemoryStream();
            //        CustPackingSlipJour1.SignatureImg.Compress(Bitmap.CompressFormat.Png, 0, stream);
            //        signbitmapData = stream.ToArray();
            //        ImgSigView.SetImageBitmap(CustPackingSlipJour1.SignatureImg);
            //    }
            //    else
            //        CustPackingSlipJour1.SignatureImg = null;

            //    List<byte[]> images = new List<byte[]>();
            //    if (CustPackingSlipJour1.Podimages != null)
            //    {
            //        foreach (var item in CustPackingSlipJour1.Podimages)
            //        {

            //            using (MemoryStream stream = new MemoryStream())
            //            {
            //                // item.Compress(Bitmap.CompressFormat.Png, 0, stream);
            //                images.Add(stream.ToArray());
            //            }

            //        }
            //        if (CustPackingSlipJour1.Podimages.Count != 10)
            //        {
            //            for (int i = CustPackingSlipJour1.Podimages.Count; i < 10; i++)
            //            {
            //                images.Add(null);
            //            }
            //        }
            //    }
            //    string result = null;

            //    if (images.Count > 0)
            //    {

            //        result = dbr.UpdateCustPackingSlipJour(int.Parse(txtEditPSNDDelRecId.Text), txtEditPSNDDelpPkSlipNo.Text,
            //                      txtEditPSNDDelName.Text,
            //                      txtEditPSNDDelDes.Text,
            //                      txtEditPSNDDelAdd.Text,
            //                      txtEditPSNDPostCode.Text,
            //                      txtEditPSNDVolume.Text,
            //                      txtEditPSNDNetWeight.Text,
            //                      txtEditPSNDQty.Text,
            //                      signbitmapData,
            //                      images[0],
            //                      images[1],
            //                      images[2],
            //                      images[3],
            //                      images[4],
            //                      images[5],
            //                      images[6],
            //                      images[7],
            //                      images[8],
            //                      images[9]
            //                       );

            //        if (CustPackingSlipJour1.SignatureImg != null)
            //            ImgSigView.SetImageBitmap(CustPackingSlipJour1.SignatureImg);
            //        CustPackingSlipJour1.SignatureImg = null;


            //        Toast.MakeText(this, result, ToastLength.Short).Show();
            //        MethodSettingsOnOff();
            //        /////////////////////////
            //        var ProgDiaglog = ProgressDialog.Show(this, "Please wait..", "Signature is being saved...", true);
            //        new Thread(new ThreadStart(delegate
            //        {
            //            RunOnUiThread(() => { LoadSaveImg(); });
            //            RunOnUiThread(() => { ProgDiaglog.Hide(); });

            //        })).Start();
            //        ///////////////////////////
            //    }


            //}
            //catch (Exception)
            //{
            //    Toast.MakeText(this, "Data Update Record Exception Error", ToastLength.Short).Show();
            //    return;
            //}

            Toast.MakeText(this, "Saved Successfully", ToastLength.Short).Show();


            MethodSettingsOnOff();
            //var ListViewPckSip = new Intent(this, typeof(PodCameraScreenCode));
            //ListViewPckSip.PutExtra("Parent", "PodDBPackingSlipNoDetailsCode");

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

        private void LoadSaveImg()
        {
            Toast.MakeText(this, "Loading ...", ToastLength.Short).Show();
        }

        private void InitializeUI()
        {

        }
    }
}