using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.PE.Exam
{
    public class ExamAnalysis : Dynamic.Accounting.IReportLoadObjectData
    {

        public ExamAnalysis(List<AcademicLib.RE.Exam.MarkSheet> dataColl)
        {
            this.dataColl = dataColl;
        }

        private List<AcademicLib.RE.Exam.MarkSheet> dataColl = null;
        public System.Collections.IEnumerable DataColl
        {
            get
            {
                return dataColl;
            }
        }

        public string ReportPath
        {
            get
            {
                return "";
            }
        }
        public void GetDataOfCurrentRow(object obj, Dynamic.ReportEngine.RDL.Row row)
        {
            //StudentId, ClassName, SectionName, RollNo, RegdNo, BoardRegdNo, BoardName, Name, PhotoPath, FatherName,
            ////Address, DOB_AD, DOB_BS, Gender, ContactNo, F_ContactNo, MotherName, M_ContactNo, HouseName, CRTH, CRPR,
            ///CR, FMTH, FMPR, FM, PMTH, PMPR, PM, OTH, OPR, ObtainMark, TotalFail, TotalFailTH, TotalFailPR, Per, Per_TH, 
            ///Per_PR, SubCount, GSubCount, GPA, GP, GP_TH, GP_PR, Division, Grade, GradeTH, GradePR, GP_Grade, H_ObtainMark,
            ///H_Per, H_GPA, H_GP, HS_ObtainMark, HS_Per, HS_GPA, HS_GP, IsFail, RankInClass, RankInSection, SymbolNo, Weight,
            ///Height, WorkingDays, PresentDays, AbsentDays, Result, NoOfStudent, DetailsColl, SNo, Comment, RankInSchool, Caste 


            AcademicLib.RE.Exam.MarkSheet csl = (AcademicLib.RE.Exam.MarkSheet)obj;
            row.Data[0] = csl.StudentId;
            row.Data[1] = csl.ClassName;
            row.Data[2] = csl.SectionName;
            row.Data[3] = csl.RollNo;
            row.Data[4] = csl.RegdNo;
            row.Data[5] = csl.BoardRegdNo;
            row.Data[6] = csl.BoardName;
            row.Data[7] = csl.Name;
            row.Data[8] = csl.PhotoPath;
            row.Data[9] = csl.FatherName;
            row.Data[10] = csl.Address;
            row.Data[11] = csl.DOB_AD;
            row.Data[12] = csl.DOB_BS;
            row.Data[13] = csl.Gender;
            row.Data[14] = csl.ContactNo;
            row.Data[15] = csl.F_ContactNo;
            row.Data[16] = csl.MotherName;
            row.Data[17] = csl.M_ContactNo;
            row.Data[18] = csl.HouseName;
            row.Data[19] = csl.CRTH;
            row.Data[20] = csl.CRPR;            
            row.Data[21] = csl.CR;
            row.Data[22] = csl.FMTH;
            row.Data[23] = csl.FMPR;
            row.Data[24] = csl.FM;
            row.Data[25] = csl.PMTH;
            row.Data[26] = csl.PMPR;
            row.Data[27] = csl.PM;
            row.Data[28] = csl.OTH;
            row.Data[29] = csl.OPR;
            row.Data[30] = csl.ObtainMark;
            row.Data[31] = csl.TotalFail;
            row.Data[32] = csl.TotalFailTH;
            row.Data[33] = csl.TotalFailPR;
            row.Data[34] = csl.Per;
            row.Data[35] = csl.Per_TH;
            row.Data[36] = csl.Per_PR;
            row.Data[37] = csl.SubCount;
            row.Data[38] = csl.GSubCount;
            row.Data[39] = csl.GPA;
            row.Data[40] = csl.GP;
            row.Data[41] = csl.GP_TH;
            row.Data[42] = csl.GP_PR;            
            row.Data[43] = csl.Division;
            row.Data[44] = csl.Grade;
            row.Data[45] = csl.GradeTH;
            row.Data[46] = csl.GradePR;
            row.Data[47] = csl.GP_Grade;
            row.Data[48] = csl.H_ObtainMark;
            row.Data[49] = csl.H_Per;
            row.Data[50] = csl.H_GPA;
            row.Data[51] = csl.H_GP;
            row.Data[52] = csl.HS_ObtainMark;
            row.Data[53] = csl.HS_Per;
            row.Data[54] = csl.HS_GPA;
            row.Data[55] = csl.HS_GP;
            row.Data[56] = csl.IsFail;
            row.Data[57] = csl.RankInClass;
            row.Data[58] = csl.RankInSection;
            row.Data[59] = csl.SymbolNo;
            row.Data[60] = csl.Weight;
            row.Data[61] = csl.Height;
            row.Data[62] = csl.WorkingDays;
            row.Data[63] = csl.PresentDays;
            row.Data[64] = csl.AbsentDays;
            row.Data[65] = csl.Result;
            row.Data[66] = csl.NoOfStudent;
            row.Data[67] = csl.DetailsColl;            
            row.Data[68] = csl.SNo;
            row.Data[69] = csl.Comment;
            row.Data[70] = csl.RankInSchool;
            row.Data[71] = csl.Caste;


        }
        private System.Collections.Specialized.ListDictionary parametsColl = new System.Collections.Specialized.ListDictionary();
        public System.Collections.Specialized.ListDictionary getParametersColl()
        {
            return parametsColl;
        }
    }
}
