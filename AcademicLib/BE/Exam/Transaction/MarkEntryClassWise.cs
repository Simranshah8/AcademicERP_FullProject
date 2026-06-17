using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Transaction
{
   public class MarkEntryClassWise :  ResponeValues
    {
        public int TranId { get; set; }
        public int ClassId { get; set; }
        public int ExamTypeId { get; set; }
        public bool IsColumnwiseFocus { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public float TotalObtainMark { get; set; }
        public string Remarks { get; set; }
    }
    public class MarkEntryClassWiseCollections : System.Collections.Generic.List<MarkEntryClassWise> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
