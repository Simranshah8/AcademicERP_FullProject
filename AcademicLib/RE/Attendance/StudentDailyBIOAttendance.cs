using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class StudentDailyBIOAttendance
    {
        public StudentDailyBIOAttendance()
        {
            ClassName = "";
            SectionName = "";
        }
        public int SNo { get; set; }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public int EnrollNo { get; set; }
        public long CardNo { get; set; }
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string OnDutyTime { get; set; }
        public string OffDutyTime { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public int LateIn { get; set; }
        public int BeforeOut { get; set; }
        public string LateInStr { get; set; }
        public string BeforeOutStr { get; set; }
        public int TotalMinutes { get; set; }
        public string WorkingHR { get; set; }
        public string ClassSec
        {
            get
            {
                return (((((ClassName + " " + SectionName).Trim()+" "+Batch).Trim() + " " + Semester).Trim() + " " + ClassYear).Trim()).Trim();
            }
        }
        public string Attendance { get; set; }
        public string Gender { get; set; }


        public string Batch { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";

        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }
    public class StudentDailyBIOAttendanceCollections : System.Collections.Generic.List<StudentDailyBIOAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
