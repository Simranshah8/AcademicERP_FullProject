using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class AppFeatures
    {
        DA.Setup.AppFeaturesDB db = null;
        int _UserId = 0;

        public AppFeatures(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.AppFeaturesDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Setup.AppFeaturesCollections dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public BE.Setup.AppFeaturesCollections getAllAppFeatures(int? ForUserId)
        {
            return db.getAllAppFeatures(_UserId,ForUserId);
        }
        public BE.Setup.AppFeaturesCollections getAppFeatures()
        {
            return db.getAppFeatures(_UserId);
        }
        public ResponeValues IsValidData(ref BE.Setup.AppFeaturesCollections dataColl)
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
                    foreach (var v in dataColl)
                        v.CUserId = _UserId;

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
