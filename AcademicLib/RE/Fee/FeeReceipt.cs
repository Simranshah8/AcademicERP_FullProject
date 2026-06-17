using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeReceipt
    {
        public FeeReceipt()
        {
            DetailsColl = new FeeReceiptCollections();
        }        
        public bool IsParent { get; set; }
        public int TranId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int AutoVoucherNo { get; set; }
        public string AutoManualNo { get; set; }
        public string RefNo { get; set; }
        public string Narration { get; set; }
        public int PaidUpToMonth { get; set; }
        public double TotalDues { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public double FineAmt { get; set; }
        public double ReceivableAmt { get; set; }
        public double ReceivedAmt { get; set; }
        public double AfterReceivedDues { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; }
        public string M_ContactNo { get; set; }
        public string Address { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string UserName { get; set; }
        public DateTime LogDateTime { get; set; }
        public bool IsCancel { get; set; }
        public string CancelBy { get; set; }
        public DateTime? CancelDateTime { get; set; }
        public string CancelRemarks { get; set; }

        public DateTime VoucherDate_AD { get; set; }
        public string VoucherDate_BS { get; set; }

        public string PaidUpToMonthName { get; set; }
        public FeeReceiptCollections DetailsColl { get; set; }
        public string BranchName { get; set; }
        public string CostClass { get; set; }
        public string ReceiptAsLedger { get; set; }
        public string JVNo { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string AcademicYearName { get; set; }
        public string FeeCategory { get; set; }
        public int FeeCategorySNo { get; set; }
        public double Waiver { get; set; }
        public bool IsNewStudent { get; set; }
        public string LedgerPanaNo { get; set; }
    }
    public class FeeReceiptCollections : System.Collections.Generic.List<FeeReceipt>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
