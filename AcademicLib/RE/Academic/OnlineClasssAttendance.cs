using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class OnlineClasssAttendance
    {
        public OnlineClasssAttendance()
        {
            Name = "";
            ClassName = "";
            SectionName = "";            
            PhotoPath = "";
            FatherName = "";
            MotherName = "";
            ContactNo = "";
            StartTime = "";
            EndTime = "";
            FirstJoinAt = "";
            LastJoinAt = "";
            UserName = "";
            
        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; } 
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string PhotoPath { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string FirstJoinAt { get; set; }
        public string LastJoinAt { get; set; } 
        public int LateMinute { get; set; }
        public int AttendanceType { get; set; }
        public int Duration { get; set; }

        public string UserName { get; set; }
        public int UserId { get; set; }
    }
    public class OnlineClasssAttendanceCollections : System.Collections.Generic.List<OnlineClasssAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
