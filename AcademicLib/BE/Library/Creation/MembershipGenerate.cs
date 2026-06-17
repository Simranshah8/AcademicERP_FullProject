using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Library.Creation
{
    public class MembershipGenerate : ResponeValues
    {
        public int ForStudentEmp { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public int MembershipNoAs { get; set; }
        public bool ReGenerate { get; set; }
    }
}
