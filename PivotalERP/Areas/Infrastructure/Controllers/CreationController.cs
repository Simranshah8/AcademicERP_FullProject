using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Infrastructure.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Infrastructure/Creation
        string photoLocation = "/Attachments/Infrastructure/img";
        public ActionResult BuildingBlock()
        {
            return View();
        }
        public ActionResult EnvInformation()
        {
            return View();
        }
        public ActionResult ProposedSite()
        {
            return View();
        }

        public ActionResult OtherInformation()
        {
            return View();
        }
        public ActionResult GeneralInformation()
        {
            return View();
        }

        public ActionResult BuildingDetails()
        {
            return View();
        }
        public ActionResult VehicleDetails()
        {
            return View();
        }
        public ActionResult RoomDetails()
        {
            return View();
        }
        #region "GeneralInformation"
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.View, (int)FormsEntity.GeneralInformation)]

        [HttpPost]
        ////[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Save, (int)FormsEntity.GeneralInformation)]
        public JsonNetResult SaveUpdateGeneralInformation()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.GeneralInformation>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Infrastructure.GeneralInformation(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(PivotalERP.BE.Global.Actions.Modify, (int)FormsEntity.GeneralInformation)]
        public JsonNetResult getGeneralInformationById(int TranId)
        {
            AcademicLib.BE.Infrastructure.GeneralInformation resVal = new AcademicLib.BE.Infrastructure.GeneralInformation();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.GeneralInformation(User.UserId, User.HostName, User.DBName).GetGeneralInformationById(0, TranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        
        [HttpPost]
        public JsonNetResult GetAllGeneralInformation()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.GeneralInformation(User.UserId, User.HostName, User.DBName).GetAllGeneralInformation(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }



        [HttpPost]
        //[PermissionsAttribute(PivotalERP.BE.Global.Actions.Modify, (int)FormsEntity.GeneralInformation)]
        public JsonNetResult getEmpShortDetById(int EmployeeId)
        {
            AcademicLib.BE.Infrastructure.EmpShortDet resVal = new AcademicLib.BE.Infrastructure.EmpShortDet();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.GeneralInformation(User.UserId, User.HostName, User.DBName).GetEmpShortDetbyId(0, EmployeeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion


        #region"Land Details"
        public ActionResult LandDetails()
        {
            return View();
        }


        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.LandDetails)]
        public JsonNetResult SaveUpdateLandDetails()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.LandDetails>(Request["jsonData"]);
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
                            beData.Photo = photoDoc.Data;
                            beData.Attachment = photoDoc.DocPath;
                        }
                    }
                    bool isModify = false;
                    if (!beData.LandDetailsId.HasValue)
                        beData.LandDetailsId = 0;

                    resVal = new AcademicLib.BL.Infrastructure.LandDetails(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Infrastructure.Global.Actions.Modify, (int)FormsEntity.LandDetails)]
        public JsonNetResult getLandDetailsById(int LandDetailsId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.LandDetails(User.UserId, User.HostName, User.DBName).GetLandDetailsById(0, LandDetailsId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.LandDetails)]
        public JsonNetResult DeleteLandDetails(int LandDetailsId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (LandDetailsId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Infrastructure.LandDetails(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, LandDetailsId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllLandDetails()
        {
            AcademicLib.BE.Infrastructure.LandDetailsCollections dataColl = new AcademicLib.BE.Infrastructure.LandDetailsCollections();
            try
            {
                dataColl = new AcademicLib.BL.Infrastructure.LandDetails(User.UserId, User.HostName, User.DBName).GetAllLandDetails(0);
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


        #region "FloorMapping"


        [HttpPost]
        public JsonNetResult SaveFloorMappingColl()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.FloorMappingCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Infrastructure.FloorMapping(User.UserId, User.HostName, User.DBName).SaveFormDataColl(beData);
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
        public JsonNetResult GetAllFloorMapping(int? BuildingId)
        {
            var dataColl = new AcademicLib.BL.Infrastructure.FloorMapping(User.UserId, User.HostName, User.DBName).GetAllFloorMapping(0, BuildingId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetFloorMappingById(int BuildingId)
        {
            var dataColl = new AcademicLib.BL.Infrastructure.FloorMapping(User.UserId, User.HostName, User.DBName).GetFloorMappingById(0, BuildingId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DetailsByBuildingFloor(int BuildingId, int FloorId)
        {
            var dataColl = new AcademicLib.BL.Infrastructure.FloorMapping(User.UserId, User.HostName, User.DBName).DetailsByBuildingFloor(0, BuildingId, FloorId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "FloorWise RoomDetails"


        [HttpPost]
        public JsonNetResult SaveFloorwiseRoomDetails()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.FloorWiseRoomDetailsCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.Infrastructure.FloorWiseRoomDetails(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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

        //public JsonNetResult SaveFloorwiseRoomDetails()
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {
        //        var beData = DeserializeObject<AcademicLib.BE.Infrastructure.FloorWiseRoomDetails>(Request["jsonData"]);
        //        if (beData != null)
        //        {
        //            beData.CUserId = User.UserId;
        //            if (!beData.FloorWiseRoomDetailsId.HasValue)
        //                beData.FloorWiseRoomDetailsId = 0;
        //            resVal = new AcademicLib.BL.Infrastructure.FloorWiseRoomDetails(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

        //            //foreach (var data in beData.subFloorWiseRoomDetailsColl)
        //            //{
        //            //    if (string.IsNullOrWhiteSpace(data.Name))
        //            //        continue;
        //            //    var newBeData = new AcademicLib.BE.Infrastructure.FloorWiseRoomDetails
        //            //    {
        //            //        FloorWiseRoomDetailsId = beData.FloorWiseRoomDetailsId,
        //            //        BuildingId = beData.BuildingId,
        //            //        FloorId = beData.FloorId,
        //            //        Name = data.Name,
        //            //        UtilitiesId = data.UtilitiesId,
        //            //        SubUtility = data.SubUtility,
        //            //        Length = data.Length,
        //            //        Breadth = data.Breadth,
        //            //        Capacity = data.Capacity,
        //            //        Resources = data.Resources,
        //            //        RoomTypeId = data.RoomTypeId
        //            //    };
        //            //    newBeData.CUserId = beData.CUserId;
        //            //    if (resVal.IsSuccess == false)
        //            //        throw new Exception(resVal.ResponseMSG);

        //            //}
        //        }
        //        else
        //        {
        //            resVal.ResponseMSG = "Blank Data Can't be Accept";
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}
        [HttpPost]
        public JsonNetResult GetAllFloorWiseRoomDetails(int? BuildingId,int? FloorId)
        {
            var dataColl = new AcademicLib.BL.Infrastructure.FloorWiseRoomDetails(User.UserId, User.HostName, User.DBName).GetAllFloorWiseRoomDetails(0, BuildingId, FloorId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        //[HttpPost]
        //public JsonNetResult DelFloorwiseRoomDetails(int FloorWiseRoomDetailsId)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {
        //        resVal = new AcademicLib.BL.Infrastructure.FloorWiseRoomDetails(User.UserId, User.HostName, User.DBName).DeleteById(0, FloorWiseRoomDetailsId);
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}
        #endregion

        #region"Building Type"
        public ActionResult BuildingType()
        {
            return View();
        }

        [HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.BuildingType)]
        public JsonNetResult SaveBuildingType()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.Creation.BuildingType>(Request["jsonData"]);
                if (beData != null)
                {
                    if (!beData.BuildingTypeId.HasValue)
                        beData.BuildingTypeId = 0;

                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Infrastructure.Creation.BuildingType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(AcademicLib.BE.Infrastructure.Creation.Global.Actions.Modify, (int)FormsEntity.BuildingType)]
        public JsonNetResult getBuildingTypeById(int BuildingTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.Creation.BuildingType(User.UserId, User.HostName, User.DBName).GetBuildingTypeById(0, BuildingTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.BuildingType)]
        public JsonNetResult DeleteBuildingType(int BuildingTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (BuildingTypeId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Infrastructure.Creation.BuildingType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, BuildingTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllBuildingType()
        {
            AcademicLib.BE.Infrastructure.Creation.BuildingTypeCollections dataColl = new AcademicLib.BE.Infrastructure.Creation.BuildingTypeCollections();
            try
            {
                dataColl = new AcademicLib.BL.Infrastructure.Creation.BuildingType(User.UserId, User.HostName, User.DBName).GetAllBuildingType(0);
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