using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Exam.Transaction
{
    public class MarkEntry : ResponeValues
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
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string Venue { get; set; }
        public string ExamRules { get; set; }
        public int? FullMarks { get; set; }
        public int? PassMarks { get; set; }
        public int? Result { get; set; }
        public double? ObtMarks { get; set; }
        public string Remarks { get; set; }
        public DateTime ResultDate { get; set; }
        public string ResultDateMiti { get; set; }
        public string Subject { get; set; }
        public string ExamDateMiti { get; set; }
        public int? TranId { get; set; }
        public string SymbolNo { get; set; } = "";

    }
    public class MarkEntryCollections : System.Collections.Generic.List<MarkEntry>
    {
        public MarkEntryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class EntranceMarkEntry : ResponeValues
    {

        public int? TranId { get; set; }
        public string EnquiryNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string AppliedClass { get; set; } = "";
        public string ExamName { get; set; } = "";
        public int? FullMarks { get; set; }
        public int? PassMarks { get; set; }
        public double? ObtMarks { get; set; }
        public int? Result { get; set; }
        public string Remarks { get; set; } = "";
        public DateTime? ExamDatetime { get; set; }
        public string SubjectIncluded { get; set; } = "";
    }

    public class EntranceMarkEntryCollections : System.Collections.Generic.List<EntranceMarkEntry>
    {
        public EntranceMarkEntryCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class MarkSetup
    {
        public int? TranId { get; set; }
        public int? ClassId { get; set; }
        public int? SubjectId { get; set; }
        public string ClassName { get; set; } = "";
        public string SubjectName { get; set; } = "";   
        public double? FullMark { get; set; } 
        public double? PassMark { get; set; } 
    }
    public class MarkSetupCollections : System.Collections.Generic.List<MarkSetup>
    {
        public MarkSetupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class EntranceMarkSetup : ResponeValues
    {

        public int? TranId { get; set; }
        public int? ClassId { get; set; }
        public int? SubjectId { get; set; }
        public double? FullMark { get; set; }
        public double? PassMark { get; set; }
    }

    public class EntranceMarkSetupCollections : System.Collections.Generic.List<EntranceMarkSetup>
    {
        public EntranceMarkSetupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}