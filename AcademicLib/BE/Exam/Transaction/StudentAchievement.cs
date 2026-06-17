using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AcademicLib.BE.Exam.Transaction
{
    public class StudentListForAchievement : ResponeValues
    {
        public int? StudentId { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string BoardRegdNo { get; set; }
    }
    public class StudentListForAchievementCollections : System.Collections.Generic.List<StudentListForAchievement>
    {
        public StudentListForAchievementCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class StudentAchievement : ResponeValues
    {
        public int? StudentId { get; set; }
        public string Remarks { get; set; }      
        public double? Point { get; set; }
        public int ExamTypeId { get; set; }
        public int RemarksTypeId { get; set; }
        public int RemarksFor { get; set; }
        public DateTime? ForDate { get; set; }
    }
    public class StudentAchievementCollections : System.Collections.Generic.List<StudentAchievement>
    {
        public StudentAchievementCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class PrevAchievement : ResponeValues
    {       
        public string Remarks { get; set; }
        public double Point { get; set; }
        public int? TranId { get; set; }

    }
    public class PrevAchievementCollections : System.Collections.Generic.List<PrevAchievement>
    {
        public PrevAchievementCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}