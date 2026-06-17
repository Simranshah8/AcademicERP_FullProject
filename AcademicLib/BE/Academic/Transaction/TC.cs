using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class TC : ResponeValues
    {
        public TC()
        {
            ClassName = "";
            SectionName = "";
            FirstName = "";
            MiddleName = "";
            LastName = "";
        }
        public int? TranId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegdNo { get; set; }
        public DateTime? AdmitDate { get; set; }
        public string AdmitDate_BS { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Caste { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string BloodGroup { get; set; }
        public string Moral { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string AcademicYear { get; set; }
        public string Medium { get; set; }
        public string StudentType { get; set; }
        public string TransportPoint { get; set; }
        public string BoarderType { get; set; }
        public string HouseName { get; set; }
        public string HouseDress { get; set; }
        public string Board { get; set; }
        public string BoardRegNo { get; set; }
        public string FullAddress { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string LocalLevel { get; set; }
        public int WardNo { get; set; }
        public string StreetName { get; set; }
        public string FatherName { get; set; }
        public string FatherContactNo { get; set; }
        public string MotherName { get; set; }
        public string PreviousClass { get; set; }
        public string SymbolNo { get; set; }
        public double Percentage { get; set; }
        public string Division { get; set; }
        public string Grade { get; set; }
        public double GPA { get; set; }
        public bool IsLeft { get; set; }
        public DateTime? LeftDate { get; set; }
        public string LeftDate_BS { get; set; }
        public string LeftRemarks { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueDate_BS { get; set; }
        public string Remarks { get; set; }
        public double DuesAmt { get; set; }

        public string UserName { get; set; }
        public DateTime LogDateTime_AD { get; set; }
        public string LogDate_BS { get; set; }
        public string ClassSec
        {
            get
            {
                return ClassName + " " + SectionName;
            }
        }

        public string Name
        {
            get
            {
                return ((FirstName + " " + MiddleName).Trim() + " " + LastName).Trim();
            }
        }

        public string UDF { get; set; }
        public string EMSId { get; set; }
        public string Nationality { get; set; }
        public int? PassOutClassId { get; set; }

    }

    public class TCCollections : System.Collections.Generic.List<TC>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class TCCCRequest : ResponeValues
    {
        public int? TranId { get; set; }
        public int? AcademicYearId { get; set; }
        public int? StudentId { get; set; }
        public string ContactNo { get; set; }
        public DateTime? DOB { get; set; }
        public string RegdNo { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string UserAgent { get; set; }
        public bool IsIssueTC { get; set; }
        public bool IsIssueCC { get; set; }
        public string PassKey { get; set; }
        public string BoardRegNo { get; set; }
    }
   
}
