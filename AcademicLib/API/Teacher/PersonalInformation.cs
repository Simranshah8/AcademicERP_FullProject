using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.API.Teacher
{
    public class PersonalInformation : ResponeValues
    {
        public DateTime? DOB_AD { get; set; }
        public string BloodGroup { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public int? CasteId { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GrandFather { get; set; }
        public string PersnalContactNo { get; set; }
        public string OfficeContactNo { get; set; }
        public string EmailId { get; set; }
        public string CitizenshipNo { get; set; }
        public int Gender { get; set; }
    }

    public class Emp_PermananetAddress : ResponeValues
    {
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
    }

    public class Emp_TemporaryAddress : ResponeValues
    {
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
    }
    public class CitizenshipDetails : ResponeValues
    {
        public string PanId { get; set; }
        public string CitizenshipNo { get; set; }
        public DateTime? CitizenIssueDate { get; set; }
        public string CitizenShipIssuePlace { get; set; }

    }

    public class CITDetails : ResponeValues
    {
        public string SSFNo { get; set; }
        public string CITCode { get; set; }
        public string CITAcNo { get; set; }
        public double CIT_Amount { get; set; }
        public string CIT_Nominee { get; set; }
        public string CIT_RelationShip { get; set; }
        public string CIT_IDType { get; set; }
        public string CIT_IDNo { get; set; }
        public DateTime? CIT_EntryDate { get; set; }

    }
}
