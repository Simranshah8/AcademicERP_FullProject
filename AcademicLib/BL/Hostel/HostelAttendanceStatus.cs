using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Hostel
{

	public class HostelAttendanceStatus
	{

		DA.Hostel.HostelAttendanceStatusDB db = null;

		int _UserId = 0;

		public HostelAttendanceStatus(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Hostel.HostelAttendanceStatusDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Hostel.HostelAttendanceStatus beData)
		{
			bool isModify = beData.AttendanceStatusId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Hostel.HostelAttendanceStatusCollections GetAllHostelAttendanceStatus(int EntityId)
		{
			return db.getAllHostelAttendanceStatus(_UserId, EntityId);
		}
		public BE.Hostel.HostelAttendanceStatus GetHostelAttendanceStatusById(int EntityId, int AttendanceStatusId)
		{
			return db.getHostelAttendanceStatusById(_UserId, EntityId, AttendanceStatusId);
		}
		public ResponeValues DeleteById(int EntityId, int AttendanceStatusId)
		{
			return db.DeleteById(_UserId, EntityId, AttendanceStatusId);
		}
		public ResponeValues IsValidData(ref BE.Hostel.HostelAttendanceStatus beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.AttendanceStatusId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.AttendanceStatusId != 0)
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

