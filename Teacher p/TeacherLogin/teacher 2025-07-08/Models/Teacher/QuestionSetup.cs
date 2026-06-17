using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Teacher
{
    public class QuestionSetup
    {
        public int TranId { get; set; }
        public int ExamSetupId { get; set; }
        public int CategoryId { get; set; }
        public int QNo { get; set; }
        public float Marks { get; set; }
        public int QuestionTitle { get; set; }
        public int AnswerTitle { get; set; }
        public string Question { get; set; }
        public List<DetailsColl> DetailsColl { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }
        public string QuestionPath { get; set; }
        public int AnswerSNo { get; set; }
        public int ExamModal { get; set; }
        public string CategoryName { get; set; }
        public string StudentDocsPath { get; set; }
        public List<string> StudentDocColl { get; set; }
        public string TeacherName { get; set; }
        public string FileType { get; set; }
        public int FileCount { get; set; }
        public int? SubmitType { get; set; }
        public string QuestionRemarks { get; set; }
        public int? StudentAnswerNo { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int? OETranId { get; set; }
        public double ObtainMark { get; set; }
        public string Remarks { get; set; }
        public bool IsChecked { get; set; }
    }

    public class QuestionSetupId
    {
        public int examSetupId { get; set; }
        public int categoryId { get; set; }
    }
    public class DetailsColl
    {
        public int SNo { get; set; }
        public string Answer { get; set; }
        public bool IsRightAnswer { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public string FilePath { set; get; }
        public int AnswerTitle { get; set; }
        public string SNo_Str { get; set; }
        public bool IsCorrect { get; set; }
        public int? OETranId { get; set; }
        public string FileType { get; set; }
        public int FileCount { get; set; }
    }


    public class OnlineExamList
    {
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
        //public string Instruction { get; set; }
        public double DeductMark { get; set; }
        public string ForType { get; set; }
        public object QuestionDetailsColl { get; set; }


    }
    public class BodyValue
    {
        public int forType { get; set; }
    }
}