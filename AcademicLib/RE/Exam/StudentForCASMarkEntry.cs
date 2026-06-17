using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class StudentForCASMarkEntry
    {
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public double ObtainMark { get; set; }
        public string Remarks { get; set; }

        public int SubjectId { get; set; }
        public int CASTypeId { get; set; }
        public DateTime ExamDate { get; set; }
        public int? EmployeeId { get; set; }
        public double Mark { get; set; }
        public int? ExamTypeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsAbsent { get; set; }
        public string CASType { get; set; }
        public string Subject { get; set; }
        public string PhotoPath { get; set; }
    }
    public class StudentForCASMarkEntryCollections : System.Collections.Generic.List<StudentForCASMarkEntry>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
