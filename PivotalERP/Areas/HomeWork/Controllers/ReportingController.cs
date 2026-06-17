using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Homework.Controllers
{
    public class ReportingController : PivotalERP.Controllers.BaseController
    {
        // GET: Homework/Reporting
        public ActionResult HADashboard()
        {
            return View();
        }


        #region "HA Dashboard"
        [HttpPost]
        public JsonNetResult GetDashboardData()
        {
            var dataColl = new AcademicLib.BL.Homework.Reporting.HADashboard(User.UserId, User.HostName, User.DBName).GetDashboardData(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        public ActionResult HAAnalysis()
        {
            return View();
        }

        #region "HA Analysis"
        [HttpPost]
        public JsonNetResult GetAnalysisData()
        {
            var dataColl = new AcademicLib.BL.Homework.Reporting.HAAnalysis(User.UserId, User.HostName, User.DBName).GetAnalysisData(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion
    }
}