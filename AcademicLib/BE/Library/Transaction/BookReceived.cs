using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Transaction
{
    public class BookReceived : ResponeValues
    {
        public int IssueId { get; set; }
        public int ReceivedType { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnRemarks { get; set; }
        public DateTime? RenewalDate { get; set; }
        public string RenewalRemarks { get; set; }
        public int CreditDays { get; set; }
        public double FineAmount { get; set; }
    }
    
}
