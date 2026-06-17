using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity;
using Dynamic.BusinessEntity.Account;
using Dynamic.BusinessEntity.Global;
using AcademicERP.BE;
using AcademicERP.BL;
using AcademicERP.DA;

namespace PivotalERP.Areas.Communication.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Communication/Creation
        public ActionResult Circular()
        {
            return View();
        }
        public ActionResult Compose()
        {
            return View();
        }
        public ActionResult Inbox()
        {
            return View();
        }
        public ActionResult Outbox()
        {
            return View();
        }
        public ActionResult SchoolMailbox()
        {
            return View();
        }
        public ActionResult SchoolOutbox()
        {
            return View();
        }
       
        public ActionResult CommunicationType()
        {
            return View();
        }

        //#region "CommunicationType"
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.View, (int)FormsEntity.CommunicationType)]

        //[HttpPost]
        //////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Save, (int)FormsEntity.CommunicationType)]
        //public JsonNetResult SaveCommunicationType()
        //{

        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {
        //        var beData = DeserializeObject<AcademicERP.BE.CommunicationType>(Request["jsonData"]);
        //        if (beData != null)
        //        {
        //            beData.CUserId = User.UserId;

        //            if (!beData.CommunicationTypeId.HasValue)
        //                beData.CommunicationTypeId = 0;

        //            resVal = new AcademicERP.BL.CommunicationType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        //[HttpPost]
        ////[PermissionsAttribute(AcademicERP.BE.Global.Actions.Modify, (int)FormsEntity.CommunicationType)]
        //public JsonNetResult getCommunicationTypeById(int CommunicationTypeId)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {
        //        resVal = new AcademicERP.BL.CommunicationType(User.UserId, User.HostName, User.DBName).GetCommunicationTypeById(0, CommunicationTypeId);
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }
        //    return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}

        //[HttpPost]
        ////[PermissionsAttribute(Dynamic.BusinessEntity.Global.Actions.Delete, (int)FormsEntity.CommunicationType)]
        //public JsonNetResult DeleteCommunicationType(int CommunicationTypeId)
        //{
        //    ResponeValues resVal = new ResponeValues();
        //    try
        //    {
        //        if (CommunicationTypeId < 0)
        //        {
        //            resVal.ResponseMSG = "can't delete defaultProduct Category";
        //            resVal.IsSuccess = false;
        //        }
        //        else
        //            resVal = new AcademicERP.BL.CommunicationType(User.UserId, User.HostName, User.DBName).DeleteById(User.UserId, CommunicationTypeId);
        //    }
        //    catch (Exception ee)
        //    {
        //        resVal.IsSuccess = false;
        //        resVal.ResponseMSG = ee.Message;
        //    }

        //    return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        //}
        //[HttpGet]
        //public JsonNetResult GetAllCommunicationType()
        //{
        //    AcademicERP.BE.CommunicationTypeCollections dataColl = new AcademicERP.BE.CommunicationTypeCollections();
        //    try
        //    {
        //        dataColl = new AcademicERP.BL.CommunicationType(User.UserId, User.HostName, User.DBName).GetAllCommunicationType(0);
        //        return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        //    }
        //    catch (Exception ee)
        //    {
        //        dataColl.IsSuccess = false;
        //        dataColl.ResponseMSG = ee.Message;
        //    }
        //    return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        //}

        //#endregion
        
       
        public ActionResult Scheduled()
        {
            return View();
        }
        public ActionResult TaskCalendar()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }


        #region ContactGroup

        [HttpPost]
        public JsonNetResult SaveContactGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Communication.Creation.ContactGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.GroupId.HasValue)
                        beData.GroupId = 0;

                    resVal = new AcademicLib.BL.Communication.Creation.ContactGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllContactGroup()
        {
            var dataColl = new AcademicLib.BL.Communication.Creation.ContactGroup(User.UserId, User.HostName, User.DBName).GetAllGroup(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetContactGroupById(int GroupId)
        {
            var dataColl = new AcademicLib.BL.Communication.Creation.ContactGroup(User.UserId, User.HostName, User.DBName).GetContactGroupById(0, GroupId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult DelContactGroupById(int GroupId)
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Communication.Creation.ContactGroup(User.UserId, User.HostName, User.DBName)
                    .DelGroupById(0, GroupId);
            }
            catch (Exception ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,
                TotalCount = 0,
                IsSuccess = resVal.IsSuccess,
                ResponseMSG = resVal.ResponseMSG
            };


        }


        #endregion ContactGroup

        #region Contact


        [HttpPost]
        public JsonNetResult SaveContact()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Communication.Creation.ContactDetails>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    if (!beData.ContactId.HasValue)
                        beData.ContactId = 0;

                    resVal = new AcademicLib.BL.Communication.Creation.ContactDetails(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

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
        public JsonNetResult GetAllContact()
        {
            var dataColl = new AcademicLib.BL.Communication.Creation.ContactDetails(User.UserId, User.HostName, User.DBName).GetAllContactDetails(0);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetContactById(int ContactId)
        {
            var dataColl = new AcademicLib.BL.Communication.Creation.ContactDetails(User.UserId, User.HostName, User.DBName).GetContactById(0, ContactId);
            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        public JsonNetResult DelContactById(int ContactId)
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Communication.Creation.ContactDetails(User.UserId, User.HostName, User.DBName)
                    .DelContactDetailsById(0, ContactId);
            }
            catch (Exception ex)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex.Message;
            }
            return new JsonNetResult()
            {
                Data = resVal,
                TotalCount = 0,
                IsSuccess = resVal.IsSuccess,
                ResponseMSG = resVal.ResponseMSG
            };


        }

        #endregion


    }
}