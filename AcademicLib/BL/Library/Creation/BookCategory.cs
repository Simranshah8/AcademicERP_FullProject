using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.BookCategory.Creation
{
    public class BookCategory
    {

        DA.BookCategory.Creation.BookCategoryDB db = null;

        int _UserId = 0;

        public BookCategory(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.BookCategory.Creation.BookCategoryDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.BookCategory.Creation.BookCategory beData)
        {
            bool isModify = beData.BookCategoryId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.BookCategory.Creation.BookCategoryColl getAllBookCategory(int EntityId)
        {
            return db.getAllBookCategory(_UserId, EntityId);
        }
        public BE.BookCategory.Creation.BookCategory getBookCategoryById(int EntityId, int BookCategoryId)
        {
            return db.BookCategoryById(_UserId, EntityId, BookCategoryId);
        }
        public ResponeValues DeleteById(int EntityId, int BookCategoryId)
        {
            return db.DeleteById(_UserId, EntityId, BookCategoryId);
        }
        public ResponeValues IsValidData(ref BE.BookCategory.Creation.BookCategory beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BookCategoryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BookCategoryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Name ";
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