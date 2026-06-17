using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicLib.BE.Academic.Transaction
{
    public class Student : ResponeValues
    {
        public Student()
        {
            AdmitDate = DateTime.Today;
            IfGurandianIs = 1;
            Gender = 1;
            SiblingDetailColl = new List<SiblingDetails>();
        }
        public int? StudentId { get; set; }

        public int? AutoNumber { get; set; }
        public string RegNo { get; set; } = "";
        public DateTime? AdmitDate { get; set; }
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Gender { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string Religion { get; set; } = "";
        public int? CasteId { get; set; }
        public string Nationality { get; set; } = "";
        public string BloodGroup { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string Email { get; set; } = "";
        public string MotherTongue { get; set; } = "";
        public string Height { get; set; } = "";
        public string Weigth { get; set; } = "";
        public bool IsPhysicalDisability { get; set; }
        public string PhysicalDisability { get; set; } = "";
        public string Aim { get; set; } = "";
        public string BirthCertificateNo { get; set; } = "";
        public string CitizenshipNo { get; set; } = "";
        public string Remarks { get; set; } = "";
        public byte[] Photo { get; set; }
        public string PhotoPath { get; set; } = "";
        public byte[] Signature { get; set; }
        public string SignaturePath { get; set; } = "";
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int RollNo { get; set; }
        public int? AcademicYear { get; set; }
        public bool IsNewStudent { get; set; }
        public int? StudentTypeId { get; set; }
        public int? MediumId { get; set; }
        public int? HouseNameId { get; set; }
        public int? HouseDressId { get; set; }
        public int? TransportPointId { get; set; }
        public int? BoardersTypeId { get; set; }
        public int? BoardId { get; set; }
        public string BoardRegNo { get; set; } = "";
        public int EnrollNo { get; set; }
        public long CardNo { get; set; }
        public string FatherName { get; set; } = "";
        public string F_Profession { get; set; } = "";
        public string F_ContactNo { get; set; } = "";
        public string F_Email { get; set; } = "";
        public string MotherName { get; set; } = "";
        public string M_Profession { get; set; } = "";
        public string M_Contact { get; set; } = "";
        public string M_Email { get; set; } = "";
        public int IfGurandianIs { get; set; }
        public string GuardianName { get; set; } = "";
        public string G_Relation { get; set; } = "";
        public string G_Profesion { get; set; } = "";
        public string G_ContactNo { get; set; } = "";
        public string G_Email { get; set; } = "";
        public string G_Address { get; set; } = "";
        public string PermanentAddress { get; set; } = "";
        public string PA_FullAddress { get; set; } = "";
        public string PA_Province { get; set; } = "";
        public string PA_District { get; set; } = "";
        public string PA_LocalLevel { get; set; } = "";
        public string PA_Village { get; set; } = "";
        public int PA_WardNo { get; set; }
        public string CurrentAddress { get; set; } = "";
        public bool IsSameAsPermanentAddress { get; set; }
        public string CA_FullAddress { get; set; } = "";
        public string CA_Province { get; set; } = "";
        public string CA_District { get; set; } = "";
        public string CA_LocalLevel { get; set; } = "";
        public int CA_WardNo { get; set; }
        public string StreetName { get; set; } = "";

        public string FirstNameNP { get; set; } = "";
        public string MiddleNameNP { get; set; } = "";
        public string LastNameNP { get; set; } = "";
        public string CitizenFrontPhoto { get; set; } = "";
        public string CitizenBackPhoto { get; set; } = "";
        public string NIDNo { get; set; } = "";
        public string NIDPhoto { get; set; } = "";
        public bool isEDJ { get; set; }
        public string EDJ { get; set; } = "";
        public string PA_HouseNo { get; set; } = "";
        public string CA_HouseNo { get; set; } = "";
        public string CA_Country { get; set; } = "";

        private List<StudentPreviousAcademicDetails> _academicDetailsColl = new List<StudentPreviousAcademicDetails>();
        public List<StudentPreviousAcademicDetails> AcademicDetailsColl
        {
            get
            {
                return _academicDetailsColl;
            }set
            {
                _academicDetailsColl = value;
            }
        }

        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }
        public string EMSId { get; set; } = "";
        public string LedgerPanaNo { get; set; } = "";

        public List<SiblingDetails> SiblingDetailColl { get; set; }

        public string FatherPhotoPath { get; set; } = "";
        public string MotherPhotoPath { get; set; } = "";
        public string GuardianPhotoPath { get; set; } = "";

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }
        public int? AdmissionEnquiryId { get; set; }
        public int? RegistrationId { get; set; }
        public string EnquiryNo { get; set; } = "";
        public double F_AnnualIncome { get; set; }
        public double M_AnnualIncome { get; set; }
        public int? ClassId_First { get; set; }


        public bool IsFollowupRequired { get; set; }
        public DateTime? FollowupDate { get; set; }
        public DateTime? FollowUpTime { get; set; }
        public DateTime? FollowupDateTime { get; set; }
        public int? SourceId { get; set; }
        public int? CommunicationTypeId { get; set; }
        public bool FormSale { get; set; }
        public int? ReceiptAsLedgerId { get; set; }
        public string ReceiptNarration { get; set; } = "";
        public string FollowupRemarks { get; set; } = "";
        public int FamilyType { get; set; }

        public string ClassName { get; set; } = "";
        public List<AcademicLib.BE.Fee.Creation.ManualBillingDetails> FeeItemColl { get; set; }
        public RegistrationEligibility Eligibility { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections EligibilityAttachmentColl { get; set; }
        public int? ClassShiftId { get; set; }
        public string ReferralCode { get; set; } = "";

        public bool IsImport { get; set; }
        public int? PaidUptoMonth { get; set; }

        public int? DonorLedgerId { get; set; }

        public int? PassOutClassId { get; set; }
        public string PassOutSymbolNo { get; set; }
        public double? PassOutGPA { get; set; }
        public string PassOutGrade { get; set; }
        public string PassOutRemarks { get; set; }

        public int? BranchId { get; set; }
    }

    public class StudentPreviousAcademicDetails
    {
        public string ClassName { get; set; } = "";
        public string Exam { get; set; } = "";
        public string PassoutYear { get; set; } = "";
        public string SymbolNo { get; set; } = "";
        public double ObtainMarks { get; set; }
        public double ObtainPer { get; set; }
        public string Division { get; set; } = "";
        public double GPA { get; set; }
        public string SchoolColledge { get; set; } = "";
        public string BoardName { get; set; } = "";

        public string Level { get; set; } = "";
        public string University { get; set; } = "";
    }
        
    public class SiblingDetails
    {
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int ForStudentId { get; set; }
        public string Relation { get; set; }
        public string Remarks { get; set; }
    }
    public enum STUDENT_SEARCHOPTIONS
    {
        [StringValue("ST.Name")]
        NAME=1,

        [StringValue("ST.RegNo")]
        REGDNO =2,

        [StringValue("ST.RollNo")]
        ROLLNO =3,

        [StringValue("ST.StudentId")]
        ID =4,

        [StringValue("ST.LedgerPanaNo")]
        LedgerPanaNo = 5

    }

    public class StudentAutoComplete
    {
        public StudentAutoComplete()
        {
            RegdNo = "";
            ClassName = "";
            Name = "";
            SectionName = "";
            FatherName = "";
            ContactNo = "";
            Address = "";
            UserName = "";
            Pwd = "";
            Batch = "";
            Faculty = "";
            Semester = "";
            ClassYear = "";
            LedgerPanaNo = "";
        }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LedgerPanaNo { get; set; }
        public string Pwd { get; set; }
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }

        public string ClassYear { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
    }

    public class StudentAutoCompleteCollections : System.Collections.Generic.List<StudentAutoComplete>
    {
        public StudentAutoCompleteCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class StudentInfo
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegNo { get; set; } = "";
        public string F_ContactNo { get; set; }
        public string M_ContactNo { get; set; }
        public string Email { get; set; }
        public string F_Email { get; set; }
        public string M_Email { get; set; }
    }
    public class StudentUser : StudentInfo
    {
        public int SNo { get; set; }        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string Branch { get; set; } 
        public string Gender { get; set; }

        public string Faculty { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }

    }
    public class StudentUserCollections : System.Collections.Generic.List<StudentUser>
    {
        public StudentUserCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentList
    {
        public StudentList()
        {
            RegdNo = "";
            ClassName = "";
            SectionName = "";
            Name = "";
            Gender = "";
            FatherName = "";
            Address = "";
            PhotoPath = "";
            UserName = "";
            Pwd = "";
            BoardRegdNo = "";
        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public int EnrollNo { get; set; }
        public long CardNo { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string BoardRegdNo { get; set; }

        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }

        public string ContactNo { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }

        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }

        public string MotherName { get; set; }
        public string MotherContactNo { get; set; }
        public string LedgerPanaNo { get; set; }
    }
    public class StudentListCollections : System.Collections.Generic.List<StudentList>
    {
        public StudentListCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class ImportStudent
    {
        public ImportStudent()
        {
            EMSId = "";
            BoardRegdNo = "";
            RegNo = "";
            HouseDress = "";
            HouseName = "";

        }
        [Column("ST.RegNo")]
        public string RegNo { get; set; }

        [Column("ST.FirstName")]
        public string FirstName { get; set; }

        [Column("ST.MiddleName")]
        public string MiddleName { get; set; }

        [Column("ST.LastName")]
        public string LastName { get; set; }

        [Column("ST.FirstName=@FirstName,ST.MiddleName=@MiddleName,ST.LastName=@LastName")]
        public string Name { get; set; }

        [Column("ST.Gender")]
        public string Gender { get; set; }

        [Column("ST.DOB_AD")]
        public DateTime? DOBAD { get; set; }

        [Column("ST.DOB_AD")]
        public string DOBAD_Str { get; set; }

        [Column("ST.CasteId")]
        public string Caste { get; set; }

        [Column("ST.BloodGroup")]
        public string BloodGroup { get; set; }

        [Column("ST.ContactNo")]
        public string ContactNo { get; set; }

        [Column("ST.Email")]
        public string EmailId { get; set; }

        [Column("ST.ClassId")]
        public string ClassName { get; set; }

        [Column("ST.SectionId")]
        public string SectionName { get; set; }

        [Column("ST.RollNo")]
        public int RollNo { get; set; }

        [Column("ST.StudentTypeId")]
        public string StudentType { get; set; }

        [Column("ST.MediumId")]
        public string Medium { get; set; }

        [Column("ST.BoardRegNo")]
        public string BoardRegdNo { get; set; }

        [Column("ST.EnrollNo")]
        public int EnrollNo { get; set; }

        [Column("ST.CardNo")]
        public long CardNo { get; set; }

        [Column("ST.FatherName")]
        public string FatherName { get; set; }

        [Column("ST.F_ContactNo")]
        public string FatherContactNo { get; set; }

        [Column("ST.MotherName")]
        public string MotherName { get; set; }

        [Column("ST.M_Contact")]
        public string MotherContactNo { get; set; }

        [Column("ST.GuardianName")]
        public string GuardianName { get; set; }

        [Column("ST.G_Relation")]
        public string GuardianRelation { get; set; }

        [Column("ST.G_ContactNo")]
        public string GuardianContactNo { get; set; }

        [Column("ST.G_Address")]
        public string GuardianAddress { get; set; }

        [Column("ST.PA_FullAddress")]
        public string P_Address { get; set; }

        [Column("ST.CA_FullAddress")]
        public string T_Address { get; set; }

        [Column("ST.AcademicYear")]
        public string AcademicYear { get; set; }

        [Column("ST.PhysicalDisability")]
        public string PhysicalDisability { get; set; }

        [Column("ST.DOB_AD")]
        public string DOB_BS { get; set; }

        [Column("ST.IsNewStudent")]
        public bool IsNewStudent { get; set; }
        public bool IsECD { get; set; }

        [Column("ST.EMSId")]
        public string EMSId { get; set; }

        [Column("ST.StudentId")]
        public int? StudentId { get; set; }

        [Column("ST.ClassId")]
        public string ClassDescription { get; set; }

        [Column("ST.F_Profession")]
        public string F_Profession { get; set; }

        [Column("ST.M_Profession")]
        public string M_Profession { get; set; }

        [Column("ST.F_Email")]
        public string F_Email { get; set; }

        [Column("ST.M_Email")]
        public string M_Email { get; set; }

        [Column("ST.BirthCertificateNo")]
        public string BirthCertificateNo { get; set; }

        [Column("ST.LedgerPanaNo")]
        public string LedgerPanaNo { get; set; }

        [Column("ST.HouseNameId")]
        public string HouseName { get; set; }

        [Column("ST.HouseDressId")]
        public string HouseDress { get; set; }

        [Column("ST.SemesterId")]
        public string Semester { get; set; }

        [Column("ST.ClassYearId")]
        public string ClassYear { get; set; }

        [Column("ST.BatchId")]
        public string Batch { get; set; }

        [Column("ST.Nationality")]
        public string Nationality { get; set; }

        [Column("ST.Religion")]
        public string Religion { get; set; }

        [Column("ST.CitizenshipNo")]
        public string CitizenshipNo { get; set; }

        [Column("ST.PA_LocalLevel")]
        public string PA_LocalLevel { get; set; }

        [Column("ST.CA_LocalLevel")]
        public string CA_LocalLevel { get; set; }

        [Column("ST.PA_Province")]
        public string PA_Province { get; set; }

        [Column("ST.CA_Province")]
        public string CA_Province { get; set; }

        [Column("ST.PA_District")]
        public string PA_District { get; set; }

        [Column("ST.CA_District")]
        public string CA_District { get; set; }

        [Column("ST.AdmitDate")]
        public string AdmitDateAD_Str { get; set; }

        [Column("ST.CA_WardNo")]
        public int CA_WardNo { get; set; }

        [Column("ST.PA_WardNo")]
        public int PA_WardNo { get; set; }

        [Column("ST.AdmitDate")]
        public DateTime? AdmitDate { get; set; }



        [Column("ST.MotherTongue")]
        public string MotherTongue { get; set; }

        [Column("ST.Height")]
        public string Height { get; set; }

        [Column("ST.Weigth")]
        public string Weigth { get; set; }

        [Column("ST.IsPhysicalDisability")]
        public bool IsPhysicalDisability { get; set; }

        [Column("ST.Aim")]
        public string Aim { get; set; }

        [Column("ST.Remarks")]
        public string Remarks { get; set; }

        [Column("ST.EnquiryNo")]
        public string EnquiryNo { get; set; }

        [Column("ST.TransportPointId")]
        public string TransportPoint { get; set; }

        [Column("ST.BoardersTypeId")]
        public string BoardersType { get; set; }

        [Column("ST.BoardId")]
        public string Board { get; set; }

        [Column("ST.ClassId_First")]
        public string ClassId_First { get; set; }

        [Column("ST.F_AnnualIncome")]
        public double F_AnnualIncome { get; set; }

        [Column("ST.M_AnnualIncome")]
        public double M_AnnualIncome { get; set; }

        [Column("ST.G_Profesion")]
        public string G_Profesion { get; set; }

        [Column("ST.G_Email")]
        public string G_Email { get; set; }

        [Column("ST.BranchId")]
        public string Branch { get; set; }


        [Column("ST.PassOutClassId")]
        public string PassOutClass { get; set; }

        [Column("ST.PassOutSymbolNo")]
        public string PassOutSymbolNo { get; set; }

        [Column("ST.PassOutGPA")]
        public double? PassOutGPA { get; set; }

        [Column("ST.PassOutGrade")]
        public string PassOutGrade { get; set; }

        [Column("ST.PassOutRemarks")]
        public string PassOutRemarks { get; set; }

        [Column("ST.NIDNo")]
        public string NIDNo { get; set; } = "";

        [Column("ST.FirstNameNP")]
        public string FirstNameNP { get; set; } = "";

        [Column("ST.MiddleNameNP")]
        public string MiddleNameNP { get; set; } = "";

        [Column("ST.LastNameNP")]
        public string LastNameNP { get; set; } = "";
    }

    public class UpdateStudent : ImportStudent
    {
        public string Where { get; set; }
        public string Query { get; set; }
        public string Table
        {
            get
            {
                return "update ST set " + Query + "  from tbl_Student ST " + Where;
            }
        }
    }

    public class UpdateStudentQuery : ImportStudent
    {
        public int GenderId { get; set; }
       public int? CasteId { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? StudentTypeId { get; set; }
        public int? MediumId { get; set; }
        public int? AcademicYearId { get; set; }
        public int? HouseNameId { get; set; }
        public int? HouseDressId { get; set; }

        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BatchId { get; set; }

        public int? TransportPointId { get; set; }
        public int? BoardersTypeId { get; set; }
        public int? BoardId { get; set; }
        public int? ClassId_FirstId { get; set; }
    }

    public class ImportStudentPhoto
    {
        public ImportStudentPhoto()
        {
            RegNo = "";
            BoardRegdNo = "";
            EMSId = "";
            PhotoPath = "";
        }
        [Column("ST.RegNo")]
        public string RegNo { get; set; }               

        [Column("ST.BoardRegNo")]
        public string BoardRegdNo { get; set; }

        [Column("ST.EnrollNo")]
        public int EnrollNo { get; set; }

        [Column("ST.CardNo")]
        public long CardNo { get; set; }
        
        [Column("ST.EMSId")]
        public string EMSId { get; set; }

        [Column("ST.StudentId")]
        public int StudentId { get; set; }

        [Column("ST.PhotoPath")]
        public string PhotoPath { get; set; }
        public int PhotoUploadedBy { get; set; }

        [Column("ST.AutoNumber")]
        public int AutoNumber { get; set; }

        [Column("ST.ClassId")]
        public int? ClassId { get; set; }

        [Column("ST.SectionId")]
        public int? SectionId { get; set; }


        [Column("ST.RollNo")]
        public int RollNo { get; set; }
    }

    public class ImportStudentDOB
    {
        public string EMSId { get; set; }
        public string RegdNo { get; set; }
        public string DOB_BS { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOBAD_Str { get; set; }
        public int? StudentId { get; set; }
        public int NY { get; set; }
        public int NM { get; set; }
        public int ND { get; set; }
    }

    public class ImportStudentAdmitDate
    {
        public string EMSId { get; set; }
        public string RegdNo { get; set; }
        public string AdmitDate_BS { get; set; }
        public DateTime? AdmitDate_AD { get; set; }
        public string AdmitDateAD_Str { get; set; }
        public int? StudentId { get; set; }
        public int NY { get; set; }
        public int NM { get; set; }
        public int ND { get; set; }
    }

    public class RegistrationEligibility : ResponeValues
    {
        public int TranId { get; set; }
        public int StudentId { get; set; }
        public DateTime? ExamDate { get; set; }
        public int? ExamTypeId { get; set; }
        public int? SubjectId { get; set; }
        public string ExaminarName { get; set; }
        public int? AppliedClassId { get; set; }
        public int? ClassPreferredForId { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; }
        public double ObtainMark { get; set; }
        public double Percentage { get; set; }
        public string Result { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public int? ReceiptTranId { get; set; }
        public int? AdmissionStatus { get; set; }
        public string AdmissionRemarks { get; set; }
        public int?  ReceiptAsLedgerId { get; set; }
        public string ReceiptNarration{ get; set; }

        public AcademicLib.BE.Fee.Transaction.FeePaymentModeCollections PaymentModeColl { get; set; }
        public List<AcademicLib.BE.Fee.Creation.ManualBillingDetails> FeeItemColl { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }

        public string Address { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public int? DiscountTypeId { get; set; }
        public string ApprovalBy { get; set; }
        public int ExaminationMode { get; set; }
        public int? RouteId { get; set; }
        public int? PointId { get; set; }
        public int? BedId { get; set; }
        public int? PaidUpToMonth { get; set; }
    }

}
