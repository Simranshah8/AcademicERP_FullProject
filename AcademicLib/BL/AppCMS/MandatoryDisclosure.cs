using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class MandatoryDisclosure
    {
        DA.AppCMS.Creation.MandatoryDisclosureDB db = null;
        int _UserId = 0;
        public MandatoryDisclosure(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.MandatoryDisclosureDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.MandatoryDisclosure beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.MandatoryDisclosureCollections GetMandatoryDisclosures(int EntityId)
        {
            return db.GetMandatoryDisclosures(_UserId, EntityId);
        }
        public BE.AppCMS.Creation.MandatoryDisclosure GetMandatoryDisclosureById(int EntityId,int TranId)
        {
            return db.GetMandatoryDisclosureById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteMandatoryDisclosure(int EntityId, int TranId)
        {
            return db.DeleteMandatoryDisclosure(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.MandatoryDisclosure beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Title";
                }
                else if (string.IsNullOrEmpty(beData.Description))
                {
                    resVal.ResponseMSG = "Please ! Enter Description";
                }
                else if (!beData.OrderNo.HasValue)
                {
                    resVal.ResponseMSG = "Please! Enter Order No.";
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