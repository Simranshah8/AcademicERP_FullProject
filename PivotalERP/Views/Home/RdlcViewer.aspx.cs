using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PivotalERP.Views.Home
{
    public partial class RdlcViewer : System.Web.Mvc.ViewPage
    {
        public bool Loaded { get; set; }

         
        protected DataTable ConvertToDataTable(System.Collections.IEnumerable list)
        {
            Newtonsoft.Json.Linq.JObject firstRow = null;
            foreach (Newtonsoft.Json.Linq.JObject obj in list)
            {
                if (firstRow == null)
                {
                    firstRow = obj;
                    break;
                }
            }

            DataTable table = new DataTable();

            // Create columns dynamically from the first JObject
            foreach (var key in firstRow.Properties())
            {
                table.Columns.Add(key.Name);
            }

            // Add rows to the DataTable
            foreach (Newtonsoft.Json.Linq.JObject obj in list)
            {
                var row = table.NewRow();
                foreach (var key in obj.Properties())
                {
                    row[key.Name] = key.Value?.ToString() ?? DBNull.Value.ToString();
                }
                table.Rows.Add(row);
            }

            return table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Dynamic.BusinessEntity.Security.User user = (Dynamic.BusinessEntity.Security.User)User;
            Dynamic.Accounting.IReportLoadObjectData reportData = null;
            var urlHelper = new System.Web.Mvc.UrlHelper(Html.ViewContext.RequestContext);
            var baseurl = urlHelper.Content("~");

            int entityId = 0, voucherId = 0, vouchetype = 0, tranid = 0;
            int rptTranId = 0;
            bool istransaction = false;
            var queryStrColl = this.Context.Request.QueryString;
            string reportName = "", rptPath = "", ds_name = "" ;
            bool hasQR = false;
            int AcademicYearId = 0;

            var aaId = this.Context.Session["AcademicYearId" + user.UserId.ToString()];
            if (aaId != null)
                AcademicYearId = (int)aaId;

            if (queryStrColl != null)
            {
                var keyColl = queryStrColl.AllKeys.ToList();

                if (keyColl.Contains("Qr"))
                    bool.TryParse(queryStrColl.Get("Qr"), out hasQR);

                if (keyColl.Contains("DS_Name"))
                    ds_name = queryStrColl.Get("DS_Name");

                if (keyColl.Contains("ReportName"))
                    reportName = queryStrColl.Get("ReportName");

                if (keyColl.Contains("RptPath"))
                    rptPath = queryStrColl.Get("RptPath");

                if (keyColl.Contains("entityid"))
                    int.TryParse(queryStrColl.Get("entityid"), out entityId);

                if (keyColl.Contains("istransaction"))
                    bool.TryParse(queryStrColl.Get("istransaction"), out istransaction);

                if (keyColl.Contains("voucherid"))
                    int.TryParse(queryStrColl.Get("voucherid"), out voucherId);

                if (keyColl.Contains("vouchertype"))
                    int.TryParse(queryStrColl.Get("vouchertype"), out vouchetype);

                if (keyColl.Contains("tranid"))
                    int.TryParse(queryStrColl.Get("tranid"), out tranid);

                if (keyColl.Contains("rpttranid"))
                    int.TryParse(queryStrColl.Get("rpttranid"), out rptTranId);

                int minRows = 0;

                if (keyColl.Contains("minRows"))
                    int.TryParse(queryStrColl.Get("minRows"), out minRows);

                string sessionId = "";
                if (keyColl.Contains("sessionid"))
                {
                    sessionId = queryStrColl.Get("sessionid");
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        object dataObj = this.Context.Session[sessionId];
                        if (dataObj != null)
                        {
                            if (istransaction)
                                reportData = new AcademicERP.Models.PrintEntityObject().GetObjectList(entityId, dataObj);
                            else
                                reportData = new AcademicERP.Models.PrintEntityObject().GetRptObjectList(entityId, dataObj);
                        }
                        this.Context.Session.Remove(sessionId);
                    }
                }

                if (string.IsNullOrEmpty(sessionId))
                {
                    if(entityId== 360) // For General Marksheet
                    {
                        int? StudentId = null;
                        int? ClassId = null;
                        int? SectionId = null;
                        int ExamTypeId = 0;
                        bool FilterSection = false;
                        string classIdColl = "";
                        int? BatchId = null;
                        int? SemesterId = null;
                        int? ClassYearId = null;
                        bool FromPublished = false;
                        int? BranchId = null;


                        if (keyColl.Contains("FromPublished"))
                            FromPublished = Convert.ToBoolean(queryStrColl.Get("FromPublished"));

                        if (keyColl.Contains("BranchId"))
                            BranchId = Convert.ToInt32(queryStrColl.Get("BranchId"));

                        if (keyColl.Contains("StudentId"))
                            StudentId = Convert.ToInt32(queryStrColl.Get("StudentId"));

                        if (keyColl.Contains("ClassId"))
                            ClassId = Convert.ToInt32(queryStrColl.Get("ClassId"));

                        if (keyColl.Contains("SectionId"))
                            SectionId = Convert.ToInt32(queryStrColl.Get("SectionId"));

                        if (keyColl.Contains("ExamTypeId"))
                            ExamTypeId = Convert.ToInt32(queryStrColl.Get("ExamTypeId"));

                        if (keyColl.Contains("FilterSection"))
                            FilterSection = Convert.ToBoolean(queryStrColl.Get("FilterSection"));

                        if (keyColl.Contains("classIdColl"))
                            classIdColl = Convert.ToString(queryStrColl.Get("classIdColl"));

                        if (keyColl.Contains("BatchId"))
                            BatchId = Convert.ToInt32(queryStrColl.Get("BatchId"));

                        if (keyColl.Contains("SemesterId"))
                            SemesterId = Convert.ToInt32(queryStrColl.Get("SemesterId"));

                        if (keyColl.Contains("ClassYearId"))
                            ClassYearId = Convert.ToInt32(queryStrColl.Get("ClassYearId"));

                        Dynamic.BusinessEntity.Setup.ReportWriterParaCollections rptParaColl = new Dynamic.BusinessEntity.Setup.ReportWriterParaCollections();
                      //  rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ExamTypeId", DefaultValue = examTypeId.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });

                        var dataColl = new AcademicLib.BL.Exam.Transaction.MarksEntry(user.UserId, user.HostName, user.DBName).getMarkSheetClassWise(AcademicYearId, StudentId, ClassId, SectionId, ExamTypeId, FilterSection, classIdColl, BatchId, SemesterId, ClassYearId, FromPublished, BranchId);
                    }
                 
                }
            }
            if (!this.IsPostBack && !Loaded && reportData!=null)
            {
                Loaded = true;
                
                ReportViewer1.KeepSessionAlive = false;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, voucherId, istransaction, rptTranId);                
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;                 
                string path = baseurl +(string.IsNullOrEmpty(rptPath) ? template.Path : rptPath); 
                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                //parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("Period", "Period"));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("UserName", user.UserName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("BranchName", comDet.BranchName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("BranchAddress", comDet.BranchAddress));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("PanVatNo", comDet.PanVat));

                string alltext = System.IO.File.ReadAllText(Server.MapPath(path));
                int find = alltext.IndexOf("<ReportParameters>");
                if (find > 0)
                {
                    int lind = alltext.LastIndexOf("</ReportParameters>");
                    if (lind > 0)
                    {
                        alltext = alltext.Substring(find, (lind - find + 1));
                        var rptParaColl = queryStrColl.AllKeys;
                        foreach (var v in rptParaColl)
                        {
                            string key = "<ReportParameter Name=\"" + v.Trim() + "\">";
                            if (alltext.Contains(key))
                            {
                                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter(v, queryStrColl.GetValues(v).First()));
                            }
                        }
                    }
                    
                }
                 
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                if (!string.IsNullOrEmpty(ds_name))
                {
                    var dataCC = reportData.DataColl;                     
                    var reportDataSource = new ReportDataSource("DataSet1", ConvertToDataTable(dataCC));
                    ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
                    
                }
                else
                    ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", reportData.DataColl));

                if (find!=-1)
                    ReportViewer1.LocalReport.SetParameters(parameterColl);

                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}