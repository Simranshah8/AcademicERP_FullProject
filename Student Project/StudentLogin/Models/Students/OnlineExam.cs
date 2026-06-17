using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students
{
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
        public string Instruction { get; set; }
        public double DeductMark { get; set; }
        public string ForType { get; set; }
        public List<QuestionDetails> QuestionDetailsColl { get; set; }


    }
    public class QuestionDetails
    {
        public string CategoryName { get; set; }
        public string ExamModal { get; set; }
        public int NoOfQuestion { get; set; }
        public double Mark { get; set; }
        public double Total { get; set; }
    }
    public class BodyValue
    {
        public int forType { get; set; }
        public int examSetupId { get; set; }
    }
    public class StartClass
    {
        public int ExamSetupId { get; set; }
        public string Location { get; set; }
        public float Lat { get; set; }
        public float Lan { get; set; }
        public string Notes { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public HttpFileCollectionBase SelectedFiles { get; set; }

    }
    public class Reponce
    {
        public bool IsSuccess { get; set; } 
        public string ResponseMSG { get; set; } 
        public DateTime? StartDateTime { get; set; } 
        public DateTime? EndDateTime { get; set; } 

    }


    //Getting Quection
    public class DetailsColl
    {
        public int SNo { get; set; }
        public string Answer { get; set; }
        public string FilePath { get; set; }
        public string SNo_Str { get; set; }
    }

    public class OnlineExamQuestion
    {

        public int TranId { get; set; }
        public int QNo { get; set; }
        public double Marks { get; set; }
        public int QuestionTitle { get; set; }
        public string Question { get; set; }
        public string QuestionPath { get; set; }
        public List<DetailsColl> DetailsColl { get; set; }
        public string CategoryName { get; set; }
        public int ExamModal { get; set; }
        public int? SubmitType { get; set; }
        public string QuestionRemarks { get; set; }
        public int? StudentAnswerNo { get; set; }
        public string AnswerText { get; set; }

        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public string FileType { get; set; }
        public int FileCount { get; set; }
        public string StudentDocsPath { get; set; }
        public List<string> StudentDocColl { get; set; }
    }
  public class SubmitOEAnswer
    {
        public int ExamSetupId { get; set; }
        public int TranId { get; set; }
        public string Location { get; set; }
        public float Lat { get; set; }
        public float Lan { get; set; }
        public string QuestionRemarks { get; set; }
        public int? AnswerSNo { get; set; }
        public string AnswerText { get; set; }
        public int? SubmitType { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public HttpPostedFileBase file2 { get; set; }
        public List<string> StudentDocColl { get; set; }
    }
}