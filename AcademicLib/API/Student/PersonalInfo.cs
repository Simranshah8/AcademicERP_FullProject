using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Student
{
    public class PersonalInfo : ResponeValues
    {
        public int Gender { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string Religion { get; set; }
        public int? CasteId { get; set; }
        public string Nationality { get; set; }
        public string BloodGroup { get; set; }
        public string MotherTongue { get; set; }
        public string Height { get; set; }
        public string Weigth { get; set; }
        public string Aim { get; set; }
        public string BirthCertificateNo { get; set; }
        public string CitizenshipNo { get; set; }
        public string Remarks { get; set; }
        public string BoardRegNo { get; set; }
        public bool IsPhysicalDisability { get; set; }
        public string PhysicalDisability { get; set; }
    }

    public class ParentDetails : ResponeValues
    {
        public string FatherName { get; set; }
        public string F_Profession { get; set; }
        public string F_ContactNo { get; set; }
        public string F_Email { get; set; }
        public string MotherName { get; set; }
        public string M_Profession { get; set; }
        public string M_ContactNo { get; set; }
        public string M_Email { get; set; }
    }

    public class ContactInfo : ResponeValues
    {
        public string ContactNo { get; set; }
        public string Email { get; set; }
    }

    public class GuardianDetails : ResponeValues
    {
        public string GuardianName { get; set; }
        public string Relation { get; set; }
        public string G_ContactNo { get; set; }
        public string G_Address { get; set; }
        public string G_Profesion { get; set; }
        public string G_Email { get; set; }
    }

    public class PermanentAddress : ResponeValues
    {
        public string PA_FullAddress { get; set; }
        public string PA_Province { get; set; }
        public string PA_District { get; set; }
        public string PA_LocalLevel { get; set; }
        public int PA_WardNo { get; set; }
        public string PA_Village { get; set; }

        public double PA_LAN { get; set; }
        public double PA_LAT { get; set; }
    }
    public class TemporaryAddress : ResponeValues
    {
        public string CA_FullAddress { get; set; }
        public string CA_Province { get; set; }
        public string CA_District { get; set; }
        public string CA_LocalLevel { get; set; }
        public int CA_WardNo { get; set; }
        public string CA_Village { get; set; }
        public double CA_LAN { get; set; }
        public double CA_LAT { get; set; }

    }
}
