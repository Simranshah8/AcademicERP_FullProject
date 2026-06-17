using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class SiblingStudent
    {
        public string P_ClassName { get; set; }
        public string P_SectionName { get; set; }
        public int P_RollNo { get; set; }
        public string P_RegNo { get; set; }
        public string P_Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Remarks { get; set; }
        public string F_Email { get; set; }
        public string M_Email { get; set; }
        public string G_Email { get; set; }

    }
    public class SiblingStudentCollections : System.Collections.Generic.List<SiblingStudent> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
