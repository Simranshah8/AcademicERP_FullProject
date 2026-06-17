using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class BillGenerateClassWise
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public DateTime BillDateAD { get; set; }
        public string BillDateBS { get; set; }
        public int BillNoFrom { get; set; }
        public int BillNoTo { get; set; } 
        public int NoOfStudent { get; set; }
        public double DiscountAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double TotalAmt { get; set; } 
        public string GenerateBy { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogDateBS { get; set; }
        public int InvoiceNoFrom { get; set; }
        public int InvoiceNoTo { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public int? BatchId { get; set; }
        public string MonthName { get; set; }
    }
    public class BillGenerateClassWiseCollections : System.Collections.Generic.List<BillGenerateClassWise> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class BillGenerateFee
    {
        public int FeeItemId { get; set; }
        public string Name { get; set; }

        public double Qty { get; set; }
        public double Amount { get; set; }
        public double DisAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double PayableAmt { get; set; }
    }
    public class BillGenerateFeeCollections : System.Collections.Generic.List<BillGenerateFee>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
