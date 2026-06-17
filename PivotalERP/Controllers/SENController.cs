using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PivotalERP.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;

namespace AcademicERP.Controllers
{
    public class SENController : APIBaseController
    {

        #region "Ledger Group"

        // POST api/LedgerGroup
        /// <summary>
        ///  Send SMS Email Notification After Save/Modile/Delete Ledger Group     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult LedgerGroup([FromBody] Dynamic.SEN_BE.Account.LedgerGroup para)
        {
            
            ResponeValues resVal = new ResponeValues();
            try
            {                
                if (para == null)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No FromBody Data Found";                    
                }
                else
                {
                    PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                    List<Dynamic.BusinessEntity.Global.SMSEmailNotification> templatesColl = new Dynamic.DataAccess.Global.SMSEmailNotificationDB(hostName,dbName).getSMSEmailNotification(UserId, Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.LedgerGroup), false);
                    if (templatesColl != null && templatesColl.Count > 0)
                    {
                        #region "Send Email"

                        Dynamic.BusinessEntity.Global.SMSEmailNotification templateEmail = templatesColl.Find(p1 => p1.ForTemplate == Dynamic.BusinessEntity.Global.FORTEMPLATES.EMAIL);
                        if (templateEmail != null)
                        {
                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(Dynamic.SEN_BE.Account.LedgerGroup), templateEmail.Message);
                            string tempMSG = templateEmail.Message;
                            foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                            {
                                tempMSG = tempMSG.Replace("##" + field.Name.Trim().ToLower() + "##", globlFun.GetProperty(para, field.Name).ToString());
                            }

                            Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails();
                            mail.Subject = templateEmail.Title;
                            mail.Message = tempMSG;

                            bool first = true;
                            foreach (var v in templateEmail.UserColl)
                            {
                                if (first && !string.IsNullOrEmpty(v.EmailId))
                                {
                                    mail.To = v.EmailId;
                                    first = false;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(mail.Cc) && !string.IsNullOrEmpty(v.EmailId))
                                    {
                                        mail.Cc = mail.Cc + ",";
                                    }

                                    if (!string.IsNullOrEmpty(v.EmailId))
                                        mail.Cc = mail.Cc + v.EmailId;
                                }
                            }

                           resVal= globlFun.SendEMail(mail);
                            
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "No Email Templates Found";
                        }

                        #endregion
                    }               
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                   "IsSuccess","ResponseMSG"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        #endregion
        #region "Ledger"

