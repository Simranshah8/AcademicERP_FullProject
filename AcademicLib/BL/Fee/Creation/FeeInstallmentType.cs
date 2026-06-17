using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Fee.Creation
{
    public class FeeInstallmentType
    {
        DA.Fee.Creation.FeeInstallmentTypeDB db = null;
        int _UserId = 0;
        public FeeInstallmentType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Fee.Creation.FeeInstallmentTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Fee.Creation.FeeInstallmentType beData)
        {
            bool isModify = beData.FeeInstallmentTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Fee.Creation.FeeInstallmentTypeCollections GetAllFeeInstallmentType(int EntityId)
        {
            return db.getAllFeeInstallment(_UserId, EntityId);
        }
        public BE.Fee.Creation.FeeInstallmentTypeCollections getAllFeeInstallmentTypeForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllFeeInstallmentForTran(_UserId, EntityId, AcademicYearId);
        }
        public BE.Fee.Creation.FeeInstallmentType GetFeeInstallmentTypeById(int EntityId, int FeeInstallmentTypeId)
        {
            return db.getFeeInstallmentById(_UserId, EntityId, FeeInstallmentTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int FeeInstallmentTypeId)
        {
            return db.DeleteById(_UserId, EntityId, FeeInstallmentTypeId);
        }
        public ResponeValues IsValidData(ref BE.Fee.Creation.FeeInstallmentType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FeeInstallmentTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FeeInstallmentTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter FeeInstallmentType Name";
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
