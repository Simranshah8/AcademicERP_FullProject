using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE
{
    public class PassoutStudents : ResponeValues
    {
        public string ClassYear { get; set; }
        public string Semester { get; set; }       
        public string BoardName { get; set; }
        public int? BoardTypeId { get; set; }
        public string BoardRegdNo { get; set; }
        public bool IsLeft { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public int UserId { get; set; }
        public int AutoNumber { get; set; }
        public int StudentId { get; set; }
        public int? SectionId { get; set; }
        public int ClassId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? PassOutClassId { get; set; }
        public string PassOutSymbolNo { get; set; }
        public string PassOutGPA { get; set; }
        public string PassOutGrade { get; set; }
        public string PassOutRemarks { get; set; }

        public int? StatusId { get; set; }
        public int? PassoutOptId { get; set; }
        public int? DropoutOptId { get; set; }
        public DateTime? PassoutDate { get; set; }
    }
    public class PassoutStudentsCollections : System.Collections.Generic.List<PassoutStudents>
    {
        public PassoutStudentsCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentsForPassout : ResponeValues
    {
        public string ClassYear { get; set; }
        public string Semester { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string FatherContact { get; set; }
        public string FatherName { get; set; }
        public string LeftRemarks { get; set; }
        public string LeftDate_BS { get; set; }
        public DateTime? LeftDate_AD { get; set; }
        public string BoardName { get; set; }
        public int? BoardTypeId { get; set; }
        public string BoardRegdNo { get; set; }
        public bool IsLeft { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public int UserId { get; set; }
        public int AutoNumber { get; set; }
        public int StudentId { get; set; }
        public int? SectionId { get; set; }
        public int ClassId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public int? PassOutClassId { get; set; }
        public string PassOutSymbolNo { get; set; }
        public double PassOutGPA { get; set; }
        public string PassOutGrade { get; set; }
        public string PassOutRemarks { get; set; }
        public int? StatusId { get; set; }
        public int? PassoutOptId { get; set; }
        public int? DropoutOptId { get; set; }
        public DateTime? PassoutDate { get; set; }
    }
    public class StudentsForPassoutCollections : System.Collections.Generic.List<StudentsForPassout>
    {
        public StudentsForPassoutCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}