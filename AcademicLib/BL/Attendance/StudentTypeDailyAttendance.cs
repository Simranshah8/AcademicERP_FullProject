using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicLib.BL.Attendance
{

	public class StudentTypeDailyAttendance  
	{ 

		DA.Attendance.StudentTypeDailyAttendanceDB db = null;

		int _UserId = 0;

		public StudentTypeDailyAttendance(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Attendance.StudentTypeDailyAttendanceDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Attendance.StudentTypeDailyAttendance beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Attendance.StudentTypeDailyAttendanceCollections GetAllStudentTypeDailyAttendance(int EntityId)
		{
			return db.getAllStudentTypeDailyAttendance(_UserId, EntityId);
		}
		public BE.Attendance.StudentTypeDailyAttendance GetStudentTypeDailyAttendanceById(int EntityId, int TranId)
		{
			return db.getStudentTypeDailyAttendanceById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public BE.Attendance.StudentTypeDailyAttendanceCollections getTypeWiseStudentAttendance(int AcademicYearId, int StudentTypeId, int ClassId, int? SectionId, DateTime ForDate, int InOutMode = 2, int? BatchId = null, int? SemesterId = null, int? ClassYearId = null)
		{
			return db.getTypeWiseAttendance(_UserId, AcademicYearId, StudentTypeId, ClassId, SectionId, ForDate, InOutMode, BatchId, SemesterId, ClassYearId);

		}
		public ResponeValues SaveUpdateStudentTypeWise(BE.Attendance.StudentTypeDailyAttendanceCollections dataColl)
		{
			ResponeValues resVal = new ResponeValues();
			resVal = db.SaveUpdateStudentTypeWise(_UserId, dataColl);
			return resVal;
		}
		public ResponeValues IsValidData(ref BE.Attendance.StudentTypeDailyAttendance beData, bool IsModify)
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
			else if (beData.ForDate.Year<1901)
			{
				resVal.ResponseMSG = "Please ! Enter  ForDate ";
			}
			else if (beData.StudentTypeId==0 )
			{
				resVal.ResponseMSG = "Please ! Select StudentType ";
			}		 
			else if (beData.StudentId==0)
			{
				resVal.ResponseMSG = "Please ! Select Student ";
			}			 
			else if (beData.Attendance==0)
			{
				resVal.ResponseMSG = "Please ! Select Attendance ";
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

