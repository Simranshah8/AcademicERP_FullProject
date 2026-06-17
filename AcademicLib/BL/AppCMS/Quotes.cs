using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.AppCMS.Creation
{
    public class Quotes
    {
        DA.AppCMS.Creation.QuotesDB db = null;
        int _UserId = 0;

        public Quotes(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.AppCMS.Creation.QuotesDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.AppCMS.Creation.Quotes beData)
        {
            bool isModify = beData.QuotesId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.AppCMS.Creation.QuotesCollections GetAllQuotes(int EntityId, string BranchCode)
        {
            return db.getAllQuotes(_UserId, EntityId,BranchCode);
        }
        public BE.AppCMS.Creation.Quotes GetQuotesById(int EntityId, int QuotesId)
        {
            return db.getQuotesById(_UserId, EntityId, QuotesId);
        }
        public ResponeValues DeleteById(int EntityId, int QuotesId)
        {
            return db.DeleteById(_UserId, EntityId, QuotesId);
        }

        public AcademicLib.BE.AppCMS.Creation.Quotes getTodayQuotes()
        {
            return db.getTodayQuotes(_UserId);
        }
            public ResponeValues IsValidData(ref BE.AppCMS.Creation.Quotes beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.QuotesId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.QuotesId != 0)
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
                else if(!beData.ForDate.HasValue || beData.ForDate.Value.Year < 2000)
                {
                    resVal.ResponseMSG = "Please ! Enter Valid Date ";
                }
                else if(!beData.ForStudent && !beData.ForTeacher && !beData.ForAdmin)
                {
                    resVal.ResponseMSG = "Please Checked Any One or more from (Student/Teacher/Admin)";
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
