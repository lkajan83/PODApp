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
using PODApp.Controls;
using PODApp.Bootstrap;

namespace PODApp.Controls
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
        //End of the code 
        /// <summary>
        /// CREATE TABLE - CustPackingSlipJour
        /// </summary>
        /// <returns></returns>
        public String TableCreatePodDb_CustPackingSlipJour()
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
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
        // public string InsertCustPackingSlipJour(string task)
        /// <summary>
        /// INSERT INTO - CustPackingSlipJour
        /// </summary>       public string InsertCustPackingSlipJour(string PackingSlipId, string DeliveryName, string DeliveryDescription, string DeliveryAddress, string DeliveryPostCode, string Volume, string NetWeight, string NoUnit,byte Podimage)
        /// <param name="task"></param>
        /// <returns></returns>
        public string InsertCustPackingSlipJour(string PackingSlipId,int DeviceId,string  Status, DateTime Date, string DeliveryName, string DeliveryDescription, string DeliveryAddress, string DeliveryPostCode, string Volume, string NetWeight, string NoUnit)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                CustPackingSlipJour item = new CustPackingSlipJour();
                item.PackingSlipId = PackingSlipId;
                item.DeviceId = DeviceId;
                item.Status = Status;
                item.Date = Date;
                item.DeliveryName = DeliveryName;
                item.DeliveryDescription = DeliveryDescription;
                item.DeliveryAddress = DeliveryAddress;
                item.DeliveryPostCode = DeliveryPostCode;
                item.Volume = Volume;
                item.NetWeight = NetWeight;
                item.NoUnit = NoUnit;

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
        /// EXPANDABE SEARCH RECORD only 
        /// </summary>
        /// <returns></returns>

        public List<CustPackingSlipJour> ExpandSrchCustPackingSlipJour(string searchTerm)
        {
            string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers"); //Call Database  
            var db1 = new SQLiteConnection(dpPath);
            var data = db1.Table<LoginUsers>(); //Call Table  
            var data1 = data.Where(x => x.UserName == LoginUsers_tmp.UserName && x.Password == LoginUsers_tmp.Password).FirstOrDefault(); //Linq Query

            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            string sql = string.Empty;
            output += "Retriving  to the data using CustPackingSlipJour";

            if (AccountSetting.podmodelsort)
            {
                if (data1.UserName == "lkajan@gmail.com")
                    sql = "SELECT * FROM CustPackingSlipJour WHERE DeliveryName" + " LIKE '%" + searchTerm + "%'";
                else
                    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = " + data1.DeviceId + " AND DeliveryName" + " LIKE '%" + searchTerm + "%'";
            }
            else
            {
                if (data1.UserName == "lkajan@gmail.com")
                    sql = "SELECT * FROM CustPackingSlipJour WHERE DeliveryName" + " LIKE '%" + searchTerm + "%'";
                else
                    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = " + data1.DeviceId + " AND DeliveryName" + " LIKE '%" + searchTerm + "%'";
            }


            //if (AccountSetting.podmodelsort)

            //    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = " + data1.DeviceId + " AND PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            //else
            //if (data1.UserName == "lkajan@gmail.com")
            //    sql = "SELECT * FROM CustPackingSlipJour WHERE PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            //else               
            //    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = "+ data1.DeviceId + " AND PackingSlipId" + " LIKE '%" + searchTerm + "%'";

            SQLiteCommand command = db.CreateCommand(sql);
            List<CustPackingSlipJour> packingSlips = command.ExecuteQuery<CustPackingSlipJour>();

            var table = db.Table<CustPackingSlipJour>();
            return packingSlips;
        }


        // END OF THE CODE
        /// <summary>
        /// RETRIEVE RECORD
        /// </summary>
        /// <returns></returns>

        public List<CustPackingSlipJour> ViewCustPackingSlipJour(string searchTerm)
        {
            string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers"); //Call Database  
            var db1 = new SQLiteConnection(dpPath);
            var data = db1.Table<LoginUsers>(); //Call Table  
            var data1 = data.Where(x => x.UserName == LoginUsers_tmp.UserName && x.Password == LoginUsers_tmp.Password).FirstOrDefault(); //Linq Query

            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            string sql = string.Empty;
            output += "Retriving  to the data using CustPackingSlipJour";

            if (AccountSetting.podmodelsort)
            {
                if (data1.UserName == "lkajan@gmail.com")
                    sql = "SELECT * FROM CustPackingSlipJour WHERE PackingSlipId" + " LIKE '%" + searchTerm + "%'";
                else
                    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = " + data1.DeviceId + " AND PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            }
            else
            {
                if (data1.UserName == "lkajan@gmail.com")
                    sql = "SELECT * FROM CustPackingSlipJour WHERE PackingSlipId" + " LIKE '%" + searchTerm + "%'";
                else
                    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = " + data1.DeviceId + " AND PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            }
              

            //if (AccountSetting.podmodelsort)

            //    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = " + data1.DeviceId + " AND PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            //else
            //if (data1.UserName == "lkajan@gmail.com")
            //    sql = "SELECT * FROM CustPackingSlipJour WHERE PackingSlipId" + " LIKE '%" + searchTerm + "%'";
            //else               
            //    sql = "SELECT distinct  * FROM CustPackingSlipJour WHERE  DeviceId = "+ data1.DeviceId + " AND PackingSlipId" + " LIKE '%" + searchTerm + "%'";

            SQLiteCommand command = db.CreateCommand(sql);
            List<CustPackingSlipJour> packingSlips = command.ExecuteQuery<CustPackingSlipJour>();

            var table = db.Table<CustPackingSlipJour>();
            return packingSlips;
        }


        public List<CustPackingSlipJour> GetAllCustPackingSlipJour()
        {
            string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers"); //Call Database  
            var db1 = new SQLiteConnection(dpPath);
            var data = db1.Table<LoginUsers>(); //Call Table  
            var data1 = data.Where(x => x.UserName == LoginUsers_tmp.UserName && x.Password == LoginUsers_tmp.Password).FirstOrDefault(); //Linq Query  
            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);
            string output = "";
            string sql = string.Empty;
            output += "Retriving  the data from  Packing Slip Id";
            if (AccountSetting.podmodelsort)
            {
                if (data1.UserName == "lkajan@gmail.com")
                    sql = "SELECT distinct  * FROM CustPackingSlipJour";
                else
                    sql = "SELECT distinct  * FROM CustPackingSlipJour where DeviceId=" + data1.DeviceId;
            }
            else
            {
                if (data1.UserName == "lkajan@gmail.com")
                    sql = "SELECT distinct  * FROM CustPackingSlipJour";
                else
                    sql = "SELECT distinct  * FROM CustPackingSlipJour where DeviceId=" + data1.DeviceId;
            }
                

            SQLiteCommand command = db.CreateCommand(sql);
            List<CustPackingSlipJour> packingSlips = command.ExecuteQuery<CustPackingSlipJour>();

            var table = db.Table<CustPackingSlipJour>();
            return packingSlips;         
        }
              
         
        /// <summary>
        /// RETRIVE VIEW FUNCTION 
        /// </summary>
        /// <returns></returns>
        public List<CustPackingSlipJour> PodRetriveCustPackingSlipJour()
        {
            try
            {
                string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers"); //Call Database  
                var db1 = new SQLiteConnection(dpPath);
                var data = db1.Table<LoginUsers>(); //Call Table  
                var data1 = data.Where(x => x.UserName == LoginUsers_tmp.UserName && x.Password == LoginUsers_tmp.Password).FirstOrDefault(); //Linq Query  
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);
                string output = "";
                string sql = string.Empty;
                output += "Retriving  the data from  Packing Slip Id";               
                if (AccountSetting.podmodelsort)                
                {
                    if (data1.UserName == "lkajan@gmail.com")
                        sql = "SELECT distinct  * FROM CustPackingSlipJour";
                    else
                        sql = "SELECT distinct  * FROM CustPackingSlipJour where DeviceId=" + data1.DeviceId;
                }
                else
                {
                    if (data1.UserName == "lkajan@gmail.com")
                        sql = "SELECT distinct  * FROM CustPackingSlipJour";
                    else
                        sql = "SELECT distinct  * FROM CustPackingSlipJour where DeviceId=" + data1.DeviceId;
                }                    
               
                SQLiteCommand command = db.CreateCommand(sql);
                List<CustPackingSlipJour> packingSlipsRetrive = command.ExecuteQuery<CustPackingSlipJour>();
                return packingSlipsRetrive;

            }
            catch (Exception ex)
            {
                return null;
            }


        }


        ///////////////////////////////////////////////////////////////////////////delete
        public List<CustPackingSlipJour> PodPkReptCustPackingSlipJour()
        {
            try
            {

                string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LoginUsers"); //Call Database  
                var db1 = new SQLiteConnection(dpPath);
                var data = db1.Table<LoginUsers>(); //Call Table  
                var data1 = data.Where(x => x.UserName == LoginUsers_tmp.UserName && x.Password == LoginUsers_tmp.Password).FirstOrDefault(); //Linq Query  
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);
                string output = "";
                string sql = string.Empty;
                output += "Retriving  the data from  Packing Slip Id";
                if (AccountSetting.podmodelsort)
                {
                    if (data1.UserName == "lkajan@gmail.com")
                        sql = "SELECT distinct  * FROM CustPackingSlipJour";
                    else
                        sql = "SELECT distinct  * FROM CustPackingSlipJour where DeviceId=" + data1.DeviceId;
                }

                else
                {
                    if (data1.UserName == "lkajan@gmail.com")
                        sql = "SELECT distinct  * FROM CustPackingSlipJour";
                }    

                SQLiteCommand command = db.CreateCommand(sql);
                List<CustPackingSlipJour> packingSlipsRetrive = command.ExecuteQuery<CustPackingSlipJour>();

                var table = db.Table<CustPackingSlipJour>();
                return packingSlipsRetrive;

            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public TableQuery<CustPackingSlipJour> RR_PrintPkSlipJour()
        {
            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);

            string output = "";
            output += "Retriving  the data from  CustPackingSlipJourLine";

            var table = db.Table<CustPackingSlipJour>();

            return table;
        }

        /// <summary>
        /// SEARCH 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustPackingSlipJour CustPackingSlipJourRecId(int RecId)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                var item = db.Get<CustPackingSlipJour>(RecId);
                return item;
            }
            catch (Exception ex)

            {
                return null;
                //Toast.MakeText(this, "Student Data Not Available", ToastLength.Short).Show();
                // return null;
            }

        }

        /// <summary>
        /// GET BUY PACKING SLIP ID
        /// </summary>
        /// <param name="PackingSlipId"></param>
        /// <returns></returns>
        public CustPackingSlipJour GetCustPackingSlipJourByPackingSlipId(string PackingSlipId)
        {
            CustPackingSlipJour obj = new Models.CustPackingSlipJour();
            string cs = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");

            using (SqliteConnection con = new SqliteConnection("Data Source=" + cs))
            {
                con.Open();

                string stm = "SELECT PackingSlipId,DeliveryName,DeliveryDescription,DeliveryAddress,DeliveryPostCode," +
                    "Volume,NetWeight,NoUnit,RecId  FROM CustPackingSlipJour  where PackingSlipId='" + PackingSlipId.Trim() + "'";

                using (SqliteCommand cmd = new SqliteCommand(stm, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            obj.PackingSlipId = rdr["PackingSlipId"].ToString();
                            obj.DeliveryName = rdr["DeliveryName"].ToString();
                            obj.DeliveryDescription = rdr["DeliveryDescription"].ToString();
                            obj.DeliveryAddress = rdr["DeliveryAddress"].ToString();
                            obj.DeliveryPostCode = rdr["DeliveryPostCode"].ToString();
                            obj.Volume = rdr["Volume"].ToString();
                            obj.NetWeight = rdr["NetWeight"].ToString();
                            obj.NoUnit = rdr["NoUnit"].ToString();
                            obj.RecId = Convert.ToInt32(rdr["RecId"]);
                        }
                    }
                }

                con.Close();
            }

            return obj;

        }

        public List<CustPackingSlipJourLine> GetCustPackingSlipJourByPackingSlipLineId(string PackingSlipId)
        {
            List<CustPackingSlipJourLine> objLineList = new List<Models.CustPackingSlipJourLine>();
            string cs = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJourLine");

            using (SqliteConnection con = new SqliteConnection("Data Source=" + cs))
            {
                con.Open();

                string stm = "SELECT PackingSlipId,Item,Description,Unit,Qty  FROM CustPackingSlipJourLine  where PackingSlipId='" + PackingSlipId.Trim() + "'";

                using (SqliteCommand cmd = new SqliteCommand(stm, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            CustPackingSlipJourLine obj = new Models.CustPackingSlipJourLine();
                            obj.PackingSlipId = rdr["PackingSlipId"].ToString();
                            obj.Item = rdr["Item"].ToString();
                            obj.Description = rdr["Description"].ToString();
                            obj.Unit = rdr["Unit"].ToString();
                            obj.Qty = rdr["Qty"].ToString();
                            objLineList.Add(obj);
                        }
                    }
                }

                con.Close();
            }

            return objLineList;

        }

        public List<byte[]> GetCustPackingImageJourByPackingSlipId(int Recid)
        {
            CustPackingSlipJour obj = new Models.CustPackingSlipJour();
            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<CustPackingSlipJour>(Recid);
            List<byte[]> images = new List<byte[]>();

            //images.Add(item.Podimage);
            //images.Add(item.Podimage1);
            //images.Add(item.Podimage2);
            //images.Add(item.Podimage3);
            //images.Add(item.Podimage4);
            //images.Add(item.Podimage5);
            //images.Add(item.Podimage6);
            //images.Add(item.Podimage7);
            //images.Add(item.Podimage8);
            //images.Add(item.Podimage9);
            return images;
        }
        public byte[] getByteArray(DataRow row, int offset)
        {
            object blob = row[offset];
            if (blob == null) return null;
            byte[] arData = (byte[])blob;
            return arData;
        }


        static byte[] GetBytes(SqliteDataReader reader)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// UPDATE 
        /// </summary>
        /// <param name="RecId"></param>
        /// <param name="PackingSlipId"></param>
        /// <returns></returns>
        public string UpdateCustPackingSlipJour(int RecId, string PackingSlipId, string DeliveryName, string DeliveryDescription,
            string DeliveryAddress, string DeliveryPostCode, string Volume, string NetWeight, string NoUnit, byte[] SignImage
            , byte[] Podimage, byte[] Podimage1, byte[] Podimage2, byte[] Podimage3, byte[] Podimage4, byte[] Podimage5, byte[] Podimage6,
             byte[] Podimage7, byte[] Podimage8, byte[] Podimage9)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                var item = db.Get<CustPackingSlipJour>(RecId);
                item.RecId = RecId;
                item.PackingSlipId = PackingSlipId;
                item.DeliveryName = DeliveryName;
                item.DeliveryDescription = DeliveryDescription;
                item.DeliveryAddress = DeliveryAddress;
                item.DeliveryPostCode = DeliveryPostCode;
                item.Volume = Volume;
                item.NetWeight = NetWeight;
                item.NoUnit = NoUnit;
                //item.Podimage = Podimage;
                //item.Podimage1 = Podimage1;
                //item.Podimage2 = Podimage2;
                //item.Podimage3 = Podimage3;
                //item.Podimage4 = Podimage4;
                //item.Podimage5 = Podimage5;
                //item.Podimage6 = Podimage6;
                //item.Podimage7 = Podimage7;
                //item.Podimage8 = Podimage8;
                //item.Podimage9 = Podimage9;
                //item.SignatureImage = SignImage;
                db.Update(item);

                return "Record Updated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// UPLOAD SIGNATURE PAD IMAGE
        /// </summary>
        /// <param name="RecId"></param>
        /// <param name="SignatureImage"></param>
        /// <returns></returns>
        public string UpdateSignatureImage(int RecId, byte[] SignatureImage)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                var item = db.Get<CustPackingSlipJour>(RecId);
                item.RecId = RecId;
                //item.SignatureImage = SignatureImage;
                db.Update(item);

                return "Record Updated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// Delete List View   SignatureImage
        /// </summary>
        /// <param name="RecId"></param>
        /// <returns></returns>
        public string DeletePodPodImg(int RecId, byte[] Podimage, byte[] Podimage1, byte[] Podimage2, byte[] Podimage3,
            byte[] Podimage4, byte[] Podimage5, byte[] Podimage6, byte[] Podimage7, byte[] Podimage8, byte[] Podimage9)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustPackingSlipJour");
                var db = new SQLiteConnection(dbPath);

                var item = db.Get<CustPackingSlipJour>(RecId);
                item.RecId = RecId;
                //item.Podimage = Podimage;
                //item.Podimage1 = Podimage1;
                //item.Podimage2 = Podimage2;
                //item.Podimage3 = Podimage3;
                //item.Podimage4 = Podimage4;
                //item.Podimage5 = Podimage5;
                //item.Podimage6 = Podimage6;
                //item.Podimage7 = Podimage7;
                //item.Podimage8 = Podimage8;
                //item.Podimage9 = Podimage9;
                db.Delete(item);

                return "CustPackingSlipJour Record Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}



