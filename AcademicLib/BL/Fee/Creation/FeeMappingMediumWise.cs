using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeMappingMediumWise
    {
        DA.Fee.Creation.FeeMappingMediumWiseDB db = null;
        int _UserId = 0;
        public FeeMappingMediumWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeMappingMediumWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeMappingMediumWiseCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeMappingMediumWiseCollections GetAllFeeMapping(int AcademicYearId, int EntityId,int MediumId)
        {
            return db.getAllFeeMapping(_UserId, AcademicYearId, EntityId,MediumId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId,int MediumId)
        {
            return db.Delete(_UserId, AcademicYearId, EntityId,MediumId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeMappingMediumWiseCollections dataColl)
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
