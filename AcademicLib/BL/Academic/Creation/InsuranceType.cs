using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamic.DataAccess.Global;

namespace AcademicLib.BL.Academic.Creation
{

	public class InsuranceType
	{

		DA.Academic.Creation.InsuranceTypeDB db = null;

		int _UserId = 0;

		public InsuranceType(int UserId, string hostName, string dbName)
		{
			this._UserId = UserId;
			db = new DA.Academic.Creation.InsuranceTypeDB(hostName, dbName);
		}
		public ResponeValues SaveFormData(BE.Academic.Creation.InsuranceType beData)
		{
			bool isModify = beData.InsuranceId > 0;
			ResponeValues isValid = IsValidData(ref beData, isModify);
			if (isValid.IsSuccess)
				return db.SaveUpdate(beData, isModify);
			else
				return isValid;
		}
		public BE.Academic.Creation.InsuranceTypeCollections GetAllInsuranceType(int EntityId)
		{
			return db.getAllInsuranceType(_UserId, EntityId);
		}
		public BE.Academic.Creation.InsuranceType GetInsuranceTypeById(int EntityId, int InsuranceId)
		{
			return db.getInsuranceTypeById(_UserId, EntityId, InsuranceId);
		}
		public ResponeValues DeleteById(int EntityId, int InsuranceId)
		{
			return db.DeleteById(_UserId, EntityId, InsuranceId);
		}
		public ResponeValues IsValidData(ref BE.Academic.Creation.InsuranceType beData, bool IsModify)
		{
			ResponeValues resVal = new ResponeValues();
			try
			{
				if (beData == null)
				{
					resVal.ResponseMSG = GLOBALMSG.NO_DATA_FOUND;
				}
				else if (IsModify && beData.InsuranceId == 0)
				{
					resVal.ResponseMSG = GLOBALMSG.INVALID_DATA + " For Modify";
				}
				else if (!IsModify && beData.InsuranceId != 0)
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

