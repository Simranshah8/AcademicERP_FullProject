using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Exam.Transaction
{
    public class SymboolNo : ResponeValues
    {
        public int EnquiryId { get; set; }
        public int RegId { get; set; }
        public string Status { get; set; }
        public string Sourse { get; set; }
        public string EntryDate { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PaymentStatus { get; set; }
        public string PhotoPath { get; set; }
        public string SymbolNo { get; set; }
        public int? TranId { get; set; }

    }
    public class SymboolNoCollections : System.Collections.Generic.List<SymboolNo>
    {
        public SymboolNoCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class EntranceSymbolNo : ResponeValues
    {

        public int? TranId { get; set; }
        public string EnquiryNo { get; set; } = "";
        public int? StartNumber { get; set; }
        public string StartAlpha { get; set; } = "";
        public int? PadWith { get; set; }
        public string Prefix { get; set; } = "";
        public string Suffix { get; set; } = "";
        public string SymbolNo { get; set; } = "";


    }
    public class EntranceSymbolNoCollections : System.Collections.Generic.List<EntranceSymbolNo>
    {
        public EntranceSymbolNoCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}