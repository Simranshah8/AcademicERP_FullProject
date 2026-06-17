using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class StudentOpening : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int TranId { get; set; }
        public int StudentId { get; set; }
        public int FeeItemId { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }

        public string RegNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string FeeItemName { get; set; } = "";
        public int? BatchId { get; set; }
        public DateTime? VoucherDate { get; set; }
    }
    public class StudentOpeningCollections : System.Collections.Generic.List<StudentOpening>
    {
        public StudentOpeningCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ImportStudentOpening
    {
        public string FeeItem { get; set; }
        public string RegdNo { get; set; }        
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }        
        public double Amount { get; set; }
        public string Remarks { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public DateTime? VoucherDate { get; set; }
    }
}
