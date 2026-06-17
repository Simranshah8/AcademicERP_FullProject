using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class EventHoliday
    {
        public string HolidayEvent { get; set; }
        public string EventType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FromDate_AD { get; set; }
        public DateTime ToDate_AD { get; set; }
        public string FromDate_BS { get; set; }
        public string ToDate_BS { get; set; }
        public string ForClass { get; set; }
        public string ColorCode { get; set; }
        public string ImagePath { get; set; }

        public string Remaining { get; set; }
    }
}