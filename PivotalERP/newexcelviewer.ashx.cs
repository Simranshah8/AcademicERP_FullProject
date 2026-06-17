using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Dynamic.ReportEngine.RdlAsp;

namespace WebSMS
{
    /// <summary>
    /// Summary description for newpdfviewer
    /// </summary>
    public class newexcelviewer : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
        public void ProcessRequest(HttpContext context)
        {
            
            Dynamic.BusinessEntity.Security.User user = (Dynamic.BusinessEntity.Security.User)context.Request.RequestContext.HttpContext.User;
            Dynamic.Accounting.GlobalObject.ConnectionString = new Dynamic.DataAccess.Global.DataAccessLayer1(user.HostName, user.DBName).ConnectionString;
            string domainPath = context.Request.Url.GetLeftPart(UriPartial.Authority);// AppDomain.CurrentDomain.BaseDirectory;
                                                                                      // Dynamic.Accounting.GlobalObject.FolderPath = domainPath;

            int entityId = 0,voucherId=0,vouchetype=0,tranid=0;
            int rptTranId = 0;
            bool istransaction =false;
            var queryStrColl = context.Request.QueryString;
            if (queryStrColl != null)
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
                        object dataObj = context.Session[sessionId];
                        if (dataObj != null)
                        {
                            if(istransaction)
                                reportData = new AcademicERP.Models.PrintEntityObject().GetObjectList(entityId, dataObj);                            
                            else
                                reportData = new AcademicERP.Models.PrintEntityObject().GetRptObjectList(entityId, dataObj);
                        }
                        context.Session.Remove(sessionId);
                    }
                }
                    

            }
            int AcademicYearId = 0;
            var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, vouchetype, tranid);
            if (comDet.IsSuccess || !string.IsNullOrEmpty(comDet.CompanyName))
            {
                System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                paraColl.Add("UserId", user.UserId.ToString());
                paraColl.Add("TranId", tranid.ToString());
                paraColl.Add("UserName", user.UserName);

                try
                {
                    var sessionId = context.Session["AcademicYearId" + user.UserId.ToString()];
                    if (sessionId != null)
                        AcademicYearId = (int)sessionId;

                    if (AcademicYearId == 0)
                        AcademicYearId = new AcademicLib.BL.Academic.Creation.AcademicYear(user.UserId, user.HostName, user.DBName).getDefaultAcademicYearId().RId;

                    if (AcademicYearId != 0)
                        paraColl.Add("AcademicYearId", AcademicYearId.ToString());

                }
                catch { }

                var rptParaColl= queryStrColl.AllKeys;
                var tmpKeys = paraColl.AllKeys;
                foreach (var v in rptParaColl)
                {
                    if (tmpKeys.Contains(v) == false && v!= "tranid")
                        paraColl.Add(v, queryStrColl.GetValues(v).First());
                }

                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, voucherId, istransaction,rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                var abt=new AcademicLib.BL.AppCMS.Creation.AboutUs(1, user.HostName, user.DBName).getAbout(null);
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
                comDet1.WebUrl = System.Web.HttpContext.Current.Server.MapPath("~");
                try
                {
                    string logoPath = System.Web.HttpContext.Current.Server.MapPath(abt.LogoPath);
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

                try
                {
                    string affiliatedLogoPath = System.Web.HttpContext.Current.Server.MapPath(com.AffiliatedLogo);
                    if (System.IO.File.Exists(affiliatedLogoPath))
                    {                       
                        comDet1.AffiliatedLogo = affiliatedLogoPath;
                    }
                }
                catch { }

                Dynamic.BusinessEntity.Global.GlobalObject.CurrentCompany = comDet1;

                Dynamic.BusinessEntity.Security.Branch branch = new Dynamic.BusinessEntity.Security.Branch();
                branch.Name = comDet.CompanyName;
                branch.Address = comDet.CompanyAddress;

                _Report = new RdlReport(paraColl);                
                _Report.iReportLoadObjectData = reportData;
                _Report.RenderType = "xlsx";

                var urlHelper = new System.Web.Mvc.UrlHelper(context.Request.RequestContext);
                var baseurl = urlHelper.Content("~");

                _Report.ReportFile = reportTemplate.GetPath(template);
                _Report.NoShow = true;

                if (_Report.Object == null)
                {
                    //error = true;
                    context.Response.ContentType = "application/pdf;charset=UTF-8";
                    context.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"document.pdf\"");
                    string str = "";
                    foreach (var err in _Report.Errors)
                    {
                        str = str + err.ToString() + "\n";
                    }
                    str = str.Replace("'", "");
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        //creating a sample Document
                        //iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);
                        //iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
                        //doc.Open();
                        //doc.Add(new iTextSharp.text.Chunk(str));
                        //doc.Close();
                        //byte[] result = ms.ToArray();
                        //context.Response.BinaryWrite(result);
                    }
                    context.Response.End();
                }
                else
                {

                    if (tranid > 0)
                    {
                        Dynamic.BusinessEntity.Global.AuditLogReport printLog = new Dynamic.BusinessEntity.Global.AuditLogReport();
                        printLog.ReportAction = Dynamic.BusinessEntity.Global.ReportActions.PRINT;
                        printLog.MacAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? context.Request.UserHostAddress;
                        printLog.PCName = "Web User";
                        printLog.UserName = user.UserName;
                        printLog.UserId = user.UserId;
                        printLog.TranId = tranid;
                        printLog.EntityId = entityId;
                        printLog.AutoManualNo = tranid.ToString();
                        printLog.SystemUser = user.UserName;
                        printLog.EntityName = ((Dynamic.BusinessEntity.Global.FormsEntity)entityId).ToString();
                        printLog.LogDate = DateTime.Now;
                        printLog.LogText = "Printed";
                        new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).SaveTransactionPrintAuditLog(printLog);
                    }
                    context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    context.Response.ContentType = "application/pdf;charset=UTF-8";
                    context.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"document.pdf\"");
                    context.Response.BinaryWrite(_Report.Object);
                    context.Response.End();
                }
            }
           


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}