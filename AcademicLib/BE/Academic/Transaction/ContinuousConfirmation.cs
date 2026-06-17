using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Academic.Transaction
{
    public class ContinuousConfirmation : ResponeValues
    {
        public int? TranId { get; set; }
        public int BranchId { get; set; }
        public int? AcademicYearId { get; set; }
        public int StudentId { get; set; }
        public bool ContinueYes { get; set; }
        public bool ContinueNo { get; set; }
        public int? NotContinueReasonId { get; set; }
        public string OtherReason { get; set; } = "";
        public string Feedback { get; set; } = "";
        public string NotContinueReason { get; set; } = "";
        public string Name { get; set; } = "";
        public string RegNo { get; set; } = "";
        public string Gender { get; set; } = "";
        public int RollNo { get; set; }
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string FatherName { get; set; } = "";
        public string FContactNo { get; set; } = "";
        public string MotherName { get; set; } = "";
        public string MContactNo { get; set; } = "";
        public string GuardianName { get; set; } = "";
        public string GContactNo { get; set; } = "";
        public string Continuous { get; set; } = "";
    }
    public class ContinuousConfirmationCollections : List<ContinuousConfirmation>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
