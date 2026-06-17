using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class StudentForExam
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string BoardRegNo { get; set; }
        public string BoardName { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SymbolNo { get; set; }
        public string SubjectDetails { get; set; }
        public string SubjectCodeDetails { get; set; }
        public string SubjectDetailsWExamDate { get; set; }
        public string SubjectDetailsWExamDateTime { get; set; }
        public int TotalSubject { get; set; }
        public string Room { get; set; }
        public string RowName { get; set; }
        public int BenchNo { get; set; }
        public string BenchOrdinalNo { get; set; }
        public int SeatCol { get; set; }
        public string ExamShiftName { get; set; }
    }
    public class StudentForExamCollections : System.Collections.Generic.List<StudentForExam>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
