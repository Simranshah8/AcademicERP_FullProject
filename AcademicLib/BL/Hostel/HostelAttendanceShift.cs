using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Hostel
{

	public class HostelAttendanceShift
	{

		DA.Hostel.HostelAttendanceShiftDB db = null;

		int _UserId = 0;

		public HostelAttendanceShift(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Hostel.HostelAttendanceShiftDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Hostel.HostelAttendanceShift beData)
		{
			bool isModify = beData.AttendanceShiftId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Hostel.HostelAttendanceShiftCollections GetAllHostelAttendanceShift(int EntityId)
		{
			return db.getAllHostelAttendanceShift(_UserId, EntityId);
		}
		public BE.Hostel.HostelAttendanceShift GetHostelAttendanceShiftById(int EntityId, int AttendanceShiftId)
		{
			return db.getHostelAttendanceShiftById(_UserId, EntityId, AttendanceShiftId);
		}
		public ResponeValues DeleteById(int EntityId, int AttendanceShiftId)
		{
			return db.DeleteById(_UserId, EntityId, AttendanceShiftId);
		}
		public ResponeValues IsValidData(ref BE.Hostel.HostelAttendanceShift beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.AttendanceShiftId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.AttendanceShiftId != 0)
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

