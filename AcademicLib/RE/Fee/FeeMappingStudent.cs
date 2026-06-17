using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeMappingStudent
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; } 
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string FeeItem { get; set; }
        public string Nature { get; set; }
    }
    public class FeeMappingStudentCollections : System.Collections.Generic.List<FeeMappingStudent>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
