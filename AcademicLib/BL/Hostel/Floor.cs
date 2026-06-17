using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class Floor
    {
        DA.Hostel.FloorDB db = null;
        int _UserId = 0;

        public Floor(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.FloorDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(AcademicLib.BE.Hostel.Floor beData)
        {
            bool isModify = beData.FloorId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public AcademicLib.BE.Hostel.FloorCollections GetAllFloor(int EntityId)
        {
            return db.getAllFloor(_UserId, EntityId);
        }
        public AcademicLib.BE.Hostel.Floor GetFloorById(int EntityId, int FloorId)
        {
            return db.getFloorById(_UserId, EntityId, FloorId);
        }
        public ResponeValues DeleteById(int EntityId, int FloorId)
        {
            return db.DeleteById(_UserId, EntityId, FloorId);
        }
        public ResponeValues IsValidData(ref AcademicLib.BE.Hostel.Floor beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.FloorId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.FloorId != 0)
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
