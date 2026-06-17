using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeSummaryStudentWise
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string Address { get; set; }
        public int FeeItemOrderNo { get; set; }
        public string FeeItemName { get; set; }
        public double OpeningAmt { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Discount { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
    }
    public class FeeSummaryStudentWiseCollections : System.Collections.Generic.List<FeeSummaryStudentWise>
    {

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
