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
namespace PivotalERP.Areas.Transport.Controllers
{
    public class ReportController : PivotalERP.Controllers.BaseController
    {
        // GET: TransportManagement/Report

        [PermissionsAttribute(Actions.View, (int)ENTITIES.TransportMapping, false)]
        public ActionResult TransportReport()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentSummary(string ClassIdColl, string SectionIdColl, string RouteIdColl, string PointIdColl, string BatchIdColl, string SemesterIdColl, string ClassYearIdColl)
        {

            var dataColl = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).getStudentSummaryList(this.AcademicYearId, ClassIdColl,SectionIdColl,RouteIdColl,PointIdColl, BatchIdColl, SemesterIdColl, ClassYearIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentSummaryForMonth(int ForMonthId)
        {

            var dataColl = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).getStudentSummaryForMonth(this.AcademicYearId, ForMonthId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
    }
}