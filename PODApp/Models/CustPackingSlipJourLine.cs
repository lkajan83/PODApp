using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PODApp.Models
{
    public class CustPackingSlipJourLine
    {
        [PrimaryKey AutoIncrement]
        public int CPJLineId { get; set; }

        [ForeignKey(typeof(CustPackingSlipJour))]
        public string PackingSlipId { get; set; }

        //Add New Field
        public string Status { get; set; }

        public DateTime Date { get; set; }

        public string Item { get; set; }
        
        public string Description { get; set; }

        public string Unit { get; set; }

        public string Qty { get; set; }

        public override string ToString()
        {
            return CPJLineId + " - " + PackingSlipId + " - " + Item + " - " + Description + " - " + Unit + " - " + Qty;
        }
    }

    //public class DupCustPackingSlipJourLine
    //{
    //    [PrimaryKey AutoIncrement]
    //    public int CPJLineId { get; set; }

    //    public string States { get; set; }

    //    public string Date { get; set; }

    //    [ForeignKey(typeof(CustPackingSlipJour))]
    //    public string PackingSlipId { get; set; }

    //    public string Item { get; set; }

    //    public string Description { get; set; }

    //    public string Unit { get; set; }

    //    public string Qty { get; set; }

    //    public override string ToString()
    //    {
    //        return CPJLineId + " - " + PackingSlipId + " - " + Item + " - " + Description + " - " + Unit + " - " + Qty;
    //    }
    //}
}