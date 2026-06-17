using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public class NameAndRole : ResponeValue
    {
        public string Name { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public string Role { get; set; } = "";
        public bool SubjectTeacher { get; set; }
        public bool ClassTeacher { get; set; }
        public bool CoOrdinator { get; set; }
        public bool HOD { get; set; }
    }
}
