using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    /// <summary>
    /// CREATE DATABASE 
    /// </summary>
    /// <returns></returns>
    public class PodDb_LogisticsLocation
    {
       public string DbCreatePodDb_LogisticsLocation()
        {
            try
            {
                var output = "";
                output += "Creating Database if it does exit.";

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PODApp");
                var db = new SQLiteConnection(dbPath);

                output += "\nDatabase Created ..";
                return output;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        /// <summary>
        /// CREATE TABLE 
        /// </summary>
        /// <returns></returns>
        public string CreateTablePodDb_LogisticsLocation()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticsLocation");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<LogisticsLocation>();
                string result = "LogisticsLocation Table has Created successfully..";
                return result;

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }


        /// <summary>
        /// INSERT INTO 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string InsertLogisticsLocation(string task)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticsLocation");
                var db = new SQLiteConnection(dbPath);

                LogisticsLocation item = new LogisticsLocation();
                item.Location = task;
                item.Description = task;

                db.Insert(item);
                return "RECORD ADDED TO LogisticsLocation";


            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }

        /// <summary>
        /// RETRIVE RECORDS
        /// </summary>
        /// <returns></returns>
        public TableQuery<LogisticsLocation> RetriveLogisticsLocation()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticsLocation");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            output += "Retriving  to the data using LogisticsLocation.";

            var table = db.Table<LogisticsLocation>();
          /*  foreach (var item in table)
            {
                output += "\n"
                + item.RecId
                + item.Location
                + item.Description;
            }*/

            return table;
        }
        public LogisticsLocation RetriveLogisticsLocationId()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticsLocation");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            output += "Retriving  to the data using LogisticsLocation.";

            TableQuery<LogisticsLocation> table = db.Table<LogisticsLocation>();

            LogisticsLocation location = table.FirstOrDefault();
            
            return location;
        }
        /// <summary>
        /// DELETE RECORED BY RECID
        /// </summary>
        /// <param name="RecId"></param>
        /// <returns></returns>
        public string DeleteLogisticsLocation(int RecId)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticsLocation");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<LogisticsLocation>(RecId);
            db.Delete(item);
            return "LogisticsLocation Recored Deleted";
        }

    }
}

