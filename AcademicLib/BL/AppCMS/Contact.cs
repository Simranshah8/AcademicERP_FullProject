using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Contact
    {
        DA.AppCMS.Creation.ContactDB db = null;
        int _UserId = 0;

        public Contact(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.ContactDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Contact beData)
        {
            bool isModify = beData.ContactId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
      
        public BE.AppCMS.Creation.Contact GetContactById(int EntityId, int ContactId)
        {
            return db.getContactById(_UserId, EntityId, ContactId);
        }
        public ResponeValues DeleteById(int EntityId, int ContactId)
        {
            return db.DeleteById(_UserId, EntityId, ContactId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.Contact beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ContactId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ContactId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Address))
                {
                    resVal.ResponseMSG = "Please ! Enter Address ";
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
