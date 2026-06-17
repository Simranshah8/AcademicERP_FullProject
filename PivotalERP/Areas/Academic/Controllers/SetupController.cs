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
using System.IO;
using System.IO.Compression;

namespace PivotalERP.Areas.Academic.Controllers
{
    public class SetUpController : PivotalERP.Controllers.BaseController
    {
        // GET: Academic/SetUp
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AcademicSetup, false)]
        public ActionResult UserCredential()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetStudentUserList(int ClassId, string SectionIdColl, int? BatchId=null, int? SemesterId=null, int? ClassYearId=null)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentUserList(this.AcademicYearId, ClassId, SectionIdColl,BatchId,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetEmployeeUserList()
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getEmployeeUserList();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult ReArrangeRollNoCS(int ReArrangeBy)
        {
            var dataColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).ReArrangeRollNoCS(ReArrangeBy, this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult ReGenerateQR()
        {
            var dataColl = new AcademicLib.BL.Global(User.UserId, User.HostName, User.DBName).ReGenerateQROfStudentEmp();

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SendNotificationToStudentUser(AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                    notification.Content = "Test";
                    notification.EntityId = 1;
                    notification.EntityName = "StudentUser";
                    notification.Heading = "Pwd";
                    notification.Subject = "Pwd";
                    notification.UserId = v.UserId;
                    notification.UserName = v.UserName;
                    notification.UserIdColl = v.UserId.ToString();
                    resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);
                }
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SendNotificationToForceLogOut(AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                    notification.Content = "Logout";
                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.LOGOUT);
                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.LOGOUT.ToString();
                    notification.Heading = "Logout";
                    notification.Subject = "Force Logout";
                    notification.UserId = v.UserId;
                    notification.UserName = v.UserName;
                    notification.UserIdColl = v.UserId.ToString();
                    resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);
                }

                if (resVal.IsSuccess)
                    resVal.ResponseMSG = "Force logout has done successfully";
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SendNotificationToDisabled(AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                    notification.Content = "Disable";
                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.DISABLE_NOTIFICATION);
                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.DISABLE_NOTIFICATION.ToString();
                    notification.Heading = "Disable";
                    notification.Subject = "Force Disable";
                    notification.UserId = v.UserId;
                    notification.UserName = v.UserName;
                    notification.UserIdColl = v.UserId.ToString();
                    resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);
                }

                if (resVal.IsSuccess)
                    resVal.ResponseMSG = "Force disable has done successfully";
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SendNotificationToEnabled(AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                    notification.Content = "Enable";
                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.ENABLE_NOTIFICATION);
                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.ENABLE_NOTIFICATION.ToString();
                    notification.Heading = "Enable";
                    notification.Subject = "Force Enable";
                    notification.UserId = v.UserId;
                    notification.UserName = v.UserName;
                    notification.UserIdColl = v.UserId.ToString();
                    resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);
                }

                if (resVal.IsSuccess)
                    resVal.ResponseMSG = "Force enable has done successfully";
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult AllNotificationDisabled()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                notification.Content = "Disable";
                notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.DISABLE_NOTIFICATION);
                notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.DISABLE_NOTIFICATION.ToString();
                notification.Heading = "Disable";
                notification.Subject = "Force Disable";
                notification.UserId = 1;
                notification.UserName = "Admin";
                notification.UserIdColl = "";
                resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);

                if (resVal.IsSuccess)
                    resVal.ResponseMSG = "Force disabled has done successfully";
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult AllNotificationEnabled()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                notification.Content = "Enable";
                notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.ENABLE_NOTIFICATION);
                notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.ENABLE_NOTIFICATION.ToString();
                notification.Heading = "Enable";
                notification.Subject = "Force Enable";
                notification.UserId = 1;
                notification.UserName = "Admin";
                notification.UserIdColl = "";
                resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);

                if (resVal.IsSuccess)
                    resVal.ResponseMSG = "Force enable has done successfully";
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };

        }
        [HttpPost]
        public JsonNetResult SendSMSToStudentUser(AcademicLib.BE.Academic.Transaction.StudentUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    if (!string.IsNullOrEmpty(v.ContactNo))
                    {
                        resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendSMS(v.ContactNo, "Test SMS", true);
                    }
                }
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SendNotificationToEmployeeUser(AcademicLib.BE.Academic.Transaction.EmployeeUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new NotificationLog();
                    notification.Content = "Test";
                    notification.EntityId = 1;
                    notification.EntityName = "EmployeeUser";
                    notification.Heading = "Pwd";
                    notification.Subject = "Pwd";
                    notification.UserId = v.UserId;
                    notification.UserName = v.UserName;
                    notification.UserIdColl = v.UserId.ToString();
                    resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notification);
                }
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SendSMSToEmployeeUser(AcademicLib.BE.Academic.Transaction.EmployeeUserCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                foreach (var v in dataColl)
                {
                    if (!string.IsNullOrEmpty(v.ContactNo))
                    {
                        resVal = new Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendSMS(v.ContactNo, "Test SMS", true);
                    }
                }
            }
            catch (Exception ee)
            {
                resVal.ResponseMSG = ee.Message;
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult UpdatePwd(int UserId, string OldPwd, string NewPwd, string UserName)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Global(UserId, User.HostName, User.DBName).UpdatePwd(OldPwd, NewPwd, UserName);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult ResetClassWisePwd(int ClassId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).ResetClassWisePwd(ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult ResetPwdEmployeee()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).ResetPwdEmployeee();
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GenerateStudentUser(int AsPer, bool canUpdate, string Prefix, string Suffix)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).GenerateUser(this.AcademicYearId, AsPer, canUpdate, Prefix, Suffix);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GenerateEmployeeUser(int AsPer, bool canUpdate, string Prefix, string Suffix)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).GenerateUser(AsPer, canUpdate, Prefix, Suffix);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.AcademicSetup, false)]
        public ActionResult Configuration()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveStudentConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Setup.ConfigurationStudent>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Setup.ConfigurationStudent(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetStudentConfiguration(int? BranchId = null)
        {
            var dataColl = new AcademicLib.BL.Academic.Setup.ConfigurationStudent(User.UserId, User.HostName, User.DBName).GetConfiguration(0, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveEmployeeConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Setup.ConfigurationEmployee>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Setup.ConfigurationEmployee(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetEmployeeConfiguration()
        {
            var dataColl = new AcademicLib.BL.Academic.Setup.ConfigurationEmployee(User.UserId, User.HostName, User.DBName).GetConfiguuration(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveAcademicConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Setup.AcademicConfiguration>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Setup.AcademicConfiguration(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAcademicConfiguration()
        {
            var dataColl = new AcademicLib.BL.Academic.Setup.AcademicConfiguration(User.UserId, User.HostName, User.DBName).GetConfiguration(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveClassWiseAcademicMonth()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Setup.ClassWiseAcademicMonth>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Setup.ClassWiseAcademicMonth(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetClassWiseAcademicMonth()
        {
            var dataColl = new AcademicLib.BL.Academic.Setup.ClassWiseAcademicMonth(User.UserId, User.HostName, User.DBName).GetAllClassWiseAcademicMonth(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelClassWiseAcademicMonth(int ClassId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Setup.ClassWiseAcademicMonth(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveUpgradeStudentClass()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Setup.UpgradeStudentClass>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Academic.Setup.UpgradeStudentClass(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetUpgradeStudentClass()
        {
            var dataColl = new AcademicLib.BL.Academic.Setup.UpgradeStudentClass(User.UserId, User.HostName, User.DBName).GetAllUpgradeStudentClass(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelUpgradeStudentClass(int ClassId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Academic.Setup.UpgradeStudentClass(User.UserId, User.HostName, User.DBName).DeleteById(0, ClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult DownloadStudentPhoto(int ClassId, int? SectionId)
        {

            try
            {
                if (SectionId.HasValue && SectionId == 0)
                    SectionId = null;

                AcademicLib.RE.Academic.StudentPhotoCollections studentColl = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).getStudentPhoto(ClassId, SectionId);
                string serverPath = Server.MapPath("~");

                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var st in studentColl)
                        {
                            string filePath = serverPath + st.PhotoPath;
                            if (System.IO.File.Exists(filePath))
                            {
                                ziparchive.CreateEntryFromFile(filePath, st.RegdNo.ToString() + ".jpg");
                            }
                        }
                    }

                    return File(memoryStream.ToArray(), "application/zip");

                }

            }
            catch (Exception ee)
            {
                throw ee;
            }

        }

        #region "HtmlTemplateConfig"
        public ActionResult HtmlTemplateConfig()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveHTMLTemplateConfig()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Academic.Setup.HTMLTemplateConfigCollection>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Academic.Setup.HTMLTemplateConfig(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetHTMLTemplatesConfig()
        {
            var dataColl = new AcademicLib.BL.Academic.Setup.HTMLTemplateConfig(User.UserId, User.HostName, User.DBName).GetHTMLTemplatesConfig(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion
        public ActionResult IncomeExpenseClosing()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult ModifyIncomeExpensesLedgerAsZero(DateTime dateFrom, DateTime dateTo, DateTime voucherDate, int ny, int nm, int nd, int ledgerGroupId = 0)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var tranBL = new Dynamic.DataAccess.Global.GlobalDB(User.HostName, User.DBName);
                bool isSuccess = tranBL.IncomeExpensesLedgerAsZero(dateFrom, dateTo, voucherDate, ny, nm, nd, 0);
                resVal.IsSuccess = isSuccess;
                resVal.ResponseMSG = isSuccess ? "Income Expenses Closing Zero updated successfully." : "Failed to update Income Expenses Closing Zero.";

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.IncomeExpensesLedgerZero;
                    auditLog.Action = Actions.Modify;
                    auditLog.LogText = "Entity Properties Of " + auditLog.EntityId.ToString();
                    auditLog.AutoManualNo = auditLog.EntityId.ToString();
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

        public ActionResult DownloadEmployeePhoto(int? DepartmentId)
        {

            try
            {
                if (DepartmentId.HasValue && DepartmentId == 0)
                    DepartmentId = null;

                AcademicLib.RE.Academic.EmployeePhotoCollections EmployeeColl = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).getEmployeePhoto(DepartmentId);
                string serverPath = Server.MapPath("~");

                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var st in EmployeeColl)
                        {
                            string filePath = serverPath + st.PhotoPath;
                            if (System.IO.File.Exists(filePath))
                            {
                                ziparchive.CreateEntryFromFile(filePath, st.EmployeeCode.ToString() + ".jpg");
                            }
                        }
                    }

                    return File(memoryStream.ToArray(), "application/zip");

                }

            }
            catch (Exception ee)
            {
                throw ee;
            }

        }
    }
}