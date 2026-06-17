using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.BusinessEntity.Exam.Transaction
{
   public class MarkEntryStudentWise: ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public float ObtainMarksTH { get; set; }
        public float ObtainMarksPR { get; set; }
        public float GraceMarksTH { get; set; }
        public float GraceMarksPR { get; set; }
    }
    public class MarkEntryStudentWiseCollections : System.Collections.Generic.List<MarkEntryStudentWise> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
