using System;
using System.Data;
using System.IO;
using SQLite;
using System.Collections.Generic;
using Android.Media;

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

        /// <summary>
        /// INSERT INTO - CustPackingSlipJour
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string InsertCustPackingSlipJour(string PackingSlipId, string DeliveryName, string DeliveryDescription, string DeliveryAddress, string DeliveryPostCode, string Volume, string NetWeight, string NoUnit)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                CustPackingSlipJour item = new CustPackingSlipJour();
                item.PackingSlipId = PackingSlipId;
                item.DeliveryName = DeliveryName;
                item.DeliveryDescription = DeliveryDescription;
                item.DeliveryAddress = DeliveryAddress;
                item.DeliveryPostCode = DeliveryPostCode;
                item.Volume = Volume;
                item.NetWeight = NetWeight;
                item.NoUnit = NoUnit;
                //item.Image = Image;                

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
        /// SEARCH 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CustPackingSlipJourrrRecId(int RecId)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);

            var item = db.Get<CustPackingSlipJour>(RecId);
            return item.DeliveryAddress;
             // i need to view PackingSlipId and DeliveryAddress etc... so How can i display as i display 
             // return item.PackingSlipId + item.DeliveryAddress + item.DeliveryDescription + item.DeliveryName + item.DeliveryPostCode + item.Volume + item.NetWeight + item.NoUnit;

        }

        /// <summary>
        /// UPDATE 
        /// </summary>
        /// <param name="RecId"></param>
        /// <param name="PackingSlipId"></param>
        /// <returns></returns>
        public string UpdateCustPackingSlipJour(int RecId, string PackingSlipId)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<CustPackingSlipJour>(RecId);
            item.PackingSlipId = PackingSlipId;
            db.Update(PackingSlipId);
            return "Record Updated";

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