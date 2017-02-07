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

namespace PODApp.ViewModels
{
    [Activity(Label = "ListVWActivity")]
    public class ListVWActivity : Activity
    {
        private string[] _countries;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListDBVWActivity);

            _countries = Resources.GetStringArray(Resource.Array.countries);
            Array.Sort(_countries);

            var lv = FindViewById<ListView>(Resource.Id.PodExlistView);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1,
                                                   Android.Resource.Id.Text1, _countries);
            lv.Adapter = adapter;
            RegisterForContextMenu(lv);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.PodExlistView)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle(_countries[info.Position]);
                var menuItems = Resources.GetStringArray(Resource.Array.menu);
                for (var i = 0; i < menuItems.Length; i++)
                    menu.Add(Menu.None, i, i, menuItems[i]);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            var menuItems = Resources.GetStringArray(Resource.Array.menu);
            var menuItemName = menuItems[menuItemIndex];
            var listItemName = _countries[info.Position];

            Toast.MakeText(this, string.Format("Selected {0} for item {1}", menuItemName, listItemName), ToastLength.Short).Show();
            return true;
        }
    }
}