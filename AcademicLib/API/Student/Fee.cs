using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Student
{
    
    public class Fee : ResponeValues
    {
        public Fee()
        {
            MonthlySummaryColl = new List<MonthlySummary>();
        }
        public double Opening { get; set; }
        public double FeeAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double PaidAmt { get; set; }
        public double DuesAmt { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public List<MonthlySummary> MonthlySummaryColl { get; set; }
    }

    public class MonthlySummary 
    {
        public MonthlySummary()
        {
            FeeItemColl = new List<FeeItem>();
            ReceiptColl = new List<Receipt>();
        }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public double Opening { get; set; }
        public double FeeAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double PaidAmt { get; set; }
        public double DuesAmt { get; set; }
        public List<FeeItem> FeeItemColl { get; set; } = new List<FeeItem>();
        public List<Receipt> ReceiptColl { get; set; } = new List<Receipt>();

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }

    public class FeeItem
    {
        public int MonthId { get; set; }
        public string FeeItemName { get; set; }
        public double Amount { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }

    public class Receipt
    {
        public Receipt()
        {
            MonthName = "";
            RefNo = "";
            Narration = "";
            VoucherDate_BS = "";
        }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public int TranId { get; set; }
        public DateTime VoucherDate_AD { get; set; }
        public string VoucherDate_BS { get; set; }
        public int ReceiptNo { get; set; }
        public string RefNo { get; set; }
        public double ReceiptAmt { get; set; }
        public double DiscountAmt { get; set; }
        public string Narration { get; set; }
        public double Dues { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
}
