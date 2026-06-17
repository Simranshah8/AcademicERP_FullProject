using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class ComplainType
    {
        DA.FrontDesk.Transaction.ComplainTypeDB db = null;
        int _UserId = 0;

        public ComplainType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.ComplainTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.ComplainType beData)
        {
            bool isModify = beData.ComplainTypeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.ComplainTypeCollections GetAllComplainType(int EntityId)
        {
            return db.getAllComplainType(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.ComplainType GetComplainTypeById(int EntityId, int ComplainTypeId)
        {
            return db.getComplainTypeById(_UserId, EntityId, ComplainTypeId);
        }
        public ResponeValues DeleteById(int EntityId, int ComplainTypeId)
        {
            return db.DeleteById(_UserId, EntityId, ComplainTypeId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.ComplainType beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ComplainTypeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ComplainTypeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter Name";
                }
                //else if (beData.ComplainTypeId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select ComplainType";
                //}

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
