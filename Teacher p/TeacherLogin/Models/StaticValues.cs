using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models
{
    public class StaticValues
    {
        public int? classId { get; set; }
        public int? sectionId { get; set; }
        public string sectionIdColl { get; set; } = "";
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
 
        public int? SubjectId { get; set; }
        public string CodeTH { get; set; } = "";
        public string CodePR { get; set; } = "";
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string ResponseMSG { get; set; } = "";
        public bool IsSuccess { get; set; }
        public bool IsECA { get; set; }
        public bool IsMath { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
        public string Batch { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public bool SubjectTeacher { get; set; }
        public bool ClassTeacher { get; set; }
        public bool CoOrdinator { get; set; }
        public bool HOD { get; set; }
        public string Role { get; set; } = "";
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

        public int? batchId { get; set; }
        public int? classYearId { get; set; }
        public int? semesterId { get; set; }
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