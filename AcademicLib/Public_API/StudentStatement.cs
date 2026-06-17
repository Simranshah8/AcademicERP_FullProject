using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.Public_API
{
    public class StudentStatement
    {
        public string RegNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string ForMonth { get; set; }
        public string Particular{ get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string DrCr { get; set; }
        public double Balance { get; set; }
        public string Remarks { get; set; }
        public int MonthId { get; set; }
    }
}
