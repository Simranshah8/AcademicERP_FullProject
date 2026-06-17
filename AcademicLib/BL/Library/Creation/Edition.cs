using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class Edition
    {
        DA.Library.Creation.EditionDB db = null;
        int _UserId = 0;
        public Edition(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.EditionDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.Edition beData)
        {
            bool isModify = beData.EditionId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Creation.EditionCollections GetAllEdition(int EntityId)
        {
            return db.getAllEdition(_UserId, EntityId);
        }
        public BE.Library.Creation.Edition GetEditionById(int EntityId, int EditionId)
        {
            return db.getEditionById(_UserId, EntityId, EditionId);
        }
        public ResponeValues DeleteById(int EntityId, int EditionId)
        {
            return db.DeleteById(_UserId, EntityId, EditionId);
        }
        public ResponeValues IsValidData(ref BE.Library.Creation.Edition beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.EditionId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.EditionId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Edition Name";
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
