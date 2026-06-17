using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class OnlineClasses
    {
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
    }

    public class OnlineClassesList
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public DateTime ForDate { get; set; }
        public DateTime Date_AD { get; set; }
        public string Date_BS { get; set; }
       
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }

        private List<OnlineClasses> _DataCollection = new List<OnlineClasses>();
        public List<OnlineClasses> DataColl
        {
            get { return _DataCollection; }
            set { _DataCollection = value; }
        }
    }
}