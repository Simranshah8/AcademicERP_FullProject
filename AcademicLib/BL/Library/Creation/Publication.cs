using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class Publication
    {
        DA.Library.Creation.PublicationDB db = null;
        int _UserId = 0;
        public Publication(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.PublicationDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.Publication beData)
        {
            bool isModify = beData.PublicationId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Creation.PublicationCollections GetAllPublication(int EntityId)
        {
            return db.getAllPublication(_UserId, EntityId);
        }
        public BE.Library.Creation.Publication GetPublicationById(int EntityId, int PublicationId)
        {
            return db.getPublicationById(_UserId, EntityId, PublicationId);
        }
        public ResponeValues DeleteById(int EntityId, int PublicationId)
        {
            return db.DeleteById(_UserId, EntityId, PublicationId);
        }
        public ResponeValues IsValidData(ref BE.Library.Creation.Publication beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.PublicationId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.PublicationId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Publication Name";
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
