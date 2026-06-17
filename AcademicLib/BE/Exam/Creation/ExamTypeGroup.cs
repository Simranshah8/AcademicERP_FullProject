using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{
  public  class ExamTypeGroup : BE.Academic.Common
    {
        public int? ExamTypeGroupId { get; set; }     
        public DateTime? ResultDate { get; set; }
        public DateTime? ResultTime { get; set; }
        public int? CurrentExamTypeId { get; set; }
        public string ResultDateBS { get; set; }
        private List<ExamTypeGroupDetails>   _ExamTypeGroupDetailsCollections = new List<ExamTypeGroupDetails>();
        public List<ExamTypeGroupDetails> ExamTypeGroupDetailsColl
        {
            get
            {
                return _ExamTypeGroupDetailsCollections;
            }
            set
            {
                _ExamTypeGroupDetailsCollections = value;
            }
        }
       

        public int id
        {
            get
            {
                if (ExamTypeGroupId.HasValue)
                    return ExamTypeGroupId.Value;
                return 0;
            }
        }

        public int ExamTypeId
        {
            get
            {
                if (ExamTypeGroupId.HasValue)
                    return ExamTypeGroupId.Value;
                return 0;
            }
        }

        public bool NeedPublished { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? LastPublishDate { get; set; }
        public string LastUpdateMiti { get; set; }
        public string LastPublishMiti { get; set; }
        public bool IsActive { get; set; }
        //Added field
        public int? DisplayYearBS { get; set; }
        public int? DisplayYearAD { get; set; }

    }
    public class ExamTypeGroupCollections : System.Collections.Generic.List<ExamTypeGroup> 
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ExamTypeGroupDetails
    { 
        public int TranId { get; set; }
        public int SNO { get; set; }
        public int ExamTypeId { get; set; }
        public double Percent { get; set; }
        public string DisplayName { get; set; }
        public bool IsCalculateAttenDance { get; set; }
        public bool ShowGradingSubject { get; set; }

        private ExamTypeWiseSubjectCollections _ExamTypeWiseSubjectCollections = new ExamTypeWiseSubjectCollections();
        public ExamTypeWiseSubjectCollections ExamTypeWiseSubjectColl
        {
            get
            {
                return _ExamTypeWiseSubjectCollections;
            }
            set
            {
                _ExamTypeWiseSubjectCollections = value;
            }
        }
    }
    public class ExamTypeGroupDetailsCollections : System.Collections.Generic.List<ExamTypeGroupDetails> { }
    public class ExamTypeWiseSubject 
    {        
        public int TranId { get;set;}
        public int? SubjectId { get;set;}
        public double PercentTH { get;set;}
        public double PercentPR { get;set;}
    }
    public class ExamTypeWiseSubjectCollections : System.Collections.Generic.List<ExamTypeWiseSubject> { }
}