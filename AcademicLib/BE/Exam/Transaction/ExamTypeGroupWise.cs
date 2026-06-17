using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class ExamTypeGroupWise : ResponeValues
    {
        public int ExamTypeGroupWiseId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int ExamTypeGroupId { get; set; }
        public int TotalFailSubject { get; set; }
        public bool IsSubjectWise { get; set; }
        public int SubjectId { get; set; }
        public bool IsTH { get; set; }
        public bool IsPR { get; set; }
    }
    public class ExamTypeGroupWiseCollections : List<ExamTypeGroupWise> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
 