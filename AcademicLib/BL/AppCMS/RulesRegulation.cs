using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class RulesRegulation
    {
        DA.AppCMS.Creation.RulesRegulationDB db = null;
        int _UserId = 0;

        public RulesRegulation(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.RulesRegulationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.RulesRegulation beData)
        {
            bool isModify = beData.RulesRegulationId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.RulesRegulationCollections GetAllRulesRegulation(int EntityId, string BranchCode)
        {
            return db.getAllRulesRegulation(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.RulesRegulation GetRulesRegulationById(int EntityId, int RulesRegulationId)
        {
            return db.getRulesRegulationById(_UserId, EntityId, RulesRegulationId);
        }
        public ResponeValues DeleteById(int EntityId, int RulesRegulationId)
        {
            return db.DeleteById(_UserId, EntityId, RulesRegulationId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.RulesRegulation beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RulesRegulationId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RulesRegulationId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Heading ";
                }
                else if (string.IsNullOrEmpty(beData.Description))
                {
                    resVal.ResponseMSG = "Please ! Enter Description ";
                }
                else
                {
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return resVal;
        }
    }
}
