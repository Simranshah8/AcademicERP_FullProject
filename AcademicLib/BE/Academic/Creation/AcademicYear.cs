using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Creation
{
    public class AcademicYear : Common
    {
        public int? AcademicYearId { get; set; }
        public int id
        {
            get
            {
                if (AcademicYearId.HasValue)
                    return AcademicYearId.Value;
                return 0;
            }
        }

        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
        public bool IsRunning { get; set; }
        public int? CostClassId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string yearNepali { get; set; } = "";
        public string yearEnglish { get; set; } = "";
    }
    public class AcademicYearCollections : System.Collections.Generic.List<AcademicYear>
    {
        public AcademicYearCollections()
        {
            ResponseMSG = "";
        }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
