using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Gallery
    {
        DA.AppCMS.Creation.GalleryDB db = null;
        int _UserId = 0;

        public Gallery(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.GalleryDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Gallery beData)
        {
            bool isModify = beData.GalleryId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.GalleryCollections GetAllGallery(int EntityId, string BranchCode)
        {
            return db.getAllGallery(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.Gallery GetGalleryById(int EntityId, int GalleryId)
        {
            return db.getGalleryById(_UserId, EntityId, GalleryId);
        }
        public ResponeValues DeleteById(int EntityId, int GalleryId)
        {
            return db.DeleteById(_UserId, EntityId, GalleryId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.Gallery beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.GalleryId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.GalleryId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Title ";
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
