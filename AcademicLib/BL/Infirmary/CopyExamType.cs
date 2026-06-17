using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class CopyExamType  
	{ 

		DA.CopyExamTypeDB db = null;

		int _UserId = 0;

		public CopyExamType(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.CopyExamTypeDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.CopyExamType beData)
		{
			bool isModify = beData.CopyExamTypeId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.CopyExamTypeCollections GetAllCopyExamType(int EntityId)
		{
			return db.getAllCopyExamType(_UserId, EntityId);
		}
		public BE.CopyExamType GetCopyExamTypeById(int EntityId, int CopyExamTypeId)
		{
			return db.getCopyExamTypeById(_UserId, EntityId, CopyExamTypeId);
		}
		public ResponeValues DeleteById(int EntityId, int CopyExamTypeId)
		{
			return db.DeleteById(_UserId, EntityId, CopyExamTypeId);
		}
		public ResponeValues IsValidData(ref BE.CopyExamType beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.CopyExamTypeId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.CopyExamTypeId != 0)
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

