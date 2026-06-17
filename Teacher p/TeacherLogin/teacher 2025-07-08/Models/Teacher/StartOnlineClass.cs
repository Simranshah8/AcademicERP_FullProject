using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class StartOnlineClass : OnlinePlatForm
    {     
        public int? ClassShiftId { get; set; }
        public int ClassId { get; set; }
        public string SectionIdColl { get; set; }
        public int SubjectId { get; set; }
        public string Notes { get; set; }
        public string StartDateTime { get; set; }
        public int Duration { get; set; }
        public int RId { get; set; }
        public int? ClassGroupId { get; set; }
    }
}