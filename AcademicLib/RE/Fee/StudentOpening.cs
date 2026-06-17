using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class ClassWiseOpening
    {
        public ClassWiseOpening()
        {
            ClassName = "";
            SectionName = "";
            Semester = "";
            ClassYear = "";
        }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int FeeItemId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FeeItemName { get; set; }
        public double Amount { get; set; }

        public string ClassSec
        {
            get
            {
                return ((ClassName + " " + (string.IsNullOrEmpty(SectionName) ? "" : SectionName)).Trim()+" "+Semester+" "+ClassYear).Trim();
            }
        }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Batch { get; set; } = "";
        public int? BatchId { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string VoucherMiti { get; set; }
    }
    public class ClassWiseOpeningCollections : System.Collections.Generic.List<ClassWiseOpening>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentOpening
    {
        public int StudentId { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FeeItemName { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string VoucherMiti { get; set; }
    }
    public class StudentOpeningCollections : System.Collections.Generic.List<StudentOpening>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
