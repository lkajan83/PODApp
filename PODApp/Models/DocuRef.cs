using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    /// <summary>
    /// DocuRef - SQLite Database
    /// </summary>
    public class DocuRef
    {
        [PrimaryKey, AutoIncrement]
        public int RecId { get; set; }
        public string PackingSlipId { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return RecId.ToString() + " - " + PackingSlipId + " - " + Location;
        }
    }
}