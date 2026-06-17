using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class StudentWiseDiscountSetup
    {
        DA.Fee.Creation.StudentWiseDiscountSetupDB db = null;
        int _UserId = 0;
        public StudentWiseDiscountSetup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.StudentWiseDiscountSetupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeItemWiseDiscountSetupCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeItemWiseDiscountSetupCollections GetStudentWiseDiscountSetup(int AcademicYearId, int EntityId, int StudentId, int? SemesterId, int? ClassYearId,int? BatchId=null)
        {
            return db.getStudentWiseDiscountSetup(_UserId, AcademicYearId, EntityId, StudentId, SemesterId, ClassYearId,BatchId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int StudentId,int? SemesterId,int? ClassYearId)
        {
            return db.Delete(_UserId, AcademicYearId, EntityId, StudentId,SemesterId,ClassYearId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeItemWiseDiscountSetupCollections dataColl)
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
