using System;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using PODApp.Models;
using PODApp.Controls;

using PODApp.ViewModels;
using Android.Content.PM;

namespace PODApp.ViewModels
{
    [Activity(Label = "PODApp", Icon = "@drawable/ArrowBack", Theme = "@style/Theme.DesignDemo", ScreenOrientation = ScreenOrientation.Portrait)]
    public class TabHostOneCode : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TabHostOne);
            AccountSetting.LoadSetting();

            //CreateTab(typeof(AdministratorRegisterCode), "REGISTER", "REGISTER", Resource.Drawable.ic_tab_sessions);
            //CreateTab(typeof(AdministratorLoginCode), "ADMINSTRATOR", "ADMINSTRATOR", Resource.Drawable.ic_tab_my_schedule);
        }

        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            try
            {
                var intent = new Intent(this, activityType);
                intent.AddFlags(ActivityFlags.NewTask);

                var spec = TabHost.NewTabSpec(tag);
                var drawableIcon = Resources.GetDrawable(drawableId);
                spec.SetIndicator(label, drawableIcon);
                spec.SetContent(intent);

                TabHost.AddTab(spec);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "CreateTab Exception Error", ToastLength.Short).Show();
                return;
            }
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
    }
}

