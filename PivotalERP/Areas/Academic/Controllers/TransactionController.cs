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

namespace PivotalERP.Areas.Academic.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {

        public ActionResult BankAccount()
        {
            return View();
        }


        #region "NewBankAccount"

        [HttpPost]
        public JsonNetResult SaveNewBankAccount()
        {
            string photoLocation = "/Attachments/Frontdesk/NewBankAccount";

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.NewBankAccount>(Request["jsonData"]);
                if (beData != null)
                {
                    if(beData.StudentId.HasValue || beData.EmployeeId.HasValue)
                    {
                        var tmpAttachmentColl = beData.AttachmentColl;

                        beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                        if (Request.Files.Count > 0)
                        {
                            var filesColl = Request.Files;
                            var photo = filesColl["photo"];
                            var UserPhoto = filesColl["UserPhoto"];
                            var Signature = filesColl["Signature"];

                            if (UserPhoto != null)
                            {
                                var photoDoc = GetAttachmentDocuments(photoLocation, UserPhoto, true);
                                beData.Photo = photoDoc.Data;
                                beData.PhotoPath = photoDoc.DocPath;
                            }

                            if (Signature != null)
                            {
                                var photoDoc = GetAttachmentDocuments(photoLocation, Signature, true);
                                beData.Photo = photoDoc.Data;
                                beData.SPhotoPath = photoDoc.DocPath;
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

                            if (!string.IsNullOrEmpty(beData.attachFile) && beData.Photo == null)
                            {
                                if (beData.attachFile.StartsWith(photoLocation))
                                {
                                    beData.Photo = GetBytesFromFile(beData.attachFile);
                                }
                            }
                        }

                        string photoLocation1 = photoLocation.Replace("/", "\\");
                        foreach (var v in tmpAttachmentColl)
                        {
                            if (!string.IsNullOrEmpty(v.DocPath) && v.Data == null)
                            {
                                if (v.DocPath.StartsWith(photoLocation) || v.DocPath.StartsWith(photoLocation1))
                                {
                                    beData.AttachmentColl.Add(v);
                                }
                            }
                        }
                        bool isModify = false;
                        if (!beData.BankId.HasValue)
                            beData.BankId = 0;
                        else if (beData.BankId > 0)
                            isModify = true;



                        beData.CUserId = User.UserId;

                        if (!beData.BankId.HasValue)
                            beData.BankId = 0;

                        resVal = new AcademicLib.BL.Academic.Transaction.NewBankAccount(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    }
                    else
                    {
                        resVal.ResponseMSG = "Please ! Select Student or Employee";
                        resVal.IsSuccess = false;
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
        public JsonNetResult getNewBankAccountById(int BankId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.NewBankAccount(User.UserId, User.HostName, User.DBName).GetNewBankAccountById(0, BankId,null);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteNewBankAccount(int BankId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BankId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct NewBankAccount";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Academic.Transaction.NewBankAccount(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BankId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllNewBankAccount()
        {
            AcademicLib.BE.Academic.Transaction.NewBankAccountCollections dataColl = new AcademicLib.BE.Academic.Transaction.NewBankAccountCollections();
            try
            {
                dataColl = new AcademicLib.BL.Academic.Transaction.NewBankAccount(User.UserId, User.HostName, User.DBName).GetAllNewBankAccount(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        // GET: Academic/Transaction
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AddStudent, false,0, (int)ENTITIES.Admission)]
        public ActionResult AddStudent(int? AdmissionEnquiryId,int? RegistrationId)
        {
            if(AdmissionEnquiryId.HasValue)
                ViewBag.AdmissionEnquiryId = AdmissionEnquiryId;            
            else
                ViewBag.AdmissionEnquiryId = 0;

            if (RegistrationId.HasValue)
                ViewBag.RegistrationId = RegistrationId;
            else
                ViewBag.RegistrationId = 0;

            return View();
        }

        [HttpPost] 
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddStudent, false, 0, (int)ENTITIES.Admission)]
        public JsonNetResult SaveStudent()
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

                        var citizentfront = filesColl["citizentfront"];
                        var citizentBack = filesColl["citizentBack"];
                        var nidphoto = filesColl["nidphoto"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo,true);
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

                        if (citizentfront != null)
                        {
                            var citizentfrontDoc = GetAttachmentDocuments(photoLocation, citizentfront, true);
                            beData.Photo = citizentfrontDoc.Data;
                            beData.CitizenFrontPhoto = citizentfrontDoc.DocPath;
                        }

                        if (citizentBack != null)
                        {
                            var citizentBackDoc = GetAttachmentDocuments(photoLocation, citizentBack, true);
                            beData.Photo = citizentBackDoc.Data;
                            beData.CitizenBackPhoto = citizentBackDoc.DocPath;
                        }

                        if (nidphoto != null)
                        {
                            var nidphotoDoc = GetAttachmentDocuments(photoLocation, nidphoto, true);
                            beData.Photo = nidphotoDoc.Data;
                            beData.NIDPhoto = nidphotoDoc.DocPath;
                        }

                        int fInd = 0;
                        foreach(var v in tmpAttachmentColl)
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
                                         DocumentTypeId=v.DocumentTypeId,
                                          Extension=att.Extension,
                                           Name=v.Name,
                                           Description=v.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }


                    if (!string.IsNullOrEmpty(beData.PhotoPath) && beData.Photo==null)
                    {
                        if (beData.PhotoPath.StartsWith(photoLocation))
                        {
                            beData.Photo = GetBytesFromFile(beData.PhotoPath);
                        }
                    }

                    if (!string.IsNullOrEmpty(beData.SignaturePath) && beData.Signature==null)
                    {
                        if (beData.SignaturePath.StartsWith(photoLocation))
                        {
                            beData.Signature = GetBytesFromFile(beData.PhotoPath);
                        }
                    }

                    string photoLocation1 = photoLocation.Replace("/", "\\");
                    foreach (var v in tmpAttachmentColl)
                    {
                        if (!string.IsNullOrEmpty(v.DocPath) && v.Data==null)
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

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.StudentId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StudentProfile;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "StudentProfile Modify" : "New  StudentProfile ");
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddStudent, false)]
        public JsonNetResult UpdateStuentPhoto()
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
                     
                        if (photo != null && beData.StudentId.HasValue && beData.StudentId>0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);                            
                            beData.PhotoPath = photoDoc.DocPath;                       

                            beData.CUserId = User.UserId;
                            resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateStudentPhoto(beData.StudentId.Value, photoDoc.DocPath);

                            if (resVal.IsSuccess)
                            {
                                resVal.ResponseId = photoDoc.DocPath;
                                Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                                auditLog.TranId =  beData.StudentId.Value ;
                                auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StudentProfile;
                                auditLog.Action = Actions.Modify;
                                auditLog.LogText = "Student Photo Update";
                                auditLog.AutoManualNo = beData.StudentId.ToString();
                                SaveAuditLog(auditLog);
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
        public JsonNetResult SaveStudentLeft()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {            
                var beData = DeserializeObject<AcademicLib.RE.Academic.StudentDetailsForLeftCollections>(Request["jsonData"]);
                if (beData != null && beData.Count>0)
                {                                       
                    resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateStudentLeft(this.AcademicYearId, beData);
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
        public JsonNetResult SaveStudentBoardReg()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.RE.Academic.StudentDetailsForLeftCollections>(Request["jsonData"]);
                if (beData != null)
                {                    
                    resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateBoardRegdNo(beData);
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
        public JsonNetResult GetStudentAutoNumber()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getAutoRegdNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentAutoRollNo(int? ClassId,int? SectionId,int? BatchId,int? SemesterId,int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getAutoRollNo(ClassId, SectionId,BatchId,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentForTran(int ClassId, int? SectionId, int? ExamTypeId, int? BranchId = null, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentForTran(this.AcademicYearId, ClassId, SectionId, ExamTypeId, BranchId, BatchId, ClassYearId, SemesterId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllStudentForTran()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getAllStudentForTran(this.AcademicYearId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSB_StudentForTran(int? ClassId,int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getAllStudentForTran(this.AcademicYearId, ClassId,SectionId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassWiseStudentList(int ClassId, string SectionIdColl, bool FilterSection = true, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null, int? BranchId = null, int? FacultyId = null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getClassWiseStudentList(this.AcademicYearId, ClassId, SectionIdColl, FilterSection, SemesterId, ClassYearId, BatchId, null, BranchId, FacultyId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassWiseStudentForLeft(int ClassId, int? SectionId, bool All = true, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null,int? BranchId=null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentListForBoardRegdNo(this.AcademicYearId, ClassId, SectionId, All, SemesterId, ClassYearId, typeId, BatchId,BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassWiseStudentForOpening(int ClassId, int? SectionId, bool All = true, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null, int? BranchId = null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentListForOpening(this.AcademicYearId, ClassId, SectionId, All, SemesterId, ClassYearId, typeId, BatchId, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AddStudent, false)]
        public JsonNetResult GetStudentById(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).GetStudentById(0, StudentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentSiblingDetails(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentSiblingDetails( StudentId,this.AcademicYearId);
            var resultObj = new
            {
                SiblingDetails = dataColl,                // List<SiblingDetail>
                AcademicDetails = dataColl.AcademicDetails // List<StudentAcademicDetail>
            };

            return new JsonNetResult() { Data = resultObj, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.AddStudent, false)]
        public JsonNetResult DelStudent(int StudentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).DeleteById(0, StudentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.UpdateStudent, false)]
        public ActionResult UpdateStudent()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult UpdateClassWiseST(List<AcademicLib.BE.Academic.Transaction.Student> studentColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateClassWiseStudent(studentColl);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetStudentForUpdate(int ClassId, int? SectionId,int? BatchId,int? ClassYearId,int? SemesterId,int? BranchId=null)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getClassWiseStudentForUpdate(this.AcademicYearId, ClassId, SectionId, BatchId, ClassYearId, SemesterId,BranchId);

                var retVal = new
                {
                    IsSuccess = true,
                    ResponseMSG = GLOBALMSG.SUCCESS,
                    DataColl=dataColl
                };

                return new JsonNetResult() { Data = retVal, TotalCount = 0, IsSuccess = retVal.IsSuccess, ResponseMSG = retVal.ResponseMSG };
            }
            catch(Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
           
            
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddEmployee, false)]
        public ActionResult Employee()
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
                        var citizentfront = filesColl["citizentfront"];
                        var citizentBack = filesColl["citizentBack"];
                        var nidphoto = filesColl["nidphoto"];

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

                        if (citizentfront != null)
                        {
                            var citizentfrontDoc = GetAttachmentDocuments(photoLocation, citizentfront, true);
                            beData.Photo = citizentfrontDoc.Data;
                            beData.CitizenFrontPhoto = citizentfrontDoc.DocPath;
                        }

                        if (citizentBack != null)
                        {
                            var citizentBackDoc = GetAttachmentDocuments(photoLocation, citizentBack, true);
                            beData.Photo = citizentBackDoc.Data;
                            beData.CitizenBackPhoto = citizentBackDoc.DocPath;
                        }

                        if (nidphoto != null)
                        {
                            var nidphotoDoc = GetAttachmentDocuments(photoLocation, nidphoto, true);
                            beData.Photo = nidphotoDoc.Data;
                            beData.NIDPhoto = nidphotoDoc.DocPath;
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
                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddEmployee, false)]
        public JsonNetResult UpdateEmployeePhoto()
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

                        if (photo != null && beData.EmployeeId.HasValue && beData.EmployeeId > 0)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.PhotoPath = photoDoc.DocPath;

                            beData.CUserId = User.UserId;
                            resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).UpdateEmployeePhoto(beData.EmployeeId.Value, photoDoc.DocPath);

                            if (resVal.IsSuccess)
                            {
                                resVal.ResponseId = photoDoc.DocPath;
                                Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                                auditLog.TranId = beData.EmployeeId.Value;
                                auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.EmployeeProfile;
                                auditLog.Action = Actions.Modify;
                                auditLog.LogText = "Employee Photo Update";
                                auditLog.AutoManualNo = beData.EmployeeId.ToString();
                                SaveAuditLog(auditLog);
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
        public JsonNetResult SaveLeftEmployee()
        {
            string photoLocation = "/Attachments/academic/employee";
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.EmployeeLeft>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;

                        for (int fInd = 0; fInd < Request.Files.Count; fInd++)
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
                                         DocumentTypeId = att.DocumentTypeId,
                                         Extension = att.Extension,
                                         Name = att.Name,
                                         Description = att.Description
                                     }
                                    );
                            }
                        }
                    }
                
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).SaveLeftEmployee(beData);
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
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getAutoRegdNo();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AddEmployee, false)]
        public JsonNetResult GetEmployeeById(int EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getEmployeeById(0, EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetLeftEmployeeById(int EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getLeftEmployeeById( EmployeeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        public JsonNetResult DelLeftEmployee(int EmployeeId,int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).DeleteLeftEmp(TranId, EmployeeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.AddEmployee, false)]
        public JsonNetResult DelEmployee(int EmployeeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).DeleteById(0, EmployeeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetLeftEmployeeList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getLeftEmployeeList();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpListForClassTeacher(int ClassId, int? SectionId, int? SemesterId = null, int? ClassYearId = null, int? BatchId = null,int? SubjectId=null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getEmpListForClassTeacher(ClassId, SectionId, SemesterId, ClassYearId, BatchId,SubjectId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllEmpShortList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getAllEmpShortList();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllEmpForSelection(int For)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getAllEmployeeForSelection(For);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost] 
        public JsonNetResult SaveEmployeeRemarks()
        {
            string photoLocation = "/Attachments/academic/employee";
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.EmployeeRemarks>(Request["jsonData"]);
                if (beData != null)
                { 
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files["file0"];
                        if (file != null)
                        {
                            var att = GetAttachmentDocuments(photoLocation, file);
                            beData.FilePath = att.DocPath;
                        }
                    }
                      
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResultWithEnum GetEmpRemarks(int EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(User.UserId, User.HostName, User.DBName).getRemarksList(DateTime.Today, DateTime.Today, null, EmployeeId, false,null);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpRemarksList(DateTime dateFrom,DateTime dateTo,int? remarksTypeId,int? remarksFor)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.EmployeeRemarks(User.UserId, User.HostName, User.DBName).getRemarksList(dateFrom,dateTo,remarksTypeId,null,false,remarksFor);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "TC"

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.TC_CC, false)]
        public ActionResult TCCC()
        {
            return View();
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.TC_CC, false)]
        public JsonNetResult SaveTC()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.TC>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.TC(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetStudentDetForTCCC(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.TC(User.UserId, User.HostName, User.DBName).getStudentDetailsForTCCC(StudentId,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTCForEdit(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.TC(User.UserId, User.HostName, User.DBName).getTCByStudentId(StudentId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllTCList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.TC(User.UserId, User.HostName, User.DBName).getAllTC();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllTCRequest(int ReportType)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.TC(User.UserId, User.HostName, User.DBName).getAllRequest(this.AcademicYearId,ReportType);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.TC_CC, false)]
        public JsonNetResult DelTC(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.TC(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "CC"

        [HttpPost]

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.TC_CC, false)]
        public JsonNetResult SaveCC()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.TC>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.CC(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetStudentDetForCC(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.CC(User.UserId, User.HostName, User.DBName).getStudentDetailsForTCCC(StudentId,this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetCCForEdit(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.CC(User.UserId, User.HostName, User.DBName).getCCByStudentId(StudentId, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllCCList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.CC(User.UserId, User.HostName, User.DBName).getAllCC();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

       
        [HttpPost]

        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.TC_CC, false)]
        public JsonNetResult DelCC(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.CC(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion


        // GET: Health/Creation
        public ActionResult HealthCampaign()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetHealthRpt(int? StudentId, int? EmployeeId)
        {
            AcademicLib.RE.Health.HealthReport dataColl = new AcademicLib.RE.Health.HealthReport();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.HealthCampaign(User.UserId, User.HostName, User.DBName).getHealthReport(StudentId, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #region HealthCampaign

        [HttpPost]
        public JsonNetResult SaveHealthCampaign()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Health.Transaction.HealthCampaign>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.Health.Transaction.HealthCampaign(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getHealthCampaignById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Health.Transaction.HealthCampaign(User.UserId, User.HostName, User.DBName).GetHealthCampaignById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteHealthCampaign(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Health.Transaction.HealthCampaign(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllHealthCampaign()
        {
            AcademicERP.BE.Health.Transaction.HealthCampaignCollections dataColl = new AcademicERP.BE.Health.Transaction.HealthCampaignCollections();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.HealthCampaign(User.UserId, User.HostName, User.DBName).GetAllHealthCampaign(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        public ActionResult HealthReport()
        {
            return View();
        }

        #region MonthlyTest

        [HttpPost]
        public JsonNetResult SaveMonthlyTest()
        {
            string photoLocation = "/Attachments/Health";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Health.Transaction.MonthlyTest>(Request["jsonData"]);
                if (beData != null)
                {
                    var tmpAttachmentColl = beData.AttachMonTesColl;

                    beData.AttachMonTesColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
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
                                beData.AttachMonTesColl.Add
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
                                beData.AttachMonTesColl.Add(v);
                            }
                        }
                    }


                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.Health.Transaction.MonthlyTest(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getMonthlyTestById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Health.Transaction.MonthlyTest(User.UserId, User.HostName, User.DBName).GetMonthlyTestById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteMonthlyTest(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Health.Transaction.MonthlyTest(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllMonthlyTest(int? StudentId,int? EmployeeId)
        {
            AcademicERP.BE.Health.Transaction.MonthlyTestCollections dataColl = new AcademicERP.BE.Health.Transaction.MonthlyTestCollections();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.MonthlyTest(User.UserId, User.HostName, User.DBName).GetAllMonthlyTest(0, StudentId, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region HealthGrowth

        [HttpPost]
        public JsonNetResult SaveHealthGrowth()
        {
            string photoLocation = "/Attachments/Health";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Health.Transaction.HealthGrowth>(Request["jsonData"]);
                if (beData != null)
                {

                    var tmpAttachmentColl = beData.AttachHelGroColl;

                    beData.AttachHelGroColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
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
                                beData.AttachHelGroColl.Add
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
                                beData.AttachHelGroColl.Add(v);
                            }
                        }
                    }


                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.Health.Transaction.HealthGrowth(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getHealthGrowthById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Health.Transaction.HealthGrowth(User.UserId, User.HostName, User.DBName).GetHealthGrowthById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteHealthGrowth(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Health.Transaction.HealthGrowth(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllHealthGrowth(int? StudentId, int? EmployeeId)
        {
            AcademicERP.BE.Health.Transaction.HealthGrowthCollections dataColl = new AcademicERP.BE.Health.Transaction.HealthGrowthCollections();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.HealthGrowth(User.UserId, User.HostName, User.DBName).GetAllHealthGrowth(0, StudentId, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region StoolTest

        [HttpPost]
        public JsonNetResult SaveStoolTest()
        {
            string photoLocation = "/Attachments/Health";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Health.Transaction.StoolTest>(Request["jsonData"]);
                if (beData != null)
                {

                    var tmpAttachmentColl = beData.AttachStTeColl;

                    beData.AttachStTeColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
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
                                beData.AttachStTeColl.Add
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
                                beData.AttachStTeColl.Add(v);
                            }
                        }
                    }
                     
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.Health.Transaction.StoolTest(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getStoolTestById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Health.Transaction.StoolTest(User.UserId, User.HostName, User.DBName).GetStoolTestById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteStoolTest(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Health.Transaction.StoolTest(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllStoolTest(int? StudentId, int? EmployeeId)
        {
            AcademicERP.BE.Health.Transaction.StoolTestCollections dataColl = new AcademicERP.BE.Health.Transaction.StoolTestCollections();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.StoolTest(User.UserId, User.HostName, User.DBName).GetAllStoolTest(0,StudentId,EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region UrineTest

        [HttpPost]
        public JsonNetResult SaveUrineTest()
        {
            string photoLocation = "/Attachments/Health";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Health.Transaction.UrineTest>(Request["jsonData"]);
                if (beData != null)
                {

                    var tmpAttachmentColl = beData.AttachUrTeColl;

                    beData.AttachUrTeColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
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
                                beData.AttachUrTeColl.Add
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
                                beData.AttachUrTeColl.Add(v);
                            }
                        }
                    }
                     

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.Health.Transaction.UrineTest(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getUrineTestById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Health.Transaction.UrineTest(User.UserId, User.HostName, User.DBName).GetUrineTestById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteUrineTest(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Health.Transaction.UrineTest(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllUrineTest(int? StudentId, int? EmployeeId)
        {
            AcademicERP.BE.Health.Transaction.UrineTestCollections dataColl = new AcademicERP.BE.Health.Transaction.UrineTestCollections();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.UrineTest(User.UserId, User.HostName, User.DBName).GetAllUrineTest(0, StudentId, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region GeneralHealth

        [HttpPost]
        public JsonNetResult SaveGeneralHealth()
        {
            string photoLocation = "/Attachments/Health";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicERP.BE.Health.Transaction.GeneralHealth>(Request["jsonData"]);
                if (beData != null)
                {

                    var tmpAttachmentColl = beData.AttachGenHeColl;

                    beData.AttachGenHeColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
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
                                beData.AttachGenHeColl.Add
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
                                beData.AttachGenHeColl.Add(v);
                            }
                        }
                    }
                     

                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicERP.BL.Health.Transaction.GeneralHealth(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getGeneralHealthById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicERP.BL.Health.Transaction.GeneralHealth(User.UserId, User.HostName, User.DBName).GetGeneralHealthById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteGeneralHealth(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicERP.BL.Health.Transaction.GeneralHealth(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllGeneralHealth(int? StudentId, int? EmployeeId)
        {
            AcademicERP.BE.Health.Transaction.GeneralHealthCollections dataColl = new AcademicERP.BE.Health.Transaction.GeneralHealthCollections();
            try
            {
                dataColl = new AcademicERP.BL.Health.Transaction.GeneralHealth(User.UserId, User.HostName, User.DBName).GetAllGeneralHealth(0, StudentId, EmployeeId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "ExtraEntity"

        public ActionResult ExtraEntity()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveExtraEntity()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ExtraEntity>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ExtraEntity(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getExtraEntityById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ExtraEntity(User.UserId, User.HostName, User.DBName).GetExtraEntityById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteExtraEntity(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Academic.Transaction.ExtraEntity(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllExtraEntity()
        {
            AcademicLib.BE.Academic.Transaction.ExtraEntityCollections dataColl = new AcademicLib.BE.Academic.Transaction.ExtraEntityCollections();
            try
            {
                dataColl = new AcademicLib.BL.Academic.Transaction.ExtraEntity(User.UserId, User.HostName, User.DBName).GetAllExtraEntity(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        #region "ExtraCertificateIssue"

        [HttpGet]
        public JsonNetResult getExtraCertificateNo(int ExtraEntityId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ExtraCertificateIssue(User.UserId, User.HostName, User.DBName).getAutoNo(ExtraEntityId,this.AcademicYearId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveExtraCertificateIssue()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ExtraCertificateIssue>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ExtraCertificateIssue(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getExtraCertificateIssueById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ExtraCertificateIssue(User.UserId, User.HostName, User.DBName).GetExtraCertificateIssueById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteExtraCertificateIssue(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Academic.Transaction.ExtraCertificateIssue(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllExtraCertificateIssue()
        {
            AcademicLib.BE.Academic.Transaction.ExtraCertificateIssueCollections dataColl = new AcademicLib.BE.Academic.Transaction.ExtraCertificateIssueCollections();
            try
            {
                dataColl = new AcademicLib.BL.Academic.Transaction.ExtraCertificateIssue(User.UserId, User.HostName, User.DBName).GetAllExtraCertificateIssue(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "Banking"

        public ActionResult NICBankAccount()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentForAccountOpen()
        {

            var dataColl = new NICBank.BL.NewAccount(User.UserId, User.HostName, User.DBName).getStudentListForAccountOpen(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpForAccountOpen()
        {

            var dataColl = new NICBank.BL.NewAccount(User.UserId, User.HostName, User.DBName).getEmpListForAccountOpen(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult OpenStudentAccount(List<NICBank.BE.IdForNewAccount> studentIdColl)
        {

            var dataColl = new NICBank.BL.NewAccount(User.UserId, User.HostName, User.DBName).OpenStudentAccount(studentIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult OpenEmpAccount(List<NICBank.BE.IdForNewAccount> empIdColl)
        {

            var dataColl = new NICBank.BL.NewAccount(User.UserId, User.HostName, User.DBName).OpenEmpAccount(empIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult CheckBAStatus(List<NICBank.BE.IdForNewAccount> bankIdColl)
        {

            var dataColl = new NICBank.BL.NewAccount(User.UserId, User.HostName, User.DBName).AccountStatus(bankIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion


        [HttpPost]
        public JsonNetResult SavePassoutStudents()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.PassoutStudentsCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.PassoutStudents(User.UserId, User.HostName, User.DBName).UpdatePassoutStudents(beData);
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
        public JsonNetResult GetStudentForPassout(int ClassId, int? SectionId, bool All = true, int? SemesterId = null, int? ClassYearId = null, int? typeId = null, int? BatchId = null,int? BranchId=null)
        {
            AcademicLib.BE.StudentsForPassoutCollections dataColl = new AcademicLib.BE.StudentsForPassoutCollections();
            try
            {
                dataColl = new AcademicLib.BL.PassoutStudents(User.UserId, User.HostName, User.DBName).GetStudentForPassout(ClassId, SectionId, this.AcademicYearId, All, SemesterId, ClassYearId, typeId, BatchId,BranchId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        //public ActionResult LeftEmployee()
        //{
        //    return View();
        //}
        public ActionResult UpdateEmployee()
        {
            return View();
        }
        public ActionResult EmployeeRemarks()
        {
            return View();
        }

        public ActionResult BirthdayList()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetEmployeeForUpdate(int? DepartmentId, int? DesignationId, int? CategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getEmployeeListForupdate(DepartmentId, DesignationId, CategoryId);

                var retVal = new
                {
                    IsSuccess = true,
                    ResponseMSG = GLOBALMSG.SUCCESS,
                    DataColl = dataColl
                };

                return new JsonNetResult() { Data = retVal, TotalCount = 0, IsSuccess = retVal.IsSuccess, ResponseMSG = retVal.ResponseMSG };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult UpdateEmployee(List<AcademicLib.BE.Academic.Transaction.Employee> employeeColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).UpdateEmployee(employeeColl);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddStudent, false, 0, (int)ENTITIES.Admission)]
        public JsonNetResult UpgradeClassSection()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.Student>(Request["jsonData"]);
                if (beData != null)
                {

                    bool isModify = false;
                    if (!beData.StudentId.HasValue)
                        beData.StudentId = 0;
                    else if (beData.StudentId > 0)
                        isModify = true;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpgradeClassSection(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.StudentId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StudentProfile;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Student Class & Section Modify" : "New  Student Class & Section ");
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AddStudent, false, 0, (int)ENTITIES.Admission)]
        public JsonNetResult UpdateStudentDocument()
        {
            ResponeValues resVal = new ResponeValues();
            string photoLocation = "/Attachments/academic/student";
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

                    bool isModify = false;
                    if (!beData.StudentId.HasValue)
                        beData.StudentId = 0;
                    else if (beData.StudentId > 0)
                        isModify = true;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateStudentDocument(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.StudentId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.StudentProfile;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Student Document Modify" : "New  Student Document ");
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

        //Added By Suresh on Chaitra 27 Starts
        public ActionResult ContinousConfirmation()
        {
            return View();
        }

        #region "ContinuousConfirmation"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.ContinuousConfirmation)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ContinuousConfirmation)]
        public JsonNetResult SaveUpdateContinuousConfirmation()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.ContinuousConfirmation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Academic.Transaction.ContinuousConfirmation(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.ContinuousConfirmation)]
        public JsonNetResult getContinuousConfirmationById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.ContinuousConfirmation(User.UserId, User.HostName, User.DBName).GetContinuousConfirmationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.ContinuousConfirmation)]
        public JsonNetResult DeleteContinuousConfirmation(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Academic.Transaction.ContinuousConfirmation(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllContinuousConfirmation(int? ClassId, int? SectionId)
        {
            AcademicLib.BE.Academic.Transaction.ContinuousConfirmationCollections dataColl = new AcademicLib.BE.Academic.Transaction.ContinuousConfirmationCollections();
            try
            {
                dataColl = new AcademicLib.BL.Academic.Transaction.ContinuousConfirmation(User.UserId, User.HostName, User.DBName).GetAllContinuousConfirmation(0, ClassId, SectionId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

    }
}