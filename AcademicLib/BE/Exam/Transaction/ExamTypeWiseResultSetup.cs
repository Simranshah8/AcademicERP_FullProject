using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
    public class ExamTypeWiseResultSetup : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string SectionIdColl { get; set; }
        public int ExamTypeId { get; set; }
        public int TotalFailSubject { get; set; }        
        public bool IsSubjectWise { get; set; }

        private List<ExamTypeWiseResultSetupDetails> _MarksSetupDetailsCollections = new List<ExamTypeWiseResultSetupDetails>();
        public List<ExamTypeWiseResultSetupDetails> ExamTypeWiseResultSetupDetailsColl
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

    }
    public class ExamTypeWiseResultSetupCollections : System.Collections.Generic.List<ExamTypeWiseResultSetup>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ExamTypeWiseResultSetupDetails
    {
        public int TranId { set; get; }
        public int SubjectId { set; get; }    
        public bool TH { set; get; }
        public bool PR { set; get; }

    }
    public class ExamTypeWiseResultSetupDetailsCollections : System.Collections.Generic.List<ExamTypeWiseResultSetupDetails> { }
}