using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Download
    {
        DA.AppCMS.Creation.DownloadDB db = null;
        int _UserId = 0;
        public Download(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.DownloadDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Download beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.DownloadCollections GetDownloads(int EntityId)
        {
            return db.GetDownloads(_UserId, EntityId);
        }
        public BE.AppCMS.Creation.Download GetDownloadById(int EntityId, int TranId)
        {
            return db.GetDownloadById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteDownload(int EntityId, int TranId)
        {
            return db.DeleteDownload(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.Download beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Title";
                }
                else if (string.IsNullOrEmpty(beData.AttachmentPath))
                {
                    resVal.ResponseMSG = "Please ! Upload File.";
                }
                else if (!beData.OrderNo.HasValue)
                {
                    resVal.ResponseMSG = "Please! Enter Order No.";
                }
                else if (!beData.IsActive.HasValue)
                {
                    resVal.ResponseMSG = "Please! Set active status";
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