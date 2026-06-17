using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class ExamSchedule
    {
        public ExamSchedule()
        {
            ExamName = "";
            ClassName = "";
            StartDate_BS = "";
            EndDate_BS = "";
            ExamDate_BS = "";
            Notes = "";
            Remarks = "";
            Code = "";
            CodeTH = "";
            CodePR = "";
            PaperType = "";
            SectionName = "";
            RoomName = "";
            ColumnName = "";
        }
        public int ExamTypeId { get; set; }
        public string ExamName { get; set; }
        public string ClassName { get; set; }
        public DateTime StartDate_AD { get; set; }
        public string StartDate_BS { get; set; }
        public DateTime EndDate_AD { get; set; }
        public string EndDate_BS { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Notes { get; set; }
        public string SubjectName { get; set; }
        public string Code { get; set; }
        public string CodeTH { get; set; }
        public string CodePR { get; set; }
        public DateTime ExamDate_AD { get; set; }
        public string ExamDate_BS { get; set; }
        public string Remarks { get; set; }

        public string PaperType { get; set; }
        public string SectionName { get; set; }

        public string RoomName { get; set; }
        public int BenchNo { get; set; }
        public int SeatNo { get; set; }
        public string ColumnName { get; set; }
        public string ExamShift { get; set; }
        public string ExamStatus { get; set; }
    }
    public class ExamScheduleCollections : System.Collections.Generic.List<ExamSchedule>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}
