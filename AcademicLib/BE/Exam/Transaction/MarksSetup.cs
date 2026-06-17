using Dynamic.BusinessEntity.SMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class MarksSetup : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string SectionIdColl { get; set; }
        public int ExamTypeId { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; }
        public bool IsAutoSum { get; set; }
      
        private List<MarksSetupDetails>  _MarksSetupDetailsCollections = new List<MarksSetupDetails>();
        public List<MarksSetupDetails> MarksSetupDetailsColl
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

        public string FromSectionIdColl { get; set; }
        public string ToSectionIdColl { get; set; }
        public string ToClassIdColl { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }
    }
    public class MarksSetupCollections : System.Collections.Generic.List<MarksSetup> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class MarksSetupDetails
    {
        public int TranId { set; get; }        
        public int SubjectId { set; get; }
        public double CRTH { set; get; }
        public double CRPR { set; get; }
        public double FMTH { set; get; }
        public double FMPR { set; get; }
        public double PMTH { set; get; }
        public double PMPR { set; get; }
        public bool IsInclude { set; get; }
        public int SubjectType { get; set; }
        public int OTH { get; set; }
        public int OPR { get; set; }
     
    }
    public class MarksSetupDetailsCollections : System.Collections.Generic.List<MarksSetupDetails> { }


    public class ImportMarkSetup
    {
        public string AcademicYear { get; set; }
        public string ExamName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; }
        public string Subject { get; set; }
        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double CR { get; set; }
        public double FMTH { get; set; }
        public double FMPR { get; set; }
        public double FM { get; set; }
        public double PMTH { get; set; }
        public double PMPR { get; set; }
        public double PM { get; set; }
        public double OTH { get; set; }
        public double OPR { get; set; }
        public bool IsInclude { get; set; }
        public string SubjectType { get; set; }


    }
}