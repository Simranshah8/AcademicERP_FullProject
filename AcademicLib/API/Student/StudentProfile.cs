using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Student
{
    public class StudentProfile : ResponeValues
    {
        public StudentProfile()
        {
            RegdNo = "";            
            Name = "";
            Gender = "";
            ClassName = "";
            SectionName = "";
            HouseName = "";
            BloodGroup = "";            
            DOB_BS = "";
            Address = "";
            BusRoot = "";
            BusStop = "";
            FatherName = "";
            F_ContactNo = "";
            MotherName = "";
            M_ContactNo = "";
            GuardianName = "";
            G_ContactNo = "";
            Address = "";
            Relation = "";
            PhotoPath = "";

            BoardName = "";
            BoardRegNo = "";
            PhysicalDisability = "";
            AdmitDate_BS = "";
            Height = "";
            Weigth = "";
            ContactNo = "";
            Email = "";
            F_Profession = "";
            M_Profession = "";
            F_Email = "";
            M_Email = "";
            PA_FullAddress = "";
            CA_FullAddress = "";
            LastPaymentDate_BS = "";
            AcademicDetailsColl = new List<BE.Academic.Transaction.StudentPreviousAcademicDetails>();

            Religion = "";
            Nationality = "";
            MotherTongue = "";
            Aim = "";
            BirthCertificateNo = "";
            CitizenshipNo = "";
            Remarks = "";

            G_Profesion = "";
            G_Email = "";

            PA_Province = "";
            PA_District = "";
            PA_LocalLevel = "";
            PA_Village = "";
            CA_Province = "";
            CA_District = "";
            CA_LocalLevel = "";
            CA_Village = "";
            AcademicYear = "";
            StudentType = "";
            Medium = "";
            TransportPoint = "";
            BoarderName = "";

            SignaturePath = "";
            SiblingDetailColl = new List<SiblingDetail>();
        }
        public int StudentId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public int EnrollNumber { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string HouseName { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string Address { get; set; }
        public string BusRoot { get; set; }
        public string BusStop { get; set; }
        public string FatherName { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; }
        public string M_ContactNo { get; set; }
        public string GuardianName { get; set; }
        public string G_ContactNo { get; set; }
        public string G_Address { get; set; }
        public string Relation { get; set; }
        public string PhotoPath { get; set; }

        public string BoardName { get; set; }
        public string BoardRegNo { get; set; }
        public string PhysicalDisability { get; set; }
        public bool IsPhysicalDisability { get; set; }
        public int? Age { get; set; }
        public DateTime? AdmitDate_AD { get; set; }
        public string AdmitDate_BS { get; set; }
        public string Height { get; set; }
        public string Weigth { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string F_Profession { get; set; }
        public string M_Profession { get; set; }
        public string F_Email { get; set; }
        public string M_Email { get; set; }
        public string PA_FullAddress { get; set; } 
        public string CA_FullAddress { get; set; } 
        public double FeeAmount { get; set; }
        public double PayAmount { get; set; }
        public double Discount { get; set; }
        public double DuesAmount { get; set; }
        public DateTime? LastPaymentDate_AD { get; set; }
        public string LastPaymentDate_BS { get; set; }
        public double LastPaymentAmt { get; set; }
        public string UserName { get; set; }
        public long CardNo { get; set; }
        public string Religion { get; set; }
        public int? CasteId { get; set; }
        public string Nationality { get; set; }
        public string MotherTongue { get; set; }
        public string Aim { get; set; }
        public string BirthCertificateNo { get; set; }
        public string CitizenshipNo { get; set; }
        public string Remarks { get; set; }

        public string G_Profesion { get; set; }
        public string G_Email { get; set; }

        public string PA_Province { get; set; }
        public string PA_District { get; set; }
        public string PA_LocalLevel { get; set; }
        public int? PA_WardNo { get; set; }
        public string PA_Village { get; set; }
        public string CA_Province { get; set; } 
        public string CA_District { get; set; }
        public string CA_LocalLevel { get; set; }
        public int? CA_WardNo { get; set; }
        public string CA_Village { get; set; }
        public string AcademicYear { get; set; }
        public bool IsNewStudent { get; set; }
        public string StudentType { get; set; }
        public string Medium { get; set; }
        public string TransportPoint { get; set; }
        public string BoarderName { get; set; }

        public string SignaturePath { get; set; }

        public double PA_LAN { get; set; }
        public double PA_LAT { get; set; }
        public double CA_LAN { get; set; }
        public double CA_LAT { get; set; }

        public string Batch { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string LedgerPanaNo { get; set; } = "";

        public string VehicleNo { get; set; } = "";
        public double TransportFare { get; set; } = 0;

        public int? UserId { get; set; }
        public string Pwd { get; set; }
        public string QrCode { get; set; }
        public string Branch { get; set; }         
        public bool IsLeft { get; set; }
        public DateTime? LeftDate_AD { get; set; }
        public string LeftDate_BS { get; set; }
        public string LeftRemarks { get; set; }

        public string Caste { get; set; }
        public string AgeDet { get; set; }

        public string EMSId { get; set; }

        public List<BE.Academic.Transaction.StudentPreviousAcademicDetails> AcademicDetailsColl { get; set; }
        public List<SiblingDetail> SiblingDetailColl { get; set; }

        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? BranchId { get; set; }
        public string QRImage { get;set; } 
        
        public string StudentStatus { get;set; }
    }

    public class SiblingDetail
    {
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public string Remarks { get; set; }
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string PhotoPath { get; set; }
        public bool IsLeft { get; set; }
        public DateTime? LeftDate_AD { get; set; }
        public string LeftDate_BS { get; set; }
        public string LeftRemarks { get; set; }
    }
    public class SiblingDetailCollections : System.Collections.Generic.List<SiblingDetail>
    {
        public List<StudentAcademicDetail> AcademicDetails { get; set; } = new List<StudentAcademicDetail>();
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class StudentAcademicDetail
    {
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string AcademicYear { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public int RollNo { get; set; }
        public string BoardRegNo { get; set; }
        public bool IsRunning { get; set; }
        public string LeftStatus { get; set; }
        public string LeftDate_BS { get; set; }
        public DateTime? LeftDate_AD { get; set; }
    }
}
