using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeMappingClassWise
    {
        DA.Fee.Creation.FeeMappingClassWiseDB db = null;
        int _UserId = 0;
        public FeeMappingClassWise(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeMappingClassWiseDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeMappingClassWiseCollections beData)
        {            
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeMappingClassWiseCollections GetAllFeeMapping(int AcademicYearId, int EntityId)
        {
            return db.getAllFeeMapping(_UserId, AcademicYearId, EntityId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId)
        {
            return db.Delete(_UserId, AcademicYearId, EntityId);
        }
        public RE.Fee.FeeMappingStudentCollections getFeeMappingStudentList( int AcademicYearId, string ClassIdColl, string FeeItemIdColl,int For)
        {
            return db.getFeeMappingStudentList(_UserId, AcademicYearId, ClassIdColl, FeeItemIdColl,For);
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
