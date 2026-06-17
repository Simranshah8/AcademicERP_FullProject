using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Infirmary.Controllers
{
    public class ReportController : PivotalERP.Controllers.BaseController
    {
        // GET: Infirmary/Report
        public ActionResult StudentHealthReport()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentHealthReport(DateTime? DateFrom, DateTime? DateTo)
        {
            var dataColl = new AcademicLib.BL.StudentHealthReport(User.UserId, User.HostName, User.DBName).GetStudentHealthReport(DateFrom, DateTo);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentHealthPastHistory(DateTime? DateFrom, DateTime? DateTo)
        {
            var dataColl = new AcademicLib.BL.StudentHealthReport(User.UserId, User.HostName, User.DBName).GetStudentHealthPastHistory(DateFrom, DateTo);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
    }
}