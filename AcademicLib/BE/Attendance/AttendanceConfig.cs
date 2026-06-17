using Dynamic.BusinessEntity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BE.Attendance
{
    public class AttendanceConfig : ResponeValues
    {
        public int? TranId { get; set; }
        public double MonthlyMinAttendance { get; set; }
        public double MaxConsecutiveAbs { get; set; }
        public double AbsFinePerDay { get; set; }      

    }
   
}