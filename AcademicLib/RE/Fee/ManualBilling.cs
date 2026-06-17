using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class ManualBilling
    {

        public int TranId { get; set; }
        public int AutoNumber { get; set; }
        public DateTime BillDate { get; set; }
        public string BillMiti { get; set; }
        public string MonthName { get; set; }
        public string FeeItem { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double DiscountPer { get; set; } 
        public double DiscountAmt { get; set; }
        public double PayableAmt { get; set; }
        public string Remarks { get; set; }

        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string BillingType { get; set; }

        public string ClassSection
        {
            get
            {
                string val = "";

                if (!string.IsNullOrEmpty(ClassName))
                    val = val + ClassName.Trim();

                if (!string.IsNullOrEmpty(SectionName))
                    val = val+" "+ SectionName.Trim();

                return val;
            }
        }

        public int? ClassId { get; set; }
        public int? SectionId { get; set; }

        public bool IsCancel { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }

    public class ManualBillingCollections : System.Collections.Generic.List<ManualBilling>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ManualBillingDetails
    {
        public int TranId { get; set; }
        public int AutoNumber { get; set; }
        public DateTime BillingDate { get; set; }
        public string BillingMiti { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string FeeItem { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double DiscountAmt { get; set; }
        public double PayableAmt { get; set; }
        public double PaidAmt { get; set; }
        public double DuesAmt { get; set; }
        public string Remarks { get; set; }
        public string RefNo { get; set; }
        public string AcademicYear { get; set; }
        public string UserName { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogMiti { get; set; }
        public string ForMonth { get; set; }
        public string BillingType { get; set; }

        public bool IsCancel { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
    }
    public class ManualBillingDetailsCollections : System.Collections.Generic.List<ManualBillingDetails>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
}
