using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeItemWiseDiscountSetup : ResponeValues
    {
        public FeeItemWiseDiscountSetup()
        {
            MonthIdColl = new List<FeeItemWiseDiscountSetupMonth>();
        }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public int TranId { get; set; }
        public int StudentId { get; set; }
        public int FeeItemId { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public string Remarks { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public List<FeeItemWiseDiscountSetupMonth> MonthIdColl { get; set; }
    }
    public class FeeItemWiseDiscountSetupCollections : System.Collections.Generic.List<FeeItemWiseDiscountSetup>
    {
        public FeeItemWiseDiscountSetupCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class FeeItemWiseDiscountSetupMonth
    {
        public int MonthId { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
    }
}
