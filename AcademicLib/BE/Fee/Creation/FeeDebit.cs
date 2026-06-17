using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeDebit : ResponeValues
    {
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int MonthId { get; set; }
        public int FeeItemId { get; set; }
        public double Rate { get; set; }
        public double DiscountAmt { get; set; }
        public double DiscountPer { get; set; }
        public double TaxAmt { get; set; }
        public double FineAmt { get; set; }
        public double PayableAmt { get; set; }
        public string Remarks { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }
    public class FeeDebitCollections : System.Collections.Generic.List<FeeDebit>
    {
        public FeeDebitCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
