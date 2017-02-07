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
using PODApp.Models;
using PODApp.Controls;
using PODApp.ViewModels;

namespace PODApp.ViewModels
{
    public class ReorderListViewAdapter : BaseAdapter, IReorderListAdapter
    {
        //private readonly List _items;
        //private readonly Activity _context;
        public static List<CustPackingSlipJour> Items { get; set; }
        public int mMobileCellPosition { get; set; }

        Activity context;
        public ReorderListViewAdapter(Activity context, List<CustPackingSlipJour> items) : base()
        {
            Items = items;
            this.context = context;
            mMobileCellPosition = int.MinValue;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
             return position;
           //change
            //if (position < 0 || position >= Items.Count)
            //{
            //    return -1;
            //}
            //var item = GetItem(position).Cast();
            //return item.Id;
        }

       

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var item = Items[position];

            View view = convertView;
            view = context.LayoutInflater.Inflate(Resource.Layout.PackSlipListView, null);
            var text1 = view.FindViewById<TextView>(Resource.Id.PKLVPod_PakID);
            var text2 = view.FindViewById<TextView>(Resource.Id.PKLVPod_PostCode);
            string[] str = Items[position].ToString().Split(' ');
            if (text1 != null)
            {
                text1.Text = item.PackingSlipId;
                    //str[0];
            }
            if (text2 != null)
            {
                text2.Text = item.DeliveryPostCode;
                    //str[2];
            }           

            view.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
            view.TranslationY = 0;
            return view;
        }

        public override int Count
        {
            get
            {
                return Items.Count;
            }
        }

        //public override ListDataItem this[int position]
        //{
        //    get { return _items[position]; }
        //}        
        //public override long GetItemId(int position)
        //{
        //    if (position < 0 || position >= _items.Count)
        //    {
        //        return -1;
        //    }
        //    var item = GetItem(position).Cast();
        //    return item.Id;
        //}

        //public override bool HasStableIds
        //{
        //    get { return true; }
        //}

        //#region --[IReorderListAdapter]--

        public void SwapItems(int index1, int index2)
        {
            var temp = Items[index1];
            Items[index1] = Items[index2];
            Items[index2] = temp;
            NotifyDataSetChanged();
            AccountSetting.podsortdraggableList = Items;
        }

        public bool CanMove(int index)
        {
            return true;
        }
       
    }

    //public static class ObjectTypeHelper
    //{
    //    public static MainViewModel Cast(this JavaList.Lang.Object obj) where MainViewModel : class
    //    {
    //        var propertyInfo = obj.GetType().GetProperty("Instance");
    //        return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
    //    }
    //}

}