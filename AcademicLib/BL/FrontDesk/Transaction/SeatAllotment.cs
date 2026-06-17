using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.FrontDesk.Transaction
{
    public class SeatAllotment
    {
        DA.FrontDesk.Transaction.SeatAllotmentDB db = null;
        int _UserId = 0;

        public SeatAllotment(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.FrontDesk.Transaction.SeatAllotmentDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.FrontDesk.Transaction.SeatAllotment beData)
        {
            bool isModify = beData.SeatAllotmentId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.FrontDesk.Transaction.SeatAllotmentCollections GetAllSeatAllotment(int EntityId)
        {
            return db.getAllSeatAllotment(_UserId, EntityId);
        }
        public BE.FrontDesk.Transaction.SeatAllotment GetSeatAllotmentById(int EntityId, int SeatAllotmentId)
        {
            return db.getSeatAllotmentById(_UserId, EntityId, SeatAllotmentId);
        }
        public ResponeValues DeleteById(int EntityId, int SeatAllotmentId)
        {
            return db.DeleteById(_UserId, EntityId, SeatAllotmentId);
        }
        public ResponeValues IsValidData(ref BE.FrontDesk.Transaction.SeatAllotment beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.SeatAllotmentId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.SeatAllotmentId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.FormTitle))
                //{
                //    resVal.ResponseMSG = "Please ! Enter FormTitle";
                //}
                else if (beData.ShiftId == 0)
                {
                    resVal.ResponseMSG = "Please ! Enter Shift ";
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
