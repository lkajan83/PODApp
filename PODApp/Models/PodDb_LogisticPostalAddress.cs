using System;
using System.Data;
using System.IO;
using SQLite;


namespace PODApp.Models
{
    /// <summary>
    /// CREATE TABLE 
    /// </summary>
    /// <returns></returns>
    public class PodDb_LogisticPostalAddress
    {
        /// <summary>
        /// CREATE DATABASE 
        /// </summary>
        /// <returns></returns>
        public String CreatePodDb_LogisticPostalAddress()
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
        //End of the code 
        /// <summary>
        /// CREATE TABLE
        /// </summary>
        /// <returns></returns>
        public String TableCreatePodDb_LogisticPostalAddress()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticPostalAddress");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<LogisticPostalAddress>();
                string result = "LogisticPostalAddress Table has created ..";
                return result;

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        // END OF LogisticPostalAddress CODE

        /// <summary>
        /// INSERT RECORD
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string InsertLogisticPostalAddress(string task)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticPostalAddress");
                var db = new SQLiteConnection(dbPath);

                LogisticPostalAddress item = new LogisticPostalAddress();
                item.Address = task;
                item.ZipCode = task;
                item.Location = task;

                db.Insert(item);
                return "RECORD ADDED TO LogisticPostalAddress";


            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
        // END OF THE CODE
        /// <summary>
        /// RETRIVE RECORDS
        /// </summary>
        /// <returns></returns>
       public TableQuery<LogisticPostalAddress> RetriveLogisticPostalAddress()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticPostalAddress");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            output += "Retriving  to the data using LogisticPostalAddress.";

            var table = db.Table<LogisticPostalAddress>();
          
            return table;
        }

        //import to map with retrieve       

        public LogisticPostalAddress RetriveLogisticPostalAddressId()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticPostalAddress");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            output += "Retriving  to the data using LogisticPostalAddress.";

            TableQuery<LogisticPostalAddress> table = db.Table<LogisticPostalAddress>();

            LogisticPostalAddress location = table.FirstOrDefault();

            return location;
        }


        /// <summary>
        /// DELETE RECORD  RetriveLogisticPostalAddress
        /// </summary>
        /// <param name="RecId"></param>
        /// <returns></returns>

        public string DeleteLogisticPostalAddress (int RecId)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LogisticPostalAddress");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<LogisticPostalAddress>(RecId);
            db.Delete(item);
            return "LogisticPostalAddress Recored Deleted";
        }
    }
}

