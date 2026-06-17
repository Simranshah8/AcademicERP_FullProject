using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.FrontDesk.Transaction
{
    public class EnquiryNumberMethod : ResponeValues
    {
        public int NumberingMethod { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }        
        public int StartNo { get; set; }
        public int NumericalPartWidth { get; set; }        
        public int? BranchId { get; set; }
        public string Declaration { get; set; }
        public bool ActiveAdmission { get; set; }        
        public string AllowReferralForUserIdColl { get; set; }
    }
    public class RegistrationNumberMethod : ResponeValues
    {
        public int NumberingMethod { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartNo { get; set; }
        public int NumericalPartWidth { get; set; }
        public int? BranchId { get; set; }
        public string Declaration { get; set; }
        public bool ActiveAdmission { get; set; }
        public string AllowReferralForUserIdColl { get; set; }
    }
}
