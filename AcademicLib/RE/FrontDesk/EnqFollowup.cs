using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.FrontDesk
{
    public class EnqFollowup : ResponeValues
    {
        public EnqFollowup()
        {
            FirstName = "";
            MiddleName = "";
            LastName = "";
        }
        public int TranId { get; set; }
        public int BranchId { get; set; }
        public int AutoNumber { get; set; }
        public string AutoManualNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; } 
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string ClassName { get; set; }
        public string Medium { get; set; }
        public string Faculty { get; set; }
        public string ClassShift { get; set; }
        public bool IsTransport { get; set; } 
        public bool IsHostel { get; set; }
        public string FatherName { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string EnqRemarks { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public bool IsClosed { get; set; } 
        public string ClosedRemarks { get; set; }
        public DateTime ClosedDateTime { get; set; }
        public string ClosedMiti { get; set; } 
        public string EnquiryMiti { get; set; } 
        public string NextFollowupMiti { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public string ClosedBy { get; set; }
        public string F_ContactNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryMiti { get; set; }

        public string Name
        {
            get
            {
                return ((FirstName + " " + MiddleName).Trim() + " " + LastName).Trim();
            }
        }
        public int? RefTranId { get; set; }
        public int FollowupType { get; set; }
        public bool IsAssignCounselor { get; set; }
        public string Counselor { get; set; }
        public bool FormSale { get; set; }
        public int Status { get; set; }
        public string StatusRemarks { get; set; }

        public string StatusStr
        {
            get
            {
                return ((ENQUIRYSTATUS)Status).ToString().Replace("_"," ");
            }
        }

        public string F_Email { get; set; }
        public string M_ContactNo { get; set; }
        public string M_Email { get; set; }
        public string G_Contact { get; set; }
        public string G_Email { get; set; }
        //Added on 27 Poush
        public DateTime? FollowupDate { get; set; }
        public string FollowupMiti { get; set; }

    }
    public class EnqFollowupCollections : System.Collections.Generic.List<EnqFollowup>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public enum ENQUIRYSTATUS
    {
        NEW=1,
        FORM_SOLD=2,
        HOLD=3,
        RESUMED=4,
        REJECTED=5,
        REGISTRATION_INITIATED=6,
        ADMISSION_INITIATED=7,
        APPROVED=8,
        COUNCELLING_ACCEPT=9,
        COUNCELLING_REJECTED = 10,
        COUNCELLING_PENDING =11,        
        COUNCELLING_HOLD=12,
        COUNCELLING_RESUMED = 13,
        COUNCELLING_IN_PROGRESS =14,
        COUNCELLING_TRANSFER = 15,
        COUNCELLING_COMPLETED =16

    }
    public enum ADMISSIONSTATUS
    {
        PENDING = 1,
        RECEIVED = 2,
        ADMISSIONGRANTED = 3,
        HOLD = 4,
        RESUMED = 5,
        REJECTED = 6
    }
}
