using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Infrastructure.Controllers
{
    public class SetupController : PivotalERP.Controllers.BaseController
    {
        // GET: Infrastructure/Setup
        public ActionResult InfrastructureSetup()
        {
            return View();
        }
        public ActionResult Floor()
        {
            return View();
        }


        #region "Utilities"
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.View, (int)FormsEntity.Utilities)]

        [HttpPost]
        ////[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Utilities)]
        public JsonNetResult SaveUpdateUtilities()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.Utilities>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.UtilitiesId.HasValue)
                        beData.UtilitiesId = 0;

                    resVal = new AcademicLib.BL.Infrastructure.Utilities(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(PivotalERP.BE.Global.Actions.Modify, (int)FormsEntity.Utilities)]
        public JsonNetResult getUtilitiesById(int UtilitiesId)
        {
            AcademicLib.BE.Infrastructure.Utilities resVal = new AcademicLib.BE.Infrastructure.Utilities();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.Utilities(User.UserId, User.HostName, User.DBName).GetUtilitiesById(0, UtilitiesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Utilities)]
        public JsonNetResult DeleteUtilitiesById(int UtilitiesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (UtilitiesId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Infrastructure.Utilities(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, UtilitiesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllUtilities()
        {
            AcademicLib.BE.Infrastructure.UtilitiesCollections dataColl = new AcademicLib.BE.Infrastructure.UtilitiesCollections();
            try
            {
                dataColl = new AcademicLib.BL.Infrastructure.Utilities(User.UserId, User.HostName, User.DBName).GetAllUtilities(0);
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

        #region "Facilities"
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.View, (int)FormsEntity.Facilities)]

        [HttpPost]
        ////[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Save, (int)FormsEntity.Facilities)]
        public JsonNetResult SaveUpdateFacilities()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Infrastructure.Facilities>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.FacilitiesId.HasValue)
                        beData.FacilitiesId = 0;

                    resVal = new AcademicLib.BL.Infrastructure.Facilities(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[PermissionsAttribute(PivotalERP.BE.Global.Actions.Modify, (int)FormsEntity.Facilities)]
        public JsonNetResult getFacilitiesById(int FacilitiesId)
        {
            AcademicLib.BE.Infrastructure.Facilities resVal = new AcademicLib.BE.Infrastructure.Facilities();
            try
            {
                resVal = new AcademicLib.BL.Infrastructure.Facilities(User.UserId, User.HostName, User.DBName).GetFacilitiesById(0, FacilitiesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        //[PermissionsAttribute(AcademicLib.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.Facilities)]
        public JsonNetResult DeleteFacilitiesById(int FacilitiesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (FacilitiesId < 0)
                {
                    resVal.ResponseMSG = "Can't delete default Income Source";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new AcademicLib.BL.Infrastructure.Facilities(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, FacilitiesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllFacilities()
        {
            AcademicLib.BE.Infrastructure.FacilitiesCollections dataColl = new AcademicLib.BE.Infrastructure.FacilitiesCollections();
            try
            {
                dataColl = new AcademicLib.BL.Infrastructure.Facilities(User.UserId, User.HostName, User.DBName).GetAllFacilities(0);
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