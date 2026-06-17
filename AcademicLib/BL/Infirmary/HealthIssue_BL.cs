using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicERP.BL
{

	public class HealthIssue
	{

		DA.HealthIssueDB db = null;

		int _UserId = 0;

		public HealthIssue(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.HealthIssueDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.HealthIssue beData)
		{
			bool isModify = beData.HealthIssueId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.HealthIssueCollections GetAllHealthIssue(int EntityId)
		{
			return db.getAllHealthIssue(_UserId, EntityId);
		}
		public BE.HealthIssue GetHealthIssueById(int EntityId, int HealthIssueId)
		{
			return db.getHealthIssueById(_UserId, EntityId, HealthIssueId);
		}
		public ResponeValues DeleteById(int EntityId, int HealthIssueId)
		{
			return db.DeleteById(_UserId, EntityId, HealthIssueId);
		}
		public ResponeValues IsValidData(ref BE.HealthIssue beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.HealthIssueId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.HealthIssueId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.Name))
				{
					resVal.ResponseMSG = "Please ! Enter Health Issue Name ";
				}

				//Added By Suresh on Baishakh 19
				else if (beData.Severity == 0 || beData.Severity.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select the Severity ";
				}
				//Ends
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

