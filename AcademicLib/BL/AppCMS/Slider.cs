using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Slider
    {
        DA.AppCMS.Creation.SliderDB db = null;
        int _UserId = 0;

        public Slider(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.SliderDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Slider beData)
        {
            bool isModify = beData.SliderId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.SliderCollections GetAllSlider(int EntityId, string BranchCode)
        {
            return db.getAllSlider(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.Slider GetSliderById(int EntityId, int SliderId)
        {
            return db.getSliderById(_UserId, EntityId, SliderId);
        }
        public ResponeValues DeleteById(int EntityId, int SliderId)
        {
            return db.DeleteById(_UserId, EntityId, SliderId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.Slider beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.SliderId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.SliderId != 0)
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
                }else if (string.IsNullOrEmpty(beData.ImagePath))
                {
                    resVal.ResponseMSG = "Please ! Select Image";
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
