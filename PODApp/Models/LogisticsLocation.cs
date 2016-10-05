using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    /// <summary>
    /// LogisticsLocation - SQLite Database
    /// </summary>
    public class LogisticsLocation
    {
        [PrimaryKey, AutoIncrement]
        public int RecId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return RecId.ToString() + " - " + Location + " - " + Description;
        }
    }
}