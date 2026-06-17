using Dynamic.BusinessEntity.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PivotalERP.Areas.Setups.Controllers
{
    public class SecurityController : PivotalERP.Controllers.BaseController
    {
        // GET: Setups/Security


        [HttpPost]
        public JsonNetResult GetUserListForSecurity()
        {

            Dynamic.BusinessEntity.Security.UserCollections dataColl = new Dynamic.BusinessEntity.Security.UserCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Security.UserDB(User.HostName, User.DBName).getAllUserShortDetailForSecurity(User.UserId);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "UserWise CostClass"

        [PermissionsAttribute(Actions.View, (int)FormsEntity.AllowUserWiseCostClass, false)]
        public ActionResult AllowCostClass()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResultWithEnum GetAllowCostClass(int? forUserId, int? forGroupId)
        {
            var dataColl = new Dynamic.DataAccess.Security.UserDB(User.HostName, User.DBName).getCostClassForAllow_Web(User.UserId, forUserId, forGroupId);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };

        }


        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)FormsEntity.AllowUserWiseCostClass, false)]
        public JsonNetResult SaveAllowCostClass()
        {
            Dynamic.DataAccess.Security.UserGroupDB groupDB = new Dynamic.DataAccess.Security.UserGroupDB(User.HostName, User.DBName);
            Dynamic.DataAccess.Security.UserDB userDB = new Dynamic.DataAccess.Security.UserDB(User.HostName, User.DBName);

            ResponeValues resVal = new ResponeValues();
            int forId = 0;
            int forType = 0;
            try
            {
                forType = Convert.ToInt32(Request["forType"]);
                forId = Convert.ToInt32(Request["forId"]);

                var allowIdColl = new List<int>();
                var allowIdForEntryColl = new List<int>();
                var beData = DeserializeObject<Dynamic.BusinessEntity.Security.AllowUserWiseCostClassCollections>(Request["jsonData"]);
                foreach (var id in beData)
                {
                    if (id.IsAllow)
                    {
                        allowIdColl.Add(id.CostClassId);
                        if (id.ForEntry)
                            allowIdForEntryColl.Add(id.CostClassId);
                    }

                }

                if (forType == 1)
                {
                    userDB.SaveUserWiseCostClassForEntry(forId, allowIdForEntryColl);
                    resVal = userDB.SaveUserWiseCostClass(forId, allowIdColl);

                }
                else
                {
                    resVal = groupDB.SaveUserGroupWiseCostClass(forId, allowIdColl);
                }


            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
            auditLog.TranId = forId;
            auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.AllowUserWiseCostClass;
            auditLog.Action = Actions.Modify;

            if (forType == 1)
                auditLog.LogText = "Update User Wise CostClass Log  of " + forId.ToString();
            else
                auditLog.LogText = "Update UserGroup Wise CostClass  Log of " + forId.ToString();

            auditLog.AutoManualNo = forId.ToString();
            SaveAuditLog(auditLog);

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        [HttpGet]
        public JsonNetResultWithEnum GetPKTables()
        {
            Dynamic.BusinessEntity.Setup.RefTableCollections DataColl = new Dynamic.DataAccess.Setup.NewEntityDB(this.User.HostName, this.User.DBName).GetAllPkTables(this.User.UserId);
            return new JsonNetResultWithEnum() {  Data = DataColl,  TotalCount = DataColl.Count, IsSuccess = DataColl.IsSuccess, ResponseMSG = DataColl.ResponseMSG };
        }


        public ActionResult DynamicAI()
        {
            return View();
        }
        public ActionResult QueryBuilder()
        {
            return View();
        }
        public ActionResult RunReportViewer()
        {
            return View();
        }
        public ActionResult ViewNewEntity()
        {
            return View();
        }

       
        [HttpPost]
        [AllowAnonymous]
        public JsonNetResult CheckOTP(string Username)
        {
            ResponeValue resVal = new ResponeValue();
            try
            {
                resVal = new AcademicLib.BL.Setup.CheckOTP(1, hostName, dbName).GetcheckOTPById(Username);
                return new JsonNetResult() { Data = resVal, TotalCount = 1, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "Default Setting"
        [HttpPost]
        [ValidateInput(false)]
        public JsonNetResult SavePackageWiseModules()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var dataColl = DeserializeObject<List<AcademicLib.BE.Setup.DefaultSetting>>(Request["jsonData"]);
                if (dataColl != null)
                {
                    resVal = new AcademicLib.BL.Setup.DefaultSetting(User.UserId, User.HostName, User.DBName).SaveDefaultSettingTy(dataColl);
                }
                else{ resVal.ResponseMSG = "Blank Data Can't be Accept";}
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetPackageWiseModules(int ModuleId, int EntityId, int? SubEntityId)
        {
            AcademicLib.BE.Setup.DefaultSettingCollections dataColl = new AcademicLib.BE.Setup.DefaultSettingCollections();
            try
            {
                dataColl = new AcademicLib.BL.Setup.DefaultSetting(User.UserId, User.HostName, User.DBName).GetDefaultSettingById(ModuleId, EntityId, SubEntityId);
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