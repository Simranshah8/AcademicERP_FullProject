using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class EmployeeProfile : ResponeValues
    {
        public EmployeeProfile()
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
            OfficeContactNo = "";
            PersonalContactNo = "";

            PasswordNo = "";
         
            PasswordIssuePlace = "";

         
            LI_InsuranceCompany = "";
            LI_PolicyName = "";
            LI_PolicyNo = "";                      
        
            LI_PaymentType = "";
          
            LI_Remarks = "";
            
            HI_InsuranceCompany = "";
            HI_PolicyName = "";
            HI_PolicyNo = "";
         
            HI_Remarks = "";
            MotherTonque = "";
            Rank = "";
            Position = "";
            TeacherType = "";
            TeachingLanguage = "";
            LicenseNo = "";
            TrkNo = "";
            PFAccountNo = "";

            BankName = "";
            BA_AccountName = "";
            BA_AccountNo = "";
            BA_Branch = "";
         

            DrivingLicenceNo = "";
            ServiceType = "";
            RemoteArea = "";
            MitiOfConfirmation = "";
            MitiOfRetirement = "";

        }
        public int EmployeeId { get; set;  }
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
        public string BloodGroup  { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string DateOfJoining_BS { get; set; }
        public DateTime? DateOfJoining_AD { get; set; }

        public string UserName { get; set; }
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
        public string PersonalContactNo { get; set; }

        public List<AcademicQualifications> QualificationColl { get; set; } = new List<AcademicQualifications>();
        public List<WorkExperience> WorkExperienceColl { get; set; } = new List<WorkExperience>();

        public string PasswordNo { get; set; }
        public DateTime? PasswordIssueDate { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public string PasswordIssuePlace { get; set; }

        public int LI_InsuranceType { get; set; }
        public string LI_InsuranceCompany { get; set; }
        public string LI_PolicyName { get; set; }
        public string LI_PolicyNo { get; set; }
        public double LI_PolicyAmount { get; set; }
        public DateTime? LI_PolicyStartDate { get; set; }
        public DateTime? LI_PolicyLastDate { get; set; }
        public double LI_PremiunAmount { get; set; }
        public string LI_PaymentType { get; set; }
        public int LI_StartMonth { get; set; }
        public bool LI_IsDeductFromSalary { get; set; }
        public string LI_Remarks { get; set; }
        public int HI_InsurenceType { get; set; }
        public string HI_InsuranceCompany { get; set; }
        public string HI_PolicyName { get; set; }
        public string HI_PolicyNo { get; set; }
        public double HI_PolicyAmount { get; set; }
        public DateTime? HI_PolicyStartDate { get; set; }
        public DateTime? HI_PolicyLastDate { get; set; }
        public double HI_PremiumAmount { get; set; }
        public int HI_PaymentType { get; set; }
        public int HI_StartMonth { get; set; }
        public bool HI_IsDeductFromSalary { get; set; }
        public string HI_Remarks { get; set; }

        public string MotherTonque { get; set; }
        public string Rank { get; set; }
        public string Position { get; set; }
        public string TeacherType { get; set; }
        public string TeachingLanguage { get; set; }
        public string LicenseNo { get; set; }
        public string TrkNo { get; set; }
        public string PFAccountNo { get; set; }
         
        public string BA_AccountName { get; set; }
        public string BA_AccountNo { get; set; }
        public string BA_Branch { get; set; }
        public bool BA_IsForPayroll { get; set; }

        public string DrivingLicenceNo { get; set; }
        public DateTime? LicenceIssueDate { get; set; }
        public DateTime? LicenceExpiryDate { get; set; }
         
        public bool IsTeaching { get; set; }

        public string EC_PersonalName { get; set; }
        public string EC_Relationship { get; set; }
        public string EC_Address { get; set; }
        public string EC_Phone { get; set; }
        public string EC_Mobile { get; set; }

        public string ServiceType { get; set; }
        public string RemoteArea { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public string MitiOfConfirmation { get; set; }
        public string MitiOfRetirement { get; set; }

        public string Supervisor1 { get; set; }
        public string Supervisor2 { get; set; }
        public string Supervisor3 { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections DocumentColl { get; set; }

        public string Pwd { get; set; }
        public string QRCode { get; set; }
        public bool IsLeft { get; set; }
        public DateTime? LeftDate { get; set; }
        public string LeftDate_BS { get; set; }
        public string LeftRemarks { get; set; }
        public string OfficeEmailId { get; set; }
        public string SpouseContactNo { get; set; }
        public string FatherContactNo { get; set; }
        public string MotherContactNo { get; set; }
        public string AgeDet { get; set; }
        public string ServicePeriod { get; set; }
        public string Level { get; set; }
        public string AccessionNo { get; set; }
        public List<BankDetails> BankDetailsColl { get; set; } = new List<BankDetails>();
        public string QRImage { get; set; } 
        //Added on 25 Poush 2082
        public string EmployeeStatus { get; set; }
        public int? UserId { get; set; }
    }
    public class AcademicQualifications  
    {
        public string DepartmentName { get; set; }
        public string University { get; set; }
        public string PassedYear { get; set; }
        public double GraduatePercentage { get; set; } 
    }

    public class WorkExperience 
    {
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartMiti { get; set; }
        public string EndMiti { get; set; }
        public string Remarks { get; set; } 
    }

    public class BankDetails
    {
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public bool ForPayRoll { get; set; }
    }
}
