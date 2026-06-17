using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
   public class AboutUs
    {
        DA.AppCMS.Creation.AboutUsDB db = null;
        int _UserId = 0;

        public AboutUs(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.AboutUsDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.AboutUs beData)
        {
            bool isModify = false;// beData.AboutUsId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public ResponeValues SaveFeedback(AcademicLib.API.AppCMS.FeedbackSuggestion beData)
        {
            return db.SaveFeedback(beData);
        }
        public ResponeValues UpdateFeedback(AcademicLib.API.AppCMS.FeedbackSuggestion beData)
        {
            beData.UserId = _UserId;
            return db.UpdateFeedback(beData);
        }
            public BE.AppCMS.Creation.AboutUsCollections GetAllAboutUs(int EntityId,string BranchCode)
        {
            return db.getAllAboutUs(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.AboutUs GetAboutUsById(int EntityId, int AboutUsId)
        {
            return db.getAboutUsById(_UserId, EntityId, AboutUsId);
        }
        public ResponeValues DeleteById(int EntityId, int AboutUsId)
        {
            return db.DeleteById(_UserId, EntityId, AboutUsId);
        }
        public AcademicLib.API.AppCMS.About getAbout(string BranchCode, bool ForAppCMS = false)
        {
            return db.getAbout(_UserId,BranchCode,ForAppCMS);
        }

        public AcademicLib.API.AppCMS.WhoWeAre getWhoWeAre(string BranchCode)
        {
            return db.getWhoWeAre(_UserId,BranchCode);
        }
        public AcademicLib.API.AppCMS.FeedbackSuggestionCollections getFeedbackList( DateTime? dateFrom, DateTime? dateTo)
        {
            return db.getFeedbackList(_UserId, dateFrom, dateTo);
        }
            public ResponeValues IsValidData(ref BE.AppCMS.Creation.AboutUs beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                //else if (IsModify && beData.AboutUsId == 0)
                //{
                //    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                //}
                //else if (!IsModify && beData.AboutUsId != 0)
                //{
                //    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                //}
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.LogoPath))
                {
                    resVal.ResponseMSG = "Please ! Select Logo Image";
                }
                else if (string.IsNullOrEmpty(beData.BannerPath))
                {
                    resVal.ResponseMSG = "Please ! Select Banner Image";
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
