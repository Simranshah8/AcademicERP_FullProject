using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Exam.Transaction
{
    public class RoomDetails
    {
        DA.Exam.Transaction.RoomDetailsDB db = null;
        int _UserId = 0;

        public RoomDetails(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Exam.Transaction.RoomDetailsDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Exam.Transaction.RoomDetails beData)
        {
            bool isModify = beData.RoomDetailsId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Exam.Transaction.RoomDetailsCollections GetAllRoomDetails(int EntityId)
        {
            return db.getAllRoomDetails(_UserId, EntityId);
        }
        public BE.Exam.Transaction.RoomDetails GetRoomDetailsById(int EntityId, int RoomDetailsId)
        {
            return db.getRoomDetailsById(_UserId, EntityId, RoomDetailsId);
        }
        public ResponeValues DeleteById(int EntityId, int RoomDetailsId)
        {
            return db.DeleteById(_UserId, EntityId, RoomDetailsId);
        }
        public ResponeValues IsValidData(ref BE.Exam.Transaction.RoomDetails beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RoomDetailsId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RoomDetailsId != 0)
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
                //else if (beData.ShiftId == 0)
                //{
                //    resVal.ResponseMSG = "Please ! Enter Shift ";
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
