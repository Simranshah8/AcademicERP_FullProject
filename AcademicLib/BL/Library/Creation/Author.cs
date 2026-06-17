using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class Author
    {
        DA.Library.Creation.AuthorDB db = null;
        int _UserId = 0;
        public Author(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.AuthorDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.Author beData)
        {
            bool isModify = beData.AuthorId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Creation.AuthorCollections GetAllAuthor(int EntityId)
        {
            return db.getAllAuthor(_UserId, EntityId);
        }
        public BE.Library.Creation.Author GetAuthorById(int EntityId, int AuthorId)
        {
            return db.getAuthorById(_UserId, EntityId, AuthorId);
        }
        public ResponeValues DeleteById(int EntityId, int AuthorId)
        {
            return db.DeleteById(_UserId, EntityId, AuthorId);
        }
        public ResponeValues IsValidData(ref BE.Library.Creation.Author beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.AuthorId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.AuthorId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Author Name";
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
