using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Exam.Reporting
{
    public class ExamDashboard: ResponeValues
    {
        public ExamDashboard()
        {
            QuoteDet = new QuoteDetail();
            ResultSumDet = new ResultSumm();
            ExamCompVsPendDet = new ExamCompVsPend();
            TotExamSchColl = new TotExamSchCollection();
            MarkEntryStatDet = new MarkEntryStat();
            ClassWiseAttendSumColl = new ClassWiseAttendSumCollection();
            CWPendReExamColl = new CWPendReExamCollection();
            SWPendReExamsColl = new SWPendReExamCollection();
            UpcommingExamColl = new UpcommingExamCollection();
            TopPerformerColl = new TopPerformerCollection();
            AwardColl = new AwardCollection();
            CTConductedColl = new CTConductedCollection();
            CASMarkStatDet = new CASMarkStat();
            ExamResultStatColl = new ExamResultStatCollection();

        }
        public QuoteDetail QuoteDet { get; set; }
        public ResultSumm ResultSumDet { get; set; }
        public ExamCompVsPend ExamCompVsPendDet { get; set; }
        public TotExamSchCollection TotExamSchColl { get; set; }
        public MarkEntryStat MarkEntryStatDet { get; set; }
        public ClassWiseAttendSumCollection ClassWiseAttendSumColl { get; set; }
        public CWPendReExamCollection CWPendReExamColl { get; set; }
        public SWPendReExamCollection SWPendReExamsColl { get; set; }
        public UpcommingExamCollection UpcommingExamColl { get; set; }
        public TopPerformerCollection TopPerformerColl { get; set; }
        public AwardCollection AwardColl { get; set; }
        public CTConductedCollection CTConductedColl { get; set; }
        public CASMarkStat CASMarkStatDet { get; set; }
        public ExamResultStatCollection ExamResultStatColl { get; set; }
    }
    public class QuoteDetail
    {
        public int? QuotesId { get; set; }
        public string Title { get; set; } = "";
        public string ImagePath { get; set; } = "";

    }
    public class ResultSumm
    {
        public int? RSTotalRes { get; set; }
        public int? RSPublished { get; set; }
        public int? RSPending { get; set; }
        public int? UERThisWeek { get; set; }
        public int? UERThisMonth { get; set; }
        public int? RETotal { get; set; }
        public int? REThisWeek { get; set; }
        public int? REThisMonth { get; set; }
    }
    public class ExamCompVsPend
    {
        public int? ExamComVsPenC { get; set; }
        public int? ExamComVsPenP { get; set; }
    }
    public class TotExamSch
    {
        public int? ExamTypeId { get; set; }
        public string ExamName { get; set; } = "";
        public int? TotalExamSch { get; set; }
    }
    public class TotExamSchCollection: System.Collections.Generic.List<TotExamSch>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class MarkEntryStat
    {
        public int? MarkEntryStatComp { get; set; }
        public int? MarkEntryStatPend { get; set; }
    }
    public class ClassWiseAttendSumm
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? CWASPresent { get; set; }
        public int? CWASAbsent { get; set; }
    }
    public class ClassWiseAttendSumCollection: System.Collections.Generic.List<ClassWiseAttendSumm>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class CWPendReExam
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? CWPendReExamNo { get; set; }
    }
    public class CWPendReExamCollection: System.Collections.Generic.List<CWPendReExam>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class SWPendReExam
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? SWReExamPend { get; set; }
    }
    public class SWPendReExamCollection: System.Collections.Generic.List<SWPendReExam>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class UpcommingExam
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public string ExamMitti { get; set; } = "";
        public string Subjects { get; set; } = "";
        public string Teachers { get; set; } = "";
    }
    public class UpcommingExamCollection: System.Collections.Generic.List<UpcommingExam>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class TopPerformer
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public string Remark { get; set; } = "";
        public string PhotoPath { get; set; } = "";
    }
    public class TopPerformerCollection: System.Collections.Generic.List<TopPerformer>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class Award
    {
        public int? StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public string Remark { get; set; } = "";
        public string PhotoPath { get; set; }
    }
    public class AwardCollection: System.Collections.Generic.List<Award>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class CTConducted
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? TotalCT { get; set; }
    }
    public class CTConductedCollection: System.Collections.Generic.List<CTConducted>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
    public class CASMarkStat
    {
        public int? CASSetup { get; set; }
        public int? CASEntry { get; set; }
    }
    public class ExamResultStat
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? ERSTotal { get; set; }
        public int? ERSCompleted { get; set; }
        public int? ERSPending { get; set; }
    }
    public class ExamResultStatCollection: System.Collections.Generic.List<ExamResultStat>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
    }
}