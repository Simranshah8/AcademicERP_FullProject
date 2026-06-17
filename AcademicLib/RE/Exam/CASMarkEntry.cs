using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class CASMarkEntry
    {
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string EmployeeName { get; set; }
        public string ExamType { get; set; }
        public string CASType { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public double Mark { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamMiti { get; set; }
        public double ObtainMark { get; set; } 
        public string Remarks { get; set; }
        public string UserName { get; set; }
        public bool IsAbsent { get; set; }
    }
    public class CASMarkEntryCollections : System.Collections.Generic.List<CASMarkEntry>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class CASMarkEntrySubject 
    {
        public int SubjectId { get; set; }
        public int CASTypeId { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamMiti { get; set; }
        public double Mark { get; set; }
        public string SubjectName { get; set; }
        public string CASTypeName { get; set; }
    }
    public class CASMarkEntrySubjectCollections : System.Collections.Generic.List<CASMarkEntrySubject> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
