using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Models.Teacher
{
    public class ExamSetup
    {

        public int? ExamSetupId { get; set; }
        public int ExamTypeId { get; set; }
        public int ClassId { get; set; }
        public string SectionIdColl { get; set; }
        public int SubjectId { get; set; }
        public string Lesson { get; set; }
        public string ExamDate { get; set; }
        public string StartTime { get; set; }
        public string ResultDate { get; set; }
        public string ResultTime { get; set; }
        public int Duration { get; set; }
        public float FullMarks { get; set; }
        public float PassMarks { get; set; }
        [AllowHtml]
        public string Instruction { get; set; }
        public bool IsAlerttoStudents { get; set; }
        public bool IsIncludeNegativeMark { get; set; }
        public float DeductMark { get; set; }
        public List<QuestionModelDetails> QuestionModelColl { get; set; }
     
        public string ExamTypeName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public DateTime ExamDate_AD { get; set; }
        public string ExamDate_BS { get; set; }
        public DateTime ResultDate_AD { get; set; }
        public string ResultDate_BS { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; }
        public bool IsAlterToStudents { get; set; }

        public string TeacherName { get; set; }

        public bool ShuffleQuestions { get; set; }

    }
    public class QuestionModelDetails
    {
        public int CategoryId { get; set; }
        public int NoOfQuestion { get; set; }
        public float Marks { get; set; }
        public float Total { get; set; }

      
        public int ExamSetupId { get; set; }
        public int SNo { get; set; }
        public string CategoryName { get; set; }
        public int ExamModal { get; set; }
    }
    public class ExamSetupId
    {
        public int examSetupId { get; set; }

    }
    public class Val
    {
        public int classId { get; set; }
        public int sectioncoll { get; set; }
        public int sectionIdColl { get; set; }
        public int examTypeId { get; set; }
    }
    public class ExamShedule
    {
        public int ExamTypeId { get; set; }
        public string ExamName { get; set; }
        public string ClassName { get; set; }

        public string SectionName { get; set; }
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
    }

}