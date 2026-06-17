using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.OnlineClass
{
    public class EmployeeAttendance
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime? ForDate_AD { get; set; }
        public string ForDate_BS { get; set; }
        public int ScheduleClass { get; set; }
        public int NoOfClassHosted { get; set; }
        public string TranIdColl { get; set; }
    }
    public class EmployeeAttendanceCollections : System.Collections.Generic.List<EmployeeAttendance>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
