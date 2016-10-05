using System;
using System.Data;
using System.IO;
using SQLite;

namespace PODApp.Models
{
    ///<summary>
    /// Employee Records - SQLite Database
    ///</summary>
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
    }
}