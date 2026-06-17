using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.FrontDesk
{
    public class StudentPaymentFollowup
    {
        public int TranId { get; set; }
        public int AutoNumber { get; set; }
        public string ForMonth { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string PaymentDueMiti { get; set; }
        public string Remarks { get; set; }
        public DateTime FollowupDate { get; set; }
        public string FollowupMiti { get; set; }
        public string FollowupBy { get; set; }
        public DateTime? NextFollowupDateTime { get; set; }
        public string NextFollowupMiti { get; set; }
        public bool IsClosed { get; set; }
        public string ClosedRemarks { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public string ClosedMiti { get; set; }
        public string ClosedBy { get; set; }
        public double DuesAmt { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string RegNo { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string Email { get; set; }
        public int? StatusId { get; set; }

    }
    public class StudentPaymentFollowupCollections : System.Collections.Generic.List<StudentPaymentFollowup> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}
