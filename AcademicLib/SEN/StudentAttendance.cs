using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.SEN
{
    public class StudentAttendance : AcademicLib.BE.Global.StudentNotification
    {       
        public DateTime ForDate { get; set; }
        public string InOutMode { get; set; }      
        public string Attendance { get; set; }
        public int LateMin { get; set; }
        public string Remarks { get; set; }
        public string PunchTime { get; set; } = "";  
    }
    public class StudentAttendanceCollections : System.Collections.Generic.List<StudentAttendance>
    {

    }
}
