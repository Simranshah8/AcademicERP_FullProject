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
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        
// "AIzaSyAK7wj3PCXVi1VqDhO4TfUo2RR1P58BjAc";

        // GET: TransportManagement/Creation
        [PermissionsAttribute(Actions.View, (int)ENTITIES.TransportSetup, false)]
        public ActionResult TransportSetup()
        {
            ViewBag.API_KEY = googleMAP_APIKEY;
            return View();
        }
        #region "Vehicle"

        [HttpPost]
        public JsonNetResult SaveVehicle()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Transport.Creation.Vehicle>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    var tmpAttachmentColl = beData.AttachmentColl;

                    beData.AttachmentColl = new Dynamic.BusinessEntity.GeneralDocumentCollections();
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];
                        
                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);                            
                            beData.ImagePath = photoDoc.DocPath;
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
                    }else
                    {
                        if (tmpAttachmentColl != null)
                            beData.AttachmentColl = tmpAttachmentColl;
                    }

                    if (!beData.VehicleId.HasValue)
                        beData.VehicleId = 0;

                    resVal = new AcademicLib.BL.Transport.Creation.Vehicle(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllVehicleList()
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.Vehicle(User.UserId, User.HostName, User.DBName).GetAllVehicle(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetVehicleById(int VehicleId)
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.Vehicle(User.UserId, User.HostName, User.DBName).GetVehicleById(0, VehicleId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelVehicle(int VehicleId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Transport.Creation.Vehicle(User.UserId, User.HostName, User.DBName).DeleteById(0, VehicleId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion
        #region "TransportRoute"

        [HttpPost]
        public JsonNetResult SaveTransportRoute()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Transport.Creation.TransportRoute>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.RouteId.HasValue)
                        beData.RouteId = 0;

                    resVal = new AcademicLib.BL.Transport.Creation.TransportRoute(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllTransportRouteList()
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.TransportRoute(User.UserId, User.HostName, User.DBName).GetAllTransportRoute(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTransportRouteById(int TransportRouteId)
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.TransportRoute(User.UserId, User.HostName, User.DBName).GetTransportRouteById(0, TransportRouteId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelTransportRoute(int TransportRouteId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Transport.Creation.TransportRoute(User.UserId, User.HostName, User.DBName).DeleteById(0, TransportRouteId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "TransportPoint"

        [HttpPost]
        public JsonNetResult SaveTransportPoint()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Transport.Creation.TransportPoint>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.PointId.HasValue)
                        beData.PointId = 0;

                    resVal = new AcademicLib.BL.Transport.Creation.TransportPoint(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllTransportPointList()
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.TransportPoint(User.UserId, User.HostName, User.DBName).GetAllTransportPoint(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetTransportPointById(int TransportPointId)
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.TransportPoint(User.UserId, User.HostName, User.DBName).GetTransportPointById(0, TransportPointId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelTransportPoint(int TransportPointId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Transport.Creation.TransportPoint(User.UserId, User.HostName, User.DBName).DeleteById(0, TransportPointId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.TransportMapping, false)]
        public ActionResult TransportMapping()
        {
            return View();
        }
        #region "TransportMapping"

        [HttpPost]
        public JsonNetResult SaveTransportMapping()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Transport.Creation.TransportMappingCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
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
        public JsonNetResult TransportMappingForMonth(int ForMonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                resVal = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).SaveForMonth(this.AcademicYearId, ForMonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult DelTransportMappingForMonth(int ForMonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                resVal = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).DeleteForMonth(this.AcademicYearId, ForMonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllTransportMapping(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelTransportMapping(int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Transport.Creation.TransportMapping(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, ClassId, SectionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Transport Attendance"


        public ActionResult TransportAttendance()
        {
            return View();
        }


        [HttpPost]
        public JsonNetResult GetTransportRouteByVehicle(int VehicleId)
        {
            var dataColl = new AcademicLib.BL.Transport.Creation.TransportRoute(User.UserId, User.HostName, User.DBName).getTransportRouteByVehicleId(VehicleId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]        
        public JsonNetResult SaveTransportAtt()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Transport.Creation.TransportAttCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Transport.Creation.TransportAtt(User.UserId, User.HostName, User.DBName).SaveUpdate(beData);
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
        public JsonNetResult GetStudentByTransportRout(DateTime ForDate, int VehicleId, int RouteId,  int AttendanceForId)
        {
            AcademicLib.BE.Transport.Creation.TransportAttCollections dataColl = new AcademicLib.BE.Transport.Creation.TransportAttCollections();
            try
            {
                dataColl = new AcademicLib.BL.Transport.Creation.TransportAtt(User.UserId, User.HostName, User.DBName).GetAllStudentTransportAtt(0, VehicleId, RouteId, ForDate, AttendanceForId, this.AcademicYearId);
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
        public JsonNetResult GetAllTransportAttendanceList(DateTime? DateFrom, DateTime? DateTo)
        {
            AcademicLib.BE.Transport.Creation.TransportAttCollections dataColl = new AcademicLib.BE.Transport.Creation.TransportAttCollections();
            try
            {
                dataColl = new AcademicLib.BL.Transport.Creation.TransportAtt(User.UserId, User.HostName, User.DBName).GetAllTransportAttendanceList(  DateFrom, DateTo);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        //Create Functiuonby Prashant
        [HttpPost]
        public JsonNetResult GetTransportAttDetail( DateTime ForDate, int VehicleId, int RouteId)
        {
            AcademicLib.BE.Transport.Creation.TransportAttCollections dataColl = new AcademicLib.BE.Transport.Creation.TransportAttCollections();
            try
            {
                dataColl = new AcademicLib.BL.Transport.Creation.TransportAtt(User.UserId, User.HostName, User.DBName).GetTransportAttDetail( 0, ForDate, VehicleId, RouteId);
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