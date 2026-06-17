using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Library
{
    public class BookDetails
    {
        public int SNo { get; set; }
        public int TranId { get; set; }
        public int BookEntryId { get; set; }
        public int AccessionNo { get; set; }
        public string BarCode { get; set; }
        public string BookTitle { get; set; }
        public string Subject { get; set; }
        public string Publication { get; set; }
        public string Edition { get; set; }
        public string MaterialType { get; set; }
        public string Department { get; set; }
        public string ClassName { get; set; }
        public string Medium { get; set; }
        public string Authors { get; set; }
        public string Year { get; set; }
        public string ISBNNo { get; set; }
        public string Volume { get; set; }
        public string Rack { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public int CreditDays { get; set; }

        public int StartedAccessionNo { get; set; }
        public int EndedAccessionNo { get; set; }

        public string IssueTo { get; set; }
        public string Name { get; set; }
        public string RegdNo { get; set; }
        public DateTime? IssueDate_AD { get; set; }
        public string IssueDate_BS { get; set; }
        public int TotalDays { get; set; }
        public DateTime? ReturnDate_AD { get; set; }
        public string ReturnDate_BS { get; set; }
        public int IssueCreditDays { get; set; }
        public string IssueRemarks { get; set; }
        public string ReturnRemarks { get; set; }
        public string IssueBy { get; set; }
        public string ReceiedBy { get; set; }
        public double FineAmount { get; set; }
        public int OutStandingDays { get; set; }

        public string BookNo { get; set; }
        public string CallNo { get; set; }

        public string SectionName { get; set; }
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public double BookPrice { get; set; }
        public string Vendor { get; set; }
        public string PurchaseDate { get; set; }
        public string BillNo { get; set; }

        public string B_Faculty { get; set; }
        public string B_Level { get; set; }
        public string B_Semester { get; set; }
        public string B_ClassYear { get; set; }

        public string PurchaseDate_BS { get; set; }
        public int Pages { get; set; }
        public string DonorName { get; set; }
        public string BookCategory { get; set; }

    }

    public class BookDetailsCollections : System.Collections.Generic.List<BookDetails>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
