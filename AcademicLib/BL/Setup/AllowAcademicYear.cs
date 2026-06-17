using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Setup
{
    public class AllowAcademicYear
    {
        DA.Setup.AllowAcademicYearDB db = null;
        int _UserId = 0;

        public AllowAcademicYear(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Setup.AllowAcademicYearDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Setup.AllowEntityAccessCollections dataColl)
        {
            ResponeValues isValid = IsValidData(ref dataColl);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, dataColl);
            else
                return isValid;
        }
        public BE.Setup.AllowEntityAccessCollections getAllowAcademicYearList(int? ForUserId,int? ForGroupId)
        {
            return db.getAllowAcademicYearList(_UserId,ForUserId,ForGroupId);
        }
        public ResponeValues IsValidData(ref BE.Setup.AllowEntityAccessCollections dataColl)
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
