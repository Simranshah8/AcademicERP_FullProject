using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Admin
{
    public class ClassWiseFeeSummary
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int NoOfStudent { get; set; }
        public double PreviousDues { get; set; }
        public double CurrentDues { get; set; }
        public double PaidAmount { get; set; }
        public double DiscountAmt { get; set; } 
        public double BalanceAmt { get; set; }
        public int? SectionId { get; set; }
        public int? ClassYearId { get; set; }
        public int? SemesterId { get; set; }
        public string Section { get; set; }
        public string ClassYear { get; set; }
        public string Semester { get; set; }
        public int C_SNo { get; set; }
        public int R_SNo { get; set; }

        public System.Collections.Generic.IEnumerable<ClassWiseFeeSummary> ChieldColl { get; set; }
    }
    public class ClassWiseFeeSummaryCollections : System.Collections.Generic.List<ClassWiseFeeSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class FeeHeadingWiseFeeSummary
    { 
        public string FeeItemName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Dues { get; set; }

        public double DrDiscountAmt { get; set; }
        public double CrDiscountAmt { get; set; }
    }
    public class FeeHeadingWiseFeeSummaryCollections : System.Collections.Generic.List<FeeHeadingWiseFeeSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
