using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeMappingStudentTypeWise
    {
        DA.Fee.Creation.FeeMappingStudentTypeWiseDB db = null;
        int _UserId = 0;
        public FeeMappingStudentTypeWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeMappingStudentTypeWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeMappingStudentTypeWiseCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeMappingStudentTypeWiseCollections GetAllFeeMapping(int AcademicYearId, int EntityId, int StudentTypeId)
        {
            return db.getAllFeeMapping(_UserId, AcademicYearId, EntityId, StudentTypeId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int StudentTypeId)
        {
            return db.Delete(_UserId, AcademicYearId, EntityId, StudentTypeId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeMappingStudentTypeWiseCollections dataColl)
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
