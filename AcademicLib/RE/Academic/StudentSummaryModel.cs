using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Academic
{
    public class StudentSummaryModel
    {
        // Fixed known columns
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string ClassTeacher { get; set; }
        public string CTContactNo { get; set; }
        public string Batch { get; set; }
        public string Faculty { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public string ClassYear { get; set; }
        public int? TotalStudent { get; set; }

        // Dynamic columns stored here: column name => value
        public Dictionary<string, int> DynamicCounts { get; set; } = new Dictionary<string, int>();
    }
    public class StudentSummaryModelCollections : System.Collections.Generic.List<StudentSummaryModel>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}