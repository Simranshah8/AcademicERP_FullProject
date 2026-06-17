using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Fee
{
    public class PendingBillGenerate
    {
        public int ClassId { get; set; }
        public int MonthId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public string MonthName { get; set; }
        public string Batch { get; set; } = "";
        public string ClassYear { get; set; } = "";
        public string Semester { get; set; } = "";
    }
    public class PendingBillGenerateCollections : System.Collections.Generic.List<PendingBillGenerate>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
