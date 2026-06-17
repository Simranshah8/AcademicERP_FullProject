using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using AcademicLib.BE.Global;
using Newtonsoft.Json;

namespace PivotalERP.Areas.Account.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {

        #region "Ledger Merge"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.LedgerMerge)]
        public ActionResult LedgerMerge()
        {
            return View();
        }

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.LedgerMerge)]
        [HttpPost]
        public JsonNetResult LedgerMerge(int fromLedgerId, int toLedgerId)
        {
            var retVal = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName).FromLedgerToLedger(User.UserId, fromLedgerId, toLedgerId);

            return new JsonNetResult() { Data = retVal, TotalCount = 1, IsSuccess = retVal.IsSuccess, ResponseMSG = retVal.ResponseMSG };
        }

        #endregion

        #region "Receipt"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Receipt, false)]
        public ActionResult Receipt(int? tranId = null)
        {
            if (tranId.HasValue)
                ViewBag.TranId = tranId;
            else
                ViewBag.TranId = 0;

            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Receipt);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Receipt);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Receipt, false)]
        public ActionResult CashDeposite()
        {
            ViewBag.TranId = 0;
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Receipt);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Receipt);
            return View();

        }


        [HttpPost]
        public JsonNetResult SaveUpdateReceipt()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Receipt);
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.Receipt);
                    //    if (!sapLastV.IsSuccess)
                    //    {
                    //        return new JsonNetResult() { Data = sapLastV, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}

                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/account/receipt", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = tranBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = tranBL.SaveFormData(beData);
                    }


                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Receipt;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.Amount.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.Amount.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.AutoManualNo);
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
        public JsonNetResult GetReceiptById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Receipt);
            var beData = tranBL.getJournalByTranId(tranId, User.UserId);


            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion

        #region "Payment"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Payment, false)]
        public ActionResult Payment(int? tranId = null)
        {
            if (tranId.HasValue)
                ViewBag.TranId = tranId;
            else
                ViewBag.TranId = 0;

            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Payment);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Payment);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Payment, false)]
        public ActionResult CashRefund()
        {
            ViewBag.TranId = 0;
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Payment);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Payment);
            return View();

        }

        [HttpPost]
        public JsonNetResult SaveUpdatePayment()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Payment);
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.Payment);
                    //    if (!sapLastV.IsSuccess)
                    //    {
                    //        return new JsonNetResult() { Data = sapLastV, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}
                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/account/payment", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = tranBL.ModifyFormData(beData);
                    }
                    else
                        resVal = tranBL.SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Payment;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify" + beData.Amount.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.Amount.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.AutoManualNo);
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
        public JsonNetResult GetPaymentById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Payment);
            var beData = tranBL.getJournalByTranId(tranId, User.UserId);


            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion

        #region "Journal"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Journal, false)]
        public ActionResult Journal(int? tranId = null)
        {
            if (tranId.HasValue)
                ViewBag.TranId = tranId;
            else
                ViewBag.TranId = 0;

            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Journal);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Journal);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Journal, false)]
        public ActionResult GovernmentJournal(int? tranId = null)
        {
            if (tranId.HasValue)
                ViewBag.TranId = tranId;
            else
                ViewBag.TranId = 0;

            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Journal);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Journal);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Journal, false)]
        public ActionResult PatientDrCr()
        {
            ViewBag.TranId = 0;
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Journal);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Journal);
            return View();

        }

        [HttpPost]
        public JsonNetResult SaveUpdateJournal()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(Request["jsonData"]);
                if (beData != null)
                {
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/account/Journal", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal).ModifyFormData(beData);
                    }
                    else
                        resVal = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Journal;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify" + beData.Amount.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.Amount.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.AutoManualNo);
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
        public JsonNetResult GetJournalById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal);
            var beData = tranBL.getJournalByTranId(tranId, User.UserId);


            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion

        #region "Contra"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Contra, false)]
        public ActionResult Contra(int? tranId = null)
        {
            if (tranId.HasValue)
                ViewBag.TranId = tranId;
            else
                ViewBag.TranId = 0;

            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Contra);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.Contra);
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveUpdateContra()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Account.Transaction.Journal>(Request["jsonData"]);
                if (beData != null)
                {
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/account/Contra", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Contra).ModifyFormData(beData);
                    }
                    else
                        resVal = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Contra).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Contra;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify" + beData.Amount.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.Amount.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.AutoManualNo);
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
        public JsonNetResult GetContraById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Contra);
            var beData = tranBL.getJournalByTranId(tranId, User.UserId);


            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion


        #region "Journal"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Journal, false)]
        public ActionResult AdditionalJournal(int? tranId = null)
        {
            if (tranId.HasValue)
                ViewBag.TranId = tranId;
            else
                ViewBag.TranId = 0;

            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseAdditionalInvoice);
            ViewBag.EntityId = Convert.ToInt32(FormsEntity.PurchaseAdditionalInvoice);
            return View();
        }

        [HttpPost]
        public JsonNetResult GetProductModelLstJV(string tranIdColl)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseInvoice(User.HostName, User.DBName).getProductProductGroupModeList(User.UserId, tranIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion
    }
}