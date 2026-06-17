using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Transaction
{

	public class ParentTeacherMeeting  
	{ 

		DA.Academic.Transaction.ParentTeacherMeetingDB db = null;
		int _UserId = 0;

		public ParentTeacherMeeting(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Transaction.ParentTeacherMeetingDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Transaction.ParentTeacherMeeting beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Transaction.ParentTeacherMeetingCollections GetAllParentTeacherMeeting(int EntityId)
		{
			return db.getAllParentTeacherMeeting(_UserId, EntityId);
		}
		public BE.Academic.Transaction.ParentTeacherMeeting GetParentTeacherMeetingById(int EntityId, int TranId)
		{
			return db.getParentTeacherMeetingById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public BE.Academic.Transaction.ParentTeacherMeeting GetAllStudentPTM(int EntityId,int ClassId, int? SectionId,DateTime? PTMDate, int? PTMBy)
		{
			return db.getAllStudentPTM(_UserId, EntityId,ClassId,SectionId,PTMDate,PTMBy);
		}
		public ResponeValues IsValidData(ref BE.Academic.Transaction.ParentTeacherMeeting beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try{
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
				else if (beData.ClassId == 0 || beData.ClassId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select Class ";
				}
				else if (!beData.PTMDate.HasValue || beData.PTMDate.Value.Year < 1900)
				{
					resVal.ResponseMSG = "Please ! Enter PTM Date";
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

