using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    /// <summary>
    /// LogisticPostalAddress - SQLite Database
    /// </summary>
    public class LogisticPostalAddress
    {
        [PrimaryKey, AutoIncrement]
        public int RecId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return RecId.ToString() + " - " + Address + " - " + ZipCode + " - " + Location;
        }
    }
}