using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicERP.Areas.Exam.Views.Report
{
    public partial class RdlCASTabulation : System.Web.Mvc.ViewPage
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

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.CASTabulation;
                int rptTranId = Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                //string path = baseurl + @"Report\Exam\Tabulation\RptTabulation.rdlc";
                string path = baseurl + template.Path;
                int classId = Convert.ToInt32(Request["ClassId"]);
                int? sectionId = Convert.ToInt32(Request["SectionId"]);
                int? examTypeId = Convert.ToInt32(Request["ExamTypeId"]);
                int? casTypeId = Convert.ToInt32(Request["CASTypeId"]);
                int? subjectId = Convert.ToInt32(Request["SubjectId"]);

                int? SemesterId = null, ClassYearId = null, BatchId = null;
                if (Request["SemesterId"] != null && Request["SemesterId"] != "null")
                    SemesterId = Convert.ToInt32(Request["SemesterId"]);

                if (Request["ClassYearId"] != null && Request["ClassYearId"] != "null")
                    ClassYearId = Convert.ToInt32(Request["ClassYearId"]);

                if (Request["BatchId"] != null && Request["BatchId"] != "null")
                    BatchId = Convert.ToInt32(Request["BatchId"]);


                bool FilterSection = Convert.ToBoolean(Request["FilterSection"]);

                bool FromPublished = false;
                try
                {
                    if (Request["FromPublished"] != null && Request["FromPublished"] != "null" && Request["FromPublished"] != "undefined")
                        FromPublished = Convert.ToBoolean(Request["FromPublished"]);
                }
                catch { }

                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);

              

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

                var dataColl = new AcademicLib.BL.Exam.Transaction.CASMarkEntry(user.UserId, user.HostName, user.DBName).getTabulation(classId, sectionId, FilterSection, subjectId, examTypeId, casTypeId,AcademicYearId);

               
                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ReportName", "CAS Tabulation"));

                if (dataColl.Count > 0)
                {
                    var fst = dataColl[0];
                    double fm = dataColl.Where(p1 => p1.StudentId == fst.StudentId).Sum(p1 => p1.FullMark);
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassSection", fst.ClassName));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamType", fst.ExamTypeName));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubjectName", fst.SubjectName));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubjectCode", fst.SubjectCode));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("FullMark", fm.ToString()));
                }
                else
                {
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClassSection", ""));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamType", ""));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubjectName", ""));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubjectCode", ""));
                    parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("FullMark", "0"));
                }
              
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();
 
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataColl)); 

                ReportViewer1.LocalReport.SetParameters(parameterColl);

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}