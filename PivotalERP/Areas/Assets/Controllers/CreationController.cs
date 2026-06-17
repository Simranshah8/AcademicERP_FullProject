using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Assets.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Assets/Creation
        public ActionResult AssetsManagement()
        {
            return View();
        }

        #region "Assets"
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.View, (int)FormsEntity.Assets)]

        [HttpPost]
        ////[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Assets)]
    
        public JsonNetResult SaveUpdateAssets()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.AssetsMgmt.AssetsCollections>(Request["jsonData"]);
                if (beData != null)
                {

                    resVal = new AcademicLib.BL.AssetsMgmt.Assets(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllAssets(int? BuildingId, int? FloorId, int? RoomTypeId, int? RoomId)
        {
            AcademicLib.BE.AssetsMgmt.AssetsCollections dataColl = new AcademicLib.BE.AssetsMgmt.AssetsCollections();
            try
            {
                dataColl = new AcademicLib.BL.AssetsMgmt.Assets(User.UserId, User.HostName, User.DBName).GetAllAssets(0, BuildingId, FloorId, RoomTypeId, RoomId);
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
        public JsonNetResult GetAllFllorwiseRoom(int? BuildingId, int? FloorId, int? RoomTypeId)
        {
            AcademicLib.BE.AssetsMgmt.FloorwiseRoomCollections dataColl = new AcademicLib.BE.AssetsMgmt.FloorwiseRoomCollections();
            try
            {
                dataColl = new AcademicLib.BL.AssetsMgmt.Assets(User.UserId, User.HostName, User.DBName).GetAllFloorwiseRoom(0, BuildingId, FloorId, RoomTypeId);
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
        public JsonNetResult GetAllAssetProduct()
        {
            AcademicLib.BE.AssetsMgmt.AssetsProductCollections dataColl = new AcademicLib.BE.AssetsMgmt.AssetsProductCollections();
            try
            {
                dataColl = new AcademicLib.BL.AssetsMgmt.Assets(User.UserId, User.HostName, User.DBName).GetAllAssetProduct(0);
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