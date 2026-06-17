using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class OnlineClassAdmin 
    {
        public OnlineClassAdmin()
        {
            RunningColl = new List<OnlineClass>();
            PassedColl = new List<PassCollData>();
            MissedColl = new List<OnlineClass>();
            ConductColl = new List<OnlineClass>();
        }
        public List<OnlineClass> RunningColl { get; set; }
        public List<PassCollData> PassedColl { get; set; }

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