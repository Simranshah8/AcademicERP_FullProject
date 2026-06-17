using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicERP.BL.Health.Transaction
{
    public class GeneralHealth
    {
        DA.Health.Transaction.GeneralHealthDB db = null;
        int _UserId = 0;
        public GeneralHealth(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Health.Transaction.GeneralHealthDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Health.Transaction.GeneralHealth beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Health.Transaction.GeneralHealthCollections GetAllGeneralHealth(int EntityId, int? StudentId, int? EmployeeId)
        {
            return db.getAllGeneralHealth(_UserId, EntityId,StudentId,EmployeeId);
        }

        public BE.Health.Transaction.GeneralHealth GetGeneralHealthById(int EntityId, int TranId)
        {
            return db.getGeneralHealthById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Health.Transaction.GeneralHealth beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.Remarks))
                {
                    resVal.ResponseMSG = "Please ! Enter GeneralHealth Remarks";
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