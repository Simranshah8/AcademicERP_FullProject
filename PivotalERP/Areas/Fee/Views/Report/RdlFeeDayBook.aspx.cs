using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AcademicERP.Areas.Fee.Views.Report
{
    public partial class RdlFeeDayBook : System.Web.Mvc.ViewPage
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

            int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.FeeReceiptCollection;
            int rptTranId = Convert.ToInt32(Request["rptTranId"]);
            PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
            Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

            string path = baseurl + template.Path;
                var queryStrColl = Context.Request.QueryString;
                var keyColl = queryStrColl.AllKeys.ToList();
            
                List<AcademicLib.RE.Fee.FeeReceipt> dataColl = new List<AcademicLib.RE.Fee.FeeReceipt>();
                
                if (keyColl.Contains("sessionid"))
                {
                    string sessionId = queryStrColl.Get("sessionid");
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        object dataObj = Context.Session[sessionId];
                        if (dataObj != null)
                        {
                            dataColl = (List<AcademicLib.RE.Fee.FeeReceipt>)dataObj; 
                        } 
                    }
                }
                 

                  var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);
                string Period = "";
                double OpeningDisAmt=0, OpeningAmt = 0, CurrentAmt = 0, CurrentDisAmt = 0;

                try
                {
                    Period = Convert.ToString(Request["Period"]);
                    OpeningDisAmt = Convert.ToDouble(Request["OpeningDisAmt"]);
                    OpeningAmt = Convert.ToDouble(Request["OpeningAmt"]);
                    CurrentAmt = Convert.ToDouble(Request["CurrentAmt"]);
                    CurrentDisAmt = Convert.ToDouble(Request["CurrentDisAmt"]);
                }
                catch { }
               

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Period", Period));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("UserName", user.UserName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("OpeningDisAmt", OpeningDisAmt.ToString()));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("OpeningAmt", OpeningAmt.ToString()));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CurrentAmt", CurrentAmt.ToString()));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CurrentDisAmt", CurrentDisAmt.ToString()));

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