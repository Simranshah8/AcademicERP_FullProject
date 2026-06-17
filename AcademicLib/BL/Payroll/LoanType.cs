using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Payroll
{
    public class LoanType
    {
        DA.Payroll.LoanTypeDB db = null;
        int _UserId = 0;
        public LoanType(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Payroll.LoanTypeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Payroll.LoanType beData)
        {
            bool isModify = beData.TranId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Payroll.LoanTypeCollections GetAllLoanType(int EntityId)
        {
            return db.getAllLoanType(_UserId, EntityId);
        }

        public AcademicLib.BE.Payroll.LoanType GetLoanTypeById(int EntityId, int TranId)
        {
            return db.getLoanTypeById(_UserId, EntityId, TranId);
        }
        public ResponeValues DeleteById(int EntityId, int TranId)
        {
            return db.DeleteById(_UserId, EntityId, TranId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.LoanType beData, bool IsModify)
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
                    resVal.ResponseMSG = "Please ! Enter LoanType Name";
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