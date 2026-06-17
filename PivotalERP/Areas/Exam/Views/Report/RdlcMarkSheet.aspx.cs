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
    public partial class RdlcMarkSheet : System.Web.Mvc.ViewPage
    {
        public bool Loaded { get; set; }
        Dynamic.BusinessEntity.Security.User user = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Dynamic.BusinessEntity.Security.User)User;
            var urlHelper = new System.Web.Mvc.UrlHelper(Html.ViewContext.RequestContext);
            var baseurl = urlHelper.Content("~");

        
            var queryStrColl = this.Context.Request.QueryString;
            string  rptPath = ""  ;            
            int AcademicYearId = 0;

            var aaId = this.Context.Session["AcademicYearId" + user.UserId.ToString()];
            if (aaId != null)
                AcademicYearId = (int)aaId;

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
            int entityId = 0;
            int rptTranId = 0;

            var keyColl = queryStrColl.AllKeys.ToList();

            if (keyColl.Contains("entityid"))
                entityId = Convert.ToInt32(queryStrColl.Get("entityid"));

            if (keyColl.Contains("rptTranId"))
                rptTranId = Convert.ToInt32(queryStrColl.Get("rptTranId"));

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

            if (!this.IsPostBack && !Loaded)
            {
                Loaded = true;

                ReportViewer1.KeepSessionAlive = false;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                string path = baseurl + (string.IsNullOrEmpty(rptPath) ? template.Path : rptPath);
                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);
                 
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();
  
                Dynamic.BusinessEntity.Setup.ReportWriterParaCollections rptParaColl = new Dynamic.BusinessEntity.Setup.ReportWriterParaCollections();
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "StudentId", AllowNull=true, DefaultValue = (StudentId.HasValue ? StudentId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ClassId", AllowNull = true, DefaultValue = (ClassId.HasValue ? ClassId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "SectionId", AllowNull = true, DefaultValue = (SectionId.HasValue ? SectionId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ExamTypeId", AllowNull = true, DefaultValue =  ExamTypeId.ToString() , DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "FilterSection", AllowNull = true, DefaultValue = FilterSection.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.YESNO });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "classIdColl", AllowNull = true, DefaultValue = classIdColl.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.TEXT });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "BatchId", AllowNull = true, DefaultValue = (BatchId.HasValue ? BatchId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "SemesterId", AllowNull = true, DefaultValue = (SemesterId.HasValue ? SemesterId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ClassYearId", AllowNull = true, DefaultValue = (ClassYearId.HasValue ? ClassYearId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                //rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "FromPublished", AllowNull = true, DefaultValue = FromPublished.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.YESNO });
                rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "BranchId", AllowNull = true, DefaultValue = (BranchId.HasValue ? BranchId.ToString() : null), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
                
                var globlDB = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName);
                var tmpDataColl = globlDB.getDataTable(user.UserId, "exec usp_PrintMarkSheet_Only @UserId=@UserId,@StudentId=@StudentId,@ClassId=@ClassId,@SectionId=@SectionId,@ExamTypeId=@ExamTypeId,@FilterSection=@FilterSection,@classIdColl=@classIdColl,@BatchId=@BatchId,@SemesterId=@SemesterId,@ClassYearId=@ClassYearId,@BranchId=@BranchId", rptParaColl);
                Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", tmpDataColl);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExamTypeId", ExamTypeId.ToString()));
                ReportViewer1.LocalReport.SetParameters(parameterColl);
                ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;             
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
             
        
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            user = (Dynamic.BusinessEntity.Security.User)User;
            var globlDB = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName);
            int sid = Convert.ToInt32(e.Parameters["StudentId"].Values[0]);
            int eid = Convert.ToInt32(e.Parameters["ExamTypeId"].Values[0]);
            var rptParaColl = new Dynamic.BusinessEntity.Setup.ReportWriterParaCollections();
            rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "StudentId", DefaultValue = sid.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
            rptParaColl.Add(new Dynamic.BusinessEntity.Setup.ReportWriterPara() { VariableName = "ExamTypeId", DefaultValue = eid.ToString(), DataType = Dynamic.BusinessEntity.Setup.DATATYPES.NUMBER });
            var subTables = globlDB.getDataTable(user.UserId, "exec usp_SubMarkSheet @UserId=@UserId,@StudentId=@StudentId,@ExamTypeId=@ExamTypeId", rptParaColl);
            Microsoft.Reporting.WebForms.ReportDataSource subDS = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", subTables);
            e.DataSources.Add(subDS);
        }
    }
}