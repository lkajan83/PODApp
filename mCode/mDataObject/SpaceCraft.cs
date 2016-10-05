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

namespace PODApp.mCode.mDataObject
{
    class SpaceCraft
    {
        private string name;
        private int id;
        public SpaceCraft()
        {
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //end 
        public override string ToString()
        {
            return name;
        }




    }
}