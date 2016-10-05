using System;
using System.Data;
using System.IO;
using SQLite;
using System.Collections.Generic;

namespace PODApp.Models
{
    /// <summary>
    /// Database controller for CustPackingSlipJour 
    /// </summary>
    public class PodDb_CustPackingSlipJour
    {
        /// <summary>
        /// CREATE DATABASE 
        /// </summary>
        /// <returns></returns>
        public String CreatePodDb_CustPackingSlipJour()
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
        /// CREATE TABLE - CustPackingSlipJour
        /// </summary>
        /// <returns></returns>
        public String TableCreatePodDb_CustPackingSlipJour()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<CustPackingSlipJour>();
                string result = "CustPackingSlipJour Table has created ..";
                return result;

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        // End of the code 
        /// <summary>
        /// INSERT INTO - CustPackingSlipJour
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string InsertCustPackingSlipJour(string DeviceId, string PackingSlipId, string qty, string volume, string weight, string deliveryName)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                CustPackingSlipJour item = new CustPackingSlipJour();               
                item.DeviceId = DeviceId;
                item.PackingSlipId = PackingSlipId;
                item.Qty = qty;
                item.Volume = volume;
                item.Weight = weight;
                item.DeliveryName = deliveryName;

                db.Insert(item);
                return "RECORD ADDED TO CustPackingSlipJour";


            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
        // END OF THE CODE

        /// <summary>
        /// RETRIEVE RECORD
        /// </summary>
        /// <returns></returns>
        //public string ViewCustPackingSlipJour()
        //{
        //    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
        //    var db = new SQLiteConnection(dbPath);
        //    string output = "";
        //    output += "Retriving  to the data using CustPackingSlipJour.";

        //    var table = db.Table<CustPackingSlipJour>();
        //    foreach (var item in table)
        //    {
        //        output += "\n"
        //        + item.RecId
        //        + item.DeviceId
        //        + item.PackingSlipId
        //        + item.Qty
        //        + item.Volume
        //        + item.Weight
        //        + item.DeliveryName;
        //    }
        //    return output;
        //}


        public List<CustPackingSlipJour> ViewCustPackingSlipJour(string searchTerm)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);
            
            string output = "";
            output += "Retriving  to the data using LogisticsLocation.";

            string sql = "SELECT * FROM CustPackingSlipJour WHERE PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            SQLiteCommand command = db.CreateCommand(sql);
           List<CustPackingSlipJour> packingSlips = command.ExecuteQuery<CustPackingSlipJour>();

            var table = db.Table<CustPackingSlipJour>();
            /*  foreach (var item in table)
              {
                  output += "\n"
                  + item.RecId
                  + item.Location
                  + item.Description;
              }*/

            return packingSlips;
        }

        /// <summary>
        /// DELETE RECORD
        /// </summary>
        /// <param name="RecId"></param>
        /// <returns></returns>
        public string DeleteCustPackingSlipJour(int RecId)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<CustPackingSlipJour>(RecId);
            db.Delete(item);
            return "CustPackingSlipJour Recored Deleted";
        }

    }
}





