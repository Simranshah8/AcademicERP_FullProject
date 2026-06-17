using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Infrastructure.Controllers
{
    public class ReportingController : PivotalERP.Controllers.BaseController
    {
        // GET: Infrastructure/Reporting
        public ActionResult PIDashboard()
        {
            return View();
        }

        //Added By Suresh on 15 Falgun for RoomDistribution

        [HttpPost]
        public JsonNetResult GetAllRoomDistribution()
        {
            AcademicLib.RE.Infrastructure.RoomDistributionCollections dataColl = new AcademicLib.RE.Infrastructure.RoomDistributionCollections();
            try
            {
                dataColl = new AcademicLib.BL.Infrastructure.FloorWiseRoomDetails(User.UserId, User.HostName, User.DBName).getAllRoomDistribution(0);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
    }
}