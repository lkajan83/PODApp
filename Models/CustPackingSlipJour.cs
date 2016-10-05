using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    /// <summary>
    /// CustPackingSlipJour - SQLite Database
    /// </summary>
    public class CustPackingSlipJour
    {
        [PrimaryKey, AutoIncrement]
        public int RecId { get; set; }
        public string DeviceId { get; set; }
        public string PackingSlipId { get; set; }
        public string Qty { get; set; }
        public string Volume { get; set; }
        public string Weight { get; set; }
        public string DeliveryName { get; set; }

        public override string ToString()
        {
            return PackingSlipId ;
            //return PackingSlipId + " - " + DeliveryName;
        }
    }
}