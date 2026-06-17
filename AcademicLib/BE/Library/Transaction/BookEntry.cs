using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Transaction
{
   public class BookEntry : ResponeValues
    {
        public BookEntry()
        {
            DetailsList = new List<BookDetails>();
            AuthorIdColl = new List<int>();
        }
       public int? BookEntryId { set; get; }
       public int? BookTitleId { set; get; }
       public List<int> AuthorIdColl { set; get; }
       public int? PublicationId { set; get; }
       public int? EditionId { set; get; }
       public string Volume { set; get; }
       public string ISBNNo { set; get; }
       public string Language { set; get; }
       public int? DonarId { set; get; }
       public int? ClassId { set; get; }
       public int? MediumId { set; get; }
       public string Location { set; get; }
       public int Status { set; get; }
        public DateTime? PurchaseDate { set; get; }
       public string Vendor { set; get; }
        public string BillNo { set; get; }
        public double BookPrice { set; get; }
       public int CreditDays { set; get; }
        public string Description { set; get; }
        public int? NoOfBooks { set; get; }
       public int StartedAccessionNo { set; get; }
        public int EndedAccessionNo { set; get; }
       public string ImagePath { set; get; }
       public string Year { set; get; } 
        public int? MaterialTypeId { get; set; }
       public int Pages { set; get; } 
       public int? DepartmentId { set; get; } 
       public int? AcademicYearId { set; get; } 
       
      public int? SubjectId { get; set; }
        public int id
        {
            get
            {
                if (BookEntryId.HasValue)
                    return BookEntryId.Value;
                return 0;
            }
        }

        public string text
        {
            get
            {
                return "";
            }
        }

        public string FrontCoverPath { get; set; }
        public string BackCoverPath { get; set; }

        public double? FixFineAmount { get; set; }
        public double? LateFineAmountPerDay { get; set; }
        public List<BookDetails> DetailsList { get; set; }

        public string BookNo { get; set; }
        public string CallNo { get; set; }

        public int? ClassLevelId { get; set; }
        public int? FacultyId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsImport { get; set; }
    }
    public class BookEntryCollections : System.Collections.Generic.List<BookEntry> {
        public BookEntryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
  
    public class BookDetails
    {
        public int BookEntryId { get; set; }
        public int SNo { get; set; }
        public int AccessionNo { get; set; }
        public string BarCode { get; set; }
        public int? RackId { get; set; }
    }


    public class PrintBarCode
    {
        public int SNo { get; set; }
        public string AccessionNo { get; set; }
        public string BarCode { get; set; }
        public string BookTitle { get; set; }
        public string ISBSNo { get; set; }
        public string ClassName { get; set; }
        public string Medium { get; set; }
        public string Department { get; set; }
    }
    public class PrintBarCodeCollections : System.Collections.Generic.List<PrintBarCode>
    {
        public PrintBarCodeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class BookDetailsForBarCode : PrintBarCode
    {
        public int TranId { get; set; }
        public int BookEntryId { get; set; }
        public string RackName { get; set; }
        public string Publication { get; set; }
        public string SubjectName { get; set; }
    }
    public class BookDetailsForBarCodeCollections : System.Collections.Generic.List<BookDetailsForBarCode>
    {
        public BookDetailsForBarCodeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ImportBookDetails
    {
        public string Barcode { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Publication { get; set; }
        public string Edition { get; set; }
        public string Year { get; set; }
        public string MaterialType { get; set; }
        public string ISBNNo { get; set; }
        public string Volume { get; set; }
        public int Page { get; set; }
        public string Language { get; set; }
        public string Donor { get; set; }
        public string Department { get; set; }
        public string ClassName { get; set; }
        public string Medium { get; set; }
        public string AcademicYear { get; set; }
        public string Rack { get; set; }
        public string Location { get; set; }
        public string Vendor { get; set; }
        public string BillNo { get; set; }
        public double BookPrice { get; set; }
        public int CreditDays { get; set; }
        public string Description { get; set; }
        public int NoOfBooks { get; set; }
        public double FixFineAmount { get; set; }
        public double LateFineAmountPerDay { get; set; }
        public string CallNo { get; set; }
        public string BookNo { get; set; }

        public string Faculty { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string ClassLevel { get; set; } = "";
        public string Semester { get; set; } = "";
        public int AccessionNo { get; set; }
    }
}
