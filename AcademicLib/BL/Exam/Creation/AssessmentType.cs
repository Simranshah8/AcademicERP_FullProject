using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Exam.Creation
{

	public class AssessmentType
	{

		DA.Exam.Creation.AssessmentTypeDB db = null;

		int _UserId = 0;

		public AssessmentType(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Exam.Creation.AssessmentTypeDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Exam.Creation.AssessmentType beData)
		{
			bool isModify = beData.AssessmentTypeId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Exam.Creation.AssessmentTypeColl GetAllAssessmentType(int EntityId)
		{
			return db.getAllAssessmentType(_UserId, EntityId);
		}
		public BE.Exam.Creation.AssessmentType GetAssessmentById(int EntityId, int AssessmentTypeId)
		{
			return db.getAssessmentTypeById(_UserId, EntityId, AssessmentTypeId);
		}
		public ResponeValues DeleteById(int EntityId, int AssessmentTypeId)
		{
			return db.DeleteById(_UserId, EntityId, AssessmentTypeId);
		}
		public ResponeValues IsValidData(ref BE.Exam.Creation.AssessmentType beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.AssessmentTypeId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.AssessmentTypeId != 0)
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

