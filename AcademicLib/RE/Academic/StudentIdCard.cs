using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class StudentIdCard
    {
        public StudentIdCard()
        {
            RegNo = "";
            Name = "";
            ClassName = "";
            SectionName = "";
            FatherName = "";
            MotherName = "";
            ContactNo = "";
            F_ContactNo = "";
            M_ContactNo = "";
            Address = "";
            PhotoPath = "";
            BlooedGroup = "";
            DOB_BS = "";
            HouseName = "";
            Medium = "";
            CardNo = "";
            BusPoint = "";
            BusStop = "";
            GuardianName = "";
            G_ContactNo = "";
            G_Relation = "";
            UserName = "";
            ValidFromBS = "";
            ValidToBS = "";
            CompAddress = "";
            CompBannerPath = "";
            CompEmailId = "";
            CompFaxNo = "";
            CompImgPath = "";
            CompLogoPath = "";
            CompName = "";
            CompPanVat = "";
            CompPhoneNo = "";
            CompRegdNo = "";
            CompWebSite = "";
            BranchAddress = "";
            BranchName = "";
            QrCodeStr = "";
        }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string F_ContactNo { get; set; }
        public string M_ContactNo { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string BlooedGroup { get; set; } 
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string HouseName { get; set; }
        public string Medium { get; set; } 
        public int EnrollNo { get; set; }
        public string CardNo { get; set; }
        public string BusPoint { get; set; }
        public string BusStop { get; set; }
        public string GuardianName { get; set; }
        public string G_Relation { get; set; }
        public string G_ContactNo { get; set; }
        public string UserName { get; set; }
        public string ValidFromBS { get; set; }
        public DateTime ValidFromAD { get; set; }
        public string ValidToBS { get; set; }
        public DateTime ValidToAD { get; set; }
        public double DuesAmt { get; set; }
        public string CompName { get; set; }
        public string CompAddress { get; set; }
        public string CompPhoneNo { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmailId { get; set; }
        public string CompWebSite { get; set; }
        public string CompLogoPath { get; set; }
        public string CompImgPath { get; set; }
        public string CompBannerPath { get; set; }
        public string CompRegdNo { get; set; }
        public string CompPanVat { get; set; }
        public string CurrentAddress { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }

        public byte[] QRCode { get; set; }
        public string QrCodeStr { get; set; }
        public byte[] QrInformatic { get; set; }

        public string RouteName { get; set; }
        public string PointName { get; set; }
        public string RoomName { get; set; }
        public string BedName { get; set; }
        public int BedNo { get; set; }

        public string FatherPhotoPath { get; set; }
        public string MotherPhotoPath { get; set; }
        public string GuardianPhotoPath { get; set; }
        public string TravelType { get; set; }

        public string MembershipNo { get; set; }
        public string LibValidFromBS { get; set; }
        public string LibValidToBS { get; set; }
        public DateTime LibValidFromAD { get; set; }
        public DateTime LibValidToAD { get; set; }

        public string Level { get; set; }
        public string Faculty { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public string Batch { get; set; }
        public string EMSId { get; set; }
        public string NextClass { get; set; }
        public int SNo { get; set; }
        public int PageSNo { get; set; }
        public int RSNo { get; set; }
        public string Pwd { get; set; }
        public string ClassTeacher { get; set; } = "";
        public string ClassTecherContactNo { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Caste { get; set; } = "";
        public string Religion { get; set; } = "";
        public string PhysicalDisability { get; set; } = "";
        public int RowSNo { get; set; }
        public string Email { get; set; }
        public string F_Email { get; set; }
        public string M_Email { get; set; }
        public string NameNP { get; set; }
    }

    public class StudentIdCardCollections : System.Collections.Generic.List<StudentIdCard>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
