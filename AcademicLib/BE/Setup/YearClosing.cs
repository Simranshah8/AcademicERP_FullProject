using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Setup
{
    public class YearClosing
    {
        public YearClosing()
        {
            ClassColl = new List<YearClosingClassDetails>();
        }
        public int ExamAs { get; set; }
        public int PromoteTo { get; set; }
        public int RollNoAs { get; set; }
        public int SectionAs { get; set; }
        public int FromAcademicYearId { get; set; }
        public int ToAcademicYearId { get; set; }
        public int? CostClassId { get; set; }
        public bool ForwardFeeMapping { get; set; }
        public bool ForwardTransportMapping { get; set; }
        public bool ForwardBedMapping { get; set; }
        public int? OpeningFeeHeadingId { get; set; }
        public bool ForwardDiscountSetup { get; set; }
        public string Pwd { get; set; }
        public string OTP { get; set; }
        public bool ForwardOpening { get; set; }
        public bool ForwardClassSchedule { get; set; }
        public List<YearClosingClassDetails> ClassColl { get; set; }
        public string StudentIdColl { get; set; }
    }
    public class YearClosingClassDetails
    {
        public int FromClassId { get; set; }
        public int ToClassId { get; set; }
        public int? BatchId { get; set; }
        public int? ExamTypeId { get; set; }
        public int? ExamTypeGroupId { get; set; }

        public int? FromClassYearId { get; set; }
        public int? ToClassYearId { get; set; }

        public int? FromSemesterId { get; set; }
        public int? ToSemesterId { get; set; }
    }
}
