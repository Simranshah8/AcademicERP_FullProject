using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.OnlineClass
{
    public class DateWiseAttendance
    {
        public int StudentId { get; set; }
        public string RegNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public int Period { get; set; }
        public DateTime? ClassStartTime { get; set; }
        public DateTime? ClassEndTime { get; set; }
        public DateTime? JoinDateTime { get; set; }
        public DateTime? LeftDateTime { get; set; }

        public DateTime? ForDate_AD { get; set; }
        public string ForDate_BS { get; set; }

        public string Attendance { get; set; }
        public int NY { get; set; }
        public int NM { get; set; }
        public int ND { get; set; }

        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class DateWiseAttendanceCollections : System.Collections.Generic.List<DateWiseAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
