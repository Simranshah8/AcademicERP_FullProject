using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeIncomeClassWise
    {
        public int ClassOrderNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int FeeItemOrderNo { get; set; }
        public string FeeItemName { get; set; }
        public double ReceivedAmt { get; set; }
        public double DiscountAmt { get; set; }
    }
    public class FeeIncomeClassWiseCollections : System.Collections.Generic.List<FeeIncomeClassWise>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}