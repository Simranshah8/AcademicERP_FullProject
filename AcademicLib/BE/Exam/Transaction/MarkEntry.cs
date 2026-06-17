using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.BusinessEntity.Exam.Transaction
{
    public class MarkEntry :  ResponeValues
    {
        public int MarksEntryId { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public int SectionId { get; set; }
        public DateTime ExamDate { get; set; }
        public bool IsColumnwiseFocus { get; set; }
        public int FullMarksTH { get; set; }
        public int FullMarksPR { get; set; }
        public int PassMarksPR { get; set; }
    }
    public class MarkEntryCollections : System.Collections.Generic.List<MarkEntry> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class MarksEntryDetails
    {
       public int TranId { get; set; }
       public int MarksEntryId { get; set; }
       public int StudentId { get; set; }
       public float ObtainMarksTH { get; set; }
       public float ObtainMarksPR { get; set; }
    }
    public class MarksEntryDetailsCollections : System.Collections.Generic.List<MarksEntryDetails> { }

   
}
 