using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeIncomeStudentWise
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
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
        public string VoucherNo { get; set; }
        public string RefNo { get; set; }
        public string VoucherDate { get; set; }

        public string Details { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
    }
    public class FeeIncomeStudentWiseCollections : System.Collections.Generic.List<FeeIncomeStudentWise>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}