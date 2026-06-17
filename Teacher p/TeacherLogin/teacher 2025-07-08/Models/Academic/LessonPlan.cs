using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherLogin.Models.Academic
{
    public class LessonPlan : TeacherLogin.Models.Responce
    {
        public LessonPlan()
        {
            DetailsColl = new List<LessonPlanDetails>();
        }
        public int? TranId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int NoOfLesson { get; set; }
        public string CoverFilePath { get; set; }
        public List<LessonPlanDetails> DetailsColl { get; set; }

        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
    public class LessonPlanCollections : System.Collections.Generic.List<LessonPlan>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class LessonPlanDetails
    {
        public LessonPlanDetails()
        {
            TopicColl = new List<LessonTopic>();
            ContentsColl = new LessonTopicTeacherContentCollections();
        }
        public int LessonId { get; set; }
        public int SNo { get; set; }
        public string LessonName { get; set; }

        public DateTime? PlanStartDate_AD { get; set; }
        public DateTime? PlanEndDate_AD { get; set; }
        public string PlanStartDate_BS { get; set; }
        public string PlanEndDate_BS { get; set; }

        public List<LessonTopic> TopicColl { get; set; }
        public LessonTopicTeacherContentCollections ContentsColl { get; set; }


        public DateTime? StartDate_AD { get; set; }
        public DateTime? EndDate_AD { get; set; }
        public string StartDate_BS { get; set; }
        public string EndDate_BS { get; set; }

        public string StartRemarks { get; set; }
        public string EndRemarks { get; set; }

        public int? StartBy { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }

        public string Status { get; set; }
        public int StatusValue { get; set; }
        public int TotalDays { get; set; }

        public double PendingPer { get; set; }
        public double InProgressPer { get; set; }
        public double CompletedPer { get; set; }

    }
    public class LessonTopic
    {
        public LessonTopic()
        {
            ContentsColl = new LessonTopicTeacherContentCollections();
        }

        public int LessonId { get; set; }
        public int SNo { get; set; }
        public string TopicName { get; set; }
        public DateTime? PlanStartDate_AD { get; set; }
        public DateTime? PlanEndDate_AD { get; set; }
        public string PlanStartDate_BS { get; set; }
        public string PlanEndDate_BS { get; set; }

        public LessonTopicTeacherContentCollections ContentsColl { get; set; }

        public DateTime? StartDate_AD { get; set; }
        public DateTime? EndDate_AD { get; set; }
        public string StartDate_BS { get; set; }
        public string EndDate_BS { get; set; }

        public string StartRemarks { get; set; }
        public string EndRemarks { get; set; }

        public int? StartBy { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }

        public string Status { get; set; }
        public int StatusValue { get; set; }
        public int TotalDays { get; set; }

    }

    public class LessonTopicTeacherContent
    {
        public int LessonId { get; set; }
        public int LessonSNo { get; set; }
        public int TopicSNo { get; set; }
        public DateTime ForDate { get; set; }
        public string Contents { get; set; }
        public int SNo { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ForDateBS { get; set; }


        public DateTime? StartDate_AD { get; set; }
        public DateTime? EndDate_AD { get; set; }
        public string StartDate_BS { get; set; }
        public string EndDate_BS { get; set; }

        public string StartRemarks { get; set; }
        public string EndRemarks { get; set; }

        public int? StartBy { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }

        public string Status { get; set; }
        public int StatusValue { get; set; }
        public int TotalDays { get; set; }
    }
    public class LessonTopicTeacherContentCollections : System.Collections.Generic.List<LessonTopicTeacherContent>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class LessonTopicContent
    {
        public int LessonId { get; set; }
        public int LessonSNo { get; set; }
        public int TopicSNo { get; set; }
        public int SNo { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class LessonTopicContentCollections : System.Collections.Generic.List<LessonTopicContent>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class LessonTopicVideo
    {
        public int LessonId { get; set; }
        public int LessonSNo { get; set; }
        public int TopicSNo { get; set; }
        public int SNo { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Remarks { get; set; }
    }
    public class LessonTopicVideoCollections : System.Collections.Generic.List<LessonTopicVideo>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class LessonTopicQuiz : TeacherLogin.Models.Responce
    {
        public LessonTopicQuiz()
        {
            QuestionColl = new List<LessonTopicQuizQuestion>();
        }
        public int LessonId { get; set; }
        public int LessonSNo { get; set; }
        public int TopicSNo { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int NoOfQuestion { get; set; }
        public double FullMark { get; set; }
        public double PassMark { get; set; }
        public int Duration { get; set; }
        public List<LessonTopicQuizQuestion> QuestionColl { get; set; }
    }
    public class LessonTopicQuizQuestion
    {
        public LessonTopicQuizQuestion()
        {
            AnswerColl = new List<LessonTopicQuizAnswer>();
        }
        public int QuestionId { get; set; }
        public int SNo { get; set; }
        public int QuizId { get; set; }
        public int QuestionType { get; set; }
        public int AnswerType { get; set; }
        public string QuestionContent { get; set; }
        public string ContentPath { get; set; }
        public double Mark { get; set; }
        public int Duration { get; set; }
        public int AnswerSNo { get; set; }
        public List<LessonTopicQuizAnswer> AnswerColl { get; set; }
    }
    public class LessonTopicQuizAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerType { get; set; }
        public int SNo { get; set; }
        public string AnswerContent { get; set; }
        public string ContentPath { get; set; }
        public bool IsRightAnswer { get; set; }
    }

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
    }

    public class TodayLessonPlanCollections : System.Collections.Generic.List<TodayLessonPlan>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }
}