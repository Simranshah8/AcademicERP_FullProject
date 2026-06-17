using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.OnlineClass.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: OnlineClass/Creation
        public ActionResult OnlineClass()
        {
            return View();
        }
        public ActionResult EmployeeAttendance()
        {
            return View();
        }
        [HttpPost]
        public JsonNetResult GetEmployeeOnlineAttendance(DateTime fromDate, DateTime toDate)
        {
            var dataColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(User.UserId, User.HostName, User.DBName).getEmployeeAttendance(fromDate, toDate);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetEmployeeOnlineAttendanceDet(string tranIdColl)
        {
            var dataColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(User.UserId, User.HostName, User.DBName).getEmployeeAttendanceDet(tranIdColl);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public ActionResult StudentAttendance()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetDateWiseOnlineAttendance(DateTime forDate,int classId,int? sectionId)
        {
            var dataColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(User.UserId, User.HostName, User.DBName).getDateWiseAttendance(forDate, classId, sectionId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetSubjectWiseOnlineAttendance(DateTime fromDate,DateTime toDate, int classId, int? sectionId,int subjectId)
        {
            var dataColl = new AcademicLib.BL.OnlineClass.OnlinePlatform(User.UserId, User.HostName, User.DBName).getSubjectWiseAttendance(fromDate, toDate, classId, sectionId, subjectId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
    }
}