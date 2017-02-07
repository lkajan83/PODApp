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
using PODApp.Bootstrap;

namespace PODApp.Controls
{
    public class DBA_PackingSlipAttachment 
    {
        /// <summary>
        /// CREATE DATABASE 
        /// </summary>
        /// <returns></returns>
        public String DB_PackingSlipAttachment()
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
        public String CT_PackingSlipAttachment()
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PackingSlipAttachment");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<PackingSlipAttachment>();
                string result = "CustPackingSlipJour Table has created ..";
                return result;

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }

        //creating
        public string IR_PackingSlipAttachment(string PackingSlipId, bool Attachment, bool Signature)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PackingSlipAttachment");
                var db = new SQLiteConnection(dbPath);

                PackingSlipAttachment item = new PackingSlipAttachment();
                item.PackingSlipId = PackingSlipId;
                item.Attachment = Attachment;
                item.Attachment = Signature;

                db.Insert(item);
                return "RECORD ADDED TO CustPackingSlipJour";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
        public string UpdatePackingSlipAttachment_Attachment(string PackingSlipId, bool Attachment)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PackingSlipAttachment");
                var db = new SQLiteConnection(dbPath);

                var item = db.Get<PackingSlipAttachment>(PackingSlipId);
                item.Attachment = Attachment;
                db.Update(item);

                return "Record Updated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdatePackingSlipAttachment_Signature(string PackingSlipId, bool Signature)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PackingSlipAttachment");
                var db = new SQLiteConnection(dbPath);

                var item = db.Get<PackingSlipAttachment>(PackingSlipId);
                item.Signature = Signature;
                db.Update(item);

                return "Record Updated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //RETRIVE  THE DATA 
    }
}