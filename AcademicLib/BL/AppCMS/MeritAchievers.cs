using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.AppCMS.Creation
{

	public class MeritAchievers
	{

		DA.AppCMS.Creation.MeritAchieversDB db = null;

		int _UserId = 0;

		public MeritAchievers(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.AppCMS.Creation.MeritAchieversDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.AppCMS.Creation.MeritAchievers beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.AppCMS.Creation.MeritAchieversCollections GetAllMeritAchievers(int EntityId)
		{
			return db.getAllMeritAchievers(_UserId, EntityId);
		}
		public BE.AppCMS.Creation.MeritAchievers GetMeritAchieversById(int EntityId, int TranId)
		{
			return db.getMeritAchieversById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}
		public ResponeValues IsValidData(ref BE.AppCMS.Creation.MeritAchievers beData, bool IsModify)
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

