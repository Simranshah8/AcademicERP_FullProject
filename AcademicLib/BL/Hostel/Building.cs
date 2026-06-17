using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class Building
    {
        DA.Hostel.BuildingDB db = null;
        int _UserId = 0;

        public Building(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.BuildingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Hostel.Building beData)
        {
            bool isModify = beData.BuildingId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Hostel.BuildingCollections GetAllBuilding(int EntityId)
        {
            return db.getAllBuilding(_UserId, EntityId);
        }
        public AcademicLib.BE.Hostel.Building GetBuildingById(int EntityId, int BuildingId)
        {
            return db.getBuildingById(_UserId, EntityId, BuildingId);
        }
        public ResponeValues DeleteById(int EntityId, int BuildingId)
        {
            return db.DeleteById(_UserId, EntityId, BuildingId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Hostel.Building beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.BuildingId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.BuildingId != 0)
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
