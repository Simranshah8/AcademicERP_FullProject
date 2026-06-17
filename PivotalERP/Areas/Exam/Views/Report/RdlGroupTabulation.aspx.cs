using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicERP.Areas.Exam.Views.Report
{
    public partial class RdlGroupTabulation : System.Web.Mvc.ViewPage
    {
        public bool Loaded { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var urlHelper = new System.Web.Mvc.UrlHelper(Html.ViewContext.RequestContext);

            var baseurl = urlHelper.Content("~");

            if (!Page.IsPostBack)
            {               
                Loaded = true;
                Dynamic.BusinessEntity.Security.User user = (Dynamic.BusinessEntity.Security.User)User;
                ReportViewer1.KeepSessionAlive = false;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.GroupTabulation;
                int rptTranId = Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                //string path = baseurl + @"Report\Exam\Tabulation\RptTabulation.rdlc";
                string path = baseurl + template.Path;

                int? branchId = Convert.ToInt32(Request["BranchId"]);

                int? curExamTypeId = Convert.ToInt32(Request["curExamTypeId"]);
                int? classId = Convert.ToInt32(Request["ClassId"]);
                int? sectionId = Convert.ToInt32(Request["SectionId"]);
                int examTypeId = Convert.ToInt32(Request["ExamTypeId"]);
                int? studentId = null;
                bool FilterSection = Convert.ToBoolean(Request["FilterSection"]);

                string ClassName = Convert.ToString(Request["ClassName"]);
                string ExamName = Convert.ToString(Request["ExamName"]);


                int? SemesterId = null, ClassYearId = null, BatchId = null;
                string Semester = "", ClassYear = "", Batch = "";
                if (Request["SemesterId"] != null && Request["SemesterId"] != "null" && Request["SemesterId"] != "undefined")
                {
                    SemesterId = Convert.ToInt32(Request["SemesterId"]);
                    Semester = Convert.ToString(Request["Semester"]);
                }


                if (Request["ClassYearId"] != null && Request["ClassYearId"] != "null" && Request["ClassYearId"] != "undefined")
                {
                    ClassYearId = Convert.ToInt32(Request["ClassYearId"]);
                    ClassYear = Convert.ToString(Request["ClassYear"]);
                }


                if (Request["BatchId"] != null && Request["BatchId"] != "null" && Request["BatchId"] != "undefined")
                {
                    BatchId = Convert.ToInt32(Request["BatchId"]);
                    Batch = Convert.ToString(Request["Batch"]);
                }



                bool FromPublished = false;
                try
                {
                    if (Request["FromPublished"] != null && Request["FromPublished"] != "null" && Request["FromPublished"] != "undefined")
                        FromPublished = Convert.ToBoolean(Request["FromPublished"]);
                }
                catch { }

                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassName", ClassName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamName", ExamName));
                if (!string.IsNullOrEmpty(Batch))
                {
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Batch", Batch));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Semester", Semester));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassYear", ClassYear));
                }
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                int AcademicYearId = 0;
                try
                {
                    var sessionId = Session["AcademicYearId" + user.UserId.ToString()];
                    if (sessionId != null)
                        AcademicYearId = (int)sessionId;

                    if (AcademicYearId == 0)
                        AcademicYearId = new AcademicLib.BL.Academic.Creation.AcademicYear(user.UserId, user.HostName, user.DBName).getDefaultAcademicYearId().RId;
                }
                catch { }

                var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(user.UserId, user.HostName, user.DBName).getGroupMarkSheetClassWise(AcademicYearId, studentId, classId, sectionId, examTypeId, FilterSection,curExamTypeId,BatchId,SemesterId,ClassYearId,FromPublished,branchId);


                List<AcademicLib.RE.Exam.GroupTabulation> dataSource = new List<AcademicLib.RE.Exam.GroupTabulation>();
                int rowSNO = 0;
                foreach (var dc in dataColl)
                {
                    rowSNO++;
                    foreach (var sd in dc.DetailsColl)
                    {
                        AcademicLib.RE.Exam.GroupTabulation tbl = new AcademicLib.RE.Exam.GroupTabulation();
                        tbl.RowSNo = rowSNO;
                        tbl.WorkingDays = dc.WorkingDays;
                        tbl.PresentDays = dc.PresentDays;
                        tbl.AbsentDays = dc.AbsentDays;
                        tbl.TotalFail = dc.TotalFail;
                        tbl.TotalFailTH = dc.TotalFailTH;
                        tbl.TotalFailPR = dc.TotalFailPR;
                        tbl.SymbolNo = dc.SymbolNo;

                        tbl.Caste = dc.Caste;
                        tbl.StudentType = dc.StudentType;
                        tbl.Address = dc.Address;
                        tbl.TeacherComment = dc.TeacherComment;
                        tbl.RollNo = dc.RollNo;
                        tbl.Division = dc.Division;
                        tbl.StudentId = dc.StudentId;
                        tbl.ClassName = dc.ClassName;
                        tbl.SectionName = dc.SectionName;
                        tbl.RegdNo = dc.RegdNo;
                        tbl.BoardName = dc.BoardName;
                        tbl.BoardRegdNo = dc.BoardRegdNo;
                        tbl.Name = dc.Name;
                        tbl.FatherName = dc.FatherName;
                        tbl.MotherName = dc.MotherName;
                        tbl.DOB_AD = dc.DOB_AD;
                        tbl.DOB_BS = dc.DOB_BS;
                        tbl.F_ContactNo = dc.F_ContactNo;
                        tbl.M_ContactNo = dc.M_ContactNo;
                        tbl.Gender = dc.Gender;
                        tbl.HouseName = dc.HouseName;
                        tbl.ObtainMark = dc.ObtainMark;
                        tbl.Per = dc.Per;
                        tbl.RankInClass = dc.RankInClass;
                        tbl.RankInSection = dc.RankInSection;
                        tbl.Result = dc.Result;
                        tbl.GP = dc.GP;
                        tbl.Grade = dc.Grade;
                        tbl.GradeTH = dc.GradeTH;
                        tbl.GradePR = dc.GradePR;
                        tbl.GPA = dc.GPA;
                        tbl.GP_Grade = dc.GP_Grade;
                        tbl.TotalFail = dc.TotalFail;
                        tbl.TotalFailTH = dc.TotalFailTH;
                        tbl.TotalFailPR = dc.TotalFailPR;
                        tbl.FM = dc.FM;
                        tbl.FMTH = dc.FMTH;
                        tbl.FMPR = dc.FMPR;
                        tbl.PM = dc.PM;
                        tbl.PMTH = dc.PMTH;
                        tbl.PMPR = dc.PMPR;

                        tbl.TeacherComment = dc.TeacherComment;
                        tbl.CompanyName = dc.CompanyName;
                        tbl.CompanyAddress = dc.CompanyAddress;
                        tbl.CompPhoneNo = dc.CompPhoneNo;
                        tbl.CompFaxNo = dc.CompFaxNo;
                        tbl.CompEmailId = dc.CompEmailId;
                        tbl.CompWebSite = dc.CompWebSite;
                        tbl.CompLogoPath = dc.CompLogoPath;
                        tbl.CompImgPath = dc.CompImgPath;
                        tbl.CompBannerPath = dc.CompBannerPath;
                        tbl.ExamName = dc.ExamName;
                        tbl.IssueDateAD = dc.IssueDateAD;
                        tbl.IssueDateBS = dc.IssueDateBS;
                        tbl.CompRegdNo = dc.CompRegdNo;
                        tbl.CompPanVat = dc.CompPanVat;
                        tbl.TotalStudentInClass = dc.TotalStudentInClass;
                        tbl.TotalStudentInSection = dc.TotalStudentInSection;
                        tbl.ResultDateAD = dc.ResultDateAD;
                        tbl.ResultDateBS = dc.ResultDateBS;
                        tbl.Exam1 = dc.Exam1;
                        tbl.Exam2 = dc.Exam2;
                        tbl.Exam3 = dc.Exam3;
                        tbl.Exam4 = dc.Exam4;
                        tbl.Exam5 = dc.Exam5;
                        tbl.Exam6 = dc.Exam6;
                        tbl.Exam7 = dc.Exam7;
                        tbl.Exam8 = dc.Exam8;
                        tbl.Exam9 = dc.Exam9;
                        tbl.Exam10 = dc.Exam10;
                        tbl.Exam11 = dc.Exam11;
                        tbl.Exam12 = dc.Exam12;
                        tbl.E_Grade_1 = dc.E_Grade_1;
                        tbl.E_Grade_2 = dc.E_Grade_2;
                        tbl.E_Grade_3 = dc.E_Grade_3;
                        tbl.E_Grade_4 = dc.E_Grade_4;
                        tbl.E_Grade_5 = dc.E_Grade_5;
                        tbl.E_Grade_6 = dc.E_Grade_6;
                        tbl.E_Grade_7 = dc.E_Grade_7;
                        tbl.E_Grade_8 = dc.E_Grade_8;
                        tbl.E_Grade_9 = dc.E_Grade_9;
                        tbl.E_Grade_10 = dc.E_Grade_10;
                        tbl.E_Grade_11 = dc.E_Grade_11;
                        tbl.E_Grade_12 = dc.E_Grade_12;
                        tbl.E_AVGGP_1 = dc.E_AVGGP_1;
                        tbl.E_AVGGP_2 = dc.E_AVGGP_2;
                        tbl.E_AVGGP_3 = dc.E_AVGGP_3;
                        tbl.E_AVGGP_4 = dc.E_AVGGP_4;
                        tbl.E_AVGGP_5 = dc.E_AVGGP_5;
                        tbl.E_AVGGP_6 = dc.E_AVGGP_6;
                        tbl.E_AVGGP_7 = dc.E_AVGGP_7;
                        tbl.E_AVGGP_8 = dc.E_AVGGP_8;
                        tbl.E_AVGGP_9 = dc.E_AVGGP_9;
                        tbl.E_AVGGP_10 = dc.E_AVGGP_10;
                        tbl.E_AVGGP_11 = dc.E_AVGGP_11;
                        tbl.E_AVGGP_12 = dc.E_AVGGP_12;

                        tbl.IsOptional = sd.IsOptional;
                        tbl.SubjectId = sd.SubjectId;
                        tbl.SubjectName = sd.SubjectName;
                        tbl.CodeTH = sd.CodeTH;
                        tbl.CodePR = sd.CodePR;
                        tbl.Sub_OM = sd.ObtainMark;
                        tbl.Sub_OMPR = sd.OPR;
                        tbl.Sub_OMTH = sd.OTH;
                        tbl.Sub_OM_Str = sd.ObtainMark_Str;
                        tbl.Sub_OMTH_Str = sd.ObtainMarkTH_Str;
                        tbl.Sub_OMPR_Str = sd.ObtainMarkPR_Str;
                        tbl.IsFailTH = sd.IsFailTH;
                        tbl.IsFailPR = sd.IsFailPR;
                        tbl.PaperType = sd.PaperType;
                        tbl.Sub_FM = sd.FM;
                        tbl.Sub_FMTH = sd.FMTH;
                        tbl.Sub_FMPR = sd.FMPR;
                        tbl.Sub_PM = sd.PM;
                        tbl.Sub_PMTH = sd.PMTH;
                        tbl.Sub_PMPR = sd.PMPR;

                        tbl.Sub_GP = sd.GP;
                        tbl.Sub_GP_TH = sd.GP_TH;
                        tbl.Sub_GP_PR = sd.GP_PR;
                        tbl.Sub_Grade = sd.Grade;
                        tbl.Sub_Grade_TH = sd.GradeTH;
                        tbl.Sub_Grade_PR = sd.GradePR;
                        tbl.Sub_GP_Grade = sd.GP_Grade;
                        tbl.Sub_GP_Grade_TH = sd.GP_GradeTH;
                        tbl.Sub_GP_Grade_PR = sd.GP_GradePR;

                        tbl.IsECA = sd.IsECA;
                        tbl.Sub_Exam1 = sd.Sub_Exam1;
                        tbl.Sub_Exam2 = sd.Sub_Exam2;
                        tbl.Sub_Exam3 = sd.Sub_Exam3;
                        tbl.Sub_Exam4 = sd.Sub_Exam4;
                        tbl.Sub_Exam5 = sd.Sub_Exam5;
                        tbl.Sub_Exam6 = sd.Sub_Exam6;
                        tbl.Sub_Exam7 = sd.Sub_Exam7;
                        tbl.Sub_Exam8 = sd.Sub_Exam8;
                        tbl.Sub_Exam9 = sd.Sub_Exam9;
                        tbl.Sub_Exam10 = sd.Sub_Exam10;
                        tbl.Sub_Exam11 = sd.Sub_Exam11;
                        tbl.Sub_Exam12 = sd.Sub_Exam12;

                        tbl.Sub_Exam1_Str = sd.Sub_Exam1_Str;
                        tbl.Sub_Exam2_Str = sd.Sub_Exam2_Str;
                        tbl.Sub_Exam3_Str = sd.Sub_Exam3_Str;
                        tbl.Sub_Exam4_Str = sd.Sub_Exam4_Str;
                        tbl.Sub_Exam5_Str = sd.Sub_Exam5_Str;
                        tbl.Sub_Exam6_Str = sd.Sub_Exam6_Str;
                        tbl.Sub_Exam7_Str = sd.Sub_Exam7_Str;
                        tbl.Sub_Exam8_Str = sd.Sub_Exam8_Str;
                        tbl.Sub_Exam9_Str = sd.Sub_Exam9_Str;
                        tbl.Sub_Exam10_Str = sd.Sub_Exam10_Str;
                        tbl.Sub_Exam11_Str = sd.Sub_Exam11_Str;
                        tbl.Sub_Exam12_Str = sd.Sub_Exam12_Str;

                        tbl.Sub_E_TH_1 = sd.Sub_E_TH_1;
                        tbl.Sub_E_TH_2 = sd.Sub_E_TH_2;
                        tbl.Sub_E_TH_3 = sd.Sub_E_TH_3;
                        tbl.Sub_E_TH_4 = sd.Sub_E_TH_4;
                        tbl.Sub_E_TH_5 = sd.Sub_E_TH_5;
                        tbl.Sub_E_TH_6 = sd.Sub_E_TH_6;
                        tbl.Sub_E_TH_7 = sd.Sub_E_TH_7;
                        tbl.Sub_E_TH_8 = sd.Sub_E_TH_8;
                        tbl.Sub_E_TH_9 = sd.Sub_E_TH_9;
                        tbl.Sub_E_TH_10 = sd.Sub_E_TH_10;
                        tbl.Sub_E_TH_11 = sd.Sub_E_TH_11;
                        tbl.Sub_E_TH_12 = sd.Sub_E_TH_12;
                        tbl.Sub_E_PR_1 = sd.Sub_E_PR_1;
                        tbl.Sub_E_PR_2 = sd.Sub_E_PR_2;
                        tbl.Sub_E_PR_3 = sd.Sub_E_PR_3;
                        tbl.Sub_E_PR_4 = sd.Sub_E_PR_4;
                        tbl.Sub_E_PR_5 = sd.Sub_E_PR_5;
                        tbl.Sub_E_PR_6 = sd.Sub_E_PR_6;
                        tbl.Sub_E_PR_7 = sd.Sub_E_PR_7;
                        tbl.Sub_E_PR_8 = sd.Sub_E_PR_8;
                        tbl.Sub_E_PR_9 = sd.Sub_E_PR_9;
                        tbl.Sub_E_PR_10 = sd.Sub_E_PR_10;
                        tbl.Sub_E_PR_11 = sd.Sub_E_PR_11;
                        tbl.Sub_E_PR_12 = sd.Sub_E_PR_12;
                        tbl.Sub_E_GP_1 = sd.Sub_E_GP_1;
                        tbl.Sub_E_GP_2 = sd.Sub_E_GP_2;
                        tbl.Sub_E_GP_3 = sd.Sub_E_GP_3;
                        tbl.Sub_E_GP_4 = sd.Sub_E_GP_4;
                        tbl.Sub_E_GP_5 = sd.Sub_E_GP_5;
                        tbl.Sub_E_GP_6 = sd.Sub_E_GP_6;
                        tbl.Sub_E_GP_7 = sd.Sub_E_GP_7;
                        tbl.Sub_E_GP_8 = sd.Sub_E_GP_8;
                        tbl.Sub_E_GP_9 = sd.Sub_E_GP_9;
                        tbl.Sub_E_GP_10 = sd.Sub_E_GP_10;
                        tbl.Sub_E_GP_11 = sd.Sub_E_GP_11;
                        tbl.Sub_E_GP_12 = sd.Sub_E_GP_12;
                        tbl.Sub_E_Grade_1 = sd.Sub_E_Grade_1;
                        tbl.Sub_E_Grade_2 = sd.Sub_E_Grade_2;
                        tbl.Sub_E_Grade_3 = sd.Sub_E_Grade_3;
                        tbl.Sub_E_Grade_4 = sd.Sub_E_Grade_4;
                        tbl.Sub_E_Grade_5 = sd.Sub_E_Grade_5;
                        tbl.Sub_E_Grade_6 = sd.Sub_E_Grade_6;
                        tbl.Sub_E_Grade_7 = sd.Sub_E_Grade_7;
                        tbl.Sub_E_Grade_8 = sd.Sub_E_Grade_8;
                        tbl.Sub_E_Grade_9 = sd.Sub_E_Grade_9;
                        tbl.Sub_E_Grade_10 = sd.Sub_E_Grade_10;
                        tbl.Sub_E_Grade_11 = sd.Sub_E_Grade_11;
                        tbl.Sub_E_Grade_12 = sd.Sub_E_Grade_12;
                        tbl.Sub_Cur_FTH = sd.Sub_Cur_FTH;
                        tbl.Sub_Cur_FPR = sd.Sub_Cur_FPR;
                        tbl.Sub_Cur_PTH = sd.Sub_Cur_PTH;
                        tbl.Sub_Cur_PPR = sd.Sub_Cur_PPR;
                        tbl.Sub_Cur_OTH = sd.Sub_Cur_OTH;
                        tbl.Sub_Cur_OPR = sd.Sub_Cur_OPR;
                        tbl.Sub_Cur_OM = sd.Sub_Cur_OM;
                        tbl.Sub_Cur_OTH_Str = sd.Sub_Cur_OTH_Str;
                        tbl.Sub_Cur_OPR_Str = sd.Sub_Cur_OPR_Str;

                        if (tbl.PaperType == 1)
                            tbl.PaperTypeName = "TH";
                        else if (tbl.PaperType == 2)
                            tbl.PaperTypeName = "PR";

                        tbl.SNo = sd.SNo;
                        dataSource.Add(tbl);
                    }
                }

                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataSource));
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetSummary", dataColl));

                ReportViewer1.LocalReport.SetParameters(parameterColl);

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}