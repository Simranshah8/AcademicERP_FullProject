using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models
{
    public class StaticValues
    {
    }

    public class HomeworkType
    {
        public int HomeworkTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class AssignmentType
    {
        public int AssignmentTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClassSchedule
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public int NoOfBreak { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int DayId { get; set; }
        public int Period { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string TeacherContactNo { get; set; }
        public string TeacherAddress { get; set; }

        
        public int Duration { get; set; }
        public string ForType { get; set; }
        public string TeacherPhotoPath { get; set; }
        public string SectionIdColl { get; set; }

    }

    public class JoinOnlineClass
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
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
    }
}