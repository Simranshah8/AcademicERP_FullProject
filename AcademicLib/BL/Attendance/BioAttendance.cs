using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Attendance
{

	public class BioAttendence
	{

		DA.Attendance.BioAttendenceDB db = null;

		int _UserId = 0;

		public BioAttendence(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Attendance.BioAttendenceDB(hostName, dbName);
		}

		public ResponeValues SaveFormData(BE.Attendance.BioAttendenceCollections beData)
		{
			ResponeValues resVal = new ResponeValues();
			resVal = db.SaveUpdate(_UserId, beData);
			return resVal;
		}
		public BE.Attendance.BioAttendenceCollections GetAllBioAttendence(int? StudentId, int? EmployeeId)
		{
			return db.getAllBioAttendence(_UserId, StudentId, EmployeeId);
		}

		public ResponeValues IsValidData(ref BE.Attendance.BioAttendence beData, bool IsModify)
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

