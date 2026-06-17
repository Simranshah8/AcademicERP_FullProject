using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
  public  class ParentExamTypeGroupWise : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int ParentExamTypeGroupId { get; set; }
        public int TotalFailSubject { get; set; }
        public bool IsSubjectWise { get; set; }
        public int SubjectId { get; set; }
        public bool IsTH { get; set; }
        public bool IsPR { get; set; }
    }
    public class ParentExamTypeGroupWiseCollections : List<ParentExamTypeGroupWise> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
