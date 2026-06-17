using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AcademicERP.Areas.Academic.Views.Report
{
    public partial class RdlIdentityCard : System.Web.Mvc.ViewPage
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

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.StudentIdCard;
                int rptTranId = Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                string path = baseurl + template.Path;
                int classId = Convert.ToInt32(Request["ClassId"]);
                int? sectionId = null;
                bool OnlyPhotoStudent = false;
                int For = 0;
                int rollNoFrom = 0, rollNoTo = 0;

                int ForMonth = 0;

                bool generateQRCode = false;
                if (Request["GenerateQR"] != null)
                    generateQRCode = Convert.ToBoolean(Request["GenerateQR"]);

                if (Request["SectionId"]!=null)
                   sectionId= Convert.ToInt32(Request["SectionId"]);

                if (Request["ForMonth"] != null)
                    ForMonth = Convert.ToInt32(Request["ForMonth"]);

                if (Request["For"] != null)
                    For = Convert.ToInt32(Request["For"]);

                if (Request["rollNoFrom"] != null)
                    rollNoFrom = Convert.ToInt32(Request["rollNoFrom"]);

                if (Request["rollNoTo"] != null)
                    rollNoTo = Convert.ToInt32(Request["rollNoTo"]);

                int? SemesterId = null,ClassYearId = null,  BatchId = null;

                if (Request["SemesterId"] != null)
                    SemesterId = Convert.ToInt32(Request["SemesterId"]);
                
                if (Request["ClassYearId"] != null)
                    ClassYearId = Convert.ToInt32(Request["ClassYearId"]);
                
                if (Request["BatchId"] != null)
                    BatchId = Convert.ToInt32(Request["BatchId"]);

                if (Request["OnlyPhotoStudent"] != null)
                    OnlyPhotoStudent = Convert.ToBoolean(Request["OnlyPhotoStudent"]);

                string studentIdColl = Convert.ToString(Request["StudentIdColl"]);
                string ClassIdColl = Convert.ToString(Request["ClassIdColl"]);
                DateTime validFrom= Convert.ToDateTime(Request["ValidFrom"]);
                DateTime validTo = Convert.ToDateTime(Request["ValidTo"]);

                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);

                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> parameterColl = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyName", comDet.CompanyName));
                parameterColl.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress", comDet.CompanyAddress));
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                int academicYearId = 0;

                try
                {
                    academicYearId = (int)Session["AcademicYearId" + user.UserId.ToString()];                    
                }
                catch { }

                if(academicYearId==0)
                    academicYearId= new AcademicLib.BL.Academic.Creation.AcademicYear(user.UserId, user.HostName, user.DBName).getDefaultAcademicYearId().RId;


                var dataColl = new AcademicLib.BL.Academic.Transaction.Student(user.UserId, user.HostName, user.DBName).getStudentListForIdCard(academicYearId, classId, sectionId, studentIdColl, validFrom, validTo, 0,For,rollNoFrom,rollNoTo,ForMonth, SemesterId, ClassYearId, BatchId,OnlyPhotoStudent,ClassIdColl);

               
                if (generateQRCode)
                {
                    #region "Generate QR Code"

                    try
                    {
                        foreach (var v in dataColl)
                        {
                            if (!string.IsNullOrEmpty(v.QrCodeStr))
                            {
                                QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
                                QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(v.QrCodeStr, QRCoder.QRCodeGenerator.ECCLevel.Q);
                                QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
                                System.Drawing.Bitmap bitmap = qRCode.GetGraphic(7);
                                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                                {
                                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                                    if (ms != null)
                                        v.QRCode = ms.ToArray();
                                }
                            }
                        }
                    }
                    catch { }


                    try
                    {
                        PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(user.UserId, user.HostName, user.DBName);
                        var templateColl = new AcademicLib.BL.Setup.SENT(user.UserId, user.HostName, user.DBName).GetSENT(Convert.ToInt32(AcademicLib.BE.Global.ENTITIES.StudentIdCard), 1, 1);
                        if (templateColl != null)
                        {
                            var qrCodeTemplates = templateColl[0];
                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.RE.Academic.StudentIdCard), qrCodeTemplates.Description);

                            if (tmpFieldsColl != null)
                            {
                                if (!string.IsNullOrEmpty(qrCodeTemplates.Description))
                                {
                                    foreach (var v in dataColl)
                                    {
                                        string tempMSG = qrCodeTemplates.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            var proStr = globlFun.GetProperty(v, field.Name).ToString();
                                            tempMSG = tempMSG.Replace("##" + field.Name.Trim().ToLower() + "##", proStr);
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", proStr);
                                        }

                                        QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
                                        QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(tempMSG, QRCoder.QRCodeGenerator.ECCLevel.Q);
                                        QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
                                        System.Drawing.Bitmap bitmap = qRCode.GetGraphic(7);
                                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                                        {
                                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                                            if (ms != null)
                                                v.QrInformatic = ms.ToArray();
                                        }
                                    }
                                }
                            }
                            
                        }

                    }
                    catch { }

                   
                     

                    #endregion

               
                }
                

                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dataColl));

                //ReportViewer1.LocalReport.SetParameters(parameterColl);
                
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}