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

namespace PODApp.mCode.mDataBase
{
    class Constants
    {
        //COLUMS
        public static string ROW_ID = "id";
        public static string NAME = "name";

        //DB PROPERTIES 
        //DATABASE NAME -  DB_NAME
        //TABLE NAME    -  TB_NAME
        // DATABASE NAME 
        public static string DB_NAME = "d_DB";
        public static string TB_NAME = "d_TB";
        public static int DB_VERSION = 1;

        //CREATE TABLE
        public static String CREATE_TB = "CREATE TABLE d_TB(id INTEGER PRIMARY KEY AUTOINCREMENT,"
            + "name TEXT NOT NULL);";

        //DROP TABLE 
        public static String DROP_TB = "DROP TABLE IF EXISTS" + TB_NAME;
    }
}