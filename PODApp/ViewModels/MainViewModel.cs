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


using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

using Android.Webkit;

using System.Threading;
using PODApp.Models;

using PODApp.ViewModels;
using System.IO;
using SQLite;
using System.Collections.Generic;
using Android.Graphics;
using Java.Util;
using System.Collections;
using PODApp.Models;

using PODApp.ViewModels;


namespace PODApp.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            CreateData();
        }

       // public List Data { get; set; } I have cheange to javaList
        public JavaList Data { get; set; }

        private void CreateData()
        {
            //Data = new List(); change to JavaList();
            Data = new JavaList();

            Data.Add(new ListDataItem("Beet greens", "1 item"));
            Data.Add(new ListDataItem("Broccoli Rabe", "2 item"));
            Data.Add(new ListDataItem("Cabbage", "3 item"));
            Data.Add(new ListDataItem("Corn salad", "4 item"));
            Data.Add(new ListDataItem("Mustard", "5 item"));
            Data.Add(new ListDataItem("Spinach", "6 items"));
            Data.Add(new ListDataItem("Avocado", "7 items"));
            Data.Add(new ListDataItem("Ivy Gourd", "8 items"));
            Data.Add(new ListDataItem("Tomato", "9 items"));
            Data.Add(new ListDataItem("Zucchini", "10 items"));
            Data.Add(new ListDataItem("Buds,Artichoke", "11 items"));
            Data.Add(new ListDataItem("Buds,Broccoli", "12 items"));
            Data.Add(new ListDataItem("Buds,Cauliflower", "13 items"));
        }
    }
}