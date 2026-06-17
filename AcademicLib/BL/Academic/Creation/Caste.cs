using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Caste
    {
        DA.Academic.Creation.CasteDB db = null;
        int _UserId = 0;
        public Caste(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.CasteDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Caste beData)
        {
            bool isModify = beData.CasteId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.CasteCollections GetAllCaste(int EntityId)
        {
            return db.getAllCaste(_UserId, EntityId);
        }
        public BE.Academic.Creation.Caste GetCasteById(int EntityId, int CasteId)
        {
            return db.getCasteById(_UserId, EntityId, CasteId);
        }
        public ResponeValues DeleteById(int EntityId, int CasteId)
        {
            return db.DeleteById(_UserId, EntityId, CasteId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Caste beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.CasteId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.CasteId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Caste Name";
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
