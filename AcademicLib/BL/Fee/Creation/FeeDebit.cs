using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeDebit
    {
        DA.Fee.Creation.FeeDebitDB db = null;
        int _UserId = 0;
        public FeeDebit(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeDebitDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(int AcademicYearId, BE.Fee.Creation.FeeDebitCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeDebitCollections GetAllFeeMapping(int AcademicYearId, int EntityId, int ClassId, int? SectionId, int MonthId, int? BatchId, int? SemesterId, int? ClassYearId)
        {
            return db.getFeeDebit(_UserId, AcademicYearId, EntityId, ClassId, SectionId, MonthId,BatchId,SemesterId,ClassYearId);
        }
        public ResponeValues Delete(int AcademicYearId, int EntityId, int ClassId, int? SectionId,int MonthId)
        {
            return db.Delete(_UserId, EntityId, AcademicYearId, ClassId, SectionId,MonthId);
        }


        public ResponeValues SaveStudentWise(int AcademicYearId, BE.Fee.Creation.FeeDebitCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdateStudentWise(_UserId, AcademicYearId, beData);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeDebitCollections GetAllStudentWise(int AcademicYearId, int EntityId, int StudentId)
        {
            return db.getFeeDebitStudentWise(_UserId, AcademicYearId, EntityId, StudentId);
        }
        public ResponeValues DeleteStudentWise(int AcademicYearId, int EntityId, int StudentId)
        {
            return db.DeleteStudentWise(_UserId, AcademicYearId, EntityId, StudentId);
        }
        public ResponeValues SaveFeeDebitFeeItemWise(int AcademicYearId, BE.Fee.Creation.FeeDebitFeeItemWise beData)
        {
            return db.SaveFeeDebitFeeItemWise(_UserId,AcademicYearId, beData);
        }
            public ResponeValues IsValidData(ref BE.Fee.Creation.FeeDebitCollections dataColl)
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
