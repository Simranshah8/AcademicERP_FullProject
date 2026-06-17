using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class ExamCenter
	{

		DA.Scholarship.ExamCenterDB db = null;

		int _UserId = 0;

		public ExamCenter(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Scholarship.ExamCenterDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Scholarship.ExamCenter beData)
		{
			bool isModify = beData.ExamCenterId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Scholarship.ExamCenterCollections GetAllExamCenter(int EntityId)
		{
			return db.getAllExamCenter(_UserId, EntityId);
		}
		public BE.Scholarship.ExamCenter GetExamCenterById(int EntityId, int ExamCenterId)
		{
			return db.getExamCenterById(_UserId, EntityId, ExamCenterId);
		}
		public ResponeValues DeleteById(int EntityId, int ExamCenterId)
		{
			return db.DeleteById(_UserId, EntityId, ExamCenterId);
		}
		public ResponeValues IsValidData(ref BE.Scholarship.ExamCenter beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.ExamCenterId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ExamCenterId != 0)
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

