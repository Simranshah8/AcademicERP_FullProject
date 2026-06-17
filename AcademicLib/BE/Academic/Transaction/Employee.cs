using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicLib.BE.Academic.Transaction
{
    public class Employee : ResponeValues
    {        
        public int? EmployeeId { get; set; }
        public int AutoNumber { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoPath { get; set; } = "";
        public string EmployeeCode { get; set; } = "";
        public int EnrollNumber { get; set; }
        public int CardNo { get; set; }
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Gender { get; set; }        
        public DateTime? DOB_AD { get; set; }
        public string BloodGroup { get; set; } = "";
        public string Religion { get; set; } = "";
        public string Nationality { get; set; } = "";
        public string PersnalContactNo { get; set; } = "";
        public string OfficeContactNo { get; set; } = "";
        public string EmailId { get; set; } = "";
        public string MaritalStatus { get; set; } = "";
        public string SpouseName { get; set; } = "";
        public DateTime? AnniversaryDate { get; set; }
        public string FatherName { get; set; } = "";
        public string MotherName { get; set; } = "";
        public string GrandFather { get; set; } = "";
        public string PanId { get; set; } = "";
        public string CitizenshipNo { get; set; } = "";
        public DateTime? CitizenIssueDate { get; set; }
        public string CitizenShipIssuePlace { get; set; } = "";
        public string DrivindLicenceNo { get; set; } = "";
        public DateTime? LicenceIssueDate { get; set; }
        public DateTime? LicenceExpiryDate { get; set; }
        public string PasswordNo { get; set; } = "";
        public DateTime? PasswordIssueDate { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public string PasswordIssuePlace { get; set; } = "";
        public string PA_Country { get; set; } = "";
        public string PA_State { get; set; } = "";
        public string PA_Zone { get; set; } = "";
        public string PA_District { get; set; } = "";
        public string PA_City { get; set; } = "";
        public string PA_Municipality { get; set; } = "";
        public int PA_Ward { get; set; }
        public string PA_Street { get; set; } = "";
        public string PA_HouseNo { get; set; } = "";
        public string PA_FullAddress { get; set; } = "";
        public string TA_Counrty { get; set; } = "";
        public string TA_State { get; set; } = "";
        public string TA_Zone { get; set; } = "";
        public string TA_District { get; set; } = "";
        public string TA_City { get; set; } = "";
        public string TA_Municipality { get; set; } = "";
        public int TA_Ward { get; set; }
        public string TA_Street { get; set; } = "";
        public string TA_HouseNo { get; set; } = "";
        public string TA_FullAddress { get; set; } = "";
        public string EC_PersonalName { get; set; } = "";
        public string EC_Relationship { get; set; } = "";
        public string EC_Address { get; set; } = "";
        public string EC_Phone { get; set; } = "";
        public string EC_Mobile { get; set; } = "";
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? CategoryId { get; set; }
        public int? LevelId { get; set; }
        public string JobTitle { get; set; } = "";
        public int? ServiceTypeId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public DateTime? DateOfRetirement { get; set; }        
        public string RemoteArea { get; set; } = "";
        public string Disability { get; set; } = "";
        public string AccessionNo { get; set; } = "";
        public string SSFNo { get; set; } = "";
        public string CITAcNo { get; set; } = "";
        public string CITCode { get; set; } = "";
        public double CIT_Amount { get; set; }
        public string CIT_Nominee { get; set; } = "";
        public string CIT_RelationShip { get; set; } = "";
        public string CIT_IDType { get; set; } = "";
        public string CIT_IDNo { get; set; } = "";
        public DateTime? CIT_EntryDate { get; set; }
        public string BankName { get; set; } = "";
        public string BA_AccountName { get; set; } = "";
        public string BA_AccountNo { get; set; } = "";
        public string BA_Branch { get; set; } = "";
        public bool BA_IsForPayroll { get; set; }
        public int LI_InsuranceType { get; set; }
        public string LI_InsuranceCompany { get; set; } = "";
        public string LI_PolicyName { get; set; } = "";
        public string LI_PolicyNo { get; set; } = "";
        public double LI_PolicyAmount { get; set; }
        public DateTime? LI_PolicyStartDate { get; set; }
        public DateTime? LI_PolicyLastDate { get; set; }
        public double LI_PremiunAmount { get; set; }
        public string LI_PaymentType { get; set; } = "";
        public int LI_StartMonth { get; set; }
        public bool LI_IsDeductFromSalary { get; set; }
        public string LI_Remarks { get; set; } = "";
        public int HI_InsurenceType { get; set; }
        public string HI_InsuranceCompany { get; set; } = "";
        public string HI_PolicyName { get; set; } = "";
        public string HI_PolicyNo { get; set; } = "";
        public double HI_PolicyAmount { get; set; }
        public DateTime? HI_PolicyStartDate { get; set; }
        public DateTime? HI_PolicyLastDate { get; set; }
        public double HI_PremiumAmount { get; set; }
        public int HI_PaymentType { get; set; }
        public int HI_StartMonth { get; set; }
        public bool HI_IsDeductFromSalary { get; set; }
        public string HI_Remarks { get; set; } = "";
        public string AD_Ledger { get; set; } = "";
        public string AD_CostCenter { get; set; } = "";
        public string AD_OTLedger { get; set; } = "";
        public int? S_FirstLevelId { get; set; }
        public int? S_SecondLevelId { get; set; }
        public int? S_ThirdLevel { get; set; }
        public byte[] Signature { get; set; }
        public string SignaturePath { get; set; } = "";
        public int? SystemUserId { get; set; }

        private List<EmployeeAcademicQualification> _AcademicQualification = new List<EmployeeAcademicQualification>();
        public List<EmployeeAcademicQualification> AcademicQualificationColl
        {
            get { return _AcademicQualification; }
            set { _AcademicQualification = value; }
        }
        private List<EmployeeWorkExperience> _WorkExperienceColl = new List<EmployeeWorkExperience>();
        public List<EmployeeWorkExperience> WorkExperienceColl
        {
            get
            {
                return _WorkExperienceColl;
            }
            set
            {
                _WorkExperienceColl = value;
            }
        }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }

        private List<EmployeeBankAccount> _BankList = new List<EmployeeBankAccount>();
        public List<EmployeeBankAccount> BankList
        {
            get
            {
                return _BankList;
            }
            set
            {
                _BankList = value;
            }
        }

        public List<int> ClassShiftIdColl { get; set; }


        public string MotherTonque { get; set; } = "";
        public string Rank { get; set; } = "";
        public string Position { get; set; } = "";
        public string TeacherType { get; set; } = "";
        public string TeachingLanguage { get; set; } = "";
        public string LicenseNo { get; set; } = "";
        public string TrkNo { get; set; } = "";
        public string PFAccountNo { get; set; } = "";

        public int? CasteId { get; set; }
        public string DOBBS_Str { get; set; } = "";

        public string EMSId { get; set; } = "";
        public bool IsTeaching { get; set; }

        public int? SubjectTeacherId { get; set; }
        public bool IsPhysicalDisability { get; set; }
        public string PhysicalDisability { get; set; } = "";

        public string FatherContactNo { get; set; } = "";
        public string MotherContactNo { get; set; } = "";
        public string SpouseContactNo { get; set; } = "";
        public string OfficeEmailId { get; set; } = "";

        // For Emp. Candidate
        public DateTime EntryDate { get; set; }
        public int? SourceId { get; set; }
        public double SalaryExpectation { get; set; }
        public string Level { get; set; } = "";
        public int TaxRuleAs { get; set; } = 1; //--- 1==Normal,2=SSF


        //Added By Suresh on Mangsir 14 2081
        public string LicenceIssuePlace { get; set; } = "";
        public int? SalaryApplicableYearId { get; set; }
        public int? SalaryApplicableMonthId { get; set; }

        public string FirstNameNP { get; set; } = "";
        public string MiddleNameNP { get; set; } = "";
        public string LastNameNP { get; set; } = "";
        public string CitizenFrontPhoto { get; set; } = "";
        public string CitizenBackPhoto { get; set; } = "";
        public string NIDNo { get; set; } = "";
        public string NIDPhoto { get; set; } = "";
        public bool isEDJ { get; set; }
        public string EDJ { get; set; } = "";
        public string Qualification { get; set; } = "";

        //Prashant Add Research and Publication
        public Employee()
        {
            EmployeeResearchPublicationColl = new EmployeeResearchPublicationCollections();
        }
        public EmployeeResearchPublicationCollections EmployeeResearchPublicationColl { get; set; }

        private List<EmployeeReference> _EmployeeReference = new List<EmployeeReference>();
        public List<EmployeeReference> ReferenceColl
        {
            get { return _EmployeeReference; }
            set { _EmployeeReference = value; }
        }

        public bool IsImport {  get; set; }
    }

    public class EmployeeReference
    {
        public string ReferencePerson { get; set; }
        public string Designation { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Organisation { get; set; } = "";

    }

    public class EmployeeCollections : List<Employee> {
    
        public EmployeeCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeAcademicQualification
    {
        public string DegreeName { get; set; }
        public string BoardUniversity { get; set; }
        public string PassedYear { get; set; }
        public string GradePercentage { get; set; }

        public string RegistrationNo { get; set; } = "";
        public string NameOfInstitution { get; set; } = "";


    }

    public class EmployeeWorkExperience
    {
        public string Organization { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        //Added By Suresh on 14 Poush
        public DateTime? EndDate { get; set; }
        //Ends
        public string Remarks { get; set; }

    }

    public class EmployeeBankAccount : ResponeValue
    {
        public string EmpCode { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public bool ForPayRoll { get; set; }

        // For Reporting
        public int EmployeeId { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }

        //Added By Suresh on 15 Poush
        public int BankId { get; set; }
    }

    public class EmployeeBankAccountCollections : System.Collections.Generic.List<EmployeeBankAccount>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class EmployeeAutoComplete
    {
        public EmployeeAutoComplete()
        {
            Code = "";
            Address = "";
            Name = "";
            MobileNo = "";        
            Address = "";
            UserName = "";
            Pwd = "";
        }
        public int EmployeeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public int EnrollNo { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
     
    }

    public class EmployeeAutoCompleteCollections : System.Collections.Generic.List<EmployeeAutoComplete>
    {
        public EmployeeAutoCompleteCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class EmployeeUser
    {
        public EmployeeUser()
        {
            EmployeeCode = "";
            Name = "";
            Department = "";
            Designation = "";
            ContactNo = "";
            Address = "";
            UserName = "";
            Pwd = "";
        }
        public int SNo { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string CodeName
        {
            get
            {
                return EmployeeCode + " " + Name;
            }
        }

        public int? DepartmentId { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
    }
    public class EmployeeUserCollections : System.Collections.Generic.List<EmployeeUser>
    {
        public EmployeeUserCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ImportEmployee
    {
        [Column("E.EmployeeCode")]
        public string EmployeeCode { get; set; }

        [Column("E.EnrollNumber")]
        public int EnrollNo { get; set; }

        [Column("E.FirstName")]
        public string FirstName { get; set; }

        [Column("E.MiddleName")]
        public string MiddleName { get; set; }

        [Column("E.LastName")]
        public string LastName { get; set; }
        public string Name { get; set; }

        [Column("E.Gender")]
        public string Gender { get; set; }

        [Column("E.BloodGroup")]
        public string BloodGroup { get; set; }

        [Column("E.PersnalContactNo")]
        public string PersonContactNo { get; set; }

        [Column("E.OfficeContactNo")]
        public string OfficeContactNo { get; set; }

        [Column("E.EmailId")]
        public string EmailId { get; set; }

        [Column("E.FatherName")]
        public string FatherName { get; set; }

        [Column("E.MotherName")]
        public string MotherName { get; set; }

        [Column("E.PanId")]
        public string PanId { get; set; }

        [Column("E.CitizenshipNo")]
        public string CitizenshipNo { get; set; }


        [Column("E.DepartmentId")]
        public string Department { get; set; }

        [Column("E.DesignationId")]
        public string Designation { get; set; }

        [Column("E.CategoryId")]
        public string Category { get; set; }

        [Column("E.LevelId")]
        public string Level { get; set; }

        [Column("E.ServiceTypeId")]
        public string ServiceType { get; set; }

        [Column("E.IsTeaching")]
        public bool IsTeaching { get; set; }

        [Column("E.DateofJoining")]
        public DateTime? DateOfJoinAD { get; set; }

        [Column("E.DateofJoining")]
        public string DateOfJoinAD_Str { get; set; }

        [Column("E.PA_FullAddress")]
        public string Address { get; set; }

        [Column("E.TA_FullAddress")]
        public string Temp_Address { get; set; }
        public DateTime? DOB_AD { get; set; }

        [Column("E.DOB_AD")]
        public string DOBAD_Str { get; set; }
        public string DOB_BS { get; set; }

        [Column("E.PA_State")]
        public string State { get; set; }

        [Column("E.PA_District")]
        public string District { get; set; }

        [Column("E.PA_Municipality")]
        public string Municipality { get; set; }

        [Column("E.PA_Ward")]
        public int WardNo { get; set; }                

        public string CitizenShipIssuePlace { get; set; }
        public string Caste { get; set; }
        public string SpouseName { get; set; }

        [Column("E.Religion")]
        public string Religion { get; set; }
        
        [Column("E.Nationality")]
        public string Nationality { get; set; }
        public string CIT_Nominee { get; set; }
        public string Disability { get; set; }
        public string MotherTonque { get; set; }
        public string Rank { get; set; }
        public string Position { get; set; }
        public string TeacherType { get; set; }
        public string TeachingLanguage { get; set; }
        [Column("E.LicenseNo")]
        public string LicenseNo { get; set; }
        [Column("E.TrkNo")]
        public string TrkNo { get; set; }

        public string LI_PolicyNo { get; set; }

        [Column("E.PFAccountNo")]
        public string PFAccountNo { get; set; }
        public string BA_AccountName { get; set; }
        public string BA_AccountNo { get; set; }

        [Column("E.EMSId")]
        public string EMSId { get; set; }
        public int? EmployeeId { get; set; }

        [Column("E.FatherContactNo")]
        public string FatherContactNo { get; set; }

        [Column("E.MotherContactNo")]
        public string MotherContactNo { get; set; }

        [Column("E.SpouseContactNo")]
        public string SpouseContactNo { get; set; }

        [Column("E.OfficeEmailId")]
        public string OfficeEmailId { get; set; }

        [Column("E.MaritalStatus")]
        public string MaritalStatus { get; set; }

        [Column("E.AnniversaryDate")]
        public DateTime? AnniversaryDateAD { get; set; }
        
        public string AnniversaryDate { get; set; }

        [Column("E.BankName")]
        public string BankName { get; set; }

        [Column("E.BA_Branch")]
        public string BA_Branch { get; set; }

        [Column("E.Qualification")]
        public string Qualification { get; set; }

        [Column("E.NIDNo")]
        public string NIDNo { get; set; }

        [Column("E.FirstNameNP")]
        public string FirstNameNP { get; set; }

        [Column("E.MiddleNameNP")]
        public string MiddleNameNP { get; set; }

        [Column("E.LastNameNP")]
        public string LastNameNP { get; set; }
    }
    public class ImportEmployeeDOB
    {
        public string EMSId { get; set; }
        public string EmpCode { get; set; }
        public string DOB_BS { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOBAD_Str { get; set; }
        public int? EmployeeId { get; set; }
        public int NY { get; set; }
        public int NM { get; set; }
        public int ND { get; set; }
    }

    public class ImportEmployeePhoto
    {
        public ImportEmployeePhoto()
        {
            EmployeeCode = "";           
            PhotoPath = "";
        }
        [Column("E.AutoNumber")]
        public int AutoNumber { get; set; }

        [Column("E.EmployeeCode")]
        public string EmployeeCode { get; set; }

        [Column("E.EnrollNumber")]
        public int EnrollNo { get; set; }

        [Column("E.CardNo")]
        public int CardNo { get; set; }

        [Column("E.EmployeeId")]
        public int EmployeeId { get; set; }

        [Column("E.PhotoPath")]
        public string PhotoPath { get; set; }

        public int PhotoUploadedBy { get; set; }

    
    }

    public class UpdateEmployee  : ImportEmployee
    {
        public string Where { get; set; }
        public string Query { get; set; }
        public string Table
        {
            get
            {
                return "update E set " + Query + "  from tbl_Employee(nolock) E " + Where;
            }
        }
    }

    public class UpdateEmployeeQuery : UpdateEmployee
    {
       public int? DepartmentId { get; set; }
       public int? DesignationId { get; set; }
        public int? CategoryId { get; set; }
        public int? LevelId { get; set; }
        public int? ServiceTypeId { get; set; }
    }
    public class EmployeeResearchPublication
    {
        public int? EmployeeId { get; set; }
        public string ResearchTitle { get; set; } = "";
        public string PublicationDate { get; set; } = "";
        public string JournalName { get; set; } = "";
        public string Coauthors { get; set; } = "";
        public string Abstract_Link { get; set; } = "";
        public string PublicationType { get; set; } = "";
        public string DOI_ISSNNo { get; set; } = "";
    }
    public class EmployeeResearchPublicationCollections : System.Collections.Generic.List<EmployeeResearchPublication>
    {
        public EmployeeResearchPublicationCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}
