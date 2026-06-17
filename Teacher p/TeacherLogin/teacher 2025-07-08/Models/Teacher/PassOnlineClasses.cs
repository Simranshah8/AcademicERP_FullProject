using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class PassCollData
    {
        public int StudentId { get; set; }
        public string ContactNo { get; set; }
        public int NoOfStudent { get; set; }
        public int NoOfPresent { get; set; }
        public string FirstJoinAt { get; set; }
        public string LastJoinAt { get; set; }
        public int PresentMinute { get; set; }
        public DateTime ForDate { get; set; }
        public int TranId { get; set; }
        public int PlatformType { get; set; }
        public string ShiftName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartDateTime_AD { get; set; }
        public string StartDate_BS { get; set; }
        public DateTime EndDateTime_AD { get; set; }
        public string EndDate_BS { get; set; }
        public bool IsRunning { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Link { get; set; }
        public string TeacherName { get; set; }
        public int Duration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }




        //public object StartDateBS { get; set; }
        //public object StartDateTime { get; set; }
        //public object EndDateBS { get; set; }
        //public object EndDateTime { get; set; }
        //public int TimeDiff { get; set; }

        //public string ClassAttem { get; set; }
        //public int Present { get; set; }
        //public int UserId { get; set; }



    }
    public class PassOnlineClasses
    {
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public DateTime ForDate { get; set; }
        public DateTime Date_AD { get; set; }
        public string Date_BS { get; set; }
        public List<PassCollData> DataColl { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
        public int tranId { set; get; }


        


    }

    public class GetOnlineClassAttById
    {
        public int StudentId { get; set; }
        public int AutoNumber { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string PhotoPath { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string FirstJoinAt { get; set; }
        public string LastJoinAt { get; set; }
        public int LateMinute { get; set; }
        public int AttendanceType { get; set; }
        public int Duration { get; set; }
        public string UserName { get; set; }
    }

    public class ResOnlineClassAttById
    {
        public List<GetOnlineClassAttById> PresentColl { get; set; }
        public List<GetOnlineClassAttById> AbsentColl { get; set; }
        public List<GetOnlineClassAttById> LateColl { get; set; }
        public List<GetOnlineClassAttById> LeaveColl { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}