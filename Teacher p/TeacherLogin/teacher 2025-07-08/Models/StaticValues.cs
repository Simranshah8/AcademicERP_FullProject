using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class StaticValues
    {
    }

    public class HomeworkColl
    {
        public int HomeworkTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class Subject
    {
        public int SubjectId { get; set; }
        public string CodeTH { get; set; }
        public string CodePR { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
   
    public class ExamType
    {
        public int Duration { get; set; }
        public int forEntity { get; set; }
        public int ExamTypeId { get; set; }
        public int ExamTypeGroupId { get; set; }
        public DateTime? ResultDate { get; set; }
        public DateTime? ResultTime { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartTime { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ExamSetupDetails
    {
        public int examTypeId { get; set; }
        public int classId { get; set; }
        public string sectionIdColl { get; set; }
        public int subjectId { get; set; }
        public int? sectionId { get; set; }
    }
    public class Responce
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int RId { get; set; }

        public string ResponseId { get; set; }


    }
    public class UserDetail
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public int EntityId { get; set; }
        public int ErrorNumber { get; set; }
        public string PhotoPath { get; set; }
    }
}