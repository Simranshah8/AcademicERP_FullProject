using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class StudentSummary
    {
        public StudentSummary()
        {
            ClassName = "";
            SectionName = "";
            RegNo = "";
            BoardRegNo = "";
            BoardName = "";
            Name = "";
            FatherName = "";
            MotherName = "";
            Address = "";
        }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; } 
        public string M_ContactNo { get; set; }

        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; } 
        public string HouseName { get; set; }
        public string Medium { get; set; }
        public string BoardName { get; set; }
        public string BoardRegNo { get; set; } 
        public int EnrollNo { get; set; }
        public long CardNo { get; set; }
        public string BusStop { get; set; } 
        public string BusPoint { get; set; }
        public string GuardianName { get; set; }
        public string G_Relation { get; set; }
        public string G_Address { get; set; }
        public string G_ContacNo { get; set; }
        public string UserName { get; set; }

        public string ClassSection
        {
            get
            {
                string val = ClassName;
                if (!string.IsNullOrEmpty(SectionName))
                    val = val + "-" + SectionName;
                return val;
            }
        }

        public string SMSText { get; set; }

        public DateTime? AdmitDate_AD { get; set; }
        public string AdmitDate_BS { get; set; }
        public DateTime? LeftDate_AD { get; set; }
        public string LeftDate_BS { get; set; }

        public string LeftRemarks { get; set; }
        public string CurrentAddress { get; set; }

        public string StudentType { get; set; }
        public string Caste { get; set; }
        public string AgeRange { get; set; }
        public string LedgerPanaNo { get; set; }
        public string EMSId { get; set; }
        public bool IsNew { get; set; }
        public string FatherOccupation { get; set; }
        public string GuardianOccupation { get; set; }
        public string MotherOccupation { get; set; }
        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string Email { get; set; }
        public string F_Email { get; set; }
        public string M_Email { get; set; }
        public string G_Email { get; set; }

        public string CitizenshipNo { get; set; } = "";
        public string MotherTongue { get; set; } = "";
        public string Height { get; set; } = "";
        public string Weight { get; set; } = "";
        public string PhysicalDisability { get; set; } = "";
        public string Aim { get; set; } = "";
        public string BirthCertificateNo { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string PA_Province { get; set; } = "";
        public string PA_District { get; set; } = "";
        public string PA_LocalLevel { get; set; } = "";
        public string PA_Village { get; set; } = "";
        public string Pwd { get; set; } = "";
        public bool IsUserActive { get; set; }
        

        public string AgeDet
        {

            get
            {
                try
                {
                    if (DOB_AD.HasValue)
                        return Dynamic.ReportEngine.RDL.VBFunctions.CalculateAgeS(DOB_AD);
                }
                catch { }

                
                return "";
            }
        }
        public int? Age
        {

            get
            {
                try
                {
                    if (DOB_AD.HasValue)
                        return Dynamic.ReportEngine.RDL.VBFunctions.CalculateAgeI(DOB_AD);
                }
                catch { }

                return null;
            }
        }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Branch { get; set; }

        public string NameNP { get; set; }
        public string AgeStr { get; set; }
    }
    public class StudentSummaryList : System.Collections.Generic.List<StudentSummary>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}
