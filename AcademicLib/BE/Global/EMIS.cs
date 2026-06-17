using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public class EMIS
    {
        public EMIS()
        {
            RegdIdAs = 1;
            TeacherIdAs = 1;
            ClassNameAs = 1;
        }
        public string SchoolCode { get; set; }
        public int AcademicYear { get; set; }

        public int RegdIdAs { get; set; }

        public int TeacherIdAs { get; set; }

        public int ClassNameAs { get; set; }
    }
}
