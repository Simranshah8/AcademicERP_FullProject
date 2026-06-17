using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Infrastructure
{

	public class Facilities
	{

		DA.Infrastructure.FacilitiesDB db = null;

		int _UserId = 0;

		public Facilities(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Infrastructure.FacilitiesDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Infrastructure.Facilities beData)
		{
			bool isModify = beData.FacilitiesId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Infrastructure.FacilitiesCollections GetAllFacilities(int EntityId)
		{
			return db.getAllFacilities(_UserId, EntityId);
		}
		public BE.Infrastructure.Facilities GetFacilitiesById(int EntityId, int FacilitiesId)
		{
			return db.getFacilitiesById(_UserId, EntityId, FacilitiesId);
		}
		public ResponeValues DeleteById(int EntityId, int FacilitiesId)
		{
			return db.DeleteById(_UserId, EntityId, FacilitiesId);
		}
		public ResponeValues IsValidData(ref BE.Infrastructure.Facilities beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.FacilitiesId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.FacilitiesId != 0)
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

