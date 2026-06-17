using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicERP.BL.Health.Transaction
{
    public class HealthGrowth
    {
        DA.Health.Transaction.HealthGrowthDB db = null;
        int _UserId = 0;
        public HealthGrowth(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Health.Transaction.HealthGrowthDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Health.Transaction.HealthGrowth beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Health.Transaction.HealthGrowthCollections GetAllHealthGrowth(int EntityId, int? StudentId, int? EmployeeId)
        {
            return db.getAllHealthGrowth(_UserId, EntityId, StudentId, EmployeeId);
        }

        public BE.Health.Transaction.HealthGrowth GetHealthGrowthById(int EntityId, int TranId)
        {
            return db.getHealthGrowthById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Health.Transaction.HealthGrowth beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.TranId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.TranId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Height))
                {
                    resVal.ResponseMSG = "Please ! Enter Height of the Students";
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