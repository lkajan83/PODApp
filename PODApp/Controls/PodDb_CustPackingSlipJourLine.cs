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
    public class PodDb_CustPackingSlipJourLine
    {
        /// <summary>
        /// CREATE DDATABASE 
        /// </summary>
        /// <returns></returns>
        public String DB_CustPackingSlipJourLine()
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

        /// <summary>
        /// CREATE TABLE
        /// </summary>
        /// <returns></returns>
        public String CT_CustPackingSlipJourLine()
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJourLine");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<CustPackingSlipJourLine>();
                string result = "CustPackingSlipJour Table has created ..";
                return result;

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }

        /// <summary>
        /// INSERT THE RECORDS
        /// </summary>
        /// <param name="PackingSlipId"></param>
        /// <param name="Item"></param>
        /// <param name="Description"></param>
        /// <param name="Unit"></param>
        /// <param name="Qty"></param>
        /// <returns></returns>
        public string IR_CustPackingSlipJourLine(string PackingSlipId, string Status, DateTime Date, string Item, string Description, string Unit, string Qty)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJourLine");
                var db = new SQLiteConnection(dbPath);

                CustPackingSlipJourLine item = new CustPackingSlipJourLine();
                item.Status = Status;
                item.Date = Date;
                item.PackingSlipId = PackingSlipId;
                item.Item = Item;
                item.Description = Description;
                item.Unit = Unit;
                item.Qty = Qty;       

                db.Insert(item);
                return "RECORD ADDED TO CustPackingSlipJour";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }


        public TableQuery<CustPackingSlipJourLine> RR_CustPackingSlipJourLine()
        {
            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJourLine");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retriving  the data from  CustPackingSlipJourLine";

            var table = db.Table<CustPackingSlipJourLine>();

            return table;
        }
    }
}