using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Infrastructure
{
    public class FloorMapping
    {
        DA.Infrastructure.FloorMappingDB db = null;

        int _UserId = 0;

        public FloorMapping(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Infrastructure.FloorMappingDB(hostName, dbName);
        }

        public ResponeValues SaveFormDataColl(BE.Infrastructure.FloorMappingCollections dataColl)
        {
            ResponeValues resVal = new ResponeValues();

            resVal = db.SaveUpdateColl(_UserId, dataColl);

            return resVal;
        }
        public BE.Infrastructure.FloorMappingCollections GetAllFloorMapping(int EntityId, int? BuildingId)
        {
            return db.getAllFloorMapping(_UserId, EntityId, BuildingId);
        }
        public BE.Infrastructure.FloorMappingCollections GetFloorMappingById(int EntityId, int BuildingId)
        {
            return db.getFloorMappingById(_UserId, EntityId, BuildingId);
        }
        public BE.Infrastructure.FloorMapping DetailsByBuildingFloor(int UserId, int BuildingId, int FloorId)
        {
            return db.DetailsByBuildingFloor(_UserId,  BuildingId, FloorId);
        }
        public ResponeValues IsValidData(ref BE.Infrastructure.FloorMapping beData, bool IsModify)
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