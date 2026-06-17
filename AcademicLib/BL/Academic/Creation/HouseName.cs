using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class HouseName
    {
        DA.Academic.Creation.HouseNameDB db = null;
        int _UserId = 0;
        public HouseName(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.HouseNameDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.HouseName beData)
        {
            bool isModify = beData.HouseNameId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.HouseNameCollections GetAllHouseName(int EntityId)
        {
            return db.getAllHouseName(_UserId, EntityId);
        }
        public BE.Academic.Creation.HouseName GetHouseNameById(int EntityId, int HouseNameId)
        {
            return db.getHouseNameById(_UserId, EntityId, HouseNameId);
        }
        public ResponeValues DeleteById(int EntityId, int HouseNameId)
        {
            return db.DeleteById(_UserId, EntityId, HouseNameId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.HouseName beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.HouseNameId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.HouseNameId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter HouseName Name";
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
