using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Setup
{
    public class EntranceSetup : ResponeValues
    {
        public EntranceSetup()
        {
            ClassWiseEntranceSetupList = new List<ClassWiseEntranceSetup>();            
        }       
        public string ExamName { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Venue { get; set; }   
        public bool ForClassWise { get; set; }
        public string ExamRules { get; set; }
        //Add by Prashant Chaitra 26
        public string Subject { get; set; }
        public int FullMarks { get; set; }
        public int PassMarks { get; set; }
        public DateTime? ResultDate { get; set; }
        public List<ClassWiseEntranceSetup> ClassWiseEntranceSetupList { get; set; }
    }

    public class ClassWiseEntranceSetup
    {
        public int ClassId { get; set; }
        public string ExamName { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Venue { get; set; }
        //Add by Prashant Chaitra 26
        public string Subject { get; set; }
        public int? FullMarks { get; set; }
        public int? PassMarks { get; set; }
        public DateTime? ResultDate { get; set; }

    }

    public class EntranceCardData : ResponeValues
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
        public string SymbolNo { get; set; }
        //DONE: Added New Property
        public string ExamDateMiti { get; set; }

    }
    public class EntranceCardDataCollections : System.Collections.Generic.List<EntranceCardData>
    {
        public EntranceCardDataCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
