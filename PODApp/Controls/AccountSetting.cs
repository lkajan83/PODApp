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

namespace PODApp.Controls
{
    public static class AccountSetting
    {
        public static string Name { get { return "SettingsPOD"; } }       
 
        public static bool  podcollapse { get; set; }

        public static bool podmodelsort { get; set; }

        public static int PodPicReson { get; set; }

        public static List<string> PodListViewList { get; set; }

        public static List<PODApp.Models.CustPackingSlipJour> podsortdraggableList { get; set; }

        public static object CusCollapse { get; internal set; }

        public static void SaveSetting()
        {
            var PodSetSave = Application.Context.GetSharedPreferences(Name, FileCreationMode.Private);
            var PodSetSaveEdit = PodSetSave.Edit();
            PodSetSaveEdit.PutString("podcollapse", podcollapse.ToString());
            PodSetSaveEdit.PutString("podmodelsort", podmodelsort.ToString());
            PodSetSaveEdit.PutString("PodPicReson", PodPicReson.ToString());
            //PodSetSaveEdit.PutInt("PodPicReson", PodPicReson.ToString());
            PodSetSaveEdit.Commit();
        }

        public static void LoadSetting()
        {
            var PodSetLoad = Application.Context.GetSharedPreferences(Name, FileCreationMode.Private);
            if (PodSetLoad != null)
            {
                string podcoll = PodSetLoad.GetString("podcollapse", "false");
                podcollapse = bool.Parse(podcoll);

                String podmodell = PodSetLoad.GetString("podmodelsort", "false");
                podmodelsort = bool.Parse(podmodell);

                String PodPicRen = PodSetLoad.GetString("PodPicReson", "false");
                //PodPicReson = int.Parse(PodPicRen);

                //String PodPicRen = PodSetLoad.GetString("PodPicReson", AccountSetting.PodPicReson.ToString());
                //PodPicReson = int.Parse(PodPicRen);
            }
        }

    }
}