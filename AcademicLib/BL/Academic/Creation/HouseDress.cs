using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class HouseDress
    {
        DA.Academic.Creation.HouseDressDB db = null;
        int _UserId = 0;
        public HouseDress(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.HouseDressDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.HouseDress beData)
        {
            bool isModify = beData.HouseDressId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.HouseDressCollections GetAllHouseDress(int EntityId)
        {
            return db.getAllHouseDress(_UserId, EntityId);
        }
        public BE.Academic.Creation.HouseDress GetHouseDressById(int EntityId, int HouseDressId)
        {
            return db.getHouseDressById(_UserId, EntityId, HouseDressId);
        }
        public ResponeValues DeleteById(int EntityId, int HouseDressId)
        {
            return db.DeleteById(_UserId, EntityId, HouseDressId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.HouseDress beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.HouseDressId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.HouseDressId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter HouseDress Name";
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
