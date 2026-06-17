using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class ClassWiseDiscountSetup
    {
        DA.Fee.Creation.ClassWiseDiscountSetupDB db = null;
        int _UserId = 0;
        public ClassWiseDiscountSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.ClassWiseDiscountSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.ClassWiseDiscountSetupCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId,AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.ClassWiseDiscountSetupCollections getClassWiseDiscountSetup(int AcademicYearId, int EntityId, int ClassId, int? SectionId, int? SemesterId, int? ClassYearId)
        {
            return db.getClassWiseDiscountSetup(_UserId,AcademicYearId, EntityId, ClassId, SectionId,   SemesterId,   ClassYearId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int ClassId, int? SectionId, int? SemesterId, int? ClassYearId)
        {
            return db.Delete(_UserId,AcademicYearId, EntityId, ClassId, SectionId,   SemesterId,  ClassYearId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.ClassWiseDiscountSetupCollections dataColl)
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
