using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Infrastructure
{

	public class Utilities
	{

		DA.Infrastructure.UtilitiesDB db = null;

		int _UserId = 0;

		public Utilities(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Infrastructure.UtilitiesDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Infrastructure.Utilities beData)
		{
			bool isModify = beData.UtilitiesId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Infrastructure.UtilitiesCollections GetAllUtilities(int EntityId)
		{
			return db.getAllUtilities(_UserId, EntityId);
		}
		public BE.Infrastructure.Utilities GetUtilitiesById(int EntityId, int UtilitiesId)
		{
			return db.getUtilitiesById(_UserId, EntityId, UtilitiesId);
		}
		public ResponeValues DeleteById(int EntityId, int UtilitiesId)
		{
			return db.DeleteById(_UserId, EntityId, UtilitiesId);
		}
		public ResponeValues IsValidData(ref BE.Infrastructure.Utilities beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.UtilitiesId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.UtilitiesId != 0)
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

