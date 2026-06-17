using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Academic.Transaction
{
    public class ClassGroup
    {
        DA.Academic.Transaction.ClassGroupDB db = null;
        int _UserId = 0;
        public ClassGroup(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Academic.Transaction.ClassGroupDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Academic.Transaction.ClassGroup beData)
        {
            bool isModify = beData.ClassGroupId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Academic.Transaction.ClassGroupCollections GetAllClassGroup(int EntityId)
        {
            return db.getAllClassGroup(_UserId, EntityId);
        }
        public BE.Academic.Transaction.ClassGroup GetClassGroupById(int EntityId, int ClassGroupId)
        {
            return db.getClassGroupById(_UserId, EntityId, ClassGroupId);
        }
        public ResponeValues DeleteById(int EntityId, int ClassGroupId)
        {
            return db.DeleteById(_UserId, EntityId, ClassGroupId);
        }
        public ResponeValues IsValidData(ref BE.Academic.Transaction.ClassGroup beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ClassGroupId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ClassGroupId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter ClassGroup Name";
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
