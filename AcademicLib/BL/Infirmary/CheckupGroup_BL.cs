using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class CheckupGroup  
	{ 

		DA.CheckupGroupDB db = null;

		int _UserId = 0;

		public CheckupGroup(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.CheckupGroupDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.CheckupGroup beData)
		{
			bool isModify = beData.CheckupGroupId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.CheckupGroupCollections GetAllCheckupGroup(int EntityId)
		{
			return db.getAllCheckupGroup(_UserId, EntityId);
		}
		public BE.CheckupGroup GetCheckupGroupById(int EntityId, int CheckupGroupId)
		{
			return db.getCheckupGroupById(_UserId, EntityId, CheckupGroupId);
		}
		public ResponeValues DeleteById(int EntityId, int CheckupGroupId)
		{
			return db.DeleteById(_UserId, EntityId, CheckupGroupId);
		}
		public ResponeValues IsValidData(ref BE.CheckupGroup beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.CheckupGroupId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.CheckupGroupId != 0)
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

