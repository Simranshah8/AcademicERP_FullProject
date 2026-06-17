using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Testimonial
    {
        DA.AppCMS.Creation.TestimonialDB db = null;
        int _UserId = 0;

        public Testimonial(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.TestimonialDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Testimonial beData)
        {
            bool isModify = beData.TestimonialId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.TestimonialCollections GetAllTestimonial(int EntityId, string BranchCode)
        {
            return db.getAllTestimonial(_UserId, EntityId, BranchCode);
        }
        public BE.AppCMS.Creation.Testimonial GetTestimonialById(int EntityId, int TestimonialId)
        {
            return db.getTestimonialById(_UserId, EntityId, TestimonialId);
        }
        public ResponeValues DeleteById(int EntityId, int TestimonialId)
        {
            return db.DeleteById(_UserId, EntityId, TestimonialId);
        }
        public ResponeValues IsValidData(ref BE.AppCMS.Creation.Testimonial beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TestimonialId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TestimonialId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Title))
                {
                    resVal.ResponseMSG = "Please ! Enter Heading ";
                }
                else if (string.IsNullOrEmpty(beData.Designation))
                {
                    resVal.ResponseMSG = "Please ! Enter Designation ";
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
