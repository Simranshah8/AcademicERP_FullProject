using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class ClassAndFeeItemWise
    {
        public int ClassOrderNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int FeeOrderNo { get; set; }
        public string FeeItemName { get; set; }
        public double Opening { get; set; }
        public double DrAmt { get; set; }
        public double DrDiscountAmt { get; set; }
        public double DrTax { get; set; }
        public double DrFineAmt { get; set; }
        public double DrTotal { get; set; }
        public double CrDiscountAmt { get; set; }
        public double CrAmt { get; set; } 
        public double CrFineAmt { get; set; }
        public double CrTotal { get; set; }
        public double BalanceAmt { get; set; }
    }
    public class ClassAndFeeItemWiseCollections : System.Collections.Generic.List<ClassAndFeeItemWise>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
