using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class TeacherProfile
    {
        public TeacherProfile()
        {
            Code = "";
            Name = "";
            Department = "";
            Designation = "";
            Category = "";
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Gender = "";
            DOB_AD = null;
            DOB_BS = "";
            Qualification = "";
            WorkExperience = "";
            SSFNo = "";
            CITCode = "";
            CITAccountNo = "";
            BankName = "";
            BankAccountNo = "";
            LifeInsuranceCompany = "";
            LifeInsurancePolicyNo = "";
            LifeInsurancePaymentType = "";
            PhotoPath = "";
            CurrentAddress = "";
            CurrentDistrict = "";
            PermanentAddress = "";
            PermanentDistrict = "";
            FatherName = "";
            MotherName = "";
            SpouseName = "";
            About = "";
            ContactNo = "";
            BloodGroup = "";
            Nationality = "";
            Religion = "";
            DateOfJoining_BS = "";
        }
        public int EmployeeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int EnrollNumber { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Category { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string Qualification { get; set; }
        public string WorkExperience { get; set; }
        public int TotalDocument { get; set; }
        public string SSFNo { get; set; }
        public string CITCode { get; set; }
        public string CITAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string LifeInsuranceCompany { get; set; }
        public string LifeInsurancePolicyNo { get; set; }
        public double LifeInsurancePAmount { get; set; }
        public string LifeInsurancePaymentType { get; set; }
        public string PhotoPath { get; set; }

        public string CurrentAddress { get; set; }
        public string CurrentDistrict { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentDistrict { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }

        public string About { get; set; }
        public string ContactNo { get; set; }
        public string BloodGroup { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string DateOfJoining_BS { get; set; }
        public DateTime? DateOfJoining_AD { get; set; }
        public string UserName { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

        public string SignaturePath { get; set; }
        public string EMSId { get; set; }
        public string EmailId { get; set; }
        public DateTime? AnniversaryDateAD { get; set; }
        public string AnniversaryDateBS { get; set; }
        public int? CasteId { get; set; }
        public string CasteName { get; set; }
        public string GrandFather { get; set; }
        public string PA_Country { get; set; }
        public string PA_State { get; set; }
        public string PA_Zone { get; set; }
        public string PA_District { get; set; }
        public string PA_City { get; set; }
        public string PA_Municipality { get; set; }
        public int PA_Ward { get; set; }
        public string PA_Street { get; set; }
        public string PA_HouseNo { get; set; }
        public string PA_FullAddress { get; set; }
        public string TA_Country { get; set; }
        public string TA_State { get; set; }
        public string TA_Zone { get; set; }
        public string TA_District { get; set; }
        public string TA_City { get; set; }
        public string TA_Municipality { get; set; }
        public int TA_Ward { get; set; }
        public string TA_Street { get; set; }
        public string TA_HouseNo { get; set; }
        public string TA_FullAddress { get; set; }
        public string MaritalStatus { get; set; }

        public string PanId { get; set; }
        public string CitizenshipNo { get; set; }
        public DateTime? CitizenIssueDate { get; set; }
        public string CitizenShipIssuePlace { get; set; }

        public string CITAcNo { get; set; }
        public double CIT_Amount { get; set; }
        public string CIT_Nominee { get; set; }
        public string CIT_RelationShip { get; set; }
        public string CIT_IDType { get; set; }
        public string CIT_IDNo { get; set; }
        public DateTime? CIT_EntryDate { get; set; }

        public string OfficeContactNo { get; set; }
        public string Supervisor1 { get; set; }
        public string Supervisor2 { get; set; }
        public string Supervisor3 { get; set; }
        public string EC_PersonalName { get; set; }
        public string EC_Relationship { get; set; }
        public string EC_Address { get; set; }
        public string EC_Phone { get; set; }
        public string EC_Mobile { get; set; }
        public string PersonalContactNo { get; set; }
        public string DrivingLicenceNo { get; set; }
        public DateTime? LicenceIssueDate { get; set; }
        public DateTime? LicenceExpiryDate { get; set; }
        public string PasswordNo { get; set; }

        public DateTime? PasswordIssueDate { get; set; }

        public DateTime? PasswordExpiryDate { get; set; }

        public string PasswordIssuePlace { get; set; }
        public string LI_InsuranceType { get; set; }
        public string LI_InsuranceCompany { get; set; }
        public string LI_PolicyName { get; set; }
        public string LI_PolicyNo { get; set; }
        public string LI_PolicyAmount { get; set; }
        public DateTime? LI_PolicyStartDate { get; set; }
        public DateTime? LI_PolicyLastDate { get; set; }
        public string LI_PremiunAmount { get; set; }
        public string LI_PaymentType { get; set; }
        public string LI_StartMonth { get; set; }
        public bool LI_IsDeductFromSalary { get; set; }
        public string LI_Remarks { get; set; }
        public string HI_InsurenceType { get; set; }
        public string HI_InsuranceCompany { get; set; }
        public string HI_PolicyName { get; set; }
        public string HI_PolicyNo { get; set; }
        public string HI_PolicyAmount { get; set; }
        public DateTime? HI_PolicyStartDate { get; set; }
        public DateTime? HI_PolicyLastDate { get; set; }
        public string HI_PremiumAmount { get; set; }
        public string HI_PaymentType { get; set; }
        public string HI_StartMonth { get; set; }
        public bool HI_IsDeductFromSalary { get; set; }

        public string HI_Remarks { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public string RemoteArea { get; set; }
        public bool IsTeaching { get; set; }
        public string ServiceType { get; set; }
        public string MotherTonque { get; set; }
        public string Rank { get; set; }
        public string Position { get; set; }
        public string TeacherType { get; set; }
        public string TeachingLanguage { get; set; }
        public string LicenseNo { get; set; }
        public string TrkNo { get; set; }
        public string PFAccountNo { get; set; }
        public string MitiOfConfirmation { get; set; }
        public string MitiOfRetirement { get; set; }

        private List<EmployeeAcademicQualification> _AcademicQualification = new List<EmployeeAcademicQualification>();
        public List<EmployeeAcademicQualification> QualificationColl
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

        private List<EmployeeDocument> _Document = new List<EmployeeDocument>();
        public List<EmployeeDocument> DocumentColl
        {
            get { return _Document; }
            set { _Document = value; }
        }
        public class EmployeeWorkExperience
        {
            public string Organization { get; set; }
            public string Department { get; set; }
            public string JobTitle { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string Remarks { get; set; }

        }
        public class EmployeeAcademicQualification
        {
            public string DepartmentName { get; set; }
            public string University { get; set; }
            public string PassedYear { get; set; }
            public float GraduatePercentage { get; set; }

        }

        public class EmployeeDocument
        {
            public int Id { get; set; }
            public string Extension { get; set; }
            public string Name { get; set; }
            public DateTime? LogDateTime { get; set; }
            public string DocPath { get; set; }
            public string Description { get; set; }
            public string DocumentTypeName { get; set; }
            public string DocFullPath { get; set; }

        }
    }
}