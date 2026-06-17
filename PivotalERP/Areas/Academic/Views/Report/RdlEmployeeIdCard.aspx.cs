using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AcademicERP.Areas.Academic.Views.Report
{
    public partial class RdlEmployeeIdCard : System.Web.Mvc.ViewPage
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
                ReportViewer1.KeepSessionAlive = true;
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                int entityId = (int)Dynamic.BusinessEntity.Global.RptFormsEntity.EmployeeIdCard;
                int rptTranId = Convert.ToInt32(Request["rptTranId"]);
                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(user.HostName, user.DBName, user.UserId, entityId, 0, false, rptTranId);
                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;

                string path = baseurl + template.Path;
             
                string empIdColl = Convert.ToString(Request["EmpIdColl"]);
                DateTime validFrom = Convert.ToDateTime(Request["ValidFrom"]);
                DateTime validTo = Convert.ToDateTime(Request["ValidTo"]);

                int departmentId= Convert.ToInt32(Request["DepartmentId"]);
                int designationId = Convert.ToInt32(Request["DesignationId"]);

                var comDet = new Dynamic.DataAccess.Global.GlobalDB(user.HostName, user.DBName).getCompanyBranchDetailsForPrint(user.UserId, entityId, 0, 0);
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
                ReportViewer1.LocalReport.DataSources.Clear();

                bool generateQRCode = false;
                if (Request["GenerateQR"] != null)
                    generateQRCode = Convert.ToBoolean(Request["GenerateQR"]);

                var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(user.UserId, user.HostName, user.DBName).getEmpListForIdCard(empIdColl, validFrom, validTo,departmentId,designationId);

                if (generateQRCode)
                {
                    #region "Generate QR Code"

                    try
                    {
                        foreach (var v in dataColl)
                        {
                            if (!string.IsNullOrEmpty(v.QrCode))
                            {                                
                                QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
                                QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(v.QrCode, QRCoder.QRCodeGenerator.ECCLevel.Q);
                                QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
                                System.Drawing.Bitmap bitmap = qRCode.GetGraphic(7);
                                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                                {
                                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                                    if (ms != null)
                                        v.QrImage = ms.ToArray();
                                }
                            }
                        }
                    }
                    catch { }


                    try
                    {
                        PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(user.UserId, user.HostName, user.DBName);
                        var templateColl = new AcademicLib.BL.Setup.SENT(user.UserId, user.HostName, user.DBName).GetSENT(Convert.ToInt32(AcademicLib.BE.Global.ENTITIES.EmployeeIdCard), 1, 1);
                        if (templateColl != null)
                        {
                            var qrCodeTemplates = templateColl[0];
                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.RE.Academic.EmployeeIdCard), qrCodeTemplates.Description);

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
                
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.ShowPrintButton = true;
                ReportViewer1.ZoomMode = ZoomMode.PageWidth;

            }
        }
    }
}