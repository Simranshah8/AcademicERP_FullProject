using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Creation
{
    public class ReExamType : BE.Academic.Common
    {
        public ReExamType()
        {
            ClassWiseColl = new List<ClassWiseResultPublichedDate>();
        }
        public int? ReExamTypeId { set; get; }
        public DateTime? ResultDate { set; get; }
        public DateTime? ResultTime { set; get; }

        public int id
        {
            get
            {
                if (ReExamTypeId.HasValue)
                    return ReExamTypeId.Value;
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
        public int ExamTypeId { get; set; }
        public string ExamTypeName { get; set; }
        public bool IsActive { get; set; }
        public string ResultDateBS { get; set; }
        //Added field
        public int? DisplayYearBS { get; set; }
        public int? DisplayYearAD { get; set; }
    }
    public class ReExamTypeCollections : System.Collections.Generic.List<ReExamType>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
   
}
