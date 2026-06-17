using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class TeacherQuota
    {       
        public int? EmployeeId { get; set; }
        public int? RemainingQuota { get; set; }
        public bool CanBlock { get; set; }
    }
    public class TeacherQuotaCollections : System.Collections.Generic.List<TeacherQuota>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
