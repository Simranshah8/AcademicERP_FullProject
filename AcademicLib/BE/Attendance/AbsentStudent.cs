using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Attendance
{
    public class AbsentStudent
    {
        public string RegdNo { get; set; }
        public string RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; } 
        public string ContactNo { get; set; }
        public string Attendance { get; set; }
        public string LateMinutes { get; set; }
        public string Remarks { get; set; }
    }
}
