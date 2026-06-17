using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Security
{
    public class LastLoginLog : ResponeValues
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Faculty { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string PublicIP { get; set; }
        public string PCName { get; set; }
        public string AppVersion { get; set; }
        public DateTime? LogDateTime { get; set; }
        public string LogMitiTime { get; set; }
        public int BeforeDay { get; set; }
        public int RollNo { get; set; }
    }

    public class LastLoginLogCollections : List<LastLoginLog>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
