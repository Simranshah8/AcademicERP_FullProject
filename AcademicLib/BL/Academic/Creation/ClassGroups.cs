using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

	public class ClassGroups
	{

		DA.Academic.Creation.ClassGroupsDB db = null;

		int _UserId = 0;

		public ClassGroups(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Creation.ClassGroupsDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Creation.ClassGroups beData)
		{
			bool isModify = beData.ClassGroupId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Creation.ClassGroupsCollections GetAllClassGroups(int EntityId)
		{
			return db.getAllClassGroups(_UserId, EntityId);
		}
		public BE.Academic.Creation.ClassGroups GetClassGroupsById(int EntityId, int ClassGroupId)
		{
			return db.getClassGroupsById(_UserId, EntityId, ClassGroupId);
		}
		public ResponeValues DeleteById(int EntityId, int ClassGroupId)
		{
			return db.DeleteById(_UserId, EntityId, ClassGroupId);
		}
		public ResponeValues IsValidData(ref BE.Academic.Creation.ClassGroups beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.ClassGroupId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.ClassGroupId != 0)
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

