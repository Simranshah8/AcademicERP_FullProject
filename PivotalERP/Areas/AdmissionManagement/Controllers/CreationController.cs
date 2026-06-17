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

namespace PivotalERP.Areas.AdmissionManagement.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Admission, false)]
        public ActionResult Admission(int? AdmissionEnquiryId, int? RegistrationId)
        {
            if (AdmissionEnquiryId.HasValue)
                ViewBag.AdmissionEnquiryId = AdmissionEnquiryId;
            else
                ViewBag.AdmissionEnquiryId = 0;

            if (RegistrationId.HasValue)
                ViewBag.RegistrationId = RegistrationId;
            else
                ViewBag.RegistrationId = 0;


            return View();
        }
        [PermissionsAttribute(Actions.View, (int)ENTITIES.EnquiryCouncelling, false)]
        public ActionResult EnquiryCouncelling()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetRegCouncellingStatuses(int? EmpId)
        {

            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).GetEmpCouncellingStatuses(this.AcademicYearId, EmpId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.RegistrationCouncelling, false)]
        public ActionResult RegistrationCouncelling()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.AdmissionConfirmation, false)]
        public ActionResult AdmissionConfirmation()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetAdmissionFollowup(int FollowupType)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getAdmissionForFollowup(this.AcademicYearId, FollowupType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetRegAdmitStudent()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getRegAdmitStudent(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.StudentEligibility, false)]
        public JsonNetResult SaveEligibleFeeReceipt()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.RegistrationEligibility>(Request["jsonData"]);
                if (beData != null)
                { 
                    beData.CUserId = User.UserId; 
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveEligibleFeeReceipt(this.AcademicYearId, beData);
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.StudentEligibility, false)]
        public ActionResult StudentEligibility()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetStudentUserList(int? ClassId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getStudentUserList(this.AcademicYearId, ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GenerateStudentUser(int AsPer, bool canUpdate, string Prefix, string Suffix)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).GenerateUser(this.AcademicYearId, AsPer, canUpdate, Prefix, Suffix);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        // GET: AdmissionManagement/Creation
        public ActionResult Dashboard()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.AdmissionEnquiry, false)]
        public ActionResult Enquiry()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Registration, false)]
        public ActionResult Registration(int? EnquiryTranId)
        {
            if (!EnquiryTranId.HasValue)
                EnquiryTranId = 0;

            ViewBag.AdmissionEnquiryId = EnquiryTranId;
            return View();
        }

      
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Registration, false)]
        public JsonNetResult SaveRegistration()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.Student>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        var signature = filesColl["signature"];

                        var father = filesColl["father"];
                        var mother = filesColl["mother"];
                        var guardian = filesColl["guardian"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.Photo = photoDoc.Data;
                            beData.PhotoPath = photoDoc.DocPath;
                        }

                        if (father != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, father, true);
                            beData.FatherPhotoPath = photoDoc.DocPath;
                        }

                        if (mother != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, mother, true);
                            beData.MotherPhotoPath = photoDoc.DocPath;
                        }

                        if (guardian != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, guardian, true);
                            beData.GuardianPhotoPath = photoDoc.DocPath;
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


                    if (!string.IsNullOrEmpty(beData.PhotoPath) && beData.Photo == null)
                    {
                        if (beData.PhotoPath.StartsWith(photoLocation))
                        {
                            beData.Photo = GetBytesFromFile(beData.PhotoPath);
                        }
                    }

                    if (!string.IsNullOrEmpty(beData.SignaturePath) && beData.Signature == null)
                    {
                        if (beData.SignaturePath.StartsWith(photoLocation))
                        {
                            beData.Signature = GetBytesFromFile(beData.PhotoPath);
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
                    if (!beData.StudentId.HasValue)
                        beData.StudentId = 0;
                    else if (beData.StudentId > 0)
                        isModify = true;

                    if (!beData.AcademicYear.HasValue || beData.AcademicYear == 0)
                        beData.AcademicYear = this.AcademicYearId;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.StudentId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Registration;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Registration Modify" : "New  Registration ");
                        auditLog.AutoManualNo = beData.StudentId.ToString();
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Registration, false)]
        public JsonNetResult SaveRegEligible()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.RegistrationEligibility>(Request["jsonData"]);
                if (beData != null)
                {
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
                     
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveUpdateEligible(beData);                     

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
        public JsonNetResult GetRegistrationAutoNumber()
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getAutoRegdNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Registration, false)]
        public JsonNetResult GetRegistrationById(int StudentId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).GetStudentById(0, StudentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.Registration, false)]
        public JsonNetResult DelRegistration(int StudentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).DeleteById(0, StudentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetRegSummary(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!dateFrom.HasValue)
                dateFrom = DateTime.Today;

            if (!dateTo.HasValue)
                dateTo = DateTime.Today;

            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getRegSummary(dateFrom.Value, dateTo.Value);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Registration, false)]
        public JsonNetResult SaveRegAssignCounselor(int TranId,DateTime? AssignDate, List<int> EmployeeIdColl)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveAssignCounselor(TranId,AssignDate,EmployeeIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetReqFollowup(int FollowupType)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getRegForFollowup(this.AcademicYearId, FollowupType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetRegFollowupList(int TranId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getFollowupList(TranId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Registration, false)]
        public JsonNetResult SaveRegistrationFollowup()
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
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveFollowup(beData);
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
        public JsonNetResult GetAdmitFollowupList(int TranId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getAdmitFollowupList(TranId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AdmissionConfirmation, false)]
        public JsonNetResult SaveAdmitFollowup()
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
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveAdmitFollowup(beData);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Registration, false)]
        public JsonNetResult SaveRegStatus(int TranId, int Status, string Remarks)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveEnqStatus(TranId, Status, Remarks);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AdmissionConfirmation, false)]
        public JsonNetResult SaveAdmitStatus(int TranId, int Status, string Remarks)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).SaveAdmitStatus(TranId, Status, Remarks);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetRegSummaryForEligible(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!dateFrom.HasValue)
                dateFrom = DateTime.Today;

            if (!dateTo.HasValue)
                dateTo = DateTime.Today;

            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getRegSummaryForEligible(dateFrom.Value, dateTo.Value);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetRegSummaryForAdmitConfirm()
        { 

            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.Registration(User.UserId, User.HostName, User.DBName).getRegSummaryForAdmitConfirm();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult EntranceExam()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult Confirmation()
        {
            return View();
        }
        public ActionResult SendEmailSMS()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Setup()
        {
            return View();
        }

        #region "CommunicationType"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AdmissionSetup, false)]
        public JsonNetResult SaveCommunicationType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            { 
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.CommunicationType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.CommunicationTypeId.HasValue)
                        beData.CommunicationTypeId = 0;

                    resVal = new AcademicLib.BL.FrontDesk.Transaction.CommunicationType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllCommunicationTypeList(bool ForTran=true)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.CommunicationType(User.UserId, User.HostName, User.DBName).GetAllCommunicationType(0,ForTran);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCommunicationTypeById(int CommunicationTypeId)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.CommunicationType(User.UserId, User.HostName, User.DBName).GetCommunicationTypeById(0, CommunicationTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelCommunicationType(int CommunicationTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.FrontDesk.Transaction.CommunicationType(User.UserId, User.HostName, User.DBName).DeleteById(0, CommunicationTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "EnquiryNumberMethod"

        [HttpPost]
        [ValidateInput(false)]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AdmissionSetup, false)]
        public JsonNetResult SaveEnquiryNumberMethod()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.EnquiryNumberMethod>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.EnquiryNumberMethod(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetEnquiryNumberMethod(int? BranchId = null)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.EnquiryNumberMethod(User.UserId, User.HostName, User.DBName).GetConfiguration(0, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #endregion

        #region "EnquiryNumberMethod"

        [HttpPost]
        [ValidateInput(false)]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AdmissionSetup, false)]
        public JsonNetResult SaveRegistrationNumberMethod()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.FrontDesk.Transaction.RegistrationNumberMethod>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.FrontDesk.Transaction.RegistrationNumberMethod(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetRegistrationNumberMethod(int? BranchId = null)
        {
            var dataColl = new AcademicLib.BL.FrontDesk.Transaction.RegistrationNumberMethod(User.UserId, User.HostName, User.DBName).GetConfiguration(0, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #endregion

        //Added By Suresh for Entrance Card Starts
        public ActionResult EntranceCard()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveEntranceSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Exam.Setup.EntranceSetup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Exam.Setup.EntranceSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData, User.BranchId, this.AcademicYearId);
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
        public JsonNetResult GetEntranceSetup()
        {
            var dataColl = new AcademicLib.BL.Exam.Setup.EntranceSetup(User.UserId, User.HostName, User.DBName).GetEntranceSetup(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetDataForEntranceCard(DateTime? DateFrom, DateTime? DateTo)
        {
            var dataColl = new AcademicLib.BL.Exam.Setup.EntranceSetup(User.UserId, User.HostName, User.DBName).GetDataForEntranceCard(DateFrom, DateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #region "EntanceExamResult"
        public ActionResult EntranceExamResult()
        {
            return View();
        }

        [HttpPost]     
        public JsonNetResult GetEntranceResult(int? ClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.EntranceExamResult(User.UserId, User.HostName, User.DBName).GetEntranceResult(0, ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveEntranceMarkEntry()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Exam.Transaction.EntranceMarkEntry>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.EntranceExamResult(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetEntranceSymbolNo(int? ClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.EntranceSymbolNo(User.UserId, User.HostName, User.DBName).GetEntranceSymboolNo(ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveEntranceSymbolNo()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Exam.Transaction.EntranceSymbolNo>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.EntranceSymbolNo(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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

        //add by prashant Baishk 02


        [HttpPost]
        public JsonNetResult GetMarkSetup(int? ClassId)
        {
            var dataColl = new AcademicLib.BL.Exam.Transaction.EntranceExamResult(User.UserId, User.HostName, User.DBName).GetMarkSetup(0, ClassId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveMarkSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<List<AcademicLib.BE.Exam.Transaction.EntranceMarkSetup>>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Exam.Transaction.EntranceExamResult(User.UserId, User.HostName, User.DBName).SaveMarkSetup(beData);
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

        #region "EnquiryFormConfig"
        [HttpPost]
        public JsonNetResult SaveEnquiryFormConfig()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Creation.EnquiryFormConfig>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Creation.EnquiryFormConfig(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        [AllowAnonymous]
        public JsonNetResult GetAllEnquiryFormConfig()
        {
            if (User == null)
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.EnquiryFormConfig(1, hostName,dbName).GetAllEnquiryFormConfig(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {
                var dataColl = new AcademicLib.BL.Academic.Creation.EnquiryFormConfig(User.UserId, User.HostName, User.DBName).GetAllEnquiryFormConfig(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }

            
        }

        #endregion
    }
}