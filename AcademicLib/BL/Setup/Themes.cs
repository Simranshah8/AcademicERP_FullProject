using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class Themes
    {
        DA.Setup.ThemesDB db = null;
        int _UserId = 0;

        public Themes(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.ThemesDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Setup.Themes dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(dataColl);
            else
                return isValid;
        }
        public BE.Setup.Themes getThems()
        {
            return db.getThemes(_UserId);
        }
        public ResponeValues IsValidData(ref BE.Setup.Themes beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
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
