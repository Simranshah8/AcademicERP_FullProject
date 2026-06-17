using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.FrontDesk
{
    public class EnqSummary
    {
        public int TranId => EnquiryId;
        public int EnquiryNo { get; set; }
        public int EnquiryId { get; set; }
        public int SNo { get; set; }
        public DateTime EnqDate_AD { get; set; }
        public string EnqDate_BS { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string Caste { get; set; }
        public string ClassName { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string BirthCertificateNo { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string PhysicalDisability { get; set; }
        public int? ClassId { get; set; }
        public int? MediumId { get; set; }
        public int? FacultyId { get; set; }
        public int? ClassShiftId { get; set; }
        public bool IsTransport { get; set; }
        public string TransportFacility { get; set; }
        public bool IsHostel { get; set; }
        public string HostelRequired { get; set; }
        public bool IsOtherfaciltity { get; set; }
        public string Otherfaciltity { get; set; }

        public bool IsTiffin { get; set; }
        public string Tiffin { get; set; }

        public bool IsTution { get; set; }

        public string PhotoPath { get; set; }
        public string F_Profession { get; set; }
        public string F_Email { get; set; }
        public string MotherName { get; set; }
        public string M_Profession { get; set; }
        public string M_ContactNo { get; set; }
        public string M_Email { get; set; }
        public string IfGuradianIs { get; set; }
        public string GuardianName { get; set; }
        public string G_Relation { get; set; }
        public string G_Professsion { get; set; }
        public string G_Contact { get; set; }
        public string G_Email { get; set; }
        public string G_Address { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string Sourse { get; set; }
        public bool IsFollowupRequired { get; set; }
        public DateTime? FollowupDate { get; set; }

        public string PA_FullAddress { get; set; }
        public string PA_Province { get; set; }
        public string PA_District { get; set; }
        public string PA_LocalLevel { get; set; }
        public int PA_WardNo { get; set; }
        public string PA_StreetName { get; set; }
        public bool IsSameAsPermanentAddress { get; set; }

        public string CA_FullAddress { get; set; }
        public string CA_Province { get; set; }
        public string CA_District { get; set; }
        public string CA_LocalLevel { get; set; }
        public int CA_WardNo { get; set; }
        public string CA_StreetName { get; set; }

        public string PreviousSchool { get; set; }
        public string PreviousSchoolAddress { get; set; }

        public double PreviousClassGpa { get; set; }
        public string OptionalFirst { get; set; }
        public string OptionalSecond { get; set; }

        public string Talent { get; set; }
        public bool AnyDisease { get; set; }
        public string Problem { get; set; }
        public string PresentCondition { get; set; }

        public string Department { get; set; }
        public string Medium { get; set; }
        public string Shift { get; set; }
        public string Source { get; set; }

        public int? ReceiptNo { get; set; }
        public double ReceiptAmt { get; set; }
        public int ReceiptTranId { get; set; }
        public string CommunicationType { get; set; }
        public string AutoManualNo { get; set; }

        public bool FormSale { get; set; }
        public int Status { get; set; }
        public string StatusRemarks { get; set; }

        public string StatusStr
        {
            get
            {
                return ((ENQUIRYSTATUS)Status).ToString().Replace("_", " ");
            }
        }


        public bool IsAssignCounselor { get; set; }
        public string Counselor { get; set; }


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
        public string EnqCommunicationType { get; set; }

        public bool IsPhysicalDisability { get; set; }

        public string StudentType { get; set; }

        public double F_AnnualIncome { get; set; }
        public double M_AnnualIncome { get; set; }
        public string PreClassName { get; set; }
        public string Exam { get; set; }
        public string PassedYear { get; set; }
        public string SymbolNo { get; set; }
        public double ObtainedMarks { get; set; }
        public string Division { get; set; }


        public string EligibleClassPreferredFor { get; set; }
        public double EligibleFullMark { get; set; }
        public double EligiblePassMark { get; set; }
        public double EligiblePercentage { get; set; }
        public double EligibleObtainMark { get; set; }
        public string EligibleResult { get; set; }
        public string EligibleStatus { get; set; }
        public string EligibleRemarks { get; set; }

        public DateTime? ExamDate { get; set; }
        public string ExamMiti { get; set; }
        public string ExaminarName { get; set; }
        public string ExamType { get; set; }
        public int? EligibleTranId { get; set; }

        public string FormSale_Str
        {
            get
            {
                if (FormSale)
                    return "YES";
                else
                    return "NO";
            }
        }
        public string NextFollowupMitiTime
        {
            get
            {
                if (NextFollowupDate.HasValue)
                    return string.IsNullOrEmpty(NextFollowupMiti) ? "" : NextFollowupMiti + " " + NextFollowupDate.Value.ToString("HH:mm ss");
                else
                    return "";
            }
        }

        public string IsPhysicalDisability_Str
        {
            get
            {
                if (IsPhysicalDisability)
                    return "YES";
                return "NO";
            }
        }

        public string ClassShiftName { get; set; }

        public string RegNo { get; set; }

        public int AdmissionStatus { get; set; }
        public string AdmissionStatusRemarks { get; set; }

        public string ReferralCode { get; set; }

        public string SLCSEEype { get; set; }
        public string PlusTwoType { get; set; }
        public double? IeltsToeflScore { get; set; }

        public string Qualification { get; set; }

        //      PhysicalDisability,IsPhysicalDisability,IsOtherfaciltity,StudentType,
        //F_AnnualIncome,M_AnnualIncome,PreClassName,Exam,PassedYear,SymbolNo,ObtainedMarks,Division
    }
    public class EnqSummaryCollections : System.Collections.Generic.List<EnqSummary>
   {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
