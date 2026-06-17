using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class GatePassEmployee
    {
        DA.FrontDesk.Transaction.GatePassEmployeeDB db = null;
        int _UserId = 0;

        public GatePassEmployee(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.GatePassEmployeeDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.GatePassEmployee beData)
        {
            bool isModify = beData.GatePassEmployeeId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.GatePassEmployeeCollections GetAllGatePassEmployee(int EntityId)
        {
            return db.getAllGatePassEmployee(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.GatePassEmployee GetGatePassEmployeeById(int EntityId, int GatePassEmployeeId)
        {
            return db.getGatePassEmployeeById(_UserId, EntityId, GatePassEmployeeId);
        }
        public ResponeValues DeleteById(int EntityId, int GatePassEmployeeId)
        {
            return db.DeleteById(_UserId, EntityId, GatePassEmployeeId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.GatePassEmployee beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.GatePassEmployeeId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.GatePassEmployeeId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Name))
                //{
                //    resVal.ResponseMSG = "Please ! Enter Name";
                //}
                //else if (beData.EmployeeId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Select Employee";
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
