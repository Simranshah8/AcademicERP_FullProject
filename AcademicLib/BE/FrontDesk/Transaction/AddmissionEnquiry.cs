using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicLib.BE.FrontDesk.Transaction
{
   public class AddmissionEnquiry : ResponeValues
    {
        public int AutoNumber { get; set; }
        public int? EnquiryId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int? CasteId { get; set; }
        public DateTime? DOB { get; set; }
        public string BirthCertificateNo { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsPhysicalDisability { get; set; }
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
        
        public byte[] Photo { get; set; }
        public string PhotoPath { get; set; }
        public string FatherName { get; set; }
        public string F_Profession { get; set; }
        public string F_ContactNo { get; set; }
        public string F_Email { get; set; }
        public string MotherName { get; set; }
        public string M_Profession { get; set; }
        public string M_ContactNo { get; set; }
        public string M_Email { get; set; }
        public int IfGuradianIs { get; set; }
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

        private List<Academic.Transaction.StudentPreviousAcademicDetails> _academicDetailsColl = new List<Academic.Transaction.StudentPreviousAcademicDetails>();
        public List<Academic.Transaction.StudentPreviousAcademicDetails> AcademicDetailsColl
        {
            get
            {
                return _academicDetailsColl;
            }
            set
            {
                _academicDetailsColl = value;
            }
        }

        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime? FollowUpDueDate { get; set; }
        public DateTime? FollowUpTime { get; set; }
        public string Remarks { get; set; }

        public string Department { get; set; }
        public string Shift { get; set; }

        public string ClassName { get; set; }
        public List<AcademicLib.BE.Fee.Creation.ManualBillingDetails> FeeItemColl { get; set; }

        public int? SourceId { get; set; }
        public int? CommunicationTypeId { get; set; }
        public string AutoManualNo { get; set; }
        public bool FormSale { get; set; }
        public int? ReceiptAsLedgerId { get; set; }
        public string ReceiptNarration { get; set; }
        public string FollowupRemarks { get; set; }
        public int? StudentTypeId { get; set; }
        public double F_AnnualIncome { get; set; }
        public double M_AnnualIncome { get; set; }
        public int? AcademicYearId { get; set; }
        public bool AlreadyFormSale { get; set; }
        public string ReferralCode { get; set; }
        public int? SEETypeId { get; set; }
        public int? PlusTwoId { get; set; }

        public string IPAddress { get; set; }
        public string Agent { get; set; }
        public string Browser { get; set; }
        public double? IeltsToeflScore { get; set; }        
        public string Qualification { get; set; }

        public string SLCSEEype { get; set; }
        public string PlusTwoType { get; set; }
        public string PassKey { get; set; }
    }
    public class AddmissionEnquiryCollections : System.Collections.Generic.List<AddmissionEnquiry> {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


    public class EmpCouncellingStatus
    {
        public string Status { get; set; }
        public int NoOfCouncelling { get; set; }
    }
    public class EmpCouncellingStatusCollections : System.Collections.Generic.List<EmpCouncellingStatus>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }


    public class ImportAddmissionEnquiry 
    {

        public string Name { get; set; }

        [Column("ST.FirstName")]
        public string FirstName { get; set; }

        [Column("ST.MiddleName")]
        public string MiddleName { get; set; }

        [Column("ST.LastName")]
        public string LastName { get; set; }

        [Column("ST.Gender")]
        public string Gender { get; set; }        

        [Column("ST.DOB")]
        public DateTime? DOB_AD { get; set; }

        [Column("ST.CasteId")]
        public string Caste { get; set; }

        [Column("ST.BirthCertificateNo")]
        public string BirthCertificateNo { get; set; }

        [Column("ST.Nationality")]
        public string Nationality { get; set; }

        [Column("ST.Religion")]
        public string Religion { get; set; }

        [Column("ST.Address")]
        public string Address { get; set; }

        [Column("ST.ContactNo")]
        public string ContactNo { get; set; }

        [Column("ST.Email")]
        public string Email { get; set; }

        [Column("ST.IsPhysicalDisability")]
        public bool IsPhysicalDisability { get; set; }

        [Column("ST.PhysicalDisability")]
        public string PhysicalDisability { get; set; }

        [Column("ST.ClassId")]
        public string ClassName { get; set; }

        [Column("ST.MediumId")]
        public string Medium { get; set; }

        [Column("ST.FacultyId")]
        public string Faculty { get; set; }

        [Column("ST.ClassShiftId")]
        public string ClassShift { get; set; }

        [Column("ST.IsTransport")]
        public bool IsTransport { get; set; }

        [Column("ST.TransportFacility")]
        public string TransportFacility { get; set; }

        [Column("ST.IsHostel")]
        public bool IsHostel { get; set; }

        [Column("ST.HostelRequired")]
        public string HostelRequired { get; set; }

        [Column("ST.IsOtherfaciltity")]
        public bool IsOtherfaciltity { get; set; }

        [Column("ST.Otherfaciltity")]
        public string Otherfaciltity { get; set; }

        [Column("ST.IsTiffin")]
        public bool IsTiffin { get; set; }

        [Column("ST.Tiffin")]
        public string Tiffin { get; set; }

        [Column("ST.IsTution")]
        public bool IsTution { get; set; }

        [Column("ST.FatherName")]
        public string FatherName { get; set; }

        [Column("ST.F_Profession")]
        public string F_Profession { get; set; }

        [Column("ST.F_ContactNo")]
        public string F_ContactNo { get; set; }

        [Column("ST.F_Email")]
        public string F_Email { get; set; }

        [Column("ST.MotherName")]
        public string MotherName { get; set; }

        [Column("ST.M_Profession")]
        public string M_Profession { get; set; }

        [Column("ST.M_ContactNo")]
        public string M_ContactNo { get; set; }

        [Column("ST.M_Email")]
        public string M_Email { get; set; }

        [Column("ST.GuardianName")]
        public string GuardianName { get; set; }

        [Column("ST.G_Relation")]
        public string G_Relation { get; set; }

        [Column("ST.G_Professsion")]
        public string G_Professsion { get; set; }

        [Column("ST.G_Contact")]
        public string G_Contact { get; set; }

        [Column("ST.G_Email")]
        public string G_Email { get; set; }

        [Column("ST.G_Address")]
        public string G_Address { get; set; }

        [Column("ST.EnquiryDate")]
        public DateTime EnquiryDate { get; set; }

        [Column("ST.Sourse")]
        public string Source { get; set; }

        [Column("ST.PA_FullAddress")]
        public string PA_FullAddress { get; set; }

        [Column("ST.CA_FullAddress")]
        public string CA_FullAddress { get; set; }

        [Column("ST.PreviousSchool")]
        public string PreviousSchool { get; set; }

        [Column("ST.PreviousSchoolAddress")]
        public string PreviousSchoolAddress { get; set; }

        [Column("ST.IeltsToeflScore")]
        public double? IeltsToeflScore { get; set; }

        public string Qualtification { get; set; }

    }
    public class UpdateAdmissionEnquiry : ImportAddmissionEnquiry
    {
        public string Where { get; set; }
        public string Query { get; set; }
        public string Table
        {
            get
            {
                return "update ST set " + Query + "  from tbl_AdmissionEnquiry ST " + Where;
            }
        }
    }


}
