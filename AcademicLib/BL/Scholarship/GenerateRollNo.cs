using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Scholarship
{

	public class GenerateRollNo
	{

		DA.Scholarship.GenerateRollNoDB db = null;

		int _UserId = 0;

		public GenerateRollNo(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Scholarship.GenerateRollNoDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Scholarship.GenerateRollNo beData)
		{
			bool isModify = beData.GenerateId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Scholarship.GenerateRollNoCollections GetAllGenerateRollNo(int EntityId)
		{
			return db.getAllGenerateRollNo(_UserId, EntityId);
		}
		public BE.Scholarship.GenerateRollNo GetGenerateRollNoById(int EntityId, int GenerateId)
		{
			return db.getGenerateRollNoById(_UserId, EntityId, GenerateId);
		}
		public ResponeValues DeleteById(int EntityId, int GenerateId)
		{
			return db.DeleteById(_UserId, EntityId, GenerateId);
		}
		public ResponeValues IsValidData(ref BE.Scholarship.GenerateRollNo beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.GenerateId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.GenerateId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (beData.SubjectId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Subject ";
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

