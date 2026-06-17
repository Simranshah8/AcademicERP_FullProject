using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Transaction
{
    public class BookIssue : ResponeValues
    {
        public BookIssue()
        {
            IssueDate = DateTime.Today;
            DetailsColl = new List<BookIssueDetails>();
        }
        public int? TranId { get; set; }
        public int? StudentId { get; set; }
        public int? EmployeeId { get; set; }
        public int AutoNumber { get; set; }
        public string AutoManualNo { get; set; }
        public DateTime IssueDate { get; set; }

        public List<BookIssueDetails> DetailsColl
        {
            get;set;
        }
    }
    public class BookIssueCollections : System.Collections.Generic.List<BookIssue>
    {

    }
    public class BookIssueDetails
    {
        public BookIssueDetails()
        {
            IssueDate = DateTime.Today;
        }
        public int SNo { get; set; }
        public int BookEntryId { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueRemarks { get; set; }
        public int CreditDays { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnRemarks { get; set; }
        public double FineAmount { get; set; }
    }

    public class PreviousBook
    {
        public int TranId { get; set; }
        public int AccessionNo { get; set; }
        public string BookTitle { get; set; }
        public string Edition { get; set; }
        public string Publication { get; set; }
        public string Department { get; set; }
        public DateTime IssueDate_AD { get; set; }
        public string IssueDate_BS { get; set; }
        public int CreditDays { get; set; }
        public string IssueRemarks { get; set; }
        public int OutStandingDays { get; set; }
        public double FineAmt { get; set; }
        public int IssueId { get; set; }
        public string Barcode { get; set; }
        public string FronCoverPath { get; set; }
        public string BackCoverPath { get; set; } 
        public string Authors { get; set; }
        public string ClassName { get; set; }
        public string Medium { get; set; }
        public int BookEntryId { get; set; }

        public string Faculty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string BookCategory { get; set; }
        public string Subject { get; set; }
        public double? BookPrice { get; set; }
    }
    public class PreviousBookCollections : System.Collections.Generic.List<PreviousBook>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class CreditRules : ResponeValues
    {
        public int BookLimit { get; set; }
        public int CreditDays { get; set; }
        public int TotalIssueBook { get; set; }
    }
}
