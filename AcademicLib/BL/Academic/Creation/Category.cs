using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Category
    {
        DA.Academic.Creation.CategoryDB db = null;
        int _UserId = 0;
        public Category(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.CategoryDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Category beData)
        {
            bool isModify = beData.CategoryId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.CategoryCollections GetAllCategory(int EntityId)
        {
            return db.getAllCategory(_UserId, EntityId);
        }
        public BE.Academic.Creation.Category GetCategoryById(int EntityId, int CategoryId)
        {
            return db.getCategoryById(_UserId, EntityId, CategoryId);
        }
        public ResponeValues DeleteById(int EntityId, int CategoryId)
        {
            return db.DeleteById(_UserId, EntityId, CategoryId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Category beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.CategoryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.CategoryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Category Name";
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
