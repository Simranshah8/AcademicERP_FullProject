using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeMappingForWise
    {
        DA.Fee.Creation.FeeMappingForWiseDB db = null;
        int _UserId = 0;
        public FeeMappingForWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeMappingForWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeMappingMediumWiseCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeMappingMediumWiseCollections GetAllFeeMapping(int AcademicYearId, int EntityId, int MediumId)
        {
            return db.getAllFeeMapping(_UserId, AcademicYearId, EntityId, MediumId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int MediumId)
        {
            return db.Delete(_UserId, AcademicYearId, EntityId, MediumId);
        }
        public BE.Fee.Creation.ManualBillingDetailsCollections getFeeItemFor(int? AcademicYearId, int? ClassId, int ForId)
        {
            return db.getFeeItemFor(_UserId, AcademicYearId, ClassId, ForId);
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
