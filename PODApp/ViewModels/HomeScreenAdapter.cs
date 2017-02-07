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
using PODApp.ViewModels;
using PODApp.Controls;

namespace PODApp.ViewModels
{
    public class HomeScreenAdapter : BaseAdapter<CustPackingSlipJour2>
    {
        List<CustPackingSlipJour2> items;
        Activity context;
        public HomeScreenAdapter(Activity context, List<CustPackingSlipJour2> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override CustPackingSlipJour2 this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            int height = 0;
            int width = 0;
            if (AccountSetting.PodPicReson == 0)
            {
                height = 365;
                width = 242;
            }
            else if (AccountSetting.PodPicReson == 1)
            {
                height = 500;
                width = 500;
            }
            else
            {
                height = 960;
                width = 644;
            }

           view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(item.Podimage.Path.LoadAndResizeBitmap(height,width));

            return view;
        }
    }

    public class HomeScreenAdapterLine : BaseAdapter<CustPackingSlipJourLine>
    {
        List<CustPackingSlipJourLine> items;
        Activity context;
        public HomeScreenAdapterLine(Activity context, List<CustPackingSlipJourLine> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override CustPackingSlipJourLine this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomJourLine, null);
            view.FindViewById<TextView>(Resource.Id.PodJN_ITEM).Text = (item.Item);
            view.FindViewById<TextView>(Resource.Id.PodJN_DESC).Text = (item.Description);
            view.FindViewById<TextView>(Resource.Id.PodJN_UNIT).Text = (item.Unit);
            view.FindViewById<TextView>(Resource.Id.PodJN_Qty).Text = (item.Qty);

            return view;

        }
    }

    //Packing Slip List
    public class PackingSlipAdapter : BaseAdapter<CustPackingSlipJour>
    {
        List<CustPackingSlipJour> items;
        Activity context;
        public PackingSlipAdapter(Activity context, List<CustPackingSlipJour> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override CustPackingSlipJour this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
            view = context.LayoutInflater.Inflate(Resource.Layout.PackSlipListView, null);
            view.FindViewById<TextView>(Resource.Id.PKLVPod_PakID).Text = (item.PackingSlipId);
            view.FindViewById<TextView>(Resource.Id.PKLVPod_PostCode).Text = (item.DeliveryPostCode);
         
            // Do i need to Display Image as delplay View Design 
            //view.FindViewById<ImageView>(Resource.Id.PKLVPod_ImDrV);

            //view.FindViewById<TextView>(Resource.Id.text2).LayoutParameters.Width = 100;
            //view.FindViewById<TextView>(Resource.Id.text2).Text = "kajan";
            //view.FindViewById<TextView>(Resource.Id.text2).LayoutParameters.Width = 100;
            //view.FindViewById<TextView>(Resource.Id.text2).Text = "kajan";
            return view;




        }
    }

}