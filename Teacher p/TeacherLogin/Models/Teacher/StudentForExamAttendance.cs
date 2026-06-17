using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class StudentForExamAttendance
    {
        public StudentForExamAttendance()
        {
            Name = "";
            RegdNo = "";
            BoardRegdNo = "";
            SymbolNo = "";
            PhotoPath = "";
        }
        public int ExamTypeId { get; set; }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string SymbolNo { get; set; }
        public string PhotoPath { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
    public class StudentForExamAttendanceCollections : System.Collections.Generic.List<StudentForExamAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}