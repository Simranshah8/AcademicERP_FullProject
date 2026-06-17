using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class DateWiseIncome
    {
        public int TranId { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public DateTime VoucherDate_AD { get; set; }
        public string VoucherDate_BS { get; set; }
        public int AutoVoucherNo { get; set; }
        public string AutoManualNo { get; set; }
        public int FeeSNo { get; set; }
        public string FeeItemName { get; set; }
        public string HeadFor { get; set; }
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
        public double FineAmt { get; set; }
        public string CreateBy { get; set; }
        public string RefNo { get; set; }
        public string Narration { get; set; }

        public string PaidUpToMonth { get; set; }
        public string ReceiptMonth { get; set; }
        public string Address { get; set; }
        public string TransportPoint { get; set; }
        public string ReceiptAs { get; set; }
        public string JvNo { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public double AfterReceivedDues { get; set; }

    }
    public class DateWiseIncomeCollections : System.Collections.Generic.List<DateWiseIncome>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
