using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{  
    
   public class ExamType : BE.Academic.Common
    {
        public ExamType()
        {
            ClassWiseColl = new List<ClassWiseResultPublichedDate>();
        }
        public int? ExamTypeId { set; get; }
        public DateTime? ResultDate { set; get; }
        public DateTime? ResultTime { set; get; }

        public int id
        {
            get
            {
                if (ExamTypeId.HasValue)
                    return ExamTypeId.Value;
                return 0;
            }
        }

        public bool IsOnlineExam { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartTime { get; set; }

        public int Duration { get; set; }
        public bool SectionWiseExam { get; set; }

        public DateTime? MarkSubmitDeadline_Teacher { get; set; }
        public DateTime? MarkSubmitDeadline_Admin { get; set; }

        public bool ForClassWiseResultPublished { get; set; }
        public List<ClassWiseResultPublichedDate> ClassWiseColl { get; set; }

        public DateTime? TeacherTime { get; set; }
        public DateTime? AdminTime { get; set; }
        public bool ForPreStudent { get; set; }

        public bool NeedPublished { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? LastPublishDate { get; set; }
        public string LastUpdateMiti { get; set; }
        public string LastPublishMiti { get; set; }
        public int? BranchId { get; set; }
        public string Branch { get; set; }
        public string ResultMiti { get; set; }
        public bool IsActive { get; set; }
        public int? DisplayYearBS { get; set; }
        public int? DisplayYearAD { get; set; }
    }
    public class ExamTypeCollections : System.Collections.Generic.List<ExamType> 
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ClassWiseResultPublichedDate
    {        
        public List<int> ClassIdColl { get; set; }
        public DateTime? ResultDateTime { get; set; }
        public int ClassId { get; set; }
        public int SNo { get; set; }
        public DateTime? ResultTime { get; set; }

        public DateTime? MarkSubmitDeadline_Teacher { get; set; }
        public DateTime? TeacherTime { get; set; }
    }

    public class ExamTypeWiseTemplate : ResponeValues
    {
        public int? ExamTypeId { get; set; }
        public int? ExamTypeGroupId { get; set; }
        public int ClassId { get; set; }
        public int? ReportTemplateId { get; set; }
        public int? AdmitCardTemplateId { get; set; }
        public string ReportName { get; set; }
        public string ClassName { get; set; }

        public string AdmitCardReportName { get; set; }
    }
    public class ExamTypeWiseTemplateCollections : System.Collections.Generic.List<ExamTypeWiseTemplate> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
