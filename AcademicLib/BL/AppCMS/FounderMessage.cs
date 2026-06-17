using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class FounderMessage
    {
        DA.AppCMS.Creation.FounderMessageDB db = null;
        int _UserId = 0;

        public FounderMessage(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.FounderMessageDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.FounderMessage beData)
        {
            bool isModify = beData.FounderMessageId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.FounderMessageCollections GetAllFounderMessage(int EntityId, string BranchCode)
        {
            return db.getAllFounderMessage(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.FounderMessage GetFounderMessageById(int EntityId, int FounderMessageId)
        {
            return db.getFounderMessageById(_UserId, EntityId, FounderMessageId);
        }
        public ResponeValues DeleteById(int EntityId, int FounderMessageId)
        {
            return db.DeleteById(_UserId, EntityId, FounderMessageId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.FounderMessage beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FounderMessageId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FounderMessageId != 0)
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
                else if (string.IsNullOrEmpty(beData.Designation))
                {
                    resVal.ResponseMSG = "Please ! Enter Designation ";
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
