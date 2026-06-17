using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeMappingBatchWise
    {
        DA.Fee.Creation.FeeMappingBatchWiseDB db = null;
        int _UserId = 0;
        public FeeMappingBatchWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeMappingBatchWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeMappingClassWiseCollections beData)
        {
            var fst = beData.First();
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId,fst.BatchId,fst.FacultyId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeMappingClassWiseCollections GetAllFeeMapping(int AcademicYearId, int EntityId,int BatchId,int FacultyId)
        {
            return db.getAllFeeMapping(_UserId, AcademicYearId, EntityId,BatchId,FacultyId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int BatchId, int FacultyId)
        {
            return db.Delete(_UserId, AcademicYearId, EntityId,BatchId,FacultyId);
        }
        
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeMappingClassWiseCollections dataColl)
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
