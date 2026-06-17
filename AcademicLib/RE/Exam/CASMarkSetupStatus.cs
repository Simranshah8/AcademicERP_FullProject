using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class CASMarkSetupStatus
    {
        public int SNo { get; set; }
        public int TranId { get; set; } 
        public string SubjectName { get; set; } 
        public string SubjectCode { get; set; }
        public double FullMark { get; set; }
        public DateTime SubmitDateTime { get; set; }
        public string SubmitMiti { get; set; }
        public string UserName { get; set; }
    }
    public class CASMarkSetupStatusCollections : System.Collections.Generic.List<CASMarkSetupStatus>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
