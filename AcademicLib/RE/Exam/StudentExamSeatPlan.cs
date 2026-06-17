using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class StudentExamSeatPlan
    {
        public StudentExamSeatPlan()
        {
            ExamType = "";
            ExamShift = "";
            Room = "";
            ClassName = "";
            SectionName = "";
            RegdNo = "";
            SymbolNo = "";
            Name = "";
            FatherName = "";
            ContactNo = "";
            ColumnName = "";
        }
        public string ExamType { get; set; }
        public string ExamShift { get; set; }
        public string Room { get; set; }
        public int BenchNo { get; set; }
        public int SeatNo { get; set; }
        public string ColumnName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; } 
        public string SymbolNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }
    }
    public class StudentExamSeatPlanCollections : System.Collections.Generic.List<StudentExamSeatPlan>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
