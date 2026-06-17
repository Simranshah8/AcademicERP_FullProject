using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class ExamCenterMapping
	{

		DA.Scholarship.ExamCenterMappingDB db = null;

		int _UserId = 0;

		public ExamCenterMapping(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Scholarship.ExamCenterMappingDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Scholarship.ExamCenterMapping beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Scholarship.ExamCenterMappingCollections GetAllExamCenterMapping(int EntityId)
		{
			return db.getAllExamCenterMapping(_UserId, EntityId);
		}
		public BE.Scholarship.ExamCenterMapping GetExamCenterMappingById(int EntityId, int TranId)
		{
			return db.getExamCenterMappingById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public ResponeValues IsValidData(ref BE.Scholarship.ExamCenterMapping beData, bool IsModify)
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
				else if (beData.ExamCenterId == 0)
				{
					resVal.ResponseMSG = "Please ! Select ExamCenter ";
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

