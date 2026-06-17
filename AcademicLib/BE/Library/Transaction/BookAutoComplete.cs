using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Transaction
{
    public class BookAutoComplete : ResponeValues
    {
        public int TranId { get; set; }
        public int AccessionNo { get; set; }
        public string Barcode { get; set; }
        public string BookTitle { get; set; }
        public string Subject { get; set; }
        public string Publication { get; set; }
        public string Authors { get; set; }
        public string MaterialType { get; set; }
        public string Department { get; set; }
        public string FrontCoverPath { get; set; }
        public string BackCoverPath { get; set; }
        public string Location { get; set; }
        public string ClassName { get; set; }
        public string MediumName { get; set; }
        public int CreditDays { get; set; }
        public string BookNo { get; set; }
        public string CallNo { get; set; }
        public string BookCategory { get; set; }
    }
    public class BookAutoCompleteCollections : System.Collections.Generic.List<BookAutoComplete>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}
