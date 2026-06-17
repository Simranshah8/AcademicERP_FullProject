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
namespace PivotalERP.Areas.Attendance.Controllers
{
    public class TransactionController : PivotalERP.Controllers.BaseController
    {

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Payheading, false)]
        public ActionResult PayHeading()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.TaxRule, false)]
        public ActionResult TaxRule()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ExpensesCategory, false)]
        public ActionResult ExpenseCategory()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.AllowPayHeading, false)]
        public ActionResult AllowPayHeading()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalaryDetails, false)]
        public ActionResult SalaryDetail()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.EmployeeLoan, false)]
        public ActionResult EmployeeLoan()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.EmployeeAdvance, false)]
        public ActionResult EmployeeAdvance()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalaryAddDeduct, false)]
        public ActionResult SalaryAddDeduct()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.SalarySheet, false)]
        public ActionResult SalarySheet()

        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Incentive, false)]
        public ActionResult Incentive()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.TaxCalculator, false)]
        public ActionResult TaxCalculator()
        {
            return View();
        }

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ArrearSalarySheet, false)]
        public ActionResult ArrearSalarySheet()
        {
            return View();
        }

        //Added by suresh for leave Request Starts:Falgun1
        public ActionResult LeaveEntry()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetAllLeaveType()
        {
            AcademicLib.BE.Attendance.LeaveTypeCollections dataColl = new AcademicLib.BE.Attendance.LeaveTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.LeaveType(User.UserId, User.HostName, User.DBName).GetAllLeaveType(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "LeaveRequest"
        

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Lea)]

        public JsonNetResult SaveLeaveRequest()
        {
            string photoLocation = "/Attachments/Academic/Student";
            ResponeValues resVal = new ResponeValues();

            try
            {
                var beData = DeserializeObject<AcademicLib.API.Attendance.LeaveRequest>(Request["jsonData"]);

                if (beData != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        beData.DocumentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                        for (int fi = 0; fi < Request.Files.Count; fi++)
                        {
                            HttpPostedFileBase file = Request.Files["file" + fi];
                            if (file != null)
                            {
                                beData.DocumentColl.Add(GetAttachmentDocuments(photoLocation, file));
                            }
                        }
                    }

                        beData.CUserId = User.UserId;

                    resVal = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).SaveFromApp(beData);
                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                        notification.Content = resVal.JsonStr;
                        notification.ContentPath = "";
                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_REQUEST);
                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_REQUEST.ToString();
                        notification.Heading = "Leave Request";
                        notification.Subject = "Leave Request";
                        notification.UserId = User.UserId;
                        notification.UserName = User.Identity.Name;
                        notification.UserIdColl = resVal.CUserName.Trim();

                        resVal = new PivotalERP.Global.GlobalFunction(User.UserId, hostName, dbName).SendNotification(User.UserId, notification, true);

                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = "New Leave Request Success";
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accepted";
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
        public JsonNetResult GetAllLeaveRequest()
        {
            AcademicLib.BE.Attendance.LeaveRequestCollections dataColl = new AcademicLib.BE.Attendance.LeaveRequestCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).GetAllLeaveRequest(0);
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
        public JsonNetResult getLeaveEntryById(int LeaveRequestId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).GetLeaveRequestById(0, LeaveRequestId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteLeaveRequest(int LeaveRequestId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (LeaveRequestId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Member Leave Request";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, LeaveRequestId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        //Code added By Suresh for Payroll Starts From here(Chaitra 2)

        #region "PayHeadCategory"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.PayHeadCategory)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.PayHeadCategory)]
        public JsonNetResult SavePayHeadCategory()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.PayHeadCategory>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.PayHeadCategoryId.HasValue)
                        beData.PayHeadCategoryId = 0;

                    resVal = new AcademicLib.BL.Payroll.PayHeadCategory(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.PayHeadCategory)]
        public JsonNetResult getPayHeadCategoryById(int PayHeadCategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.PayHeadCategory(User.UserId, User.HostName, User.DBName).GetPayHeadCategoryById(0, PayHeadCategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.PayHeadCategory)]
        public JsonNetResult DeletePayHeadCategory(int PayHeadCategoryId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (PayHeadCategoryId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.PayHeadCategory(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, PayHeadCategoryId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllPayHeadCategory()
        {
            AcademicLib.BE.Payroll.PayHeadCategoryCollections dataColl = new AcademicLib.BE.Payroll.PayHeadCategoryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PayHeadCategory(User.UserId, User.HostName, User.DBName).GetAllPayHeadCategory(0);
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
        public JsonNetResult GetBranchListforPayhead()
        {
            AcademicLib.BE.Payroll.BranchForPayHeadingCollections dataColl = new AcademicLib.BE.Payroll.BranchForPayHeadingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).GetBranchForPayHeading(0);
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
        public JsonNetResult GetCategoryListforPayhead()
        {
            AcademicLib.BE.Payroll.CategoryForPayHeadingCollections dataColl = new AcademicLib.BE.Payroll.CategoryForPayHeadingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).GetCategoryForPayHeading(0);
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


        #region "PayHeadGroup"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.PayHeadGroup)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.PayHeadGroup)]
        public JsonNetResult SavePayHeadGroup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.PayHeadGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.PayHeadGroupId.HasValue)
                        beData.PayHeadGroupId = 0;

                    resVal = new AcademicLib.BL.Payroll.PayHeadGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.PayHeadGroup)]
        public JsonNetResult getPayHeadGroupById(int PayHeadGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.PayHeadGroup(User.UserId, User.HostName, User.DBName).GetPayHeadGroupById(0, PayHeadGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.PayHeadGroup)]
        public JsonNetResult DeletePayHeadGroup(int PayHeadGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (PayHeadGroupId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.PayHeadGroup(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, PayHeadGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllPayHeadGroup()
        {
            AcademicLib.BE.Payroll.PayHeadGroupCollections dataColl = new AcademicLib.BE.Payroll.PayHeadGroupCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PayHeadGroup(User.UserId, User.HostName, User.DBName).GetAllPayHeadGroup(0);
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


        #region "PayHeading"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.PayHeading)]

        [HttpPost]        
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Payheading, false)]
        public JsonNetResult SavePayHeading()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.PayHeading>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.PayHeadingId.HasValue)
                        beData.PayHeadingId = 0;

                    resVal = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.PayHeading)]
        public JsonNetResult getPayHeadingById(int PayHeadingId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).GetPayHeadingById(0, PayHeadingId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.Payheading, false)]
        public JsonNetResult DeletePayHeading(int PayHeadingId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (PayHeadingId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, PayHeadingId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllPayHeading()
        {
            AcademicLib.BE.Payroll.PayHeadingCollections dataColl = new AcademicLib.BE.Payroll.PayHeadingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).GetAllPayHeading(0);
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
        public JsonNetResult GetAllPayHeadingForTran()
        {
            AcademicLib.BE.Payroll.PayHeadingCollections dataColl = new AcademicLib.BE.Payroll.PayHeadingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.PayHeading(User.UserId, User.HostName, User.DBName).getAllPayHeadingForTran();
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



        #region "TaxRule"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.TaxRule)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.TaxRule, false)]
        public JsonNetResult SaveTaxRule()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.TaxRuleCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Payroll.TaxRule(User.UserId, User.HostName, User.DBName).Update(beData);
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

        [HttpGet]
        public JsonNetResult GetAllTaxRule(int? TaxFor)
        {
            AcademicLib.BE.Payroll.TaxRuleCollections dataColl = new AcademicLib.BE.Payroll.TaxRuleCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.TaxRule(User.UserId, User.HostName, User.DBName).GetAllTaxRule(0, TaxFor);
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


        #region EmployeeLoan

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.EmployeeLoan, false)]
        public JsonNetResult SaveEmployeeLoan()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.EmployeeLoan>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.EmployeeLoan(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getEmployeeLoanById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.EmployeeLoan(User.UserId, User.HostName, User.DBName).GetEmployeeLoanById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteEmployeeLoan(int TranId)
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
                    resVal = new AcademicLib.BL.Payroll.EmployeeLoan(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllEmployeeLoan()
        {
            AcademicLib.BE.Payroll.EmployeeLoanCollections dataColl = new AcademicLib.BE.Payroll.EmployeeLoanCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.EmployeeLoan(User.UserId, User.HostName, User.DBName).GetAllEmployeeLoan(0);
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


        #region "LoanType"

        [HttpPost]
        public JsonNetResult SaveLoanType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.LoanType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.LoanType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getLoanTypeById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.LoanType(User.UserId, User.HostName, User.DBName).GetLoanTypeById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteLoanType(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.LoanType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllLoanType()
        {
            AcademicLib.BE.Payroll.LoanTypeCollections dataColl = new AcademicLib.BE.Payroll.LoanTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.LoanType(User.UserId, User.HostName, User.DBName).GetAllLoanType(0);
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

        #region "EmployeeAdvance"

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.EmployeeAdvance, false)]
        public JsonNetResult SaveEmployeeAdvance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.EmployeeAdvance>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.EmployeeAdvance(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getEmployeeAdvanceById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.EmployeeAdvance(User.UserId, User.HostName, User.DBName).GetEmployeeAdvanceById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteEmployeeAdvance(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.EmployeeAdvance(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllEmployeeAdvance()
        {
            AcademicLib.BE.Payroll.EmployeeAdvanceCollections dataColl = new AcademicLib.BE.Payroll.EmployeeAdvanceCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.EmployeeAdvance(User.UserId, User.HostName, User.DBName).GetAllEmployeeAdvance(0);
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


        #region "AdvanceType"

        [HttpPost]
        public JsonNetResult SaveAdvanceType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.AdvanceType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.AdvanceType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getAdvanceTypeById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.AdvanceType(User.UserId, User.HostName, User.DBName).GetAdvanceTypeById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteAdvanceType(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.AdvanceType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllAdvanceType()
        {
            AcademicLib.BE.Payroll.AdvanceTypeCollections dataColl = new AcademicLib.BE.Payroll.AdvanceTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.AdvanceType(User.UserId, User.HostName, User.DBName).GetAllAdvanceType(0);
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


        #region "Incentive"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Incentive)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Incentive, false)]
        public JsonNetResult SaveIncentive()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.Incentive>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.IncentiveId.HasValue)
                        beData.IncentiveId = 0;

                    resVal = new AcademicLib.BL.Payroll.Incentive(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.Incentive)]
        public JsonNetResult getIncentiveById(int IncentiveId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.Incentive(User.UserId, User.HostName, User.DBName).GetIncentiveById(0, IncentiveId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Incentive)]
        public JsonNetResult DeleteIncentive(int IncentiveId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (IncentiveId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.Incentive(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, IncentiveId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllIncentive()
        {
            AcademicLib.BE.Payroll.IncentiveCollections dataColl = new AcademicLib.BE.Payroll.IncentiveCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.Incentive(User.UserId, User.HostName, User.DBName).GetAllIncentive(0);
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


        #region "IncentiveType"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.IncentiveType)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.IncentiveType)]
        public JsonNetResult SaveIncentiveType()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.IncentiveType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.IncentiveType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.IncentiveType)]
        public JsonNetResult getIncentiveTypeById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.IncentiveType(User.UserId, User.HostName, User.DBName).GetIncentiveTypeById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.IncentiveType)]
        public JsonNetResult DeleteIncentiveType(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.IncentiveType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllIncentiveType()
        {
            AcademicLib.BE.Payroll.IncentiveTypeCollections dataColl = new AcademicLib.BE.Payroll.IncentiveTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.IncentiveType(User.UserId, User.HostName, User.DBName).GetAllIncentiveType(0);
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

        #region "Brand"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.Brand)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Brand)]
        public JsonNetResult SaveBrand()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.Brand>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.Brand(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.Brand)]
        public JsonNetResult getBrandById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.Brand(User.UserId, User.HostName, User.DBName).GetBrandById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DeleteBrand(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.Brand(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllBrand()
        {
            AcademicLib.BE.Payroll.BrandCollections dataColl = new AcademicLib.BE.Payroll.BrandCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.Brand(User.UserId, User.HostName, User.DBName).GetAllBrand(0);
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

        #region ExpenseCategory

        [HttpPost]
        public JsonNetResult SaveExpenseCategory()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.ExpenseCategory>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.ExpenseCategory(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getExpenseCategoryById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.ExpenseCategory(User.UserId, User.HostName, User.DBName).GetExpenseCategoryById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteExpenseCategory(int TranId)
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
                    resVal = new AcademicLib.BL.Payroll.ExpenseCategory(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllExpenseCategory()
        {
            AcademicLib.BE.Payroll.ExpenseCategoryCollections dataColl = new AcademicLib.BE.Payroll.ExpenseCategoryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.ExpenseCategory(User.UserId, User.HostName, User.DBName).GetAllExpenseCategory(0);
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

        #region ExpenseGroup

        [HttpPost]
        public JsonNetResult SaveExpenseGroup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.ExpenseGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.ExpenseGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult getExpenseGroupById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.ExpenseGroup(User.UserId, User.HostName, User.DBName).GetExpenseGroupById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DeleteExpenseGroup(int TranId)
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
                    resVal = new AcademicLib.BL.Payroll.ExpenseGroup(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetAllExpenseGroup()
        {
            AcademicLib.BE.Payroll.ExpenseGroupCollections dataColl = new AcademicLib.BE.Payroll.ExpenseGroupCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.ExpenseGroup(User.UserId, User.HostName, User.DBName).GetAllExpenseGroup(0);
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

        #region "SalaryAddDeduct"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.SalaryAddDeduct)]

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalaryAddDeduct, false)]
        public JsonNetResult SaveSalaryAddDeduct()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.SalaryAddDeduct>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Payroll.SalaryAddDeduct(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.SalaryAddDeduct)]
        public JsonNetResult getSalaryAddDeductById(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.SalaryAddDeduct(User.UserId, User.HostName, User.DBName).GetSalaryAddDeductById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.SalaryAddDeduct)]
        public JsonNetResult DeleteSalaryAddDeduct(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (TranId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.SalaryAddDeduct(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllSalaryAddDeduct()
        {
            AcademicLib.BE.Payroll.SalaryAddDeductCollections dataColl = new AcademicLib.BE.Payroll.SalaryAddDeductCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.SalaryAddDeduct(User.UserId, User.HostName, User.DBName).GetAllSalaryAddDeduct(0);
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

        #region "AllowPayheading"
        [HttpGet]
        public JsonNetResult GetAllEmployeeForAllowPayHeading(int? BranchId, int? DepartmentId, int? CategoryId)
        {
            AcademicLib.BE.Payroll.EmployeeForAllowPayHeadingCollections dataColl = new AcademicLib.BE.Payroll.EmployeeForAllowPayHeadingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.AllowPayHeading(User.UserId, User.HostName, User.DBName).GetAllAllowPayHeading(0, BranchId, DepartmentId, CategoryId);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.AllowPayHeading, false)]
        public JsonNetResult SaveAllowPayHeading()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.AllowPayHeadingCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Payroll.AllowPayHeading(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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

        #region "AllowExpenseCategory"
        [HttpGet]
        public JsonNetResult GetAllEmployeeForAllowExpenseCategory(int? BranchId, int? DepartmentId, int? CategoryId)
        {
            AcademicLib.BE.Payroll.EmployeeForAllowExpenseCategoryCollections dataColl = new AcademicLib.BE.Payroll.EmployeeForAllowExpenseCategoryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.AllowExpenseCategory(User.UserId, User.HostName, User.DBName).GetAllAllowExpenseCategory(0, BranchId, DepartmentId, CategoryId);
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
        public JsonNetResult SaveAllowExpenseCategory()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.AllowExpenseCategoryCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Payroll.AllowExpenseCategory(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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

        #region "ExpenseRateSetup"
        [HttpGet]
        public JsonNetResult GetAllEmployeeForExpenseRateSetup(int? BranchId, int? DepartmentId, int? CategoryId)
        {
            AcademicLib.BE.Payroll.EmployeeForExpenseRateSetupCollections dataColl = new AcademicLib.BE.Payroll.EmployeeForExpenseRateSetupCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.ExpenseRateSetup(User.UserId, User.HostName, User.DBName).GetAllExpenseRateSetup(0, BranchId, DepartmentId, CategoryId);
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
        public JsonNetResult SaveExpenseRateSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.ExpenseRateSetupCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Payroll.ExpenseRateSetup(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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

        #region "SalaryDetail"
        [HttpPost]
        public JsonNetResult GetAllEmployeeForSalaryDetail(int? BranchId, int? DepartmentId, int? CategoryId, int YearId, int MonthId)
        {
            AcademicLib.BE.Payroll.EmployeeForSalaryDetailCollections dataColl = new AcademicLib.BE.Payroll.EmployeeForSalaryDetailCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.SalaryDetail(User.UserId, User.HostName, User.DBName).GetAllSalaryDetail(0, BranchId, DepartmentId, CategoryId, YearId, MonthId);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalaryDetails, false)]
        public JsonNetResult SaveSalaryDetail()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.SalaryDetailCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Payroll.SalaryDetail(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DeleteSalaryDetail(int BranchId, int DepartmentId, int CategoryId, int YearId, int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.SalaryDetail(User.UserId, User.HostName, User.DBName).DeleteSalaryDetail(User.UserId, BranchId, DepartmentId, CategoryId, YearId, MonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DelSalaryDetailData(int EmployeeId, int YearId, int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (EmployeeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Salary Detail";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.SalaryDetail(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, EmployeeId, YearId, MonthId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "SalarySheet"
        [HttpPost]
        public JsonNetResult GetAllEmployeeForSalarySheet(int? BranchId, int? DepartmentId, int? CategoryId, int? YearId, int? MonthId)
        {
            AcademicLib.BE.Payroll.SalarySheetDetail dt = new AcademicLib.BE.Payroll.SalarySheetDetail();
            try
            {
                dt = new AcademicLib.BL.Payroll.SalarySheet(User.UserId, User.HostName, User.DBName).GetAllSalarySheet(0, BranchId, DepartmentId, CategoryId, YearId, MonthId);
                return new JsonNetResult() { Data = dt, TotalCount = dt.PayColl.Count + dt.AttColl.Count, IsSuccess = dt.IsSuccess, ResponseMSG = dt.ResponseMSG };
            }
            catch (Exception ee)
            {
                dt.IsSuccess = false;
                dt.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dt.IsSuccess, ResponseMSG = dt.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalarySheet, false)]
        public JsonNetResult SaveSalarySheet()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var payData = DeserializeObject<AcademicLib.BE.Payroll.SalarySheetCollections>(Request["dtColl"]);
                var atData = DeserializeObject<AcademicLib.BE.Payroll.AttendanceTypeCollections>(Request["atColl"]);
                if (payData != null && atData != null)
                {

                    resVal = new AcademicLib.BL.Payroll.SalarySheet(User.UserId, User.HostName, User.DBName).SaveUpdate(payData, atData);
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

        [PermissionsAttribute(Actions.Save, (int)ENTITIES.SalarySheet, false)]
        public JsonNetResult SaveSalaryJV(int YearId,int MonthId)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                resVal = new AcademicLib.BL.Payroll.SalarySheet(User.UserId, User.HostName, User.DBName).SaveJV(YearId, MonthId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DeleteSalarySheet(int BranchId, int DepartmentId, int CategoryId, int YearId, int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.SalarySheet(User.UserId, User.HostName, User.DBName).DeleteSalarySheet(User.UserId, BranchId, DepartmentId, CategoryId, YearId, MonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }



        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DelSalarySheetData(int EmployeeId, int YearId, int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (EmployeeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Salary Sheet";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.SalarySheet(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, EmployeeId, YearId, MonthId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "ArrearSalarySheet"
        [HttpGet]
        public JsonNetResult GetAllEmployeeForArrearSalarySheet(int? BranchId, int? DepartmentId, int? CategoryId, int? YearId, int? MonthId)
        {
            AcademicLib.BE.Payroll.EmployeeForArrearSalarySheetCollections dataColl = new AcademicLib.BE.Payroll.EmployeeForArrearSalarySheetCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.ArrearSalarySheet(User.UserId, User.HostName, User.DBName).GetAllArrearSalarySheet(0, BranchId, DepartmentId, CategoryId, YearId, MonthId);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ArrearSalarySheet, false)]
        public JsonNetResult SaveArrearSalarySheet()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.ArrearSalarySheetCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Payroll.ArrearSalarySheet(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DeleteArrearSalarySheet(int BranchId, int DepartmentId, int CategoryId, int YearId, int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.ArrearSalarySheet(User.UserId, User.HostName, User.DBName).DeleteArrearSalarySheet(User.UserId, BranchId, DepartmentId, CategoryId, YearId, MonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Brand)]
        public JsonNetResult DelArrearSalarySheetData(int EmployeeId, int YearId, int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (EmployeeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Salary Sheet";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.ArrearSalarySheet(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, EmployeeId, YearId, MonthId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        public ActionResult AttendanceType()
        {
            return View();
        }

        #region "AttendanceType"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.AttendanceType)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.AttendanceType)]
        public JsonNetResult SaveAttendanceType()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.AttendanceType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AttendanceTypeId.HasValue)
                        beData.AttendanceTypeId = 0;

                    resVal = new AcademicLib.BL.Payroll.AttendanceType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.AttendanceType)]
        public JsonNetResult getAttendanceTypeById(int AttendanceTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.AttendanceType(User.UserId, User.HostName, User.DBName).GetAttendanceTypeById(0, AttendanceTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.AttendanceType)]
        public JsonNetResult DeleteAttendanceType(int AttendanceTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (AttendanceTypeId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Attendance Type";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.AttendanceType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, AttendanceTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllAttendanceType()
        {
            AcademicLib.BE.Payroll.AttendanceTypeCollections dataColl = new AcademicLib.BE.Payroll.AttendanceTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.AttendanceType(User.UserId, User.HostName, User.DBName).GetAllAttendanceType(0);
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
        public JsonNetResult GetAttendanceTypeForTran()
        {
            AcademicLib.BE.Payroll.AttendanceTypeCollections dataColl = new AcademicLib.BE.Payroll.AttendanceTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.AttendanceType(User.UserId, User.HostName, User.DBName).getAttendanceTypeForTran();
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
        public ActionResult UnitOfWork()
        {
            return View();
        }


        #region "UnitsOfWork"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.UnitsOfWork)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.UnitsOfWork)]
        public JsonNetResult SaveUnitsOfWork()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.UnitsOfWork>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.UnitsOfWorkId.HasValue)
                        beData.UnitsOfWorkId = 0;

                    resVal = new AcademicLib.BL.Payroll.UnitsOfWork(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.UnitsOfWork)]
        public JsonNetResult getUnitsOfWorkById(int UnitsOfWorkId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.UnitsOfWork(User.UserId, User.HostName, User.DBName).GetUnitsOfWorkById(0, UnitsOfWorkId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.UnitsOfWork)]
        public JsonNetResult DeleteUnitsOfWork(int UnitsOfWorkId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (UnitsOfWorkId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.UnitsOfWork(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, UnitsOfWorkId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllUnitsOfWork()
        {
            AcademicLib.BE.Payroll.UnitsOfWorkCollections dataColl = new AcademicLib.BE.Payroll.UnitsOfWorkCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.UnitsOfWork(User.UserId, User.HostName, User.DBName).GetAllUnitsOfWork(0);
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

        public ActionResult EmployeeGroup()
        {
            return View();
        }

        #region "EmployeeGroup"
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.EmployeeGroup)]

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.EmployeeGroup)]
        public JsonNetResult SaveEmployeeGroup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Payroll.EmployeeGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.EmployeeGroupId.HasValue)
                        beData.EmployeeGroupId = 0;

                    resVal = new AcademicLib.BL.Payroll.EmployeeGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Payroll.Global.Actions.Modify, (int)FormsEntity.EmployeeGroup)]
        public JsonNetResult getEmployeeGroupById(int EmployeeGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Payroll.EmployeeGroup(User.UserId, User.HostName, User.DBName).GetEmployeeGroupById(0, EmployeeGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.EmployeeGroup)]
        public JsonNetResult DeleteEmployeeGroup(int EmployeeGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (EmployeeGroupId < 0)
                {
                    resVal.ResponseMSG = "can't delete defaultProduct Category";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Payroll.EmployeeGroup(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, EmployeeGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllEmployeeGroup()
        {
            AcademicLib.BE.Payroll.EmployeeGroupCollections dataColl = new AcademicLib.BE.Payroll.EmployeeGroupCollections();
            try
            {
                dataColl = new AcademicLib.BL.Payroll.EmployeeGroup(User.UserId, User.HostName, User.DBName).GetAllEmployeeGroup(0);
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.PayrollConfig, false)]
        public ActionResult PayrollConfiguration()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.PayrollConfig, false)]
        public JsonNetResult SavePayrollConfig()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.PayrollConfig>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.PayrollConfig(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetPayrollConfiguration()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.PayrollConfig(User.UserId, User.HostName, User.DBName).GetPayrollConfig(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllPaySlipsReport()
        {
            AcademicLib.BE.PaySlipReportCollections dataColl = new AcademicLib.BE.PaySlipReportCollections();
            try
            {
                dataColl = new AcademicLib.BL.PayrollConfig(User.UserId, User.HostName, User.DBName).GetPaySlipsReport(0);
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
        public JsonNetResult GetSalaryJV(int YearId, int MonthId)
        {
            (AcademicLib.RE.Payroll.LedgerSJVCollections ledgerColl, AcademicLib.RE.Payroll.PayHeadSJVCollections payHeadColl) dataColl;
            try
            {
                dataColl = new AcademicLib.BL.Payroll.SalaryJVDet(User.UserId, User.HostName, User.DBName).GetSalaryJVDet(YearId, MonthId);
                return new JsonNetResult
                {
                    Data = new { LedgerSJV = dataColl.ledgerColl, PayHeadSJV = dataColl.payHeadColl },
                    TotalCount = dataColl.ledgerColl.Count + dataColl.payHeadColl.Count,
                    IsSuccess = dataColl.ledgerColl.IsSuccess && dataColl.payHeadColl.IsSuccess,
                    ResponseMSG = dataColl.ledgerColl.IsSuccess && dataColl.payHeadColl.IsSuccess ? dataColl.ledgerColl.ResponseMSG : "Error in retrieving data"
                };
            }
            catch (Exception ex)
            {
                return new JsonNetResult
                {
                    Data = null,
                    TotalCount = 0,
                    IsSuccess = false,
                    ResponseMSG = ex.Message
                };
            }
        }
    }
}