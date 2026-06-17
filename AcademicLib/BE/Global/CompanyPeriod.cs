using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public class CompanyPeriodMonth
    {
        public int NY { get; set; }
        public int NM { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string MonthName { get; set; }
        public int DaysInMonth { get; set; }
        public string YearName { get; set; }
        public string MonthYear { get; set; }
        public int MSNo { get; set; }
        public bool IsRunningMonth { get; set; }
    }
    public class CompanyPeriodMonthCollections : System.Collections.Generic.List<CompanyPeriodMonth>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}
