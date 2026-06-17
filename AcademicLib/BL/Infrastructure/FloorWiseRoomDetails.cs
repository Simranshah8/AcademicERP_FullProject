using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Infrastructure
{
    public class FloorWiseRoomDetails
    {
        DA.Infrastructure.FloorWiseRoomDetailsDB db = null;

        int _UserId = 0;

        public FloorWiseRoomDetails(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Infrastructure.FloorWiseRoomDetailsDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Infrastructure.FloorWiseRoomDetailsCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.SaveUpdate(_UserId, dataColl);

            return resVal;
        }
        //public ResponeValues SaveFormData(BE.Infrastructure.FloorWiseRoomDetails beData)
        //{
        //    bool isModify = beData.FloorWiseRoomDetailsId > 0;
        //    ResponeValues isValid = IsValidData(ref beData, isModify);
        //    if (isValid.IsSuccess)
        //        return db.SaveUpdate(beData, isModify);
        //    else
        //        return isValid;
        //}
        public BE.Infrastructure.FloorWiseRoomDetailsCollections GetAllFloorWiseRoomDetails(int EntityId, int? BuildingId,int? FloorId)
        {
            return db.getAllFloorWiseRoomDetails(_UserId, EntityId, BuildingId, FloorId);
        }
        public RE.Infrastructure.RoomDistributionCollections getAllRoomDistribution( int EntityId)
        {
            return db.getAllRoomDistribution(_UserId, EntityId);
        }
            //public ResponeValues DeleteById(int EntityId, int FloorWiseRoomDetailsId)
            //{
            //    return db.DeleteById(_UserId, EntityId, FloorWiseRoomDetailsId);
            //}
            public ResponeValues IsValidData(ref BE.Infrastructure.FloorWiseRoomDetails beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FloorWiseRoomDetailsId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FloorWiseRoomDetailsId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
                }
                else if (beData.BuildingId == 0 || beData.BuildingId.HasValue == false)
                {
                    resVal.ResponseMSG = "Please ! Select Building ";
                }
                else if (beData.FloorId == 0 || beData.FloorId.HasValue == false)
                {
                    resVal.ResponseMSG = "Please ! Select Floor ";
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