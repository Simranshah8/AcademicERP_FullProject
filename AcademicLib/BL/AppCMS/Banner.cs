using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Banner
    {
        DA.AppCMS.Creation.BannerDB db = null;
        int _UserId = 0;

        public Banner(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.BannerDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Banner beData)
        {
            bool isModify = beData.BannerId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.BannerCollections GetAllBanner(int EntityId, string BranchCode)
        {
            return db.getAllBanner(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.Banner GetBannerById(int EntityId, int BannerId)
        {
            return db.getBannerById(_UserId, EntityId, BannerId);
        }
        public ResponeValues DeleteById(int EntityId, int BannerId)
        {
            return db.DeleteById(_UserId, EntityId, BannerId);
        }
        public AcademicLib.BE.AppCMS.Creation.BannerCollections getAllBannerForApp(string BranchCode)
        {
            return db.getAllBannerForApp(_UserId,BranchCode);
        }
            public ResponeValues IsValidData(ref BE.AppCMS.Creation.Banner beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BannerId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BannerId != 0)
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
                else if (string.IsNullOrEmpty(beData.ImagePath))
                {
                    resVal.ResponseMSG = "Please ! Select Image";
                }else if (!beData.PublishOn.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Published On Date";
                }else if (!beData.ValidUpTo.HasValue)
                {
                    resVal.ResponseMSG = "Please ! Enter Valid Upto Date";
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
