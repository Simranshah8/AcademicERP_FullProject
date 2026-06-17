using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class Room
    {
        DA.Hostel.RoomDB db = null;
        int _UserId = 0;

        public Room(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.RoomDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Hostel.Room beData)
        {
            bool isModify = beData.RoomId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Hostel.RoomCollections GetAllRoom(int EntityId)
        {
            return db.getAllRoom(_UserId, EntityId);
        }
        public AcademicLib.BE.Hostel.Room GetRoomById(int EntityId, int RoomId)
        {
            return db.getRoomById(_UserId, EntityId, RoomId);
        }
        public AcademicLib.BE.Hostel.RoomDetailsForMappingCollections getAllRoomForMapping()
        {
            return db.getAllRoomForMapping(_UserId);
        }
            public ResponeValues DeleteById(int EntityId, int RoomId)
        {
            return db.DeleteById(_UserId, EntityId, RoomId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Hostel.Room beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.RoomId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.RoomId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                //else if (string.IsNullOrEmpty(beData.Logo))
                //{
                //    resVal.ResponseMSG = "Please ! Select Logo";
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
