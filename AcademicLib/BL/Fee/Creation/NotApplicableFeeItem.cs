using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class NotApplicableFeeItem
    {
        DA.Fee.Creation.NotApplicableFeeItemDB db = null;
        int _UserId = 0;
        public NotApplicableFeeItem(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.NotApplicableFeeItemDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.NotApplicableFeeItemCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.NotApplicableFeeItemCollections GetAllFeeMapping(int EntityId, int AcademicYearId, int ClassId,int? SectionId, int? SemesterId, int? ClassYearId, int FeeItemId)
        {
            return db.getAllFeeMapping(_UserId, EntityId, AcademicYearId,  ClassId,SectionId,SemesterId,ClassYearId, FeeItemId);
        }
        public ResponeValues Delete(int EntityId, int AcademicYearId, int ClassId, int? SectionId,int? SemesterId,int? ClassYearId, int FeeItemId)
        {
            return db.Delete(_UserId, EntityId, AcademicYearId, ClassId, SectionId,SemesterId,ClassYearId, FeeItemId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.NotApplicableFeeItemCollections dataColl)
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
