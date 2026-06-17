using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class AdvanceType
    {
        DA.Payroll.AdvanceTypeDB db = null;
        int _UserId = 0;
        public AdvanceType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.AdvanceTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Payroll.AdvanceType beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Payroll.AdvanceTypeCollections GetAllAdvanceType(int EntityId)
        {
            return db.getAllAdvanceType(_UserId, EntityId);
        }

        public AcademicLib.BE.Payroll.AdvanceType GetAdvanceTypeById(int EntityId, int TranId)
        {
            return db.getAdvanceTypeById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.AdvanceType beData, bool IsModify)
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
                else if (string.IsNullOrEmpty(beData.Name))
                {
                    resVal.ResponseMSG = "Please ! Enter AdvanceType Name";
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