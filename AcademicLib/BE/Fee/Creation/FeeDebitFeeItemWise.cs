using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Fee.Creation
{
    public class FeeDebitFeeItemWise
    {
        public int MonthId { get; set; }
        public int FeeItemId { get; set; }
        public double Amount { get; set; }
        public string ClassIdColl { get; set; }
        public string SectionIdColl { get; set; }

        public int? BatchId { get; set; }
        public int? SemesterId { get; set; }
        public int? ClassYearId { get; set; }
    }
}
