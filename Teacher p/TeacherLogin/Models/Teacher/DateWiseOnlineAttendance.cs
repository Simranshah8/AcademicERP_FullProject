using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class DateWiseOnlineAttendance
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
    }
    public class DateWiseOnlineAttendanceCollections : System.Collections.Generic.List<DateWiseOnlineAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}