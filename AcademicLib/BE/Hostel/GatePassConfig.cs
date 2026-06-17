using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Hostel
{
    public class GatePassConfig : ResponeValues
    {
        public int NumberingMethod { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartNo { get; set; }
        public int NumericalPartWidth { get; set; }
        public int? BranchId { get; set; }
        public string Declaration { get; set; }
    }
}