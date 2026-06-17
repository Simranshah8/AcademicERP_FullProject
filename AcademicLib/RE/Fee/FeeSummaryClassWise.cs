using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeSummaryClassWise
    {
        public int ClassOrderNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int FeeItemOrderNo { get; set; }
        public string FeeItemName { get; set; }
        public double OpeningAmt { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Discount { get; set; }
    }
    public class FeeSummaryClassWiseCollections : System.Collections.Generic.List<FeeSummaryClassWise>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
