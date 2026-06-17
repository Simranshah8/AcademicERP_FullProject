using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.RE.Exam
{
    public class GroupTabulation : Tabulation
    {
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

    }
}
