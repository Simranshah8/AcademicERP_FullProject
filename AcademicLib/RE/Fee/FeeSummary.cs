using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class FeeSummary
    {
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; }
        public string M_ContactNo { get; set; }
        public string Address { get; set; }
        public bool IsLeft { get; set; }
        public bool IsFixedStudent { get; set; }
        public bool IsHostel { get; set; }
        public bool IsNewStudent { get; set; }
        public bool IsTransport { get; set; }
        public double Opening { get; set; }
        public double DrAmt { get; set; }
        public double DrDiscountAmt { get; set; }
        public double DrFineAmt { get; set; }
        public double DrTax { get; set; }
        public double DrTotal { get; set; }
        public double CrAmt { get; set; }
        public double CrDiscountAmt { get; set; }
        public double CrFineAmt { get; set; }
        public double TotalDebit { get; set; }
        public double TotalCredit { get; set; }
        public double TotalDues { get; set; }
        public int UserId { get; set; }
        public string MonthName { get; set; }

        public long CardNo { get; set; }
        public int EnrollNo { get; set; }
        public string LedgerPanaNo { get; set; }
        public int ClassOrderNo { get; set; }
        public string FeeItemName { get; set; }
        public string RouteName { get; set; }
        public string PointName { get; set; }
        public string BoardersName { get; set; }

        public int AutoNumber { get; set; }

        public DateTime? LastReceiptDate { get; set; }
        public string LastReceiptMiti { get; set; }
        public string LastReceiptNo { get; set; }
        public double LastReceiptAmt { get; set; }

        public double FutureDR { get; set; }
        public double FutureCR { get; set; }

        public double FutureDues 
        { 
            get
            {
                return TotalDues  - FutureCR;
            } 
        }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string Email { get; set; }
        public bool IsDefaulter { get; set; }

        public int RefTranId { get; set; }
        public string FollowupRemarks { get; set; }
        public DateTime NextFollowupDateTime { get; set; }
        public string NextFollowupMiti { get; set; }
        public string NextFollowupBy { get; set; }
        public string ClosedRemarks { get; set; }
        public string ClosedBy { get; set; }
        public DateTime ClosedDate { get; set; }
        public string ClosedMiti { get; set; }
        public DateTime? LeftDate { get; set; }
        public string LeftMiti { get; set; }
        public string LeftReason { get; set; }
        public string HouseName { get; set; } = "";
        public string HouseDress { get; set; } = "";         
        public string VehicleName { get; set; } = "";
        public string VehicleNumber { get; set; } = "";
        public string AcademicYear { get; set; }
        public double Closing { get; set; }
        public int FeeOrderNo { get; set; }
        public int ParentStudentId { get; set; }
        public string Gender { get; set; }
        public string FollowupStatus { get; set; }
        public string ContactNo { get; set;}
        public string G_ContactNo { get; set;}

    }
    public class FeeSummaryCollections : System.Collections.Generic.List<FeeSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
