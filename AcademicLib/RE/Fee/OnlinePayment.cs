using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class OnlinePayment
    {
        public int TranId { get; set; }
        public string UserName { get; set; }
        public string SourceName { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public double Amount { get; set; }
        public string RefId { get; set; }
        public string MobileNo { get; set; }
        public string Notes { get; set; }
        public string FromReq { get; set; } 
        public DateTime LogDateTime_AD { get; set; }
        public string LogDateTime_BS { get; set; }

        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptAsLedger { get; set; }
        public string PaidUptoMonth { get; set; }
    }
    public class OnlinePaymentCollections : System.Collections.Generic.List<OnlinePayment>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
