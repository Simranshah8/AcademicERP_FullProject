using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Infrastructure
{

	public class LandDetails
	{

		DA.Infrastructure.LandDetailsDB db = null;

		int _UserId = 0;

		public LandDetails(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Infrastructure.LandDetailsDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Infrastructure.LandDetails beData)
		{
			bool isModify = beData.LandDetailsId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Infrastructure.LandDetailsCollections GetAllLandDetails(int EntityId)
		{
			return db.getAllLandDetails(_UserId, EntityId);
		}
		public BE.Infrastructure.LandDetails GetLandDetailsById(int EntityId, int LandDetailsId)
		{
			return db.getLandDetailsById(_UserId, EntityId, LandDetailsId);
		}
		public ResponeValues DeleteById(int EntityId, int LandDetailsId)
		{
			return db.DeleteById(_UserId, EntityId, LandDetailsId);
		}
		public ResponeValues IsValidData(ref BE.Infrastructure.LandDetails beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.LandDetailsId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.LandDetailsId != 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Save";
				}
				else if (beData.CUserId == 0)
				{
					resVal.ResponseMSG = "Invalid User for CRUD";
				}
				else if (string.IsNullOrEmpty(beData.TotalArea))
				{
					resVal.ResponseMSG = "Please ! Enter TotalArea ";
				}
				else if (beData.OwnerShipId == 0 || beData.OwnerShipId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select OwnerShip ";
				}
				else if (beData.UtilizationId == 0 || beData.UtilizationId.HasValue == false)
				{
					resVal.ResponseMSG = "Please ! Select Utilization ";
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

