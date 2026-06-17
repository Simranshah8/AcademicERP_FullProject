using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.RE.Homework.Reporting
{
    public class HAAnalysis: ResponeValues
    {
        public TotalHWCollections TotalHWColl { get; set; }
        public TotalAssignCollections TotalAssignColl { get; set; }
        public CSWSummaryHWCollections CSWSummaryHWColl { get; set; }
        public CSWSummaryAssignCollections CSWSummaryAssignColl { get; set; }
        public TeachPerformanceHWCollections TPHWColl { get; set; }
        public TeachPerformanceAssignCollections TPAColl { get; set; }
        public SPRHWCollections SPRHWColl { get; set; }
        public SPRAssignCollections SPRAssignColl { get; set; }
        public TopPerformerCollections TopPerformerColl { get; set; }
        public StudentNeedAttentCollections studentNeedAttent { get; set; }
        public SubVsOverdueHW subVsOverdueHW { get; set; }
        public SubVsOverdueAssign subVsOverdueAssign { get; set; }
        public HAAnalysis()
        {
            TotalHWColl = new TotalHWCollections();
            TotalAssignColl = new TotalAssignCollections();
            CSWSummaryHWColl = new CSWSummaryHWCollections();
            CSWSummaryAssignColl = new CSWSummaryAssignCollections();
            TPHWColl = new TeachPerformanceHWCollections();
            TPAColl = new TeachPerformanceAssignCollections();
            SPRHWColl = new SPRHWCollections();
            SPRAssignColl = new SPRAssignCollections();
            TopPerformerColl = new TopPerformerCollections();
            studentNeedAttent = new StudentNeedAttentCollections();
            subVsOverdueAssign = new SubVsOverdueAssign();
            subVsOverdueHW = new SubVsOverdueHW();
        }

    }
    public class TotalHW
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? THSubmitted { get; set; }
        public int? THPending { get; set; }
        public int? THOverdue { get; set; }
    }
    public class TotalHWCollections: System.Collections.Generic.List<TotalHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class TotalAssign
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? TASubmitted { get; set; }
        public int? TAPending { get; set; }
        public int? TAOverdue { get; set; }
    }
    public class TotalAssignCollections: System.Collections.Generic.List<TotalAssign>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class CSWSummaryHW
    {
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; }
        public int? CSWTH { get; set; }
        public int? CSWTHSubmitted { get; set; }
        public int? CSWTHPending { get; set; }
        public int? CSWTHOverdue { get; set; }
    }
    public class CSWSummaryHWCollections: System.Collections.Generic.List<CSWSummaryHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class CSWSummaryAssign
    {
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public string ClassName { get; set; } = "";
        public string SectionName { get; set; }
        public int? CSWTA { get; set; }
        public int? CSWTASubmitted { get; set; }
        public int? CSWTAPending { get; set; }
        public int? CSWTAOverdue { get; set; }
    }
    public class CSWSummaryAssignCollections: System.Collections.Generic.List<CSWSummaryAssign>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class TeachPerfomanceHW
    {
        public int? EmployeeId { get; set; }
        public string TeacherName { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public int? TPTHGiven { get; set; }
        public int? TPTHOverdue { get; set; }
        public double? TPTHSubmittedPct { get; set; }
    }
    public class TeachPerformanceHWCollections: System.Collections.Generic.List<TeachPerfomanceHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class SubVsOverdueHW
    {
        public double? HWOverdueByTeach { get; set; }
        public double? HWSubByTeach { get; set; }
    }
    public class TeachPerformanceAssign
    {
        public int? EmployeeId { get; set; }
        public string TeacherName { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public int? TPTAGiven { get; set; }
        public int? TPTAOverdue { get; set; }
        public double? TPTASubmittedPct { get; set; }
    }
    public class TeachPerformanceAssignCollections: System.Collections.Generic.List<TeachPerformanceAssign>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class SubVsOverdueAssign
    {
        public double? AssignOverdueByTeach { get; set; }
        public double? AssignSubByTeach { get; set; }
    }
    public class SPRHW
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public int? SPRTotal { get; set; }
        public double? SPRSubmittedPct { get; set; }
        public int? SPRSubmitted { get; set; }
        public double? SPRAvgMark { get; set; }
        public int? SPRFullMark { get; set; }
    }
    public class SPRHWCollections: System.Collections.Generic.List<SPRHW>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class SPRAssign
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; } = "";
        public double? SPRSubmittedPct { get; set; }
        public int? SPRSubmitted { get; set; }
        public double? SPRAvgMark { get; set; }
        public int? SPRTotal { get; set; }
        public int? SPRFullMark { get; set; }
    }
    public class SPRAssignCollections: System.Collections.Generic.List<SPRAssign>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }
    public class TopPerformer
    {
        public int? StudentId { get; set; }
        public string Name { get; set; } = "";
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public double? AvgMarks { get; set; } 
        public double? TotalMarks { get; set; }
    }

    public class TopPerformerCollections: System.Collections.Generic.List<TopPerformer>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }

    public class StudentNeedAttent
    {
        public int? StudentId { get; set; }
        public string Name { get; set; } = "";
        public int? ClassId { get; set; }
        public string ClassName { get; set; } = "";
        public int? SectionId { get; set; }
        public string SectionName { get; set; } = "";
        public string PhotoPath { get; set; } = "";
        public double? AvgMarks { get; set; }
        public double? TotalMarks { get; set; }
    }

    public class StudentNeedAttentCollections : System.Collections.Generic.List<StudentNeedAttent>
    {
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; } = "";
    }


}