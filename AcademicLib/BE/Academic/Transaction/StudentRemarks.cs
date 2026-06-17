using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class StudentRemarks : ResponeValues
    {
        public StudentRemarks()
        {
            RemarksFor = REMARKSFOR.MERITS;
        }
        public int? TranId { get; set; }
        public int StudentId { get; set; }
        public DateTime ForDate { get; set; }
        public int RemarksTypeId { get; set; }
        public string Remarks { get; set; }
        public string FilePath { get; set; }
        public int? EmployeeId { get; set; }
        public REMARKSFOR RemarksFor { get; set; }
        public double Point { get; set; }
    }
    public class StudentRemarksCollections : System.Collections.Generic.List<StudentRemarks>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum REMARKSFOR
    {
        MERITS=1,
        DEMERITS=2,
        OTHERS=3
    }
}
