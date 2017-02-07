using System;
using System.Data;
using System.IO;
using SQLite;
using SQLite.Extensions;
using System.Collections.Generic;
using Android.Media;
using Android.Database;
using Mono.Data.Sqlite;
using Android.Graphics;
using PODApp.Models;

namespace PODApp.Controls
{
    public class DBA_LoginUsers
    {
        public String PODApp_SysDBUsers()
        {
            try
            {
                var output = "";
                output += "Creating Database if it does exit.";

                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PODApp");
                var db = new SQLiteConnection(dbPath);

                output += "\nDatabase Created ..";
                return output;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }

        public string PODApp_CtUsers()

        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LoginUsers");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<LoginUsers>();
                string result = "User Table has Created";
                return result;
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }

        public string PODApp_IrUsers(string UserName, string Password)
        {
            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LoginUsers");
            var db = new SQLiteConnection(dbPath);

            LoginUsers item = new LoginUsers();
            item.UserName = UserName;
            item.Password = Password;

            db.Insert(item);
            return "RECORD ADDED TO USER LOGIN";
        }

        //public TableQuery<LoginUsers>RRPODAppRBUsersRrt()
        //{
        //    string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LoginUsers");
        //    var db = new SQLiteConnection(dbPath);
        //    string output = "";
        //    output += "Retriving  the data from  LoginUsers";
        //    var table = db.Table<LoginUsers>();
        //    return table;
        //}
        //public List<LoginUsers> RRPODAppRBUsers()
        //{
        //    string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LoginUsers");
        //    var db = new SQLiteConnection(dbPath);
        //    string output = "";
        //    string sql = string.Empty;
        //    output += "Retriving  the data from  LoginUsers";
        //    sql = "SELECT distinct * FROM LoginUsers ";
        //    SQLiteCommand command = db.CreateCommand(sql);
        //    List<LoginUsers> ReptLoginUsers = command.ExecuteQuery<LoginUsers>();
        //    return ReptLoginUsers;
        //}
    }
}