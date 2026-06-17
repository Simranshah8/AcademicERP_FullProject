using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class StudentRecord
    {
        public string classId { get; set; }
        public string sectionId { get; set; }
        public string sectionIdColl { get; set; }
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public int EnrollNo { get; set; }
        public int CardNo { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string BoardRegdNo { get; set; }
        public DateTime? DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string ContactNo { get; set; }

    }
    public class Remarks
    {
        public DateTime forDate { get; set; }
        public string studentIdColl { get; set; }
        public string description { get; set; }
        public int remarksTypeId { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        
    }
    public class Notice
    {
        public string studentIdColl { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public HttpPostedFileBase file1 { get; set; }
    }

    public class HeightAndWeight : StudentRecord
    {
       
        public int examTypeId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Remarks { get; set; }
     
        public int AcademicYearId { get; set; }
    }
    public class HeightAndWeightCollections : List<HeightAndWeight>
    {
        public HeightAndWeightCollections()
        {
            ResponseMSG = "";
        }

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}