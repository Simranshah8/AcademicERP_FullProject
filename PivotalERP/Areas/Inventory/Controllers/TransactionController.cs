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

namespace PivotalERP.Areas.Inventory.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {

       

        [HttpGet]
        public JsonNetResult getPurchaseDuesBillList(int ledgerId)
        {

            Dynamic.ReportEntity.Account.PartyWiseDuesBillListCollections dataColl = new Dynamic.ReportEntity.Account.PartyWiseDuesBillListCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.PurchaseInvoiceDB(User.HostName, User.DBName).getAllDuesBillList(ledgerId, User.UserId,null,false,true);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult getDuesBillList(int ledgerId)
        {

            Dynamic.ReportEntity.Account.PartyWiseDuesBillListCollections dataColl = new Dynamic.ReportEntity.Account.PartyWiseDuesBillListCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesAllotmentCancelDB(User.HostName, User.DBName).getAllDuesBillList(ledgerId, User.UserId,null,false,null);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Sales Quotation"

        [HttpGet]
        public JsonNetResult getPendinSalesQuotation(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingQuotation(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSalesAllotmentDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                }

                new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getSalesAllotmentDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetSalesQuotaionDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                }
                beData.CUserId = User.UserId;
                new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getSalesQuotationDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getSalesQuotationPartyDetails(int tranId)
        {

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.SalesQuotation tranBeData = new Dynamic.BusinessEntity.Inventory.Transaction.SalesQuotation();
            try
            {
                tranBeData.TranId = tranId;
                tranBeData.CUserId = User.UserId;

                tranBeData = new Dynamic.DataAccess.Inventory.Transaction.SalesQuotationDB(User.HostName, User.DBName).getPartyDetailsByRefNo(User.UserId, 0, "", tranId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = tranBeData, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Sales Order"

        [HttpGet]
        public JsonNetResult getPendinSalesOrder(int ledgerId, int? agentId, DateTime? voucherDate, int? orderTranId = null)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingOrder(User.UserId, ledgerId, agentId.Value, voucherDate.Value, true, orderTranId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getSalesOrderPartyDetails(int tranId)
        {

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.SalesOrder tranBeData = new Dynamic.BusinessEntity.Inventory.Transaction.SalesOrder();
            try
            {
                tranBeData.TranId = tranId;
                tranBeData.CUserId = User.UserId;

                tranBeData = new Dynamic.DataAccess.Inventory.Transaction.SalesOrderDB(User.HostName, User.DBName).getPartyDetailsByRefNo(User.UserId, 0, "", tranId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = tranBeData, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Sales Allotment"

        [HttpGet]
        public JsonNetResult getPendinSalesAllotment(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingSalesAllotment(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getPurchaseQuotationPartyDetails(int tranId)
        {

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PurchaseQuotation dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PurchaseQuotation();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.PurchaseQuotationDB(User.HostName, User.DBName).getPurchaseQuotationByTranId(tranId, User.UserId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getSalesAllotmentPartyDetails(int tranId)
        {

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoiceDetails dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoiceDetails();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesAllotmentDB(User.HostName, User.DBName).getSalesAllotmentDetails(User.UserId, 0, "", tranId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getDueFixedProductList(int productId)
        {

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.ItemDetailsCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.ItemDetailsCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName).getDueFixedProductList(User.UserId, productId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getSalesAllotmentStatus(int ledgerId)
        {

            Dynamic.ReportEntity.Inventory.AllotmentStatusCollections dataColl = new Dynamic.ReportEntity.Inventory.AllotmentStatusCollections();
            try
            {
                dataColl = new Dynamic.Reporting.Inventory.SalesAllotment(User.HostName, User.DBName).getAllotmentStatus(User.UserId, ledgerId);
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult UpdateAllotmentStatus(int ledgerId, string status)
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.DataAccess.Inventory.Transaction.SalesAllotmentDB(User.HostName, User.DBName).SaveUpdateAllotmentStatus(User.UserId, ledgerId, status);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSalesAllotmentById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotment(User.HostName, User.DBName);
            var beData = tranBL.getSalesInvoiceByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesInvoiceDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "Delivery Note"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult PetrolPumpDelivery()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.DeliveryNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.DeliveryNote);
            return View();
        }

        [HttpGet]
        public JsonNetResult getPendingDeliveryNote(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingDeliveryNote(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getDeliveryNotePartyDetails(int tranId)
        {

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.DeliveryNoteDB(User.HostName, User.DBName).getDeliveryNoteDetailsByRefNo(User.UserId, 0, "", tranId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Dispatch Section"

        [HttpGet]
        public JsonNetResult getPendingDispatchSection(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingDispatchSection(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getPendingDispatchOrder(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.DispatchSectionDB(User.HostName, User.DBName).getPendingDispatchOrder(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateDispatchSection()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.DispatchSection(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.DispatchSection>(Request["jsonData"]);
                if (beData != null)
                {

                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/deliverynote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.DispatchSection;
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
        public JsonNetResult GetDispatchSecionById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.DispatchSection(User.HostName, User.DBName);
            var beData = tranBL.getDispatchSectionByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getDispatchSectionDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "Sales Invoice"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult PetrolPump()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult TrailCenterCounterSI()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult TraiilCenterSI()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice);
            return View();
        }


        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult SalesInvoice()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice);
            //ViewBag.EntityDE = SerializedObject(new Dynamic.DataAccess.Setup.EntityWiseDisableButtonDB(User.HostName, User.DBName).getEntityWiseDisableButtonConfiguration(User.UserId, ViewBag.EntityId));

            return View();
        }



        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateSalesInvoice()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesInvoice(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
                    //    if (!sapLastV.IsSuccess)
                    //    {
                    //        return new JsonNetResult() { Data = sapLastV, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/sales", file));
                            }
                        }
                    }

                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);

                        try
                        {
                            if (resVal.RId > 0)
                                new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).SaveUpdateSalesLossToConsumption(User.UserId, false, resVal.RId, null);
                        }
                        catch { }
                    }

                    if (resVal.IsSuccess)
                    {
                        var recTranId = 0;
                        if (beData.SalesInvoiceDetail.ReceiptVoucherId.HasValue)
                        {
                            int.TryParse(resVal.ResponseId, out recTranId);
                        }
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
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
                 
                //resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetSalesVehilceListForAutoC()
        {
            var dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getVehilceListForAutoComplete(User.UserId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost]
        public JsonNetResult GetSalesInvoiceById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesInvoice(User.HostName, User.DBName);
            var beData = tranBL.getSalesInvoiceByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesInvoiceDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost]
        public JsonNetResult GetSalesInvoiceByVoucherNo(string voucherNo, bool ForReturn)
        {
            var tranBL = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName);
            var beData = tranBL.getSalesInvoiceByVoucherNo(voucherNo, ForReturn, User.UserId,0,0);
            beData.CUserId = User.UserId;

            if (beData.IsSuccess)
                tranBL.getSalesInvoiceDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }


        [HttpGet]
        public JsonNetResult getPendinSalesInvoice(int ledgerId, int? agentId, DateTime? voucherDate, string whereQry = "")
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesReturnDB(User.HostName, User.DBName).getPendingInvoice(User.UserId, ledgerId, agentId.Value, voucherDate.Value, whereQry);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSalesOrderDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote>(Request["jsonData"]);
                }
                beData.CUserId = User.UserId;
                new Dynamic.DataAccess.Inventory.Transaction.DeliveryNoteDB(User.HostName, User.DBName).getSalesOrderDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }



        [HttpPost]
        public JsonNetResult GetDeliveryNoteDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                }

                beData.CUserId = User.UserId;
                new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getDeliveryDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSalesDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn>(Request["jsonData"]);
                }

                beData.CUserId = User.UserId;

                new Dynamic.DataAccess.Inventory.Transaction.SalesReturnDB(User.HostName, User.DBName).getSalesDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult getCastPartyListForTxn()
        {
            ResponeValues resVal = new ResponeValues();
            List<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoiceDetails> dataColl = new List<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoiceDetails>();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getCashPartyListForTran(User.UserId);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Insurance"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult Insurance()
        {
            return View();
        }

        [HttpGet]
        public JsonNetResult getVehicleDetailsByEngNo(string engineNo)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.Insurance(User.HostName, User.DBName).getVehicleDetails(User.UserId, engineNo);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Insurance, false)]
        public JsonNetResult SaveUpdateInsurance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.Insurance>(Request["jsonData"]);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/insurance", file));
                            }
                        }
                    }

                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Insurance(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Insurance(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Insurance;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "Bank Quotation"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.BankQuotation, false)]
        public ActionResult BankQuotation()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.BankQuotation);
            return View();
        }


        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.BankQuotation, false)]
        public JsonNetResult SaveUpdateBankQuotation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.BankQuotation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.BranchId = User.BranchId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankQuotation(User.UserId,User.HostName, User.DBName).SaveFormData(beData);
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankQuotation(User.UserId, User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankQuotation(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.BankQuotation;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "Bank DO"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.BankDO, false)]
        public ActionResult BankDO()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.BankDO);
            return View();
        }

        [HttpGet]
        public JsonNetResult getBankQuotationByNo(string quotationNo)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.BankQuotation dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.BankQuotation();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.BankDODB(User.HostName, User.DBName).getBankQuotationByNo(User.UserId, quotationNo);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.BankDO, false)]
        public JsonNetResult SaveUpdateBankDO()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.BankDO>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/insurance", file));
                            }
                        }
                    }

                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.BranchId = User.BranchId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankDO(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankDO(User.UserId, User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankDO(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.BankDO;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "Bank Allotment"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.BankAllotment, false)]
        public ActionResult BankAllotment()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.BankAllotment);
            return View();
        }

        [HttpGet]
        public JsonNetResult getBankDOByRef(string doRefNo)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.BankDO dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.BankDO();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.BankAllotmentDB(User.HostName, User.DBName).getBankDOByDORefNo(User.UserId, doRefNo);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = resVal.IsSuccess ? 1 : 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.BankQuotation, false)]
        public JsonNetResult SaveUpdateBankAllotment()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.BankAllotment>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.BranchId = User.BranchId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankAllotment(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankAllotment(User.UserId, User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankAllotment(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.BankAllotment;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "Namsari"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Namsari, false)]
        public ActionResult Namsari()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Namsari);
            return View();
        }

        [HttpGet]
        public JsonNetResult getBankDOByEngNo(string engineNo)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.Namsari(User.UserId, User.HostName, User.DBName).getBankDOForByEngineNo(User.UserId, engineNo);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Namsari, false)]
        public JsonNetResult SaveUpdateNamsari()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.Namsari>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.BranchId = User.BranchId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Namsari(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Namsari(User.UserId, User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Namsari(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Namsari;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "Bank Pay Letter"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.BankPayLetter, false)]
        public ActionResult BankPayLetter()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.BankPayLetter);
            return View();
        }


        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.BankPayLetter, false)]
        public JsonNetResult SaveUpdateBankPayLetter()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.BankPayLetter>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/insurance", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.BranchId = User.BranchId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankPayLetter(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankPayLetter(User.UserId, User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.BankPayLetter(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.BankPayLetter;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        [HttpPost]
        public JsonNetResult GetADForRefTran(int TranId, Dynamic.BusinessEntity.Account.VoucherTypes voucherTypes)
        {
            Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice tran = new Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice();
            try
            {
                tran = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName).getAditionDetailsForRefTran(User.UserId, TranId, voucherTypes);

                return new JsonNetResult() { Data = tran, TotalCount = 1, IsSuccess = tran.IsSuccess, ResponseMSG = tran.ResponseMSG };
            }
            catch (Exception ee)
            {
                tran.IsSuccess = false;
                tran.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = tran.IsSuccess, ResponseMSG = tran.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetTransactionLst(int? voucherId, int? costClassId, int voucherType, TableFilter filter)
        {
            Dynamic.BusinessEntity.Inventory.Transaction.TransactionLstCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.TransactionLstCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.TransactionLstDB(User.HostName, User.DBName).getTransactionForPaging(voucherId, costClassId, "", (Dynamic.BusinessEntity.Account.VoucherTypes)voucherType, filter);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTransactionLstWithTranId(int? voucherId, int? costClassId, int voucherType, string tranIdColl, TableFilter filter)
        {
            Dynamic.BusinessEntity.Inventory.Transaction.TransactionLstCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.TransactionLstCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.TransactionLstDB(User.HostName, User.DBName).getTransactionForPaging(voucherId, costClassId, tranIdColl, (Dynamic.BusinessEntity.Account.VoucherTypes)voucherType, filter);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "StockTransfor"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.StockJournal, false)]
        public ActionResult StockTransfor()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.StockTransfor);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.StockTransfor);
            return View();
        }
        [HttpGet]
        public JsonNetResult GetUserWiseGodown()
        {
            Dynamic.BusinessEntity.Inventory.GodownCollections dataColl = new Dynamic.BusinessEntity.Inventory.GodownCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.Godown(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetProductDetails()
        {
            Dynamic.BusinessEntity.Inventory.ProductCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.Product(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetProductBrand()
        {
            Dynamic.BusinessEntity.Inventory.ProductBrandCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductBrandCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductBrand(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetCostCenter()
        {
            Dynamic.BusinessEntity.Account.CostCenterCollections dataColl = new Dynamic.BusinessEntity.Account.CostCenterCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Account.CostCenter(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStockTransforById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.StockTransfor(User.HostName, User.DBName);
            var beData = tranBL.getStockTransforByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getStockTransforDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        [HttpPost]
        public JsonNetResult SaveUpdateStockTransfor()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.StockTransfor>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.StockTransfor(User.HostName, User.DBName).SaveFormData(beData);
                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.StockTransfor(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.StockTransfor(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StockTransfor;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "PhysicalStock"

        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.PhysicalStock)]
        public ActionResult PhysicalStock()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PhysicalStock);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PhysicalStock);
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveUpdatePhysicalStock()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PhysicalStock>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PhysicalStock(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }



                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PhysicalStock(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PhysicalStock(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PhysicalStock;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "Consumption"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Consumption, false)]
        public ActionResult Consumption()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.Consumption);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.Consumption);
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveUpdateConsumption()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.StockTransfor>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }



                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Consumption;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Consumption, false)]
        public JsonNetResult DelShrinkageWorkingLoss()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (User.UserId == 1)
                    resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).DeleteShrinageWorkingLoss(User.UserId);
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Access denied";
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
        public JsonNetResult PendingSalesForConsumption(bool ReCalculate)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).SaveUpdateSalesLossToConsumption(User.UserId, ReCalculate, null, null);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Configuration;
                    auditLog.Action = Actions.Save;
                    auditLog.LogText = "SalesInvoice To Consumption";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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
        public JsonNetResult GetConsumptionById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName);
            var beData = tranBL.getConsumptionByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getConsumptionDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "Stock Demand"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Consumption, false)]
        public ActionResult StockDemand()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PartsDemand);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PartsDemand);
            return View();

        }
        [HttpPost]
        public JsonNetResult SaveUpdateStockDemand()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.StockTransfor>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/sales", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PartsDemand(User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PartsDemand(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PartsDemand(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PartsDemand;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPartsDemandById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PartsDemand(User.HostName, User.DBName);
            var beData = tranBL.getStockTransforByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getStockTransforDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "ManufacturingStockJournal"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.StockJournal, false)]
        public ActionResult ManufacturingStockJournal()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.ManufacturingStockJournal);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.ManufacturingStockJournal);
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveUpdateManufacturingStockJournal()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.ManufacturingStockJournal>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/sales", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ManufacturingStockJournal(User.HostName, User.DBName).SaveFormData(beData);
                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ManufacturingStockJournal(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ManufacturingStockJournal(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManufacturingStockJournal;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        #endregion

        #region "CannibalizeIn"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.StockJournal, false)]
        public ActionResult CannibalizeIn()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.CannibalizeIn);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.CannibalizeIn);
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveUpdateCannibalizeIn()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.CannibalizeIn>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeIn(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeIn(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeIn(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.CannibalizeIn;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetCannibalizeInById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeIn(User.HostName, User.DBName);
            var beData = tranBL.getCannibalizeInByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getCannibalizeInDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion
        #region "CannibalizeOut"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.StockJournal, false)]
        public ActionResult CannibalizeOut()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.CannibalizeOut);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.CannibalizeOut);
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveUpdateCannibalizeOut()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.CannibalizeIn>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/sales", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeOut(User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeOut(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeOut(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.CannibalizeOut;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetCannibalizeOutById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.CannibalizeOut(User.HostName, User.DBName);
            var beData = tranBL.getCannibalizeInByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getCannibalizeInDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion

        #region "StockJournal"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.StockJournal, false)]
        public ActionResult StockJournal()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.StockJournal);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.StockJournal);
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveUpdateStockJournal()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.StockJournal>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.StockJournal);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.StockJournal(User.HostName, User.DBName);
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.StockJournal(User.HostName, User.DBName).SaveFormData(beData);
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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StockJournal;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetStockJournalById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.StockJournal(User.HostName, User.DBName);
            var beData = tranBL.getStockJournalByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getStockJournalDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "SalesProjection"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesQuotation, false)]
        public ActionResult SalesProjection()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesQuotation);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesQuotation);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesQuotation, false)]
        public JsonNetResult SaveUpdateSalesProjection()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesQuotation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesQuotation;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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

        #endregion

        #region "SalesDeliveryNote"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.DeliveryNote, false)]
        public ActionResult SalesDeliveryNote()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.DeliveryNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.DeliveryNote);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.DeliveryNote, false)]
        public JsonNetResult SaveUpdateDeliveryNote()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.DeliveryNote(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.DeliveryNote);
                    //    if (!sapLastV.IsSuccess)
                    //    {
                    //        return new JsonNetResult() { Data = sapLastV, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/deliverynote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.DeliveryNote;
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
        public JsonNetResult GetDeliveryNoteById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.DeliveryNote(User.HostName, User.DBName);
            var beData = tranBL.getDeliveryNoteByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getDeliveryNoteDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "SalesDeliveryNoteReturn"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.DeliveryNote, false)]
        public ActionResult SalesDeliveryNoteReturn()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.ReceivedChallan);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.ReceivedChallan);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.DeliveryNote, false)]
        public JsonNetResult SaveSalesDeliveryNoteReturn()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.ReceivedChallan>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ReceivedChallan(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ReceivedChallan(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ReceivedChallan(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ReceivedChallan;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetDeliveryNoteReturnById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.ReceivedChallan(User.HostName, User.DBName);
            var beData = tranBL.getReceivedChallanByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getReceivedChallanDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion

        #region "Sales Quotation"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesQuotation, false)]
        public ActionResult SalesQuotation()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesQuotation);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesQuotation);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesQuotation, false)]
        public JsonNetResult SaveUpdateSalesQuotation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesQuotation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesQuotation;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetSalesQuotationById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesQuotation(User.HostName, User.DBName);
            var beData = tranBL.getSalesQuotationByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesQuotationDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        //[HttpGet]
        //public JsonNetResult getPendinSalesQuotation(int ledgerId, int? agentId, DateTime? voucherDate)
        //{
        //    if (!agentId.HasValue)
        //        agentId = 0;

        //    if (!voucherDate.HasValue)
        //        voucherDate = DateTime.Today;

        //    ResponeValues resVal = new ResponeValues();
        //    Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
        //    try
        //    {
        //        dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingQuotation(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
        //        resVal.IsSuccess = true;
        //        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }

        //    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}

        #endregion
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesOrder, false)]
        public ActionResult SalesOrderCancel()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesOrderCancel);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesOrderCancel);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesOrder, false)]
        public JsonNetResult SaveUpdateSalesOrderCancel()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesOrder>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrder(User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrderCancel(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrderCancel(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesOrderCancel;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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


        [PermissionsAttribute(Actions.View, (int)ENTITIES.DeliveryNote, false)]
        public ActionResult DispatchSection()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.DispatchSection);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.DispatchSection);
            return View();
        }

        #region "Dispatch Order"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult DispatchOrder()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.DispatchOrder);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.DispatchOrder);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateDispatchOrder()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.DispatchOrder(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.DispatchOrder>(Request["jsonData"]);
                if (beData != null)
                {

                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/deliverynote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.DispatchOrder;
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
        public JsonNetResult GetDispatchOrderById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.DispatchOrder(User.HostName, User.DBName);
            var beData = tranBL.getDispatchOrderByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getDispatchOrderDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion



        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult SalesAllotment()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesAllotment);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotment);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateSalesAllotment()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrder(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotment(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotment(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotment;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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


        #region "Sales Order"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesOrder, false)]
        public ActionResult PetrolPumpOrder()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesOrder);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesOrder);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesOrder, false)]
        public ActionResult SalesOrder()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesOrder);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesOrder);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesOrder, false)]
        public JsonNetResult SaveUpdateSalesOrder()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesOrder>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrder(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrder(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrder(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesOrder;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetSalesOrderById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesOrder(User.HostName, User.DBName);
            var beData = tranBL.getSalesOrderByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesOrderDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        //[HttpGet]
        //public JsonNetResult getPendinSalesOrder(int ledgerId, int? agentId, DateTime? voucherDate)
        //{
        //    if (!agentId.HasValue)
        //        agentId = 0;

        //    if (!voucherDate.HasValue)
        //        voucherDate = DateTime.Today;

        //    ResponeValues resVal = new ResponeValues();
        //    Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
        //    try
        //    {
        //        dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingOrder(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
        //        resVal.IsSuccess = true;
        //        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }

        //    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}
        #endregion
        #region "Delivery Note"

        //[HttpGet]
        //public JsonNetResult getPendingDeliveryNote(int ledgerId, int? agentId, DateTime? voucherDate)
        //{
        //    if (!agentId.HasValue)
        //        agentId = 0;

        //    if (!voucherDate.HasValue)
        //        voucherDate = DateTime.Today;

        //    ResponeValues resVal = new ResponeValues();
        //    Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
        //    try
        //    {
        //        dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getPendingDeliveryNote(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
        //        resVal.IsSuccess = true;
        //        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }

        //    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}

        //[HttpGet]
        //public JsonNetResult getDeliveryNotePartyDetails(int tranId)
        //{

        //    ResponeValues resVal = new ResponeValues();
        //    Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.DeliveryNote();
        //    try
        //    {
        //        dataColl = new Dynamic.DataAccess.Inventory.Transaction.DeliveryNoteDB(User.HostName, User.DBName).getDeliveryNoteDetailsByRefNo(User.UserId, 0, "", tranId);
        //        resVal.IsSuccess = true;
        //        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }

        //    return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}

        #endregion

        #region "SalesReturn"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesReturn, false)]
        public ActionResult SalesReturn()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesReturn);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesReturn);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesReturn, false)]
        public JsonNetResult SaveUpdateSalesReturn()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesReturn(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.SalesReturn);
                    //    if (!sapLastV.IsSuccess)
                    //    {
                    //        return new JsonNetResult() { Data = sapLastV, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}

                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/salesreturn", file));
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

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.SalesReturn(User.HostName, User.DBName).SaveFormData(beData);


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesReturn;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetSalesReturnById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesReturn(User.HostName, User.DBName);
            var beData = tranBL.getSalesReturnByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesReturnDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        // [HttpGet]
        //public JsonNetResult GetSalesVehilceListForAutoC()
        //{
        //    var dataColl = new Dynamic.DataAccess.Inventory.Transaction.SalesInvoiceDB(User.HostName, User.DBName).getVehilceListForAutoComplete(User.UserId);

        //    return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        //}

        #endregion

        #region "Purchase Return"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseReturn, false)]
        public ActionResult PurchaseReturn()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseReturn);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseReturn);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PurchaseReturn, false)]
        public JsonNetResult SaveUpdatePurchaseReturn()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseReturn(User.HostName, User.DBName).SaveFormData(beData);

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseReturn(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseReturn(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PurchaseReturn;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPurchaseReturnById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseReturn(User.HostName, User.DBName);
            var beData = tranBL.getPurchaseReturnByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getPurchaseReturnDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }
        #endregion

        #region "Purchase Indent"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult Indent()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.IndentForm);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.IndentForm);
            return View();
        }

        [HttpPost]
        public JsonNetResult GetIndentById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.IndentForm(User.HostName, User.DBName);
            var beData = tranBL.getIndentFormByTranId(tranId, User.UserId);
            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost]
        [PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.IndentForm, false)]
        public JsonNetResult SaveUpdateIndent()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.IndentForm>(Request["jsonData"]);
                if (beData != null)
                {
                    if (string.IsNullOrEmpty(beData.RequesterName))
                        beData.RequesterName = User.UserName;

                    var existDoc = beData.DocumentColl;
                    beData.CanUpdateDocument = true;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    bool isModify = false;

                    var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.IndentForm(User.HostName, User.DBName);
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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.IndentForm;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPendingIndentForSO(int? ForBranchId, DateTime? DateFrom, DateTime? DateTo)
        {
            var tranBL = new Dynamic.DataAccess.Inventory.Transaction.IndentFormDB(User.HostName, User.DBName);
            var dataColl = tranBL.getPendingIndentForSO(User.UserId, ForBranchId, DateFrom, DateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult ClosedIndent(int TranId)
        {
            var tranBL = new Dynamic.DataAccess.Inventory.Transaction.IndentFormDB(User.HostName, User.DBName);
            var dataColl = tranBL.Closed(User.UserId, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion



        #region "Purchase Quotation"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseQuotation, false)]
        public ActionResult PurchaseQuotation()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseQuotation);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseQuotation);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PurchaseQuotation, false)]
        public JsonNetResult SaveUpdatePurchaseQuotation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseQuotation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseQuotation(User.HostName, User.DBName).SaveFormData(beData);

                    var existDoc = beData.DocumentColl;

                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseQuotation(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseQuotation(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PurchaseQuotation;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPurchaseQuotationById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseQuotation(User.HostName, User.DBName);
            var beData = tranBL.getPurchaseQuotationByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getPurchaseQuotationDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpGet]
        public JsonNetResult getPendingPurchaseQuotation(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.PurchaseOrderDB(User.HostName, User.DBName).getPendingQuotation(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPurchaseQuotationDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.PurchaseOrder beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseOrder>(Request["jsonData"]);
                }
                beData.CUserId = User.UserId;
                new Dynamic.DataAccess.Inventory.Transaction.PurchaseOrderDB(User.HostName, User.DBName).getPurchaseOrderDetails(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }
        #endregion

        #region "Purchase Order"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseOrder, false)]
        public ActionResult PurchaseOrder()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseOrder);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseOrder);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PurchaseOrder, false)]
        public JsonNetResult SaveUpdatePurchaseOrder()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseOrder>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseOrder);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseOrder(User.HostName, User.DBName);
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseOrder(User.HostName, User.DBName).SaveFormData(beData);
                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = beData.CreatedBy;

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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PurchaseOrder;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPurchaseOrderById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseOrder(User.HostName, User.DBName);
            var beData = tranBL.getPurchaseOrderByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getPurchaseOrderDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost]
        public JsonNetResult GetPurchaseOrderDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.ReceiptNote beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.ReceiptNote>(Request["jsonData"]);
                }

                beData.CUserId = User.UserId;
                new Dynamic.DataAccess.Inventory.Transaction.ReceiptNoteDB(User.HostName, User.DBName).getPurchaseOrderDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult getPendingPurchaseOrder(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.ReceiptNoteDB(User.HostName, User.DBName).getPendingOrder(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Receipt Note"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ReceiptNote, false)]
        public ActionResult ReceiptNote()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.ReceiptNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.ReceiptNote);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ReceiptNote, false)]
        public JsonNetResult SaveUpdateReceiptNote()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.ReceiptNote(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.ReceiptNote>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.ReceiptNote);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/receiptnote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
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
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ReceiptNote;
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
        public JsonNetResult GetReceiptNoteById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.ReceiptNote(User.HostName, User.DBName);
            var beData = tranBL.getReceiptNoteByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getReceiptNoteDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpGet]
        public JsonNetResult getPendingReceiptNote(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.PurchaseInvoiceDB(User.HostName, User.DBName).getPendingReceiptNote(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetReceiptNoteDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.PurchaseInvoice beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseInvoice>(Request["jsonData"]);
                }
                beData.CUserId = User.UserId;

                new Dynamic.DataAccess.Inventory.Transaction.PurchaseInvoiceDB(User.HostName, User.DBName).getReceiptDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        #endregion

        #region "Receipt Note Return"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ReceiptNote, false)]
        public ActionResult ReceiptNoteReturn()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.ReceiptNoteReturn);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.ReceiptNoteReturn);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ReceiptNote, false)]
        public JsonNetResult SaveUpdateReceiptNoteReturn()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {


                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.ReceiptNote>(Request["jsonData"]);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    beData.CUserId = User.UserId;
                    //resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ReceiptNote(User.HostName, User.DBName).SaveFormData(beData);
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = beData.CreatedBy;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ReceiptNoteReturn(User.HostName, User.DBName).ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = new Dynamic.BusinessLogic.Inventory.Transaction.ReceiptNoteReturn(User.HostName, User.DBName).SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ReceiptNoteReturn;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetReceiptNoteReturnById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.ReceiptNoteReturn(User.HostName, User.DBName);
            var beData = tranBL.getReceiptNoteByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getReceiptNoteDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        #endregion

        #region "Purchse Addiional Invoice"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Journal, false)]
        public ActionResult PurchaseAdditionalInvoice()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseAdditionalInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseAdditionalInvoice);
            return View();
        }

        [HttpPost]
        public JsonNetResult GetPurchaseListForAditionalInvoice(DateTime voucherDate,int VoucherId,int CostClassId)
        {
            var journalBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal);
            var dataColl = journalBL.getAllPurchaseListForAditionalInvoice(User.UserId, voucherDate, 0, 0,VoucherId,CostClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost]
        public JsonNetResult GetProductProductGroupForJV(string tranIdColl)
        {
            var journalBL = new Dynamic.DataAccess.Inventory.Transaction.PurchaseInvoiceDB(User.HostName, User.DBName);
            var query = journalBL.getProductProductGroupModeList(User.UserId, tranIdColl);

            var qry1 = from q in query
                       group q by q.ProductId into g
                       select new Dynamic.BusinessEntity.Global.CommonClass
                       {
                           id = g.Key,
                           text = g.First().ProductName
                       };

            var ProductColl = new List<Dynamic.BusinessEntity.Global.CommonClass>();
            foreach (var q in qry1)
            {
                ProductColl.Add(q);
            }

            var qry2 = from q in query
                       group q by q.ProductGroupId into g
                       select new Dynamic.BusinessEntity.Global.CommonClass
                       {
                           id = g.Key,
                           text = g.First().ProductGroup
                       };

            var ProductGroupColl = new List<Dynamic.BusinessEntity.Global.CommonClass>();
            foreach (var q in qry2)
            {
                ProductGroupColl.Add(q);
            }

            var qry3 = from q in query
                       group q by q.Model into g
                       select new
                       {
                           Text = g.Key,
                       };

            var ModelColl = new List<Dynamic.BusinessEntity.Global.CommonClass>();
            var mid = 1;
            foreach (var q in qry3)
            {
                ModelColl.Add(new Dynamic.BusinessEntity.Global.CommonClass()
                {
                    id = mid,
                    text = q.Text
                });

                mid++;
            }

            var retVal = new
            {
                ProductColl = ProductColl,
                ProductGroupColl = ProductGroupColl,
                ModelColl = ModelColl,
                IsSuccess = true,
                ResponseMSG = GLOBALMSG.SUCCESS
            };

            return new JsonNetResult() { Data = retVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        //[HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.PurchaseAdditionalInvoice, false)]
        //public JsonNetResult SaveUpdatePurchaseAdditionalInvoice()
        //{
        //        ResponeValues resVal = new ResponeValues();
        //        try
        //        {


        //            var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseListForAditionInvoice>(Request["jsonData"]);
        //            if (beData != null)
        //            {

        //            resVal = new Dynamic.BusinessLogic.Inventory.Transaction.(User.HostName, User.DBName).SaveFormData(beData);
        //            }
        //            else
        //            {
        //                resVal.ResponseMSG = "Blank Data Can't be Accept";
        //            }

        //        }
        //        catch (Exception ee)
        //        {
        //            resVal.IsSuccess = false;
        //            resVal.ResponseMSG = ee.Message;
        //        }

        //        return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //    }
        #endregion

        #region "Purchase Invoice"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult DairyPurchaseSetup()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseInvoice);
            return View();
        }

        [HttpPost]
        public JsonNetResult GetAllDairyPS()
        {
            var tranBL = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName);
            var beData = tranBL.GetAllPartyDPS(User.UserId);

            return new JsonNetResult() { Data = beData, TotalCount = beData.Count, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDairyPurchaseSetup(int ProductId, int? PartyId, bool ShowLog)
        {
            var tranBL = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName);
            var beData = tranBL.GetDairyPurchaseSetup(User.UserId, ProductId, PartyId, ShowLog);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveDairyPurchaseSetup(List<Dynamic.BusinessEntity.Setup.DairyPurchaseSetup> dataColl)
        {
            var tranBL = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName);
            var beData = tranBL.SaveDairyPurchaseSetup(User.UserId, dataColl);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDairyPurchaseSetup(int ProductId, int? PartyId)
        {
            var tranBL = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName);
            var beData = tranBL.DeleteDairyPurchaseSetup(User.UserId, ProductId, PartyId);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult DairySalesInvoice()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult DairyPurchaseInvoice()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseInvoice);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult TeaPurchaseInvoice()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseInvoice);
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult PurchaseInvoice()

        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseInvoice);
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public JsonNetResult SaveUpdatePurchaseInvoice()
        {
            ResponeValues resVal = new ResponeValues();
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseInvoice(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseInvoice>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseInvoice);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/purchase", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }


                    beData.CUserId = User.UserId;
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


                        try
                        {
                            if (resVal.RId > 0)
                                new Dynamic.BusinessLogic.Inventory.Transaction.Consumption(User.HostName, User.DBName).SaveUpdateSalesLossToConsumption(User.UserId, false, null, resVal.RId);
                        }
                        catch { }
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PurchaseInvoice;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPurchaseInvoiceById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseInvoice(User.HostName, User.DBName);
            var beData = tranBL.getPurchaseInvoiceByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getPurchaseInvoiceDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpGet]
        public JsonNetResult getPendingPurchaseInvoice(int ledgerId, int? agentId, DateTime? voucherDate)
        {
            if (!agentId.HasValue)
                agentId = 0;

            if (!voucherDate.HasValue)
                voucherDate = DateTime.Today;

            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections dataColl = new Dynamic.BusinessEntity.Inventory.Transaction.PendingDeliverNoteForRecChallanCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.Transaction.PurchaseReturnDB(User.HostName, User.DBName).getPendingInvoice(User.UserId, ledgerId, agentId.Value, voucherDate.Value);
                resVal.IsSuccess = true;
                resVal.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPurchaseDetailsByItemAllocationId(Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null || beData.ItemAllocationColl == null || beData.ItemAllocationColl.Count == 0)
                {
                    beData = DeserializeObjectIgnoreNull<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn>(Request["jsonData"]);
                }

                beData.CUserId = User.UserId;
                new Dynamic.DataAccess.Inventory.Transaction.PurchaseReturnDB(User.HostName, User.DBName).getPurchaseDetailsByItemAllocationId(ref beData);
                beData.IsSuccess = true;
                beData.ResponseMSG = GLOBALMSG.SUCCESS;
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        #endregion

        #region "SSF Claim"

        public ActionResult SSFClaim()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult getPatientDetailsForClaim(int PatientNo)
        {
            Dynamic.BusinessEntity.Global.GlobalObject.CurrentUser = User;

            Dynamic.ReportEntity.SSF.Claim claim = new Dynamic.ReportEntity.SSF.Claim();
            try
            {
                double OpeningPoint = 0;
                double openingAmt = 0;
                Dynamic.ReportEntity.Account.LedgerVoucherCollections dataColl = new Dynamic.Reporting.Account.LedgerSummary(User.HostName, User.DBName).getLedgerVoucher(User.UserId, BaseDate.NepaliDate, 0, DateTime.Today, DateTime.Today, ref openingAmt,ref OpeningPoint, true, false, PatientNo);

                Dynamic.ReportEntity.Account.LedgerVoucherCollections tmpDataColl = new Dynamic.ReportEntity.Account.LedgerVoucherCollections();
                double currentClosing = openingAmt;
                DateTime currentDateTime = DateTime.Today;
                foreach (var v in dataColl)
                {
                    currentClosing += v.DebitAmt - v.CreditAmt;
                    v.CurrentClosing = currentClosing;

                    if (v.VoucherDate.Year > 1900)
                        v.VoucherAge = (int)(currentDateTime - v.VoucherDate).TotalDays;

                    tmpDataColl.Add(v);
                }

                double drAmt = 0, crAmt = 0;
                drAmt = tmpDataColl.Sum(p1 => p1.DebitAmt);
                crAmt = tmpDataColl.Sum(p1 => p1.CreditAmt);

                double cl = openingAmt + drAmt - crAmt;

                double totalCRAmt = (openingAmt < 0 ? Math.Abs(openingAmt) : -openingAmt) + crAmt;

                foreach (var cd in tmpDataColl)
                {
                    double dues = totalCRAmt - cd.DebitAmt;

                    if (dues < 0)
                        cd.DuesAmt = Math.Abs(dues);

                    totalCRAmt = dues;
                }

                var ledData = new
                {
                    OpeningAmt = openingAmt,
                    DrAmt = drAmt,
                    CrAmt = crAmt,
                    ClosingAmt = cl,
                    DataColl = tmpDataColl
                };

                claim.TotalOpening = openingAmt;
                claim.TotalDrAmt = claim.DrAmount_H + drAmt;
                claim.TotalCrAmt = claim.CrAmount_H + crAmt;
                claim.ClosingAmt = openingAmt + claim.TotalDrAmt - claim.TotalCrAmt;

                var returnData = new
                {
                    ClaimData = claim,
                    LedgerVoucher = ledData
                };

                return new JsonNetResult() { Data = returnData, TotalCount = 1, IsSuccess = claim.IsSuccess, ResponseMSG = claim.ResponseMSG };
            }
            catch (Exception ee)
            {
                claim.IsSuccess = false;
                claim.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = claim, TotalCount = 1, IsSuccess = claim.IsSuccess, ResponseMSG = claim.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetSSFDiagnosis()
        {
            var beData = new Dynamic.Reporting.SSF.Claim(User.HostName, User.DBName).getAllDiagnosis(User.UserId);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


 

        private String encodeFileToBase64Binary(string fileName)
        {
            using (System.IO.FileStream reader = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                byte[] buffer = new byte[reader.Length];
                reader.Read(buffer, 0, (int)reader.Length);
                return Convert.ToBase64String(buffer);
            }
        }

        private ResponeValues PrintVoucher(int entityId, int voucherId, Dynamic.BusinessEntity.Account.VoucherTypes voucherType, int tranid)
        {
            ResponeValues resVal = new ResponeValues();
            Dynamic.BusinessEntity.Global.CompanyBranchDetailsForPrint comDet = null;

            try
            {
                var curUser = User;

                int rptTranId = 0;

                comDet = new Dynamic.DataAccess.Global.GlobalDB(curUser.HostName, curUser.DBName).getCompanyBranchDetailsForPrint(curUser.UserId, entityId, (int)voucherType, tranid);

                if (comDet.IsSuccess)
                {
                    Global.ReportTemplate reportTemplate = null;

                    if (rptTranId > 0)
                        reportTemplate = new Global.ReportTemplate(curUser.HostName, curUser.DBName, curUser.UserId, entityId, voucherId, true, rptTranId);
                    else
                        reportTemplate = new Global.ReportTemplate(curUser.HostName, curUser.DBName, curUser.UserId, entityId, voucherId, true);

                    if (reportTemplate.TemplateAttachments == null || reportTemplate.TemplateAttachments.Count == 0)
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "No Template Found";
                    }
                    else
                    {
                        var com = new Dynamic.DataAccess.Setup.CompanyDetailDB(curUser.HostName, curUser.DBName).getCompanyDetailsWithOutLogo(curUser.UserId,comDet.BranchId);

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
                        Dynamic.BusinessEntity.Global.GlobalObject.CurrentBranch = branch;

                        Dynamic.BusinessEntity.Global.ReportTempletes template = reportTemplate.DefaultTemplate;
                        System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                        paraColl.Add("UserId", curUser.UserId.ToString());
                        paraColl.Add("TranId", tranid.ToString());
                        paraColl.Add("UserName", User.Identity.Name);
                        Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                        _rdlReport.RenderType = "pdf";
                        _rdlReport.NoShow = false;

                        if (!string.IsNullOrEmpty(template.Path))
                        {

                            _rdlReport.ReportFile = reportTemplate.GetPath(template);

                            if (_rdlReport.Object != null)
                            {
                                string fileName = Guid.NewGuid().ToString() + ".pdf";
                                string basePath = "print-tran-log//" + fileName;
                                string sFile = GetPath("~//" + basePath);
                                reportTemplate.SavePDF(_rdlReport.Object, sFile);
                                resVal.IsSuccess = true;
                                resVal.ResponseMSG = sFile;
                                resVal.ResponseId = fileName;

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
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = comDet.ResponseMSG;
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return resVal;
        }

        #endregion


        #region "DischargeSlip"

        public ActionResult DischargeSlip()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult SaveDischargeSlip()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.DischargeSlip>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new Dynamic.BusinessLogic.Inventory.Transaction.DischargeSlip(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllDischargeSlip()
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.DischargeSlip(User.UserId, User.HostName, User.DBName).GetAllDischargeSlip(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDischargeSlipById(int TranId)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.DischargeSlip(User.UserId, User.HostName, User.DBName).GetDischargeSlipById(0, TranId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPatientForDS(int PatientId)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Transaction.DischargeSlip(User.UserId, User.HostName, User.DBName).getPatient(PatientId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        public JsonNetResult DelDischargeSlip(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.Transaction.DischargeSlip(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        #region "SalesDebitNote"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult SalesDebitNote()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesDebitNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesDebitNote);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public JsonNetResult SaveUpdateSalesDebitNote()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesDebitNote(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn>(Request["jsonData"]);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/SalesDebitNote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesDebitNote;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetSalesDebitNoteById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesDebitNote(User.HostName, User.DBName);
            var beData = tranBL.getSalesReturnByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesReturnDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion

        #region "SalesCreditNote"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult SalesCreditNote()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesCreditNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesCreditNote);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateSalesCreditNote()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesCreditNote(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesReturn>(Request["jsonData"]);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/SalesCreditNote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesCreditNote;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetSalesCreditNoteById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesCreditNote(User.HostName, User.DBName);
            var beData = tranBL.getSalesReturnByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesReturnDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion

        #region "PurchaseDebitNote"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult PurchaseDebitNote()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseDebitNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseDebitNote);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PurchaseInvoice, false)]
        public JsonNetResult SaveUpdatePurchaseDebitNote()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseDebitNote(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn>(Request["jsonData"]);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/PurchaseDebitNote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PurchaseDebitNote;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPurchaseDebitNoteById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseDebitNote(User.HostName, User.DBName);
            var beData = tranBL.getPurchaseReturnByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getPurchaseReturnDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion

        #region "PurchaseCreditNote"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PurchaseInvoice, false)]
        public ActionResult PurchaseCreditNote()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.PurchaseCreditNote);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.PurchaseCreditNote);
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PurchaseInvoice, false)]
        public JsonNetResult SaveUpdatePurchaseCreditNote()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseCreditNote(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.PurchaseReturn>(Request["jsonData"]);
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
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/PurchaseCreditNote", file));
                            }
                        }
                    }
                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.PurchaseCreditNote;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.TranId.ToString("N") : "New " + auditLog.EntityId.ToString()) + beData.TranId.ToString("N");
                        auditLog.AutoManualNo = IsNullStr(beData.TranId.ToString());
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
        public JsonNetResult GetPurchaseCreditNoteById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.PurchaseCreditNote(User.HostName, User.DBName);
            var beData = tranBL.getPurchaseReturnByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getPurchaseReturnDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion


        #region "Sales Allotment Return"



        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult SalesAllotmentReturn()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesAllotmentReturn);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotmentReturn);
            //ViewBag.EntityDE = SerializedObject(new Dynamic.DataAccess.Setup.EntityWiseDisableButtonDB(User.HostName, User.DBName).getEntityWiseDisableButtonConfiguration(User.UserId, ViewBag.EntityId));

            return View();
        }



        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateSalesAllotmentReturn()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotmentReturn(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (SAP)
                    //{
                    //    var sapLastV = new PivotalOtherLib.Global.SAPConnection(User.HostName, User.DBName).CheckLastTranIsPending(beData.VoucherId, Dynamic.BusinessEntity.Account.VoucherTypes.SalesInvoice);
                    //    if (!sapLastV.IsSuccess)
                    //    {
                    //        return new JsonNetResult() { Data = sapLastV, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
                    //    }
                    //}
                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/sales", file));
                            }
                        }
                    }

                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {

                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotmentReturn;
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
               
                //resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetSalesAllotmentReturnById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotmentReturn(User.HostName, User.DBName);
            var beData = tranBL.getSalesInvoiceByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesInvoiceDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion


        #region "Sales Allotment Cancel"



        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalesInvoice, false)]
        public ActionResult SalesAllotmentCancel()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.SalesAllotmentCancel);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotmentCancel);
            //ViewBag.EntityDE = SerializedObject(new Dynamic.DataAccess.Setup.EntityWiseDisableButtonDB(User.HostName, User.DBName).getEntityWiseDisableButtonConfiguration(User.UserId, ViewBag.EntityId));

            return View();
        }



        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalesInvoice, false)]
        public JsonNetResult SaveUpdateSalesAllotmentCancel()
        {
            ResponeValues resVal = new ResponeValues();
            var salesBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotmentCancel(User.HostName, User.DBName);
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Transaction.SalesInvoice>(Request["jsonData"]);
                if (beData != null)
                {

                    beData.CanUpdateDocument = true;
                    var existDoc = beData.DocumentColl;
                    beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments("/Attachments/inventory/sales", file));
                            }
                        }
                    }

                    if (existDoc != null && existDoc.Count > 0)
                    {
                        foreach (var edoc in existDoc)
                            beData.DocumentColl.Add(edoc);
                    }

                    beData.CUserId = User.UserId;
                    beData.CreatedBy = User.UserId;
                    beData.ModifyBy = User.UserId;
                    beData.CanUpdateDocument = true;

                    bool isModify = false;
                    if (beData.TranId > 0)
                    {
                        isModify = true;
                        resVal = salesBL.ModifyFormData(beData);
                    }
                    else
                    {
                        resVal = salesBL.SaveFormData(beData);
                    }

                    if (resVal.IsSuccess)
                    {

                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesAllotmentCancel;
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
                //resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetSalesAllotmentCancelById(int tranId)
        {
            var tranBL = new Dynamic.BusinessLogic.Inventory.Transaction.SalesAllotmentCancel(User.HostName, User.DBName);
            var beData = tranBL.getSalesInvoiceByTranId(tranId, User.UserId);
            beData.CUserId = User.UserId;
            tranBL.getSalesInvoiceDetails(ref beData);

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion

        #region "Production Addiional Invoice"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Journal, false)]
        public ActionResult ProductionAdditionalInvoice()
        {
            ViewBag.VoucherType = Convert.ToInt32(Dynamic.BusinessEntity.Account.VoucherTypes.ProductionAditionalCost);
            ViewBag.EntityId = Convert.ToInt32(Dynamic.BusinessEntity.Global.FormsEntity.ProductionAditionalCost);
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStockJrnListForAditionalInvoice(DateTime voucherDate, int VoucherId, int CostClassId)
        {
            var journalBL = new Dynamic.BusinessLogic.Account.Transaction.Journal(User.HostName, User.DBName, Dynamic.BusinessEntity.Account.Transaction.TranTypes.Journal);
            var dataColl = journalBL.getAllStockJournalListForAditionalInvoice(User.UserId, voucherDate, 0, 0,VoucherId,CostClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }

        [HttpPost]
        public JsonNetResult GetProductProductGroupForSJ(string tranIdColl)
        {
            var journalBL = new Dynamic.DataAccess.Inventory.Transaction.StockJournalDB(User.HostName, User.DBName);
            var query = journalBL.getProductProductGroupModeList(User.UserId, tranIdColl);

            var qry1 = from q in query
                       group q by q.ProductId into g
                       select new Dynamic.BusinessEntity.Global.CommonClass
                       {
                           id = g.Key,
                           text = g.First().ProductName
                       };

            var ProductColl = new List<Dynamic.BusinessEntity.Global.CommonClass>();
            foreach (var q in qry1)
            {
                ProductColl.Add(q);
            }

            var qry2 = from q in query
                       group q by q.ProductGroupId into g
                       select new Dynamic.BusinessEntity.Global.CommonClass
                       {
                           id = g.Key,
                           text = g.First().ProductGroup
                       };

            var ProductGroupColl = new List<Dynamic.BusinessEntity.Global.CommonClass>();
            foreach (var q in qry2)
            {
                ProductGroupColl.Add(q);
            }

            var qry3 = from q in query
                       group q by q.Model into g
                       select new
                       {
                           Text = g.Key,
                       };

            var ModelColl = new List<Dynamic.BusinessEntity.Global.CommonClass>();
            var mid = 1;
            foreach (var q in qry3)
            {
                ModelColl.Add(new Dynamic.BusinessEntity.Global.CommonClass()
                {
                    id = mid,
                    text = q.Text
                });

                mid++;
            }

            var retVal = new
            {
                ProductColl = ProductColl,
                ProductGroupColl = ProductGroupColl,
                ModelColl = ModelColl,
                IsSuccess = true,
                ResponseMSG = GLOBALMSG.SUCCESS
            };

            return new JsonNetResult() { Data = retVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
        }


        #endregion


    }
}