using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class Examiner  
	{ 

		DA.ExaminerDB db = null;

		int _UserId = 0;

		public Examiner(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.ExaminerDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Examiner beData)
		{
			bool isModify = beData.ExaminerId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.ExaminerCollections GetAllExaminer(int EntityId)
		{
			return db.getAllExaminer(_UserId, EntityId);
		}
		public BE.Examiner GetExaminerById(int EntityId, int ExaminerId)
		{
			return db.getExaminerById(_UserId, EntityId, ExaminerId);
		}
		public ResponeValues DeleteById(int EntityId, int ExaminerId)
		{
			return db.DeleteById(_UserId, EntityId, ExaminerId);
		}
		public ResponeValues IsValidData(ref BE.Examiner beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.ExaminerId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.ExaminerId != 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
			}
			else if (beData.CUserId == 0)
			{
				resVal.ResponseMSG = "Invalid User for CRUD";
			}
 
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Examiner Name ";
				}
				else if (string.IsNullOrEmpty(beData.Designation))
				{
					resVal.ResponseMSG = "Please ! Enter Designation ";
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

