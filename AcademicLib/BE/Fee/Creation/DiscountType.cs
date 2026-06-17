using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class DiscountType : ResponeValues
    {
        public DiscountType()
        {
            DetailsColl = new List<DiscountTypeDetails>();
        }
        public int? DiscountTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public List<DiscountTypeDetails> DetailsColl { get; set; }
    }
    public class DiscountTypeCollections : System.Collections.Generic.List<DiscountType>
    {
        public DiscountTypeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class DiscountTypeDetails
    {
        public DiscountTypeDetails()
        {
            MonthIdColl = new List<int>();
        }
        public int DTranId { get; set; }
        public int DiscountTypeId { get; set; }
        public int SNo { get; set; }
        public int? FeeItemId { get; set; }
        public double DiscountPer { get; set; }
        public double DiscountAmt { get; set; }
        public bool ForAllMonth { get; set; }

        public List<int> MonthIdColl { get; set; }
    }
}
