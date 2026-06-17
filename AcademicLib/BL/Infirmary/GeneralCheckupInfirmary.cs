using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class GeneralCheckupInfirmary
	{

		DA.GeneralCheckupInfirmaryDB db = null;

		int _UserId = 0;

		public GeneralCheckupInfirmary(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.GeneralCheckupInfirmaryDB(hostName, dbName);
		}
		public BE.StudentForGCInfirmaryCollections GetAllStudentForGChkupInfirmary(int EntityId, int? ClassId)
		{
			return db.getStudentForGCInfirmary(_UserId, EntityId, ClassId);
		}
		public ResponeValues SaveFormData(BE.GeneralCheckupInfirmary beData)
		{
			bool isModify = beData.TranId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}


		public BE.GeneralCheckupInfirmaryCollections GetAllGeneralCheckupInfirmary(int EntityId)
		{
			return db.getAllGeneralCheckupInfirmary(_UserId, EntityId);
		}
		public BE.GeneralCheckupInfirmary GetGeneralCheckupInfirmaryById(int EntityId, int TranId)
		{
			return db.getGeneralCheckupInfirmaryById(_UserId, EntityId, TranId);
		}
		public ResponeValues DeleteById(int EntityId, int TranId)
		{
			return db.DeleteById(_UserId, EntityId, TranId);
		}

		public BE.GeneralCheckUpData GetDataForGeneralCheckupById(int EntityId, int TranId)
		{
			return db.getDataForGeneralCheckupInfirmary(_UserId, EntityId, TranId);
		}
		public BE.GeneralCheckupInfirmary GetGeneralCheckupForDetailById(int EntityId, int TranId)
		{
			return db.getGeneralCheckupForDetailById(_UserId, EntityId, TranId);
		}
		public ResponeValues IsValidData(ref BE.GeneralCheckupInfirmary beData, bool IsModify)
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
				//Added By Suresh on 26 Baishakh
				else if (!beData.HealthCampaignId.HasValue || beData.HealthCampaignId == 0)
				{
					resVal.ResponseMSG = "Please ! Select Health Campaign ";
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

