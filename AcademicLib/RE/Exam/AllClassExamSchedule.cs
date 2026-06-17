using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class AllClassExamSchedule
    {
        public int ClassOrderNo { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public DateTime ExamDate_AD { get; set; }
        public string ExamDate_BS { get; set; }
        public int NY { get; set; }
        public int NM { get; set; }
        public int ND { get; set; }
        public string SubjectName { get; set; } 
        public string ExamTime { get; set; }
        public string ExamShift { get; set; }
        public string ExamShiftWithTime { get; set; }

        public string Batch { get; set; } = "";
        public string Semester { get; set; } = "";
        public string ClassYear { get; set; } = "";
    }
    public class AllClassExamScheduleCollections : System.Collections.Generic.List<AllClassExamSchedule>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
