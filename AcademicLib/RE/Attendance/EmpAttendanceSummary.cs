using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class EmpAttendanceSummary : ResponeValues
    {      
        public int? EmployeeId { get; set; }
        public int EnrollNumber { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string LevelName { get; set; }
        public string ServiceType { get; set; }
        public string GroupName { get; set; }
        public int TotalDays { get; set; }
        public int TotalWeekEnd { get; set; }
        public int TotalPresent { get; set; }
        public int TotalLeave { get; set; }
        public int TotalHoliday { get; set; }

        public int WeekendPresent { get; set; }
        public int HolidayPresent { get; set; }
        public int LeavePresent { get; set; }
        public string Category { get; set; }
        public string CompanyName { get; set; }
        public double WorkingDuration { get; set; }
        public double OTDuration { get; set; }
        public double SinglePunchDeduction { get; set; }
        public double EarlyInMinutes { get; set; }
        public double LateInMinutes { get; set; }
        public double EarlyOutMinutes { get; set; }
        public double DelayOutMinutes { get; set; }
        public int SinglePunchCount { get; set; }
        public int EarlyInMinutesCount { get; set; }
        public int LateInMinutesCount { get; set; }
        public int EarlyOutMinutesCount { get; set; }
        public int DelayOutMinutesCount { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public int TotalAbsent { get; set; }
        public string WorkingShift { get; set; }
        public DateTime ForDate { get; set; }
        public string TeacherPhotoPath { get; set; }
    }

    public class EmpAttendanceSummaryCollections : System.Collections.Generic.List<EmpAttendanceSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
