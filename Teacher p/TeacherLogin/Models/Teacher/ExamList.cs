using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class ExamList
    {
        public ExamList()
        {
            ExamTypeName = "";
            ExamDate_BS = "";
            SubjectName = "";
            Lession = "";
            StartTime = "";
            Instruction = "";
            ForType = "";
        }
        public int ExamSetupId { get; set; }

        public string ClassName { get; set; }
        public string ExamTypeName { get; set; }
        public DateTime ExamDate_AD { get; set; }
        public string ExamDate_BS { get; set; }
        public string SubjectName { get; set; }
        public string Lession { get; set; }
        public string StartTime { get; set; }
        public double Duration { get; set; }
        public double FullMark { get; set; }
        public double PassMarks { get; set; }
        public string Instruction { get; set; }
        public double DeductMark { get; set; }
        public string ForType { get; set; }

        public List<QuestionSummary> QuestionDetailsColl { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int NoOfStudent { get; set; }
        public int NoOfPresent { get; set; }
        public int NoOfAbsent { get; set; }
    }
    public class ExamListCollections : System.Collections.Generic.List<ExamList>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class QuestionSummary
    {
        public string CategoryName { get; set; }
        public string ExamModal { get; set; }
        public int NoOfQuestion { get; set; }
        public double Mark { get; set; }
        public double Total { get; set; }
    }
}