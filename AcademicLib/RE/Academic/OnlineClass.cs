using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class OnlineClassAdmin : ResponeValues
    {
        public OnlineClassAdmin()
        {
            RunningColl = new List<OnlineClass>();
            PassedColl = new List<AcademicLib.RE.Academic.PassedOnlineClass>();
            MissedColl = new List<OnlineClass>();
            ConductColl = new List<OnlineClass>();
        }
        public List<OnlineClass> RunningColl { get; set; }
        public List<AcademicLib.RE.Academic.PassedOnlineClass> PassedColl { get; set; }

        public List<OnlineClass> MissedColl { get; set; }
        public List<OnlineClass> ConductColl { get; set; }
    }
    public class OnlineClass
    {
        public int TranId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string ContactNo { get; set; }
        public string StartDateBS { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int Duration { get; set; }
        public string Link { get; set; }
        public bool IsRunning { get; set; }
        public string EndDateBS { get; set; }
        public DateTime? EndDateTime { get; set; } 
        public int TimeDiff { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }      
        public string ClassAttem { get; set; }
        public int Present { get; set; }

        public int UserId { get; set; }
    }
}
