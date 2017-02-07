using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using SQLite;
using Android.Media;
using Android.Graphics;
using System.Data.SqlClient;
using System.Drawing;
using SQLiteNetExtensions.Attributes;

namespace PODApp.Models
{
    public class PackingSlipAttachment
    {

        [PrimaryKey, AutoIncrement]
        public int RecId { get; set; }

        [ForeignKey(typeof(CustPackingSlipJour))]
        public string PackingSlipId { get; set; }

        //bool
        public bool Attachment { get; set; }

        //bool
        public bool Signature { get; set; }

    }
}