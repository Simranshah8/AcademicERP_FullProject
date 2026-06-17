using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class MessFeeItemMapping
    {
        DA.Fee.Creation.MessFeeItemMappingDB db = null;
        int _UserId = 0;
        public MessFeeItemMapping(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.MessFeeItemMappingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId,BE.Fee.Creation.MessFeeItemMappingCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.MessFeeItemMappingCollections GetAllFeeMapping(int EntityId, int AcademicYearId, int ClassId, int? SectionId)
        {
            return db.getMessFeeItemMapping(_UserId,EntityId,AcademicYearId, ClassId, SectionId);
        }
        public ResponeValues Delete(int EntityId, int AcademicYearId, int ClassId, int? SectionId)
        {
            return db.Delete(_UserId, EntityId, AcademicYearId, ClassId, SectionId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.MessFeeItemMappingCollections dataColl)
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
