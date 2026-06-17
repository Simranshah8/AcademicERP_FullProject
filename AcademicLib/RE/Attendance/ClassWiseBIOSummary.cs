using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class ClassWiseBIOSummary
    {
        public int SNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int NoOfStudent { get; set; }
        public int TotalDays { get; set; } 
        public int Present { get; set; } 
        public int Absent { get; set; }
        public int Leave { get; set; }
        public double Present_Per { get; set; }
        public double Absent_Per { get; set; }
        public double Leave_Per { get; set; }
    }
    public class ClassWiseBIOSummaryCollections : System.Collections.Generic.List<ClassWiseBIOSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
