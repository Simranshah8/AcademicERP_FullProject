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
    public class newpdfviewer : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
                
        public RdlReport _Report { get; set; }

        public bool error { get; set; }

        public Dynamic.Accounting.IReportLoadObjectData reportData = null;
        public static string DBName = string.Empty;

        protected NameValueCollection GetObjectAsKeyVal(object obj, string[] keys)
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
                if (keys.Contains(v.Key) == false)
                    nvColl.Add(v.Key, v.Value);
            }
            return nvColl;
        }
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
            try
            {
                string rpType = "pdf";
                Dynamic.BusinessEntity.Security.User user = (Dynamic.BusinessEntity.Security.User)context.Request.RequestContext.HttpContext.User;

                var ConnectionString = new Dynamic.DataAccess.Global.DataAccessLayer1(user.HostName, user.DBName).ConnectionString; ;
                Dynamic.Accounting.GlobalObject.ConnectionString = ConnectionString;

                string domainPath = context.Request.Url.GetLeftPart(UriPartial.Authority);// AppDomain.CurrentDomain.BaseDirectory;


                int entityId = 0, voucherId = 0, vouchetype = 0, tranid = 0;
                int rptTranId = 0;
                bool istransaction = false;
                var queryStrColl = context.Request.QueryString;
                if (queryStrColl != null)
                {
                    var keyColl = queryStrColl.AllKeys.ToList();
                    if (keyColl.Contains("entityid"))
                        int.TryParse(queryStrColl.Get("entityid"), out entityId);

                    if (keyColl.Contains("rpType"))
                        rpType = queryStrColl.Get("rpType");

                    if (keyColl.Contains("istransaction"))
                        bool.TryParse(queryStrColl.Get("istransaction"), out istransaction);

                    if (keyColl.Contains("voucherid"))
                        int.TryParse(queryStrColl.Get("voucherid"), out voucherId);

                    if (keyColl.Contains("vouchertype"))
                        int.TryParse(queryStrColl.Get("vouchertype"), out vouchetype);

                    if (keyColl.Contains("tranid"))
                        int.TryParse(queryStrColl.Get("tranid"), out tranid);

                    if (keyColl.Contains("TranId"))
                        int.TryParse(queryStrColl.Get("TranId"), out tranid);

                    if (keyColl.Contains("rpttranid"))
                        int.TryParse(queryStrColl.Get("rpttranid"), out rptTranId);
                    else if (keyColl.Contains("rptTranId"))
                        int.TryParse(queryStrColl.Get("rptTranId"), out rptTranId);

                    string sessionId = "";
                    if (keyColl.Contains("sessionid"))
                    {
                        sessionId = queryStrColl.Get("sessionid");
                        if (!string.IsNullOrEmpty(sessionId))
                        {
                            object dataObj = context.Session[sessionId];
                            if (dataObj != null)
                            {
                                if (istransaction)
                                    reportData = new AcademicERP.Models.PrintEntityObject().GetObjectList(entityId, dataObj);
                                else
                                    reportData = new AcademicERP.Models.PrintEntityObject().GetRptObjectList(entityId, dataObj);
                            }
                            // context.Session.Remove(sessionId);
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
                        var aaId = context.Session["AcademicYearId" + user.UserId.ToString()];
                        if (aaId != null)
                            AcademicYearId = (int)aaId;

                        var academicYearBL = new AcademicLib.BL.Academic.Creation.AcademicYear(user.UserId, user.HostName, user.DBName);
                        if (AcademicYearId == 0)
                            AcademicYearId = academicYearBL.getDefaultAcademicYearId().RId;

                        if (AcademicYearId != 0)
                        {
                            paraColl.Add("AcademicYearId", AcademicYearId.ToString());
                            var academicYear = academicYearBL.GetAcademicYearById(0, AcademicYearId);
                            if (academicYear != null)
                            {
                                paraColl.Add("AcademicYearName", academicYear.Name);
                                paraColl.Add("AcademicYearDisplayName", academicYear.Description);
                            }
                        }
                    }
                    catch { }

                    var rptParaColl = queryStrColl.AllKeys;
                    var tmpKeys = paraColl.AllKeys;
                    foreach (var v in rptParaColl)
                    {
                        if (tmpKeys.Contains(v) == false && v != "tranid")
                        {
                            var qval = queryStrColl.GetValues(v).First();// undefined
                            if(qval!= "undefined")
                            {
                                paraColl.Add(v, queryStrColl.GetValues(v).First());
                            }
                        }                            
                    }

                    PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, voucherId, istransaction, rptTranId);
                    Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                    var abt = new AcademicLib.BL.AppCMS.Creation.AboutUs(user.UserId, user.HostName, user.DBName).getAbout(null);
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
                    comDet1.Country = com.Country;
                    comDet1.WebUrl = System.Web.HttpContext.Current.Server.MapPath("~");
                    comDet1.Affiliated = com.Affiliated;
                    comDet1.Slogan = com.Slogan;
                    comDet1.Established = com.Established;
                    comDet1.AffiliatedLogo = abt.AffiliatedLogo;

                    try
                    {
                        string logoPath = System.Web.HttpContext.Current.Server.MapPath(abt.LogoPath);
                        if (System.IO.File.Exists(logoPath))
                        {
                            comDet1.CompanyLogoPath = logoPath;
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
                        string affiliatedLogoPath = System.Web.HttpContext.Current.Server.MapPath(abt.AffiliatedLogo);
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
                     
                    Dynamic.BusinessEntity.Global.GlobalObject.CurrentBranch = branch;

                    if (string.IsNullOrEmpty(rpType))
                        rpType = "pdf";

                    var paraColl1 = GetObjectAsKeyVal(comDet1, paraColl.AllKeys);
                    paraColl.Add(paraColl1);

                    _Report = new RdlReport(paraColl);
                    _Report.ComDet = comDet;
                    _Report.ConnectionString = ConnectionString;
                    _Report.iReportLoadObjectData = reportData;
                    _Report.RenderType = rpType;
                    
                    var urlHelper = new System.Web.Mvc.UrlHelper(context.Request.RequestContext);
                    var baseurl = urlHelper.Content("~");

                    PivotalERP.Global.ReportTemplate headerTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId,(int)(Dynamic.BusinessEntity.Global.FormsEntity.PageHeader), 0, true);
                    if (headerTemplate.DefaultTemplate != null)
                    {
                        var headerFile = System.Web.HttpContext.Current.Server.MapPath("~") + headerTemplate.DefaultTemplate.Path;
                        if (System.IO.File.Exists(headerFile))
                        {
                            _Report.HeaderTemplate = headerFile;
                        }
                    }

                    PivotalERP.Global.ReportTemplate footerTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, (int)(Dynamic.BusinessEntity.Global.FormsEntity.PageFooter), 0, true);
                    if (footerTemplate.DefaultTemplate != null)
                    {
                        var footerFile = System.Web.HttpContext.Current.Server.MapPath("~") + footerTemplate.DefaultTemplate.Path;
                        if (System.IO.File.Exists(footerFile))
                        {
                            _Report.FooterTemplate = footerFile;
                        }
                    }
                      
                    _Report.ReportFile = reportTemplate.GetPath(template);
                    _Report.NoShow = true; 

                    if (_Report.RenderType == "pdf")
                    {
                        if (_Report.Object == null)
                        {
                            //error = true;
                            context.Response.ContentType = "application/pdf;charset=UTF-8";
                            context.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"document.pdf\"");
                            string str = "";
                            if (_Report.Errors != null)
                            {
                                foreach (var err in _Report.Errors)
                                {
                                    str = str + err.ToString() + "\n";
                                }
                            }
                            else
                            {
                                
                            }
                         
                            str = str.Replace("'", "");
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                //creating a sample Document
                                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);
                                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
                                doc.Open();
                                doc.Add(new iTextSharp.text.Chunk(str));
                                doc.Close();
                                byte[] result = ms.ToArray();
                                context.Response.BinaryWrite(result);
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
                            context.Response.Charset = "UTF-8";
                            context.Response.ContentType = "application/pdf;charset=UTF-8";
                            context.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"document.pdf\"");
                            context.Response.BinaryWrite(_Report.Object);
                            context.Response.End();
                        }
                    }
                    else if (_Report.RenderType == "html" || _Report.RenderType == "htm")
                    {
                        string css = "";
                        if (_Report.CSS != null)
                            css = "<style type=text/css>" + _Report.CSS + "</style>"; ;

                        string js = "";
                        if (_Report.JavaScript != null)
                            js = "<script>" + _Report.JavaScript + "</script>";


                        string html = js + "\n" + css + "\n" + _Report.Html;

                        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                        context.Response.Charset = "UTF-8";
                        context.Response.ContentType = "application/text;charset=UTF-8";
                        context.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"document.html\"");
                        context.Response.Write(html);
                        context.Response.End();
                    }


                }


            }
            catch (Exception eCx)
            {
                //try
                //{
                //    //error = true;
                //    context.Response.ContentType = "application/pdf;charset=UTF-8";
                //    context.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"document.pdf\"");
                //    string str = eCx.Message;

                //    if (!string.IsNullOrEmpty(eCx.StackTrace))
                //    {
                //        str = str +"\n\n"+ eCx.StackTrace;
                //    }

                //    try
                //    {
                //        System.Diagnostics.StackFrame stackFrame = (new System.Diagnostics.StackTrace(eCx, true)).GetFrame(0);
                //        str=str+" "+ string.Format("At line {0} column {1} in {2}: {3} {4}{3}{5}  ",
                //           stackFrame.GetFileLineNumber(), stackFrame.GetFileColumnNumber(),
                //           stackFrame.GetMethod(), Environment.NewLine, stackFrame.GetFileName(),
                //           eCx.Message);
                //    }
                //    catch { }
                    


                //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                //    {
                //        //creating a sample Document
                //        iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);
                //        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
                //        doc.Open();
                //        doc.Add(new iTextSharp.text.Chunk(str));
                //        doc.Close();
                //        byte[] result = ms.ToArray();
                //        context.Response.BinaryWrite(result);
                //    }
                //    context.Response.End();
                //}
                //catch { }
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