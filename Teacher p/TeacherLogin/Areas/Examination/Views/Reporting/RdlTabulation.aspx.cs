using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeacherLogin.Areas.Examination.Views.Reporting
{
    
    public partial class RdlTabulation : System.Web.Mvc.ViewPage
    {
        TeacherLogin.Models.Teacher.TeacherLogin user = null;
        private TeacherLogin.Models.AboutCompany GetCompanyDet()
        {
            TeacherLogin.Models.AboutCompany com = new Models.AboutCompany();

            try
            {
                string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["baseURL"].ToString() + "/v1/";
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetAboutCompany", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", user.access_token);
                var responseData = request.Execute<TeacherLogin.Models.AboutCompany>(com,keyValues);
                if (responseData != null)
                {
                    com = ((TeacherLogin.Models.AboutCompany)responseData);
                }
                return com;
            }
            catch (Exception ee)
            {
                return com;
            }
        }

        private List<TeacherLogin.Models.Teacher.Tabulation> GetData(int? ClassId,int? SectionId,int ExamTypeId,bool IsGroup)
        {
            List<TeacherLogin.Models.Teacher.Tabulation> dataColl = new List<Models.Teacher.Tabulation>();
            try
            {
                string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["baseURL"].ToString() + "/v1/";

                string api=(IsGroup ? "Teacher/GetObtainMarkGroup" : @"Teacher/GetObtainMark") ;
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl,  api, "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", user.access_token);
                var para = new
                {
                    classId = ClassId,
                    sectionId = SectionId,
                    examTypeId = ExamTypeId
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.MarkSheetResponse>(para, keyValues);


                if (responseData != null)
                {
                    var resDataColl = ((TeacherLogin.Models.Teacher.MarkSheetResponse)responseData).DataColl;

                    foreach (var dc in resDataColl)
                    {
                        foreach (var sd in dc.DetailsColl)
                        {
                            TeacherLogin.Models.Teacher.Tabulation tbl = new Models.Teacher.Tabulation();
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
                            tbl.WorkingDays = dc.WorkingDays;
                            tbl.PresentDays = dc.PresentDays;
                            tbl.AbsentDays = dc.AbsentDays;
                            tbl.TotalFail = dc.TotalFail;
                            tbl.TotalFailTH = dc.TotalFailTH;
                            tbl.TotalFailPR = dc.TotalFailPR;

                            tbl.SubjectId = sd.SubjectId;
                            tbl.SubjectName = sd.SubjectName;
                            tbl.Sub_OM = sd.ObtainMark;
                            tbl.Sub_OMPR = sd.OPR;
                            tbl.Sub_OMTH = sd.OTH;
                            tbl.Sub_OM_Str = sd.ObtainMark_Str;
                            tbl.Sub_OMTH_Str = sd.ObtainMarkTH_Str;
                            tbl.Sub_OMPR_Str = sd.ObtainMarkPR_Str;
                            tbl.PaperType = sd.PaperType;
                            tbl.CodeTH = sd.CodeTH;
                            tbl.CodePR = sd.CodePR;


                            if (tbl.PaperType == 1)
                                tbl.PaperTypeName = "TH";
                            else if (tbl.PaperType == 2)
                                tbl.PaperTypeName = "PR";

                            tbl.SNo = sd.SNo;
                            dataColl.Add(tbl);
                        }


                    }
                }
            }
            catch (Exception ee)
            {
                return dataColl;
            }

            return dataColl;
        }
        public bool Loaded { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
             user= (TeacherLogin.Models.Teacher.TeacherLogin)User;
            var urlHelper = new System.Web.Mvc.UrlHelper(Html.ViewContext.RequestContext);

            var baseurl = urlHelper.Content("~").Trim().ToLower().Replace("teacher","");

            if (!this.IsPostBack && !Loaded)
            {
                Loaded = true;
                
                ReportViewer1.KeepSessionAlive = false;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                string path = baseurl + @"Report\Exam\Tabulation\RptTabulation.rdlc";// Request["rptPath"];
                int? classId = Convert.ToInt32(Request["ClassId"]);
                int? sectionId = Convert.ToInt32(Request["SectionId"]);
                int examTypeId = Convert.ToInt32(Request["ExamTypeId"]);
                int? studentId = null;
                bool FilterSection = Convert.ToBoolean(Request["FilterSection"]);

                string ClassName = Convert.ToString(Request["ClassName"]);
                string ExamName = Convert.ToString(Request["ExamName"]);

                bool IsGroup = Convert.ToBoolean(Request["IsGroup"]);

                int entityId = 0;// (int)Dynamic.BusinessEntity.Global.RptFormsEntity.Tabulation;
                var comDet = GetCompanyDet();

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.Name));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.Address));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassName", ClassName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamName", ExamName));

                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                var dataSouce = GetData(classId, sectionId, examTypeId, IsGroup);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataSouce));
                //ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetSummary", summaryColl));

                ReportViewer1.LocalReport.SetParameters(parameterColl);

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}