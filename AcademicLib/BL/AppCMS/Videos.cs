using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Videos
    {
        DA.AppCMS.Creation.VideosDB db = null;
        int _UserId = 0;

        public Videos(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.VideosDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Videos beData)
        {
            bool isModify = beData.VideosId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.VideosCollections GetAllVideos(int EntityId, string BranchCode)
        {
            return db.getAllVideos(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.Videos GetVideosById(int EntityId, int VideosId)
        {
            return db.getVideosById(_UserId, EntityId, VideosId);
        }
        public ResponeValues DeleteById(int EntityId, int VideosId)
        {
            return db.DeleteById(_UserId, EntityId, VideosId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.Videos beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.VideosId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.VideosId != 0)
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
                    if(beData.UrlColl!=null && beData.UrlColl.Count > 0)
                    {
                        string url = "";
                        foreach(var u in beData.UrlColl)
                        {
                            if (!string.IsNullOrEmpty(url))
                                url = url + "##";

                            url = url + u;
                        }
                        beData.AddUrl = url;
                    }
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
