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

            var baseurl = urlHelper.Content("~").Trim().ToLower().Replace("teacher", ""); 
            var BaseUrl = System.Configuration.ConfigurationManager.AppSettings["baseURL"].ToString() + "/v1/";

            if (!this.IsPostBack && !Loaded)
            {
                Loaded = true;
                TeacherLogin.Models.Teacher.TeacherLogin user = (TeacherLogin.Models.Teacher.TeacherLogin)User;
                ReportViewer1.KeepSessionAlive = false;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                var reportService = new TeacherLogin.Models.ReportApiService(user);


                int entityId = 359;
                int voucherId = 0;
                bool isTran = false;
                int rptTranId= Convert.ToInt32(Request["rptTranId"]);
                //TODO: Api Call
                var template = reportService.GetReportTemplate(entityId, voucherId, isTran, rptTranId);

                //string path = baseurl + @"Report\Exam\Tabulation\RptTabulation.rdlc";
                string path = baseurl + template.Path;
                //string path = @"~/Report/Exam/Tabulation/RptTabulation.rdlc";

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
                if (Request["SemesterId"] != null && Request["SemesterId"]!="null" && Request["SemesterId"] != "undefined")
                    SemesterId = Convert.ToInt32(Request["SemesterId"]);

                if (Request["ClassYearId"] != null && Request["ClassYearId"] != "null" && Request["ClassYearId"] != "undefined")
                    ClassYearId = Convert.ToInt32(Request["ClassYearId"]);

                if (Request["BatchId"] != null && Request["BatchId"] != "null" && Request["BatchId"] != "undefined")
                    BatchId = Convert.ToInt32(Request["BatchId"]);

                bool FromPublished = false;
                try
                {
                    if (Request["FromPublished"] != null && Request["FromPublished"] != "null" && Request["FromPublished"] != "undefined")
                        FromPublished = Convert.ToBoolean(Request["FromPublished"]);
                }
                catch { }
                //TODO: Api Call
                var comDet = reportService.GetCompanyDetails();

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName",comDet.Name));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.Address));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassName", ClassName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamName", ExamName));
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                int academicYearId = 0;
                try
                {
                    var sessionId = Session["AcademicYearId" + user.userId.ToString()];
                    if (sessionId != null)
                        academicYearId = (int)sessionId;

                    if (academicYearId == 0)
                    {
                        //TODO: Api Call
                        var AcademicYearList = reportService.GetAcademicYearList();
                        
                        var runningYear = AcademicYearList.FirstOrDefault(x => x.IsRunning == true);
                        if (runningYear != null)
                        {
                            academicYearId = runningYear.AcademicYearId.Value;
                        }
                    }
                }
                catch { }

                if ((!classId.HasValue || classId.Value==0 ) && string.IsNullOrEmpty(classIdColl))
                {

                }
                else
                {

                    try
                    {
                        if (Request["FromPublished"] != null && Request["FromPublished"] != "null" && Request["FromPublished"] != "undefined")
                            FromPublished = Convert.ToBoolean(Request["FromPublished"]);
                    }
                    catch { }
                    //TODO: Api Call
                    List<TeacherLogin.Models.Tabulation> dataSource = new List<TeacherLogin.Models.Tabulation>();
                    try
                    {
                        var dataColl = new TeacherLogin.Models.APIRequest(BaseUrl, "Teacher/GetTabulation", "POST");
                        Dictionary<string, string> keyValues = new Dictionary<string, string>();
                        keyValues.Add("Bearer", user.access_token);
                        var para = new
                        {
                            AcademicYearId= academicYearId,
                            BranchId = branchId,
                            ClassId = classId,
                            SectionId = sectionId,
                            ExamTypeId = examTypeId,
                            FilterSection = FilterSection,
                            FromPublished = FromPublished
                        };
                        var resTabuData = dataColl.Execute<List<TeacherLogin.Models.Tabulation>>(para, keyValues);

                        if (resTabuData != null)
                        {
                            dataSource = ((List<TeacherLogin.Models.Tabulation>)resTabuData);
                        }
                    }
                    catch (Exception ee)
                    {

                    }

                    ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataSource));
                    ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetSummary", dataSource));

                    ReportViewer1.LocalReport.SetParameters(parameterColl);

                }

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}