using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using SQLite;
using Android.Media;
using Android.Graphics;
using System.Data.SqlClient;
using System.Drawing;
using Uri = Android.Net.Uri;

namespace PODApp.Models
{
    /// <summary>
    /// CustPackingSlipJour - SQLite Database
    /// </summary>
    public class CustPackingSlipJour
    {
        [PrimaryKey, AutoIncrement]
        public int RecId { get; set; }            

        public string PackingSlipId { get; set; }

        //Add New Field
        public int DeviceId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPostCode { get; set; }
        public string Volume { get; set; }
        public string NetWeight { get; set; }
        public string NoUnit { get; set; }        
       
        /// <summary>
        /// Need to delete 
        /// </summary>

        //public byte[] Podimage { get; set; }
        //public byte[] Podimage1 { get; set; }
        //public byte[] Podimage2 { get; set; }
        //public byte[] Podimage3 { get; set; }
        //public byte[] Podimage4 { get; set; }
        //public byte[] Podimage5 { get; set; }
        //public byte[] Podimage6 { get; set; }
        //public byte[] Podimage7 { get; set; }
        //public byte[] Podimage8 { get; set; }
        //public byte[] Podimage9 { get; set; }
        //public byte[] SignatureImage { get; set; }



        public override string ToString()
        {
           return PackingSlipId + "  " + DeliveryPostCode ;           
        }       
    }

    public static class CustPackingSlipJour1
    {

        public static string PackingSlipId { get; set; }

        public static int Recid { get; set; }

        public static Bitmap SignatureImg { get; set; }

        public static int imagecount { get; set; }

        public static List<Uri> Podimages { get; set; }
    }

    public class CustPackingSlipJour2
    {

        public string PackingSlipId { get; set; }

        public Uri Podimage { get; set; }

        public int imagecount { get; set; }

        public List<Bitmap> Podimages { get; set; }
    }
}

