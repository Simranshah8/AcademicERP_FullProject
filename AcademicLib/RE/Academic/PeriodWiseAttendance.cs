using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Attendance
{
    public class PeriodWiseAttendance : ResponeValues
    {
        public int? StudentId { get; set; }
        public int? YearId { get; set; }
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }
        public string Day8 { get; set; }
        public string Day9 { get; set; }
        public string Day10 { get; set; }
        public string Day11 { get; set; }
        public string Day12 { get; set; }
        public string Day13 { get; set; }
        public string Day14 { get; set; }
        public string Day15 { get; set; }
        public string Day16 { get; set; }
        public string Day17 { get; set; }
        public string Day18 { get; set; }
        public string Day19 { get; set; }
        public string Day20 { get; set; }
        public string Day21 { get; set; }
        public string Day22 { get; set; }
        public string Day23 { get; set; }
        public string Day24 { get; set; }
        public string Day25 { get; set; }
        public string Day26 { get; set; }
        public string Day27 { get; set; }
        public string Day28 { get; set; }
        public string Day29 { get; set; }
        public string Day30 { get; set; }
        public string Day31 { get; set; }
        public string Day32 { get; set; }
        public int? TotalDays { get; set; }
        public int? TotalWeekEnd { get; set; }
        public int? TotalHoliday { get; set; }
        public int? TotalPresent { get; set; }
        public int? TotalLeave { get; set; }
        public int? TotalAbsent { get; set; }
        public string Subject { get; set; }
        public int? PeriodId { get; set; }

    }
    public class PeriodWiseAttendanceCollections : System.Collections.Generic.List<PeriodWiseAttendance>
    {
        public PeriodWiseAttendanceCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
