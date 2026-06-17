using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dynamic.ReportEngine.RdlAsp;
using System.Data;
using System.Collections.Specialized;

namespace WebSMS
{

    public partial class ShowReport : System.Web.UI.Page
    {

        public RdlReport _Report { get; set; }

        public bool error { get; set; }

        public Dynamic.Accounting.IReportLoadObjectData reportData = null;
        public static string DBName = string.Empty;

        protected NameValueCollection GetObjectAsKeyVal(object obj)
        {
            NameValueCollection nvColl = new NameValueCollection();
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select new
                             {
                                 Key = p.Name,
                                 Value = p.GetValue(obj, null).ToString()
                             };

            foreach (var v in properties)
            {
                nvColl.Add(v.Key, v.Value);
            }
            return nvColl;
        }
        public bool Name { get; set; }

        public ShowReport()
        {
            _Report = new RdlReport(null);
        }

        public string GetHtmlReport(int entityid)
        {
            //Schooling Reporting
            Dynamic.Accounting.IReportLoadObjectData reportData = null;// new WebSMS.Main().GetReport(entityid);
            _Report.RenderType = "html";
            _Report.iReportLoadObjectData = reportData;
            //_Report.ReportFile = "../" + reportData.ReportPath;
            _Report.NoShow = true;
            string css = "";
            if (_Report.CSS != null)
                css = "<style type=text/css>" + _Report.CSS + "</style>"; ;

            string js = "";
            if (_Report.JavaScript != null)
                js = "<script>" + _Report.JavaScript + "</script>";


            string ht = js + "\n" + css + "\n" + _Report.Html;

            return ht;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Dynamic.BusinessEntity.Security.User user = (Dynamic.BusinessEntity.Security.User)this.Context.Request.RequestContext.HttpContext.User;
            Dynamic.Accounting.GlobalObject.ConnectionString = new Dynamic.DataAccess.Global.DataAccessLayer1(user.HostName, user.DBName).ConnectionString;
            string domainPath = this.Context.Request.Url.GetLeftPart(UriPartial.Authority);// AppDomain.CurrentDomain.BaseDirectory;


            int entityId = 0, voucherId = 0, vouchetype = 0, tranid = 0;
            int rptTranId = 0;
            bool istransaction = false;
            var queryStrColl = this.Context.Request.QueryString;
            if (queryStrColl != null)
            {
                try
                {
                    var keyColl = queryStrColl.AllKeys.ToList();
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
                            //context.Session.Remove(sessionId);
                        }
                    }

                }
                catch { }

            }


            var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, vouchetype, tranid);
            if (comDet != null && comDet.IsSuccess || !string.IsNullOrEmpty(comDet.CompanyName))
            {
                System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                paraColl.Add("UserId", user.UserId.ToString());
                paraColl.Add("TranId", tranid.ToString());
                paraColl.Add("UserName", user.UserName);

                var rptParaColl = queryStrColl.AllKeys;
                var tmpKeys = paraColl.AllKeys;
                foreach (var v in rptParaColl)
                {
                    if (tmpKeys.Contains(v) == false && v != "tranid")
                        paraColl.Add(v, queryStrColl.GetValues(v).First());
                }

                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, voucherId, istransaction, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;


                var com = new Dynamic.DataAccess.Setup.CompanyDetailDB(user.HostName, user.DBName).getCompanyDetailsWithOutLogo(user.UserId,comDet.BranchId);

                Dynamic.BusinessEntity.Setup.CompanyDetail comDet1 = new Dynamic.BusinessEntity.Setup.CompanyDetail();
                comDet1.Name = comDet.CompanyName;
                comDet1.Address = comDet.CompanyAddress;
                comDet1.PanVatNo = comDet.PanVat;
                comDet1.PhoneNo = comDet.PhoneNo;
                comDet1.EmailId = comDet.EmailId;
                comDet1.WebSite = comDet.WebSite;
                comDet1.MailingName = com.MailingName;
                comDet1.RegdNo = com.RegdNo;
                comDet1.FaxNo = com.FaxNo;
                comDet1.ZipCode = com.ZipCode;
                comDet1.City = com.City;
                comDet1.Zone = com.Zone;
                comDet1.CompanyLogoPath = com.CompanyLogoPath;
                comDet1.WebUrl = System.Web.HttpContext.Current.Server.MapPath("~");
                try
                {
                    string logoPath = System.Web.HttpContext.Current.Server.MapPath(comDet1.CompanyLogoPath);
                    if (System.IO.File.Exists(logoPath))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(logoPath);
                        byte[] arr;
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            arr = ms.ToArray();
                        }
                        comDet1.Logo = arr;
                    }
                }
                catch { }


                Dynamic.BusinessEntity.Global.GlobalObject.CurrentCompany = comDet1;

                Dynamic.BusinessEntity.Security.Branch branch = new Dynamic.BusinessEntity.Security.Branch();
                branch.Name = comDet.CompanyName;
                branch.Address = comDet.CompanyAddress;

                _Report = new RdlReport(paraColl);
                _Report.iReportLoadObjectData = reportData;
                _Report.RenderType = "html";

                var urlHelper = new System.Web.Mvc.UrlHelper(this.Context.Request.RequestContext);
                var baseurl = urlHelper.Content("~");

                _Report.ReportFile = reportTemplate.GetPath(template);
                _Report.NoShow = true;
                Response.AppendHeader("content-disposition", "attachment; filename=file.xls");
                if (_Report.Html == null)
                {
                    error = true;
                }
                else
                {
                    string css = "";
                    if (_Report.CSS != null)
                        css = "<style type=text/css>" + _Report.CSS + "</style>"; ;

                    string js = "";
                    if (_Report.JavaScript != null)
                        js = "<script>" + _Report.JavaScript + "</script>";


                    string ht = js + "\n" + css + "\n" + _Report.Html;

                    Response.ContentType = "application/excel";
                    Response.Write(ht);
                    Response.Flush();
                    Response.Close();
                    Response.End();
                }

            }
        }

        public string Meta
        {
            get
            {
                if (_Report.ReportFile == "statistics")
                    return "<meta http-equiv=\"Refresh\" contents=\"10\"/>";
                else
                    return "";
            }
        }
    }
}