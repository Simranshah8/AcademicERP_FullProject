using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.FrontDesk.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {

        public ActionResult EmployeeCandidate()
        {
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddEmployee, false)]
        public JsonNetResult SaveEmployee()
        {
            string photoLocation = "/Attachments/academic/employee";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.Employee>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        var signature = filesColl["signature"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Photo = photoDoc.Data;
                            beData.PhotoPath = photoDoc.DocPath;

                        }

                        if (signature != null)
                        {
                            var signatureDoc = GetAttachmentDocuments(photoLocation, signature, true);
                            beData.Signature = signatureDoc.Data;
                            beData.SignaturePath = signatureDoc.DocPath;
                        }

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.AttachmentColl.Add
                                    (
                                     new Dynamic.BusinessEntity.GeneralDocument()
                                     {
                                         Data = att.Data,
                                         DocPath = att.DocPath,
                                         DocumentTypeId = v.DocumentTypeId,
                                         Extension = att.Extension,
                                         Name = v.Name,
                                         Description = v.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.AttachmentColl.Add(v);
                            }
                        }
                    }

                    if (!beData.EmployeeId.HasValue)
                        beData.EmployeeId = 0;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.EmployeeCandidate(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetEmployeeAutoNumber()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.EmployeeCandidate(User.UserId, User.HostName, User.DBName).getAutoRegdNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AddEmployee, false)]
        public JsonNetResult GetEmployeeById(int EmployeeId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.EmployeeCandidate(User.UserId, User.HostName, User.DBName).getEmployeeById(0, EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.AddEmployee, false)]
        public JsonNetResult DelEmployee(int EmployeeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.EmployeeCandidate(User.UserId, User.HostName, User.DBName).DeleteById(0, EmployeeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpSummary(string DepartmentIdColl = "")
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.EmployeeCandidate(User.UserId, User.HostName, User.DBName).getEmployeeSummaryList(DepartmentIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.AdmissionEnquiry, false)]
        public ActionResult Enquiry()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetEmpCouncellingStatuses(int? EmpId)
        { 

            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).GetEmpCouncellingStatuses(this.AcademicYearId,EmpId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEnqSummary(DateTime? dateFrom,DateTime? dateTo)
        {
            if (!dateFrom.HasValue)
                dateFrom = DateTime.Today;

            if (!dateTo.HasValue)
                dateTo = DateTime.Today;

            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).getEnqSummary(dateFrom.Value, dateTo.Value);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAdmissionEnqNo()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).getAutoNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetEnquiryById(int TranId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).GetAddmissionEnquiryById(0, TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.AdmissionEnquiry, false)]
        public JsonNetResult DelEnquiry(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AdmissionEnquiry, false)]
        public JsonNetResult SaveEnquiry()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            { 
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.AddmissionEnquiry>(Request["jsonData"]);
                if (beData != null)
                {
                    if (beData.AttachmentColl == null)
                        beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();

                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Photo = photoDoc.Data;
                            beData.PhotoPath = photoDoc.DocPath;

                        }

                      
                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.AttachmentColl.Add
                                    (
                                     new Dynamic.BusinessEntity.GeneralDocument()
                                     {
                                         Data = att.Data,
                                         DocPath = att.DocPath,
                                         DocumentTypeId = v.DocumentTypeId,
                                         Extension = att.Extension,
                                         Name = v.Name,
                                         Description = v.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }


                    if (!string.IsNullOrEmpty(beData.PhotoPath) && beData.Photo == null)
                    {
                        if (beData.PhotoPath.StartsWith(photoLocation))
                        {
                            beData.Photo = GetBytesFromFile(beData.PhotoPath);
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.AttachmentColl.Add(v);
                            }
                        }
                    }
                     


                    beData.CUserId = User.UserId;

                    if (!beData.EnquiryId.HasValue)
                        beData.EnquiryId = 0;

                    bool isModify = false;
                    if (beData.EnquiryId > 0)
                        isModify = true;

                    var tranBL = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName);
                    beData.IsAnonymous = false;
                    resVal = tranBL.SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.EnquiryId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.AdmissionEnquiry;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Admission Enquiry Modify" : "New  Admisssion Enquiry ");
                        auditLog.AutoManualNo = beData.EnquiryId.ToString();
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FollowUp, false)]
        public ActionResult FollowUp()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetEnqFollowup(int FollowupType)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).getEnqForFollowup(this.AcademicYearId, FollowupType);

            return new JsonNetResult() { Data = dataColl, TotalCount =dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetEnqForCounCelling()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).getEnqForCounCelling(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEnqFollowupList(int TranId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).getFollowupList(TranId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveEnquiryFollowup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.StudentPaymentFollowup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.TranId = 0;
                    beData.CUserId = User.UserId;
                    beData.AcademicYearId = this.AcademicYearId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).SaveFollowup(beData);
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
        public JsonNetResult SaveEnqFollowupClosed(int RefTranId, string Remarks)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).SaveClosed(RefTranId, Remarks);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveAssignCounselor(int TranId,DateTime? AssignDate,List<int> EmployeeIdColl)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).SaveAssignCounselor(TranId,AssignDate, EmployeeIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveEnqStatus(int TranId, int Status,string Remarks)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AddmissionEnquiry(User.UserId, User.HostName, User.DBName).SaveEnqStatus(TranId, Status, Remarks);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SeatAllotment, false)]
        public ActionResult SeatAllotment()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PreAdmission, false)]
        public ActionResult PreAdmission()
        {
            return View();
        }

        #region "Visitor"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.VisitorBook, false)]
        public ActionResult VisitorBook()
        {
            return View();
        }

        [HttpPost]
         [PermissionsAttribute(Actions.Save, (int)ENTITIES.VisitorBook, false)]
        public JsonNetResult SaveVisitorBook()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.Visitor>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.PhotoPath = photoDoc.DocPath;
                        }



                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.AttachmentColl.Add
                                    (
                                     new Dynamic.BusinessEntity.GeneralDocument()
                                     {
                                         Data = att.Data,
                                         DocPath = att.DocPath,
                                         DocumentTypeId = v.DocumentTypeId,
                                         Extension = att.Extension,
                                         Name = v.Name,
                                         Description = v.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }



                    if (!string.IsNullOrEmpty(beData.PhotoPath) && beData.Photo == null)
                    {
                        if (beData.PhotoPath.StartsWith(photoLocation))
                        {
                            // beData.Photo = GetBytesFromFile(beData.PhotoPath);
                        }
                    }


                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation))
                            {
                                v.Data = GetBytesFromFile(beData.PhotoPath);
                            }
                        }
                    }
                    beData.CUserId = User.UserId;

                    if (!beData.VisitorId.HasValue)
                        beData.VisitorId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Visitor(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllVisitorList(DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Visitor(User.UserId, User.HostName, User.DBName).GetAllVisitor(0,dateFrom,dateTo);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetVisitorById(int VisitorId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Visitor(User.UserId, User.HostName, User.DBName).GetVisitorById(0, VisitorId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

         [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelVisitor(int VisitorId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.Visitor(User.UserId, User.HostName, User.DBName).DeleteById(0, VisitorId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion


        [PermissionsAttribute(Actions.View, (int)ENTITIES.GatePass, false)]
        public ActionResult GatePass()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.GatePass, false)]
        public JsonNetResult SaveGatePass()
        { 
            ResponeValues resVal = new ResponeValues();
            try
            {
              
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.GatePass>(Request["jsonData"]);
                if (beData != null)
                {
                    
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.GatePass(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.GatePass, false)]
        public JsonNetResult UpdateInTimeOfGatePass()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.GatePass>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                     
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.GatePass(User.UserId, User.HostName, User.DBName).UpdateInTime(beData.TranId.Value, beData.InTime.Value);
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
        public JsonNetResultWithEnum GetAllGatePassList(DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.GatePass(User.UserId, User.HostName, User.DBName).GetAllGatePass(0, dateFrom, dateTo);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [PermissionsAttribute(Actions.View, (int)ENTITIES.PhoneCallLog, false)]
        public ActionResult PhoneCallLog()
        {
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.GatePass, false)]
        public JsonNetResult SavePhoneCallLog()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.PostalCallLog>(Request["jsonData"]);
                if (beData != null)
                {

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.PostalCallLog(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllPhoneCallLogList(DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.PostalCallLog(User.UserId, User.HostName, User.DBName).GetAllPostalCallLog(0, dateFrom, dateTo);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }



        [PermissionsAttribute(Actions.View, (int)ENTITIES.PostalServices, false)]
        public ActionResult PostalServices()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SavePivotalServices()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string photoLocation = "/Attachments/fronddesk";
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.PostalService>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);

                            beData.PhotoPath = photoDoc.DocPath;

                        }


                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.AttachmentColl.Add
                                    (
                                     new Dynamic.BusinessEntity.GeneralDocument()
                                     {
                                         Data = att.Data,
                                         DocPath = att.DocPath,
                                         DocumentTypeId = v.DocumentTypeId,
                                         Extension = att.Extension,
                                         Name = v.Name,
                                         Description = v.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }


                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.AttachmentColl.Add(v);
                            }
                        }
                    }

                    //if (tmpAttachmentColl != null)
                    //{

                    //    foreach (var v in tmpAttachmentColl)
                    //    {
                    //        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                    //        {
                    //            if (v.DocPath.StartsWith(photoLocation))
                    //            {
                    //                v.Data = GetBytesFromFile(beData.PhotoPath);
                    //            }
                    //        }
                    //    }

                    //}
                    beData.CUserId = User.UserId;

                    if (!beData.PostalServicesId.HasValue)
                        beData.PostalServicesId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.PostalServices(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult SaveDispatchServices()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                string photoLocation = "/Attachments/fronddesk";
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.PostalDispatch>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        
                         
                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);
                                beData.AttachmentColl.Add
                                    (
                                     new Dynamic.BusinessEntity.GeneralDocument()
                                     {
                                         Data = att.Data,
                                         DocPath = att.DocPath,
                                         DocumentTypeId = v.DocumentTypeId,
                                         Extension = att.Extension,
                                         Name = v.Name,
                                         Description = v.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }


                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.AttachmentColl.Add(v);
                            }
                        }
                    }

                    if (!beData.PostalDispatchId.HasValue)
                        beData.PostalDispatchId = 0;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.PostalDispatch(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllReceivedList(DateTime fromDate,DateTime toDate)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.PostalServices(User.UserId, User.HostName, User.DBName).GetAllReceivedList(fromDate, toDate);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count(), IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllDispatchedList()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.PostalDispatch(User.UserId, User.HostName, User.DBName).GetAllDispatchList( /*dateFrom, dateTo*/);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count(), IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPostalServiceById(int PostalServicesId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.PostalServices(User.UserId, User.HostName, User.DBName).GetPostalServiceById(0, PostalServicesId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPostalDispatchById(int PostalDispatchId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.PostalDispatch(User.UserId, User.HostName, User.DBName).GetDispatchById(0, PostalDispatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelReceived(int PostalServicesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.PostalServices(User.UserId, User.HostName, User.DBName).DeleteById(PostalServicesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelPostalDispatch(int PostalDispatchId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.PostalDispatch(User.UserId, User.HostName, User.DBName).DeleteById(PostalDispatchId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.NewEmployeeApplication, false)]
        public ActionResult NewEmployee()
        {
            return View();
        }
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Complain, false)]
        public ActionResult Complain()
        {
            return View();
        }
        
        public ActionResult Setup()
        {
            return View();
        }

        #region "Source"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FrontDeskSetup, false)]
        public JsonNetResult SaveSource()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.Source>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.SourceId.HasValue)
                        beData.SourceId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Source(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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

        [AllowAnonymous]
        [HttpPost]
        public JsonNetResult GetAllSourceList(bool ForTran=true)
        {
            if (User == null)
            {
                var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Source(1, hostName,dbName).GetAllSource(0, ForTran);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {

                var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Source(User.UserId, User.HostName, User.DBName).GetAllSource(0, ForTran);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }

        }

        [HttpPost]
        public JsonNetResult GetSourceById(int SourceId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Source(User.UserId, User.HostName, User.DBName).GetSourceById(0, SourceId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelSource(int SourceId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.Source(User.UserId, User.HostName, User.DBName).DeleteById(0, SourceId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "ComplainType"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FrontDeskSetup, false)]
        public JsonNetResult SaveComplainType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.ComplainType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ComplainTypeId.HasValue)
                        beData.ComplainTypeId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.ComplainType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllComplainTypeList()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.ComplainType(User.UserId, User.HostName, User.DBName).GetAllComplainType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetComplainTypeById(int ComplainTypeId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.ComplainType(User.UserId, User.HostName, User.DBName).GetComplainTypeById(0, ComplainTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelComplainType(int ComplainTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.ComplainType(User.UserId, User.HostName, User.DBName).DeleteById(0, ComplainTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PaymentFollowup, false)]
        public ActionResult PaymentFollowup()
        {
            return View();
        }
        
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PaymentFollowup, false)]
        public JsonNetResult SavePaymentFollowup()
        { 
            ResponeValues resVal = new ResponeValues();
            try
            {                
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.StudentPaymentFollowup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.TranId = 0;
                    beData.CUserId = User.UserId;
                    beData.AcademicYearId = this.AcademicYearId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.StudentPaymentFollowup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetPaymentFollowupList(int StudentId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.StudentPaymentFollowup(User.UserId, User.HostName, User.DBName).getStudentWiseList(StudentId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDateWisePaymentFollowupList(DateTime? dateFrom,DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.StudentPaymentFollowup(User.UserId, User.HostName, User.DBName).getFollowupList(dateFrom, dateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveFollowupClosed(int RefTranId,string Remarks)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.StudentPaymentFollowup(User.UserId, User.HostName, User.DBName).SaveClosed(RefTranId, Remarks);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        //Added By sureshFor Complain in 10 Baishakh starts

        #region "Complain"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Complain)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Complain)]
        public JsonNetResult SaveUpdateComplain()
        {
            string photoLocation = "/Attachments/Fronddesk";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.Complain>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var tmpAttachmentColl = beData.AttachmentColl;
                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var UserPhoto = filesColl["UserPhoto"];

                        if (UserPhoto != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                            beData.Photo = photoDoc.Data;
                            beData.PhotoPath = photoDoc.DocPath;
                        }

                        // Additional code for processing photos can go here if needed.

                        int fInd = 0;
                        foreach (var v in tmpAttachmentColl)
                        {
                            HttpPostedFileBase file = filesColl["file" + fInd];
                            if (file != null)
                            {
                                var att = GetAttachmentDocuments(photoLocation, file);

                                if (att != null)
                                {
                                    beData.AttachmentColl.Add
                                     (
                                      new Dynamic.BusinessEntity.GeneralDocument()
                                      {
                                          Data = att.Data,
                                          DocPath = att.DocPath,
                                          DocumentTypeId = v.DocumentTypeId,
                                          Extension = att.Extension,
                                          Name = v.Name,
                                          Description = v.Description
                                      }
                                     );
                                }
                            }
                            fInd++;
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                        {
                            if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                            {
                                //  v.Data = GetBytesFromFile(beData.DocPath);
                                beData.AttachmentColl.Add(v);
                            }
                        }
                    }

                    bool isModify = false;
                    if (!beData.ComplainId.HasValue)
                        beData.ComplainId = 0;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Complain(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.Complain)]
        public JsonNetResult getComplainById(int ComplainId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.Complain(User.UserId, User.HostName, User.DBName).GetComplainById(0, ComplainId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Complain)]
        public JsonNetResult DeleteComplain(int ComplainId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ComplainId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Complain(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, ComplainId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllComplain(DateTime? dateFrom, DateTime? dateTo, int? SourceId, int? ComplainTypeId, int? StatusId)
        {
            AcademicLib.BE.FrontDesk.Transaction.ComplainCollections dataColl = new AcademicLib.BE.FrontDesk.Transaction.ComplainCollections();
            try
            {
                dataColl = new AcademicLib.BL.FrontDesk.Transaction.Complain(User.UserId, User.HostName, User.DBName).GetAllComplain(0, dateFrom.Value, dateTo.Value, SourceId, ComplainTypeId, StatusId);
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
        public JsonNetResult SaveComplainReply()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.ComplainReply>(Request["jsonData"]);
                if (beData != null)
                {

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Complain(User.UserId, User.HostName, User.DBName).SaveComplainReply(beData);
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
        public JsonNetResult DelGatepass(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.GatePass(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "AttendanceFollowUp"
        public ActionResult AttendanceFollowUp()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveAttendanceFollowup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.AttendanceFollowUp>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.AttendanceFollowUp(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllAttendanceFollowUp(DateTime? DateFrom, DateTime? DateTo, int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId, int? ClassShiftId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AttendanceFollowUp(User.UserId, User.HostName, User.DBName).GetAllAttendanceFollowUp(DateFrom, DateTo, ClassId, SectionId, this.AcademicYearId, BatchId, SemesterId, ClassYearId, ClassShiftId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetAttendanceFollowup(int StudentId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.AttendanceFollowUp(User.UserId, User.HostName, User.DBName).getStudentAttendanceFollowup(StudentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.VisitorBook, false)]
        public JsonNetResult UpdateInTimeOfVisitorBook()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.Visitor>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Visitor(User.UserId, User.HostName, User.DBName).UpdateInTime(beData.VisitorId.Value, beData.InTime.Value, beData.OutTime.Value);
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
    }
}