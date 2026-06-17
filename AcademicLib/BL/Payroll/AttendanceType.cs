using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Payroll
{

	public class AttendanceType
	{

		DA.Payroll.AttendanceTypeDB db = null;

		int _UserId = 0;

		public AttendanceType(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Payroll.AttendanceTypeDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Payroll.AttendanceType beData)
		{
			bool isModify = beData.AttendanceTypeId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Payroll.AttendanceTypeCollections GetAllAttendanceType(int EntityId)
		{
			return db.getAllAttendanceType(_UserId, EntityId);
		}
		public AcademicLib.BE.Payroll.AttendanceTypeCollections getAttendanceTypeForTran()
		{
			return db.getAttendanceTypeForTran(_UserId);
		}
			public AcademicLib.BE.Payroll.AttendanceType GetAttendanceTypeById(int EntityId, int AttendanceTypeId)
		{
			return db.getAttendanceTypeById(_UserId, EntityId, AttendanceTypeId);
		}
		public ResponeValues DeleteById(int EntityId, int AttendanceTypeId)
		{
			return db.DeleteById(_UserId, EntityId, AttendanceTypeId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Payroll.AttendanceType beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.AttendanceTypeId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.AttendanceTypeId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Name ";
				}
				else if (beData.Types == 0 )
				{
					resVal.ResponseMSG = "Please ! Select Types ";
				}
				
				else if (beData.PeriodType == 0 )
				{
					resVal.ResponseMSG = "Please ! Select PeriodType ";
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

