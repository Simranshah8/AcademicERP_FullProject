using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Academic.Setup
{
    public class HTMLTemplateConfig
    {
        DA.Academic.Setup.HTMLTemplateConfigDB db = null;
        int _UserId = 0;
        public HTMLTemplateConfig(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Setup.HTMLTemplateConfigDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Setup.HTMLTemplateConfigCollection beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, beData);
            else
                return isValid;
        }
        public BE.Academic.Setup.HTMLTemplateConfigCollection GetHTMLTemplatesConfig(int? EntityId)
        {
            return db.GetHTMLTemplatesConfig(_UserId, EntityId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Setup.HTMLTemplateConfigCollection dataColl)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (dataColl == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
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