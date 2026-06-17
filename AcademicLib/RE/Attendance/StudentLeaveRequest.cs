using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class StudentLeaveRequest : EmpLeaveRequest
    {
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Gender { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }        
        public string PhotoPath { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; } 
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public int StudentId { get; set; }

        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }
    public class StudentLeaveRequestCollections : System.Collections.Generic.List<StudentLeaveRequest> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
