using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class StudentFeeItemDetails
    {
        public StudentFeeItemDetails()
        {
            SectionName = "";
            RegdNo = "";
        }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string FeeItemName { get; set; } 
        public double Amount { get; set; }

        public int BillNo { get; set; }
        public double DiscountAmt { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }

        public double PDues { get; set; }
        public string Batch { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Semester { get; set; } = "";
        public string InvoiceNo { get; set; }
    }
    public class StudentFeeItemDetailsCollections : System.Collections.Generic.List<StudentFeeItemDetails>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
