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
using SQLite;

namespace PODApp.Models
{
    class LoginUsers
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int DeviceId { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(25)]

        public string UserName { get; set; }

        [MaxLength(15)]

        public string Password { get; set; }

        [MaxLength(25)]

        public string Type { get; set; }
    }

   public class LoginUsers_tmp
    {
        public static int DeviceId { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(25)]
        public static string UserName { get; set; }

        [MaxLength(15)]
        public static string Password { get; set; }

        [MaxLength(25)]
        public static string Type { get; set; }
    }
}