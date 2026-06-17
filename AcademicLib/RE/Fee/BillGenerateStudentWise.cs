using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class BillGenerateStudentWise
    {
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public int RollNo { get; set; } 
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public DateTime BillDateAD { get; set; }
        public string BillDateBS { get; set; }
        public int FromMonth { get; set; }
        public int ToMonth { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogDateBS { get; set; }
        public int BillNoFrom { get; set; }
        public int BillNoTo { get; set; }
        public double Amount { get; set; }
        public double DiscountAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double Total { get; set; }
        public string FromMonthName { get; set; }
        public string ToMonthName { get; set; }

        public string Batch { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Semester { get; set; } = "";
        public int InvoiceNoFrom { get; set; }
        public int InvoiceNoTo { get; set; }
    }

    public class BillGenerateStudentWiseCollections : System.Collections.Generic.List<BillGenerateStudentWise>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
