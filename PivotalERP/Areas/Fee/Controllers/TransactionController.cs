using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.Fee.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {
        public ActionResult ConcessionWaivers()
        {
            return View();
        }
        // GET: Fee/Transaction
        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeReceipt, false)]
        public ActionResult FeeReceipt()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeReturn, false)]
        public ActionResult FeeReturn()
        {
            return View();
        }


        #region "Sibling FeeReceipt"
        // GET: Fee/Transaction
        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeReceipt, false)]
        public ActionResult SiblingFeeReceipt()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetDuesForSiblingReceipt(int StudentId, int? PaidUpToMonth)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt resVal = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getSiblingDuesDetails(this.AcademicYearId, StudentId, PaidUpToMonth);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "FeeReceipt"

        [HttpPost]
        public JsonNetResult GetAutoNoOfFeeReceipt(AcademicLib.BE.Fee.Transaction.FeeReceiptNo beData)
        {
            AcademicLib.BE.Fee.Transaction.FeeReceiptNo resVal = new AcademicLib.BE.Fee.Transaction.FeeReceiptNo();
            try
            {
                beData.AcademicYearId = this.AcademicYearId;
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getAutoNo(beData);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetDuesForReceipt(int StudentId,int? PaidUpToMonth, string PaidUpMonthColl=null,int? SemesterId=null,int? ClassYearId=null,int? BatchId=null)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt resVal = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getDuesDetails(this.AcademicYearId,StudentId, PaidUpToMonth,PaidUpMonthColl,SemesterId,ClassYearId,BatchId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDuesForAdmission(int ClassId,int StudentId, int PaidUpToMonth, int? SemesterId, int? ClassYearId,int? RouteId,int? PointId,int? BedId)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt resVal = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getDuesForAdmission(this.AcademicYearId,ClassId, StudentId, PaidUpToMonth, SemesterId, ClassYearId,RouteId,PointId,BedId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FeeReceipt, false)]
        public JsonNetResult SaveFeeReceipt()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var usr = this.User;
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Transaction.FeeReceipt>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = usr.UserId;

                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    else if(beData.TranId>0)
                        isModify = true;

                    beData.AcademicYearId = this.AcademicYearId;

                    var feeReceiptBL = new AcademicLib.BL.Fee.Transaction.FeeReceipt(usr.UserId, usr.HostName, usr.DBName);
                    resVal = feeReceiptBL.SaveFormData(this.AcademicYearId, beData,false);

                    if(resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId =(isModify ? beData.TranId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.FeeReceipt;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Fee Receipt Modify" + beData.ReceivedAmt.ToString("N") : "New Fee Receipt :-"+beData.ReceivedAmt.ToString("N"));
                        auditLog.AutoManualNo =beData.AutoVoucherNo.ToString();
                        SaveAuditLog(auditLog);

                        if (!isModify)
                        {
                            var newBeData = feeReceiptBL.getFeeForSMS(this.AcademicYearId, resVal.RId);
                            if(newBeData!=null && !string.IsNullOrEmpty(newBeData.ReceiptNo) && !string.IsNullOrEmpty(newBeData.UserId) )
                            {
                                var templatesColl = new AcademicLib.BL.Setup.SENT(User.UserId, User.HostName, User.DBName).GetSENT((int)ENTITIES.FeeReceipt, 3, 3);

                                PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName);
                                if (templatesColl != null && templatesColl.Count > 0)
                                {
                                    #region "Send Notification"

                                    AcademicLib.BE.Setup.SENT templateNotifiation = templatesColl[0];
                                    if (templateNotifiation != null)
                                    {
                                        System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT), templateNotifiation.Description);
                                        string tempMSG = templateNotifiation.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(newBeData, field.Name).ToString());
                                        }

                                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                        notification.Content = tempMSG;
                                        notification.ContentPath = "";
                                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.FEE_RECEIPT);
                                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.FEE_RECEIPT.ToString();
                                        notification.Heading = templateNotifiation.Title;
                                        notification.Subject = templateNotifiation.Title;
                                        notification.UserId = User.UserId;
                                        notification.UserName = User.Identity.Name;
                                        notification.UserIdColl = newBeData.UserId;

                                         new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification, true);
                                    }
                                    

                                    #endregion
                                }


                                templatesColl = new AcademicLib.BL.Setup.SENT(User.UserId, User.HostName, User.DBName).GetSENT((int)ENTITIES.FeeReceipt, 3, 1);
                                
                                if (templatesColl != null && templatesColl.Count > 0)
                                {
                                    #region "Send SMS"

                                    AcademicLib.BE.Setup.SENT templateNotifiation = templatesColl[0];
                                    if (templateNotifiation != null)
                                    {
                                        System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.BE.Fee.Transaction.FeeReceipt_SENT), templateNotifiation.Description);
                                        string tempMSG = templateNotifiation.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(newBeData, field.Name).ToString());
                                        }

                                        if (!string.IsNullOrEmpty(newBeData.ContactNo) && (newBeData.ContactNo.Length==10 || newBeData.ContactNo.Length==13) && tempMSG.Length>0)
                                        {
                                            if (smsAPIUsed)
                                            {
                                                try
                                                {
                                                  var smsResVal=  new Global.GlobalFunction(usr.UserId, usr.HostName, usr.DBName).SendSMS(newBeData.ContactNo, tempMSG, true);                                                   
                                                    if(!smsResVal.IsSuccess)
                                                    {
                                                        resVal.IsSuccess = true;
                                                        resVal.ResponseMSG = "Unable To Send SMS";
                                                    }
                                                }
                                                catch (Exception eee)
                                                {
                                                    resVal.ResponseMSG = eee.Message;

                                                    return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                                                }
                                            }
                                            else
                                            {
                                                AcademicERP.Global.SMSFunction smsFN = new AcademicERP.Global.SMSFunction();
                                                SMSServer smsUser = smsFN.GetSMSUser();
                                                var smsResVal = smsFN.IsValidSMSUser(ref smsUser);

                                                int count = 0;
                                                if (smsResVal.IsSuccess)
                                                {
                                                    try
                                                    {
                                                        SMSLog sms = new SMSLog();
                                                        sms.Message = tempMSG;
                                                        sms.StudentId = beData.StudentId.ToString();
                                                        sms.PhoneNo = newBeData.ContactNo;
                                                        var sRes = smsFN.SendSMS(sms, smsUser);
                                                        if (!sRes.IsSuccess)
                                                        {
                                                            if (sRes.ResponseMSG.Contains("Credit Balance"))
                                                            {
                                                                resVal.ResponseMSG = sRes.ResponseMSG;
                                                                return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                                                            }
                                                        }
                                                        else
                                                            count++;
                                                    }
                                                    catch (Exception eee)
                                                    {
                                                        resVal.ResponseMSG = eee.Message;

                                                        return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                                                    }
                                                    smsFN.closeConnection();
                                                }
                                            }
                                           

                                        }
                                        else
                                        {
                                            resVal.ResponseMSG = "Blank Data Can't be Accept";
                                        }

                                    }


                                    #endregion
                                }
                            }
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
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost] 
        public JsonNetResult FeeReceiptEmail(int TranId)
        {
            ResponeValues resVal = new ResponeValues();           
            try
            {
                var usr = User;
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(usr.UserId, usr.HostName, usr.DBName).SendEmail(TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.FeeReceipt, false)]
        public JsonNetResult CancelFeeReceipt(AcademicLib.BE.Fee.Transaction.FeeReceipt beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {                
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).Cancel(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = beData.TranId.Value;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.FeeReceipt;
                        auditLog.Action =  Actions.Modify;
                        auditLog.LogText = "Cancel Fee Receipt";
                        auditLog.AutoManualNo = beData.TranId.Value.ToString();
                        SaveAuditLog(auditLog);
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
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllFeeReceiptList()
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).GetAllFeeReceipt(this.AcademicYearId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.FeeReceipt, false)]
        public JsonNetResult GetFeeReceiptById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).GetFeeReceiptById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.FeeReceipt, false)]
        public JsonNetResult DelFeeReceipt(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFeeReceiptCollection(DateTime? dateFrom,DateTime? dateTo,bool showCancel,int? fromReceipt,int? toReceipt)
        {
            double openingAmt = 0, openingDisAmt = 0;
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getFeeReceiptCollection(this.AcademicYearId, dateFrom, dateTo, showCancel,fromReceipt,toReceipt,ref openingAmt,ref openingDisAmt);

            var retVal = new
            {
                IsSuccess=dataColl.IsSuccess,
                ResponseMSG=dataColl.ResponseMSG,
                OpeningAmt=openingAmt,
                OpeningDisAmt=openingDisAmt,
                CurrentAmt=dataColl.Sum(p1=>p1.ReceivedAmt),
                CurrentDisAmt=dataColl.Sum(p1=>p1.DiscountAmt),
                DataColl=dataColl
            };

            return new JsonNetResult() { Data = retVal, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetOnlinePaymentList(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!dateFrom.HasValue)
                dateFrom = DateTime.Today;

            if (!dateTo.HasValue)
                dateTo = DateTime.Today;

            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getOnlinePaymentList(this.AcademicYearId, dateFrom.Value, dateTo.Value);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintFeeReceiptColl()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Fee.FeeReceipt> paraData = DeserializeObject<List<AcademicLib.RE.Fee.FeeReceipt>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "FeeReturn"

        [HttpPost]
        public JsonNetResult GetAutoNoOfFeeReturn(AcademicLib.BE.Fee.Transaction.FeeReceiptNo beData)
        {
            AcademicLib.BE.Fee.Transaction.FeeReceiptNo resVal = new AcademicLib.BE.Fee.Transaction.FeeReceiptNo();
            try
            {
                beData.AcademicYearId = this.AcademicYearId;
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReturn(User.UserId, User.HostName, User.DBName).getAutoNo(beData);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetDuesForReturn(int StudentId, int? PaidUpToMonth, string PaidUpMonthColl = null, int? SemesterId = null, int? ClassYearId = null)
        {
            AcademicLib.BE.Fee.Transaction.StudentFeeReceipt resVal = new AcademicLib.BE.Fee.Transaction.StudentFeeReceipt();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReturn(User.UserId, User.HostName, User.DBName).getDuesDetails(this.AcademicYearId, StudentId, PaidUpToMonth, PaidUpMonthColl, SemesterId, ClassYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

       
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FeeReturn, false)]
        public JsonNetResult SaveFeeReturn()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var usr = this.User;

                var beData = DeserializeObject<AcademicLib.BE.Fee.Transaction.FeeReceipt>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = usr.UserId;

                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    else if (beData.TranId > 0)
                        isModify = true;

                    beData.AcademicYearId = this.AcademicYearId;

                    var feeReceiptBL = new AcademicLib.BL.Fee.Transaction.FeeReturn(usr.UserId, usr.HostName, usr.DBName);
                    resVal = feeReceiptBL.SaveFormData(this.AcademicYearId, beData, false);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.FeeReturn;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Fee Return Modify" + beData.ReceivedAmt.ToString("N") : "New Fee Return :-" + beData.ReceivedAmt.ToString("N"));
                        auditLog.AutoManualNo = beData.AutoVoucherNo.ToString();
                        SaveAuditLog(auditLog);
                         

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
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

  

        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.FeeReturn, false)]
        public JsonNetResult CancelFeeReturn(AcademicLib.BE.Fee.Transaction.FeeReceipt beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Fee.Transaction.FeeReturn(User.UserId, User.HostName, User.DBName).Cancel(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = beData.TranId.Value;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.FeeReturn;
                        auditLog.Action = Actions.Modify;
                        auditLog.LogText = "Cancel Fee Return";
                        auditLog.AutoManualNo = beData.TranId.Value.ToString();
                        SaveAuditLog(auditLog);
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
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

       

        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.FeeReturn, false)]
        public JsonNetResult GetFeeReturnById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReturn(User.UserId, User.HostName, User.DBName).GetFeeReceiptById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.FeeReturn, false)]
        public JsonNetResult DelFeeReturn(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReturn(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFeeReturnCollection(DateTime? dateFrom, DateTime? dateTo, bool showCancel, int? fromReceipt, int? toReceipt)
        {
            double openingAmt = 0, openingDisAmt = 0;
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReturn(User.UserId, User.HostName, User.DBName).getFeeReceiptCollection(this.AcademicYearId, dateFrom, dateTo, showCancel, fromReceipt, toReceipt, ref openingAmt, ref openingDisAmt);

            var retVal = new
            {
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG,
                OpeningAmt = openingAmt,
                OpeningDisAmt = openingDisAmt,
                CurrentAmt = dataColl.Sum(p1 => p1.ReceivedAmt),
                CurrentDisAmt = dataColl.Sum(p1 => p1.DiscountAmt),
                DataColl = dataColl
            };

            return new JsonNetResult() { Data = retVal, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

       

        [HttpPost]
        public JsonNetResult PrintFeeReturnColl()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Fee.FeeReceipt> paraData = DeserializeObject<List<AcademicLib.RE.Fee.FeeReceipt>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeSetup, false)]
        public ActionResult Setup()
        {
            return View();
        }

        #region "Fee Configuration"

        [HttpPost]
        public JsonNetResult SaveFeeConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Setup.FeeConfiguration>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFeeConfiguration()
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).GetFeeConfigurationById(0,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }




        #endregion

        #region "Billing Configuration"

        [HttpPost,ValidateInput(false)]
        public JsonNetResult SaveBillConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Setup.BillConfiguration>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Fee.Setup.BillConfiguration(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBillConfiguration()
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.BillConfiguration(User.UserId, User.HostName, User.DBName).GetBillConfigurationById(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }




        #endregion

        [HttpPost]
        public JsonNetResult GenerateCostCenter()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).GenerateStudentCostCenter();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GenerateCostCenterEmp()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).GenerateEmployeeCostCenter();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GenerateFeeReceiptToJournal(bool IsReGenerate)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).GenerateFeeReceiptToJournal(this.AcademicYearId,IsReGenerate);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCostCenterGenLog()
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getCostCenterGenerateLog();

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult MergeStudentOpening(int FeeItemId)
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).MergeStudentOpening(this.AcademicYearId, FeeItemId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GenerateSalesInvoiceOfBill(bool IsReGenerate,DateTime? DateFrom,DateTime? DateTo)
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).GenerateSalesInvoice(this.AcademicYearId, IsReGenerate,DateFrom,DateTo);

            if (dataColl.IsSuccess)
            {
                var dataColl1 = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).UpdateMissingSalesInvoice();
                if (dataColl1.IsSuccess == false)
                {
                    dataColl.IsSuccess = false;
                    dataColl.ResponseMSG = dataColl1.ResponseMSG;
                }
            }            
            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult UpdateMissingLeftStudent(int FromAcademicYearId,int ToAcademicYearId)
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).UpdateMissingLeftStudent(FromAcademicYearId, ToAcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult UpdateMissingSalesInvoice()
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).UpdateMissingSalesInvoice();

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult MissingFeeToAccount(DateTime? DateFrom, DateTime? DateTo)
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FeeConfiguration(User.UserId, User.HostName, User.DBName).MissingFeeToAccount(DateFrom, DateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


    }
}