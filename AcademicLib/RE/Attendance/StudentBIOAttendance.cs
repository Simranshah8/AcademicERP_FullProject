using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class StudentBIOAttendance
    {
        public int SNo { get; set; }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; } 
        public int RollNo { get; set; }
        public string DayName { get; set; }
        public DateTime EffectiveDate_AD { get; set; }
        public string EffectiveDate_BS { get; set; }
        public string OnDutyTime { get; set; }
        public string OffDutyTime { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public int LateIn { get; set; }
        public int BeforeOut { get; set; } 
        public int TotalMin { get; set; }
        public string AttendanceType { get; set; }

        public string LateInStr { get; set; }
        public string BeforeOutStr { get; set; }
        public string TotalMinStr { get; set; }

        public bool IsPresent { get; set; }
        public bool IsWeekEnd { get; set; }
        public bool IsHoliday { get; set; }        
        public int TotalDays { get; set; }
        public bool OnLeave { get; set; }
        public string LeaveRemarks { get; set; }
        public bool IsEvent { get; set; }
        public string EventRemarks { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }

        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public string PhotoPath { get; set; }
        public bool IsAbsent { get; set; }


    }
    public class StudentBIOAttendanceCollections : System.Collections.Generic.List<StudentBIOAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
