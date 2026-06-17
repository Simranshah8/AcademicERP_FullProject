using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class HealthOperation  
	{ 

		DA.HealthOperationDB db = null;

		int _UserId = 0;

		public HealthOperation(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.HealthOperationDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.HealthOperation beData)
		{
			bool isModify = beData.HealthCampaignId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.HealthOperationCollections GetAllHealthOperation(int EntityId)
		{
			return db.getAllHealthOperation(_UserId, EntityId);
		}
		public BE.HealthOperation GetHealthOperationById(int EntityId, int HealthCampaignId)
		{
			return db.getHealthOperationById(_UserId, EntityId, HealthCampaignId);
		}
		public ResponeValues DeleteById(int EntityId, int HealthCampaignId)
		{
			return db.DeleteById(_UserId, EntityId, HealthCampaignId);
		}
		public ResponeValues IsValidData(ref BE.HealthOperation beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
			if (beData == null)
			{
				resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
			}
			else if (IsModify && beData.HealthCampaignId == 0)
			{
				resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
			}
			else if (!IsModify && beData.HealthCampaignId != 0)
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

