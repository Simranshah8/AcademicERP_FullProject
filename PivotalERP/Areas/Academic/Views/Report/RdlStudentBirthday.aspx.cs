using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicERP.Areas.Academic.Views.Report
{
    public partial class RdlStudentBirthday : System.Web.Mvc.ViewPage
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

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentBirthDay;
                int rptTranId = Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                string path = baseurl + template.Path;
                              
                DateTime dateFrom = Convert.ToDateTime(Request["DateFrom"]);
                DateTime dateTo= Convert.ToDateTime(Request["DateTo"]);

                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);

                //System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                //parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                //parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                int academicYearId = 0;

                try
                {
                    academicYearId = (int)Session["AcademicYearId" + user.UserId.ToString()];
                }
                catch { }

                if (academicYearId == 0)
                    academicYearId = new AcademicLib.BL.Academic.Creation.AcademicYear(user.UserId, user.HostName, user.DBName).getDefaultAcademicYearId().RId;

                var dataColl = new AcademicLib.BL.Academic.Transaction.Student(user.UserId, user.HostName, user.DBName).getStudentBirthDayList(academicYearId, dateFrom, dateTo);
                 
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataColl));

                //ReportViewer1.LocalReport.SetParameters(parameterColl);

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}