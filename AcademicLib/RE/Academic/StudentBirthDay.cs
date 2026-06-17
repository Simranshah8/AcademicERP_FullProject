using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class StudentBirthDay
    {
        public StudentBirthDay()
        {
            RegdNo = "";
            Name = "";
            ClassName = "";
            SectionName = "";
            FatherName = "";
            ContactNo = "";
            Address = "";
            PhotoPath = "";
            Template = "";

            Email = "";

             CompName = "";
         CompAddress = "";
        CompPhoneNo = "";
        CompFaxNo = "";
         CompEmail = "";
         CompWebSite = "";
        CompLogoPath = "";
          CompImgPath = "";
          CompBannerPath = "";
    }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string FatherName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public int AgeYear { get; set; }
        public int AgeMonth { get; set; }
        public int AgeDay { get; set; }
        public string PhotoPath { get; set; }
        public DateTime DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string ClassSection
        {
            get
            {
                return ClassName + " " + SectionName;
            }
        }
        public string Age
        {
            get
            {
                string a = "";

                if (AgeYear > 0)
                    a = AgeYear.ToString() + " Y ";

                if (AgeMonth > 0)
                    a = a + AgeMonth.ToString() + "M ";

                if (AgeDay > 0)
                    a = a + AgeDay.ToString() + " D ";

                return a;
            }
        }
        public string Email { get; set; }

        public string CompName { get; set; }
        public string CompAddress { get; set; }
        public string CompPhoneNo { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmail { get; set; }
        public string CompWebSite { get; set; }
        public string CompLogoPath { get; set; }
        public string CompImgPath { get; set; } 
        public string CompBannerPath { get; set; }
        public string Template { get; set; }
        public string F_Email { get; set; }
        public string M_Email { get; set; }
        public string G_Email { get; set; }
    }
    public class StudentBirthDayCollections : System.Collections.Generic.List<StudentBirthDay>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
