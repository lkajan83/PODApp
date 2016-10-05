using System;
using System.Data;
using System.IO;
using SQLite;
using Android.Media;

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
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPostCode { get; set; }
        public string Volume { get; set; }  
        public string NetWeight { get; set; }       
        public string NoUnit { get; set; }

        public override string ToString()
        {
            return PackingSlipId + " - " + DeliveryName;
        }
    }
}


