using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeSummary_PC
    {
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; }
        public string M_ContactNo { get; set; }
        public string Address { get; set; }
        public bool IsLeft { get; set; }
        public bool IsFixedStudent { get; set; }
        public bool IsHostel { get; set; }
        public bool IsNewStudent { get; set; }
        public bool IsTransport { get; set; }
        public double Opening_P { get; set; }
        public double DrAmt_P { get; set; }
        public double DrDiscountAmt_P { get; set; }
        public double DrFineAmt_P { get; set; }
        public double DrTax_P { get; set; }
        public double DrTotal_P { get; set; }
        public double CrAmt_P { get; set; }
        public double CrDiscountAmt_P { get; set; }
        public double CrFineAmt_P { get; set; }
        public double TotalDebit_P { get; set; }
        public double TotalCredit_P { get; set; }
        public double TotalDues_P { get; set; }
        public int UserId { get; set; }
        public string MonthName { get; set; } 
        public long CardNo { get; set; }
        public int EnrollNo { get; set; }
        public string LedgerPanaNo { get; set; }
        public int ClassOrderNo { get; set; }
        public string FeeItemName { get; set; }
        public string RouteName { get; set; }
        public string PointName { get; set; }
        public string BoardersName { get; set; }
        public int AutoNumber { get; set; }
        public double DrAmt_C { get; set; }
        public double DrDiscountAmt_C { get; set; }
        public double DrFineAmt_C { get; set; }
        public double DrTax_C { get; set; }
        public double DrTotal_C { get; set; }
        public double CrAmt_C { get; set; }
        public double CrDiscountAmt_C { get; set; }
        public double CrFineAmt_C { get; set; }
        public double TotalDebit_C { get; set; }
        public double TotalCredit_C { get; set; }
        public double TotalDues_C { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }

    }
    public class FeeSummary_PCCollections : System.Collections.Generic.List<FeeSummary_PC>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
