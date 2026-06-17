using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class BillMissingStudent
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string StudentType { get; set; }
        public DateTime? AdmitDate { get; set; }
        public string AdmitMiti { get; set; }
        public string Batch { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Semester { get; set; } = "";
    }
    public class BillMissingStudentCollections : System.Collections.Generic.List<BillMissingStudent>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
