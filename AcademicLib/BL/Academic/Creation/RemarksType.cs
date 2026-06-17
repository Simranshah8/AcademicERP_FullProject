using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class RemarksType
    {
        DA.Academic.Creation.RemarksTypeDB db = null;
        int _UserId = 0;
        public RemarksType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.RemarksTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.RemarksType beData)
        {
            bool isModify = beData.RemarksTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.RemarksTypeCollections GetAllRemarksType(int EntityId)
        {
            return db.getAllRemarksType(_UserId, EntityId);
        }
        public BE.Academic.Creation.RemarksType GetRemarksTypeById(int EntityId, int RemarksTypeId)
        {
            return db.getRemarksTypeById(_UserId, EntityId, RemarksTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int RemarksTypeId)
        {
            return db.DeleteById(_UserId, EntityId, RemarksTypeId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.RemarksType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RemarksTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RemarksTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter RemarksType Name";
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
