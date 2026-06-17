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
namespace PivotalERP.Areas.Hostel.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        [PermissionsAttribute(Actions.View, (int)ENTITIES.AddHostel, false)]
        public ActionResult AddHostel()
        {
            return View();
        }
        #region "Hostel"

        [HttpPost]
        public JsonNetResult SaveHostel()
        {            
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Hostel.Hostel>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;


                    if (!beData.HostelId.HasValue)
                        beData.HostelId = 0;

                    resVal = new AcademicLib.BL.Hostel.Hostel(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllHostelList()
        {
            var dataColl = new AcademicLib.BL.Hostel.Hostel(User.UserId, User.HostName, User.DBName).GetAllHostel(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetHostelById(int HostelId)
        {
            var dataColl = new AcademicLib.BL.Hostel.Hostel(User.UserId, User.HostName, User.DBName).GetHostelById(0, HostelId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHostel(int HostelId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.Hostel(User.UserId, User.HostName, User.DBName).DeleteById(0, HostelId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Building"

        [HttpPost]
        public JsonNetResult SaveBuilding()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Hostel.Building>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }                        
                    }
                  
                    if (!beData.BuildingId.HasValue)
                        beData.BuildingId = 0;

                    resVal = new AcademicLib.BL.Hostel.Building(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllBuildingList()
        {
            var dataColl = new AcademicLib.BL.Hostel.Building(User.UserId, User.HostName, User.DBName).GetAllBuilding(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetBuildingById(int BuildingId)
        {
            var dataColl = new AcademicLib.BL.Hostel.Building(User.UserId, User.HostName, User.DBName).GetBuildingById(0, BuildingId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBuilding(int BuildingId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.Building(User.UserId, User.HostName, User.DBName).DeleteById(0, BuildingId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Floor"

        [HttpPost]
        public JsonNetResult SaveFloor()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Hostel.Floor>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;


                    if (!beData.FloorId.HasValue)
                        beData.FloorId = 0;

                    resVal = new AcademicLib.BL.Hostel.Floor(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllFloorList()
        {
            var dataColl = new AcademicLib.BL.Hostel.Floor(User.UserId, User.HostName, User.DBName).GetAllFloor(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFloorById(int FloorId)
        {
            var dataColl = new AcademicLib.BL.Hostel.Floor(User.UserId, User.HostName, User.DBName).GetFloorById(0, FloorId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFloor(int FloorId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.Floor(User.UserId, User.HostName, User.DBName).DeleteById(0, FloorId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "Room"

        [HttpPost]
        public JsonNetResult SaveRoom()
        {
            string photoLocation = "/Attachments/academic/student";
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Hostel.Room>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.ImagePath = photoDoc.DocPath;
                        }
                    }

                    if (!beData.RoomId.HasValue)
                        beData.RoomId = 0;

                    resVal = new AcademicLib.BL.Hostel.Room(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllRoomList()
        {
            var dataColl = new AcademicLib.BL.Hostel.Room(User.UserId, User.HostName, User.DBName).GetAllRoom(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetAllRoomListForMapping()
        {
            var dataColl = new AcademicLib.BL.Hostel.Room(User.UserId, User.HostName, User.DBName).getAllRoomForMapping();

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetRoomById(int RoomId)
        {
            var dataColl = new AcademicLib.BL.Hostel.Room(User.UserId, User.HostName, User.DBName).GetRoomById(0, RoomId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelRoom(int RoomId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.Room(User.UserId, User.HostName, User.DBName).DeleteById(0, RoomId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.BedMapping, false)]
        public ActionResult BedMapping()
        {
            return View();
        }
        #region "BedMapping"

        [HttpPost]
        public JsonNetResult SaveBedMapping()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<AcademicLib.BE.Hostel.BedMappingCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
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
        public JsonNetResult GetAllBedMapping(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(0,this.AcademicYearId, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBedMapping(int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).Delete(0, this.AcademicYearId, ClassId, SectionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult BedMappingForMonth(int ForMonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                resVal = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).SaveForMonth(this.AcademicYearId, ForMonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBedMappingForMonth(int ForMonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                resVal = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).DeleteForMonth(this.AcademicYearId, ForMonthId);

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        public ActionResult HostelRpt()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetStudentSummary(string ClassIdColl, string SectionIdColl, string RoomIdColl,string BatchIdColl=null, string SemesterIdColl = null, string ClassYearIdColl = null)
        {

            var dataColl = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).getStudentSummaryList(this.AcademicYearId, ClassIdColl, SectionIdColl, RoomIdColl,BatchIdColl,SemesterIdColl,ClassYearIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetStudentSummaryForMonth(int ForMonthId)
        {

            var dataColl = new AcademicLib.BL.Hostel.BedMapping(User.UserId, User.HostName, User.DBName).getStudentSummaryForMonth(this.AcademicYearId, ForMonthId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult PrintStudentSummary()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Hostel.StudentSummary> paraData = DeserializeObject<List<AcademicLib.RE.Hostel.StudentSummary>>(jsonData);
            ResponeValues resVal = new ResponeValues();
            try
            {
                var key = Guid.NewGuid().ToString().Replace("-", "");
                Session.Add(key, paraData);
                resVal.ResponseId = key;
                resVal.IsSuccess = true;
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "HostelAttendanceStatus"

        [HttpPost]
        public JsonNetResult SaveHostelAttendanceStatus()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Hostel.HostelAttendanceStatus>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AttendanceStatusId.HasValue)
                        beData.AttendanceStatusId = 0;

                    resVal = new AcademicLib.BL.Hostel.HostelAttendanceStatus(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllHostelAttendanceStatusList()
        {
            var dataColl = new AcademicLib.BL.Hostel.HostelAttendanceStatus(User.UserId, User.HostName, User.DBName).GetAllHostelAttendanceStatus(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetHostelAttendanceStatusById(int AttendanceStatusId)
        {
            var dataColl = new AcademicLib.BL.Hostel.HostelAttendanceStatus(User.UserId, User.HostName, User.DBName).GetHostelAttendanceStatusById(0, AttendanceStatusId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHostelAttendanceStatus(int AttendanceStatusId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.HostelAttendanceStatus(User.UserId, User.HostName, User.DBName).DeleteById(0, AttendanceStatusId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion


        #region "HostelAttendanceShift"

        [HttpPost]
        public JsonNetResult SaveHostelAttendanceShift()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Hostel.HostelAttendanceShift>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.AttendanceShiftId.HasValue)
                        beData.AttendanceShiftId = 0;

                    resVal = new AcademicLib.BL.Hostel.HostelAttendanceShift(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResultWithEnum GetAllHostelAttendanceShiftList()
        {
            var dataColl = new AcademicLib.BL.Hostel.HostelAttendanceShift(User.UserId, User.HostName, User.DBName).GetAllHostelAttendanceShift(0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [HttpPost]
        public JsonNetResult GetHostelAttendanceShiftById(int AttendanceShiftId)
        {
            var dataColl = new AcademicLib.BL.Hostel.HostelAttendanceShift(User.UserId, User.HostName, User.DBName).GetHostelAttendanceShiftById(0, AttendanceShiftId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelHostelAttendanceShift(int AttendanceShiftId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Hostel.HostelAttendanceShift(User.UserId, User.HostName, User.DBName).DeleteById(0, AttendanceShiftId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion


        #region "GatePass"

        [HttpPost]
        public JsonNetResult SaveGatePass()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Hostel.GatePassConfig>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Hostel.GatePassConfig(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetGatePass(int? BranchId = null)
        {
            var dataColl = new AcademicLib.BL.Hostel.GatePassConfig(User.UserId, User.HostName, User.DBName).GetConfiguration(0, BranchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        #endregion

        #region "Hostel Attendance"

        [HttpPost]
        public JsonNetResult GetAllHostelAttendance(int? HostelId, int? BuildingId, int? FloorId, DateTime? ForDate, int? ShiftId)
        {
            var dataColl = new AcademicLib.BL.Hostel.HostelAttendance(User.UserId, User.HostName, User.DBName).getAllHostelAttendance(this.AcademicYearId, HostelId, BuildingId, FloorId, ForDate, ShiftId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult SaveHostelAttendance()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Hostel.HostelAttendanceCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Hostel.HostelAttendance(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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

         
        public ActionResult HostelGuardian()
        {
            return View();
        }

      


        public ActionResult Routine()
        {
            return View();
        }

        public ActionResult GatePass()
        {
            return View();
        }

        public ActionResult Attendance()
        {
            return View();
        }

        public ActionResult HostelSetup()
        {
            return View();
        }
    }
}