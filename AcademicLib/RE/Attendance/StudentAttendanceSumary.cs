using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Attendance
{
    public class StudentAttendanceSumary : ResponeValues
    {
        public int? StudentId { get; set; }
        public string RegNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public int? RollNo { get; set; }
        public string EMSId { get; set; } = "";
        public string LeftStatus { get; set; } = "";
        public string Batch { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public int? TotalDays { get; set; }
        public int? TotalWeekEnd { get; set; }
        public int? TotalHoliday { get; set; }
        public int? TotalPresent { get; set; }
        public int? TotalLeave { get; set; }
        public int? TotalAbsent { get; set; }
        public int? SchoolDays { get; set; }
    }
    public class StudentAttendanceSumaryCollections: List<StudentAttendanceSumary>
    {
        public StudentAttendanceSumaryCollections()
        {
            ResponseMSG = "";
        }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }

    }
}