using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicERP.Areas.Exam.Views.Report
{
    public partial class RldTabulation : System.Web.Mvc.ViewPage
    {
        public bool Loaded { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var urlHelper = new System.Web.Mvc.UrlHelper(Html.ViewContext.RequestContext);

            var baseurl = urlHelper.Content("~");

            if (!this.IsPostBack && !Loaded)
            {
                Loaded = true;
                Dynamic.BusinessEntity.Security.User user = (Dynamic.BusinessEntity.Security.User)User;
                ReportViewer1.KeepSessionAlive = false;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.Tabulation;
                int rptTranId= Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                //string path = baseurl + @"Report\Exam\Tabulation\RptTabulation.rdlc";
                string path = baseurl + template.Path;

                int? branchId = Convert.ToInt32(Request["BranchId"]);
                int? classId = Convert.ToInt32(Request["ClassId"]);
                int? sectionId = Convert.ToInt32(Request["SectionId"]);
                int examTypeId = Convert.ToInt32(Request["ExamTypeId"]);
                int? studentId = null;
                bool FilterSection = Convert.ToBoolean(Request["FilterSection"]);

                string ClassName = Convert.ToString(Request["ClassName"]);
                string ExamName = Convert.ToString(Request["ExamName"]);
                string classIdColl = Convert.ToString(Request["classIdColl"]);

                int? SemesterId = null, ClassYearId = null, BatchId = null;
                string Semester = "", ClassYear = "", Batch = "";
                if (Request["SemesterId"] != null && Request["SemesterId"]!="null" && Request["SemesterId"] != "undefined")
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
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName",comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassName", ClassName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamName", ExamName));

                if (!string.IsNullOrEmpty(Batch))
                {
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Batch", Batch));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Semester", Semester));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassYear", ClassYear));
                }

                ReportViewer1.LocalReport.EnableExternalImages = true;
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

                if((!classId.HasValue || classId.Value==0 ) && string.IsNullOrEmpty(classIdColl))
                {

                }else
                {
                    var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(user.UserId, user.HostName, user.DBName).getMarkSheetClassWise(AcademicYearId, studentId, classId, sectionId, examTypeId, FilterSection, classIdColl, BatchId, SemesterId, ClassYearId, FromPublished,branchId);


                    List<AcademicLib.RE.Exam.Tabulation> dataSource = new List<AcademicLib.RE.Exam.Tabulation>();
                    int rowSNO = 0;
                    foreach (var dc in dataColl)
                    {
                        rowSNO++;
                        foreach (var sd in dc.DetailsColl)
                        {
                            AcademicLib.RE.Exam.Tabulation tbl = new AcademicLib.RE.Exam.Tabulation();
                            tbl.RowSNo = rowSNO;
                            tbl.TotalFail = dc.TotalFail;
                            tbl.TotalFailTH = dc.TotalFailTH;
                            tbl.TotalFailPR = dc.TotalFailPR;
                            tbl.WorkingDays = dc.WorkingDays;
                            tbl.PresentDays = dc.PresentDays;
                            tbl.AbsentDays = dc.AbsentDays;
                            tbl.SymbolNo = dc.SymbolNo;

                            tbl.Caste = dc.Caste;
                            tbl.StudentType = dc.StudentType;
                            tbl.Address = dc.Address;
                            tbl.TeacherComment = dc.Comment;

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

                            tbl.CAS1 = sd.CAS1;
                            tbl.CAS2 = sd.CAS2;
                            tbl.CAS3 = sd.CAS3;
                            tbl.CAS4 = sd.CAS4;
                            tbl.CAS5 = sd.CAS5;
                            tbl.CAS6 = sd.CAS6;
                            tbl.CAS7 = sd.CAS7;
                            tbl.CAS8 = sd.CAS8;
                            tbl.CAS9 = sd.CAS9;
                            tbl.CAS10 = sd.CAS10;
                            tbl.CAS11 = sd.CAS11;
                            tbl.CAS12 = sd.CAS12;

                            if (tbl.PaperType == 1)
                                tbl.PaperTypeName = "TH";
                            else if (tbl.PaperType == 2)
                                tbl.PaperTypeName = "PR";

                            tbl.SNo = sd.SNo;
                            dataSource.Add(tbl);
                        }


                    }
                    ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataSource));
                    ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetSummary", dataColl));

                    ReportViewer1.LocalReport.SetParameters(parameterColl);

                }

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}