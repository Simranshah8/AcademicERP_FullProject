using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class ReminderSlip
    {
        public ReminderSlip()
        {
            ClassName = "";
            SectionName = "";
            UptoMonth = "";
            Email = "";
            Semester = "";
            ClassYear = "";
        }
        public int StudentId { get; set; }
        public string RegNo { get; set; }
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
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
        public string TransportRoute { get; set; }
        public string TransportPoint { get; set; }
        public string RoomName { get; set; }
        public string ReminderNotes { get; set; }
        public string CompName { get; set; }
        public string CompAddress { get; set; }
        public string CompPhoneNo { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmailId { get; set; } 
        public string CompWebSite { get; set; }
        public string CompLogoPath { get; set; }
        public string CompImgPath { get; set; }
        public string CompBannerPath { get; set; }
        public string CompanyRegdNo { get; set; }
        public string CompanyPanVat { get; set; }

        public string ClassSec
        {
            get
            {
                return (ClassName + " " + SectionName+" "+Semester+" "+ClassYear).Trim();
            }
        }
        public string UptoMonth { get; set; }
        public int ClassOrderNo { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string Email { get; set; }
    }
    public class ReminderSlipCollections : System.Collections.Generic.List<ReminderSlip>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
