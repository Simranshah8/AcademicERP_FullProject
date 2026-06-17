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
    public class CreationController : PivotalERP.Controllers.BaseController
    {

        #region "WorkingShift"
        public ActionResult WorkingShift()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveWorkingShift()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.WorkingShift>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.WorkingShiftId.HasValue)
                        beData.WorkingShiftId = 0;


                    resVal = new AcademicLib.BL.Attendance.WorkingShift(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllWorkingShift()
        {
            var dataColl = new AcademicLib.BL.Attendance.WorkingShift(User.UserId, User.HostName, User.DBName).getAllWorkingShift();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetWorkingShiftById(int WorkingShiftId)
        {
            var dataColl = new AcademicLib.BL.Attendance.WorkingShift(User.UserId, User.HostName, User.DBName).getWorkingShiftById(WorkingShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelWorkingShift(int WorkingShiftId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.WorkingShift(User.UserId, User.HostName, User.DBName).DeleteById(0, WorkingShiftId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult SaveShiftMapping()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.WorkingShiftMapping>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.ShiftMappingId.HasValue)
                        beData.ShiftMappingId = 0;


                    resVal = new AcademicLib.BL.Attendance.WokringShiftMapping(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllShiftMapping()
        {
            var dataColl = new AcademicLib.BL.Attendance.WokringShiftMapping(User.UserId, User.HostName, User.DBName).getAllWorkingShift();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelShiftMapping(int ShiftMappingId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.WokringShiftMapping(User.UserId, User.HostName, User.DBName).DeleteById(0, ShiftMappingId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        // GET: Attendance/Creation
        [PermissionsAttribute(Actions.View, (int)ENTITIES.EnrollNumber, false)]
        public ActionResult EnrollNumber()
        {
            return View();
        }

        #region "EnrollNumberEmployee"

        [HttpPost]
        public JsonNetResult SaveEnrollNumberEmployee()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.RE.Academic.EmployeeSummaryCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.Employee(User.UserId, User.HostName, User.DBName).UpdateEnrollCardNo(beData);
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
        #region "EnrollNumberStudent"

        [HttpPost]
        public JsonNetResult SaveEnrollNumberStudent()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Academic.Transaction.StudentListCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Academic.Transaction.Student(User.UserId, User.HostName, User.DBName).UpdateEnrollCardNo(beData);
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
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Attendance, false)]
        public ActionResult AddAttendance()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult SaveStudentDailyAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var uid = User.UserId;
                var beData = DeserializeObject<AcademicLib.BE.Attendance.AttendanceStudentWiseCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        string idColl = "";
                        foreach (var v in beData)
                        {
                            if (!string.IsNullOrEmpty(idColl))
                                idColl = idColl + ",";

                            idColl = idColl + v.StudentId.ToString();
                        }
                        string[] idArray = idColl.Split(',');

                        var idRes = new AcademicLib.BL.Global(uid, hostName, dbName).GetStudentIdColl(idColl, 1);
                        if (idRes.IsSuccess)
                        {
                            PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(uid, hostName, dbName);
                            var templatesPresentColl = new AcademicLib.BL.Setup.SENT(uid, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.PresentStudent, 3, 3);
                            var templatesAbsentColl = new AcademicLib.BL.Setup.SENT(uid, hostName, dbName).GetSENT((int)AcademicLib.BE.Global.ENTITIES.AbsentStudent, 3, 3);

                            List<Dynamic.BusinessEntity.Global.NotificationLog> notificationColl = new List<Dynamic.BusinessEntity.Global.NotificationLog>();


                            var query = from si in idRes
                                        join p in beData on si.Id equals p.StudentId
                                        select new AcademicLib.SEN.StudentAttendance
                                        {
                                            ClassName = si.ClassName,
                                            Id = si.Id,
                                            Name = si.Name,
                                            RegNo = si.RegNo,
                                            RollNo = si.RollNo,
                                            SectionName = si.SectionName,
                                            TodayAD = si.TodayAD,
                                            TodayBS = si.TodayBS,
                                            UserId = si.UserId,
                                            Attendance = p.Attendance.ToString(),
                                            ForDate = p.ForDate,
                                            InOutMode = p.InOutMode.ToString(),
                                            LateMin = p.LateMin,
                                            Remarks = p.Remarks
                                        };



                            foreach (var st in query)
                            {

                                if (st.Attendance == "PRESENT" || st.Attendance == "LATE")
                                {
                                    if (templatesPresentColl != null)
                                    {
                                        var templateNotifiation = templatesPresentColl[0];

                                        #region "Send Notification"

                                        if (templateNotifiation != null)
                                        {
                                            string tempMSG = templateNotifiation.Description;
                                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.SEN.StudentAttendance), templateNotifiation.Description);
                                            foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                            {
                                                try
                                                {
                                                    tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(st, field.Name).ToString());
                                                }
                                                catch { }
                                            }

                                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                            notification.Content = tempMSG;
                                            notification.ContentPath = "";
                                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE);
                                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE.ToString();
                                            notification.Heading = templateNotifiation.Title;
                                            notification.Subject = templateNotifiation.Title;
                                            notification.UserId = uid;
                                            notification.UserName = User.Identity.Name;
                                            notification.UserIdColl = st.UserId.ToString();
                                            notificationColl.Add(notification);
                                        }


                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (templatesAbsentColl != null)
                                    {
                                        var templateNotifiation = templatesAbsentColl[0];

                                        #region "Send Notification"

                                        if (templateNotifiation != null)
                                        {
                                            string tempMSG = templateNotifiation.Description;
                                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.SEN.StudentAttendance), templateNotifiation.Description);
                                            foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                            {
                                                try
                                                {
                                                    tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(st, field.Name).ToString());
                                                }
                                                catch { }
                                            }

                                            Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                            notification.Content = tempMSG;
                                            notification.ContentPath = "";
                                            notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE);
                                            notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.DAILY_ATTENDANCE.ToString();
                                            notification.Heading = templateNotifiation.Title;
                                            notification.Subject = templateNotifiation.Title;
                                            notification.UserId = uid;
                                            notification.UserName = User.Identity.Name;
                                            notification.UserIdColl = st.UserId.ToString();
                                            notificationColl.Add(notification);
                                        }


                                        #endregion
                                    }
                                }

                            }

                            if (notificationColl != null && notificationColl.Count > 0)
                                globlFun.SendNotification(uid, notificationColl, true);
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
        public JsonNetResult GetStudentDailyAttendance(int ClassId, int? SectionId, DateTime forDate, int InOutMode, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).getClassWiseAttendance(this.AcademicYearId, ClassId, SectionId, forDate, InOutMode, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveStudentSubjectWiseAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Attendance.AttendanceSubjectWiseCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Attendance.AttendanceSubjectWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                    if (resVal.IsSuccess)
                    {
                        string idColl = "";
                        foreach (var v in beData)
                        {
                            if (!string.IsNullOrEmpty(idColl))
                                idColl = idColl + ",";

                            idColl = idColl + v.StudentId.ToString();
                        }
                        string[] idArray = idColl.Split(',');

                        var idRes = new AcademicLib.BL.Global(User.UserId, User.HostName, User.DBName).GetUserIdColl(idColl, 1);

                        if (idRes.IsSuccess)
                        {
                            Dictionary<int, int> userIdColl = new Dictionary<int, int>();
                            foreach (var uc in idRes.ResponseId.Split('#'))
                            {
                                string[] ucSep = uc.Split(',');
                                if (ucSep.Length == 2)
                                {
                                    userIdColl.Add(Convert.ToInt32(ucSep[0]), Convert.ToInt32(ucSep[1]));
                                }
                            }
                            var gloablFN = new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName);
                            foreach (var st in beData)
                            {
                                if (userIdColl.ContainsKey(st.StudentId))
                                {
                                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                    notification.Content = "Attendance : " + st.Attendance.ToString();
                                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.SUBJECTWISE_ATTENDANCE);
                                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.SUBJECTWISE_ATTENDANCE.ToString();
                                    notification.Heading = "Student Subject Attendance";
                                    notification.Subject = "Student Subject Attendance";
                                    notification.UserId = User.UserId;
                                    notification.UserName = User.Identity.Name;
                                    notification.UserIdColl = userIdColl[st.StudentId].ToString();
                                    gloablFN.SendNotification(User.UserId, notification);
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
        public JsonNetResult GetStudentSubjectWiseAttendance(int ClassId, int? SectionId, int SubjectId, DateTime forDate, int InOutMode, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null, int? PeriodId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceSubjectWise(User.UserId, User.HostName, User.DBName).getClassWiseAttendance(this.AcademicYearId, ClassId, SectionId, SubjectId, forDate, InOutMode, BatchId, SemesterId, ClassYearId, PeriodId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveEmployeeDailyAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Attendance.AttendanceEmployeewiseCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Attendance.AttendanceEmployeewise(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetEmployeeDailyAttendance(int DepartmentId, DateTime forDate, int InOutMode)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceEmployeewise(User.UserId, User.HostName, User.DBName).getDepartmentWiseAttendance(DepartmentId, forDate, InOutMode);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult Report()
        {
            return View();
        }

        #region "Device"


        public ActionResult AddDevice()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveDevice()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.Device>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DeviceId.HasValue)
                        beData.DeviceId = 0;

                    resVal = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        string host = HttpContext.Request.UserHostAddress;
                        var com = new Dynamic.DataAccess.Setup.CompanyDetailDB(User.HostName, User.DBName).getCompanyDetailsWithOutLogo(User.UserId,null);
                        var sDevice = new AcademicERP.Global.SMSFunction();
                        sDevice.SaveAttendanceDevice(com.Name, beData.MachineSerialNo, User.DBName, User.HostName, host, (int)beData.DeviceCompany);

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

        [AllowAnonymous]
        [HttpPost]
        public JsonNetResultWithEnum GetAllDeviceList()
        {
            if (User == null)
            {
                var dataColl = new AcademicLib.BL.Attendance.Device(1, hostName, dbName).GetAllDevice(0);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            else
            {
                var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).GetAllDevice(0);

                return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }

        }

        [HttpPost]
        public JsonNetResult GetDeviceById(int DeviceId)
        {
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).GetDeviceById(0, DeviceId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDevice(int DeviceId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).GetDeviceById(0, DeviceId);
                resVal = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).DeleteById(0, DeviceId);

                if (resVal.IsSuccess)
                {
                    var sDevice = new AcademicERP.Global.SMSFunction();
                    sDevice.DelAttendanceDevice(dataColl.MachineSerialNo);
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

        public ActionResult AttendanceRule()
        {
            return View();
        }

        public ActionResult StudentBiometricAttendance()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentDailyBIOAttendance(DateTime forDate, string ClassIdColl, string BatchIdColl, string SemesterIdColl, string ClassYearIdColl)
        {
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getStudentDailyAttendance(this.AcademicYearId, forDate, ClassIdColl, "", BatchIdColl, SemesterIdColl, ClassYearIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentMonthlyBIOAttendance(int YearId,int MonthId,int ClassId,int? SectionId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getStudentMonthlyAttendance(this.AcademicYearId, YearId, MonthId, ClassId, SectionId, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentBIOAttendance(int StudentId,DateTime dateFrom,DateTime dateTo)
        {
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getStudentBIOAttendance(this.AcademicYearId, StudentId,dateFrom,dateTo,null,null,null);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetClassWiseBIOAttendance(DateTime forDate)
        {
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getClassWiseBIOAttendance(this.AcademicYearId, forDate);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult StudentManualAttendance()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetClassWiseSummary(DateTime? fromDate,DateTime? toDate)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).getClassWiseSummary(this.AcademicYearId, null, null, fromDate, toDate);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSTMADaily(int? ClassId,int? SectionId, DateTime forDate,int InOutMode, int? BatchId=null, int? SemesterId=null, int? ClassYearId=null,int? ClassShiftId=null)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).getManualDailyAttendance(this.AcademicYearId, ClassId, SectionId, forDate, InOutMode,BatchId,SemesterId,ClassYearId,ClassShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSTPeriodADaily(int? ClassId, int? SectionId, DateTime forDate, int? BatchId = null, int? ClassYearId = null, int? SemesterId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).getPeriodWiseAttendance(this.AcademicYearId, forDate, ClassId, SectionId,BatchId,ClassYearId,SemesterId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetSubjectWiseAttendance(DateTime fromDate, DateTime toDate, int classId, int? sectionId, int subjectId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).getSubjectWiseAttendance(this.AcademicYearId, fromDate, toDate, classId, sectionId, subjectId, BatchId, SemesterId, ClassYearId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult EmployeeAttendance()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetEmpDailyAttendance(DateTime forDate,string branchIdColl)
        {
            if (!string.IsNullOrEmpty(branchIdColl))
            {
                if (branchIdColl == "0")
                    branchIdColl = "";
            }
            else
                branchIdColl = "";

            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getEmpDailyAttendance(forDate, branchIdColl,false);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpManualDailyAttendance(DateTime forDate,string branchIdColl)
        {
            if (!string.IsNullOrEmpty(branchIdColl))
            {
                if (branchIdColl == "0")
                    branchIdColl = "";
            }
            else
                branchIdColl = "";

            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getEmpDailyAttendance(forDate, branchIdColl,true);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpAbsentList(DateTime forDate)
        {
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getEmpAbsentList(forDate, "");

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetEmpMonthlyBIOAttendance(int YearId, int MonthId,string branchIdColl, int empType,int ForEmp)
        {
            if (!string.IsNullOrEmpty(branchIdColl))
            {
                if (branchIdColl == "0")
                    branchIdColl = "";
            }
            else
                branchIdColl = "";

            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getEmpMonthlyAttendance(YearId, MonthId,branchIdColl, 1, ForEmp);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetEmpWiseAttendance(int employeeId,DateTime fromDate,DateTime toDate)
        {
            int totalAbsent = 0;
            double totalWorkingHour = 0;
            var dataColl = new AcademicLib.BL.Attendance.Device(User.UserId, User.HostName, User.DBName).getEmployeeWiseAttendance(employeeId, fromDate, toDate,null,null, ref totalAbsent, ref totalWorkingHour);
            if (dataColl != null && dataColl.Count > 0)
            {
                dataColl[0].TotalAbsent = totalAbsent;
                dataColl[0].TotalWorkingHour = totalWorkingHour;
            }
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult RdlEmpDateWiseInOut()
        {
            return View();
        }

        #region "Financial Period"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.FinancialPeriod, false)]
        public ActionResult FinancialPeriod()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FinancialPeriod, false)]
        public JsonNetResult SaveFinancialPeriod()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.FinancialPeriod>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.PeriodId.HasValue)
                        beData.PeriodId = 0;

                    resVal = new AcademicLib.BL.Attendance.FinancialPeriod(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllFinancialPeriodList()
        {
            var dataColl = new AcademicLib.BL.Attendance.FinancialPeriod(User.UserId, User.HostName, User.DBName).GetAllFinancialPeriod(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [HttpPost]        
        public JsonNetResult GetFinancialPeriodById(int PeriodId)
        {
            var dataColl = new AcademicLib.BL.Attendance.FinancialPeriod(User.UserId, User.HostName, User.DBName).GetFinancialPeriodById(0, PeriodId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.FinancialPeriod, false)]
        public JsonNetResult DelFinancialPeriod(int PeriodId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.FinancialPeriod(User.UserId, User.HostName, User.DBName).DeleteById(0, PeriodId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetPeriodAutoOrderNo()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.FinancialPeriod(User.UserId, User.HostName, User.DBName).GetPeriodAutoOrderNo(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Leave"

        public ActionResult LeaveType()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveLeaveType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.LeaveType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.LeaveTypeId.HasValue)
                        beData.LeaveTypeId = 0;

                    resVal = new AcademicLib.BL.Attendance.LeaveType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllLeaveTypeList()
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveType(User.UserId, User.HostName, User.DBName).GetAllLeaveType(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetLeaveTypeById(int LeaveTypeId)
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveType(User.UserId, User.HostName, User.DBName).GetLeaveTypeById(0, LeaveTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelLeaveType(int LeaveTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.LeaveType(User.UserId, User.HostName, User.DBName).DeleteById(0, LeaveTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        public ActionResult LeaveQuota()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveLeaveQuota()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.LeaveQuota>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.LeaveQuotaId.HasValue)
                        beData.LeaveQuotaId = 0;

                    resVal = new AcademicLib.BL.Attendance.LeaveQuota(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult DelLeaveQuota(int LeaveQuotaId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.LeaveQuota(User.UserId, User.HostName, User.DBName).DeleteById(0, LeaveQuotaId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResultWithEnum GetAllLeaveQuotaList()
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveQuota(User.UserId, User.HostName, User.DBName).GetAllLeaveQuota(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }
        [HttpPost]
        public JsonNetResult GetLeaveQuotaById(int LeaveQuotaId)
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveQuota(User.UserId, User.HostName, User.DBName).GetLeaveQuotaById(0, LeaveQuotaId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        public ActionResult LeaveOpening()
        {
            return View();
        }
        

        [HttpPost]
        public JsonNetResult SaveLeaveOpening()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.LeaveOpening>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    beData.OpeningQty = beData.LeaveQuotaDetail.Sum(p1 => p1.NoOfLeave??0);
                    resVal = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllLeaveOpeningList()
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).GetAllLeaveOpening(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetLeaveOpeningById(int EmployeeId,int PeriodId)
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).getLeaveOpeningById(EmployeeId, PeriodId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelLeaveOpening(int LeaveOpeningId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).DeleteById(0, LeaveOpeningId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        public ActionResult LeaveRequest()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetEmpLeaveReq(int LeaveStatus,DateTime? dateFrom,DateTime? dateTo,int? EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).getEmpLeaveRequestLst(dateFrom, dateTo, LeaveStatus,EmployeeId);

            var retData = new {
                LeaveColl = dataColl,
                BalanceColl=dataColl.LeaveBalanceColl
            };

            return new JsonNetResult() { Data = retData, TotalCount = dataColl.Count , IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentLeaveReq(int LeaveStatus, DateTime? dateFrom, DateTime? dateTo, int? StudentId, int? ClassId, int? SectionId, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).getStudentLeaveRequestLst(dateFrom, dateTo, LeaveStatus, StudentId, ClassId, SectionId, this.AcademicYearId, BatchId, SemesterId, ClassYearId);

            var retData = new
            {
                LeaveColl = dataColl
            };

            return new JsonNetResult() { Data = retData, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult LeaveApprove(AcademicLib.API.Attendance.LeaveApprove beData)
        {
            ResponeValues resVal = new ResponeValues();

            if (string.IsNullOrEmpty(beData.ApprovedRemarks))
                resVal.ResponseMSG = "Please ! Enter Remarks";
            else if(beData.ApprovedType<=1)
                resVal.ResponseMSG = "Please ! Select Approved Status";
            else
            {
                beData.ApprovedBy = User.UserId;
                beData.ApprovedByUser = User.UserName;
                var retVal = new AcademicLib.BL.Attendance.LeaveRequest(User.UserId, User.HostName, User.DBName).LeaveApproved(beData);

                if (retVal.IsSuccess && !string.IsNullOrEmpty(retVal.CUserName))
                {
                    Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();

                    string approvedTypes = ((AcademicLib.BE.Attendance.APPROVEDTYPES)beData.ApprovedType).ToString();
                    notification.Content = retVal.JsonStr;
                    notification.ContentPath = "";
                    notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_APPROVED);
                    notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.LEAVE_APPROVED.ToString();
                    notification.Heading = "Leave " + approvedTypes;
                    notification.Subject = "Leave " + approvedTypes;
                    notification.UserId = User.UserId;
                    notification.UserName = User.Identity.Name;
                    notification.UserIdColl = retVal.CUserName.Trim();

                    resVal = new PivotalERP.Global.GlobalFunction(User.UserId, hostName, dbName).SendNotification(User.UserId, notification, true);

                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = GLOBALMSG.SUCCESS;
                }
            }


            return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "StudentTypeDailyAttendance"
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveStudentTypeDailyAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.StudentTypeDailyAttendance>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllStudentTypeDailyAttendance()
        {
            var dataColl = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(User.UserId, User.HostName, User.DBName).GetAllStudentTypeDailyAttendance(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]        
        public JsonNetResult GetStudentTypeDailyAttendanceById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(User.UserId, User.HostName, User.DBName).GetStudentTypeDailyAttendanceById(0, TranId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult DelStudentTypeDailyAttendance(int TranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(User.UserId, User.HostName, User.DBName).DeleteById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        public ActionResult EmployeeAttendSummary()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetEmployeeAttendSummary(string BranchIdColl, string DepartmentIdColl, string GroupIdColl, DateTime? DateFrom, DateTime? DateTo, int? EmpType,int ForEmp)
        {
            AcademicLib.RE.Attendance.EmpAttendanceSummaryCollections dataColl = new AcademicLib.RE.Attendance.EmpAttendanceSummaryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.EmpAttendanceSummary(User.UserId, User.HostName, User.DBName).GetEmpAttendanceSummary(BranchIdColl, DepartmentIdColl, GroupIdColl, DateFrom, DateTo, EmpType, ForEmp);
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
        public JsonNetResult GetStudentAttendanceSumary(DateTime? DateFrom, DateTime? DateTo, int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            AcademicLib.RE.Attendance.StudentAttendanceSumaryCollections dataColl = new AcademicLib.RE.Attendance.StudentAttendanceSumaryCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.Reporting.StudentAttendanceSumary(User.UserId, User.HostName, User.DBName).GetStudentAttendanceSumary(DateFrom, DateTo, this.AcademicYearId, ClassId, SectionId, BatchId, SemesterId, ClassYearId, User.BranchId);
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
        public JsonNetResult DeleteStudentManualDailyAttendance(DateTime? ForDate, int? ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AttendaneTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).DeleteStudentManualDailyAttendance(ForDate, ClassId, SectionId, BatchId, ClassYearId, SemesterId, AttendaneTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllPeriodForAttendance(int? BatchId, int? ClassId, int? SectionId, int? SemesterId, int? ClassYearId, int SubjectId)
        {
            AcademicLib.RE.Attendance.PeriodForAttendanceCollections dataColl = new AcademicLib.RE.Attendance.PeriodForAttendanceCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.AttendanceStudentWise(User.UserId, User.HostName, User.DBName).getPeriodForAttendance(0, BatchId, ClassId, SectionId, SemesterId, ClassYearId, SubjectId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        //Delete Subject PeriodWise Attendance
        [HttpPost]
        public JsonNetResult DeleteSubjectWiseAttendance(DateTime? ForDate, int? ClassId, int? SectionId, int? BatchId, int? ClassYearId, int? SemesterId, int? AttendaneTypeId, int? PeriodId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.AttendanceSubjectWise(User.UserId, User.HostName, User.DBName).DeleteSubjectWiseAttendance(ForDate, ClassId, SectionId, BatchId, ClassYearId, SemesterId, AttendaneTypeId, PeriodId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        //Delete EmployeeWise Attendance
        [HttpPost]
        public JsonNetResult DeleteEmployeeWiseAttendance(DateTime? ForDate, int? DepartmentId, int? AttendaneTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.AttendanceEmployeewise(User.UserId, User.HostName, User.DBName).DeleteEmployeeWiseAttendance(ForDate, DepartmentId, AttendaneTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        //Stydent Type Attendance starts
        [HttpPost]
        public JsonNetResult GetStudentTypeWiseAttendance(int StudentTypeId, int ClassId, int? SectionId, DateTime ForDate, int InOutMode, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(User.UserId, User.HostName, User.DBName).getTypeWiseStudentAttendance(this.AcademicYearId, StudentTypeId, ClassId, SectionId, ForDate, InOutMode, BatchId, SemesterId, ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult SaveUpdateStudentTypeWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.StudentTypeDailyAttendanceCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Attendance.StudentTypeDailyAttendance(User.UserId, User.HostName, User.DBName).SaveUpdateStudentTypeWise(beData);
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
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ClassSetup, false)]
        public JsonNetResult SaveBioAttendence()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.BioAttendenceCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Attendance.BioAttendence(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllBioAttendence(int? StudentId, int? EmployeeId)
        {
            var dataColl = new AcademicLib.BL.Attendance.BioAttendence(User.UserId, User.HostName, User.DBName).GetAllBioAttendence(StudentId, EmployeeId);

            return new JsonNetResult()
            {
                Data = dataColl,
                TotalCount = dataColl.Count,
                IsSuccess = dataColl.IsSuccess,
                ResponseMSG = dataColl.ResponseMSG
            };
        }

        #region "Pending Attendance"
        [HttpPost]
        public JsonNetResult GetPendingAttendance(int? ClassId, int? SectionId, int? BatchId, int? SemesterId, int? ClassYearId, DateTime? DateFrom, DateTime? DateTo, int For = 1)
        {
            var dataColl = new AcademicLib.BL.Attendance.PendingAttendance(User.UserId, User.HostName, User.DBName).GetPendngAttendanace(this.AcademicYearId, ClassId, SectionId, BatchId, SemesterId, ClassYearId, DateFrom, DateTo, For);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        #region "Allow Leave Type"
        public ActionResult AllowLeaveType()
        {
            return View();
        }
               
        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Modify, (int)FormsEntity.AllowLeaveType)]
        public JsonNetResult GetAllEmployeeForAllowLeaveType(int? BranchId, int? DepartmentId, int? DesignationId, int? LevelId, int? EmployeeGroupId)
        {
            AcademicLib.BE.Attendance.EmployeeForAllowLeaveTypeCollections dataColl = new AcademicLib.BE.Attendance.EmployeeForAllowLeaveTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.AllowLeaveType(User.UserId, User.HostName, User.DBName).getAllAllowLeaveType(0, BranchId, DepartmentId, DesignationId, LevelId, EmployeeGroupId);
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
        //[ValidateInput(false)]
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Save, (int)FormsEntity.AllowLeaveType)]
        public JsonNetResult SaveAllowLeaveType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.AllowLeaveTypeCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Attendance.AllowLeaveType(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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
        public JsonNetResult DelAllowLeaveType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.AllowLeaveType(User.UserId, User.HostName, User.DBName).DeleteById(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        public ActionResult EmployeeWiseLeaveQuota()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetAllEmpLeaveQuota(int? BranchId, int? DepartmentId, int? CategoryId, int PeriodId)
        {
            AcademicLib.BE.Attendance.LeaveQuotaByEmpCollections dataColl = new AcademicLib.BE.Attendance.LeaveQuotaByEmpCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.LeaveQuotaByEmp(User.UserId, User.HostName, User.DBName).getAllEmpLeaveQuota(BranchId, DepartmentId, CategoryId, PeriodId);
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
        [ValidateInput(false)]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Att_LeaveQuotaByEmployeeColl)]
        public JsonNetResult SaveLeaveQuotaColl()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.LeaveQuotaByEmpCollections>(Request["jsonData"]);

                if (beData != null && beData.Count > 0)
                {
                    // Loop through each LeaveQuotaByEmp object to calculate and set OpeningQty
                    foreach (var empQuota in beData)
                    {
                        if (empQuota.LeaveQuotaByEmpDetailsColl != null)
                        {
                            foreach (var detail in empQuota.LeaveQuotaByEmpDetailsColl)
                            {
                                detail.NoOfLeave = detail.NoOfLeave ?? 0;
                            }
                            empQuota.OpeningQty = empQuota.LeaveQuotaByEmpDetailsColl.Sum(detail => detail.NoOfLeave.Value);
                        }
                    }

                    // Call the SaveUpdate method with the updated beData
                    resVal = new AcademicLib.BL.Attendance.LeaveQuotaByEmp(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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
        [ValidateInput(false)]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Att_LeaveQuotaByEmployee)]
        public JsonNetResult SaveLeaveQuotaByEmp()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.LeaveQuotaByEmp>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    beData.OpeningQty = beData.LeaveQuotaByEmpDetailsColl.Sum(p1 => p1.NoOfLeave);
                    resVal = new AcademicLib.BL.Attendance.LeaveQuotaByEmp(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Att_LeaveQuotaByEmployee)]
        public JsonNetResult DelLeaveQuotaByEmp(int? TranId, int? PeriodId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.LeaveQuotaByEmp(User.UserId, User.HostName, User.DBName).DeleteById(TranId, PeriodId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        public ActionResult EmployeeWiseLeaveOpening()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetAllEmpLeaveOpening(int? BranchId, int? DepartmentId, int? CategoryId, int PeriodId)
        {
            AcademicLib.BE.Attendance.LeaveOpeningCollections dataColl = new AcademicLib.BE.Attendance.LeaveOpeningCollections();
            try
            {
                dataColl = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).getAllEmpLeaveOpening(BranchId, DepartmentId, CategoryId, PeriodId);
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
        [ValidateInput(false)]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Att_FinancialYrWiseLeaveOpening)]
        public JsonNetResult SaveLeaveOpeningColl()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Attendance.LeaveOpeningCollections>(Request["jsonData"]);
                if (beData != null && beData.Count > 0)
                {
                    //beData.CUserId = User.UserId;
                    //beData.OpeningQty = beData.LeaveQuotaDetail.Sum(p1 => p1.NoOfLeave);
                    foreach (var empQuota in beData)
                    {
                        if (empQuota.LeaveQuotaDetail != null)
                        {
                            foreach (var detail in empQuota.LeaveQuotaDetail)
                            {
                                detail.NoOfLeave = detail.NoOfLeave;
                            }
                            empQuota.OpeningQty = empQuota.LeaveQuotaDetail.Sum(detail => detail.NoOfLeave ?? 0);//nullable
                        }
                    }
                    resVal = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Att_FinancialYrWiseLeaveOpening)]
        public JsonNetResult DelLeaveOpeningEmpWise(int? TranId, int? PeriodId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Attendance.LeaveOpening(User.UserId, User.HostName, User.DBName).DeleteEmpById(TranId, PeriodId);
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