using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class Incentive
    {
        DA.Payroll.IncentiveDB db = null;
        int _UserId = 0;
        public Incentive(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.IncentiveDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Payroll.Incentive beData)
        {
            bool isModify = beData.IncentiveId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Payroll.IncentiveCollections GetAllIncentive(int EntityId)
        {
            return db.getAllIncentive(_UserId, EntityId);
        }

        public AcademicLib.BE.Payroll.Incentive GetIncentiveById(int EntityId, int IncentiveId)
        {
            return db.getIncentiveById(_UserId, EntityId, IncentiveId);
        }
        public ResponeValues DeleteById(int EntityId, int IncentiveId)
        {
            return db.DeleteById(_UserId, EntityId, IncentiveId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.Incentive beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.IncentiveId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.IncentiveId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Incentive Name";
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