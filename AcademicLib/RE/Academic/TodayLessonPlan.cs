using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Academic
{
    public class TodayLessonPlan
    {
        public int LessonId { get; set; }
        public int LessonSNo { get; set; }
        public int TopicSNo { get; set; }
        public int SNo { get; set; }
        public string SubjectName { get; set; }
        public string CoverFilePath { get; set; }
        public string LessonName { get; set; }
        public string TopicName { get; set; }
        public DateTime? ForDate { get; set; }
        public string ForMiti { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Contents { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartRemarks { get; set; }
        public string EndRemarks { get; set; }
        public string StartBy { get; set; }
        public string StartMiti { get; set; }
        public string EndMiti { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        public string Status { get; set; }
        public int StatusValue { get; set; }
        public int TotalDays { get; set; }
        public string ClassName { get; set; }
        public int? ContentSNo { get; set; }
        public string Batch { get; set; }
        public string ClassYear { get; set; }
        public string Semester { get; set; }
    }

    public class TodayLessonPlanCollections : System.Collections.Generic.List<TodayLessonPlan>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
