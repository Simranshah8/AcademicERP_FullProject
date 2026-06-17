using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Report
{
    public class StudentDetails : ResponeValues
    {

        public int? StudentId { get; set; }
        public int? SystemId { get; set; }
        public string RegNo { get; set; } = "";
        public DateTime? AdmitDate { get; set; }
        public string AdmitDateMiti { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string StudentNameNP { get; set; } = "";
        public string Gender { get; set; } = "";
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; } = "";
        public string Religion { get; set; } = "";
        public bool? isEDJ { get; set; }
        public string EDJ { get; set; } = "";
        public string Caste { get; set; } = "";
        public string Nationality { get; set; } = "";
        public string BloodGroup { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string Email { get; set; } = "";
        public string MotherTongue { get; set; } = "";
        public string Height { get; set; } = "";
        public string Weigth { get; set; } = "";
        public bool? IsPhysicalDisability { get; set; }
        public string PhysicalDisability { get; set; } = "";
        public string Aim { get; set; } = "";
        public string BirthCertificateNo { get; set; } = "";
        public string CitizenshipNo { get; set; } = "";
        public string NIDNo { get; set; } = "";
        public string EnquiryNo { get; set; } = "";
        public string ReferralCode { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public string SignaturePath { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string FacultyName { get; set; } = "";
        public string LevelName { get; set; } = "";
        public string SemesterName { get; set; } = "";
        public string BatchName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public int? RollNo { get; set; } 
        public string AcademicYear { get; set; } = "";
        public bool? IsNewStudent { get; set; }
        public string StudentType { get; set; } = "";
        public string Medium { get; set; } = "";
        public string HouseName { get; set; } = "";
        public string HouseDress { get; set; } = "";
        public string TransportPoint { get; set; } = "";
        public string BoardersName { get; set; } = "";
        public string BoardName { get; set; } = "";
        public string BoardRegNo { get; set; } = "";
        public int? EnrollNo { get; set; } 
        public long? CardNo { get; set; } 
        public string EMSId { get; set; } = "";
        public string LedgerPanaNo { get; set; } = "";
        public string FirstofClass { get; set; } = "";
        public string FatherName { get; set; } = "";
        public string F_Profession { get; set; } = "";
        public string F_ContactNo { get; set; } = "";
        public string F_Email { get; set; } = "";
        public double? F_AnnualIncome { get; set; }
        public string FatherPhotoPath { get; set; } = "";
        public string MotherName { get; set; } = "";
        public string M_Profession { get; set; } = "";
        public string M_Contact { get; set; } = "";
        public string M_Email { get; set; } = "";
        public double? M_AnnualIncome { get; set; } 
        public string MotherPhotoPath { get; set; } = "";
        public int? IfGurandianIs { get; set; }
        public string GuardianName { get; set; } = "";
        public string G_Relation { get; set; } = "";
        public string G_Profesion { get; set; } = "";
        public string G_ContactNo { get; set; } = "";
        public string G_Email { get; set; } = "";
        public string G_Address { get; set; } = "";
        public string GuardianPhotoPath { get; set; } = "";
        public string DonorName { get; set; } = "";
        public string PA_FullAddress { get; set; } = "";
        public string PA_Province { get; set; } = "";
        public string PA_District { get; set; } = "";
        public string PA_LocalLevel { get; set; } = "";
        public int? PA_WardNo { get; set; } 
        public string PA_Village { get; set; } = "";
        public string PA_HouseNo { get; set; } = "";
        public string CA_Country { get; set; } = "";
        public string CA_FullAddress { get; set; } = "";
        public string CA_Province { get; set; } = "";
        public string CA_District { get; set; } = "";
        public string CA_LocalLevel{ get; set; } = "";
        public int? CA_WardNo { get; set; } 
        public string StreetName { get; set; } = "";
        public string CA_HouseNo { get; set; } = "";
        public string PassOutClass { get; set; } = "";
        public string PassOutSymbolNo { get; set; } = "";
        public double? PassOutGPA { get; set; } 
        public string PassOutRemarks { get; set; } = "";
        public string CitizenFrontPhoto { get; set; } = "";
        public string CitizenBackPhoto { get; set; } = "";
        public string NIDPhoto { get; set; } = "";
        public string PaidUpToMonthNP { get; set; } = "";
        public string PassOutGrade { get; set; } = "";
        public SiblingDetailsCollections SiblingDetailColl { get; set; }
        public StudentAcademicCollections AcademicDetailsColl { get; set; }
        public Dynamic.BusinessEntity.GeneralDocumentCollections AttachmentColl { get; set; }

        public StudentDetails()
        {
            SiblingDetailColl = new SiblingDetailsCollections();
            AcademicDetailsColl = new StudentAcademicCollections();
            AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
        }

    }
    public class StudentCollections : List<StudentDetails>
    {
        public StudentCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class SiblingDetails : ResponeValues
    {
        public int? StudentID { get; set; }
        public string ClassSection { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string Relation { get; set; } = "";
        public string Remarks { get; set; } = "";

    }
    public class SiblingDetailsCollections : List<SiblingDetails>
    {
        public SiblingDetailsCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class StudentAcademicDetails : ResponeValues
    {
        public int? StudentID { get; set; }
        public string ClassName { get; set; } = "";
        public string Level { get; set; } = "";
        public string University { get; set; } = "";
        public string Exam { get; set; } = "";
        public string PassoutYear { get; set; } = "";
        public string SymbolNo { get; set; } = "";
        public double? ObtainMarks { get; set; } 
        public double? ObtainPer { get; set; }
        public string Division { get; set; } = "";
        public double? GPA { get; set; }
        public string SchoolCollege { get; set; } = "";

    }
    public class StudentAcademicCollections : List<StudentAcademicDetails>
    {
        public StudentAcademicCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

}
