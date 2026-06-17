using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Library
{
    public class StudentLedger
    {
        public int SNo { get; set; }
        public int IssueNo { get; set; }
        public string Subject { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueMiti { get; set; }
        public int CreditDays { get; set; }
        public string IssueRemarks { get; set; }
        public string ReceivedType { get; set; }
        public double FineAmount { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnMiti { get; set; }
        public int AccessionNo { get; set; }
        public string BookTitle { get; set; }
    }
    public class StudentLedgerCollections : System.Collections.Generic.List<StudentLedger>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
