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

namespace PODApp.ViewModels
{
    public  class ListDataItem
    {
        public static long DataHash;
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public ListDataItem(string heading, string subHeading)
        {
            Title = heading;
            SubTitle = subHeading;
            Id = DataHash++;
        }
    }
}

