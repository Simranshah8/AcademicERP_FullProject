using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicERP.Areas.Fee.Views.Report
{
    public partial class RptStudentLedger : System.Web.Mvc.ViewPage
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

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentLedger;
                int rptTranId = Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                string path = baseurl + template.Path;
                int studentId = Convert.ToInt32(Request["studentId"]);
                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Address", comDet.CompanyAddress));                

                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                int AcademicYearId = 0;
                int? SemesterId = null;
                int? ClassYearId = null;
                try
                {

                    var sessionId = Session["AcademicYearId" + user.UserId.ToString()];
                    if (sessionId != null)
                        AcademicYearId = (int)sessionId;

                    if (AcademicYearId == 0)
                        AcademicYearId = new AcademicLib.BL.Academic.Creation.AcademicYear(user.UserId, user.HostName, user.DBName).getDefaultAcademicYearId().RId;
                }
                catch { }

                try
                {
                    var sid = Request["semesterId"];
                    if (sid != null)
                        SemesterId = Convert.ToInt32(sid);

                    sid = Request["classYearId"];
                    if (sid != null)
                        ClassYearId = Convert.ToInt32(sid);
                }
                catch { }

                
                var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(user.UserId, user.HostName, user.DBName).getStudentLedger(AcademicYearId, studentId,SemesterId,ClassYearId);
                List<AcademicLib.RE.Fee.StudentVoucher> vouchersColl = new List<AcademicLib.RE.Fee.StudentVoucher>();
                vouchersColl.Add(dataColl);
                var academicYearName = dataColl.AcademicYear;
                foreach(var v in vouchersColl)
                {
                    v.AcademicYear = academicYearName;
                }
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", vouchersColl));
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", dataColl.LedgerDetailsColl));

                //parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("AcademicYear", dataColl.AcademicYear));
                ReportViewer1.LocalReport.SetParameters(parameterColl);

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}