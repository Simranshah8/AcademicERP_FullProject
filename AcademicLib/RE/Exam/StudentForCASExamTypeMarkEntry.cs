using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class StudentForCASExamTypeMarkEntry
    {
        public StudentForCASExamTypeMarkEntry()
        {
            RegdNo = "";
            ClassName = "";
            SectionName = "";
            Name = "";
            Remarks = "";
            EmployeeName = "";
            CASTypeName = "";
            IsEditable = true;
        }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public double ObtainMark { get; set; }
        public string Remarks { get; set; }
        public int? EmployeeId { get; set; }
        public double Mark { get; set; }
        public string EmployeeName { get; set; }
        public int CASTypeId { get; set; } 
        public string CASTypeName { get; set; }
        public int Under { get; set; }
        public int Scheme { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ExamTypeId { get; set; }
        public bool IsEditable { get; set; }
        public string Subject { get; set; }
        public string PhotoPath { get; set; }
        public int? PresentDays { get; set; }
        public int? WorkingDays { get; set; }
        public string Formula { get; set; }
        public int TranId { get; set; }
        public string SymbolNo { get; set; }
    }

    public class StudentForCASExamTypeMarkEntryCollections : System.Collections.Generic.List<StudentForCASExamTypeMarkEntry>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
