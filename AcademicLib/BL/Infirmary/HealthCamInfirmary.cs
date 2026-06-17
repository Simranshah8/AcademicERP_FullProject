using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class HealthCamInfirmary
	{

		DA.HealthCamInfirmaryDB db = null;

		int _UserId = 0;

		public HealthCamInfirmary(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.HealthCamInfirmaryDB(hostName, dbName);
		}
		public BE.StudentForHCInfirmaryCollections GetAllStudentForHCInfirmary(int EntityId, int? ClassId)
		{
			return db.getStudentForHealthCampaign(_UserId, EntityId, ClassId);
		}


		public ResponeValues SaveFormData(BE.HealthCamInfirmary beData)
		{
			bool isModify = beData.HealthCamInfirmaryId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}

	
		public BE.HealthCamInfirmaryCollections GetAllHealthCamInfirmary(int EntityId)
		{
			return db.getAllHealthCamInfirmary(_UserId, EntityId);
		}
		public BE.HealthCamInfirmary GetHealthCamInfirmaryById(int EntityId, int HealthCamInfirmaryId)
		{
			return db.getHealthCamInfirmaryById(_UserId, EntityId, HealthCamInfirmaryId);
		}
		public ResponeValues DeleteById(int EntityId, int HealthCamInfirmaryId)
		{
			return db.DeleteById(_UserId, EntityId, HealthCamInfirmaryId);
		}

		public BE.HealtCampaignData GetDataForHealthCampaignById(int EntityId, int HealthCampaignId)
		{
			return db.getDataForHealthCampaignInfirmary(_UserId, EntityId, HealthCampaignId);
		}
		public BE.HealthCamInfirmary GetHealthCamInfirmaryForDetailById(int EntityId, int HealthCamInfirmaryId)
		{
			return db.getHealthCamInfirmaryForDetailById(_UserId, EntityId, HealthCamInfirmaryId);
		}
		public ResponeValues IsValidData(ref BE.HealthCamInfirmary beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.HealthCamInfirmaryId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.HealthCamInfirmaryId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				//Added By Suresh on 21 Chaitra
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

		//public ResponeValues SaveHealthCampaignInfirmary(BE.HealthCamInfirmary beData,BE.StudentForTestValueCollections dataColl)
		//{
		//	ResponeValues resVal = new ResponeValues();

		//	resVal = db.UpdateStudentHCInfirmary(_UserId, beData, dataColl);

		//	return resVal;
		//}
	}
}

