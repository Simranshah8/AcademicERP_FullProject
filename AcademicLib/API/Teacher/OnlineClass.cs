using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class OnlineClass
    {
        public int TranId { get; set; }
        public int UserId { get; set; }
        public int? ClassShiftId { get; set; }
        public int ClassId { get; set; }

        public string SectionIdColl { get; set; }
        public int SubjectId { get; set; }
        public int PlatformType { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Link { get; set; }
        public string Notes { get; set; }
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        public bool IsRunning { get; set; }
        public DateTime EndDateTime { get; set; }
        public string UserIdColl { get; set; }
        public string Message { get; set; }
        public int? ClassGroupId { get; set; }
    }
}
