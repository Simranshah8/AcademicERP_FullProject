using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class RunningClasses
    {
        public int TranId { get; set; }
        public int PlatformType { get; set; }
        public string ShiftName { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }

        public string SectionName { get; set; }
        public DateTime StartDateTime_AD { get; set; }
        public string StartDate_BS { get; set; }
        public object EndDateTime_AD { get; set; }
        public object EndDate_BS { get; set; }
        public bool IsRunning { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Link { get; set; }
        public int RId { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

        public string TeacherName { get; set; }
    }
}