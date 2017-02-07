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

using PODApp.Controls;
using PODApp.Bootstrap;
using PODApp.Models;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "Nav_FooterCode", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Nav_FooterCode : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Load Nav_Footer Layer 
            SetContentView(Resource.Layout.Nav_Footer);
            
            //Declare Image SETTING
            ImageView PodFd_Sng = FindViewById<ImageView>(Resource.Id.PodFd_Sng);
            PodFd_Sng.Click += PodFd_Sng_Click;


            //Declare Image HOME
            ImageView PodFd_Hme = FindViewById<ImageView>(Resource.Id.PodFd_Hme);
            PodFd_Hme.Click += PodFd_Hme_Click;

            //Declare Image SYN
            ImageView PodFd_Syn = FindViewById<ImageView>(Resource.Id.PodFd_Syn);
            PodFd_Syn.Click += PodFd_Syn_Click;
        }
        /// <summary>
        /// SETTING
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodFd_Sng_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PodSettingsCode));
        }

        /// <summary>
        /// HOME PAGE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodFd_Hme_Click(object sender, EventArgs e)
        {
            MethodPODNavCollapse();
            MethodPODNavManualSrt();
            MethodPODNavPictuResn();
        }

        private void MethodPODNavCollapse()
        {
            try
            {
                // if (m_ParentActivity != PodNavManSet == true)
                if (AccountSetting.podcollapse)
                {
                    var PckSlipNavManSrt = new Intent(this, typeof(PodPackingListFilterCollapseCode));
                   AccountSetting.podcollapse = true;
                    PckSlipNavManSrt.PutExtra("Parent", "PodDBPackingSlipNoDetailsCode");
                    StartActivity(PckSlipNavManSrt);
                }
                else
                {
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    var PckSlipNavManSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                   AccountSetting.podcollapse = false;
                    PckSlipNavManSrt.PutExtra("Parent", "PodDBPackingSlipNoDetailsCode");
                    StartActivity(PckSlipNavManSrt);
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Create Database", ToastLength.Short).Show();
                return;
            }

        }
        private void MethodPODNavPictuResn()
        {
            try
            {
                //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
                //if (AccountSetting.PodPicReson && spinner.Count > 0)
                //{
                //    AccountSetting.PodPicReson = true;
                //}
                //else
                //{
                //    AccountSetting.PodPicReson = false;
                //}
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Picture Resolution Error Handle", ToastLength.Short).Show();
                return;
            }
        }
        private void MethodPODNavManualSrt()
        {
            try
            {

                if (AccountSetting.podmodelsort)
                {
                    var NavManualSrt = new Intent(this, typeof(PodDraggableListView));
                    Toast.MakeText(this, "checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = true;
                    NavManualSrt.PutExtra("Parent", "PodDraggableListView");
                    StartActivity(NavManualSrt);
                }
                else
                {
                    var NavManualSrt = new Intent(this, typeof(PodPackingListFilterListCode));
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
                    AccountSetting.podmodelsort = false;
                    NavManualSrt.PutExtra("Parent", "Nav_FooterCode");
                    StartActivity(NavManualSrt);
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Create Database", ToastLength.Short).Show();
                return;
            }

        }
        /// <summary>
        /// syn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PodFd_Syn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SynchronizeCode));
        }
    }
}