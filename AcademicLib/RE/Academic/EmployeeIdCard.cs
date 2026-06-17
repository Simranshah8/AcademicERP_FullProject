using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class EmployeeIdCard
    {
        public EmployeeIdCard()
        {
            EmployeeCode = "";
            Name = "";
            Gender = "";
            Category = "";
            Department = "";
            Designation = "";
            FatherName = "";
            MotherName = "";
            ContactNo = "";
            PA_FullAddress = "";
            TA_FullAddress = "";
            PhotoPath = "";
            BloodGroup = "";
            DOB_BS = "";
            UserName = "";
            ValidFromBS = "";
            ValidToBS = "";

            CompName = "";
        CompAddress = "";
        CompPhoneNo = "";
        CompFaxNo = "";
        CompEmailId = "";
        CompWebSite = "";
        CompLogoPath = "";
        CompImgPath = "";
        CompBannerPath = "";
        CompRegdNo = "";
        CompPanVat = "";

        BranchName = "";
        BranchAddress = "";
          
        JoiningMiti = "";
        MembershipNo = "";
        LibValidFromBS = "";
        LibValidToBS = "";
      
        CitizenshipNo = "";
   
        TRKNo = "";
        Rank = "";
        ServiceType = "";
        PAN = "";
        PersonalContact = "";
        Level = "";
    }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string PA_FullAddress { get; set; }
        public string TA_FullAddress { get; set; }
        public string PhotoPath { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public int EnrollNo { get; set; }
        public int CardNo { get; set; }
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

        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string QrCode { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string JoiningMiti { get; set; }
        public string MembershipNo { get; set; }
        public string LibValidFromBS { get; set; }
        public string LibValidToBS { get; set; }
        public DateTime? LibValidFromAD { get; set; }
        public DateTime? LibValidToAD { get; set; }
        public string CitizenshipNo { get; set; }        
        public byte[] QrImage { get; set; }
        public byte[] QrInformatic { get; set; }
        public string TRKNo { get; set; }
        public string Rank { get; set; }
        public string ServiceType { get; set; }
        public string PAN { get; set; }
        public string PersonalContact { get; set; }
        public string Level { get; set; }

        public string EmployeeEmailId { get; set; } = "";
        public string SpouseName { get; set; } = "";
        public string Pwd { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public bool IsTeaching { get; set; } 
        public string Religion { get; set; } = "";
        public string Nationality { get; set; } = "";
        public string PersonalContactNo { get; set; } = "";
        public string MaritalStatus { get; set; } = "";
        public string GrandFatherName { get; set; } = "";
        public string DrivingLicenceNo { get; set; } = "";
        public string PassPortNo { get; set; } = "";
        public string SSFNo { get; set; } = "";
        public string CITCode { get; set; } = "";
        public string EMSID { get; set; } = "";
        public string FatherContactNo { get; set; } = "";
        public string MotherContactNo { get; set; } = "";
        public string SpouseContactNo { get; set; } = "";
        public string PhysicalDisability { get; set; } = "";
        public string SubjectTeacher { get; set; } = "";
        public string ClassTeacherOf { get; set; } = "";
        public string OfficialContactNo { get; set; } = "";


    }
    public class EmployeeIdCardCollections : System.Collections.Generic.List<EmployeeIdCard>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