        // POST api/Ledger
        /// <summary>
        ///  Send SMS Email Notification After Save/Modile/Delete Ledger     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult Ledger([FromBody] Dynamic.SEN_BE.Account.Ledger para)
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No FromBody Data Found";
                }
                else
                {
                    PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                    List<Dynamic.BusinessEntity.Global.SMSEmailNotification> templatesColl = new Dynamic.DataAccess.Global.SMSEmailNotificationDB(hostName, dbName).getSMSEmailNotification(UserId, Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.Ledger), false);
                    if (templatesColl != null && templatesColl.Count > 0)
                    {
                        #region "Send Email"

                        Dynamic.BusinessEntity.Global.SMSEmailNotification templateEmail = templatesColl.Find(p1 => p1.ForTemplate == Dynamic.BusinessEntity.Global.FORTEMPLATES.EMAIL);
                        if (templateEmail != null)
                        {
                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(Dynamic.SEN_BE.Account.Ledger), templateEmail.Message);
                            string tempMSG = templateEmail.Message;
                            foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                            {
                                tempMSG = tempMSG.Replace("##" + field.Name.Trim().ToLower() + "##", globlFun.GetProperty(para, field.Name).ToString());
                            }

                            Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails();
                            mail.Subject = templateEmail.Title;
                            mail.Message = tempMSG;

                            bool first = true;
                            foreach (var v in templateEmail.UserColl)
                            {
                                if (first && !string.IsNullOrEmpty(v.EmailId))
                                {
                                    mail.To = v.EmailId;
                                    first = false;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(mail.Cc) && !string.IsNullOrEmpty(v.EmailId))
                                    {
                                        mail.Cc = mail.Cc + ",";
                                    }

                                    if (!string.IsNullOrEmpty(v.EmailId))
                                        mail.Cc = mail.Cc + v.EmailId;
                                }
                            }

                            resVal = globlFun.SendEMail(mail);

                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "No Email Templates Found";
                        }

                        #endregion
                    }
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                   "IsSuccess","ResponseMSG"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        #endregion

        #region "Ledger Merge"

        // POST api/Ledger
        /// <summary>
        ///  Send SMS Email Notification After Save/Modile/Delete Ledger     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        public IHttpActionResult LedgerMerge([FromBody] Dynamic.SEN_BE.Account.LedgerMerge para)
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                if (para == null)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No FromBody Data Found";
                }
                else
                {
                    PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                    List<Dynamic.BusinessEntity.Global.SMSEmailNotification> templatesColl = new Dynamic.DataAccess.Global.SMSEmailNotificationDB(hostName, dbName).getSMSEmailNotification(UserId, Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.LedgerMerge), false);
                    if (templatesColl != null && templatesColl.Count > 0)
                    {
                        #region "Send Email"

                        Dynamic.BusinessEntity.Global.SMSEmailNotification templateEmail = templatesColl.Find(p1 => p1.ForTemplate == Dynamic.BusinessEntity.Global.FORTEMPLATES.EMAIL);
                        if (templateEmail != null)
                        {
                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(Dynamic.SEN_BE.Account.LedgerMerge), templateEmail.Message);
                            string tempMSG = templateEmail.Message;
                            foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                            {
                                tempMSG = tempMSG.Replace("##" + field.Name.Trim().ToLower() + "##", globlFun.GetProperty(para, field.Name).ToString());
                            }

                            Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails();
                            mail.Subject = templateEmail.Title;
                            mail.Message = tempMSG;

                            bool first = true;
                            foreach (var v in templateEmail.UserColl)
                            {
                                if (first && !string.IsNullOrEmpty(v.EmailId))
                                {
                                    mail.To = v.EmailId;
                                    first = false;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(mail.Cc) && !string.IsNullOrEmpty(v.EmailId))
                                    {
                                        mail.Cc = mail.Cc + ",";
                                    }

                                    if (!string.IsNullOrEmpty(v.EmailId))
                                        mail.Cc = mail.Cc + v.EmailId;
                                }
                            }

                            resVal = globlFun.SendEMail(mail);

                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "No Email Templates Found";
                        }

                        #endregion
                    }
                }
                return Json(resVal, new JsonSerializerSettings
                {
                    ContractResolver = new JsonContractResolver()
                    {
                        IsInclude = true,
                        IncludeProperties = new List<string>
                                 {
                                   "IsSuccess","ResponseMSG"
                                 }
                    }
                });
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }


        }


        #endregion

        // POST api/PAEmailToAgent
        /// <summary>
        ///  Send Email Party Ageing Report To Agent (ASM)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ResponeValues))]
        [Obsolete]
        public IHttpActionResult StudentBirthday([FromBody] JObject para)
        {
            ResponeValues resVal = new ResponeValues();
            
            try
            {

                resVal = SendStudentBirthday(1,hostName,dbName);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return Json(resVal, new JsonSerializerSettings
            {
            });

        }

        public ResponeValues SendStudentBirthday(int UserId,string hostName,string dbName)
        {
            ResponeValues resVal = new ResponeValues();
            string steps = "";
            try
            {
                int entityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.RptFormsEntity.StudentBirthDay);
                DateTime dateFrom = DateTime.Today;
                var tmpDataColl = new AcademicLib.BL.Academic.Transaction.Student(UserId, hostName, dbName).getStudentBirthDayList(null, dateFrom, dateFrom);
                System.Collections.Generic.Dictionary<int, string> attachFileColl = new Dictionary<int, string>();
                if (tmpDataColl != null && tmpDataColl.IsSuccess)
                {
                    #region " Send Email"
                    {
                        try
                        {
                            var templatesColl = new AcademicLib.BL.Setup.SENT(UserId, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.STUDENT_BIRTHDAY, 3, 2);
                            if (templatesColl != null && templatesColl.Count > 0)
                            {

                                var templateEmail = templatesColl[0];
                                PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(hostName, dbName, UserId, entityId, 0, false);
                                if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                                {
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = "No Report Template Found";
                                }
                                else
                                {
                                    PivotalERP.Global.GlobalFunction glbFN = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                                    System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = glbFN.GetPropertyInfos(typeof(AcademicLib.RE.Academic.StudentBirthDay), templateEmail.Description);
                                    foreach (var vq in tmpDataColl)
                                    {
                                        string tempMSG = templateEmail.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", glbFN.GetProperty(vq, field.Name).ToString());
                                        }
                                        vq.Template = tempMSG;

                                        var studentDataColl = new AcademicLib.RE.Academic.StudentBirthDayCollections();
                                        studentDataColl.Add(vq);

                                        foreach (var tmpTemp in reportTemplate.TemplateAttachments)
                                        {
                                            if (tmpTemp.ForEmail)
                                            {
                                                Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.GetTemplate(tmpTemp);
                                                if (template != null && !string.IsNullOrEmpty(template.Path))
                                                {
                                                    Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = new Dynamic.DataAccess.Global.GlobalDB(hostName, dbName).getCompanyBranchDetailsForPrint(UserId, 0, 0, 0);
                                                    System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                                                    paraColl.Add("UserId", UserId.ToString());
                                                    paraColl.Add("UserName", User.Identity.Name);

                                                    var reportFilePath = reportTemplate.GetPath(template);

                                                    if (reportFilePath.Contains(".rdlc") || reportFilePath.Contains(".RDLC"))
                                                    {

                                                        Microsoft.Reporting.WebForms.Warning[] warnings;
                                                        string[] streamIds;
                                                        string contentType;
                                                        string encoding;
                                                        string extension;

                                                        Microsoft.Reporting.WebForms.ReportViewer reportViewer = new ReportViewer();
                                                        LocalReport report = new LocalReport();
                                                        reportViewer.KeepSessionAlive = false;
                                                        reportViewer.AsyncRendering = false;
                                                        reportViewer.LocalReport.EnableExternalImages = true;
                                                        reportViewer.ProcessingMode = ProcessingMode.Local;
                                                        reportViewer.LocalReport.ReportPath = reportFilePath;
                                                        ReportDataSource datasource = new ReportDataSource("DataSet1", studentDataColl);
                                                        reportViewer.LocalReport.DataSources.Clear();
                                                        reportViewer.LocalReport.DataSources.Add(datasource);
                                                        //Export the RDLC Report to Byte Array.


                                                        string basePath = "print-tran-log//" + "Birthday-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".jpg";
                                                        string sFile = GetPath("~//" + basePath);
                                                        System.IO.FileStream newFile = new System.IO.FileStream(sFile, System.IO.FileMode.Create);

                                                        //  string renderFormat = (filenameToSave.EndsWith(".xlsx") ? "EXCELOPENXML" : "Excel");
                                                        // byte[] bytes = reportViewer.LocalReport.Render("EXCELOPENXML", null, out contentType, out encoding, out extension, out streamIds, out warnings);
                                                        byte[] bytes = reportViewer.LocalReport.Render("Image", null, out contentType, out encoding, out extension, out streamIds, out warnings);
                                                        newFile.Write(bytes, 0, bytes.Length);
                                                        newFile.Close();

                                                        if (!attachFileColl.ContainsKey(vq.StudentId))
                                                            attachFileColl.Add(vq.StudentId, basePath);

                                                        if (!string.IsNullOrEmpty(vq.Email))
                                                        {
                                                            Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                                                            {
                                                                To = vq.Email,
                                                                Cc = templateEmail.EmailCC,
                                                                BCC = templateEmail.EmailBCC,
                                                                Subject = templateEmail.Title,
                                                                Message = tempMSG,
                                                                CUserId = UserId
                                                            };

                                                            glbFN.SendEMail(mail, null, sFile, true);
                                                        }


                                                        resVal.IsSuccess = true;
                                                        resVal.ResponseMSG = basePath;
                                                    }
                                                    else
                                                    {
                                                        Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                                                        _rdlReport.RenderType = "tif";
                                                        _rdlReport.NoShow = false;
                                                        _rdlReport.iReportLoadObjectData = new AcademicLib.PE.Academic.StudentBirthDay(studentDataColl);
                                                        _rdlReport.ReportFile = reportFilePath;

                                                        if (_rdlReport.Object != null)
                                                        {
                                                            string basePath = "print-tran-log//" + "Birthday-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".tif";
                                                            string sFile = GetPath("~//" + basePath);
                                                            reportTemplate.SavePDF(_rdlReport.Object, sFile);

                                                            if (!attachFileColl.ContainsKey(vq.StudentId))
                                                                attachFileColl.Add(vq.StudentId, basePath);

                                                            if (!string.IsNullOrEmpty(vq.Email))
                                                            {
                                                                Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                                                                {
                                                                    To = vq.Email,
                                                                    Cc = templateEmail.EmailCC,
                                                                    BCC = templateEmail.EmailBCC,
                                                                    Subject = templateEmail.Title,
                                                                    Message = tempMSG,
                                                                    CUserId = UserId
                                                                };

                                                                glbFN.SendEMail(mail, null, sFile, true);
                                                            }

                                                            resVal.IsSuccess = true;
                                                            resVal.ResponseMSG = basePath;
                                                        }
                                                        else
                                                        {
                                                            string str = "";
                                                            foreach (var err in _rdlReport.Errors)
                                                            {
                                                                str = str + err.ToString() + "\n";
                                                            }
                                                            str = str.Replace("'", "");
                                                            resVal.IsSuccess = false;
                                                            resVal.ResponseMSG = str;
                                                        }
                                                    }

                                                }
                                            }

                                        }


                                    }

                                    steps =steps+ " Email Sended";
                                }
                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Email Template Not Found";
                            }
                        }
                        catch (Exception eee)
                        {
                            steps = steps + "Email :" + eee.Message;
                        }


                    }
                    #endregion

                    #region " Send Notification"
                    {
                        try
                        {
                            var templatesColl = new AcademicLib.BL.Setup.SENT(UserId, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.STUDENT_BIRTHDAY, 3, 3);
                            if (templatesColl != null && templatesColl.Count > 0)
                            {
                                var templateNotifiation = templatesColl[0];
                                PivotalERP.Global.GlobalFunction glbFN = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                                System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = glbFN.GetPropertyInfos(typeof(AcademicLib.RE.Academic.StudentBirthDay), templateNotifiation.Description);
                                foreach (var vq in tmpDataColl)
                                {
                                    if (vq.UserId > 0)
                                    {
                                        string tempMSG = templateNotifiation.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", glbFN.GetProperty(vq, field.Name).ToString());
                                        }
                                        vq.Template = tempMSG;

                                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                                        var findAtt = "";

                                        try
                                        {
                                            findAtt = attachFileColl[vq.StudentId];
                                        }
                                        catch { }
                                        notification.Content = tempMSG;
                                        notification.ContentPath = (string.IsNullOrEmpty(findAtt) ? "" : findAtt);
                                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIRTHDAY);
                                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.BIRTHDAY.ToString();
                                        notification.Heading = templateNotifiation.Title;
                                        notification.Subject = templateNotifiation.Title;
                                        notification.UserId = UserId;
                                        notification.UserName = User.Identity.Name;
                                        notification.UserIdColl = vq.UserId.ToString();

                                        glbFN.SendNotification(UserId, notification, true);
                                    }
                                }

                                steps = steps + " Notification Sended";
                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "Notification Template Not Found";
                            }
                        }
                        catch (Exception eee)
                        {
                            steps = steps + "Notification :" + eee.Message;
                        }
                    }
                    #endregion

                    #region " Send SMS"
                    {
                        try
                        {
                            var templatesColl = new AcademicLib.BL.Setup.SENT(UserId, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.STUDENT_BIRTHDAY, 3, 1);
                            if (templatesColl != null && templatesColl.Count > 0)
                            {
                                var templateSMS = templatesColl[0];
                                PivotalERP.Global.GlobalFunction glbFN = new PivotalERP.Global.GlobalFunction(UserId, hostName, dbName, GetBaseUrl);
                                System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = glbFN.GetPropertyInfos(typeof(AcademicLib.RE.Academic.StudentBirthDay), templateSMS.Description);
                                List<AcademicLib.BE.Global.StudentSMS> smsList = new List<AcademicLib.BE.Global.StudentSMS>();
                                foreach (var vq in tmpDataColl)
                                {
                                    if (!string.IsNullOrEmpty(vq.ContactNo))
                                    {
                                        string tempMSG = templateSMS.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", glbFN.GetProperty(vq, field.Name).ToString());
                                        }
                                        vq.Template = tempMSG;
                                        smsList.Add(new AcademicLib.BE.Global.StudentSMS()
                                        {
                                            StudentId = vq.StudentId,
                                            Message = tempMSG,
                                            ContactNo = vq.ContactNo,
                                            EntityId = entityId,
                                            StudentName = vq.Name,
                                            Title = "BirthDay",
                                            UserId = vq.UserId
                                        });
                                    }
                                }
                                sendSMS(smsList);

                                steps = steps + " SMS Sended";
                            }
                            else
                            {
                                resVal.IsSuccess = false;
                                resVal.ResponseMSG = "SMS Template Not Found";
                            }
                        }
                        catch (Exception eee)
                        {
                            steps = steps + "SMS :" + eee.Message;
                        }
                    }
                    #endregion
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "No Data Found";
                }

                resVal.ResponseMSG = resVal.ResponseMSG + steps;


            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message+" "+steps;

            }

            return resVal;
        }
        [Obsolete]
        private ResponeValues sendSMS(List<AcademicLib.BE.Global.StudentSMS> dataColl)
        { 
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (dataColl != null)
                {
                    var apiData = System.Configuration.ConfigurationManager.AppSettings["smsAPI"];
                    string smsAPI = (apiData != null ? apiData.ToString() : "");
                    if (!string.IsNullOrEmpty(smsAPI))
                    {
                        int count = 0;
                        var smsFN = new AcademicERP.Global.SMSFunction();
                        foreach (var beData in dataColl)
                        {
                            if (!string.IsNullOrEmpty(beData.ContactNo) && !string.IsNullOrEmpty(beData.Message))
                            {
                                try
                                {
                                    SMSLog sms = new SMSLog();
                                    sms.Message = beData.Message;
                                    sms.StudentId = beData.StudentId.ToString() + ":" + beData.UserId.ToString();
                                    sms.PhoneNo = beData.ContactNo.Trim();
                                    if (sms.PhoneNo.Length >= 10)
                                    {
                                        var sRes = smsFN.SendSMS(sms, smsAPI, hostName, dbName);
                                        if (!sRes.IsSuccess)
                                        {
                                            if (sRes.ResponseMSG.Contains("Credit Balance"))
                                            {
                                                resVal.ResponseMSG = sRes.ResponseMSG;
                                                resVal.IsSuccess = false;                                                
                                            }
                                        }
                                        else
                                            count++;
                                    }
                                }
                                catch (Exception eee)
                                {
                                    resVal.IsSuccess = false;
                                    resVal.ResponseMSG = eee.Message;
                                }
                            }
                        }
                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = "SMS Send Success (" + count.ToString() + ")";
                    }
                    else
                    {
                        AcademicERP.Global.SMSFunction smsFN = new AcademicERP.Global.SMSFunction();
                        SMSServer smsUser = smsFN.GetSMSUser();
                        resVal = smsFN.IsValidSMSUser(ref smsUser);

                        int count = 0;
                        if (resVal.IsSuccess)
                        {                         
                            foreach (var beData in dataColl)
                            {
                                if (!string.IsNullOrEmpty(beData.ContactNo) && !string.IsNullOrEmpty(beData.Message))
                                {
                                    try
                                    {
                                        SMSLog sms = new SMSLog();
                                        sms.Message = beData.Message;
                                        sms.StudentId = beData.StudentId.ToString() + ":" + beData.UserId.ToString();
                                        sms.PhoneNo = beData.ContactNo.Trim();
                                        if (sms.PhoneNo.Length >= 10)
                                        {
                                            var sRes = smsFN.SendSMS(sms, smsUser);
                                            if (!sRes.IsSuccess)
                                            {
                                                if (sRes.ResponseMSG.Contains("Credit Balance"))
                                                {
                                                    resVal.ResponseMSG = sRes.ResponseMSG;
                                                    resVal.IsSuccess = false;                                                   
                                                }
                                            }
                                            else
                                                count++;
                                        }
                                    }
                                    catch (Exception eee)
                                    {                                        
                                        resVal.IsSuccess = false;
                                        resVal.ResponseMSG = eee.Message;
                                    }
                                }
                            }
                            smsFN.closeConnection();
                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = "SMS Send Success (" + count.ToString() + ")";
                        }
                    } 
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message ;
            }

            return resVal;
        }
    }
}