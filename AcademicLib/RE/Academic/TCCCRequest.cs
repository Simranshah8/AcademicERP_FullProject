using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class TCCCRequest  
    {
        public int SNo { get; set; }
        public int StudentId { get; set; }
        public string RegNo { get; set; }
        public int RollNo { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string RequestMiti { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string UserAgent { get; set; }
        public bool IsIssueTC { get; set; }
        public bool IsIssueCC { get; set; }
        public string BoardRegNo { get; set; }        
    }

    public class TCCCRequestCollections : System.Collections.Generic.List<TCCCRequest>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}
