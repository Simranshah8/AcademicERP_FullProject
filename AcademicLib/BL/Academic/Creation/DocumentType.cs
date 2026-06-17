using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class DocumentType
    {
        DA.Academic.Creation.DocumentTypeDB db = null;
        int _UserId = 0;
        public DocumentType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.DocumentTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.DocumentType beData)
        {
            bool isModify = beData.DocumentTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.DocumentTypeCollections GetAllDocumentType(int EntityId)
        {
            return db.getAllDocumentType(_UserId, EntityId);
        }
        public BE.Academic.Creation.DocumentType GetDocumentTypeById(int EntityId, int DocumentTypeId)
        {
            return db.getDocumentTypeById(_UserId, EntityId, DocumentTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int DocumentTypeId)
        {
            return db.DeleteById(_UserId, EntityId, DocumentTypeId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.DocumentType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.DocumentTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.DocumentTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter DocumentType Name";
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
