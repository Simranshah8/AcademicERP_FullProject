using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class StudentManualDailyAttendance
    {
        public StudentManualDailyAttendance()
        {
            RegdNo = "";
            ClassName = "";
            SectionName = "";
            Name = "";
            FatherName = "";
            ContactNo = "";
            Attendance = "";
            Remarks = "";

        }
        public int SNo { get; set; }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }      
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Attendance { get; set; }
        public int LateMin { get; set; }
        public string Remarks { get; set; }
        
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string MotherName { get; set; }
        public string M_Contact { get; set; }
        public string GuardianName { get; set; }
        public string G_ContactNo { get; set; }
        public string PersonalContactNo { get; set; }
        public string ClassSection
        {
            get
            {
                string val = "";

                if (!string.IsNullOrEmpty(ClassName))
                    val = ClassName;

                if (!string.IsNullOrEmpty(SectionName))
                    val = val + " " + SectionName;

                return val;

            }
        }
        public int UserId { get; set; }
         
    }
    public class StudentManualDailyAttendanceCollections : System.Collections.Generic.List<StudentManualDailyAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
