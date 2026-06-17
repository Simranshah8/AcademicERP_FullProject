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
namespace PivotalERP.Areas.HomeWork.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {
        // GET: HomeWork/Transaction
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Homework, false)]
        public ActionResult NewHomeWork()
        {
            return View();
        }
        #region "HomeWork"

        
        [HttpPost,ValidateInput(false)]
        public JsonNetResult SaveHomeWork()
        {
            string photoLocation = "/Attachments/homework";
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.HomeWork>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HomeWorkId.HasValue)
                        beData.HomeWorkId = 0;

                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;                       
                        int fInd = 0;
                        foreach (var v in filesColl)
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
                                         DocumentTypeId = null,
                                         Extension = att.Extension,
                                         Name = att.Name,
                                         Description = att.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }

                    resVal = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
                    if (resVal.IsSuccess && !string.IsNullOrEmpty(resVal.ResponseId))
                    {
                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                        notification.Content = resVal.ResponseMSG;
                        notification.ContentPath = resVal.RId.ToString();
                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK);
                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.HOMEWORK.ToString();
                        notification.Heading = "Homework";
                        notification.Subject = beData.Topic;
                        notification.UserId = User.UserId;
                        notification.UserName = User.Identity.Name;
                        notification.UserIdColl = resVal.ResponseId.Trim();

                        resVal = new PivotalERP.Global.GlobalFunction(User.UserId, hostName, dbName).SendNotification(User.UserId, notification, false);

                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                    }
                    else
                    {                       
                        resVal.ResponseMSG = "No Student Found On this Class ";

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
        public JsonNetResult GetAllHomeWorkList(DateTime? dateFrom, DateTime? dateTo, int? classId, int? sectionId, int? subjectId, int? employeeId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).GetAllHomeWork(0, dateFrom, dateTo, false, null, classId, sectionId, subjectId, employeeId, BatchId, SemesterId, ClassYearId);

            //int hwCount = dataColl.Count;
            //var query = from dc in dataColl
            //            group dc by new { dc.ClassName, dc.SectionName } into g
            //            select new
            //            {
            //                ClassName=g.Key.ClassName,
            //                SectionName=g.Key.SectionName,
            //                ChieldColl=g,
            //                TotalCount=g.Count(),
            //                TotalHWCount= hwCount
            //            };

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult DelHomeWork(int HomeWorkId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).DeleteById(0, HomeWorkId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetHomeworkById(int HomeWorkId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).getHomeWorkDetailsById(HomeWorkId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "HomeworkType"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveHomeworkType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.HomeworkType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.HomeworkTypeId.HasValue)
                        beData.HomeworkTypeId = 0;

                    resVal = new AcademicLib.BL.HomeWork.HomeworkType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllHomeworkTypeList()
        {
            var dataColl = new AcademicLib.BL.HomeWork.HomeworkType(User.UserId, User.HostName, User.DBName).GetAllHomeworkType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetHomeworkTypeById(int HomeworkTypeId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.HomeworkType(User.UserId, User.HostName, User.DBName).GetHomeworkTypeById(0, HomeworkTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHomeworkType(int HomeworkTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.HomeworkType(User.UserId, User.HostName, User.DBName).DeleteById(0, HomeworkTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetHomeworkTypeAutoOrderNo()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.HomeworkType(User.UserId, User.HostName, User.DBName).GetHomeworkTypeAutoOrderNo(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Assignment"

        public ActionResult Assignment()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveAssignment()
        {
            string photoLocation = "/Attachments/homework";
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.Assignment>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AssignmentId.HasValue)
                        beData.AssignmentId = 0;

                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        int fInd = 0;
                        foreach (var v in filesColl)
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
                                         DocumentTypeId = null,
                                         Extension = att.Extension,
                                         Name = att.Name,
                                         Description = att.Description
                                     }
                                    );
                            }
                            fInd++;
                        }
                    }

                    resVal = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAssignmentList(DateTime? dateFrom, DateTime? dateTo, bool isStudent = false, int? studentId = null, int? ClassId = null, int? SectionId = null, int? SubjectId = null, int? EmployeeId = null, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).GetAllAssignment(0, dateFrom, dateTo, isStudent, studentId, ClassId, SectionId, SubjectId, EmployeeId, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAssignmentById(int AssignmentId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).GetAssignmentById(0, AssignmentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAssignment(int AssignmentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).DeleteById(0, AssignmentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "AssignmentType"

        [HttpPost, ValidateInput(false)]
        public JsonNetResult SaveAssignmentType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.AssignmentType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AssignmentTypeId.HasValue)
                        beData.AssignmentTypeId = 0;

                    resVal = new AcademicLib.BL.HomeWork.AssignmentType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAssignmentTypeList()
        {
            var dataColl = new AcademicLib.BL.HomeWork.AssignmentType(User.UserId, User.HostName, User.DBName).GetAllAssignmentType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAssignmentTypeById(int AssignmentTypeId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.AssignmentType(User.UserId, User.HostName, User.DBName).GetAssignmentTypeById(0, AssignmentTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelAssignmentType(int AssignmentTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.AssignmentType(User.UserId, User.HostName, User.DBName).DeleteById(0, AssignmentTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAssTypeAutoOrderNo()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.AssignmentType(User.UserId, User.HostName, User.DBName).GetAssTypeAutoOrderNo(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        public ActionResult HomeworkType()
        {
            return View();
        }
        public ActionResult HomeworkReport()
        {
            return View();
        }
        public ActionResult AssignmentType()
        {
            return View();
        }
        public ActionResult AssignmentReport()
        {
            return View();
        }

        public ActionResult AddHomework()
        {
            return View();
        }

        #region"TeacherName"
        [HttpGet]
        public JsonNetResult GetTeacherName()
        {
            AcademicLib.BE.HomeWork.TeacherNameCollections dataColl = new AcademicLib.BE.HomeWork.TeacherNameCollections();
            try
            {
                dataColl = new AcademicLib.BL.HomeWork.AddHomework(User.UserId, User.HostName, User.DBName).GetTeacherName(0);
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
        public JsonNetResult GetTeacherWiseClass(int EmployeeId)
        {
            AcademicLib.BE.HomeWork.TeacherWiseClassCollections dataColl = new AcademicLib.BE.HomeWork.TeacherWiseClassCollections();
            try
            {
                dataColl = new AcademicLib.BL.HomeWork.AddHomework(User.UserId, User.HostName, User.DBName).GetTeacherWiseClass(0, EmployeeId, this.AcademicYearId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost, ValidateInput(false)]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ModulewiseEntity)]
        public JsonNetResult SaveAddHomeWork()
        {
            string AttLocation = "/Attachments/homework";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.HomeWork>(Request["jsonData"]);                
                bool isModify = beData.HomeWorkId > 0;
                var finalAttachments = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                if (beData.AttachmentColl != null && beData.AttachmentColl.Count > 0)
                {
                    foreach (var existing in beData.AttachmentColl)
                    {                      

                        finalAttachments.Add(existing);
                    }
                }
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var newAtt = GetAttachmentDocuments(AttLocation, file);
                            if (newAtt != null)
                            {
                                finalAttachments.Add(newAtt);
                            }
                        }
                    }
                }
                beData.AttachmentColl = finalAttachments;
                beData.CUserId = User.UserId;
                if (!beData.HomeWorkId.HasValue) beData.HomeWorkId = 0;
                resVal = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,TotalCount = 0,IsSuccess = resVal.IsSuccess,ResponseMSG = resVal.ResponseMSG
            };
        }

        [HttpPost]
        public JsonNetResult GetAllAddHomework(DateTime? dateFrom, DateTime? dateTo, int? classId, int? sectionId, int? subjectId, int? employeeId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).GetAllHomeWork(0, dateFrom, dateTo, false, null, classId, sectionId, subjectId, employeeId, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAddedHomeworkById(int HomeWorkId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).GetHomeWorkById(0, HomeWorkId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.IncomeSource)]
        public JsonNetResult DeleteAddedHomework(int HomeWorkId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (HomeWorkId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default RescueEquipment ";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.HomeWork.HomeWork(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, HomeWorkId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion


        public ActionResult Configuration()
        {
            return View();
        }

        #region"Configuration"
        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.IncomeSource)]
        public JsonNetResult SaveConfiguration()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.Configuration>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.BranchId.HasValue)
                        beData.BranchId = 0;
                    resVal = new AcademicLib.BL.HomeWork.Configuration(User.UserId, User.HostName, User.DBName).SaveFormData(beData, this.AcademicYearId);
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
        public JsonNetResult GetHAConfiguration(int? BranchId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.Configuration(User.UserId, User.HostName, User.DBName).GetHAConfiguration(0, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        public ActionResult AddAssignment()
        {
            return View();
        }

        #region"AddAssignment"
        [HttpPost,ValidateInput(false)]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.ModulewiseEntity)]
        public JsonNetResult SaveAddAssignment()
        {
            string AttLocation = "/Attachments/homework";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.HomeWork.Assignment>(Request["jsonData"]);
                bool isModify = beData.AssignmentId > 0;
                var finalAttachments = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                if (beData.AttachmentColl != null && beData.AttachmentColl.Count > 0)
                {
                    foreach (var existing in beData.AttachmentColl)
                    {

                        finalAttachments.Add(existing);
                    }
                }
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var newAtt = GetAttachmentDocuments(AttLocation, file);
                            if (newAtt != null)
                            {
                                finalAttachments.Add(newAtt);
                            }
                        }
                    }
                }
                beData.AttachmentColl = finalAttachments;
                beData.CUserId = User.UserId;
                if (!beData.AssignmentId.HasValue) beData.AssignmentId = 0;
                resVal = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,
                TotalCount = 0,
                IsSuccess = resVal.IsSuccess,
                ResponseMSG = resVal.ResponseMSG
            };
        }

        [HttpPost]
        public JsonNetResult GetAllAddAssignment(DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).GetAllAssignment(0, dateFrom, dateTo);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAddedAssignmentById(int AssignmentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).GetAssignmentById(0, AssignmentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAssignmentDetailsById(int AssignmentId)
        {
            var dataColl = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).getAssignmentDetailsById(AssignmentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.IncomeSource)]
        public JsonNetResult DeleteAddedAssignment(int AssignmentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (AssignmentId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default RescueEquipment ";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.HomeWork.Assignment(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, AssignmentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion
    }
}