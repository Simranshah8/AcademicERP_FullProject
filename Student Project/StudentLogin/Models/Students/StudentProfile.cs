using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students
{

    public class StudentProfile : Students.Reponce
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
            AcademicDetailsColl = new List<StudentPreviousAcademicDetails>();
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
        public int CardNo { get; set; }

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
        public List<StudentPreviousAcademicDetails> AcademicDetailsColl { get; set; }
    }

    public class StudentPreviousAcademicDetails
    {
        public string ClassName { get; set; }
        public string Exam { get; set; }
        public string PassoutYear { get; set; }

        public string SymbolNo { get; set; }
        public double ObtainMarks { get; set; }
        public double ObtainPer { get; set; }
        public string Division { get; set; }
        public double GPA { get; set; }
        public string SchoolColledge { get; set; }
        public string BoardName { get; set; }
    }
}