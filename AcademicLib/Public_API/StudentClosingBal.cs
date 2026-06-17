using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.Public_API
{
    public class StudentClosingBal
    {
        public string RegNo { get; set; }
        public string FeeItemName { get; set; }
        public string FeeItemCode { get; set; }
        public double DuesAmt { get; set; }
        public string UptoMonthName { get; set; }
        public int? UpToMonthId { get; set; }
    }
}
