using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students
{
    public class ExamShedule
    {
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
    }


    public class ExamType {
        public int Duration { get; set; }
        public int forEntity { get; set; }
        public int ExamTypeId { get; set; }
        public DateTime? ResultDate { get; set; }
        public DateTime? ResultTime { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartTime { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }



    }
    public class Val
    {
        public int examTypeId { get; set; }
    }
}