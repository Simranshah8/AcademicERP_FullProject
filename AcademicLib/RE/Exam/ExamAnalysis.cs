using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Exam.Reporting
{
    public class ExamAnalysis:ResponeValues
    {
        public ExamAnalysis()
        {
            ExamPerAnalysisColl = new ExamPerAnalysisCollection();
            MarkDistByExamColl = new MarkDistByExamCollection();
            AttendAnalysisColl = new AttendAnalysisCollection();
            ReExamMarkAnalysisColl = new ReExamMarkAnalysisCollection();
            BulkAttendColl = new BulkAttendCollection();
            ETResultDispatchColl = new ETResultDispatchCollection();
            CWPerAnalysisColl = new CWPerAnalysisCollection();
            ExamAnalysisTPColl = new ExamAnalysisTPCollection();
            CWAchievementColl = new CWAchievementCollection();
            CASPerformanceAnalysisDet = new CASPerformanceAnalysis();
            ClassWiseCASSumColl = new ClassWiseCASSumCollection();
        }
        public ExamPerAnalysisCollection ExamPerAnalysisColl { get; set; }
        public MarkDistByExamCollection MarkDistByExamColl { get; set; }
        public AttendAnalysisCollection AttendAnalysisColl { get; set; }
        public ReExamMarkAnalysisCollection ReExamMarkAnalysisColl { get; set; }
        public BulkAttendCollection BulkAttendColl { get; set; }
        public ETResultDispatchCollection ETResultDispatchColl { get; set; }
        public CWPerAnalysisCollection CWPerAnalysisColl { get; set; }
        public ExamAnalysisTPCollection ExamAnalysisTPColl { get; set; }
        public CWAchievementCollection CWAchievementColl { get; set; }
        public CASPerformanceAnalysis CASPerformanceAnalysisDet { get; set; }
        public ClassWiseCASSumCollection ClassWiseCASSumColl { get; set; }
    }
    public class ExamPerAnalysis
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? EPAAverage { get; set; }
        public int? EPAPass { get; set; }
    }
    public class ExamPerAnalysisCollection: System.Collections.Generic.List<ExamPerAnalysis>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class MarkDistByExam
    {
        public int? ExamTypeId { get; set; }
        public string ExamName { get; set; } = "";
        public int? MDELowestMark { get; set; }
        public int? MDEHighestMark { get; set; }
    }
    public class MarkDistByExamCollection: System.Collections.Generic.List<MarkDistByExam>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class AttendAnalysis
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? AATotal { get; set; }
    }
    public class AttendAnalysisCollection: System.Collections.Generic.List<AttendAnalysis>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class ReExamMarkAnalysis
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? REMARegular { get; set; }
        public int? REMAReExam { get; set; }
    }
    public class ReExamMarkAnalysisCollection: System.Collections.Generic.List<ReExamMarkAnalysis>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class BulkAttend
    {
        public int? ExamTypeId { get; set; }
        public string ExamName { get; set; } = "";
        public int? BulkAttendTot { get; set; }
    }
    public class BulkAttendCollection: System.Collections.Generic.List<BulkAttend>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class ETResultDispatch
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? ETRDTotal { get; set; }
    }
    public class ETResultDispatchCollection: System.Collections.Generic.List<ETResultDispatch>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class CWPerAnalysis
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? CWPAAvg { get; set; }
        public int? CWPAPass { get; set; }
    }
    public class CWPerAnalysisCollection: System.Collections.Generic.List<CWPerAnalysis>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class ExamAnalysisTP
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public double? TPPer { get; set; }
        public int? TPSubjectId { get; set; }
        public string TPSubjectName { get; set; } = "";
    }
    public class ExamAnalysisTPCollection : System.Collections.Generic.List<ExamAnalysisTP>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class CWAchievement
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public int? CWANoOfStud { get; set; }
        public string CWASubjectName { get; set; }
    }
    public class CWAchievementCollection: System.Collections.Generic.List<CWAchievement>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class CASPerformanceAnalysis
    {
        public int? CASPASetup { get; set; }
        public int? CASPAEntry { get; set; }
    }
    public class ClassWiseCASSum
    {
        public int? ClassId { get; set;}
        public string ClassName { get; set; }
        public int? CWCASTot { get; set; }
    }
    public class ClassWiseCASSumCollection: System.Collections.Generic.List<ClassWiseCASSum>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}