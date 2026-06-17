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
namespace PivotalERP.Areas.Fee.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        // GET: Fee/Creation

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeItem, false)]
        public ActionResult FeeItem()
        {
            return View();
        }
        #region "FeeItem"

        [HttpPost]
        public JsonNetResult SaveFeeItem()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeItem>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.FeeItemId.HasValue)
                        beData.FeeItemId = 0;

                    resVal = new AcademicLib.BL.Fee.Creation.FeeItem(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllFeeItemList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeItem(User.UserId, User.HostName, User.DBName).GetAllFeeItem(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFeeItemById(int FeeItemId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeItem(User.UserId, User.HostName, User.DBName).GetFeeItemById(0, FeeItemId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeItem(int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeItem(User.UserId, User.HostName, User.DBName).DeleteById(0, FeeItemId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFeeItemAutoOrderNo()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeItem(User.UserId, User.HostName, User.DBName).GetFeeItemAutoOrderNo(0);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion
        #region "FeeItemGroup"

        [HttpPost]
        public JsonNetResult SaveFeeItemGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeItemGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.FeeItemGroupId.HasValue)
                        beData.FeeItemGroupId = 0;

                    resVal = new AcademicLib.BL.Fee.Creation.FeeItemGroup(User.UserId, User.HostName, User.DBName).SaveFormData(beData);
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
        public JsonNetResult GetAllFeeItemGroupList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeItemGroup(User.UserId, User.HostName, User.DBName).GetAllFeeItemGroup(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetFeeItemGroupById(int FeeItemGroupId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeItemGroup(User.UserId, User.HostName, User.DBName).GetFeeItemGroupById(0, FeeItemGroupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeItemGroup(int FeeItemGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeItemGroup(User.UserId, User.HostName, User.DBName).DeleteById(0, FeeItemGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion
        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeMapping, false)]
        public ActionResult FeeMapping()
        {
            return View();
        }

        #region "FeeMappingBatchWise"

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FeeMapping, false)]
        public JsonNetResult SaveFeeMappingBatchWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeMappingClassWiseCollections>(Request["jsonData"]);
                if (beData != null || beData.Count==0)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeMappingBatchWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "BatchWise Feemapping";
                        auditLog.AutoManualNo ="" ;
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetFeeMappingBatchWise(int BatchId,int FacultyId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingBatchWise(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0,BatchId,FacultyId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeMappingBatchWise(int BatchId,int FacultyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeMappingBatchWise(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0,BatchId,FacultyId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "BatchWise Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FeeMappingClassWise"

        [HttpPost]
        public JsonNetResult SaveFeeMappingClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeMappingClassWiseCollections>(Request["jsonData"]);
                if (beData != null)
                {                   
                    resVal = new AcademicLib.BL.Fee.Creation.FeeMappingClassWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Class Wise Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFeeMappingClassWise()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingClassWise(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
      
        [HttpPost]
        public JsonNetResult DelFeeMappingClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeMappingClassWise(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Class Wise Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FeeMappingMediumWise"

        [HttpPost]
        public JsonNetResult SaveFeeMappingMediumWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeMappingMediumWiseCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeMappingMediumWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Medium Wise Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFeeMappingMediumWise(int MediumId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingMediumWise(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0,MediumId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeMappingMediumWise(int MediumId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeMappingMediumWise(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0,MediumId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Medium Wise Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FeeMappingForWise"

        [HttpPost]
        public JsonNetResult SaveFeeMappingForWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeMappingMediumWiseCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeMappingForWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "For Wise Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFeeMappingForWise(int MediumId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingForWise(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, MediumId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeMappingForWise(int MediumId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeMappingForWise(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, MediumId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "For Wise Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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
        public JsonNetResult GetFeeItemFor(int ForId,int? ClassId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingForWise(User.UserId, User.HostName, User.DBName).getFeeItemFor(this.AcademicYearId,ClassId,ForId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS};
        }

        #endregion

        #region "ExtraFeeItemMappingSetup"

        [HttpPost]
        public JsonNetResult SaveExtraFeeItemMappingSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.ExtraFeeItemMappingCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.ExtraFeeItemMapping(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Extra Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllExtraFeeItemMappingSetup(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ExtraFeeItemMapping(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(0, this.AcademicYearId, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelExtraFeeItemMappingSetup(int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ExtraFeeItemMapping(User.UserId, User.HostName, User.DBName).Delete(0, this.AcademicYearId, ClassId, SectionId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Extra Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "MessFeeItemMappingSetup"

        [HttpPost]
        public JsonNetResult SaveMessFeeItemMappingSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.MessFeeItemMappingCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.MessFeeItemMapping(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Mess Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllMessFeeItemMappingSetup(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.MessFeeItemMapping(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(0,this.AcademicYearId, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelMessFeeItemMappingSetup(int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.MessFeeItemMapping(User.UserId, User.HostName, User.DBName).Delete(0, this.AcademicYearId, ClassId, SectionId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Mess Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FeeMappingStudentTypeWise"

        [HttpPost]
        public JsonNetResult SaveFeeMappingStudentTypeWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeMappingStudentTypeWiseCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeMappingStudentTypeWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "StudentType Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFeeMappingStudentTypeWise(int StudentTypeId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeMappingStudentTypeWise(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, StudentTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeMappingStudentTypeWise(int StudentTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeMappingStudentTypeWise(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, StudentTypeId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "StudentType Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "NotApplicableFeeItem"

        [HttpPost]
        public JsonNetResult SaveNotApplicableFeeItem()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.NotApplicableFeeItemCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.NotApplicableFeeItem(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "NotApplicable Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllNotApplicableFeeItem(int ClassId,int? SectionId,int? SemesterId,int? ClassYearId,int FeeItemId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.NotApplicableFeeItem(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(0, this.AcademicYearId, ClassId,SectionId,SemesterId,ClassYearId,FeeItemId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelNotApplicableFeeItem(int ClassId, int? SectionId,int? SemesterId,int? ClassYearId, int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.NotApplicableFeeItem(User.UserId, User.HostName, User.DBName).Delete(0, this.AcademicYearId, ClassId,SectionId,SemesterId,ClassYearId,FeeItemId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "NotApplicable Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FixedAmountStudent"

        [HttpPost]
        public JsonNetResult SaveFixedAmountStudent()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FixedAmountStudentCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FixedAmountStudent(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);
                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "FixedAmount  Feemapping";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFixedAmountStudent(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FixedAmountStudent(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFixedAmountStudent(int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FixedAmountStudent(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, ClassId, SectionId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "FixedAmount  Feemapping";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.DiscountSetup, false)]
        public ActionResult DiscountSetup()
        {
            return View();
        }
        #region "DiscountType"

        [HttpPost]
        public JsonNetResult SaveDiscountType()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.DiscountType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.DiscountTypeId.HasValue)
                        beData.DiscountTypeId = 0;

                    resVal = new AcademicLib.BL.Fee.Creation.DiscountType(User.UserId, User.HostName, User.DBName).SaveFormData(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "DiscountType";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllDiscountTypeList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.DiscountType(User.UserId, User.HostName, User.DBName).GetAllDiscountType(0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetDiscountTypeById(int DiscountTypeId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.DiscountType(User.UserId, User.HostName, User.DBName).GetDiscountTypeById(0, DiscountTypeId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelDiscountType(int DiscountTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.DiscountType(User.UserId, User.HostName, User.DBName).DeleteById(0, DiscountTypeId);
                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "DiscountType";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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
        public JsonNetResult GetDiscountStudentList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.DiscountType(User.UserId, User.HostName, User.DBName).getDiscountStudentList(this.AcademicYearId,"","");

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult PrintDiscountStudentList()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Fee.DiscountStudent> paraData = DeserializeObject<List<AcademicLib.RE.Fee.DiscountStudent>>(jsonData);
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
        #endregion
        #region "ClassWiseDiscountSetup"

        [HttpPost]
        public JsonNetResult SaveClassWiseDiscountSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.ClassWiseDiscountSetupCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.ClassWiseDiscountSetup(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "ClassWise DiscountSetup";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllClassWiseDiscountSetup(int ClassId, int? SectionId, int? SemesterId, int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ClassWiseDiscountSetup(User.UserId, User.HostName, User.DBName).getClassWiseDiscountSetup(this.AcademicYearId, 0, ClassId, SectionId, SemesterId,  ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelClassWiseDiscountSetup(int ClassId, int? SectionId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ClassWiseDiscountSetup(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, ClassId, SectionId,  SemesterId,  ClassYearId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "ClassWise DiscountSetup";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FullDiscountSetup"

        [HttpPost]
        public JsonNetResult SaveFullDiscountSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.ClassWiseDiscountSetupCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FullDiscountSetup(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Full DiscountSetup";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFullDiscountSetup(int ClassId, int? SectionId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FullDiscountSetup(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, ClassId, SectionId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFullDiscountSetup(int ClassId, int? SectionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FullDiscountSetup(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, ClassId, SectionId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Full DiscountSetup";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FeeItemWiseDiscountSetup"

        [HttpPost]
        public JsonNetResult SaveFeeItemWiseDiscountSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeItemWiseDiscountSetupCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeItemWiseDiscountSetup(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "FeeItemWise DiscountSetup";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetAllFeeItemWiseDiscountSetup(int ClassId, int? SectionId, int? SemesterId, int? ClassYearId, int FeeItemId,int? BatchId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeItemWiseDiscountSetup(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, ClassId, SectionId,   SemesterId,   ClassYearId, FeeItemId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeItemWiseDiscountSetup(int ClassId, int? SectionId, int? SemesterId, int? ClassYearId, int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeItemWiseDiscountSetup(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, ClassId, SectionId, SemesterId, ClassYearId, FeeItemId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "FeeItemWise DiscountSetup";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "StudentWiseDiscountSetup"

        [HttpPost]
        public JsonNetResult SaveStudentWiseDiscountSetup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeItemWiseDiscountSetupCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.StudentWiseDiscountSetup(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "StudentWise DiscountSetup";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetStudentWiseDiscountSetup(int StudentId,int? SemesterId,int? ClassYearId,int? BatchId=null)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.StudentWiseDiscountSetup(User.UserId, User.HostName, User.DBName).GetStudentWiseDiscountSetup(this.AcademicYearId, 0, StudentId,SemesterId,ClassYearId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelStudentWiseDiscountSetup(int StudentId,int? SemesterId,int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.StudentWiseDiscountSetup(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, StudentId,SemesterId,ClassYearId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "StudentWise DiscountSetup";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FineSetup, false)]
        public ActionResult FineSetup()
        {
            return View();
        }

        #region FineSetup

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.FineSetup, false)]
        public JsonNetResult SaveFineSetup()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Fee.Setup.Fine>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    
                    resVal = new AcademicLib.BL.Fee.Setup.FineSetup(User.UserId, User.HostName, User.DBName).SaveFormData(beData,this.AcademicYearId);


                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "FineSetup";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }
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
        public JsonNetResult GetFineSetup()
        {
            var dataColl = new AcademicLib.BL.Fee.Setup.FineSetup(User.UserId, User.HostName, User.DBName).GetFineSetup(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.StudentOpening, false)]
        public ActionResult StudentOpening()
        {
            return View();
        }
        #region "StudentOpening"

        [HttpPost]
        public JsonNetResult SaveStudentOpening()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.StudentOpeningCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.StudentOpening(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId,beData);

                    if (resVal.IsSuccess && beData.Count>0)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = FormsEntity.StudentProfile;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Student Opening of Class Id("+beData.First().ClassId.ToString()+")";
                        auditLog.AutoManualNo = beData.First().ClassId.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResult SaveStudentFeeWiseOpening()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.StudentOpeningCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.StudentOpening(User.UserId, User.HostName, User.DBName).SaveFeeItemWise(this.AcademicYearId,beData);
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
        public JsonNetResult GetClassWiseOpening()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.StudentOpening(User.UserId, User.HostName, User.DBName).getClassWiseOpening(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetStudentWiseOpening(int ClassId,int? SectionId, int? SemesterId, int? ClassYearId,int? BatchId=null)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.StudentOpening(User.UserId, User.HostName, User.DBName).getClassWiseOpening(this.AcademicYearId,ClassId, SectionId,SemesterId,ClassYearId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetAllStudentOpening(int ClassId, int? SectionId,int? FeeItemId,int? SemesterId,int? ClassYearId,int? BatchId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.StudentOpening(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(0,this.AcademicYearId, ClassId, SectionId,FeeItemId,SemesterId,ClassYearId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelStudentOpening(int ClassId, int? SectionId, int? SemesterId, int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.StudentOpening(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId,0, ClassId, SectionId,SemesterId,ClassYearId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.NewCompany;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Student Opening";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.StudentGroup, false)]
        public ActionResult StudentGroup()
        {
            return View();
        }
        //Billing

        [PermissionsAttribute(Actions.View, (int)ENTITIES.BillGenerate, false)]
        public ActionResult BillGenerate()
        {
            return View();
        }
        #region "Bill Generate Class Wise"

        [HttpPost]
        public JsonNetResult GetBillMissingStudent(int ClassId,int ForMonth,int? SemesterId,int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).getBillMissingStudent(this.AcademicYearId, ClassId, ForMonth,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult BillGenerateMissingStudent(int ForMonth,string StudentIdColl)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).billGenerateMissingStudent(this.AcademicYearId, ForMonth,StudentIdColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetPendingBillGenerateFeeList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).getPendingBillGenerateList(this.AcademicYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult FineGenerateClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.BillGenerate>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var billBL = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName);
                    resVal = billBL.GenerateFine(this.AcademicYearId,beData.MonthId,false);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Class Wise Fine Generate " + beData.MonthId.ToString() + " TO " + beData.MonthId.ToString();
                        auditLog.AutoManualNo = beData.ClassId.ToString();
                        SaveAuditLog(auditLog);
                         
                    }
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
        public JsonNetResult BillGenerateClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            { 
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.BillGenerate>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    var billBL = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName);
                    resVal = billBL.SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Class Wise Bill Generate " + beData.MonthId.ToString() + " TO " + beData.MonthId.ToString();
                        auditLog.AutoManualNo = beData.ClassId.ToString();
                        SaveAuditLog(auditLog);

                        int fromMonthId = beData.FromMonthId.HasValue ? beData.FromMonthId.Value : beData.MonthId;
                        int toMonthId = beData.ToMonthId.HasValue ? beData.ToMonthId.Value : beData.MonthId;
                        var dataColl = billBL.getBillForSMS(this.AcademicYearId, beData.ClassId, null, fromMonthId, toMonthId,beData.BatchId,beData.FacultyId,beData.SemesterId,beData.ClassYearId);
                        PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName);
                        if (dataColl != null && dataColl.Count>0)
                        {
                            var templatesColl = new AcademicLib.BL.Setup.SENT(User.UserId, User.HostName, User.DBName).GetSENT((int)ENTITIES.BillGenerate, 3, 3);
                            if (templatesColl != null && templatesColl.Count > 0)
                            {
                                #region "Send Notification"

                                AcademicLib.BE.Setup.SENT templateNotifiation = templatesColl[0];
                                if (templateNotifiation != null)
                                {
                                    List<Dynamic.BusinessEntity.Global.NotificationLog> notificationLogColl = new List<NotificationLog>();

                                    System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.BE.Fee.Creation.BillGenerate_SENT), templateNotifiation.Description);
                                    foreach(var newBeData in dataColl)
                                    {
                                        string tempMSG = templateNotifiation.Description;
                                        foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                        {
                                            tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", globlFun.GetProperty(newBeData, field.Name).ToString());
                                        }

                                        Dynamic.BusinessEntity.Global.NotificationLog notification = new Dynamic.BusinessEntity.Global.NotificationLog();
                                        notification.Content = tempMSG;
                                        notification.ContentPath = "";
                                        notification.EntityId = Convert.ToInt32(AcademicLib.BE.Global.NOTIFICATION_ENTITY.BILL_GENERATE);
                                        notification.EntityName = AcademicLib.BE.Global.NOTIFICATION_ENTITY.BILL_GENERATE.ToString();
                                        notification.Heading = templateNotifiation.Title;
                                        notification.Subject = templateNotifiation.Title;
                                        notification.UserId = User.UserId;
                                        notification.UserName = User.Identity.Name;
                                        notification.UserIdColl = newBeData.UserId;
                                        notificationLogColl.Add(notification);
                                    }
                                   
                                    new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName).SendNotification(User.UserId, notificationLogColl, true);
                                }


                                #endregion
                            }
 
                        }
                    }
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
        public JsonNetResult BillSendEmail()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.BillGenerate>(Request["jsonData"]);
                if (beData != null)
                {
                    var usr = User;
                    var billBL = new AcademicLib.BL.Fee.Creation.BillGenerate(usr.UserId, usr.HostName, usr.DBName);
                    beData.CUserId = User.UserId;
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                    auditLog.Action = Actions.Print;
                    auditLog.LogText = "Class Wise Bill Email " + beData.MonthId.ToString() + " TO " + beData.MonthId.ToString();
                    auditLog.AutoManualNo = beData.ClassId.ToString();
                    SaveAuditLog(auditLog);

                    int fromMonthId = beData.FromMonthId.HasValue ? beData.FromMonthId.Value : beData.MonthId;
                    int toMonthId = beData.ToMonthId.HasValue ? beData.ToMonthId.Value : beData.MonthId;
                    var dataColl = billBL.getBillForSMS(this.AcademicYearId, beData.ClassId, null, fromMonthId, toMonthId, beData.BatchId, beData.FacultyId, beData.SemesterId, beData.ClassYearId);
                    PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(User.UserId, User.HostName, User.DBName);
                    if (dataColl != null && dataColl.Count > 0)
                    {
                        var templatesColl = new AcademicLib.BL.Setup.SENT(User.UserId, User.HostName, User.DBName).GetSENT((int)ENTITIES.BillGenerate, 3, 2);
                        if (templatesColl != null && templatesColl.Count > 0)
                        {
                            #region "Send Email"

                            var uid = User.UserId;
                            var CU = User;
                            var srvPath = System.Web.HttpContext.Current.Server.MapPath("~");
                            //System.Threading.Thread thread = new System.Threading.Thread(() =>
                            {
                                try
                                {
                                    AcademicLib.BE.Setup.SENT templateEmail = templatesColl[0];
                                    if (templateEmail != null)
                                    {
                                        var comDet = new Dynamic.DataAccess.Global.GlobalDB(CU.HostName, CU.DBName).getCompanyBranchDetailsForPrint(uid, (int)(RptFormsEntity.BillPrint), 0, 0);
                                        PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(CU.HostName, CU.DBName, uid, (int)(RptFormsEntity.BillPrint), 0, false);

                                        if (reportTemplate.TemplateAttachments != null || reportTemplate.TemplateAttachments.Count > 0)
                                        {
                                            Dynamic.BusinessEntity.Global.ReportTempletes template = null;
                                            foreach (var rT in reportTemplate.TemplateAttachments)
                                            {
                                                if (rT.ForEmail == true)
                                                {
                                                    template = reportTemplate.GetTemplate(rT);
                                                    break;
                                                }
                                            }

                                            if (template == null)
                                                template = reportTemplate.DefaultTemplate;

                                            if (!string.IsNullOrEmpty(template.Path))
                                            {
                                                System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                                                paraColl.Add("UserId", uid.ToString());
                                                paraColl.Add("UserName", CU.UserName);
                                                paraColl.Add("FromMonthId", beData.FromMonthId.ToString());
                                                paraColl.Add("ToMonthId", beData.ToMonthId.ToString());
                                                paraColl.Add("ClassId", beData.ClassId.ToString());                                                
                                                paraColl.Add("SectionId","0" );

                                                if(beData.BatchId.HasValue)
                                                    paraColl.Add("BatchId", beData.BatchId.ToString());
                                                else
                                                    paraColl.Add("BatchId", "0");

                                                
                                                paraColl.Add("AcademicYearId", "0");

                                                if(beData.FacultyId.HasValue)
                                                    paraColl.Add("FacultyId", beData.FacultyId.ToString());
                                                else
                                                    paraColl.Add("FacultyId", "0");

                                                if(beData.SemesterId.HasValue)  
                                                    paraColl.Add("SemesterId", beData.SemesterId.ToString());
                                                else
                                                    paraColl.Add("SemesterId", "0");


                                                if(beData.ClassYearId.HasValue)
                                                    paraColl.Add("ClassYearId", beData.ClassYearId.ToString());
                                                else
                                                    paraColl.Add("ClassYearId", "0");

                                                System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.BE.Fee.Creation.BillGenerate_SENT), templateEmail.Description);
                                                foreach (var newBeData in dataColl)
                                                {
                                                    if (string.IsNullOrEmpty(newBeData.EmailId))
                                                        continue;

                                                    string tempMSG = templateEmail.Description;
                                                    string subject = templateEmail.Title;
                                                    foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                                    {
                                                        var pVal = globlFun.GetProperty(newBeData, field.Name).ToString();
                                                        tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", pVal);
                                                        subject = subject.Replace("$$" + field.Name.Trim().ToLower() + "$$", pVal);
                                                    }

                                                    if (paraColl.AllKeys.Contains("StudentId"))
                                                        paraColl.Remove("StudentId");

                                                    paraColl.Add("StudentId", newBeData.StudentId.ToString());

                                                    Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                                                    _rdlReport.ComDet = comDet;
                                                    _rdlReport.ConnectionString = ConnectionString;
                                                    _rdlReport.RenderType = "pdf";
                                                    _rdlReport.NoShow = false;
                                                    _rdlReport.ReportFile = reportTemplate.GetPath(template, srvPath);
                                                    if (_rdlReport.Object != null)
                                                    {
                                                        string basePath = @"print-tran-log\salesbill-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "-" + newBeData.StudentId.ToString() + ".pdf";
                                                        string sFile = srvPath + @"\" + basePath;
                                                        reportTemplate.SavePDF(_rdlReport.Object, sFile);

                                                        Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                                                        {
                                                            To = newBeData.EmailId,
                                                            Cc = IsNullStr(templateEmail.EmailCC),
                                                            BCC = IsNullStr(templateEmail.EmailBCC),
                                                            Subject = subject,
                                                            Message = tempMSG,
                                                            CUserId = uid
                                                        };
                                                        globlFun.SendEMail(mail, null, sFile);
                                                    }
                                                }
                                            }

                                        }

                                    }
                                }
                                catch (Exception exMail)
                                {

                                }
                            }//);

                           // thread.IsBackground = true;
                           // thread.Start();


                            #endregion


                            resVal.IsSuccess = true;
                            resVal.ResponseMSG = "EMail Send";
                        }
                        else
                        {
                            resVal.IsSuccess = false;
                            resVal.ResponseMSG = "Template not found";
                        }
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "data not found for email send";
                    }
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
        public JsonNetResult GetClassWiseBillGenerateList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).getClassWiseBillGenerateList(this.AcademicYearId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetClassWiseBillGenerateFeeList(int MonthId,int ClassId,int? SemesterId,int? ClassYearId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).getClassWiseFeeItem(this.AcademicYearId, MonthId,ClassId,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetStudentBGD(int MonthId, int ClassId, int? SemesterId, int? ClassYearId,int? BatchId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).getStudentFeeDetails(this.AcademicYearId, ClassId, MonthId,SemesterId,ClassYearId,BatchId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelBillGenerateClassWise(int MonthId,int ClassId,int? SemesterId,int? ClassYearId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, MonthId, ClassId,SemesterId,ClassYearId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Delete Class Wise Bill Generate " + MonthId.ToString();
                    auditLog.AutoManualNo = ClassId.ToString();
                    SaveAuditLog(auditLog);
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
        public JsonNetResult DelBillGenerateStudentWise(int StudentId,int FromMonthId,int ToMonthId,int? SemesterId,int? ClassYearId,int? BatchId=null)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).DeleteStudentWise(this.AcademicYearId, StudentId, FromMonthId, ToMonthId,SemesterId,ClassYearId,BatchId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Delete Student Wise Bill Generate " + FromMonthId.ToString()+" TO "+ToMonthId.ToString();
                    auditLog.AutoManualNo = StudentId.ToString();
                    SaveAuditLog(auditLog);
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

        #region "Bill Generate StudentWise"

        [HttpPost]
        public JsonNetResult BillGenerateStudentWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.BillGenerate>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;
                    resVal = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).SaveStudentWise(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.SalesInvoice;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Student Wise Bill Generate " + beData.FromMonthId.Value.ToString() + " TO " + beData.ToMonthId.Value.ToString();
                        auditLog.AutoManualNo = beData.StudentId.Value.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResult GetStudentWiseBillGenerateFeeList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).getStudentWiseBillGenerateList(this.AcademicYearId, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ManualBilling, false)]
        public ActionResult ManualBilling()
        {
            return View();
        }
        #region "ManualBilling"

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ManualBilling, false)]
        public JsonNetResult SaveManualBilling()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.ManualBilling>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    
                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    else if (beData.TranId > 0)
                        isModify = true;

                    resVal = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual Billing Modify"+beData.TotalAmount.ToString("N") : "New Manual Billing" + beData.TotalAmount.ToString("N"));
                        auditLog.AutoManualNo = beData.AutoNumber.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        [HttpGet]
        public JsonNetResult GetBillingTypes()
        {
            Dynamic.APIEnitity.CommonCollections dataColl = new Dynamic.APIEnitity.CommonCollections();
            try
            {
                int id = 1;
                foreach (string str in Enum.GetNames(typeof(AcademicLib.BE.Fee.Creation.BILLINGTYPES)))
                {
                    Dynamic.APIEnitity.Common beData = new Dynamic.APIEnitity.Common();
                    beData.Id = id;
                    beData.Text = str;
                    dataColl.Add(beData);
                    id++;
                }
                dataColl.IsSuccess = true;
                dataColl.ResponseMSG = GLOBALMSG.SUCCESS;

                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };

            }
            catch (Exception ee)
            {
                return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = false, ResponseMSG = ee.Message };
            }
        }

        [HttpPost]
        public JsonNetResultWithEnum GetManualBillingDetails(DateTime? dateFrom,DateTime? dateTo,bool? IsCancel)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).getManualBillingDetails(this.AcademicYearId, dateFrom, dateTo, IsCancel);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResultWithEnum GetBillingDetails(DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).getBillingDetails( dateFrom, dateTo);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResultWithEnum GetAllManualBillingList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).GetAllManualBilling(this.AcademicYearId, 0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetManualBillingById(int ManualBillingId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).GetManualBillingById(0, ManualBillingId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetManualBillingDetById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).getManualBillingDetailsById(TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ManualBilling, false)]
        public JsonNetResult DelManualBilling(int ManualBillingId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).DeleteById(0, ManualBillingId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = ManualBillingId;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Delete Manual Billing ";
                    auditLog.AutoManualNo = ManualBillingId.ToString();
                    SaveAuditLog(auditLog);
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
        public JsonNetResult CancelMB(AcademicLib.BE.Fee.Creation.ManualBilling beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).Cancel(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = beData.TranId.Value;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                        auditLog.Action = Actions.Modify;
                        auditLog.LogText = "Cancel Manual Billing ";
                        auditLog.AutoManualNo = beData.TranId.Value.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResult GetAutoNoOfManualBilling(int? CostClassId=null)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).getAutoNo(this.AcademicYearId,CostClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetFeeRate(int StudentId,int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).getFeeRate(this.AcademicYearId, StudentId, FeeItemId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        [PermissionsAttribute(Actions.View, (int)ENTITIES.FeeDebit, false)]
        public ActionResult FeeDebit()
        {
            return View();
        }
        #region "FeeDebit"

        [HttpPost]
        public JsonNetResult SaveFeeDebit()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeDebitCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = 0;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ENQEnquiryFeedBack;
                        auditLog.Action = Actions.Save;
                        auditLog.LogText = "Fee Debit";
                        auditLog.AutoManualNo = "";
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResult GetAllFeeDebit(int ClassId, int? SectionId, int MonthId, int? BatchId=null, int? SemesterId = null, int? ClassYearId = null)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).GetAllFeeMapping(this.AcademicYearId, 0, ClassId, SectionId, MonthId,BatchId,SemesterId,ClassYearId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeDebit(int ClassId, int? SectionId,int MonthId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).Delete(this.AcademicYearId, 0, ClassId, SectionId, MonthId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = 0;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ENQEnquiryFeedBack;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Delete Fee Debit";
                    auditLog.AutoManualNo = "";
                    SaveAuditLog(auditLog);
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

        #region "FeeDebit StudentWIse"

        [HttpPost]
        public JsonNetResult SaveFeeDebitStudentWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeDebitCollections>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).SaveStudentWise(this.AcademicYearId, beData);
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
        public JsonNetResult GetAllFeeDebitStudentWise(int StudentId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).GetAllStudentWise(this.AcademicYearId, 0, StudentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult DelFeeDebitStudentWise(int StudentId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).DeleteStudentWise(this.AcademicYearId, 0, StudentId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        #endregion

        #region "FeeDebit FeeItemWise"

        [HttpPost]
        public JsonNetResult SaveFeeDebitFeeItemWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                
                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.FeeDebitFeeItemWise>(Request["jsonData"]);
                if (beData != null)
                {
                    resVal = new AcademicLib.BL.Fee.Creation.FeeDebit(User.UserId, User.HostName, User.DBName).SaveFeeDebitFeeItemWise(this.AcademicYearId, beData);
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

        [PermissionsAttribute(Actions.View, (int)ENTITIES.BillPrint, false)]
        public ActionResult BillPrint()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetReminderSlip(int UptoMonthId, int forStudent,int? classId,int? sectionId,int? BatchId,int? ClassYearId,int? SemesterId)
        {
            var dataColl = new AcademicLib.BL.Fee.Transaction.FeeReceipt(User.UserId, User.HostName, User.DBName).getReminderSlip(this.AcademicYearId, UptoMonthId, forStudent,classId,sectionId,BatchId,ClassYearId,SemesterId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost,ValidateInput(false)]
        public JsonNetResult PrintReminderSlip()
        {
            var jsonData = Request["jsonData"];
            List<AcademicLib.RE.Fee.ReminderSlip> paraData = DeserializeObject<List<AcademicLib.RE.Fee.ReminderSlip>>(jsonData);
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

        [HttpPost, ValidateInput(false)]
        public JsonNetResult ReminderSlipSendEmail()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var jsonData = Request["jsonData"];
                List<AcademicLib.RE.Fee.ReminderSlip> dataColl = DeserializeObject<List<AcademicLib.RE.Fee.ReminderSlip>>(jsonData);                 
                if (dataColl != null && dataColl.Count>0)
                {
                    var usr = User;
                    PivotalERP.Global.GlobalFunction globlFun = new PivotalERP.Global.GlobalFunction(usr.UserId, usr.HostName, usr.DBName);
                    var templatesColl = new AcademicLib.BL.Setup.SENT(usr.UserId, usr.HostName, usr.DBName).GetSENT((int)ENTITIES.ReminderSlip, 3, 2);
                    if (templatesColl != null && templatesColl.Count > 0)
                    {
                        #region "Send Email"

                        var uid = usr.UserId;
                        var srvPath = System.Web.HttpContext.Current.Server.MapPath("~");
                        {
                            try
                            {
                                AcademicLib.BE.Setup.SENT templateEmail = templatesColl[0];
                                if (templateEmail != null)
                                {
                                    var comDet = new Dynamic.DataAccess.Global.GlobalDB(usr.HostName, usr.DBName).getCompanyBranchDetailsForPrint(uid, (int)(RptFormsEntity.FeeReminderSlip), 0, 0);
                                    PivotalERP.Global.ReportTemplate reportTemplate = new PivotalERP.Global.ReportTemplate(usr.HostName, usr.DBName, uid, (int)(RptFormsEntity.FeeReminderSlip), 0, false);

                                    if (reportTemplate.TemplateAttachments != null || reportTemplate.TemplateAttachments.Count > 0)
                                    {
                                        Dynamic.BusinessEntity.Global.ReportTempletes template = null;
                                        foreach (var rT in reportTemplate.TemplateAttachments)
                                        {
                                            if (rT.ForEmail == true)
                                            {
                                                template = reportTemplate.GetTemplate(rT);
                                                break;
                                            }
                                        }

                                        if (template == null)
                                            template = reportTemplate.DefaultTemplate;

                                        if (!string.IsNullOrEmpty(template.Path))
                                        {
                                            System.Collections.Specialized.NameValueCollection paraColl = GetObjectAsKeyVal(comDet);
                                            paraColl.Add("UserId", uid.ToString());
                                            paraColl.Add("UserName", usr.UserName);
                                            System.Collections.Generic.List<System.Reflection.PropertyInfo> tmpFieldsColl = globlFun.GetPropertyInfos(typeof(AcademicLib.RE.Fee.ReminderSlip), templateEmail.Description);
                                            foreach (var newBeData in dataColl)
                                            {
                                                if (string.IsNullOrEmpty(newBeData.Email))
                                                    continue;

                                                string tempMSG = templateEmail.Description;
                                                string subject = templateEmail.Title;
                                                foreach (System.Reflection.PropertyInfo field in tmpFieldsColl)
                                                {
                                                    var pVal = globlFun.GetProperty(newBeData, field.Name).ToString();
                                                    tempMSG = tempMSG.Replace("$$" + field.Name.Trim().ToLower() + "$$", pVal);
                                                    subject = subject.Replace("$$" + field.Name.Trim().ToLower() + "$$", pVal);
                                                }

                                                if (paraColl.AllKeys.Contains("StudentId"))
                                                    paraColl.Remove("StudentId");

                                                paraColl.Add("StudentId", newBeData.StudentId.ToString());

                                                List<AcademicLib.RE.Fee.ReminderSlip> tmpDataColl = new List<AcademicLib.RE.Fee.ReminderSlip>();
                                                tmpDataColl.Add(newBeData);
                                                Dynamic.Accounting.IReportLoadObjectData reportData = new AcademicLib.PE.Fee.ReminderSlip(tmpDataColl);
                                                Dynamic.ReportEngine.RdlAsp.RdlReport _rdlReport = new Dynamic.ReportEngine.RdlAsp.RdlReport(paraColl);
                                                _rdlReport.iReportLoadObjectData = reportData;
                                                _rdlReport.RenderType = "pdf";
                                                _rdlReport.NoShow = false;
                                                _rdlReport.ReportFile = reportTemplate.GetPath(template, srvPath);
                                                if (_rdlReport.Object != null)
                                                {
                                                    string basePath = @"print-tran-log\fee-reminder-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "-" + newBeData.StudentId.ToString() + ".pdf";
                                                    string sFile = srvPath + @"\" + basePath;
                                                    reportTemplate.SavePDF(_rdlReport.Object, sFile);

                                                    Dynamic.BusinessEntity.Global.MailDetails mail = new Dynamic.BusinessEntity.Global.MailDetails()
                                                    {
                                                        To = newBeData.Email,
                                                        Cc = IsNullStr(templateEmail.EmailCC),
                                                        BCC = IsNullStr(templateEmail.EmailBCC),
                                                        Subject = subject,
                                                        Message = tempMSG,
                                                        CUserId = uid
                                                    };
                                                    globlFun.SendEMail(mail, null, sFile);
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                            catch (Exception exMail)
                            {

                            }
                        }//);

                        // thread.IsBackground = true;
                        // thread.Start();


                        #endregion


                        resVal.IsSuccess = true;
                        resVal.ResponseMSG = "EMail Send";
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "Template not found";
                    }
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



        #region "ManualBilling"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.ManualBilling, false)]
        public ActionResult ManualBillingClassWise()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ManualBillingClassWise, false)]
        public JsonNetResult SaveManualBillingClassWise()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.ManualBilling>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;


                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    else if (beData.TranId > 0)
                        isModify = true;

                    resVal = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).SaveFormData(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual Billing ClassWise Modify" + beData.TotalAmount.ToString("N") : "New Manual Billing ClassWise" + beData.TotalAmount.ToString("N"));
                        auditLog.AutoManualNo = beData.AutoNumber.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResultWithEnum GetManualBillingClassWiseDetails(DateTime? dateFrom, DateTime? dateTo)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).getManualBillingDetails(this.AcademicYearId, dateFrom, dateTo);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResultWithEnum GetAllManualBillingClassWiseList()
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).GetAllManualBilling(this.AcademicYearId, 0);

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetManualBillingClassWiseById(int ManualBillingId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).GetManualBillingById(0, ManualBillingId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetManualBillingClassWiseDetById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).getManualBillingDetailsById(TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ManualBillingClassWise, false)]
        public JsonNetResult DelManualBillingClassWise(int ManualBillingId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).DeleteById(0, ManualBillingId);

                if (resVal.IsSuccess)
                {
                    Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                    auditLog.TranId = ManualBillingId;
                    auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                    auditLog.Action = Actions.Delete;
                    auditLog.LogText = "Delete Manual Billing Class Wise ";
                    auditLog.AutoManualNo = ManualBillingId.ToString();
                    SaveAuditLog(auditLog);
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
        public JsonNetResult CancelMBClassWise(AcademicLib.BE.Fee.Creation.ManualBilling beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;

                    resVal = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).Cancel(beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = beData.TranId.Value;
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                        auditLog.Action = Actions.Modify;
                        auditLog.LogText = "Cancel Manual Billing ClassWise";
                        auditLog.AutoManualNo = beData.TranId.Value.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResult GetFeeRateMBC(int ClassId, int FeeItemId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).getFeeRate(this.AcademicYearId, ClassId, FeeItemId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        [HttpPost]
        public JsonNetResult GetDataFromRegAutoManualNo(string RegNo, string AutoManualNo)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBilling(User.UserId, User.HostName, User.DBName).GetDataFromRegAutoManualNo(RegNo, AutoManualNo, 0);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        public JsonNetResult GetStudentBillGenerateFeeList(int? FromMonthId, int? ToMonthId, int? StudentId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.BillGenerate(User.UserId, User.HostName, User.DBName).GetStudentWiseFeeItem(this.AcademicYearId, FromMonthId, ToMonthId, StudentId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #region "Manual Biling Class Wise"

        [HttpPost]
        public JsonNetResult GetAutoNoClassWiseManualBilling(int? CostClassId = null)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).getAutoNo(this.AcademicYearId, CostClassId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ManualBillingClassWise, false)]
        public JsonNetResult SaveClassWiseManualBilling()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {

                var beData = DeserializeObject<AcademicLib.BE.Fee.Creation.ManualBilling>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;


                    bool isModify = false;
                    if (!beData.TranId.HasValue)
                        beData.TranId = 0;
                    else if (beData.TranId > 0)
                        isModify = true;

                    resVal = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).SaveClassWiseManualBilling(this.AcademicYearId, beData);

                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.TranId.Value : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.ManualBilling;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Class Wise Manual Billing Modify" + beData.TotalAmount.ToString("N") : "New Class Wise Manual Billing" + beData.TotalAmount.ToString("N"));
                        auditLog.AutoManualNo = beData.AutoNumber.ToString();
                        SaveAuditLog(auditLog);
                    }

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
        public JsonNetResult GetClassWiseManualBillingDetailsById(int TranId)
        {
            var dataColl = new AcademicLib.BL.Fee.Creation.ManualBillingClassWise(User.UserId, User.HostName, User.DBName).getClassWiseManualBillingDetailsById(TranId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion
    }
}