using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.FrontDesk
{
    public class AttendanceFollowup
    {
        public int TranId { get; set; }
        public string FollowupMiti { get; set; }
        public string FollowUpTo { get; set; }
        public string ContactNo { get; set; }
        public string FollowUpStatus { get; set; }
        public string FollowUpRemarks { get; set; }
        public string FollowupBy { get; set; }
    }
    public class AttendanceFollowupCollections : System.Collections.Generic.List<AttendanceFollowup>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
