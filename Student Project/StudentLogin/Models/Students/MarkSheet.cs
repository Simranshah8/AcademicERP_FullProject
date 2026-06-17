using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentLogin.Models.Students.MarkSheet
{

    public class Val
    {
       public int examTypeId { get; set; }
     }
    public class MarkSheet
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public DataColl DataColl { get; set; }
    }

    public class MarkSheetPDF
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseId { get; set; }
        
    }
    public class DetailsColl
    {
        public int SNo { get; set; }
        public string SubjectName { get; set; }
        public int SubjectId { get; set; }
        public int PaperType { get; set; }
        public string CodeTH { get; set; }
        public string CodePR { get; set; }
        public bool IsOptional { get; set; }
        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double CR { get; set; }
        public double FMTH { get; set; }
        public double FMPR { get; set; }
        public double FM { get; set; }
        public double PMTH { get; set; }
        public double PMPR { get; set; }
        public double PM { get; set; }
        public bool IsInclude { get; set; }
        public object StudentRemarks { get; set; }
        public object SubjectRemarks { get; set; }
        public double OTH { get; set; }
        public double OPR { get; set; }
        public double ObtainMark { get; set; }
        public string ObtainMark_Str { get; set; }
        public string ObtainMarkTH_Str { get; set; }
        public string ObtainMarkPR_Str { get; set; }
        public bool IsFail { get; set; }
        public bool IsFailTH { get; set; }
        public bool IsFailPR { get; set; }
        public double Per { get; set; }
        public double Per_TH { get; set; }
        public double Per_PR { get; set; }
        public double GP { get; set; }
        public double GP_TH { get; set; }
        public double GP_PR { get; set; }
        public string Grade { get; set; }
        public string GradeTH { get; set; }
        public string GradePR { get; set; }
        public string GP_Grade { get; set; }
        public string GP_GradeTH { get; set; }
        public string GP_GradePR { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsAbsentTH { get; set; }
        public bool IsAbsentPR { get; set; }
        public double H_OM { get; set; }
        public double H_OM_TH { get; set; }
        public double H_OM_PR { get; set; }
        public double H_GP { get; set; }
        public double H_GP_TH { get; set; }
        public double H_GP_PR { get; set; }
    }
    public class DataColl
    {
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public object BoardName { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public DateTime DOB_AD { get; set; }
        public string DOB_BS { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string F_ContactNo { get; set; }
        public string MotherName { get; set; }
        public string M_ContactNo { get; set; }
        public string HouseName { get; set; }
        public double CRTH { get; set; }
        public double CRPR { get; set; }
        public double CR { get; set; }
        public double FMTH { get; set; }
        public double FMPR { get; set; }
        public double FM { get; set; }
        public double PMTH { get; set; }
        public double PMPR { get; set; }
        public double PM { get; set; }
        public double OTH { get; set; }
        public double OPR { get; set; }
        public double ObtainMark { get; set; }
        public int TotalFail { get; set; }
        public int TotalFailTH { get; set; }
        public int TotalFailPR { get; set; }
        public double Per { get; set; }
        public double Per_TH { get; set; }
        public double Per_PR { get; set; }
        public double SubCount { get; set; }
        public double GSubCount { get; set; }
        public double GPA { get; set; }
        public double GP { get; set; }
        public double GP_TH { get; set; }
        public double GP_PR { get; set; }
        public string Division { get; set; }
        public string Grade { get; set; }
        public string GradeTH { get; set; }
        public string GradePR { get; set; }
        public string GP_Grade { get; set; }
        public double H_ObtainMark { get; set; }
        public double H_Per { get; set; }
        public double H_GPA { get; set; }
        public double H_GP { get; set; }
        public double HS_ObtainMark { get; set; }
        public double HS_Per { get; set; }
        public double HS_GPA { get; set; }
        public double HS_GP { get; set; }
        public bool IsFail { get; set; }
        public int RankInClass { get; set; }
        public int RankInSection { get; set; }
        public string SymbolNo { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public string Result { get; set; }
        public List<DetailsColl> DetailsColl { get; set; }
    }
}