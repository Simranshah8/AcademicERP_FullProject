using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;


namespace AcademicLib.RE.Fee
{

    public class BillingSummary : ResponeValues
    {
        public int? FeeItemId { get; set; }
        public int? OrderNo { get; set; }
        public string FeeItemName { get; set; } = "";
        public double? BillingAmt { get; set; } 
        public double? DisAmt { get; set; } 
        public double? PayableAmt { get; set; } 
        public int? TranId { get; set; } 
        public int? BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string BillDate_BS { get; set; } = "";
        public int? StudentId { get; set; }
        public string RegNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public int? RollNo { get; set; } 
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string Batch { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public String Source { get; set; } = "";
        public int? InvoiceNo { get; set; } 
        public int? SNo { get; set; }
        //Added by suresh opn 30 bhadra
        public string Ledger { get; set; } = "";
        public string LedgerGroup { get; set; } = "";
        public string Product { get; set; } = "";
        public string ProductType { get; set; } = "";
    }
    public class BillingSummaryCollection : List<BillingSummary>
{
    public BillingSummaryCollection()
    {
        ResponseMSG = "";
    }

    public string ResponseMSG { get; set; }
    public bool IsSuccess { get; set; }
}

}