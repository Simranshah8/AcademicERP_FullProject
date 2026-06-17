using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class SENT
    {
        DA.Setup.SENTDB db = null;
        int _UserId = 0;
        public SENT(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.SENTDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Setup.SENT beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Setup.SENTCollections GetAllSENT(int EntityId,int ForATS)
        {
            return null;
            //return db.getTemplates(_UserId, EntityId,ForATS);
        }
        public BE.Setup.SENTCollections GetSENT(int EntityId, int ForATS,int TemplateType)
        {
            return db.getTemplates(_UserId, EntityId, ForATS, TemplateType);
        }
        public BE.Setup.SENT getSENTById( int EntityId, int TranId)
        {
            return db.getSENTById(_UserId, EntityId, TranId);
        }
            public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Setup.SENT beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Template Name";
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
