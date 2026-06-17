using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Academic
{
    public class ClassScheduleStatus : ResponeValues
    {
        public ClassScheduleStatus()
        {
            completedColl = new CompletedColl();
            pendingColl = new PendingColl();
        }
        public CompletedColl completedColl { get; set; }
        public PendingColl pendingColl { get; set; }
    }
    public class ClassScheduleStatusColl : List<ClassScheduleStatus>
    {
        public ClassScheduleStatusColl()
        {
            ResponseMSG = " ";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class Completed : ResponeValues
    {

        public string ClassSection { get; set; } = "";
        public string Shift { get; set; } = "";
        public int? TotalPeriod { get; set; }
        public int? PeriodAssigned { get; set; }
        public string TeacherName { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        public string User { get; set; } = "";//change in to Sting
        public int? UserId { get; set; }
        public DateTime? LogDateTime { get; set; }
    }
    public class CompletedColl : List<Completed>
    {
        public CompletedColl()
        {
            ResponseMSG = " ";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class Pending : ResponeValues
    {
        public string ClassSection { get; set; } = "";
    }
    public class PendingColl : List<Pending>
    {
        public PendingColl()
        {
            ResponseMSG = " ";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

}