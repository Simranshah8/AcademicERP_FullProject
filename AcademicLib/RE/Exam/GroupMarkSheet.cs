using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class GroupMarkSheet 
    {
        public GroupMarkSheet()
        {
            DetailsColl = new List<GroupMarkSheetDetails>();
        }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int RollNo { get; set; }
        public string RegdNo { get; set; }
        public string BoardRegdNo { get; set; }
        public string BoardName { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public DateTime? DOB_AD { get; set; }
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

        public string TeacherComment { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompPhoneNo { get; set; }
        public string CompFaxNo { get; set; }
        public string CompEmailId { get; set; }
        public string CompWebSite { get; set; }
        public string CompLogoPath { get; set; }
        public string CompImgPath { get; set; }
        public string CompBannerPath { get; set; }
        public string ExamName { get; set; }
        public DateTime IssueDateAD { get; set; }
        public string IssueDateBS { get; set; }
        public string CompRegdNo { get; set; }
        public string CompPanVat { get; set; }
        public int TotalStudentInClass { get; set; }
        public int TotalStudentInSection { get; set; }
        public DateTime ResultDateAD { get; set; }
        public string ResultDateBS { get; set; }
        public double Exam1 { get; set; }
        public double Exam2 { get; set; }
        public double Exam3 { get; set; }
        public double Exam4 { get; set; }
        public double Exam5 { get; set; }
        public double Exam6 { get; set; }
        public double Exam7 { get; set; }
        public double Exam8 { get; set; }
        public double Exam9 { get; set; }
        public double Exam10 { get; set; }
        public double Exam11 { get; set; }
        public double Exam12 { get; set; }
        public string E_Grade_1 { get; set; }
        public string E_Grade_2 { get; set; }
        public string E_Grade_3 { get; set; }
        public string E_Grade_4 { get; set; }
        public string E_Grade_5 { get; set; }
        public string E_Grade_6 { get; set; }
        public string E_Grade_7 { get; set; }
        public string E_Grade_8 { get; set; }
        public string E_Grade_9 { get; set; }
        public string E_Grade_10 { get; set; }
        public string E_Grade_11 { get; set; }
        public string E_Grade_12 { get; set; }
        public double E_AVGGP_1 { get; set; }
        public double E_AVGGP_2 { get; set; }
        public double E_AVGGP_3 { get; set; }
        public double E_AVGGP_4 { get; set; }
        public double E_AVGGP_5 { get; set; }
        public double E_AVGGP_6 { get; set; }
        public double E_AVGGP_7 { get; set; }
        public double E_AVGGP_8 { get; set; }
        public double E_AVGGP_9 { get; set; }
        public double E_AVGGP_10 { get; set; }
        public double E_AVGGP_11 { get; set; }
        public double E_AVGGP_12 { get; set; }
        public string Caste { get; set; }
        public string StudentType { get; set; }

        //public string E1 { get; set; }
        //public string E2 { get; set; }
        //public string E3 { get; set; }
        //public string E4 { get; set; }
        //public string E5 { get; set; }
        //public string E6 { get; set; }
        //public string E7 { get; set; }
        //public string E8 { get; set; }
        //public string E9 { get; set; }
        //public string E10 { get; set; }
        //public string E11 { get; set; }
        //public string E12 { get; set; }

        public List<GroupMarkSheetDetails> DetailsColl { get; set; }
    }

    public class GroupMarkSheetCollections : System.Collections.Generic.List<GroupMarkSheet>
    {
        public string ResponseMSG { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class GroupMarkSheetDetails
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
        public string StudentRemarks { get; set; }
        public string SubjectRemarks { get; set; }
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
        public bool IsECA { get; set; }
        public double Sub_Exam1 { get; set; }
        public double Sub_Exam2 { get; set; }
        public double Sub_Exam3 { get; set; }
        public double Sub_Exam4 { get; set; }
        public double Sub_Exam5 { get; set; }
        public double Sub_Exam6 { get; set; }
        public double Sub_Exam7 { get; set; }
        public double Sub_Exam8 { get; set; }
        public double Sub_Exam9 { get; set; }
        public double Sub_Exam10 { get; set; }
        public double Sub_Exam11 { get; set; }
        public double Sub_Exam12 { get; set; }
        public double Sub_E_TH_1 { get; set; }
        public double Sub_E_TH_2 { get; set; }
        public double Sub_E_TH_3 { get; set; }
        public double Sub_E_TH_4 { get; set; }
        public double Sub_E_TH_5 { get; set; }
        public double Sub_E_TH_6 { get; set; }
        public double Sub_E_TH_7 { get; set; }
        public double Sub_E_TH_8 { get; set; }
        public double Sub_E_TH_9 { get; set; }
        public double Sub_E_TH_10 { get; set; }
        public double Sub_E_TH_11 { get; set; }
        public double Sub_E_TH_12 { get; set; }
        public double Sub_E_PR_1 { get; set; }
        public double Sub_E_PR_2 { get; set; }
        public double Sub_E_PR_3 { get; set; }
        public double Sub_E_PR_4 { get; set; }
        public double Sub_E_PR_5 { get; set; }
        public double Sub_E_PR_6 { get; set; }
        public double Sub_E_PR_7 { get; set; }
        public double Sub_E_PR_8 { get; set; }
        public double Sub_E_PR_9 { get; set; }
        public double Sub_E_PR_10 { get; set; }
        public double Sub_E_PR_11 { get; set; }
        public double Sub_E_PR_12 { get; set; }
        public double Sub_E_GP_1 { get; set; }
        public double Sub_E_GP_2 { get; set; }
        public double Sub_E_GP_3 { get; set; }
        public double Sub_E_GP_4 { get; set; }
        public double Sub_E_GP_5 { get; set; }
        public double Sub_E_GP_6 { get; set; }
        public double Sub_E_GP_7 { get; set; }
        public double Sub_E_GP_8 { get; set; }
        public double Sub_E_GP_9 { get; set; }
        public double Sub_E_GP_10 { get; set; }
        public double Sub_E_GP_11 { get; set; }
        public double Sub_E_GP_12 { get; set; }
        public string Sub_E_Grade_1 { get; set; }
        public string Sub_E_Grade_2 { get; set; }
        public string Sub_E_Grade_3 { get; set; }
        public string Sub_E_Grade_4 { get; set; }
        public string Sub_E_Grade_5 { get; set; }
        public string Sub_E_Grade_6 { get; set; }
        public string Sub_E_Grade_7 { get; set; }
        public string Sub_E_Grade_8 { get; set; }
        public string Sub_E_Grade_9 { get; set; }
        public string Sub_E_Grade_10 { get; set; }
        public string Sub_E_Grade_11 { get; set; }
        public string Sub_E_Grade_12 { get; set; }

        public double Sub_Cur_FTH { get; set; }
        public double Sub_Cur_FPR { get; set; }
        public double Sub_Cur_PTH { get; set; }
        public double Sub_Cur_PPR { get; set; }
        public double Sub_Cur_OTH { get; set; }
        public double Sub_Cur_OPR { get; set; }
        public string Sub_Cur_OM { get; set; }
        public string Sub_Cur_OTH_Str { get; set; }
        public string Sub_Cur_OPR_Str { get; set; }
        public bool IsExtra { get; set; }

        public string Sub_Exam1_Str { get; set; }
        public string Sub_Exam2_Str { get; set; }
        public string Sub_Exam3_Str { get; set; }
        public string Sub_Exam4_Str { get; set; }
        public string Sub_Exam5_Str { get; set; }
        public string Sub_Exam6_Str { get; set; }
        public string Sub_Exam7_Str { get; set; }
        public string Sub_Exam8_Str { get; set; }
        public string Sub_Exam9_Str { get; set; }
        public string Sub_Exam10_Str { get; set; }
        public string Sub_Exam11_Str { get; set; }
        public string Sub_Exam12_Str { get; set; }


        public string Sub_E_TH_Grade_1 { get; set; }
        public string Sub_E_TH_Grade_2 { get; set; }
        public string Sub_E_TH_Grade_3 { get; set; }
        public string Sub_E_TH_Grade_4 { get; set; }
        public string Sub_E_TH_Grade_5 { get; set; }
        public string Sub_E_TH_Grade_6 { get; set; }
        public string Sub_E_TH_Grade_7 { get; set; }
        public string Sub_E_TH_Grade_8 { get; set; }
        public string Sub_E_TH_Grade_9 { get; set; }
        public string Sub_E_TH_Grade_10 { get; set; }
        public string Sub_E_TH_Grade_11 { get; set; }
        public string Sub_E_TH_Grade_12 { get; set; }

        public string Sub_E_PR_Grade_1 { get; set; }
        public string Sub_E_PR_Grade_2 { get; set; }
        public string Sub_E_PR_Grade_3 { get; set; }
        public string Sub_E_PR_Grade_4 { get; set; }
        public string Sub_E_PR_Grade_5 { get; set; }
        public string Sub_E_PR_Grade_6 { get; set; }
        public string Sub_E_PR_Grade_7 { get; set; }
        public string Sub_E_PR_Grade_8 { get; set; }
        public string Sub_E_PR_Grade_9 { get; set; }
        public string Sub_E_PR_Grade_10 { get; set; }
        public string Sub_E_PR_Grade_11 { get; set; }
        public string Sub_E_PR_Grade_12 { get; set; }
    }
}
