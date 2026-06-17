using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class HealthChekUpExam  
	{ 

		DA.HealthChekUpExamDB db = null;

		int _UserId = 0;

		public HealthChekUpExam(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.HealthChekUpExamDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.HealthChekUpExam beData)
		{
			bool isModify = beData.ExamCheckUpId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.HealthChekUpExamCollections GetAllHealthChekUpExam(int EntityId)
		{
			return db.getAllHealthChekUpExam(_UserId, EntityId);
		}
		public BE.HealthChekUpExam GetHealthChekUpExamById(int EntityId, int ExamCheckUpId)
		{
			return db.getHealthChekUpExamById(_UserId, EntityId, ExamCheckUpId);
		}
		public ResponeValues DeleteById(int EntityId, int ExamCheckUpId)
		{
			return db.DeleteById(_UserId, EntityId, ExamCheckUpId);
		}

		
		public ResponeValues IsValidData(ref BE.HealthChekUpExam beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.ExamCheckUpId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.ExamCheckUpId != 0)
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

