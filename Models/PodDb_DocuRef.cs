using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    public class PodDb_DocuRef
    {
        /// <summary>
        /// CREATE DATABASE 
        /// </summary>
        /// <returns></returns>
        /// 
        public String CreatePodDb_PodDbDocuRef()
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
        public String TableCreatePodDb_PodDbDocuRef()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DocuRef");
                var db = new SQLiteConnection(dbPath);

                db.CreateTable<DocuRef>();
                string result = "DocuRef Table has Created successfully..";
                return result;

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        /// <summary>
        /// INSERT RECORD
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string InsertPodDbDocuRef(string task)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DocuRef");
                var db = new SQLiteConnection(dbPath);

                DocuRef item = new DocuRef();
                item.PackingSlipId = task;
                item.Location = task;

                db.Insert(item);
                return "RECORD ADDED TO DocuRef";
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
        //public string RetrivePodDbDocuRef()
        public TableQuery<DocuRef> RetrivePodDbDocuRef()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DocuRef");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            output += "Retriving  the data from  DocRef";

            var table = db.Table<DocuRef>();
            /*   foreach (var item in table)
             {
                 output += "\n"
                 + item.RecId
                 + item.PackingSlipId
                 + item.Location;
             }*/
            // return output;

            return table;
        }


        public DocuRef RetriveDocuRefId()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DocuRef");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            output += "Retriving  to the data using LogisticsLocation.";

            TableQuery<DocuRef> table = db.Table<DocuRef>();

            DocuRef location = table.FirstOrDefault();

            return location;
        }



        public string DeletePodDbDocuRef(int RecId)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DocuRef");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<DocuRef>(RecId);
            db.Delete(item);
            return "DocuRef Record Deleted";
        }
    }
}

