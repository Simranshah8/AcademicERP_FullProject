using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicLib.BL.Exam.Creation
{
	public class EvaluationArea
	{

		AcademicLib.DA.Exam.Creation.EvaluationAreaDB db = null;

		int _UserId = 0;

		public EvaluationArea(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new AcademicLib.DA.Exam.Creation.EvaluationAreaDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(AcademicLib.BE.Exam.Creation.EvaluationArea beData)
		{
			bool isModify = beData.EvaluationId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public AcademicLib.BE.Exam.Creation.EvaluationAreaCollections GetAllEvaluationArea(int EntityId)
		{
			return db.getAllEvaluationArea(_UserId, EntityId);
		}
		public AcademicLib.BE.Exam.Creation.EvaluationArea GetEvaluationAreaById(int EntityId, int EvaluationId)
		{
			return db.getEvaluationAreaById(_UserId, EntityId, EvaluationId);
		}
		public ResponeValues DeleteById(int EntityId, int EvaluationId)
		{
			return db.DeleteById(_UserId, EntityId, EvaluationId);
		}
		public ResponeValues IsValidData(ref AcademicLib.BE.Exam.Creation.EvaluationArea beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.EvaluationId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.EvaluationId != 0)
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