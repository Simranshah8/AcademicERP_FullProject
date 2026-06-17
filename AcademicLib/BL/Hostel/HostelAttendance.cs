using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Hostel
{
    public class HostelAttendance
    {
        DA.Hostel.HostelAttendanceDB db = null;
        int _UserId = 0;
        public HostelAttendance(int UserId, string hostName, string dbName)
        {
            this._UserId = UserId;
            db = new DA.Hostel.HostelAttendanceDB(hostName, dbName);
        }
        public BE.Hostel.HostelAttendanceCollections getAllHostelAttendance(int AcademicYearId, int? HostelId, int? BuildingId, int? FloorId, DateTime? ForDate, int? ShiftId)
        {
            return db.getAllHostelAttendance(_UserId, AcademicYearId, HostelId, BuildingId, FloorId, ForDate, ShiftId);
        }
        public ResponeValues SaveFormData(BE.Hostel.HostelAttendanceCollections beData)
        {
            ResponeValues isValid = IsValidData(ref beData);
            if (isValid.IsSuccess)
                return db.SaveUpdate(beData);
            else
                return isValid;
        }
        public ResponeValues IsValidData(ref BE.Hostel.HostelAttendanceCollections beData)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (beData == null)
                {
                    resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
                }
                else
                {
                    foreach (var data in beData)
                    {
                        if (!data.HostelId.HasValue || data.HostelId <= 0)
                        {
                            resVal.ResponseMSG = "Please select a Hostel.";
                            return resVal;
                        }

                        if (!data.BuildingId.HasValue || data.BuildingId <= 0)
                        {
                            resVal.ResponseMSG = "Please select a Building.";
                            return resVal;
                        }

                        if (!data.FloorId.HasValue || data.FloorId <= 0)
                        {
                            resVal.ResponseMSG = "Please select a Floor.";
                            return resVal;
                        }

                        if (!data.ShiftId.HasValue || data.ShiftId <= 0)
                        {
                            resVal.ResponseMSG = "Please select a Shift.";
                            return resVal;
                        }

                        if (data.ForDate.Year < 2020)
                        {
                            resVal.ResponseMSG = "Please ! Enter For Date";
                            return resVal;
                        }
                        data.CUserId = _UserId;
                    }
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