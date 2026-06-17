using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeItemGroup 
    {
        DA.Fee.Creation.FeeItemGroupDB db = null;
        int _UserId = 0;
        public FeeItemGroup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeItemGroupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Creation.FeeItemGroup beData)
        {
            bool isModify = beData.FeeItemGroupId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeItemGroupCollections GetAllFeeItemGroup(int EntityId)
        {
            return db.getAllFeeItemGroup(_UserId, EntityId);
        }
        public BE.Fee.Creation.FeeItemGroup GetFeeItemGroupById(int EntityId, int FeeItemGroupId)
        {
            return db.getFeeItemGroupById(_UserId, EntityId, FeeItemGroupId);
        }
        public ResponeValues DeleteById(int EntityId, int FeeItemGroupId)
        {
            return db.DeleteById(_UserId, EntityId, FeeItemGroupId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeItemGroup beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FeeItemGroupId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FeeItemGroupId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter FeeItemGroup Name";
                }else if(beData.FeeItemIdColl==null || beData.FeeItemIdColl.Count == 0)
                {
                    resVal.ResponseMSG = "Please ! Select Fee Item Name";
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
