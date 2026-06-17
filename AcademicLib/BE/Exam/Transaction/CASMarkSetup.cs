using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class CASMarksSetup : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }         
        
        public int SubjectId { get; set; }
        public int ExamTypeId { get; set; }

        public double FullMark { get; set; }
        
        private List<CASMarksSetupDetails> _MarksSetupDetailsCollections = new List<CASMarksSetupDetails>();
        public List<CASMarksSetupDetails> MarksSetupDetailsColl
        {
            get
            {
                return _MarksSetupDetailsCollections;
            }
            set
            {
                _MarksSetupDetailsCollections = value;
            }
        }

        public ExamClassSubjectCollections ExamClassSubjectsColl { get; set; }
    }
    public class CASMarksSetupCollections : System.Collections.Generic.List<CASMarksSetup>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class CASMarksSetupDetails
    {
        public int TranId { set; get; }
        public int CASTypeId { set; get; } 
        public double FullMark { get; set; }
        public double Mark { set; get; }
        public int Under { set; get; }
        public int Scheme { set; get; }
        public DateTime? DateFrom { set; get; }
        public DateTime? DateTo { set; get; }
        public int? ExamTypeId { set; get; }        
        public int? AttendanceFrom { get; set; }
        public string Formula { get; set; }

    }
    public class CASMarksSetupDetailsCollections : System.Collections.Generic.List<CASMarksSetupDetails> { }

    public class ExamClassSubject
    {
        public int ExamTypeId { set; get; }
        public int ClassId { set; get; }
        public int SubjectId { set; get; }
        public string ExamType { set; get; }
        public string ClassName { set; get; }
        public string SubjectName { set; get; }
        public string SubjectCode { set; get; }

    }
    public class ExamClassSubjectCollections : System.Collections.Generic.List<ExamClassSubject>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
