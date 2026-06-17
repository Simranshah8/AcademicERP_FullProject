using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class ExtraFeeItemMapping
    {
        DA.Fee.Creation.ExtraFeeItemMappingDB db = null;
        int _UserId = 0;
        public ExtraFeeItemMapping(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.ExtraFeeItemMappingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.ExtraFeeItemMappingCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.ExtraFeeItemMappingCollections GetAllFeeMapping(int EntityId, int AcademicYearId, int ClassId, int? SectionId)
        {
            return db.getExtraFeeItemMapping(_UserId,EntityId, AcademicYearId, ClassId, SectionId);
        }
        public ResponeValues Delete(int EntityId, int AcademicYearId, int ClassId, int? SectionId)
        {
            return db.Delete(_UserId,EntityId, AcademicYearId, ClassId, SectionId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.ExtraFeeItemMappingCollections dataColl)
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
