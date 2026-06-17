using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicERP.BL.Health.Transaction
{
    public class UrineTest
    {
        DA.Health.Transaction.UrineTestDB db = null;
        int _UserId = 0;
        public UrineTest(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Health.Transaction.UrineTestDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Health.Transaction.UrineTest beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Health.Transaction.UrineTestCollections GetAllUrineTest(int EntityId, int? StudentId, int? EmployeeId)
        {
            return db.getAllUrineTest(_UserId, EntityId, StudentId, EmployeeId);
        }

        public BE.Health.Transaction.UrineTest GetUrineTestById(int EntityId, int TranId)
        {
            return db.getUrineTestById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref BE.Health.Transaction.UrineTest beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.Protein))
                {
                    resVal.ResponseMSG = "Please ! Enter Protien Level Of Student";
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