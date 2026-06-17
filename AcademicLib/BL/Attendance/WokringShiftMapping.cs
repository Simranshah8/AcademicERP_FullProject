using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Attendance
{
    public class WokringShiftMapping
    {
        DA.Attendance.WorkingShiftMappingDB db = null;
        int _UserId = 0;
        public WokringShiftMapping(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Attendance.WorkingShiftMappingDB(hostName, dbName);
        }
        public ResponeValues SaveFormData(BE.Attendance.WorkingShiftMapping beData)
        {
            bool isModify = beData.ShiftMappingId > 0;
            ResponeValues isValid = IsValidData(ref beData, isModify);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData, isModify);
            else
                return isValid;
        }
        public BE.Attendance.WorkingShiftMappingCollections getAllWorkingShift()
        {
            return db.getAllShiftMapping(_UserId);
        }      
        public ResponeValues DeleteById(int EntityId, int DeviceId)
        {
            return db.DeleteById(_UserId, EntityId, DeviceId);
        }

        public ResponeValues IsValidData(ref BE.Attendance.WorkingShiftMapping beData, bool IsModify)
        {
            ResponeValues resVal = new ResponeValues();

            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else if (IsModify && beData.ShiftMappingId == 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
                }
                else if (!IsModify && beData.ShiftMappingId != 0)
                {
                    resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
                }
                else if (beData.CUserId == 0)
                {
                    resVal.ResponseMSG = "Invalid User for CRUD";
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
