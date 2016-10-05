using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    /// <summary>
    /// SalesShippingStat - SQLite Database
    /// </summary>
    public class SalesShippingStat
    {
        public string Partition { get; set; }
        public string RecId { get; set; }
        public string CartonsQty { get; set; }
        public string DeliveryDate { get; set; }
        public string FreightZone { get; set; }
        public string GrossWeight { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceId { get; set; }
        public string NetWeight { get; set; }
        public string Nolabels { get; set; }
        public string OrigSalesId { get; set; }
        public string PackingSlipId { get; set; }
        public string ParmId { get; set; }
        public string Quantity { get; set; }
        public string SalesId { get; set; }
        public string UnitWeight { get; set; }
        public string Volume { get; set; }
        public string VolumeQty_BR { get; set; }
        public string VolumeType_BR { get; set; }
        public string modifiedDateTime { get; set; }
        public string dataAreaId { get; set; }
        public string recVersion { get; set; }
    }
}