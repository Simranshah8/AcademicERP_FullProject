using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Global
{
    public class CurrentDate
    {
        public DateTime Date_AD { get; set; }
        public string Date_BS { get; set; }
        public int NY { get; set; }
        public int NM { get; set; }
        public int ND { get; set; }
        public string MonthName { get; set; }
        public int StartDayId { get; set; }
        public int DaysInMonth { get; set; }
    }
}
