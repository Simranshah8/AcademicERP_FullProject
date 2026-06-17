using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class MarkSetupStatus
    {
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public bool IsPending { get; set; }
        public DateTime? CreateDateTime_AD { get; set; }
        public string CreateDateTime_BS { get; set; }
        public string UserName { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; }
        public int TotalSubject { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class MarkSetupStatusCollections : System.Collections.Generic.List<MarkSetupStatus> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
