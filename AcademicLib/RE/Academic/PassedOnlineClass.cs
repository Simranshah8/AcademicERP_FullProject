using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class PassedOnlineClass : RunningClass
    {
        public PassedOnlineClass()
        {
            ContactNo = "";
            FirstJoinAt = "";
            LastJoinAt = "";
            SectionName = "";
        }
        public string ContactNo { get; set; }
        public int NoOfStudent { get; set; }
        public int NoOfPresent { get; set; }
        public string FirstJoinAt { get; set; }
        public string LastJoinAt { get; set; }
        public int PresentMinute { get; set; }

        public DateTime ForDate { get; set; }
        public string TeacherPhotoPath { get; set; }
    }

    public class PassedOnlineClassCollections : System.Collections.Generic.List<PassedOnlineClass>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
