using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam
{
    public class ExamGroupWiseResultSetup : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string SectionIdColl { get; set; }
        public int ExamTypeId { get; set; }
        public int TotalFailSubject { get; set; }
        public bool IsSubjectWise { get; set; }

        private List<ExamGroupWiseResultSetupDetails> _MarksSetupDetailsCollections = new List<ExamGroupWiseResultSetupDetails>();
        public List<ExamGroupWiseResultSetupDetails> ExamGroupWiseResultSetupDetailsColl
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
    public class ExamGroupWiseResultSetupCollections : System.Collections.Generic.List<ExamGroupWiseResultSetup>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ExamGroupWiseResultSetupDetails
    {
        public int TranId { set; get; }
        public int SubjectId { set; get; }
        public bool TH { set; get; }
        public bool PR { set; get; }

    }
    public class ExamGroupWiseResultSetupDetailsCollections : System.Collections.Generic.List<ExamGroupWiseResultSetupDetails> { }
}