using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BE.Exam.Setup
{
    public class Configuration : ResponeValues
    {
        public Configuration()
        {
            ResultForPassStudent = "PASS";
            ResultForFailStudent = "FAIL";
            AbsentSymbol = "AB";
            ResultForPassWithGraceMark = "PASS";
            StudentRankAs = 1;
            StudentCommentAs = 1;
            ClassWiseRankList = new List<ClassWiseRankAs>();
            ClassWiseCommentList = new List<ClassWiseCommentAs>();
            ClassWiseDuesList = new List<ClassWiseDuesAs>();
            ConditionForFailPassList = new List<ConditionForFailPassAs>();
            ClassWiseStarSymbolList = new List<ClassWiseStarSymbolAs>();
            ExamConfigForAppList = new List<ExamConfigForApp>();
            ReportTemplateList = new List<Dynamic.BusinessEntity.Global.ReportTempletes>();
            ClassWiseGPAList = new List<ClassWiseGPAAs>();
            ClassWiseGPList = new List<ClassWiseGPAs>();
            PassFailCondition1 = 1;
            PassFailCondition2 = 2;
            PassFailResult1 = 2;
            PassFailResult2 = 1;
            SymbolForReExam = "";
            ResultForPassedReExam = "";
            ResultForFailedReExam = "";
        }
        public bool IsShowRankForFailStudent { get; set;  }
        public bool IsShowDivisionForFailStudent { get; set; }
        public bool IsShowGradeForFailStudent { get; set; }
        public bool IsAllowGraceMarkStudentwise { get; set; }
        public bool IsAllowGraceMarkSubjectwise { get; set; }
        public bool IsAllowGraceMarkClassWise { get; set; }
        public bool IsAllowMarkEntry { get; set; }
        public bool IsShowForFailStudent { get; set; }
        public bool IsMatchSubjectStudentWise { get; set; }
        public string ResultForPassStudent { get; set; }
        public string ResultForFailStudent { get; set; }
        public string AbsentSymbol { get; set; }
        public string ResultForPassWithGraceMark { get; set; }
        public string WithHeldSymbol { get; set; }

        public string SymbolForReExam { get; set; }
        public string ResultForPassedReExam { get; set; }
        public string ResultForFailedReExam { get; set; }
        public int NoOfDecimalPlace { get; set; }
        public int StudentRankAs { get; set; }
        public int StudentCommentAs { get; set; }
        public int GPAAs { get; set; }
        public int GPAs { get; set; }
        public int PassFailCondition1 { get; set; }
        public int PassFailResult1 { get; set; }

        public int PassFailCondition2 { get; set; }
        public int PassFailResult2 { get; set; }

        public bool ShowStartForFailTH { get; set; }
        public bool ShowStartForFailPR { get; set; }
        public bool ShowStartForFail { get; set; }
        public bool AllowExtraSubjectInSubjectMapping { get; set; }
        public bool ForClassWiseComment { get; set; }
        public bool AllowSubjectWiseComment { get; set; }
        public string FailDivision { get; set; }
        public int FailDivisionAs { get; set; }
        public bool ForClassWiseRank { get; set; }        
        public bool ShowStartForClassWise { get; set; }
        public bool PassFailConditionClassWise { get; set; }
        public bool ShowResultSummaryForStudent { get; set; }
        public bool ShowResultSummaryForClassTeacher { get; set; }
        public bool ShowResultSummaryForAdmin { get; set; }
        public bool ClassWiseGPA { get; set; }
        public bool ClassWiseGP { get; set; }
        public List<ClassWiseRankAs> ClassWiseRankList { get; set; }
        public List<ClassWiseCommentAs> ClassWiseCommentList { get; set; }
        public List<ClassWiseDuesAs> ClassWiseDuesList { get; set; }
        public List<ConditionForFailPassAs> ConditionForFailPassList { get; set; }
        public List<ClassWiseStarSymbolAs> ClassWiseStarSymbolList { get; set; }
        public List<ExamConfigForApp> ExamConfigForAppList { get; set; }
        public List<Dynamic.BusinessEntity.Global.ReportTempletes> ReportTemplateList { get; set; }
        public List<ClassWiseGPAAs> ClassWiseGPAList { get; set; }
        public List<ClassWiseGPAs> ClassWiseGPList { get; set; }
        public bool ActiveMajorMinor { get; set; }
        public int? GradeId { get; set; }
        public int? NoOfGrade { get; set; }
        public bool CalculateECASubject { get; set; }
        public int? SubjectGradeId { get; set; }
        public bool AllowAllSubjectCT { get; set; } = true;
        //Added on 19 magh 2082 by suresh
        public bool MarksheetTeacherBeforeResult { get; set; } = true;
        public bool MarksheetClassTeacherBeforeResult { get; set; } = true;
        public bool MarksheetAdminBeforeResult { get; set; } = true;
        public bool TabulationTeacherBeforeResult { get; set; } = true;
        public bool TabulationClassTeacherBeforeResult { get; set; } = true;
        public bool TabulationAdminBeforeResult { get; set; } = true;
        public bool IncludeReExamMarks { get; set; } = true;
        //Added
        public bool ClassWiseDivGrade { get; set; } = true;
        public bool ExamTypeWiseGrade { get; set; } = true;
    }

    public class ClassWiseRankAs
    {
        public int ClassId { get; set; }
        public int RankAs { get; set; }
    }

    public class ClassWiseCommentAs
    {
        public int ClassId { get; set; }
        public int CommentAs { get; set; }
    }

    public class ClassWiseDuesAs
    {
        public int ClassId { get; set; }
        public int UptoMonthId { get; set; }

        public double DuesAmt { get; set; }
    }
    public class ConditionForFailPassAs
    {
        public int ClassId { get; set; }
        public int Condition1 { get; set; }
        public int Result1 { get; set; }
        public int Condition2 { get; set; }
        public int Result2 { get; set; }
    }
    public class ClassWiseStarSymbolAs
    {
        public int ClassId { get; set; }
        public bool ShowStartForFailTH { get; set; }
        public bool ShowStartForFailPR { get; set; }
        public bool ShowStartForFail { get; set; }
    }

    public class ClassWiseGPAAs
    {
        public int ClassId { get; set; }
        public int GPAAs { get; set; }
    }
    public class ExamConfigForApp
    {
        public int ClassId { get; set; }

        public int? UptoMonthId { get; set; }
        public double MinDues { get; set; }
        public int MarkType { get; set; }
        public int? GeneralMarkSheet_RptTranId { get; set; }
        public int? GroupMarkSheet_RptTranId { get; set; }
        public int? AdmitCard_RptTranId { get; set; }
    }

    public class SeatPlanConfiguraion
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }
        public int ExamTypeId { get; set; }
    }


    public class ClassWiseGPAs
    {
        public int ClassId { get; set; }
        public int GPAs { get; set; }
    }
    public class ClassWiseMinDues : ResponeValues
    {
        public int ClassId { get; set; }
        public double MinDues { get; set; }
    }
}
