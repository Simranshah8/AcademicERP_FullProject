using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Medium
    {
        DA.Academic.Creation.MediumDB db = null;
        int _UserId = 0;
        public Medium(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.MediumDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Medium beData)
        {
            bool isModify = beData.MediumId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.MediumCollections GetAllMedium(int EntityId)
        {
            return db.getAllMedium(_UserId, EntityId);
        }
        public BE.Academic.Creation.Medium GetMediumById(int EntityId, int MediumId)
        {
            return db.getMediumById(_UserId, EntityId, MediumId);
        }
        public ResponeValues DeleteById(int EntityId, int MediumId)
        {
            return db.DeleteById(_UserId, EntityId, MediumId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Medium beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.MediumId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.MediumId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Medium Name";
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
