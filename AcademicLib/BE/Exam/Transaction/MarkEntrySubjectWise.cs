using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class MarkEntrySubjectWise : ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int ExamTypeId { get; set; }
        public int SubjectId { get; set; }
        public bool IsColumnwiseFocus { get; set; }
        public int StudentId { get; set; }
        public float ObtainMarkTH { get; set; }
        public float ObtainMarksPR { get; set; }
        public float GraceMarkTH { get; set; }
        public float GraceMarkPR { get; set; }
        public string Remarks { get; set; }
    }
    public class MarkEntrySubjectWiseCollections : System.Collections.Generic.List<MarkEntrySubjectWise> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
