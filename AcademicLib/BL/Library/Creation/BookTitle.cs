using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Library.Creation
{
    public class BookTitle
    {
        DA.Library.Creation.BookTitleDB db = null;
        int _UserId = 0;
        public BookTitle(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Library.Creation.BookTitleDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Library.Creation.BookTitle beData)
        {
            bool isModify = beData.BookTitleId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Library.Creation.BookTitleCollections GetAllBookTitle(int EntityId)
        {
            return db.getAllBookTitle(_UserId, EntityId);
        }
        public BE.Library.Creation.BookTitle GetBookTitleById(int EntityId, int BookTitleId)
        {
            return db.getBookTitleById(_UserId, EntityId, BookTitleId);
        }
        public ResponeValues DeleteById(int EntityId, int BookTitleId)
        {
            return db.DeleteById(_UserId, EntityId, BookTitleId);
        }
        public ResponeValues IsValidData(ref BE.Library.Creation.BookTitle beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BookTitleId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BookTitleId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter BookTitle Name";
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
