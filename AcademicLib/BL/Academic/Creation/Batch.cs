using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Creation
{
    public class Batch
    {
        DA.Academic.Creation.BatchDB db = null;
        int _UserId = 0;
        public Batch(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Creation.BatchDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Creation.Batch beData)
        {
            bool isModify = beData.BatchId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Creation.BatchCollections GetAllBatch(int EntityId)
        {
            return db.getAllBatch(_UserId, EntityId);
        }
        public BE.Academic.Creation.BatchCollections getAllBatchForTran(int EntityId, int AcademicYearId)
        {
            return db.getAllBatchForTran(_UserId, EntityId, AcademicYearId);
        }
        public BE.Academic.Creation.Batch GetBatchById(int EntityId, int BatchId)
        {
            return db.getBatchById(_UserId, EntityId, BatchId);
        }
        public ResponeValues DeleteById(int EntityId, int BatchId)
        {
            return db.DeleteById(_UserId, EntityId, BatchId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Creation.Batch beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BatchId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BatchId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Batch Name";
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
